import axios from 'axios'
import type { Result } from '@/Common/Result'
import type { UsuarioDto } from '@/Types/Usuarios/UsuarioDto'
import type { InsertUsuarioDto } from '@/Types/Usuarios/InsertUsuarioDto'

import { API_URL, getErrorMessage } from '@/Services/apiBaseService'

export default class UsuarioService {
  /**
   * Lista todos los usuarios del sistema (solo administradores).
   */
  static async listar(): Promise<Result<UsuarioDto[]>> {
    try {
      const response = await axios.get(`${API_URL}/Usuarios`)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  /**
   * Crea un nuevo usuario (solo rol Secretario permitido por el backend).
   */
  static async crear(dto: InsertUsuarioDto): Promise<Result<UsuarioDto>> {
    try {
      const response = await axios.post(`${API_URL}/Usuarios`, dto)
      return { isSuccess: true, value: response.data, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  /**
   * Cambia la contraseña de un usuario (mínimo 8 caracteres).
   */
  static async cambiarPassword(id: number, newPassword: string): Promise<Result<void>> {
    try {
      const response = await axios.put(`${API_URL}/Usuarios/${id}/password`, { newPassword })
      return { isSuccess: true, value: undefined, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }

  /**
   * Elimina un usuario. El backend rechaza autoeliminarse o
   * eliminar al último administrador (400 con mensaje).
   */
  static async eliminar(id: number): Promise<Result<void>> {
    try {
      const response = await axios.delete(`${API_URL}/Usuarios/${id}`)
      return { isSuccess: true, value: undefined, status: response.status }
    } catch (Ex: any) {
      return { isSuccess: false, error: { message: getErrorMessage(Ex), status: Ex.response?.status } }
    }
  }
}
