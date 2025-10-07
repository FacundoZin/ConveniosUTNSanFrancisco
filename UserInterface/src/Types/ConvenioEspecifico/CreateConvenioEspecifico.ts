import type { InsertEmpresaDto } from "../Empresa/InsertEmpresa";
import type { EstadoConvenio, RolInvolucrado } from "../Enums/Enums";
import type { InsertInvolucradosDto } from "../Involucrados/InsertInvolucrados";

export interface InsertConvenioEspecificoDto {
  numeroConvenio: string | null;
  titulo: string | null;
  fechaFirmaConvenio: string | null; 
  fechaInicioActividades: string | null; 
  fechaFinConvenio: string | null;
  convenioMarcoId: number | null; 
  comentarioOpcional: string | null;

  estado: EstadoConvenio; 

  esActa: boolean; 
  numeroResolucion: string | null;
  refrendado: boolean; 
}


export interface CargarConvenioEspecificoRequestDto {
  insertConvenioDto: InsertConvenioEspecificoDto;
  insertEmpresaDto: InsertEmpresaDto;

  insertInvolucradosDto: InsertInvolucradosDto[] | null;

  idCarreras: number[] | null; 

  idConvenioMarcoVinculado: number | null; 
}
