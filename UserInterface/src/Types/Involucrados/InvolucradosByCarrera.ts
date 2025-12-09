export interface InvolucradosDto {
  id: number
  nombre: string
  apellido: string
  email: string
  telefono: string
  legajo: number | null
  carrera: string | null
  rolInvolucrado: string
}

export interface TableInvolucradosByCarreraDto {
  involucrados: InvolucradosDto[]
  cantidad: number
}
