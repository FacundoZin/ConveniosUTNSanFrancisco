import type { InsertEmpresaDto } from "../Empresa/InsertEmpresa";
import type { EstadoConvenio, RolInvolucrado } from "../Enums/Enums";
import type { InsertInvolucradosDto } from "../Involucrados/InsertInvolucrados";

export interface InsertConvenioEspecificoDto {
  numeroConvenio: string | null;
  titulo: string | null;
  fechaFirmaConvenio: string | null; // string($date) | null
  fechaInicioActividades: string | null; // string($date) | null
  fechaFinConvenio: string | null; // string($date) | null
  convenioMarcoId: number | null; // integer($int32) | null
  comentarioOpcional: string | null;

  estado: EstadoConvenio; // EstadoConvenio está importado.

  esActa: boolean; // default: false
  numeroResolucion: string | null;
  refrendado: boolean; // default: false
}


export interface CargarConvenioEspecificoRequestDto {
  insertConvenioDto: InsertConvenioEspecificoDto;
  insertEmpresaDto: InsertEmpresaDto; // Reutilizado

  // Array de objetos de involucrados, es anulable
  insertInvolucradosDto: InsertInvolucradosDto[] | null;

  // Array de IDs de carrera, es anulable
  idCarreras: number[] | null; // integer($int32)[] | null

  // ID del convenio marco con el que se vincula, es anulable
  idConvenioMarcoVinculado: number | null; // integer($int32) | null
}
