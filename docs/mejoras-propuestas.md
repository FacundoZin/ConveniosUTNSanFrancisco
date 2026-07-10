# Mejoras Propuestas — Sistema de Gestión de Convenios UTN San Francisco

> Documento de prospectiva técnica elaborado a partir del análisis del código fuente,
> la arquitectura actual y las necesidades institucionales manifestadas durante el desarrollo del proyecto.

---

## 1. Autenticación, Autorización y Seguridad

### 1.1 Sistema de autenticación

**Problema actual**: El sistema no tiene ningún tipo de autenticación. Cualquier persona con acceso a la red puede operar todos los endpoints sin restricción.

**Propuesta**:

- Implementar **ASP.NET Core Identity** con JWT Bearer tokens.
- Los usuarios se autentican via `POST /api/auth/login` y reciben un access token (15-60 min) + refresh token rotativo.
- El frontend almacena el token en memoria (nunca en `localStorage` por riesgo XSS) y lo envía vía `Authorization: Bearer` header.
- El backend valida el token en cada request mediante el middleware `UseAuthentication()` + `UseAuthorization()`.
- Ruta de registro restringida: los nuevos usuarios solo pueden ser creados por un administrador existente, no hay auto-registro público.

**Endpoints nuevos**:
| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/login` | Iniciar sesión |
| POST | `/api/auth/refresh` | Renovar token |
| POST | `/api/auth/logout` | Invalidar refresh token |
| GET | `/api/auth/me` | Perfil del usuario actual |

### 1.2 Roles y permisos

**Problema actual**: Roles solo existen como conceptos UML. No hay enforcement en runtime.

**Propuesta**:

Implementar tres roles funcionales que sí se reflejen en claims del JWT:

| Rol | Permisos |
|-----|----------|
| `Consultor` | Lectura total: listados, búsquedas, detalle de convenios, descarga de archivos. Sin escritura. |
| `Gestor` | Lectura + escritura sobre convenios, empresas e involucrados. Asociar/desvincular entidades. |
| `Administrador` | Todos los permisos anteriores + gestión de usuarios del sistema, configuración global, borrado definitivo. |

El rol `Firmante` del modelo UML se absorbe dentro de `Gestor` (subir documentos es parte de la gestión del convenio). Si se necesita segregación más fina, se implementa vía permisos individuales (claims policy-based):

```csharp
[Authorize(Policy = "Convenios.Escribir")]
[Authorize(Policy = "Archivos.Subir")]
```

### 1.3 Política CORS segura

**Problema actual**: `AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()` en producción.

**Propuesta**: En producción, CORS debe restringirse al origen específico del frontend (ej: `https://convenios.utnsf.edu.ar`). En desarrollo se mantiene la política actual.

### 1.4 HTTPS

**Problema actual**: `UseHttpsRedirection` está comentado en `Program.cs`.

**Propuesta**: Habilitar redirect HTTP→HTTPS y agregar middleware HSTS en producción. El API nunca debe operar sobre HTTP plano en un entorno institucional.

---

## 2. Portal de Administración de Usuarios

**Propuesta**: Una vista accesible solo para `Administrador` que permita:

- Listar usuarios del sistema.
- Crear/editar/deshabilitar usuarios.
- Asignar roles.
- Ver última actividad y fecha de conexión.

Entidad nueva `Usuario` (independiente de `Involucrados` — un involucrado es una persona física que participa en convenios; un usuario es quien opera el sistema; pueden solaparse o no).

```
Usuario { Id, Email, PasswordHash, Nombre, Apellido, Rol, Activo, UltimoAcceso, CreadoEn }
```

---

## 3. Panel de Control y Dashboard Principal

### 3.1 Dashboard ejecutivo

**Propuesta**: Reemplazar el listado plano actual por un dashboard con:

- **Widgets de métricas**: total convenios activos, próximos a vencer (próximos 30 días), vencidos, empresas registradas.
- **Gráfico de barras**: convenios firmados por mes (últimos 12 meses).
- **Gráfico de torta**: distribución por estado (Borrador / Vigente / Finalizado).
- **Tabla de vencimientos próximos**: convenios que se vencen en los próximos X días, con indicador de urgencia (color rojo/amarillo/verde).
- **Últimas actividades**: feed con los cambios recientes realizados en el sistema (quién creó/ editó qué y cuándo).

