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
  Id: number;
  numeroconvenio: number;
  Titulo: string;
  FechaFirmaConvenio: string;
  FechaInicioActividades: string;
  FechaFinConvenio: string;
  ConvenioType: string;
}

export interface ConvenioEspecificoCompleto{
  Id: number;
  numeroconvenio: number;
  Titulo: string;
  FechaFirmaConvenio: string;
  FechaInicioActividades: string;
  FechaFinConvenio: string;
  ConvenioMarcoId: number;
  ComentarioOpcional: string;
  Involucrados: InvolucradoDto[];
}

export interface InvolucradoDto {
  Id: number;
  Nombre: string;
  Apellido: string;
  Email: string;
  Telefono: string;
  Legajo: number;
  RolInvolucrado: string;
}