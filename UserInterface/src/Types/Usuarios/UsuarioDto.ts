export type RolUsuario = 'Secretario' | 'Administrador'

export interface UsuarioDto {
  id: number
  username: string
  nombre: string
  rol: RolUsuario
}