Implementación recomendada: `vue-chartjs` para los gráficos, consumiendo endpoints específicos de estadísticas.

### 3.2 Tarjeta resumen de convenio

**Propuesta**: Enriquecer las cards de convenio con indicadores visuales:
- Badge de estado con color (Borrador = gris, Vigente = verde, Finalizado = rojo).
- Icono que indique si tiene acta adjunta.
- Indicador de días restantes para vencimiento.
- Sello visual si está refrendado.

---

## 4. Reportes y Estadísticas

### 4.1 Reportes exportables

**Propuesta**: Los reportes de conteo actuales (por mes, por rango) deben poder exportarse a PDF y Excel.

- **Excel**: Generación server-side con ClosedXML (open-source, Sin MIT, soporta .xlsx).
- **PDF**: Generación server-side con QuestPDF (API fluida, moderna) o client-side con jsPDF + html2canvas para reportes simples.
- Endpoint unificado `GET /api/reportes/{tipo}?formato=pdf|xlsx`.

### 4.2 Reportes nuevos

| Reporte | Descripción |
|---------|-------------|
| Convenios por empresa | Todos los convenios (marco + específicos) de una empresa, con estados y fechas |
| Convenios por área/carrera | Convenios específicos filtrados por carrera involucrada |
| Histórico por período | Todos los convenios firmados entre dos fechas, con métricas agregadas |
| Resumen institucional | Totales: convenios activos, empresas vinculadas, carreras involucradas, distribución por tipo |
| Convenios sin marco | Convenios específicos no vinculados a un marco (posible indicador de inconsistencia) |

### 4.3 Endpoint de estadísticas

```api
GET /api/estadisticas/resumen
→ { totalConvenios, activos, proximosAVencer, vencidos, totalEmpresas, totalInvolucrados }

GET /api/estadisticas/por-mes?anio=2026
→ [{ mes, cantidad }] (12 filas)

GET /api/estadisticas/por-estado
→ [{ estado, cantidad }]
```

---

## 5. Notificaciones

### 5.1 Notificaciones in-app

**Propuesta**: Sistema de notificaciones visuales dentro del frontend.

- Pinia store `notificacionStore` que maneja el estado de notificaciones (leídas / no leídas).
- Campanita en el header con contador de no leídas.
- Panel desplegable con las últimas notificaciones.
- Tipos de notificaciones auto-generadas:
  - Convenio próximo a vencer (N días antes configurable).
  - Convenio vencido.
  - Nuevo convenio creado por otro usuario (en entorno multi-usuario).

### 5.2 Notificaciones por email

**Propuesta**: Envío de alertas por correo electrónico usando `MailKit` o `FluentEmail`.

- Configuración SMTP almacenable (servidor, puerto, credenciales, remitente).
- Disparadores:
  - Convenio próximo a vencer (job diario + email).
  - Convenio que cambió de estado.
  - Reporte programado adjunto por email.
- Las notificaciones se envían a los Involucrados del convenio que tengan email cargado.

### 5.3 Notificaciones en tiempo real

**Propuesta**: SignalR hub para invalidación de datos en cliente sin polling.

- Cuando un recurso se modifica, el server notifica a los clientes conectados.
- Los clientes invalidan su caché local y refrescan la vista relevante.
- Útil en entorno multi-usuario para mantener sincronizadas las sesiones.

---

## 6. Gestión Avanzada de Archivos

### 6.1 Almacenamiento configurable

