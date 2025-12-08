import type { InsertEmpresaDto } from '../Empresa/InsertEmpresa'
import { EstadoConvenio } from '../Enums/Enums'
import type { InsertInvolucradosDto } from '../Involucrados/InsertInvolucrados'

export interface UpdateConvenioEspecificoDto {
  id: number
  numeroConvenio: string | null
  titulo: string | null
  fechaFirmaConvenio: string | null
  fechaInicioActividades: string | null
  fechaFinConvenio: string | null
  comentarioOpcional: string | null

  estado: EstadoConvenio
  esActa: boolean
  numeroResolucion: string | null
  refrendado: boolean

  convenioMarcoId: number | null
}

export interface UpdateConvenioEspecificoRequestDto {
  updateConvenioDto: UpdateConvenioEspecificoDto
  insertEmpresaDto: InsertEmpresaDto | null
  insertInvolucradosDtos: InsertInvolucradosDto[] | null
  idCarreras: number[] | null
  idsInvolucraodsEliminados: number[] | null
  idMarcoVinculado: number | null
  desvincularConvenioMarco: boolean
  desvincularEmpresa: boolean
  idsInvolucradosExistentes: number[] | null
}

export function CreateUpdateRequestConvEspecifico(): UpdateConvenioEspecificoRequestDto {
  return {
    updateConvenioDto: {
      id: 0,
      numeroConvenio: null,
      titulo: null,
      fechaFirmaConvenio: null,
      fechaInicioActividades: null,
      fechaFinConvenio: null,
      comentarioOpcional: null,
      estado: EstadoConvenio.Borrador,
      esActa: false,
      numeroResolucion: null,
      refrendado: false,
      convenioMarcoId: null,
    },
    insertEmpresaDto: null, //
    insertInvolucradosDtos: null, //
    idCarreras: null, //
    idsInvolucraodsEliminados: null, //
    idMarcoVinculado: null,
    desvincularConvenioMarco: false,
    desvincularEmpresa: false, //
    idsInvolucradosExistentes: null,
  }
}
