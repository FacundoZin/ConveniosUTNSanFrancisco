import type { InsertEmpresaDto } from "../Empresa/InsertEmpresa";
import { EstadoConvenio } from "../Enums/Enums";

export interface InsertConvenioMarcoDto {
  numeroConvenio: string | null;
  titulo: string | null;
  fechaFirmaConvenio: string | null; // string($date) | null
  fechaFin: string | null; // string($date) | null
  comentarioOpcional: string | null;
  
  // El estado es requerido con un tipo de enum específico
  estado: EstadoConvenio;
  
  numeroResolucion: string | null;
  
  // Refrendado tiene un valor por defecto, no es nullable, por lo que es requerido.
  refrendado: boolean; 
}

export interface CargarConvenioMarcoRequestDto {
  insertConvenioDto: InsertConvenioMarcoDto;
  insertEmpresaDto: InsertEmpresaDto | null;
  
  // Array de números que es anulable
  numeroConvEspecificoParaVincular: string | null;
}

export function createRequestConvMarc(): CargarConvenioMarcoRequestDto{
  return {
    insertConvenioDto:{
      numeroConvenio: null,
      titulo: null,
      fechaFirmaConvenio: null,
      fechaFin: null,
      comentarioOpcional: null,
      estado: EstadoConvenio.Borrador,
      numeroResolucion: null,
      refrendado: false
    },
    insertEmpresaDto: null,
    numeroConvEspecificoParaVincular: null
  }
}