**Problema actual**: Ruta hardcodeada `C:\conveniosdocuments\`.

**Propuesta**: 
- La ruta se define en `appsettings.json` (`FileStorage:BasePath`).
- Se agrega método de extensión `UseFileStorage()` en `Program.cs` que valida que la ruta exista al iniciar y la crea si no existe.
- Se agrega soporte futuro para Azure Blob Storage o S3 reemplazando el proveedor de almacenamiento (patrón Strategy).

### 6.2 Previsualización de archivos

**Propuesta**: Vista previa inline de documentos sin descargar:

- **PDF**: `vue-pdf-embed` (renderiza con pdf.js en el navegador).
- **Imágenes**: Tag `<img>` nativo.
- **Documentos Office**: Enlace a vista previa de Google Docs / Microsoft Office Online, o enlace de descarga directa.

### 6.3 Versionado de archivos

**Propuesta**: Cuando se sube un archivo con el mismo nombre a un mismo convenio, no sobrescribir sino crear una nueva versión.

- Tabla `ArchivosAdjuntosVersion` o columna `Version` en la tabla actual.
- Se conserva el histórico de versiones anteriores.
- El frontend puede mostrar "Versión actual" + "Ver versiones anteriores".

### 6.4 Control de tipos de archivo

**Propuesta**: Validación server-side por magic bytes (no solo por extensión) al subir archivos. Configuración de tipos permitidos y tamaño máximo en `appsettings.json`.

---

## 7. Workflows y Automatización

### 7.1 Flujo de aprobación de convenios

**Propuesta**: El estado de un convenio no es libremente editable sino que sigue un workflow:

```
Borrador → PendienteAprobacion → Vigente → Finalizado
              ↓ (rechazo)
           Borrador (con comentario de rechazo)
```

- Cada transición de estado se registra en una tabla `HistorialEstados`:
  ```
  HistorialEstados { Id, ConvenioTipo, ConvenioId, EstadoAnterior, EstadoNuevo,
                     UsuarioId, Fecha, Comentario }
  ```
- Solo `Gestor` crea en Borrador. Solo `Administrador` puede aprobar y pasar a Vigente.
- El estado se cierra automáticamente (Finalizado) al cumplirse `FechaFin` (ya implementado en `BackgroundSetConvStateService`).

### 7.2 Renovación de convenios

**Propuesta**: Al finalizar un convenio, opción de "Iniciar renovación":

- Crea un nuevo convenio con los mismos datos precargados, fechas nuevas y referencia al convenio anterior (`ConvenioRenovadoId`).
- El convenio original queda como `Finalizado` con referencia al nuevo.

### 7.3 Alertas de vencimiento configurables

**Propuesta**: Agregar al endpoint de búsqueda `SearchProximosAvencer` la posibilidad de configurar los días de anticipación:

- Administrador define política global: "alertar N días antes del vencimiento".
- El job nocturno actual (`BackgroundSetConvStateService`) se amplía para emitir eventos de notificación.

---

## 8. Auditoría y Trazabilidad

### 8.1 Log de auditoría

**Propuesta**: Tabla `AuditLog` que registre todas las operaciones de escritura:

```
AuditLog { Id, UsuarioId, Accion (CREAR/ACTUALIZAR/ELIMINAR/DESCARGAR),
           EntidadTipo, EntidadId, Cambio (JSON diff), Fecha, IP }
