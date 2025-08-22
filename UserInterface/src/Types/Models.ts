export interface Convenioview {
  Id: number
  numeroconvenio: number
  Titulo: string
  FechaFirmaConvenio: string 
  FechaFinConvenio: string
  ConvenioType: string
}

export interface ConvenioMarcoCompleto{
  Idconvenio: number;
  Titulo: string;
  numeroconvenio: number;
  FechaFirmaConvenio: string;
  FechaFin: string;
  ComentarioOpcional: string;
  empresa: EmpresaDto;
  ConveniosEspecificos: ConvenioEspecificoDto[]
}

export interface EmpresaDto {
  Id: number;
  Nombre_Empresa: string;
  RazonSocial: string;
  Cuit: string;
  Direccion_Empresa: string;
  Telefono_Empresa: string;
  Email_Empresa: string;
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