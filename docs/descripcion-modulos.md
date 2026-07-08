# Documentación de Módulos — SistemaConveniosUTNSanFrancisco

> Documentación complementaria al diagrama UML de Casos de Uso (`docs/Diagrama Casos de Uso.drawio`).
> Estructura por módulo según consigna: Objetivo, Usuarios, Pantallas, Interfaces relacionadas.

## Contexto general del sistema

- **Nombre**: Sistema de gestión de convenios para Secretaría de Vinculación Tecnológica — UTN San Francisco.
- **Arquitectura**: API REST en .NET (Backend) + SPA en Vue.js 3 + TypeScript (Frontend).
- **Actor físico único**: El sistema **no implementa autenticación ni autorización** (sin `[Authorize]`, JWT, Identity ni roles enforced). Existe un único actor físico "Usuario" que opera todo el sistema.
- **Actores lógicos modelados (roles conceptuales UML)**: Para satisfacer la consigna "todos los tipos de actores involucrados", se modelan tres actores lógicos como roles funcionales derivados del uso. Un mismo usuario físico puede actuar bajo distintos roles según la operación que realice:
  - **Usuario Consultor**: rol de solo lectura (búsquedas, listados, visualización de detalles, reportes).
  - **Usuario Gestor**: rol de escritura sobre convenios (cargar, actualizar, asociar entidades).
  - **Usuario Firmante**: rol de carga de documentos adjuntos a convenios.
- **Almacenamiento de documentos**: LOCAL, en el sistema de archivos del servidor (sin Azure/S3/Blob ni servicios externos).
- **API base (Frontend → Backend)**: `http://localhost:8888/api` (configurada en `UserInterface/src/Services/apiBaseService.ts`).

---

## Módulo: Panel de Búsqueda

- **Objetivo**: Permitir la búsqueda y filtrado de convenios (marcos, específicos o ambos) mediante múltiples criterios paramétricos, búsquedas directas y reportes de conteo.

- **Usuarios**:
  - `Usuario Consultor` — ejecuta todas las búsquedas y reportes del módulo.

- **Pantallas**:
  - `DashBoardView.vue` — pantalla principal (`/`) que lista convenios y da acceso al panel de filtros.
  - `FilterPanel.vue` — panel lateral (offcanvas) donde se selecciona el tipo de convenio y el filtro a aplicar.
  - `SearchBar.vue` (shared) — barra de búsqueda genérica.
  - Componentes de búsqueda específicos por filtro (15 vistas/componentes en `modules/convenios/components/search/`):
    - `SearchByTitle.vue`, `SearchByEmpresa.vue`, `SearchByEstado.vue`,
      `SearchByNumeroResolucion.vue`, `SearchByNumeroConvenio.vue`,
      `SearchByAreas.vue`, `SearchByFechaFirma.vue`, `SearchByFechaFin.vue`,
      `SearchByAntiguedad.vue`, `SearchByMes.vue`, `SearchByAnio.vue`,
      `SearchByDesdeHasta.vue`, `SearchCountByMes.vue`, `SearchCountByRango.vue`.
  - `CountConveniosResult.vue` — visualización de resultados de los reportes de cantidad.

- **Casos de uso** (6 UC, todos asociados a `Usuario Consultor`):
  1. **Buscar convenios por criterio** (marcos / específicos / ambos)
     - Agrupa los 12 filtros paramétricos: Título, Empresa, Estado, N° Resolución, N° Convenio, Área (solo específicos), Fecha Firma, Fecha Fin, Antigüedad, Mes Firma, Año Firma, Rango Firma.
  2. **Ver convenios próximos a vencer** — búsqueda directa, sin input.
  3. **Ver convenios refrendados** — búsqueda directa.
  4. **Ver convenios con acta** (específicos únicamente) — búsqueda directa.
  5. **Consultar cantidad de convenios firmados por mes** — reporte de conteo.
  6. **Consultar cantidad de convenios firmados por rango** — reporte de conteo.

- **Interfaces relacionadas**:
  - **API REST .NET**: `GET /api/ConveniosMarcos`, `GET /api/ConveniosEspecificos` — listado base.
  - **Commands de filtro** (`APIconvenios/Commands/FilterCommands/Commands/`) — 15 comandos especializados:
    `SearchByTitleCmd`, `SearchByEmpresaCmd`, `SearchByEstadoCmd`,
    `SearchByNumeroResolucionCmd`, `SearchByNumeroConvenioCmd`,
    `SearchByAreasCmd`, `SearchByFechaFirmaCmd`, `SearchByFechaFinCmd`,
    `SearchByAntiguedadCmd`, `SearchByMesCmd`, `SearchByAnioCmd`,
    `SearchByDesdeHastaCmd`, `SearchActaCmd`, `SearchByRefrendadoCmd`,
    `SearchProximosAvencerCmd`.
  - **Base de datos**: lectura de tablas `ConveniosMarcos` y `ConveniosEspecificos` (paginada vía `ConvenioQueryObject`).

