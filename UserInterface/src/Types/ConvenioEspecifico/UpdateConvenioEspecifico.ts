import type { InsertEmpresaDto } from "../Empresa/InsertEmpresa";
import type { EstadoConvenio } from "../Enums/Enums";
import type { InsertInvolucradosDto } from "../Involucrados/InsertInvolucrados";

export interface UpdateConvenioEspecificoDto {
  id: number; // integer($int32) - REQUERIDO para identificar el convenio a actualizar
  numeroConvenio: string | null;
  titulo: string | null;
  fechaFirmaConvenio: string | null; // string($date) | null
  fechaInicioActividades: string | null; // string($date) | null
  fechaFinConvenio: string | null; // string($date) | null
  comentarioOpcional: string | null;

  estado: EstadoConvenio; // No nullable
  esActa: boolean; // default: false
  numeroResolucion: string | null;
  refrendado: boolean; // default: false
  
  // ID del convenio marco con el que está o estará vinculado
  convenioMarcoId: number | null; // integer($int32) | null
}


/**
 * ==========================================
 * 3. DTO PRINCIPAL DE SOLICITUD (REQUEST)
 * ==========================================
 */

/**
 * DTO de Solicitud Principal para la Actualización de un Convenio Específico.
 */
export interface UpdateConvenioEspecificoRequestDto {
  updateConvenioDto: UpdateConvenioEspecificoDto;
  insertEmpresaDto: InsertEmpresaDto; // DTO de la empresa (para actualizar o reemplazar)

  // Array de nuevos involucrados o datos actualizados de involucrados existentes
  insertInvolucradosDtos: InsertInvolucradosDto[] | null;

  // Array de IDs de carrera (para vincular o desvincular)
  idCarreras: number[] | null; 
  
  // IDs de involucrados a eliminar (nuevo campo de control de eliminación)
  idsInvolucraodsEliminados: number[] | null; 

  // ID del convenio marco con el que se va a vincular o cambiar
  idConvenioMarcoVinculado: number | null; // integer($int32) | null
  
  // Flags de control de desvinculación (nuevos campos)
  desvincularConvenioMarco: boolean; // default: false
  desvincularEmpresa: boolean; // default: false
}