import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { IConvenioQueryObject } from '@/Types/Filters'
import type { CantidadConveniosDto } from '@/Types/Convenios/CantidadConveniosDto'
import type { 
  ConvenioEspecificoDto, 
  ConvenioMarcoDto, 
  InfoConvenioMarcoDto, 
  InfoConvenioEspecificoDto,
  ConvenioCreated
} from '@/Types/ViewModels/ViewModels'
import type { CargarConvenioMarcoRequestDto } from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { CargarConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico'
import type { UpdateConvenioMarcoRequetsDto } from '@/Types/ConvenioMarco/UpdateConvenioMarco'
import type { UpdateConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/UpdateConvenioEspecifico'
import type { UltimosConveniosDto } from '@/Types/Convenios/ConvenioUltimoDto'
import type { ComboBoxConvenioMarcoDto } from '@/Types/ConvenioMarco/ComboBoxConvenioMarcoDto'
import type { InvolucradosWithConveniosDto } from '@/Types/Involucrados/InvolucradosWithConveniosDto'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class ConvenioService {
  static async GetConvenios(
    body: IConvenioQueryObject,
  ): Promise<Result<ConvenioEspecificoDto[] | ConvenioMarcoDto[] | number | CantidadConveniosDto>> {
    try {
      const response = await axios.post(`${API_URL}/Convenios`, body)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      if (Ex.response?.status === 404) {
        return { isSuccess: true, value: [], status: 404 }
      }
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetConvenioMarcoCompleto(id: number): Promise<Result<InfoConvenioMarcoDto>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetConvenioEspecificoCompleto(id: number): Promise<Result<InfoConvenioEspecificoDto>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DeleteConvenioMarco(id: number): Promise<Result<null>> {
    try {
      const response = await axios.delete(`${API_URL}/ConveniosMarcos/${id}`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DeleteConvenioEspecifico(id: number): Promise<Result<null>> {
    try {
      const response = await axios.delete(`${API_URL}/ConveniosEspecificos/${id}`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async CreateConvenioMarco(Dto: CargarConvenioMarcoRequestDto): Promise<Result<ConvenioCreated>> {
    try {
      const response = await axios.post(`${API_URL}/ConveniosMarcos`, Dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async CreateConvenioEspecifico(Dto: CargarConvenioEspecificoRequestDto): Promise<Result<ConvenioCreated>> {
    try {
      const response = await axios.post(`${API_URL}/ConveniosEspecificos`, Dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async EditarConvenioMarco(Dto: UpdateConvenioMarcoRequetsDto): Promise<Result<null>> {
    try {
      const response = await axios.put(`${API_URL}/ConveniosMarcos`, Dto)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async EditarConvenioEspecifico(Dto: UpdateConvenioEspecificoRequestDto): Promise<Result<null>> {
    try {
      const response = await axios.put(`${API_URL}/ConveniosEspecificos`, Dto)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetIdConvMarcoByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos/${numeroConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetIdConvEspByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosEspecificos/${numeroConvenio}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DesvincularConvenioEspecifico(idConvenioMarco: number, idConvenioEspecifico: number): Promise<Result<null>> {
    try {
      const response = await axios.patch(`${API_URL}/ConveniosMarcos/${idConvenioMarco}/especificos/${idConvenioEspecifico}/desvincular`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DesvincularEmpresaDeMarco(idConvenioMarco: number): Promise<Result<null>> {
    try {
      const response = await axios.patch(`${API_URL}/ConveniosMarcos/${idConvenioMarco}/desvincular-empresa`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DesvincularConvenioMarco(idConvenioEspecifico: number): Promise<Result<null>> {
    try {
      const response = await axios.patch(`${API_URL}/ConveniosEspecificos/${idConvenioEspecifico}/desvincular-marco`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async DesvincularEmpresaDeEspecifico(idConvenioEspecifico: number): Promise<Result<null>> {
    try {
      const response = await axios.patch(`${API_URL}/ConveniosEspecificos/${idConvenioEspecifico}/desvincular-empresa`)
      return { isSuccess: true, value: null, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetConveniosPorInvolucrado(id: number): Promise<Result<InvolucradosWithConveniosDto>> {
    try {
      const response = await axios.get(`${API_URL}/Convenios/involucrado/${id}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetAllConveniosMarcos(): Promise<Result<ComboBoxConvenioMarcoDto[]>> {
    try {
      const response = await axios.get(`${API_URL}/ConveniosMarcos`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  static async GetUltimosConvenios(cantidad: number = 5): Promise<Result<UltimosConveniosDto>> {
    try {
      const response = await axios.get(`${API_URL}/Convenios/ultimos?cantidad=${cantidad}`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
