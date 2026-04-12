import type { ConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'

export interface InvolucradosWithConveniosDto {
  id: number
  nombre: string
  apellido: string
  conveniosEspecificos: ConvenioEspecificoDto[]
}
