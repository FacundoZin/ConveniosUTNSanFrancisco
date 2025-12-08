import type { InsertEmpresaDto } from '../Empresa/InsertEmpresa'
import { EstadoConvenio } from '../Enums/Enums'
import type { InsertInvolucradosDto } from '../Involucrados/InsertInvolucrados'

export interface InsertConvenioEspecificoDto {
  numeroConvenio: string | null
  titulo: string | null
  fechaFirmaConvenio: string | null
  fechaInicioActividades: string | null
  fechaFinConvenio: string | null
  convenioMarcoId: number | null
  comentarioOpcional: string | null

  estado: EstadoConvenio

  esActa: boolean
  numeroResolucion: string | null
  refrendado: boolean
}

export interface CargarConvenioEspecificoRequestDto {
  insertConvenioDto: InsertConvenioEspecificoDto
  insertEmpresaDto: InsertEmpresaDto | null
  idsInvolucradosExistentes: number[] | null

  insertInvolucradosDto: InsertInvolucradosDto[] | null

  idCarreras: number[] | null

  idConvenioMarco: number | null
}

export function createRequestConvEspecifico(): CargarConvenioEspecificoRequestDto {
  return {
    insertConvenioDto: {
      numeroConvenio: null,
      titulo: null,
      fechaFirmaConvenio: null,
      fechaInicioActividades: null,
      fechaFinConvenio: null,
      convenioMarcoId: null,
      comentarioOpcional: null,
      estado: EstadoConvenio.Borrador,
      esActa: false,
      numeroResolucion: null,
      refrendado: false,
    },
    insertEmpresaDto: null,
    insertInvolucradosDto: null,
    idCarreras: [],
    idConvenioMarco: null,
    idsInvolucradosExistentes: null,
  }
}
