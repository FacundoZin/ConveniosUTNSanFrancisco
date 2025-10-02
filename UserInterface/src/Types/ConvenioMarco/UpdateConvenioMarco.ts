import type { InsertEmpresaDto } from "../Empresa/InsertEmpresa";
import type { EstadoConvenio } from "../Enums/Enums";

export interface UpdateConvenioMarcoDto {
  id: number; // integer($int32) - Requerido para identificar el convenio a actualizar
  numeroConvenio: string | null;
  titulo: string | null;
  fechaFirmaConvenio: string | null; // string($date) | null
  fechaFin: string | null; // string($date) | null
  comentarioOpcional: string | null;

  estado: EstadoConvenio; // No nullable
  numeroResolucion: string | null;
  refrendado: boolean; // default: false
}


export interface UpdateConvenioMarcoRequetsDto {
  // DTO principal con la data del convenio
  updateConvenioMarcoDto: UpdateConvenioMarcoDto;

  // DTO para la empresa (para actualizarla o reemplazarla)
  insertEmpresaDto: InsertEmpresaDto;

  // IDs de convenios específicos que se deben vincular a este marco
  idsConveniosEspecificosParaVincular: number[] | null; 
  
  // IDs de convenios específicos que se deben desvincular de este marco
  idsConveniosEspecificosParaDesvincular: number[] | null; 

  // Flag de control para desvincular completamente la empresa asociada
  empresaDesvinculada: boolean; // default: false
}