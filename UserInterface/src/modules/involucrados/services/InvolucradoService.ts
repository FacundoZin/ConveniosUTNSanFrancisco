import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { ComboBoxInvolucradosDto } from '@/Types/Involucrados/ComboBoxInvolucradosDto'
import type { ValidateInvolucradoDto } from '@/Types/Involucrados/ValidateInvolucradoDto'
import type { InvolucradoExistDto } from '@/Types/Involucrados/InvolucradoExistDto'
import type { TableInvolucradosByAreaDto } from '@/Types/Involucrados/InvolucradosByArea'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class InvolucradoService {
  static async GetAllInvolucrados(): Promise<Result<ComboBoxInvolucradosDto[]>> {
    try {
      const response = await axios.get(`${API_URL}/Involucrados`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetInvolucradosDisponibles(idConvenio: number): Promise<Result<ComboBoxInvolucradosDto[]>> {
    try {
      const response = await axios.get(`${API_URL}/Involucrados/available/${idConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async ValidateInvolucrado(dto: ValidateInvolucradoDto): Promise<Result<InvolucradoExistDto>> {
    try {
      const response = await axios.post(`${API_URL}/Involucrados/validate`, dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetInvolucradosByArea(areaId: number): Promise<Result<TableInvolucradosByAreaDto>> {
    try {
      const response = await axios.get(`${API_URL}/Involucrados/area/${areaId}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
