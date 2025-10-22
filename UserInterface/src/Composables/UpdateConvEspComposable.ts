import { ApiService } from '@/Services/ApiService'
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
  ConvenioEspecificoRequest: Ref<UpdateConvenioEspecificoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  Carreras: Carrera[]
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto>
  involucradosForm: Ref<InsertInvolucradosDto[]>
  getEmpresas: () => Promise<void>
  submitForm: () => Promise<ConvenioCreated | null>
}

export function useCreateConvEspComposable(): CreateConvenioEspecificoComposable {
  const IsLoading = ref(false)
  const ConvenioEspecificoRequest = ref<UpdateConvenioEspecificoRequestDto>(
    CreateUpdateRequestConvEspecifico(),
  )
  const InfoConvenioEspecificoCompleta = ref<InfoConvenioEspecificoDto | null>(null)
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const ConvenioCreado = ref<ConvenioCreated | null>(null)
  const Carreras: Carrera[] = carrerasList
  const route = useRoute()

  // --- COMPUTED PROPERTY ---
  const empresaForm = computed<InsertEmpresaDto>({
    get() {
      return (
        ConvenioEspecificoRequest.value.insertEmpresaDto ?? {
          id: null,
          nombre: null,
          razonSocial: null,
          cuit: null,
          direccion: null,
          telefono: null,
          email: null,
        }
      )
    },
    set(value) {
      if (Object.values(value).some((v) => v !== '' && v != null)) {
        ConvenioEspecificoRequest.value.insertEmpresaDto = { ...value }
      } else {
        ConvenioEspecificoRequest.value.insertEmpresaDto = null
      }
    },
  })

  const involucradosForm = computed<InsertInvolucradosDto[]>({
    get() {
      return ConvenioEspecificoRequest.value.insertInvolucradosDtos ?? []
    },
    set(value: InsertInvolucradosDto[] | null) {
      if (value && value.length > 0) {
        ConvenioEspecificoRequest.value.insertInvolucradosDtos = value
      } else {
        ConvenioEspecificoRequest.value.insertInvolucradosDtos = null
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
    errorMensaje.value = null
    try {
      const result = await ApiService.EditarConvenioEspecifico(ConvenioEspecificoRequest.value)
      if (!result.isSuccess) {
        errorMensaje.value = result.error.message
        return null
      }
      return result.value
    } catch (error) {
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
    const infoConvenioMarcoCompleta = await GetInfoConvenioEspecifico(id)
    await getEmpresas()

    ConvenioEspecificoRequest.value.updateConvenioDto.id = id
    ConvenioEspecificoRequest.value.updateConvenioDto.numeroConvenio =
      infoConvenioMarcoCompleta?.numeroConvenio ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.titulo =
      infoConvenioMarcoCompleta?.titulo ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.fechaFirmaConvenio =
      infoConvenioMarcoCompleta?.fechaFirmaConvenio ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.fechaInicioActividades =
      infoConvenioMarcoCompleta?.fechaInicioActividades ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.fechaFinConvenio =
      infoConvenioMarcoCompleta?.fechaFinConvenio ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.comentarioOpcional =
      infoConvenioMarcoCompleta?.comentarioOpcional ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.estado =
      infoConvenioMarcoCompleta?.estado ?? ConvenioEspecificoRequest.value.updateConvenioDto.estado
    ConvenioEspecificoRequest.value.updateConvenioDto.esActa =
      infoConvenioMarcoCompleta?.esActa ?? ConvenioEspecificoRequest.value.updateConvenioDto.esActa
    ConvenioEspecificoRequest.value.updateConvenioDto.numeroResolucion =
      infoConvenioMarcoCompleta?.numeroResolucion ?? null
    ConvenioEspecificoRequest.value.updateConvenioDto.refrendado =
      infoConvenioMarcoCompleta?.refrendado ??
      ConvenioEspecificoRequest.value.updateConvenioDto.refrendado
    ConvenioEspecificoRequest.value.idConvenioMarcoVinculado =
      infoConvenioMarcoCompleta?.convenioMarcoId ?? null
    ConvenioEspecificoRequest.value.idCarreras =
      infoConvenioMarcoCompleta?.carrerasInvolucradas?.map((c) => c.Id) ?? null

    IsLoading.value = false
  })

  return {
    IsLoading,
    ConvenioEspecificoRequest,
    InfoConvenioEspecificoCompleta,
    errorMensaje,
    empresas,
    Carreras,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    involucradosForm,
    getEmpresas,
    submitForm,
  }
}
