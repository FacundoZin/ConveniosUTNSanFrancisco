import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { ViewArchivoDto } from '@/Types/ViewModels/ViewModels'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class DocumentService {
  static async CargarArchivoToMarco(nombreArchivo: string, file: File, convenioMarcoId: number): Promise<ViewArchivoDto | null> {
    const formData = new FormData()
    formData.append('NombreArchivo', nombreArchivo)
    formData.append('file', file)
    formData.append('ConvenioMarcoId', convenioMarcoId.toString())

    try {
      const response = await axios.post<ViewArchivoDto>(`${API_URL}/Documents`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })
      return response.data
    } catch (error: any) {
      console.error('Error cargando archivo a marco:', error)
      return null
    }
  }

  static async CargarArchivoToEspecifico(nombreArchivo: string, file: File, convenioEspecificoId: number): Promise<ViewArchivoDto | null> {
    const formData = new FormData()
    formData.append('NombreArchivo', nombreArchivo)
    formData.append('file', file)
    formData.append('ConvenioEspecificoId', convenioEspecificoId.toString())

    try {
      const response = await axios.post<ViewArchivoDto>(`${API_URL}/Documents`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })
      return response.data
    } catch (error: any) {
      console.error('Error cargando archivo a específico:', error)
      return null
    }
  }

  static async EliminarArchivo(idDocumento: number): Promise<boolean> {
    try {
      await axios.delete(`${API_URL}/Documents/${idDocumento}`)
      return true
    } catch (error) {
      console.error('Error eliminando archivo:', getErrorMessage(error))
      return false
    }
  }

  static async DescargarArchivo(idDocumento: number, nombreArchivo: string): Promise<void> {
    try {
      const response = await axios.get(`${API_URL}/Documents/${idDocumento}`, { responseType: 'blob' })
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
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/archivos/${idConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetArchivosConvEspecifico(idConvenio: number): Promise<Result<ViewArchivoDto[]>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/archivos/${idConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
