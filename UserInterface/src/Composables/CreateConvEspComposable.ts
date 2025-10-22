import { ApiService } from '@/Services/ApiService'
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
import { computed, onMounted, ref, type Ref } from 'vue'

interface CreateConvenioEspecificoComposable {
  ConvenioEspecificoRequest: Ref<CargarConvenioEspecificoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  Carreras: Carrera[]
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto>
  involucradosForm: Ref<InsertInvolucradosDto[]>
  VincularConvenioMarco(NombreUnico: string): void
  getEmpresas: () => Promise<void>
  submitForm: () => Promise<ConvenioCreated | null>
  resetForm: () => void
}

export function useCreateConvEspComposable(): CreateConvenioEspecificoComposable {
  const ConvenioEspecificoRequest = ref<CargarConvenioEspecificoRequestDto>(
    createRequestConvEspecifico(),
  )
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const ConvenioCreado = ref<ConvenioCreated | null>(null)
  const Carreras: Carrera[] = carrerasList

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
      return ConvenioEspecificoRequest.value.insertInvolucradosDto ?? []
    },
    set(value: InsertInvolucradosDto[] | null) {
      if (value && value.length > 0) {
        ConvenioEspecificoRequest.value.insertInvolucradosDto = value
      } else {
        ConvenioEspecificoRequest.value.insertInvolucradosDto = null
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
      const result = await ApiService.CreateConvenioEspecifico(ConvenioEspecificoRequest.value)
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

  VincularConvenioMarco = (NombreUnico: string) => {
    //
  }

  const resetForm = () => {
    ConvenioEspecificoRequest.value = createRequestConvEspecifico()
    cargarNuevaEmpresa.value = false
  }

  onMounted(() => {
    getEmpresas()
  })

  return {
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
