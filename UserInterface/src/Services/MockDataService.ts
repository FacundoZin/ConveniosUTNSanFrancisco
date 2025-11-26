import type { Result } from '@/Common/Result'
import type { Carrera } from '@/Types/CarrerasInvolucradas/CarrerasInvolucradas'
import type { CargarConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico'
import type { UpdateConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/UpdateConvenioEspecifico'
import type { CargarConvenioMarcoRequestDto } from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { UpdateConvenioMarcoRequetsDto } from '@/Types/ConvenioMarco/UpdateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import { EstadoConvenio } from '@/Types/Enums/Enums'
import type { IConvenioQueryObject } from '@/Types/Filters'
import type {
    ConvenioCreated,
    ConvenioEspecificoDto,
    ConvenioMarcoDto,
    EmpresaDto,
    InfoConvenioEspecificoDto,
    InfoConvenioMarcoDto,
    InvolucradosDto,
    ViewArchivoDto,
} from '@/Types/ViewModels/ViewModels'

// ============================================================================
// DATOS MOCK - EMPRESAS
// ============================================================================

const mockEmpresas: EmpresaDto[] = [
  {
    id: 1,
    nombre_Empresa: 'TechSolutions S.A.',
    razonSocial: 'TechSolutions Sociedad Anónima',
    cuit: '30-71234567-8',
    direccion_Empresa: 'Av. Libertador 1500, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 123456',
    email_Empresa: 'contacto@techsolutions.com.ar',
  },
  {
    id: 2,
    nombre_Empresa: 'Municipalidad de San Francisco',
    razonSocial: 'Municipalidad de San Francisco',
    cuit: '30-99887766-5',
    direccion_Empresa: 'Av. de la Universidad 500, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 987654',
    email_Empresa: 'convenios@muniSF.gob.ar',
  },
  {
    id: 3,
    nombre_Empresa: 'Industrias Metalúrgicas del Centro',
    razonSocial: 'Industrias Metalúrgicas del Centro S.R.L.',
    cuit: '30-65432109-7',
    direccion_Empresa: 'Parque Industrial, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 456789',
    email_Empresa: 'rrhh@imc.com.ar',
  },
  {
    id: 4,
    nombre_Empresa: 'Cámara de Comercio e Industria',
    razonSocial: 'Cámara de Comercio e Industria de San Francisco',
    cuit: '30-55443322-1',
    direccion_Empresa: 'Bv. 9 de Julio 2200, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 234567',
    email_Empresa: 'info@camarasf.org.ar',
  },
  {
    id: 5,
    nombre_Empresa: 'Agro Tech Argentina',
    razonSocial: 'Agro Tech Argentina S.A.',
    cuit: '30-88776655-4',
    direccion_Empresa: 'Ruta 19 Km 5, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 345678',
    email_Empresa: 'convenios@agrotech.com.ar',
  },
  {
    id: 6,
    nombre_Empresa: 'Universidad Nacional de Córdoba',
    razonSocial: 'Universidad Nacional de Córdoba',
    cuit: '30-54667789-2',
    direccion_Empresa: 'Haya de la Torre s/n, Córdoba Capital',
    telefono_Empresa: '+54 351 4334000',
    email_Empresa: 'vinculacion@unc.edu.ar',
  },
  {
    id: 7,
    nombre_Empresa: 'Software Factory Córdoba',
    razonSocial: 'Software Factory Córdoba S.R.L.',
    cuit: '30-77665544-3',
    direccion_Empresa: 'Av. Córdoba 850, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 567890',
    email_Empresa: 'pasantias@softfactory.com.ar',
  },
  {
    id: 8,
    nombre_Empresa: 'Cooperativa Eléctrica',
    razonSocial: 'Cooperativa de Electricidad y Servicios Públicos',
    cuit: '30-44332211-9',
    direccion_Empresa: 'Bv. Roca 1800, San Francisco, Córdoba',
    telefono_Empresa: '+54 3564 678901',
    email_Empresa: 'administracion@coopelectrica.coop',
  },
]

// ============================================================================
// DATOS MOCK - CARRERAS
// ============================================================================

const mockCarreras: Carrera[] = [
  { id: 1, nombre: 'Ingeniería en Sistemas de Información' },
  { id: 2, nombre: 'Ingeniería Mecánica' },
  { id: 3, nombre: 'Ingeniería Electromecánica' },
  { id: 4, nombre: 'Ingeniería Civil' },
  { id: 5, nombre: 'Ingeniería Industrial' },
  { id: 6, nombre: 'Licenciatura en Administración' },
]

// ============================================================================
// DATOS MOCK - INVOLUCRADOS
// ============================================================================

const mockInvolucrados: InvolucradosDto[] = [
  {
    id: 1,
    nombre: 'Juan',
    apellido: 'Pérez',
    email: 'juan.perez@utn.edu.ar',
    telefono: '+54 3564 111222',
    legajo: 12345,
    rolInvolucrado: 'Docente',
  },
  {
    id: 2,
    nombre: 'María',
    apellido: 'González',
    email: 'maria.gonzalez@utn.edu.ar',
    telefono: '+54 3564 222333',
    legajo: 23456,
    rolInvolucrado: 'Secretario',
  },
  {
    id: 3,
    nombre: 'Carlos',
    apellido: 'Rodríguez',
    email: 'carlos.rodriguez@empresa.com',
    telefono: '+54 3564 333444',
    rolInvolucrado: 'Externo',
  },
  {
    id: 4,
    nombre: 'Ana',
    apellido: 'Martínez',
    email: 'ana.martinez@utn.edu.ar',
    telefono: '+54 3564 444555',
    legajo: 45678,
    rolInvolucrado: 'Alumno',
  },
  {
    id: 5,
    nombre: 'Roberto',
    apellido: 'Fernández',
    email: 'roberto.fernandez@utn.edu.ar',
    telefono: '+54 3564 555666',
    legajo: 34567,
    rolInvolucrado: 'Docente',
  },
]

// ============================================================================
// DATOS MOCK - ARCHIVOS
// ============================================================================

const mockArchivos: ViewArchivoDto[] = [
  { idArchivo: 1, nombreArchivo: 'Convenio_Marco_TechSolutions_2024.pdf' },
  { idArchivo: 2, nombreArchivo: 'Acta_Acuerdo_Municipalidad.pdf' },
  { idArchivo: 3, nombreArchivo: 'Anexo_I_Practicas_Profesionales.pdf' },
  { idArchivo: 4, nombreArchivo: 'Resolucion_123_2024.pdf' },
  { idArchivo: 5, nombreArchivo: 'Convenio_Especifico_Pasantias.pdf' },
  { idArchivo: 6, nombreArchivo: 'Protocolo_Seguridad_Industrial.pdf' },
]

// ============================================================================
// DATOS MOCK - CONVENIOS MARCO
// ============================================================================

let mockConveniosMarco: ConvenioMarcoDto[] = [
  {
    id: 1,
    titulo: 'Convenio Marco de Cooperación Académica y Tecnológica',
    numeroconvenio: 'CM-2024-001',
    nombreEmpresa: 'TechSolutions S.A.',
    fechaFirmaConvenio: '2024-01-15',
    fechaFin: '2026-01-15',
    convenioType: 'marco',
    estado: EstadoConvenio.Vigente,
    refrendado: true,
  },
  {
    id: 2,
    titulo: 'Convenio Marco de Vinculación Institucional',
    numeroconvenio: 'CM-2024-002',
    nombreEmpresa: 'Municipalidad de San Francisco',
    fechaFirmaConvenio: '2024-02-20',
    fechaFin: '2027-02-20',
    convenioType: 'marco',
    estado: EstadoConvenio.Vigente,
    refrendado: true,
  },
  {
    id: 3,
    titulo: 'Convenio Marco para Prácticas Industriales',
    numeroconvenio: 'CM-2023-015',
    nombreEmpresa: 'Industrias Metalúrgicas del Centro',
    fechaFirmaConvenio: '2023-06-10',
    fechaFin: '2025-06-10',
    convenioType: 'marco',
    estado: EstadoConvenio.Vigente,
    refrendado: true,
  },
  {
    id: 4,
    titulo: 'Convenio Marco de Desarrollo Empresarial',
    numeroconvenio: 'CM-2024-003',
    nombreEmpresa: 'Cámara de Comercio e Industria',
    fechaFirmaConvenio: '2024-03-05',
    fechaFin: '2026-03-05',
    convenioType: 'marco',
    estado: EstadoConvenio.Vigente,
    refrendado: false,
  },
  {
    id: 5,
    titulo: 'Convenio Marco de Investigación Agropecuaria',
    numeroconvenio: 'CM-2023-020',
    nombreEmpresa: 'Agro Tech Argentina',
    fechaFirmaConvenio: '2023-09-12',
    fechaFin: '2024-09-12',
    convenioType: 'marco',
    estado: EstadoConvenio.Finalizado,
    refrendado: true,
  },
  {
    id: 6,
    titulo: 'Convenio Marco Interuniversitario',
    numeroconvenio: 'CM-2024-004',
    nombreEmpresa: 'Universidad Nacional de Córdoba',
    fechaFirmaConvenio: '2024-04-18',
    fechaFin: '2029-04-18',
    convenioType: 'marco',
    estado: EstadoConvenio.Vigente,
    refrendado: true,
  },
  {
    id: 7,
    titulo: 'Convenio Marco de Desarrollo de Software',
    numeroconvenio: 'CM-2024-005',
    nombreEmpresa: 'Software Factory Córdoba',
    fechaFirmaConvenio: '2024-05-22',
    fechaFin: null,
    convenioType: 'marco',
    estado: EstadoConvenio.Borrador,
    refrendado: false,
  },
]

// ============================================================================
// DATOS MOCK - CONVENIOS ESPECÍFICOS
// ============================================================================

let mockConveniosEspecificos: ConvenioEspecificoDto[] = [
  {
    id: 1,
    titulo: 'Pasantías en Desarrollo de Software',
    numeroconvenio: 'CE-2024-001',
    nombreEmpresa: 'TechSolutions S.A.',
    fechaFirmaConvenio: '2024-02-01',
    fechaInicioActividades: '2024-03-01',
    fechaFin: '2024-12-31',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 2,
    titulo: 'Prácticas Profesionales Supervisadas - Ingeniería Civil',
    numeroconvenio: 'CE-2024-002',
    nombreEmpresa: 'Municipalidad de San Francisco',
    fechaFirmaConvenio: '2024-03-10',
    fechaInicioActividades: '2024-04-01',
    fechaFin: '2024-11-30',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 3,
    titulo: 'Proyecto de Investigación en Metalurgia',
    numeroconvenio: 'CE-2024-003',
    nombreEmpresa: 'Industrias Metalúrgicas del Centro',
    fechaFirmaConvenio: '2024-01-20',
    fechaInicioActividades: '2024-02-15',
    fechaFin: '2025-02-15',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 4,
    titulo: 'Acta Complementaria - Capacitación Empresarial',
    numeroconvenio: 'CE-2024-004',
    nombreEmpresa: 'Cámara de Comercio e Industria',
    fechaFirmaConvenio: '2024-04-05',
    fechaInicioActividades: '2024-05-01',
    fechaFin: '2024-10-31',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: true,
    refrendado: false,
  },
  {
    id: 5,
    titulo: 'Pasantías en Sistemas de Información',
    numeroconvenio: 'CE-2024-005',
    nombreEmpresa: 'Software Factory Córdoba',
    fechaFirmaConvenio: '2024-06-01',
    fechaInicioActividades: '2024-07-01',
    fechaFin: '2024-12-31',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 6,
    titulo: 'Prácticas en Mantenimiento Eléctrico',
    numeroconvenio: 'CE-2024-006',
    nombreEmpresa: 'Cooperativa Eléctrica',
    fechaFirmaConvenio: '2024-03-15',
    fechaInicioActividades: '2024-04-15',
    fechaFin: '2024-09-15',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 7,
    titulo: 'Intercambio Académico de Docentes',
    numeroconvenio: 'CE-2024-007',
    nombreEmpresa: 'Universidad Nacional de Córdoba',
    fechaFirmaConvenio: '2024-05-10',
    fechaInicioActividades: '2024-08-01',
    fechaFin: '2025-07-31',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 8,
    titulo: 'Proyecto de Investigación Agronómica',
    numeroconvenio: 'CE-2023-025',
    nombreEmpresa: 'Agro Tech Argentina',
    fechaFirmaConvenio: '2023-10-05',
    fechaInicioActividades: '2023-11-01',
    fechaFin: '2024-04-30',
    convenioType: 'especifico',
    estado: EstadoConvenio.Finalizado,
    esActa: false,
    refrendado: true,
  },
  {
    id: 9,
    titulo: 'Pasantías en Ingeniería Industrial',
    numeroconvenio: 'CE-2024-008',
    nombreEmpresa: 'Industrias Metalúrgicas del Centro',
    fechaFirmaConvenio: '2024-02-28',
    fechaInicioActividades: '2024-04-01',
    fechaFin: '2024-11-30',
    convenioType: 'especifico',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    refrendado: true,
  },
  {
    id: 10,
    titulo: 'Convenio Específico en Borrador',
    numeroconvenio: 'CE-2024-009',
    nombreEmpresa: 'TechSolutions S.A.',
    fechaFirmaConvenio: null,
    fechaInicioActividades: null,
    fechaFin: null,
    convenioType: 'especifico',
    estado: EstadoConvenio.Borrador,
    esActa: false,
    refrendado: false,
  },
]

// ============================================================================
// DATOS MOCK - INFORMACIÓN COMPLETA DE CONVENIOS
// ============================================================================

const mockInfoConveniosMarco: InfoConvenioMarcoDto[] = [
  {
    id: 1,
    numeroconvenio: 'CM-2024-001',
    titulo: 'Convenio Marco de Cooperación Académica y Tecnológica',
    fechaFirmaConvenio: '2024-01-15',
    fechaFin: '2026-01-15',
    comentarioOpcional:
      'Convenio para el desarrollo de actividades académicas, prácticas profesionales y proyectos de investigación conjuntos.',
    estado: EstadoConvenio.Vigente,
    numeroResolucion: 'RES-2024-045',
    refrendado: true,
    empresa: mockEmpresas[0],
    conveniosEspecificos: [mockConveniosEspecificos[0], mockConveniosEspecificos[9]],
    archivosAdjuntos: [mockArchivos[0], mockArchivos[3]],
  },
  {
    id: 2,
    numeroconvenio: 'CM-2024-002',
    titulo: 'Convenio Marco de Vinculación Institucional',
    fechaFirmaConvenio: '2024-02-20',
    fechaFin: '2027-02-20',
    comentarioOpcional:
      'Acuerdo de cooperación con la Municipalidad para proyectos de desarrollo urbano y social.',
    estado: EstadoConvenio.Vigente,
    numeroResolucion: 'RES-2024-067',
    refrendado: true,
    empresa: mockEmpresas[1],
    conveniosEspecificos: [mockConveniosEspecificos[1]],
    archivosAdjuntos: [mockArchivos[1]],
  },
  {
    id: 3,
    numeroconvenio: 'CM-2023-015',
    titulo: 'Convenio Marco para Prácticas Industriales',
    fechaFirmaConvenio: '2023-06-10',
    fechaFin: '2025-06-10',
    comentarioOpcional:
      'Convenio para la realización de prácticas profesionales en el sector metalúrgico.',
    estado: EstadoConvenio.Vigente,
    numeroResolucion: 'RES-2023-234',
    refrendado: true,
    empresa: mockEmpresas[2],
    conveniosEspecificos: [mockConveniosEspecificos[2], mockConveniosEspecificos[8]],
    archivosAdjuntos: [mockArchivos[5]],
  },
  {
    id: 4,
    numeroconvenio: 'CM-2024-003',
    titulo: 'Convenio Marco de Desarrollo Empresarial',
    fechaFirmaConvenio: '2024-03-05',
    fechaFin: '2026-03-05',
    comentarioOpcional: 'Acuerdo para capacitación y vinculación con el sector empresarial local.',
    estado: EstadoConvenio.Vigente,
    numeroResolucion: 'RES-2024-089',
    refrendado: false,
    empresa: mockEmpresas[3],
    conveniosEspecificos: [mockConveniosEspecificos[3]],
    archivosAdjuntos: [],
  },
  {
    id: 5,
    numeroconvenio: 'CM-2023-020',
    titulo: 'Convenio Marco de Investigación Agropecuaria',
    fechaFirmaConvenio: '2023-09-12',
    fechaFin: '2024-09-12',
    comentarioOpcional: 'Convenio finalizado para investigación en tecnología agropecuaria.',
    estado: EstadoConvenio.Finalizado,
    numeroResolucion: 'RES-2023-345',
    refrendado: true,
    empresa: mockEmpresas[4],
    conveniosEspecificos: [mockConveniosEspecificos[7]],
    archivosAdjuntos: [],
  },
  {
    id: 6,
    numeroconvenio: 'CM-2024-004',
    titulo: 'Convenio Marco Interuniversitario',
    fechaFirmaConvenio: '2024-04-18',
    fechaFin: '2029-04-18',
    comentarioOpcional: 'Convenio de cooperación académica con la UNC.',
    estado: EstadoConvenio.Vigente,
    numeroResolucion: 'RES-2024-112',
    refrendado: true,
    empresa: mockEmpresas[5],
    conveniosEspecificos: [mockConveniosEspecificos[6]],
    archivosAdjuntos: [],
  },
  {
    id: 7,
    numeroconvenio: 'CM-2024-005',
    titulo: 'Convenio Marco de Desarrollo de Software',
    fechaFirmaConvenio: '2024-05-22',
    fechaFin: undefined,
    comentarioOpcional: 'Convenio en proceso de firma.',
    estado: EstadoConvenio.Borrador,
    numeroResolucion: undefined,
    refrendado: false,
    empresa: mockEmpresas[6],
    conveniosEspecificos: [],
    archivosAdjuntos: [],
  },
]

const mockInfoConveniosEspecificos: InfoConvenioEspecificoDto[] = [
  {
    id: 1,
    numeroconvenio: 'CE-2024-001',
    titulo: 'Pasantías en Desarrollo de Software',
    fechaFirmaConvenio: '2024-02-01',
    fechaInicioActividades: '2024-03-01',
    fechaFinConvenio: '2024-12-31',
    comentarioOpcional: 'Pasantías para estudiantes de Ingeniería en Sistemas.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-056',
    refrendado: true,
    convenioMarcoId: 1,
    convenioMarco: mockConveniosMarco[0],
    empresa: mockEmpresas[0],
    involucrados: [mockInvolucrados[0], mockInvolucrados[3]],
    documentosAdjuntos: [mockArchivos[2]],
    carrerasInvolucradas: [mockCarreras[0]],
  },
  {
    id: 2,
    numeroconvenio: 'CE-2024-002',
    titulo: 'Prácticas Profesionales Supervisadas - Ingeniería Civil',
    fechaFirmaConvenio: '2024-03-10',
    fechaInicioActividades: '2024-04-01',
    fechaFinConvenio: '2024-11-30',
    comentarioOpcional: 'Prácticas en obras públicas municipales.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-078',
    refrendado: true,
    convenioMarcoId: 2,
    convenioMarco: mockConveniosMarco[1],
    empresa: mockEmpresas[1],
    involucrados: [mockInvolucrados[1], mockInvolucrados[3]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[3]],
  },
  {
    id: 3,
    numeroconvenio: 'CE-2024-003',
    titulo: 'Proyecto de Investigación en Metalurgia',
    fechaFirmaConvenio: '2024-01-20',
    fechaInicioActividades: '2024-02-15',
    fechaFinConvenio: '2025-02-15',
    comentarioOpcional: 'Investigación aplicada en procesos metalúrgicos.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-034',
    refrendado: true,
    convenioMarcoId: 3,
    convenioMarco: mockConveniosMarco[2],
    empresa: mockEmpresas[2],
    involucrados: [mockInvolucrados[0], mockInvolucrados[4]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[1], mockCarreras[2]],
  },
  {
    id: 4,
    numeroconvenio: 'CE-2024-004',
    titulo: 'Acta Complementaria - Capacitación Empresarial',
    fechaFirmaConvenio: '2024-04-05',
    fechaInicioActividades: '2024-05-01',
    fechaFinConvenio: '2024-10-31',
    comentarioOpcional: 'Capacitación en gestión empresarial.',
    estado: EstadoConvenio.Vigente,
    esActa: true,
    numeroResolucion: 'RES-2024-095',
    refrendado: false,
    convenioMarcoId: 4,
    convenioMarco: mockConveniosMarco[3],
    empresa: mockEmpresas[3],
    involucrados: [mockInvolucrados[1]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[5]],
  },
  {
    id: 5,
    numeroconvenio: 'CE-2024-005',
    titulo: 'Pasantías en Sistemas de Información',
    fechaFirmaConvenio: '2024-06-01',
    fechaInicioActividades: '2024-07-01',
    fechaFinConvenio: '2024-12-31',
    comentarioOpcional: 'Pasantías en desarrollo de aplicaciones web.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-145',
    refrendado: true,
    convenioMarcoId: undefined,
    convenioMarco: mockConveniosMarco[6],
    empresa: mockEmpresas[6],
    involucrados: [mockInvolucrados[0], mockInvolucrados[3]],
    documentosAdjuntos: [mockArchivos[4]],
    carrerasInvolucradas: [mockCarreras[0]],
  },
  {
    id: 6,
    numeroconvenio: 'CE-2024-006',
    titulo: 'Prácticas en Mantenimiento Eléctrico',
    fechaFirmaConvenio: '2024-03-15',
    fechaInicioActividades: '2024-04-15',
    fechaFinConvenio: '2024-09-15',
    comentarioOpcional: 'Prácticas en mantenimiento de redes eléctricas.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-082',
    refrendado: true,
    convenioMarcoId: undefined,
    convenioMarco: mockConveniosMarco[6],
    empresa: mockEmpresas[7],
    involucrados: [mockInvolucrados[4]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[2]],
  },
  {
    id: 7,
    numeroconvenio: 'CE-2024-007',
    titulo: 'Intercambio Académico de Docentes',
    fechaFirmaConvenio: '2024-05-10',
    fechaInicioActividades: '2024-08-01',
    fechaFinConvenio: '2025-07-31',
    comentarioOpcional: 'Programa de intercambio con la UNC.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-123',
    refrendado: true,
    convenioMarcoId: 6,
    convenioMarco: mockConveniosMarco[5],
    empresa: mockEmpresas[5],
    involucrados: [mockInvolucrados[0], mockInvolucrados[4]],
    documentosAdjuntos: [],
    carrerasInvolucradas: mockCarreras,
  },
  {
    id: 8,
    numeroconvenio: 'CE-2023-025',
    titulo: 'Proyecto de Investigación Agronómica',
    fechaFirmaConvenio: '2023-10-05',
    fechaInicioActividades: '2023-11-01',
    fechaFinConvenio: '2024-04-30',
    comentarioOpcional: 'Proyecto finalizado de investigación en cultivos.',
    estado: EstadoConvenio.Finalizado,
    esActa: false,
    numeroResolucion: 'RES-2023-356',
    refrendado: true,
    convenioMarcoId: 5,
    convenioMarco: mockConveniosMarco[4],
    empresa: mockEmpresas[4],
    involucrados: [mockInvolucrados[0]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[4]],
  },
  {
    id: 9,
    numeroconvenio: 'CE-2024-008',
    titulo: 'Pasantías en Ingeniería Industrial',
    fechaFirmaConvenio: '2024-02-28',
    fechaInicioActividades: '2024-04-01',
    fechaFinConvenio: '2024-11-30',
    comentarioOpcional: 'Pasantías en gestión de producción.',
    estado: EstadoConvenio.Vigente,
    esActa: false,
    numeroResolucion: 'RES-2024-071',
    refrendado: true,
    convenioMarcoId: 3,
    convenioMarco: mockConveniosMarco[2],
    empresa: mockEmpresas[2],
    involucrados: [mockInvolucrados[1], mockInvolucrados[3]],
    documentosAdjuntos: [],
    carrerasInvolucradas: [mockCarreras[4]],
  },
  {
    id: 10,
    numeroconvenio: 'CE-2024-009',
    titulo: 'Convenio Específico en Borrador',
    fechaFirmaConvenio: undefined,
    fechaInicioActividades: undefined,
    fechaFinConvenio: undefined,
    comentarioOpcional: 'Convenio en proceso de elaboración.',
    estado: EstadoConvenio.Borrador,
    esActa: false,
    numeroResolucion: undefined,
    refrendado: false,
    convenioMarcoId: 1,
    convenioMarco: mockConveniosMarco[0],
    empresa: mockEmpresas[0],
    involucrados: [],
    documentosAdjuntos: [],
    carrerasInvolucradas: [],
  },
]

// ============================================================================
// SERVICIO MOCK
// ============================================================================

export default class MockDataService {
  // Simular delay de red
  private static async delay(ms: number = 300): Promise<void> {
    return new Promise((resolve) => setTimeout(resolve, ms))
  }

  // ========================================================================
  // CONVENIOS - BÚSQUEDA Y LISTADO
  // ========================================================================

  static async GetConvenios(
    body: IConvenioQueryObject,
  ): Promise<Result<ConvenioEspecificoDto[] | ConvenioMarcoDto[]>> {
    await this.delay()

    try {
      // Determinar el tipo de convenio
      const convenioType = this.extractConvenioType(body)

      let results: ConvenioEspecificoDto[] | ConvenioMarcoDto[] = []

      if (convenioType === 'marco') {
        results = [...mockConveniosMarco]
      } else if (convenioType === 'especifico') {
        results = [...mockConveniosEspecificos]
      } else {
        // Si no se especifica tipo, devolver vacío (no mezclar tipos)
        return { isSuccess: true, value: [], status: 200 }
      }

      // Aplicar filtros
      results = this.applyFilters(results, body) as ConvenioEspecificoDto[] | ConvenioMarcoDto[]

      return { isSuccess: true, value: results, status: 200 }
    } catch (error: any) {
      return {
        isSuccess: false,
        error: { message: 'Error al obtener convenios', status: 500 },
      }
    }
  }

  // ========================================================================
  // CONVENIOS MARCO - DETALLE
  // ========================================================================

  static async GetConvenioMarcoCompleto(id: number): Promise<Result<InfoConvenioMarcoDto>> {
    await this.delay()

    const convenio = mockInfoConveniosMarco.find((c) => c.id === id)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Marco no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio, status: 200 }
  }

  // ========================================================================
  // CONVENIOS ESPECÍFICOS - DETALLE
  // ========================================================================

  static async GetConvenioEspecificoCompleto(
    id: number,
  ): Promise<Result<InfoConvenioEspecificoDto>> {
    await this.delay()

    const convenio = mockInfoConveniosEspecificos.find((c) => c.id === id)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Específico no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio, status: 200 }
  }

  // ========================================================================
  // DELETE - CONVENIOS
  // ========================================================================

  static async DeleteConvenioMarco(id: number): Promise<Result<null>> {
    await this.delay()

    const index = mockConveniosMarco.findIndex((c) => c.id === id)

    if (index === -1) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Marco no encontrado', status: 404 },
      }
    }

    mockConveniosMarco.splice(index, 1)
    return { isSuccess: true, value: null, status: 200 }
  }

  static async DeleteConvenioEspecifico(id: number): Promise<Result<null>> {
    await this.delay()

    const index = mockConveniosEspecificos.findIndex((c) => c.id === id)

    if (index === -1) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Específico no encontrado', status: 404 },
      }
    }

    mockConveniosEspecificos.splice(index, 1)
    return { isSuccess: true, value: null, status: 200 }
  }

  // ========================================================================
  // CREATE - CONVENIOS
  // ========================================================================

  static async CreateConvenioMarco(
    Dto: CargarConvenioMarcoRequestDto,
  ): Promise<Result<ConvenioCreated>> {
    await this.delay()

    const newId = Math.max(...mockConveniosMarco.map((c) => c.id)) + 1

    const newConvenio: ConvenioMarcoDto = {
      id: newId,
      titulo: Dto.insertConvenioDto.titulo || 'Nuevo Convenio Marco',
      numeroconvenio: Dto.insertConvenioDto.numeroConvenio || `CM-2024-${String(newId).padStart(3, '0')}`,
      nombreEmpresa: mockEmpresas.find((e) => e.id === Dto.insertEmpresaDto?.id)?.nombre_Empresa || null,
      fechaFirmaConvenio: Dto.insertConvenioDto.fechaFirmaConvenio || null,
      fechaFin: Dto.insertConvenioDto.fechaFin || null,
      convenioType: 'marco',
      estado: Dto.insertConvenioDto.estado || EstadoConvenio.Borrador,
      refrendado: Dto.insertConvenioDto.refrendado || false,
    }

    mockConveniosMarco.push(newConvenio)

    return {
      isSuccess: true,
      value: { id: newId, ConvenioType: 'marco' },
      status: 201,
    }
  }

  static async CreateConvenioEspecifico(
    Dto: CargarConvenioEspecificoRequestDto,
  ): Promise<Result<ConvenioCreated>> {
    await this.delay()

    const newId = Math.max(...mockConveniosEspecificos.map((c) => c.id)) + 1

    const newConvenio: ConvenioEspecificoDto = {
      id: newId,
      titulo: Dto.insertConvenioDto.titulo || 'Nuevo Convenio Específico',
      numeroconvenio: Dto.insertConvenioDto.numeroConvenio || `CE-2024-${String(newId).padStart(3, '0')}`,
      nombreEmpresa: mockEmpresas.find((e) => e.id === Dto.insertEmpresaDto?.id)?.nombre_Empresa || null,
      fechaFirmaConvenio: Dto.insertConvenioDto.fechaFirmaConvenio || null,
      fechaInicioActividades: Dto.insertConvenioDto.fechaInicioActividades || null,
      fechaFin: Dto.insertConvenioDto.fechaFinConvenio || null,
      convenioType: 'especifico',
      estado: Dto.insertConvenioDto.estado || EstadoConvenio.Borrador,
      esActa: Dto.insertConvenioDto.esActa || false,
      refrendado: Dto.insertConvenioDto.refrendado || false,
    }

    mockConveniosEspecificos.push(newConvenio)

    return {
      isSuccess: true,
      value: { id: newId, ConvenioType: 'especifico' },
      status: 201,
    }
  }

  // ========================================================================
  // UPDATE - CONVENIOS
  // ========================================================================

  static async EditarConvenioMarco(Dto: UpdateConvenioMarcoRequetsDto): Promise<Result<null>> {
    await this.delay()

    const index = mockConveniosMarco.findIndex((c) => c.id === Dto.updateConvenioMarcoDto.id)

    if (index === -1) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Marco no encontrado', status: 404 },
      }
    }

    // Actualizar convenio en la lista
    mockConveniosMarco[index] = {
      ...mockConveniosMarco[index],
      titulo: Dto.updateConvenioMarcoDto.titulo || mockConveniosMarco[index].titulo,
      numeroconvenio: Dto.updateConvenioMarcoDto.numeroConvenio || mockConveniosMarco[index].numeroconvenio,
      fechaFirmaConvenio: Dto.updateConvenioMarcoDto.fechaFirmaConvenio || mockConveniosMarco[index].fechaFirmaConvenio,
      fechaFin: Dto.updateConvenioMarcoDto.fechaFin || mockConveniosMarco[index].fechaFin,
      estado: Dto.updateConvenioMarcoDto.estado !== undefined ? Dto.updateConvenioMarcoDto.estado : mockConveniosMarco[index].estado,
      refrendado: Dto.updateConvenioMarcoDto.refrendado !== undefined ? Dto.updateConvenioMarcoDto.refrendado : mockConveniosMarco[index].refrendado,
    }

    return { isSuccess: true, value: null, status: 200 }
  }

  static async EditarConvenioEspecifico(
    Dto: UpdateConvenioEspecificoRequestDto,
  ): Promise<Result<null>> {
    await this.delay()

    const index = mockConveniosEspecificos.findIndex((c) => c.id === Dto.updateConvenioDto.id)

    if (index === -1) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Específico no encontrado', status: 404 },
      }
    }

    // Actualizar convenio en la lista
    mockConveniosEspecificos[index] = {
      ...mockConveniosEspecificos[index],
      titulo: Dto.updateConvenioDto.titulo || mockConveniosEspecificos[index].titulo,
      numeroconvenio: Dto.updateConvenioDto.numeroConvenio || mockConveniosEspecificos[index].numeroconvenio,
      fechaFirmaConvenio:
        Dto.updateConvenioDto.fechaFirmaConvenio || mockConveniosEspecificos[index].fechaFirmaConvenio,
      fechaInicioActividades:
        Dto.updateConvenioDto.fechaInicioActividades || mockConveniosEspecificos[index].fechaInicioActividades,
      fechaFin: Dto.updateConvenioDto.fechaFinConvenio || mockConveniosEspecificos[index].fechaFin,
      estado: Dto.updateConvenioDto.estado !== undefined ? Dto.updateConvenioDto.estado : mockConveniosEspecificos[index].estado,
      esActa: Dto.updateConvenioDto.esActa !== undefined ? Dto.updateConvenioDto.esActa : mockConveniosEspecificos[index].esActa,
      refrendado:
        Dto.updateConvenioDto.refrendado !== undefined ? Dto.updateConvenioDto.refrendado : mockConveniosEspecificos[index].refrendado,
    }

    return { isSuccess: true, value: null, status: 200 }
  }

  // ========================================================================
  // EMPRESAS
  // ========================================================================

  static async GetEmpresas(): Promise<ComboBoxEmpresasDto[]> {
    await this.delay()

    return mockEmpresas.map((e) => ({
      idEmpresa: e.id,
      nombreEmpresa: e.nombre_Empresa,
    }))
  }

  // ========================================================================
  // BÚSQUEDA POR NÚMERO DE CONVENIO
  // ========================================================================

  static async GetIdConvMarcoByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    await this.delay()

    const convenio = mockConveniosMarco.find((c) => c.numeroconvenio === numeroConvenio)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Marco no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio.id, status: 200 }
  }

  static async GetIdConvEspByNumeroConv(numeroConvenio: string): Promise<Result<number>> {
    await this.delay()

    const convenio = mockConveniosEspecificos.find((c) => c.numeroconvenio === numeroConvenio)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio Específico no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio.id, status: 200 }
  }

  // ========================================================================
  // DESVINCULACIÓN
  // ========================================================================

  static async DesvincularConvenioEspecifico(
    idConvenioMarco: number,
    idConvenioEspecifico: number,
  ): Promise<Result<null>> {
    await this.delay()
    // Simulación: en un sistema real, esto actualizaría la relación
    return { isSuccess: true, value: null, status: 200 }
  }

  static async DesvincularEmpresaDeMarco(idConvenioMarco: number): Promise<Result<null>> {
    await this.delay()
    return { isSuccess: true, value: null, status: 200 }
  }

  static async DesvincularConvenioMarco(idConvenioEspecifico: number): Promise<Result<null>> {
    await this.delay()
    return { isSuccess: true, value: null, status: 200 }
  }

  static async DesvincularEmpresaDeEspecifico(
    idConvenioEspecifico: number,
  ): Promise<Result<null>> {
    await this.delay()
    return { isSuccess: true, value: null, status: 200 }
  }

  // ========================================================================
  // ARCHIVOS (SIMULADOS)
  // ========================================================================

  static async CargarArchivoToMarco(
    nombreArchivo: string,
    file: File,
    convenioMarcoId: number,
  ): Promise<ViewArchivoDto | null> {
    await this.delay()

    const newArchivo: ViewArchivoDto = {
      idArchivo: mockArchivos.length + 1,
      nombreArchivo: nombreArchivo,
    }

    mockArchivos.push(newArchivo)
    return newArchivo
  }

  static async CargarArchivoToEspecifico(
    nombreArchivo: string,
    file: File,
    convenioEspecificoId: number,
  ): Promise<ViewArchivoDto | null> {
    await this.delay()

    const newArchivo: ViewArchivoDto = {
      idArchivo: mockArchivos.length + 1,
      nombreArchivo: nombreArchivo,
    }

    mockArchivos.push(newArchivo)
    return newArchivo
  }

  static async EliminarArchivo(idDocumento: number): Promise<boolean> {
    await this.delay()
    const index = mockArchivos.findIndex((a) => a.idArchivo === idDocumento)
    if (index !== -1) {
      mockArchivos.splice(index, 1)
      return true
    }
    return false
  }

  static async DescargarArchivo(idDocumento: number, nombreArchivo: string): Promise<void> {
    await this.delay()
    console.log(`[MOCK] Descargando archivo: ${nombreArchivo} (ID: ${idDocumento})`)
    // En un entorno real, esto descargaría el archivo
    alert(`[DEMO MODE] Descarga simulada: ${nombreArchivo}`)
  }

  static async GetArchivosConvMarco(idConvenio: number): Promise<Result<ViewArchivoDto[]>> {
    await this.delay()

    const convenio = mockInfoConveniosMarco.find((c) => c.id === idConvenio)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio.archivosAdjuntos || [], status: 200 }
  }

  static async GetArchivosConvEspecifico(idConvenio: number): Promise<Result<ViewArchivoDto[]>> {
    await this.delay()

    const convenio = mockInfoConveniosEspecificos.find((c) => c.id === idConvenio)

    if (!convenio) {
      return {
        isSuccess: false,
        error: { message: 'Convenio no encontrado', status: 404 },
      }
    }

    return { isSuccess: true, value: convenio.documentosAdjuntos || [], status: 200 }
  }

  // ========================================================================
  // MÉTODOS AUXILIARES - FILTRADO
  // ========================================================================

  private static extractConvenioType(body: IConvenioQueryObject): string {
    // Buscar en cada filtro el convenioType
    if (body.ByTitulo?.convenioType) return body.ByTitulo.convenioType
    if (body.ByNumeroResolucion?.convenioType) return body.ByNumeroResolucion.convenioType
    if (body.ByNumeroConvenio?.convenioType) return body.ByNumeroConvenio.convenioType
    if (body.ByEmpresa?.convenioType) return body.ByEmpresa.convenioType
    if (body.ByIsActa?.convenioType) return body.ByIsActa.convenioType
    if (body.ByIsRefrendado?.convenioType) return body.ByIsRefrendado.convenioType
    if (body.ByEstado?.convenioType) return body.ByEstado.convenioType
    if (body.ByCarrera?.conveniotype) return body.ByCarrera.conveniotype
    if (body.ByFechaFirma?.convenioType) return body.ByFechaFirma.convenioType
    if (body.ByFechaFin?.convenioType) return body.ByFechaFin.convenioType
    if (body.ByAntiguedadDto?.convenioType) return body.ByAntiguedadDto.convenioType
    if (body.ByProximosAvencer?.convenioType) return body.ByProximosAvencer.convenioType
    if (body.ByMes?.convenioType) return body.ByMes.convenioType
    if (body.ByAño?.convenioType) return body.ByAño.convenioType
    if (body.ByDesdeHastaDto?.convenioType) return body.ByDesdeHastaDto.convenioType

    return '' // Devolver todos si no se especifica
  }

  private static applyFilters(
    results: (ConvenioEspecificoDto | ConvenioMarcoDto)[],
    body: IConvenioQueryObject,
  ): (ConvenioEspecificoDto | ConvenioMarcoDto)[] {
    let filtered = [...results]

    // Filtro por título
    if (body.ByTitulo?.Title) {
      const searchTerm = body.ByTitulo.Title.toLowerCase()
      filtered = filtered.filter((c) => c.titulo?.toLowerCase().includes(searchTerm))
    }

    // Filtro por número de convenio
    if (body.ByNumeroConvenio?.NumeroConvenio) {
      const searchTerm = body.ByNumeroConvenio.NumeroConvenio.toLowerCase()
      filtered = filtered.filter((c) => c.numeroconvenio?.toLowerCase().includes(searchTerm))
    }

    // Filtro por empresa
    if (body.ByEmpresa?.EmpresaName) {
      const searchTerm = body.ByEmpresa.EmpresaName.toLowerCase()
      filtered = filtered.filter((c) => c.nombreEmpresa?.toLowerCase().includes(searchTerm))
    }

    // Filtro por estado
    if (body.ByEstado?.Estado !== undefined && body.ByEstado?.Estado !== null) {
      filtered = filtered.filter((c) => c.estado === body.ByEstado!.Estado)
    }

    // Filtro por refrendado
    if (body.ByIsRefrendado?.refrendado !== undefined && body.ByIsRefrendado?.refrendado !== null) {
      filtered = filtered.filter((c) => c.refrendado === body.ByIsRefrendado!.refrendado)
    }

    // Filtro por acta (solo para específicos)
    if (body.ByIsActa?.IsActa !== undefined && body.ByIsActa?.IsActa !== null) {
      filtered = filtered.filter((c) => {
        if ('esActa' in c) {
          return c.esActa === body.ByIsActa!.IsActa
        }
        return false
      })
    }

    // Filtro por fecha de firma
    if (body.ByFechaFirma?.FechaInicio) {
      filtered = filtered.filter((c) => c.fechaFirmaConvenio === body.ByFechaFirma!.FechaInicio)
    }

    // Filtro por fecha fin
    if (body.ByFechaFin?.FechaFin) {
      filtered = filtered.filter((c) => c.fechaFin === body.ByFechaFin!.FechaFin)
    }

    // Filtro por mes
    if (body.ByMes?.month !== undefined && body.ByMes?.year !== undefined) {
      filtered = filtered.filter((c) => {
        if (!c.fechaFirmaConvenio) return false
        const fecha = new Date(c.fechaFirmaConvenio)
        return fecha.getMonth() + 1 === body.ByMes!.month && fecha.getFullYear() === body.ByMes!.year
      })
    }

    // Filtro por año
    if (body.ByAño?.year !== undefined) {
      filtered = filtered.filter((c) => {
        if (!c.fechaFirmaConvenio) return false
        const fecha = new Date(c.fechaFirmaConvenio)
        return fecha.getFullYear() === body.ByAño!.year
      })
    }

    // Filtro por rango de fechas
    if (body.ByDesdeHastaDto?.desde && body.ByDesdeHastaDto?.hasta) {
      const desde = new Date(body.ByDesdeHastaDto.desde)
      const hasta = new Date(body.ByDesdeHastaDto.hasta)

      filtered = filtered.filter((c) => {
        if (!c.fechaFirmaConvenio) return false
        const fecha = new Date(c.fechaFirmaConvenio)
        return fecha >= desde && fecha <= hasta
      })
    }

    // Ordenar por antigüedad si se solicita
    if (body.ByAntiguedadDto) {
      filtered.sort((a, b) => {
        const fechaA = a.fechaFirmaConvenio ? new Date(a.fechaFirmaConvenio).getTime() : 0
        const fechaB = b.fechaFirmaConvenio ? new Date(b.fechaFirmaConvenio).getTime() : 0

        if (body.ByAntiguedadDto!.ascendente) {
          return fechaA - fechaB
        } else {
          return fechaB - fechaA
        }
      })
    }

    // Filtro próximos a vencer (convenios que vencen en los próximos 30 días)
    if (body.ByProximosAvencer) {
      const hoy = new Date()
      const treintaDias = new Date()
      treintaDias.setDate(hoy.getDate() + 30)

      filtered = filtered.filter((c) => {
        if (!c.fechaFin) return false
        const fechaFin = new Date(c.fechaFin)
        return fechaFin >= hoy && fechaFin <= treintaDias
      })
    }

    return filtered
  }
}
