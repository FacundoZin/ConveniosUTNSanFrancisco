import type { RolUsuario } from './UsuarioDto'

export interface InsertUsuarioDto {
  username: string
  password: string
  nombre: string
  rol: RolUsuario
}
