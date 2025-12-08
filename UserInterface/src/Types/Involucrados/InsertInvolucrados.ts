import type { RolInvolucrado } from '../Enums/Enums'

export interface InsertInvolucradosDto {
  nombre: string | null
  apellido: string | null
  email: string | null
  telefono: string | null
  legajo: number | null // integer($int32) | null
  idCarrera: number // integer($int32)
  rolInvolucrado: RolInvolucrado // RolInvolucradoRoles | null
}
