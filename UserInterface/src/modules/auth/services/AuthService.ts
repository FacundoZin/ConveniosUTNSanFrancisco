import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { SesionUsuarioDto } from '@/Types/Auth/SesionUsuarioDto'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class AuthService {
  static async login(username: string, password: string): Promise<Result<SesionUsuarioDto>> {
    try {
      const response = await axios.post(`${API_URL}/Auth/login`, { username, password })
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  /**
   * Cierra la sesión en el servidor (invalida la cookie de autenticación).
   */
  static async logout(): Promise<Result<void>> {
    try {
      const response = await axios.post(`${API_URL}/Auth/logout`)
      return { isSuccess: true, value: undefined, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  /**
   * Consulta la sesión actual a partir de la cookie.
   * Devuelve failure con 401 si no hay sesión activa.
   */
  static async obtenerSesionActual(): Promise<Result<SesionUsuarioDto>> {
    try {
      const response = await axios.get(`${API_URL}/Auth/me`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
