import type { EstadoConvenio } from "./Enums/Enums";

export interface IByTituloParams {
  Title: string; 
  'ConvenioType.Type': string; 
}

export interface IByNumeroResolucionParams {
  NumeroResolucion: string;
  'ConvenioType.Type': string; 
}

export interface IByNumeroConvenioParams {
  NumeroConvenio: string; 
  'ConvenioType.Type': string; 
}

export interface IByEmpresaParams {
  EmpresaName: string; 
  'convenioType.Type': string; 
}

export interface IByIsActaParams {
  IsActa: boolean; 
  'ConvenioType.Type': string; 
}

export interface IByIsRefrendadoParams {
  refrendado: boolean; 
  'convenioType.Type': string; 
}

export interface IByEstadoParams {
  Estado: EstadoConvenio; 
  'convenioType.Type': string; 
}

export interface IByCarreraParams {
  nombreCarrera: string; // REQUERIDA
  conveniotype: string; // REQUERIDA
}

export interface IByFechaFirmaParams {
  FechaInicio: string; 
  'convenioType.Type': string;
}

export interface IByFechaFinParams {
  FechaFin: string; 
  'convenioType.Type': string; 
}

export interface IByAntiguedadDtoParams {
  ascendente: boolean; 
  'ConvenioType.Type': string; 
}

export interface IByProximosAvencerParams {
  'convenioType.Type': string;
}

export interface IConvenioQueryObject {
  ByTitulo: IByTituloParams | null;
  ByNumeroResolucion: IByNumeroResolucionParams | null;
  ByNumeroConvenio: IByNumeroConvenioParams | null;
  ByEmpresa: IByEmpresaParams | null;
  ByIsActa: IByIsActaParams | null;
  ByIsRefrendado: IByIsRefrendadoParams | null;
  ByEstado: IByEstadoParams | null;
  ByCarrera: IByCarreraParams | null;
  ByFechaFirma: IByFechaFirmaParams | null;
  ByFechaFin: IByFechaFinParams | null;
  
  ByAntiguedadDto: IByAntiguedadDtoParams | null;
  ByProximosAvencer: IByProximosAvencerParams | null;
}

