import type { EstadoConvenio } from './Enums/Enums'

export interface IByTituloParams {
  Title: string
  convenioType: string
}

export interface IByNumeroResolucionParams {
  NumeroResolucion: string
  convenioType: string
}

export interface IByNumeroConvenioParams {
  NumeroConvenio: string
  convenioType: string
}

export interface IByEmpresaParams {
  EmpresaName: string
  convenioType: string
}

export interface IByIsActaParams {
  IsActa: boolean
  convenioType: string
}

export interface IByIsRefrendadoParams {
  refrendado: boolean
  convenioType: string
}

export interface IByEstadoParams {
  Estado: EstadoConvenio
  convenioType: string
}

export interface IByCarreraParams {
  nombreCarrera: string // REQUERIDA
  conveniotype: string // REQUERIDA
}

export interface IByFechaFirmaParams {
  FechaInicio: string
  convenioType: string
}

export interface IByFechaFinParams {
  FechaFin: string
  convenioType: string
}

export interface IByAntiguedadDtoParams {
  ascendente: boolean
  convenioType: string
}

export interface IByProximosAvencerParams {
  convenioType: string
}

export interface IConvenioQueryObject {
  ByTitulo: IByTituloParams | null
  ByNumeroResolucion: IByNumeroResolucionParams | null
  ByNumeroConvenio: IByNumeroConvenioParams | null
  ByEmpresa: IByEmpresaParams | null
  ByIsActa: IByIsActaParams | null
  ByIsRefrendado: IByIsRefrendadoParams | null
  ByEstado: IByEstadoParams | null
  ByCarrera: IByCarreraParams | null
  ByFechaFirma: IByFechaFirmaParams | null
  ByFechaFin: IByFechaFinParams | null

  ByAntiguedadDto: IByAntiguedadDtoParams | null
  ByProximosAvencer: IByProximosAvencerParams | null
}
