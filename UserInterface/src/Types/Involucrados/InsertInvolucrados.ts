import type { RolInvolucrado } from '../Enums/Enums'

export interface InsertInvolucradosDto {
  nombre: string | null
  apellido: string | null
  email: string | null
  telefono: string | null
  legajo: number | null // integer($int32) | null
  rolInvolucrado: RolInvolucrado // RolInvolucradoRoles | null
}
