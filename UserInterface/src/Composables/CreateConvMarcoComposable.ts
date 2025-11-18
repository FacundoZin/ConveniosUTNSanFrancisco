import ApiService from '@/Services/ApiService'
import {
  createRequestConvMarc,
  type CargarConvenioMarcoRequestDto,
} from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
import type { ConvenioCreated } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { computed, onMounted, ref, watch, type Ref } from 'vue'

// Define el tipo de retorno para que TypeScript sepa qué devuelve el composable
interface CreateConvenioMarcoComposable {
  IsLoading: Ref<boolean>
  ConvenioMarcoRequest: Ref<CargarConvenioMarcoRequestDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  cargarNuevaEmpresa: Ref<boolean>
  ConvenioCreado: Ref<ConvenioCreated | null>
  empresaForm: Ref<InsertEmpresaDto> // Es un computed, pero se trata como Ref en el retorno
  submitForm: () => Promise<ConvenioCreated | null>
  resetForm: () => void
}

export function useCreateConvMarcoComposable(): CreateConvenioMarcoComposable {
  // --- ESTADO REACTIVO ---
  const IsLoading = ref(false)
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
          nombre: '',
          razonSocial: '',
          cuit: '',
          direccion: '',
          telefono: '',
          email: '',
        }
      )
    },
    set(value) {
      const allEmpty = (
        [
          'nombre',
          'razonSocial',
          'cuit',
          'direccion',
          'telefono',
          'email',
        ] as (keyof InsertEmpresaDto)[]
      ).every((key) => !value[key] || value[key] === '')

      if (allEmpty) {
        ConvenioMarcoRequest.value.insertEmpresaDto = null
      } else {
        // Clonamos el objeto para que Vue detecte la actualización
        ConvenioMarcoRequest.value.insertEmpresaDto = { ...value }
      }
    },
  })

  const getEmpresas = async () => {
    try {
      const response = await ApiService.GetEmpresas()

      if (response) {
        empresas.value = response
      }
    } catch (err) {
      console.error('Error al obtener empresas', err)
    }
  }

  const submitForm = async (): Promise<ConvenioCreated | null> => {
    IsLoading.value = true
    errorMensaje.value = null
    try {
      const result = await ApiService.CreateConvenioMarco(ConvenioMarcoRequest.value)
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
    ConvenioMarcoRequest.value = createRequestConvMarc()
    cargarNuevaEmpresa.value = false
  }

  onMounted(async () => {
    IsLoading.value = true
    await getEmpresas()
    IsLoading.value = false
  })

  watch(cargarNuevaEmpresa, (nuevoValor) => {
    if (nuevoValor && !ConvenioMarcoRequest.value.insertEmpresaDto) {
      ConvenioMarcoRequest.value.insertEmpresaDto = {
        id: null,
        nombre: '',
        razonSocial: '',
        cuit: '',
        direccion: '',
        telefono: '',
        email: '',
      }
    }
  })

  return {
    IsLoading,
    ConvenioMarcoRequest,
    errorMensaje,
    empresas,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    submitForm,
    resetForm,
  }
}
