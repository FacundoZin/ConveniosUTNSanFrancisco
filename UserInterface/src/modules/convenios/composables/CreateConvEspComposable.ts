import ApiService from '@/Services/ApiService'
import { carrerasList, type Carrera } from '@/Types/CarrerasInvolucradas/CarrerasInvolucradas'
import {
  createRequestConvEspecifico,
  type CargarConvenioEspecificoRequestDto,
} from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico'

import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import type { ConvenioCreated } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { onMounted, ref, type Ref, watch } from 'vue'

interface CreateConvenioEspecificoComposable {
  IsLoading: Ref<boolean>
  ConvenioEspecificoRequest: Ref<CargarConvenioEspecificoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  Carreras: Carrera[]
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto>
  involucradosForm: Ref<InsertInvolucradosDto[]>
  getEmpresas: () => Promise<void>
  submitForm: () => Promise<ConvenioCreated | null>
  resetForm: () => void
}

export function useCreateConvEspComposable(): CreateConvenioEspecificoComposable {
  const IsLoading = ref(false)
  const ConvenioEspecificoRequest = ref<CargarConvenioEspecificoRequestDto>(
    createRequestConvEspecifico(),
  )
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const ConvenioCreado = ref<ConvenioCreated | null>(null)
  const Carreras: Carrera[] = carrerasList

  const empresaForm = ref<InsertEmpresaDto>({
    id: null,
    nombre: null,
    razonSocial: null,
    cuit: null,
    direccion: null,
    telefono: null,
    email: null,
  })

  const involucradosForm = ref<InsertInvolucradosDto[]>([])

  // Watchers para sincronizar los formularios locales con el request principal

  watch(
    empresaForm,
    (newValue: InsertEmpresaDto) => {
      if (Object.values(newValue).some((v) => v !== '' && v != null)) {
        ConvenioEspecificoRequest.value.insertEmpresaDto = { ...newValue }
      } else {
        ConvenioEspecificoRequest.value.insertEmpresaDto = null
      }
    },
    { deep: true },
  )

  watch(
    involucradosForm,
    (newValue: InsertInvolucradosDto[]) => {
      if (newValue && newValue.length > 0) {
        ConvenioEspecificoRequest.value.insertInvolucradosDto = newValue
      } else {
        ConvenioEspecificoRequest.value.insertInvolucradosDto = null
      }
    },
    { deep: true },
  )

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
    try {
      const result = await ApiService.CreateConvenioEspecifico(ConvenioEspecificoRequest.value)
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

  const resetForm = () => {
    ConvenioEspecificoRequest.value = createRequestConvEspecifico()
    cargarNuevaEmpresa.value = false
    
    // Resetear formularios locales
    empresaForm.value = {
      id: null,
      nombre: null,
      razonSocial: null,
      cuit: null,
      direccion: null,
      telefono: null,
      email: null,
    }
    involucradosForm.value = []
  }

  onMounted(async () => {
    IsLoading.value = true
    await getEmpresas()
    IsLoading.value = false
  })

  return {
    IsLoading,
    ConvenioEspecificoRequest,
    errorMensaje,
    empresas,
    Carreras,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    involucradosForm,
    getEmpresas,
    submitForm,
    resetForm,
  }
}
