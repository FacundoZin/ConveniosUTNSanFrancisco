import router from '@/router'
import { ApiService } from '@/Services/ApiService'
import {
  UpdateRequestConvMarc,
  type UpdateConvenioMarcoRequetsDto,
} from '@/Types/ConvenioMarco/UpdateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
import type { ConvenioCreated, InfoConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { computed, onMounted, ref, type Ref } from 'vue'
import { useRoute } from 'vue-router'

interface CreateConvenioMarcoComposable {
  infoConvenioMarcoCompleta: Ref<InfoConvenioMarcoDto | null>
  ConvenioMarcoRequest: Ref<UpdateConvenioMarcoRequetsDto>
  errorMensaje: Ref<string | null>
  empresas: Ref<ComboBoxEmpresasDto[]>
  cargarNuevaEmpresa: Ref<boolean>
  empresaForm: Ref<InsertEmpresaDto>
  IsLoading: Ref<boolean>
  DesvincularConvenioEspecificos: (id: number) => void
  submitForm: () => Promise<ConvenioCreated | null>
  IrAConvenio: () => void
  IrAConvenioEspecifico: (idConvenioEspecifico: number) => void
}

export function useUpdateConvMarcoComposable(): CreateConvenioMarcoComposable {
  // --- ESTADO REACTIVO ---
  const infoConvenioMarcoCompleta = ref<InfoConvenioMarcoDto | null>(null)
  const ConvenioMarcoRequest = ref<UpdateConvenioMarcoRequetsDto>(UpdateRequestConvMarc())
  const errorMensaje = ref<string | null>(null)
  const empresas = ref<ComboBoxEmpresasDto[]>([])
  const cargarNuevaEmpresa = ref(false)
  const IsLoading = ref(true)

  const route = useRoute()

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

  const DesvincularConvenioEspecificos = (id: number) => {
    const currentValue = ConvenioMarcoRequest.value

    const newIdsArray = currentValue.idsConveniosEspecificosParaDesvincular
      ? [...currentValue.idsConveniosEspecificosParaDesvincular, id]
      : [id]

    ConvenioMarcoRequest.value = {
      ...currentValue,
      idsConveniosEspecificosParaDesvincular: newIdsArray,
    }
  }

  const getEmpresas = async () => {
    try {
      const response = await ApiService.GetEmpresas()
      if (response) empresas.value = response
    } catch (err) {
      console.error('Error al obtener empresas', err)
    }
  }

  const getInfoConvenio = async (id: number): Promise<InfoConvenioMarcoDto | null> => {
    try {
      const response = await ApiService.GetConvenioMarcoCompleto(id)
      if (response && response.isSuccess) {
        infoConvenioMarcoCompleta.value = response.value
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

  const IrAConvenio = () => {
    const id = parseInt(Array.isArray(route.params.id) ? route.params.id[0] : route.params.id)
    router.push({ name: 'VistaConvenioMarco', params: { id } })
  }

  const IrAConvenioEspecifico = (idConvenioEspecifico: number) => {
    router.push({ name: 'VistaConvenioEspecifico', params: { id: idConvenioEspecifico } })
  }

  const submitForm = async (): Promise<ConvenioCreated | null> => {
    errorMensaje.value = null
    try {
      const result = await ApiService.EditarConvenioMarco(ConvenioMarcoRequest.value)
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

  onMounted(async () => {
    IsLoading.value = true
    const id = parseInt(Array.isArray(route.params.id) ? route.params.id[0] : route.params.id)
    const InfoConvenioMarcoCompleta = await getInfoConvenio(id)
    await getEmpresas()
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.id = id
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.numeroConvenio =
      InfoConvenioMarcoCompleta?.numeroConvenio || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.titulo =
      InfoConvenioMarcoCompleta?.titulo || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.fechaFirmaConvenio =
      InfoConvenioMarcoCompleta?.fechaFirmaConvenio || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.fechaFin =
      InfoConvenioMarcoCompleta?.fechaFin || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.comentarioOpcional =
      InfoConvenioMarcoCompleta?.comentarioOpcional || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.estado =
      InfoConvenioMarcoCompleta?.estado || ConvenioMarcoRequest.value.updateConvenioMarcoDto.estado
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.numeroResolucion =
      InfoConvenioMarcoCompleta?.numeroResolucion || null
    ConvenioMarcoRequest.value.updateConvenioMarcoDto.refrendado =
      InfoConvenioMarcoCompleta?.refrendado ||
      ConvenioMarcoRequest.value.updateConvenioMarcoDto.refrendado

    IsLoading.value = false
  })

  return {
    infoConvenioMarcoCompleta,
    ConvenioMarcoRequest,
    errorMensaje,
    empresas,
    cargarNuevaEmpresa,
    empresaForm,
    IsLoading,
    DesvincularConvenioEspecificos,
    submitForm,
    IrAConvenio,
    IrAConvenioEspecifico,
  }
}
