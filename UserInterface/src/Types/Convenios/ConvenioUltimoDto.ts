import type { EstadoConvenio } from '@/Types/Enums/Enums'

export interface ConvenioUltimoDto {
  id: number
  titulo: string | null
  convenioType: 'marco' | 'especifico'
  nombreEmpresa: string | null
  estado: EstadoConvenio
}

export interface UltimosConveniosDto {
  conveniosMarcos: ConvenioUltimoDto[]
  conveniosEspecificos: ConvenioUltimoDto[]
}
