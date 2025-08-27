export interface ConveniosResponse {
  conveniosEspecificos: Convenioview[];
  conveniosMarco: Convenioview[];
}

export interface Convenioview {
  id: number
  numeroconvenio: number
  titulo: string
  nombreEmpresa: string
  fechaFirmaConvenio: string 
  fechaFin: string
  convenioType: string
}

export interface ConvenioMarcoCompleto{
  idconvenio: number;
  titulo: string;
  numeroconvenio: number;
  fechaFirmaConvenio: string;
  fechaFin: string;
  comentarioOpcional: string;
  empresa: EmpresaDto;
  conveniosEspecificos: ConvenioEspecificoDto[]
}

export interface EmpresaDto {
  id: number;
  nombre_Empresa: string;
  razonSocial: string;
  cuit: string;
  direccion_Empresa: string;
  telefono_Empresa: string;
  email_Empresa: string;
}

export interface ConvenioEspecificoDto{
  id: number;
  numeroconvenio: number;
  titulo: string;
  fechaFirmaConvenio: string;
  fechaInicioActividades: string;
  fechaFinConvenio: string;
  convenioType: string;
}

export interface ConvenioEspecificoCompleto{
  id: number;
  numeroconvenio: number;
  titulo: string;
  fechaFirmaConvenio: string;
  fechaInicioActividades: string;
  fechaFinConvenio: string;
  convenioMarcoId: number;
  comentarioOpcional: string;
  involucrados: InvolucradoDto[];
}

export interface InvolucradoDto {
  id: number;
  nombre: string;
  apellido: string;
  email: string;
  telefono: string;
  legajo: number;
  rolInvolucrado: string;
}