---

## Módulo: Empresas

- **Objetivo**: Permitir la administración completa de empresas: visualización, registro, edición, y consulta de los convenios vinculados a cada empresa.

- **Usuarios**:
  - `Usuario Consultor` — visualiza y lista empresas y sus convenios.
  - `Usuario Gestor` — registra nuevas empresas y edita información de empresas existentes.

- **Pantallas**:
  - `DashboardEmpresas.vue` — dashboard de empresas (`/empresas`) con grilla de cards. Cada card muestra el nombre de la empresa, botón "Ver Convenios" y un botón de edición (lápiz) en la esquina superior que aparece al hacer hover.
  - `EmpresaConveniosView.vue` — listado de convenios por empresa (`/empresa/:id/convenios`).
  - Componentes:
    - `EmpresaCard.vue`, `EmpresaCardReadOnly.vue`, `EmpresaAsociada.vue`.
    - `modals/CreateEmpresaModal.vue` — modal para registrar una nueva empresa con todos sus campos (nombre, razón social, CUIT, dirección, teléfono, email).
    - `modals/EditEmpresaModal.vue` — modal para editar los datos de una empresa existente, con los campos precargados.

- **Casos de uso** (4 UC, distribuidos por actor lógico):

  | # | Caso de uso | Actor |
  |---|---|---|
  | 1 | Visualizar empresas con convenios | Usuario Consultor |
  | 2 | Listar convenios por empresa | Usuario Consultor |
  | 3 | Registrar empresa | Usuario Gestor |
  | 4 | Editar empresa | Usuario Gestor |

- **Interfaces relacionadas**:
  - **API REST .NET**:
    - `GET /api/Empresa` (paginado), `GET /api/Empresa/all` — listado de empresas.
    - `GET /api/Empresa/{id}` — datos completos de una empresa específica.
    - `POST /api/Empresa` — registro de nueva empresa.
    - `PUT /api/Empresa/{id}` — edición de empresa existente.
    - `GET /api/Convenios/empresa/{id}` — convenios de una empresa.
  - **Base de datos**: tabla `Empresas` con lectura/escritura, y join con convenios asociados.

---

## Módulo: Involucrados

- **Objetivo**: Permitir la visualización y filtrado de personas involucradas en convenios, agrupadas por área, y ver los convenios en los que participa cada persona.

- **Usuarios**:
  - `Usuario Consultor` — visualiza y filtra involucrados, y consulta sus convenios.

- **Pantallas**:
  - `InvolucradosPorAreaView.vue` — involucrados filtrados por área (`/involucrados-por-area`).
  - `InvolucradoConveniosView.vue` — convenios en los que participa una persona (`/involucrado/:id/convenios`).
  - Componentes:
    - `InvolucradosViewCard.vue`, `InvolucradosExistingCard.vue`, `InvolucradosExistentesSelector.vue`,
      `InvolucradosCard.vue`, `InvolucradoForm.vue`, `AreasCardList.vue`.

- **Casos de uso** (3 UC, todos asociados a `Usuario Consultor`):
  1. **Visualizar personas involucradas**.
  2. **Filtrar personas por área**.
  3. **Ver convenios por persona**.

- **Interfaces relacionadas**:
  - **API REST .NET**:
    - `GET /api/Involucrados` — listado de involucrados (combo box).
    - `GET /api/Involucrados/available/{idConvenio}` — disponibles para asociar a un convenio.
    - `GET /api/Involucrados/area/{areaId}` — filtrado por área.
    - `POST /api/Involucrados/validate` — validación de existencia.
    - `GET /api/Convenios/involucrado/{id}` — convenios en los que participa una persona.
  - **Base de datos**: lectura de tabla `Involucrados` con relación a `Carreras` (áreas).
  - **Nota**: Los involucrados **no se crean standalone**: se asocian a convenios desde los módulos de Convenios Marcos/Específicos. El `enum Roles` (`Docente/Alumno/Secretario/Externo`) que aparece en `Involucrado.RolInvolucrado` es el rol **dentro de un convenio** (firmante/suscriptor), **no** un rol de usuario del sistema.

---

## Módulo: Convenios Marcos

- **Objetivo**: Permitir la administración completa de convenios marco: carga, edición, visualización de detalles, asociación de empresas, involucrados y convenios específicos vinculados, y carga de documentos adjuntos.

