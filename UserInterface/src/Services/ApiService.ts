import type { Result } from '@/Common/Result'
import type { CargarConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico'
import type { UpdateConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/UpdateConvenioEspecifico'
import type { CargarConvenioMarcoRequestDto } from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { UpdateConvenioMarcoRequetsDto } from '@/Types/ConvenioMarco/UpdateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { IConvenioQueryObject } from '@/Types/Filters'
import type {
  ConvenioCreated,
  ConvenioEspecificoDto,
  ConvenioMarcoDto,
  InfoConvenioEspecificoDto,
  InfoConvenioMarcoDto,
  ViewArchivoDto,
} from '@/Types/ViewModels/ViewModels'
import axios from 'axios'
import MockDataService from './MockDataService'

// ============================================================================
// CONFIGURACIÓN: Cambiar a false para usar la API real
// ============================================================================
const USE_MOCK_DATA = true

const API_URL = 'http://localhost:8888/api'

const getErrorMessage = (error: any) => {
  return error.response?.data?.message || 'Error de conexión o error desconocido.'
}

export default class ApiService {
  static async GetConvenios(
    body: IConvenioQueryObject,
  ): Promise<Result<ConvenioEspecificoDto[] | ConvenioMarcoDto[]>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetConvenios(body)
    }
    try {
      const response = await axios.post(`${API_URL}/Convenios`, body)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      if (Ex.response?.status === 404) {
        return { isSuccess: true, value:[], status: 404 }
      }
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async GetConvenioMarcoCompleto(id: number): Promise<Result<InfoConvenioMarcoDto>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetConvenioMarcoCompleto(id)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async GetConvenioEspecificoCompleto(
    id: number,
  ): Promise<Result<InfoConvenioEspecificoDto>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetConvenioEspecificoCompleto(id)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DeleteConvenioMarco(id: number): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DeleteConvenioMarco(id)
    }
    try {
      const response = await axios.delete(`${API_URL}/ConveniosMarcos/${id}`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DeleteConvenioEspecifico(id: number): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DeleteConvenioEspecifico(id)
    }
    try {
      const response = await axios.delete(`${API_URL}/ConveniosEspecificos/${id}`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async CreateConvenioMarco(
    Dto: CargarConvenioMarcoRequestDto,
  ): Promise<Result<ConvenioCreated>> {
    if (USE_MOCK_DATA) {
      return MockDataService.CreateConvenioMarco(Dto)
    }
    try {
      const response = await axios.post(`${API_URL}/ConveniosMarcos`, Dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async CreateConvenioEspecifico(
    Dto: CargarConvenioEspecificoRequestDto,
  ): Promise<Result<ConvenioCreated>> {
    if (USE_MOCK_DATA) {
      return MockDataService.CreateConvenioEspecifico(Dto)
    }
    try {
      const response = await axios.post(`${API_URL}/ConveniosEspecificos`, Dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async EditarConvenioMarco(Dto: UpdateConvenioMarcoRequetsDto): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.EditarConvenioMarco(Dto)
    }
    try {
      const response = await axios.put(`${API_URL}/ConveniosMarcos`, Dto)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async EditarConvenioEspecifico(
    Dto: UpdateConvenioEspecificoRequestDto,
  ): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.EditarConvenioEspecifico(Dto)
    }
    try {
      const response = await axios.put(`${API_URL}/ConveniosEspecificos`, Dto)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async GetEmpresas(): Promise<ComboBoxEmpresasDto[]> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetEmpresas()
    }
    const response = await axios.get(`${API_URL}/Empresa`)
    return response.data
  }

  static async GetIdConvMarcoByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetIdConvMarcoByNumeroConv(numeroConvenio)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/${numeroConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async GetIdConvEspByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetIdConvEspByNumeroConv(numeroConvenio)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/${numeroConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DesvincularConvenioEspecifico(
    idConvenioMarco: number,
    idConvenioEspecifico: number,
  ): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DesvincularConvenioEspecifico(idConvenioMarco, idConvenioEspecifico)
    }
    try {
      const response = await axios.delete(
        `${API_URL}/ConveniosMarcos/${idConvenioMarco}/especificos/${idConvenioEspecifico}`,
      )
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DesvincularEmpresaDeMarco(idConvenioMarco: number): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DesvincularEmpresaDeMarco(idConvenioMarco)
    }
    try {
      const response = await axios.delete(`${API_URL}/ConveniosMarcos/${idConvenioMarco}/empresa`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DesvincularConvenioMarco(idConvenioEspecifico: number): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DesvincularConvenioMarco(idConvenioEspecifico)
    }
    try {
      const response = await axios.delete(
        `${API_URL}/ConveniosEspecificos/${idConvenioEspecifico}/marco`,
      )
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async DesvincularEmpresaDeEspecifico(idConvenioEspecifico: number): Promise<Result<null>> {
    if (USE_MOCK_DATA) {
      return MockDataService.DesvincularEmpresaDeEspecifico(idConvenioEspecifico)
    }
    try {
      const response = await axios.delete(
        `${API_URL}/ConveniosEspecificos/${idConvenioEspecifico}/empresa`,
      )
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async CargarArchivoToMarco(
    nombreArchivo: string,
    file: File,
    convenioMarcoId: number,
  ): Promise<ViewArchivoDto | null> {
    if (USE_MOCK_DATA) {
      return MockDataService.CargarArchivoToMarco(nombreArchivo, file, convenioMarcoId)
    }
    const formData = new FormData()
    formData.append('NombreArchivo', nombreArchivo)
    formData.append('file', file)
    formData.append('ConvenioMarcoId', convenioMarcoId.toString())

    for (const pair of formData.entries()) {
      console.log(pair[0], pair[1])
    }

    try {
      const response = await axios.post<ViewArchivoDto>(`${API_URL}/Documents`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })

      console.log('Respuesta completa:', response)

      return response.data
    } catch (error: any) {
      console.error('AXIOS ERROR COMPLETO:', error)

      if (error.response) {
        console.error('STATUS:', error.response.status)
        console.error('DATA:', error.response.data)
        console.error('HEADERS:', error.response.headers)
      } else if (error.request) {
        console.error('NO RESPONSE. REQUEST fue:', error.request)
      } else {
        console.error('Error general:', error.message)
      }

      return null
    }
  }

  static async CargarArchivoToEspecifico(
    nombreArchivo: string,
    file: File,
    convenioEspecificoId: number,
  ): Promise<ViewArchivoDto | null> {
    if (USE_MOCK_DATA) {
      return MockDataService.CargarArchivoToEspecifico(nombreArchivo, file, convenioEspecificoId)
    }
    const formData = new FormData()
    formData.append('NombreArchivo', nombreArchivo)
    formData.append('file', file)
    formData.append('ConvenioEspecificoId', convenioEspecificoId.toString())

    for (const pair of formData.entries()) {
      console.log(pair[0], pair[1])
    }

    try {
      const response = await axios.post<ViewArchivoDto>(`${API_URL}/Documents`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })

      console.log('Respuesta completa:', response)

      return response.data
    } catch (error: any) {
      console.error('AXIOS ERROR COMPLETO:', error)

      if (error.response) {
        console.error('STATUS:', error.response.status)
        console.error('DATA:', error.response.data)
        console.error('HEADERS:', error.response.headers)
      } else if (error.request) {
        console.error('NO RESPONSE. REQUEST fue:', error.request)
      } else {
        console.error('Error general:', error.message)
      }

      return null
    }
  }

  static async EliminarArchivo(idDocumento: number): Promise<boolean> {
    if (USE_MOCK_DATA) {
      return MockDataService.EliminarArchivo(idDocumento)
    }
    try {
      const response = await axios.delete(`${API_URL}/Documents/${idDocumento}`, {})
      return true
    } catch (error) {
      console.error('Error eliminando archivo:', getErrorMessage(error))
      return false
    }
  }

  static async DescargarArchivo(idDocumento: number, nombreArchivo: string): Promise<void> {
    if (USE_MOCK_DATA) {
      return MockDataService.DescargarArchivo(idDocumento, nombreArchivo)
    }
    try {
      const response = await axios.get(`${API_URL}/Documents/${idDocumento}`, {
        responseType: 'blob',
      })

      // Crear un enlace temporal para descargar
      const url = window.URL.createObjectURL(response.data)
      const link = document.createElement('a')
      link.href = url
      link.download = nombreArchivo
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
    } catch (error) {
      console.error('Error descargando archivo:', error)
    }
  }

  static async GetArchivosConvMarco(idConvenio: number): Promise<Result<ViewArchivoDto[]>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetArchivosConvMarco(idConvenio)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/archivos${idConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }

  static async GetArchivosConvEspecifico(idConvenio: number): Promise<Result<ViewArchivoDto[]>> {
    if (USE_MOCK_DATA) {
      return MockDataService.GetArchivosConvEspecifico(idConvenio)
    }
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/archivos${idConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return {
        isSuccess: false,
        error: { message: getErrorMessage(Ex), status: Ex.response?.status },
      }
    }
  }
}
