import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { PaginatedResult } from '@/Common/PaginatedResult'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { EmpresaWithConveniosDto } from '@/Types/Empresa/EmpresaWithConveniosDto'
import type { EditEmpresaDto } from '@/Types/Empresa/EditEmpresaDto'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class EmpresaService {
  /**
   * Obtiene una lista paginada de empresas.
   */
  static async GetEmpresasPaginado(pagina: number = 1, cantidad: number = 12): Promise<PaginatedResult<ComboBoxEmpresasDto[]>> {
    const response = await axios.get(`${API_URL}/Empresa`, {
      params: { pagina, cantidad }
    })
    return response.data
  }

  /**
   * Obtiene todas las empresas (generalmente para comboBox)
   */
  static async GetTodasLasEmpresas(): Promise<ComboBoxEmpresasDto[]> {
    const response = await axios.get(`${API_URL}/Empresa/all`)
    return response.data
  }

  /**
   * @deprecated Use GetEmpresasPaginado o GetTodasLasEmpresas según corresponda. 
   * Mantenido por compatibilidad temporal si es necesario.
   */
  static async GetEmpresas(): Promise<ComboBoxEmpresasDto[]> {
    return this.GetTodasLasEmpresas();
  }

  static async GetConveniosPorEmpresa(id: number): Promise<Result<EmpresaWithConveniosDto>> {
    try {
      const response = await axios.get(`${API_URL}/Convenios/empresa/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async EditarInfoEmpresa(id: number, dto: EditEmpresaDto): Promise<Result<void>> {
    try {
      const response = await axios.put(`${API_URL}/Empresa/${id}`, dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async CrearEmpresa(dto: InsertEmpresaDto): Promise<Result<number>> {
    try {
      const response = await axios.post(`${API_URL}/Empresa`, dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
