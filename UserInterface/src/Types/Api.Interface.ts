export interface ConvenioQueryObject {
  TituloConvenio?: string
  Nombre_empresa?: string
  ProximosAterminar?: boolean
  AntiguedadDescendente?: boolean
  AntiguedadAscendente?: boolean
  PaginaActual?: number
  CantidadResultados?: number
}

export interface FilterConvenios{
  
}

export interface CreateConvenioMarcoDto {
  numeroconvenio: number          
  titulo: string                  
  fechaFirmaConvenio: string      
  fechaFin: string                
  comentarioOpcional?: string     
}

export interface InsertEmpresaDto {
  nombre: string                  
  razonSocial: string             
  cuit: string                    
  direccion: string               
  telefono: string                
  email: string                   
}

export interface CargarConvenioMarcoRequestDto {
  convenioDto: CreateConvenioMarcoDto
  empresaDto: InsertEmpresaDto
}

export interface InsertConvenioEspecificoDto {
  numeroConvenio: number               
  titulo: string                        
  fechaFirmaConvenio: string            
  fechaInicioActividades: string        
  fechaFinConvenio: string              
  convenioMarcoId: number              
  comentarioOpcional?: string          
}

export interface InsertInvolucradosFormDto {
  id: number                           
  nombre: string
  apellido: string
  email: string
  telefono: string
  legajo: string                       
  rolInvolucrado: number
}

export interface InsertInvolucradosDto {
  id: number                           
  nombre: string
  apellido: string
  email: string
  telefono: string
  legajo: number                      
  rolInvolucrado: number
}

export interface CargarConvenioEspecificoRequestDto {
  convenioDto: InsertConvenioEspecificoDto
  involucradosDto?: InsertInvolucradosDto[]   
}



export interface UpdateConvenioMarcoDto {
  id: number                          
  numeroconvenio: number              
  titulo: string                      
  fechaFirmaConvenio: string          
  fechaFin: string                    
  comentarioOpcional?: string       
}

export interface UpdateConvenioEspecificoDto {
  id: number;
  numeroconvenio: number;
  titulo: string;
  fechaFirmaConvenio: string;
  fechaInicioActividades: string;
  fechaFinConvenio: string;
  comentarioOpcional: string | null;
}

export interface UploadConvenioDocument{
  File: File
  ConvenioId: number
  ConvenioType: 'marco' | 'especifico'
}