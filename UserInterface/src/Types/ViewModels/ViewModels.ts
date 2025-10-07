import type { EstadoConvenio } from "../Enums/Enums";

export interface ConvenioEspecificoDto {
  id: number;
  titulo?: string | null;
  numeroConvenio?: string | null;
  nombreEmpresa?: string | null;
  fechaFirmaConvenio?: string | null;
  fechaInicioActividades?: string | null;
  fechaFin?: string | null;
  convenioType: "especifico";
  estado?: EstadoConvenio; 
  esActa: boolean;
  refrendado: boolean;
}

export interface ConvenioMarcoDto {
  id: number;
  titulo: string | null;
  numeroConvenio?: string | null;
  nombreEmpresa?: string | null;
  fechaFirmaConvenio?: string | null;
  fechaFin?: string | null;
  convenioType: "marco";
  estado: EstadoConvenio; // Asumiendo que `EstadoConvenio` es un enum.
  refrendado: boolean;
}


