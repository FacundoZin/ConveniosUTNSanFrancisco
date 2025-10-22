import type { InsertEmpresaDto } from '../Empresa/InsertEmpresa'
import { EstadoConvenio } from '../Enums/Enums'

export interface UpdateConvenioMarcoDto {
  id: number // integer($int32) - Requerido para identificar el convenio a actualizar
  numeroConvenio: string | null
  titulo: string | null
  fechaFirmaConvenio: string | null // string($date) | null
  fechaFin: string | null // string($date) | null
  comentarioOpcional: string | null

  estado: EstadoConvenio // No nullable
  numeroResolucion: string | null
  refrendado: boolean // default: false
}

export interface UpdateConvenioMarcoRequetsDto {
  updateConvenioMarcoDto: UpdateConvenioMarcoDto

  insertEmpresaDto: InsertEmpresaDto | null

  idsConveniosEspecificosParaVincular: number[] | null

  idsConveniosEspecificosParaDesvincular: number[] | null

  empresaDesvinculada: boolean // default: false
}

export function UpdateRequestConvMarc(): UpdateConvenioMarcoRequetsDto {
  return {
    updateConvenioMarcoDto: {
      id: 0,
      numeroConvenio: null,
      titulo: null,
      fechaFirmaConvenio: null,
      fechaFin: null,
      comentarioOpcional: null,
      estado: EstadoConvenio.Borrador,
      numeroResolucion: null,
      refrendado: false,
    },
    insertEmpresaDto: null,
    idsConveniosEspecificosParaDesvincular: null,
    idsConveniosEspecificosParaVincular: null,
    empresaDesvinculada: false,
  }
}
