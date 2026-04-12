import type { ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'
import type { ConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'

export interface EmpresaWithConveniosDto {
  idEmpresa: number
  nombreEmpresa: string
  convenioMarco: ConvenioMarcoDto | null
  conveniosEspecificos: ConvenioEspecificoDto[]
}
