import { ApiService } from '@/Services/ApiService'
import {
  createRequestConvMarc,
  type CargarConvenioMarcoRequestDto,
} from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
import type { ConvenioCreated } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { computed, onMounted, ref, type Ref } from 'vue'

// Define el tipo de retorno para que TypeScript sepa qué devuelve el composable
interface CreateConvenioMarcoComposable {
  ConvenioMarcoRequest: Ref<CargarConvenioMarcoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto> // Es un computed, pero se trata como Ref en el retorno
  getEmpresas: () => Promise<void>
  submitForm: () => Promise<ConvenioCreated | null>
  resetForm: () => void
}

export function useCreateConvenioMarcoForm(): CreateConvenioMarcoComposable {
  // --- ESTADO REACTIVO ---
  const ConvenioMarcoRequest = ref<CargarConvenioMarcoRequestDto>(createRequestConvMarc())
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const ConvenioCreado = ref<ConvenioCreated | null>(null)

  // --- COMPUTED PROPERTY ---
  const empresaForm = computed<InsertEmpresaDto>({
    get() {
      return (
        ConvenioMarcoRequest.value.insertEmpresaDto ?? {
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
        ConvenioMarcoRequest.value.insertEmpresaDto = { ...value }
      } else {
        ConvenioMarcoRequest.value.insertEmpresaDto = null
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
      const result = await ApiService.CreateConvenioMarco(ConvenioMarcoRequest.value)
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

  const resetForm = () => {
    ConvenioMarcoRequest.value = createRequestConvMarc()
    cargarNuevaEmpresa.value = false
  }

  onMounted(() => {
    getEmpresas()
  })

  return {
    ConvenioMarcoRequest,
    errorMensaje,
    empresas,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    getEmpresas,
    submitForm,
    resetForm,
  }
}
