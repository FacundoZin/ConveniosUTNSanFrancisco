export interface InvolucradosDto {
  id: number
  nombre: string
  apellido: string
  email: string
  telefono: string
  legajo: number | null
  area: string | null
  rolInvolucrado: string
}

export interface TableInvolucradosByAreaDto {
  involucrados: InvolucradosDto[]
  cantidad: number
}
