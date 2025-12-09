import type { Carrera } from '../CarrerasInvolucradas/CarrerasInvolucradas'
import type { EstadoConvenio } from '../Enums/Enums'

export interface ConvenioCreated {
  id: number
  ConvenioType: string
}

export type ListConveniosDto =
  | {
      data: ConvenioEspecificoDto[]
      Type: 'especifico'
    }
  | {
      data: ConvenioMarcoDto[]
      Type: 'marco'
    }
  | {
      data: []
      Type: ''
    }
  | {
      conveniosMarcos: ConvenioMarcoDto[]
      conveniosEspecificos: ConvenioEspecificoDto[]
      Type: 'ambos'
      data: any[]
    }

export interface ConvenioEspecificoDto {
  id: number
  titulo?: string | null
  numeroconvenio?: string | null
  nombreEmpresa?: string | null
  fechaFirmaConvenio?: string | null
  fechaInicioActividades?: string | null
  fechaFin?: string | null
  convenioType: 'especifico'
  estado: EstadoConvenio
  esActa: boolean
  refrendado: boolean
}

export interface ConvenioMarcoDto {
  id: number
  titulo: string | null
  numeroconvenio?: string | null
  nombreEmpresa?: string | null
  fechaFirmaConvenio?: string | null
  fechaFin?: string | null
  convenioType: 'marco'
  estado: EstadoConvenio // Asumiendo que `EstadoConvenio` es un enum.
  refrendado: boolean
}

export interface EmpresaDto {
  id: number
  nombre_Empresa: string
  razonSocial?: string
  cuit?: string
  direccion_Empresa?: string
  telefono_Empresa?: string
  email_Empresa?: string
}

export interface InvolucradosDto {
  id: number
  nombre: string
  apellido: string
  email?: string
  telefono?: string
  legajo?: number
  carrera?: string
  rolInvolucrado: string
}

export interface ViewArchivoDto {
  idArchivo: number
  nombreArchivo: string
}

export interface InfoConvenioEspecificoDto {
  id: number
  numeroconvenio?: string
  titulo?: string
  fechaFirmaConvenio?: string
  fechaInicioActividades?: string
  fechaFinConvenio?: string
  comentarioOpcional?: string
  estado: EstadoConvenio
  esActa: boolean
  numeroResolucion?: string
  refrendado: boolean
  convenioMarcoId?: number
  convenioMarco: ConvenioMarcoDto
  empresa?: EmpresaDto
  involucrados?: InvolucradosDto[]
  documentosAdjuntos?: ViewArchivoDto[]
  carrerasInvolucradas?: Carrera[]
}

export interface InfoConvenioMarcoDto {
  id: number
  numeroconvenio?: string
  titulo?: string
  fechaFirmaConvenio?: string
  fechaFin?: string
  comentarioOpcional?: string
  estado: EstadoConvenio
  numeroResolucion?: string
  refrendado: boolean
  empresa?: EmpresaDto
  conveniosEspecificos?: ConvenioEspecificoDto[]
  archivosAdjuntos?: ViewArchivoDto[]
}