- **Usuarios**:
  - `Usuario Consultor` — visualiza detalles de convenios marcos.
  - `Usuario Gestor` — carga, actualiza y asocia entidades (empresas, involucrados, convenios específicos).
  - `Usuario Firmante` — sube documentos adjuntos.

- **Pantallas**:
  - `DashBoardView.vue` — listado base (`/`).
  - `ConvenioMarcoView.vue` — vista de detalle de un convenio marco (`/ConvenioMarco/:id`).
  - `EditConvenioMarcoView.vue` — edición (`/editConvenioMarco/:id`).
  - `CargaConvMarcoView.vue` — alta de convenio marco (`/CargarConvenioMarco`).
  - Componentes:
    - `ConvMarcoCard.vue`, `ConvMarcoCardReadOnly.vue`.
    - `ConveniosEspecificosTable.vue` — tabla de específicos asociados.
    - `FileUploader.vue` — carga de documentos.
    - `VincularConvEspecifico.vue` — asociación de específicos.
    - Componentes de empresa/involucrado compartidos (ver módulos anteriores).

- **Casos de uso** (7 UC, distribuidos por actor lógico):

  | # | Caso de uso | Actor |
  |---|---|---|
  | 1 | Cargar convenio marco | Usuario Gestor |
  | 2 | Actualizar convenio marco | Usuario Gestor |
  | 3 | Ver detalles de convenio marco | Usuario Consultor |
  | 4 | Asociar empresas a convenio marco | Usuario Gestor |
  | 5 | Asociar involucrados a convenio marco | Usuario Gestor |
  | 6 | Asociar convenios específicos | Usuario Gestor |
  | 7 | Subir documentos (Word, PDF, Excel, etc.) | Usuario Firmante |

- **Interfaces relacionadas**:
  - **API REST .NET**:
    - `GET /api/ConveniosMarcos`, `GET /api/ConveniosMarcos/{id}` — listado y detalle.
    - `POST /api/ConveniosMarcos` — alta.
    - `PUT /api/ConveniosMarcos` — actualización.
    - `DELETE /api/ConveniosMarcos/{id}` — borrado.
    - `DELETE /api/ConveniosMarcos/{idConvenioMarco}/especificos/{idConvenioEspecifico}` — desvincular específico.
    - `DELETE /api/ConveniosMarcos/{idConvenioMarco}/empresa` — desvincular empresa.
    - `GET /api/ConveniosMarcos/archivos/{idConvenio}` — archivos asociados.
  - **Servicio de documentos**: `POST /api/Documents` (multipart/form-data) — subida de archivos al FS local del servidor.
  - **Base de datos**: tablas `ConveniosMarcos`, `Empresas`, `Involucrados`, `ArchivosAdjuntos`, `ConveniosEspecificos`.

---

## Módulo: Convenios Específicos

- **Objetivo**: Permitir la administración completa de convenios específicos: carga, edición, visualización de detalles, asociación a convenio marco, asociación de empresas e involucrados, y carga de documentos adjuntos.

- **Usuarios**:
  - `Usuario Consultor` — visualiza detalles de convenios específicos.
  - `Usuario Gestor` — carga, actualiza y asocia entidades (convenio marco, empresas, involucrados).
  - `Usuario Firmante` — sube documentos adjuntos.

- **Pantallas**:
  - `DashBoardView.vue` — listado base (`/`).
  - `ConvenioEspecificoView.vue` — vista de detalle (`/ConvenioEspecifico/:id`).
  - `EditConvenioEspecificoView.vue` — edición (`/editConvenioEspecifico/:id`).
  - `CargaConvEspecificoView.vue` — alta (`/CargarConvenioEspecifico`).
  - Componentes:
    - `ConvEspecificoCard.vue`, `ConvEspecificoCardReadOnly.vue`.
    - `FileUploader.vue` — carga de documentos.
    - `VincularConvMarco.vue` — asociación a convenio marco.
    - Componentes de empresa/involucrado compartidos.

- **Casos de uso** (7 UC, distribuidos por actor lógico):

  | # | Caso de uso | Actor |
  |---|---|---|
  | 1 | Cargar convenio específico | Usuario Gestor |
  | 2 | Actualizar convenio específico | Usuario Gestor |
  | 3 | Ver detalles de convenio específico | Usuario Consultor |
  | 4 | Asociar convenio marco | Usuario Gestor |
  | 5 | Asociar empresas a convenio específico | Usuario Gestor |
  | 6 | Asociar involucrados a convenio específico | Usuario Gestor |
  | 7 | Subir documentos (Word, PDF, Excel, etc.) | Usuario Firmante |