```

- Implementación vía `SaveChangesInterceptor` de EF Core (se dispara automáticamente en cada `SaveChangesAsync()`).
- Visible solo para `Administrador` en una vista `/admin/auditoria` con filtros por fecha, usuario, entidad y acción.
- No reemplaza al `FileLogger` actual, sino que lo complementa con datos estructurados consultables.

### 8.2 Trazabilidad de descargas

**Propuesta**: Registrar en `AuditLog` las descargas de documentos (`ACCION = DESCARGAR`). Útil para saber quién accedió a qué documento y cuándo.

---

## 9. Mejoras Técnicas y Deuda Técnica

### 9.1 Paginación en base de datos

**Problema actual**: `ConveniosFilterService.ApplyPagination` trabaja sobre `IEnumerable` en memoria. Todos los resultados se cargan antes de paginar.

**Propuesta**: Mover la paginación al nivel `IQueryable` para que `Skip`/`Take` se traduzcan a SQL OFFSET/LIMIT. Esto requiere:
- Asegurar que los filtros retornen `IQueryable<T>` hasta el último paso antes de materializar.
- Evaluar si los comandos de filtro (que mezclan linq con objetos) pueden componerse como expressions.

Impacto esperado: reducción drástica de memoria en búsquedas con miles de registros.

### 9.2 Normalizar tabla `ArchivosAdjuntos`

**Problema actual**: Dos FKs nullable (`ConvenioEspecificoId`, `ConvenioMarcoId`) sin restricción de que exactamente una esté seteada.

**Propuestas**:

**Opción A (recomendada)**: Tabla de unión polimórfica:

```
ArchivosAdjuntos (PK Id)
ConvenioArchivos (PK Id, FK ArchivoId, FK ConvenioId, ConvenioTipo CHAR(1))
```

**Opción B**: Dos tablas separadas (`ArchivosConvenioMarco`, `ArchivosConvenioEspecifico`).

La opción A es más escalable si en el futuro se adjuntan archivos a otras entidades.

### 9.3 Mover seed data a archivo JSON

**Problema actual**: Datos de prueba hardcodeados en `DbSeeder.cs`.

**Propuesta**: Los datos de seed se leen de un archivo `seed-data.json` en el proyecto. Esto permite modificar datos de prueba sin recompilar.

### 9.4 Caché de listados frecuentes

**Propuesta**: Agregar caching distribuidocon `IMemoryCache` para:

- Listados de empresas (`GET /api/Empresa/all`).
- Listados de involucrados (`GET /api/Involucrados`).
- Carreras/áreas.

Invalidar caché al hacer POST/PUT sobre esas entidades.

### 9.5 Configuración externalizada

**Propuesta**: Las rutas de archivos, cadena de conexión, configuración SMTP y CORS deben leerse de `appsettings.json` + variables de entorno (`IConfiguration`), no estar hardcodeadas.

### 9.6 Logging con serilog

**Propuesta**: Reemplazar el `FileLogger` custom por **Serilog** con:

- Output a archivo en `%AppData%/SistemaConveniosUTNv3/Logs/log-{fecha}.log`.
- Rolling file sink (máximo 30 días de retención).
- Structured logging (JSON) para facilitar consultas posteriores.
- Niveles configurables (Information en producción, Debug en desarrollo).

### 9.7 Health checks

**Propuesta**: Endpoint `GET /api/health` que verifique:

- Conexión a base de datos (query rápida: `SELECT 1`).
- Acceso a la carpeta de archivos.
- Disponibilidad de SMTP (si configurado).
- Estado del background service de vencimientos.

---

## 10. Despliegue y DevOps

### 10.1 Contenerización con Docker

**Propuesta**: `Dockerfile` para el backend (`mcr.microsoft.com/dotnet/aspnet:8.0`) + `docker-compose.yml` que orqueste:

```yaml
services:
  api:
    build: ./APIconvenios
    ports:
      - "8888:8080"
    volumes:
      - ./data/db:/app/data
      - ./data/documents:/app/documents
    environment:
      - ConnectionStrings__DefaultConnection=...
      - FileStorage__BasePath=/app/documents
