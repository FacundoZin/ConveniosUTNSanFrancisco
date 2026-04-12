import type { ConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'

export interface InvolucradosWithConveniosDto {
  nombre: string
  apellido: string
  conveniosEspecificos: ConvenioEspecificoDto[]
}