- **Interfaces relacionadas**:
  - **API REST .NET**:
    - `GET /api/ConveniosEspecificos`, `GET /api/ConveniosEspecificos/{id}` — listado y detalle.
    - `POST /api/ConveniosEspecificos` — alta.
    - `PUT /api/ConveniosEspecificos` — actualización.
    - `DELETE /api/ConveniosEspecificos/{id}` — borrado.
    - `DELETE /api/ConveniosEspecificos/{idConvenioEspecifico}/marco` — desvincular convenio marco.
    - `DELETE /api/ConveniosEspecificos/{idConvenioEspecifico}/empresa` — desvincular empresa.
    - `GET /api/ConveniosEspecificos/archivos/{idConvenio}` — archivos asociados.
  - **Servicio de documentos**: `POST /api/Documents` (multipart/form-data) — subida de archivos al FS local del servidor.
  - **Base de datos**: tablas `ConveniosEspecificos`, `ConveniosMarcos` (relación jerárquica), `Empresas`, `Involucrados`, `ArchivosAdjuntos`.

---

## Vista de Conjunto — Relaciones entre módulos

```
                    ┌─────────────────────────────────────────┐
                    │   SistemaConveniosUTNSanFrancisco       │
                    │                                         │
  Usuario Consultor │  ┌──────────────┐    filtra            │
      ──────────────┼─►│ Panel de     │──────────────┐       │
                    │  │ Búsqueda     │              │       │
                    │  └──────────────┘              ▼       │
                    │  ┌──────────────┐    asocia  ┌──────────────┐
                    │  │ Empresas     │◄────────►│ Convenios    │
                    │  └──────────────┘          │  Marcos      │
      Usuario Gestor │  ┌──────────────┐    asocia │              │
      ──────────────┼─►│ Involucrados │◄────────►│              │
                    │  └──────────────┘          └──────┬───────┘
                    │                                   │ asocia
                    │                                   │ (jerárquica)
                    │                                   ▼
                    │                          ┌──────────────┐
      Usuario       │                          │ Convenios    │
      Firmante      │                          │ Específicos  │
      ──────────────┼─────────────────────────►│              │
                    │                          └──────────────┘
                    └─────────────────────────────────────────┘
```

### Resumen de relaciones

| Origen | Destino | Tipo |
|---|---|---|
| Panel de Búsqueda | Convenios Marcos / Específicos | filtra (dependencia de consulta) |
| Empresas | Convenios Marcos / Específicos | asocia (bidireccional) |
| Involucrados | Convenios Marcos / Específicos | asocia (bidireccional) |
| Convenios Marcos | Convenios Específicos | asocia jerárquica (bidireccional) |

### Reparto de actores por módulo

| Módulo | Consultor | Gestor | Firmante |
|---|:---:|:---:|:---:|
| Panel de Búsqueda | ✅ (6 UC) | ❌ | ❌ |
| Empresas | ✅ (2 UC) | ✅ (2 UC) | ❌ |
| Involucrados | ✅ (3 UC) | ❌ | ❌ |
| Convenios Marcos | ✅ (1 UC) | ✅ (5 UC) | ✅ (1 UC) |
| Convenios Específicos | ✅ (1 UC) | ✅ (5 UC) | ✅ (1 UC) |

**Total**: 23 casos de uso repartidos en 3 actores lógicos sobre 5 módulos.

---

## Notas técnicas

1. **Actor único físico**: El sistema no distingue roles enforced en runtime. Los tres actores lógicos (Consultor/Gestor/Firmante) son un modelado UML legítimo basado en **roles conceptuales** (un actor UML representa cualquier rol funcional, no necesariamente un login con un permiso específico), y colapsan en el `Usuario` físico único que opera todo el sistema.

2. **El `enum Roles`** (`Docente/Alumno/Secretario/Externo`) **no es** un rol de usuario del sistema. Es el rol que una persona ocupa **dentro de un convenio** como Involucrado (firmante/suscriptor). Modelarlo como actor del sistema sería técnicamente incorrecto.

3. **Almacenamiento de documentos**: local en el servidor (`FileStream` + `FileMode.Create`), no se invoca ningún servicio externo (Azure Blob, S3, etc.). No se modeló actor secundario externo por esta razón.

4. **"Un caso de uso por filtro"** sería un anti-patrón UML: un caso de uso representa un objetivo con valor medible para el actor, no un campo de búsqueda. Los 12 filtros paramétricos se agruparon en un único UC `Buscar convenios por criterio`; las búsquedas directas (Próximos a vencer, Refrendados, Actas) y los reportes (cantidad por mes/rango) son UC separados porque devuelven distinto tipo de valor.

5. **Interfaces (APIs/BD) en el diagrama**: según convención UML, las interfaces internas del propio sistema (API REST, base de datos) no se modelan en el diagrama de casos de uso — solo se documentan como "Interfaces relacionadas" en este archivo. Modelar BD o API como actor es un error clásico: son componentes internos, no actores externos.