```

Esto elimina la dependencia del path `C:\conveniosdocuments\` y de `%AppData%` del usuario Windows.

### 10.2 Pipeline CI/CD

**Propuesta**: GitHub Actions workflow con:

1. `dotnet build` + `dotnet test`.
2. `npm run type-check` + `npm run lint` + `npm run build`.
3. `docker build` y push a registry institucional (GitHub Container Registry o Docker Hub privado).
4. Deployment automatizado al servidor de producción.

### 10.3 Estrategia de migraciones

**Propuesta**: Separar las migraciones automáticas (`dbContext.Database.Migrate()`) en un paso explícito:

- En desarrollo: `Migrate()` automático (como ahora).
- En producción: migraciones aplicadas como paso separado del deployment, o mediante `dotnet ef database update` controlado.

---

## 11. Experiencia de Usuario (UX)

### 11.1 Tema claro/oscuro

**Propuesta**: Selector de tema con persistencia en `localStorage` + `prefers-color-scheme` como valor por defecto. Usar variables CSS de Bootstrap 5 (data-bs-theme).

### 11.2 Búsqueda global rápida

**Propuesta**: En el header, un campo de búsqueda global (similar a Spotlight/cmd+K) que permita buscar empresas y convenios sin navegar a otra vista. Implementación: `Ctrl+K` abre un modal con búsqueda asíncrona (debounced 300ms).

### 11.3 Atajos de teclado

**Propuesta**: Atajos para operaciones frecuentes:

- `Ctrl+K`: Búsqueda global.
- `Ctrl+N`: Nuevo convenio.
- `Ctrl+E`: Enfoque barra de búsqueda.
- `Escape`: Cerrar panel/modal.

### 11.4 Modo tabla compacta

**Propuesta**: Alternativa al layout de cards actual: vista de tabla con columnas seleccionables (título, empresa, estado, fechas, refrendado). Ideal para usuarios que necesitan ver muchos registros de una sola mirada.

### 11.5 Paginación persistente

**Propuesta**: Guardar el tamaño de página seleccionado por el usuario en `localStorage`. Actualmente está hardcodeado.

### 11.6 Feedback visual en operaciones

**Propuesta**: Indicadores de carga skeleton (no spinners genéricos) mientras se obtienen datos, y toasts de confirmación (verde = éxito, rojo = error) después de cada operación CRUD.

---

## 12. Integraciones Potenciales

### 12.1 SIU Guaraní

**Propuesta**: Integración con SIU Guaraní para consultar carreras, alumnos y docentes activos. Evita tener que cargar manualmente esta información.

- API REST de SIU (versión 3+ tiene API).
- Sincronización periódica de carreras y personas.
- Validación de legajos de involucrados contra la base institucional.

### 12.2 Firma digital

**Propuesta**: Integración con plataforma de firma digital (ej: Firma.AR, DocuSign, o la plataforma de firma del gobierno nacional) para que los convenios puedan firmarse digitalmente dentro del sistema.

### 12.3 Sistema de Turnos / Notificados

**Propuesta**: Si un convenio requiere acción de un área externa (ej: Consejo Directivo), el sistema debe poder notificar y registrar el seguimiento de esa acción.

---

## 13. Resumen de Priorización

| Prioridad | Mejora | Impacto | Esfuerzo estimado |
|-----------|--------|---------|-------------------|
| 🔴 Alta | Autenticación y autorización | Crítico: seguridad | 2-3 semanas |
| 🔴 Alta | Paginación en DB | Alto: performance | 1 semana |
| 🔴 Alta | Ruta de archivos configurable | Medio: estabilidad | 2 días |
| 🟡 Media | Dashboard con gráficos | Alto: UX | 2-3 semanas |
| 🟡 Media | Notificaciones in-app | Alto: comunicación | 2 semanas |
| 🟡 Media | Reportes exportables | Alto: valor institucional | 2 semanas |
| 🟡 Media | Log de auditoría | Medio: trazabilidad | 1 semana |
| 🟡 Media | Migrar logging a Serilog | Medio: mantenibilidad | 3 días |
| 🟢 Baja | Tema claro/oscuro | Bajo: vanity | 2 días |
| 🟢 Baja | Atajos de teclado | Bajo: UX | 3 días |
| 🟢 Baja | Versionado de archivos | Medio: utilidad | 1 semana |
| 🟢 Baja | Integración SIU Guaraní | Alto*: depende de terceros | 4-6 semanas |
| 🟢 Baja | Docker / CI/CD | Medio: DevOps | 1 semana |

> *El esfuerzo es estimado para un desarrollador familiarizado con el código base. Depende del contexto real del equipo.

---

## 14. Glosario

| Término | Significado |
|---------|-------------|
| Claim | Par clave-valor en un token JWT que representa una afirmación sobre el usuario (ej: `role: Administrador`) |
| JWT | JSON Web Token — estándar para transmisión segura de claims entre frontend y backend |
| Policy-based auth | Esquema de autorización donde los permisos se definen como políticas reutilizables en lugar de roles fijos |
| SignalR | Biblioteca de ASP.NET Core para comunicación bidireccional en tiempo real (WebSocket) |
| Health check | Endpoint que verifica que el sistema y sus dependencias funcionan correctamente |
| Skeleton | Placeholder animado que muestra la estructura de la UI mientras los datos se cargan |
| Magic bytes | Primeros bytes de un archivo que identifican su tipo real, independientemente de la extensión |
| QuestPDF | Librería .NET para generación de PDF con API fluida (licencia MIT para ingresos < $1M USD/año) |
| ClosedXML | Librería .NET para lectura/escritura de archivos Excel .xlsx (licencia MIT) |

---

*Documento generado en julio 2026 — basado en análisis estático del código fuente y las necesidades institucionales documentadas en el informe del proyecto.*
