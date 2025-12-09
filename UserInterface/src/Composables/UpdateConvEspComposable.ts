import ApiService from '@/Services/ApiService'
import { carrerasList, type Carrera } from '@/Types/CarrerasInvolucradas/CarrerasInvolucradas'
import {
  CreateUpdateRequestConvEspecifico,
  type UpdateConvenioEspecificoRequestDto,
} from '@/Types/ConvenioEspecifico/UpdateConvenioEspecifico'

import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import type { ConvenioCreated, InfoConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { computed, onMounted, ref, type Ref } from 'vue'
import { useRoute } from 'vue-router'

interface CreateConvenioEspecificoComposable {
  IsLoading: Ref<boolean>
  InfoConvenioEspecificoCompleta: Ref<InfoConvenioEspecificoDto | null>
  UpdateConvEspRequest: Ref<UpdateConvenioEspecificoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  Carreras: Carrera[]
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto>
  involucradosForm: Ref<InsertInvolucradosDto[]>
  submitForm: () => Promise<ConvenioCreated | null>
  GetInfoConvenioEspecifico: (id: number) => Promise<InfoConvenioEspecificoDto | null>
}

export function UseUpdateConvEspComposable(): CreateConvenioEspecificoComposable {
  const IsLoading = ref(false)
  const UpdateConvEspRequest = ref<UpdateConvenioEspecificoRequestDto>(
    CreateUpdateRequestConvEspecifico(),
  )
  const InfoConvenioEspecificoCompleta = ref<InfoConvenioEspecificoDto | null>(null)
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const ConvenioCreado = ref<ConvenioCreated | null>(null)
  const Carreras: Carrera[] = carrerasList
  const route = useRoute()

  // --- STATE ---
  const empresaForm = ref<InsertEmpresaDto>({
    id: null,
    nombre: '',
    razonSocial: '',
    cuit: '',
    direccion: '',
    telefono: '',
    email: '',
  })

  const involucradosForm = computed<InsertInvolucradosDto[]>({
    get() {
      return UpdateConvEspRequest.value.insertInvolucradosDtos ?? []
    },
    set(value: InsertInvolucradosDto[] | null) {
      if (value && value.length > 0) {
        UpdateConvEspRequest.value.insertInvolucradosDtos = value
      } else {
        UpdateConvEspRequest.value.insertInvolucradosDtos = null
      }
    },
  })

  const getEmpresas = async () => {
    try {
      const response = await ApiService.GetEmpresas()
      if (response) empresas.value = response
    } catch (err) {
      console.error('Error al obtener empresas', err)
    }
  }

  const submitForm = async (): Promise<ConvenioCreated | null> => {
    IsLoading.value = true
    errorMensaje.value = null

    // Mapear empresaForm a insertEmpresaDto
    if (cargarNuevaEmpresa.value) {
      // Si es nueva empresa, enviamos todos los datos
      UpdateConvEspRequest.value.insertEmpresaDto = { ...empresaForm.value, id: null }
    } else if (empresaForm.value.id) {
      // Si es empresa existente, solo enviamos el ID
      UpdateConvEspRequest.value.insertEmpresaDto = {
        id: empresaForm.value.id,
        nombre: '',
        razonSocial: '',
        cuit: '',
        direccion: '',
        telefono: '',
        email: '',
      }
    } else {
      UpdateConvEspRequest.value.insertEmpresaDto = null
    }

    try {
      console.log('UpdateConvEspRequest:', JSON.parse(JSON.stringify(UpdateConvEspRequest.value)))
      const result = await ApiService.EditarConvenioEspecifico(UpdateConvEspRequest.value)
      if (!result.isSuccess) {
        IsLoading.value = false
        errorMensaje.value = result.error.message
        return null
      }
      IsLoading.value = false
      return result.value
    } catch (error) {
      IsLoading.value = false
      errorMensaje.value = 'Ocurrió un error al cargar el convenio'
      if (isAxiosError(error)) {
        if (error.response) {
          console.log(
            `Error al cargar el convenio (${error.response.status}):`,
            error.response.data,
          )
        } else {
          console.log('Error al cargar el convenio: no se recibió respuesta del servidor')
        }
      } else {
        console.error(error)
      }
      return null
    }
  }

  const GetInfoConvenioEspecifico = async (
    id: number,
  ): Promise<InfoConvenioEspecificoDto | null> => {
    try {
      const response = await ApiService.GetConvenioEspecificoCompleto(id)
      if (response && response.isSuccess) {
        InfoConvenioEspecificoCompleta.value = response.value
        return response.value
      } else {
        console.error('Error al obtener información del convenio', response?.error.message)
        return null
      }
    } catch (error) {
      console.error('Error al obtener información del convenio', error)
      return null
    }
  }

  onMounted(async () => {
    IsLoading.value = true
    const id = parseInt(Array.isArray(route.params.id) ? route.params.id[0] : route.params.id)
    const infoConvenioEspecificoCompleta = await GetInfoConvenioEspecifico(id)
    await getEmpresas()

    UpdateConvEspRequest.value.updateConvenioDto.id = id
    UpdateConvEspRequest.value.updateConvenioDto.numeroConvenio =
      infoConvenioEspecificoCompleta?.numeroconvenio ?? null
    UpdateConvEspRequest.value.updateConvenioDto.titulo =
      infoConvenioEspecificoCompleta?.titulo ?? null
    UpdateConvEspRequest.value.updateConvenioDto.fechaFirmaConvenio =
      infoConvenioEspecificoCompleta?.fechaFirmaConvenio ?? null
    UpdateConvEspRequest.value.updateConvenioDto.fechaInicioActividades =
      infoConvenioEspecificoCompleta?.fechaInicioActividades ?? null
    UpdateConvEspRequest.value.updateConvenioDto.fechaFinConvenio =
      infoConvenioEspecificoCompleta?.fechaFinConvenio ?? null
    UpdateConvEspRequest.value.updateConvenioDto.comentarioOpcional =
      infoConvenioEspecificoCompleta?.comentarioOpcional ?? null
    UpdateConvEspRequest.value.updateConvenioDto.estado = infoConvenioEspecificoCompleta!.estado
    UpdateConvEspRequest.value.updateConvenioDto.esActa = infoConvenioEspecificoCompleta!.esActa
    UpdateConvEspRequest.value.updateConvenioDto.numeroResolucion =
      infoConvenioEspecificoCompleta?.numeroResolucion ?? null
    UpdateConvEspRequest.value.updateConvenioDto.refrendado =
      infoConvenioEspecificoCompleta!.refrendado
    UpdateConvEspRequest.value.idMarcoVinculado =
      infoConvenioEspecificoCompleta?.convenioMarcoId ?? null
    UpdateConvEspRequest.value.idCarreras =
      infoConvenioEspecificoCompleta?.carrerasInvolucradas
        ?.map((c) => c.id)
        .filter((id): id is number => id !== undefined) ?? null

    IsLoading.value = false
  })

  return {
    IsLoading,
    UpdateConvEspRequest,
    InfoConvenioEspecificoCompleta,
    errorMensaje,
    empresas,
    Carreras,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    involucradosForm,
    submitForm,
    GetInfoConvenioEspecifico,
  }
}
