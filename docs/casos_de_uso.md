# Análisis de Casos de Uso — Sistema de Gestión de Convenios UTN San Francisco

---

## 1. Actores del Sistema

| Actor | Descripción |
|---|---|
| **Gestor de Convenios** | Usuario interno de la universidad (ej. personal de Secretaría de Extensión) que carga, edita, consulta y administra los convenios. Es el actor principal del sistema. |
| **Consultante** | Usuario que solo necesita buscar y visualizar información de convenios existentes (puede ser el mismo gestor u otro miembro de la facultad con acceso de lectura). |

> [!NOTE]
> El sistema **no implementa autenticación ni autorización** en los controladores analizados. No existen roles diferenciados a nivel de acceso (no hay atributos `[Authorize]`, políticas, ni claims). Por lo tanto, la distinción entre actores es **conceptual** basada en el comportamiento funcional, no en restricciones técnicas reales del código.
>
> Tampoco se detectan integraciones con sistemas externos (no hay webhooks, callbacks, ni endpoints de consumo por terceros).

---

## 2. Casos de Uso

### Flujo A — Gestión de Convenios Marco

Un **convenio marco** es un acuerdo general entre la universidad y una entidad externa (empresa, institución, organismo). Establece un paraguas de colaboración bajo el cual pueden firmarse convenios específicos. Posee un ciclo de vida con estados: **Borrador → Vigente → Finalizado**.

| # | Caso de Uso | Explicación |
|---|---|---|
| A1 | **Registrar un convenio marco** | El gestor carga un nuevo convenio marco indicando sus datos principales (título, número de convenio, fecha de firma, fecha de fin, estado, número de resolución, si fue refrendado, y un comentario opcional). Opcionalmente puede **asociar una empresa** al momento de la carga, y también **vincular un convenio específico ya existente** indicando su número de convenio. |
| A2 | **Consultar el detalle completo de un convenio marco** | El consultante accede a toda la información de un convenio marco: sus datos generales, la empresa asociada (si existe), la lista de convenios específicos que agrupa, y los archivos adjuntos vinculados. |
| A3 | **Editar un convenio marco** | El gestor modifica los datos de un convenio marco existente. En la misma operación puede: asociar una nueva empresa, vincular convenios específicos adicionales por su número, desvincular convenios específicos previamente asociados, o desvincular la empresa actual. |
| A4 | **Eliminar un convenio marco** | El gestor elimina un convenio marco del sistema. |
| A5 | **Desvincular empresa de un convenio marco** | El gestor rompe la relación entre un convenio marco y la empresa que tenía asociada, sin eliminar ni el convenio ni la empresa. |
| A6 | **Desvincular un convenio específico de un convenio marco** | El gestor quita la relación jerárquica entre un convenio marco y uno de los convenios específicos que tenía agrupados. El convenio específico sigue existiendo pero queda sin convenio marco padre. |
| A7 | **Consultar los archivos adjuntos de un convenio marco** | El consultante lista todos los documentos asociados a un determinado convenio marco. |
| A8 | **Listar todos los convenios marco** | El consultante obtiene un listado resumido de todos los convenios marco registrados en el sistema. |

> [!IMPORTANT]
> **Relación «include»**: El caso **A1 (Registrar)** y **A3 (Editar)** incluyen implícitamente la capacidad de **vincular/desvincular empresas** y **vincular/desvincular convenios específicos**. Estos no son operaciones aisladas sino parte del flujo de carga y edición.

---

### Flujo B — Gestión de Convenios Específicos

Un **convenio específico** es un acuerdo concreto de colaboración (pasantías, proyectos, actividades académicas, etc.). Puede estar vinculado a un convenio marco, a una empresa, a involucrados (personas) y a carreras de la universidad. También posee el ciclo de vida **Borrador → Vigente → Finalizado** y puede marcarse como **acta** o como **refrendado**.

| # | Caso de Uso | Explicación |
|---|---|---|
| B1 | **Registrar un convenio específico** | El gestor carga un nuevo convenio específico con sus datos (título, número, fechas de firma/inicio/fin, estado, si es acta, número de resolución, si fue refrendado, comentario). En la misma operación puede: asociar una **empresa** (nueva o existente), vincular **involucrados nuevos** (creándolos), vincular **involucrados ya existentes** por sus IDs, asociar **carreras** de la universidad, y vincularlo a un **convenio marco**. |
| B2 | **Consultar el detalle completo de un convenio específico** | El consultante accede a toda la información del convenio: datos generales, empresa asociada, convenio marco padre (si aplica), lista de involucrados con sus datos de contacto y rol, carreras involucradas, y archivos adjuntos. |
| B3 | **Editar un convenio específico** | El gestor modifica los datos de un convenio específico. En la misma operación puede: agregar o quitar involucrados, cambiar las carreras asociadas, vincular o desvincular la empresa, vincular o desvincular el convenio marco padre. |
| B4 | **Eliminar un convenio específico** | El gestor elimina un convenio específico del sistema. |
| B5 | **Desvincular empresa de un convenio específico** | El gestor rompe la asociación entre un convenio específico y su empresa, sin eliminar ninguno de los dos. |
| B6 | **Desvincular convenio marco de un convenio específico** | El gestor quita la referencia al convenio marco padre de un convenio específico. El convenio específico pasa a no tener convenio marco asociado. |
| B7 | **Consultar los archivos adjuntos de un convenio específico** | El consultante lista los documentos asociados a un convenio específico determinado. |
| B8 | **Listar todos los convenios específicos** | El consultante obtiene un listado resumido de todos los convenios específicos existentes. |

> [!IMPORTANT]
> **Relación «include»**: **B1 (Registrar)** incluye la posibilidad de crear involucrados nuevos al vuelo y vincular involucrados existentes. Es un caso de uso compuesto que orquesta la carga del convenio con todas sus relaciones en una sola acción.

---

### Flujo C — Gestión Documental

Los documentos (archivos adjuntos) se asocian a convenios marco o específicos y representan la documentación de respaldo (resoluciones, actas, acuerdos firmados, etc.).

| # | Caso de Uso | Explicación |
|---|---|---|
| C1 | **Adjuntar un documento a un convenio** | El gestor sube un archivo (PDF, imagen, etc.) y lo asocia a un convenio marco o a un convenio específico, indicando un nombre descriptivo para el archivo. |
| C2 | **Descargar un documento adjunto** | El consultante descarga un archivo previamente adjuntado a un convenio, recibiendo el archivo con su nombre y tipo original. |
| C3 | **Eliminar un documento adjunto** | El gestor elimina un archivo adjunto del sistema. |

> [!NOTE]
> La subida de archivos se realiza de forma independiente a la carga del convenio (es una operación separada posterior). No se sube documentación dentro del flujo de registro del convenio; primero se crea el convenio y luego se le adjuntan documentos.

---

### Flujo D — Gestión de Empresas / Entidades Externas

Las empresas (o instituciones/organismos) son las contrapartes externas con las que la universidad firma convenios.

| # | Caso de Uso | Explicación |
|---|---|---|
| D1 | **Listar empresas disponibles** | El gestor o consultante obtiene la lista de todas las empresas registradas en el sistema (nombre e identificador) para seleccionar una al momento de crear o editar convenios. |
| D2 | **Editar información de una empresa** | El gestor actualiza los datos de una empresa existente (nombre, razón social, CUIT, dirección, teléfono, email). |

> [!WARNING]
> **Lógica incompleta detectada:**
> - No existe un caso de uso para **crear una empresa de forma independiente**. Las empresas se crean únicamente de forma implícita al registrar o editar un convenio (marco o específico) y enviar los datos de una empresa nueva en el mismo request.
> - No existe un caso de uso para **eliminar una empresa**.
> - No existe un endpoint para **consultar el detalle de una empresa** de forma individual.
> - El endpoint de edición de empresa **no devuelve confirmación de éxito con los datos actualizados** ni valida si la empresa existe previamente (no hay manejo de errores visible).

---

### Flujo E — Gestión de Personas Involucradas

Los involucrados son las personas (docentes, alumnos, secretarios, externos) que participan en los convenios específicos.

| # | Caso de Uso | Explicación |
|---|---|---|
| E1 | **Validar si un involucrado ya existe** | Antes de agregar una persona a un convenio, el gestor verifica (por nombre y apellido) si esa persona ya está registrada en el sistema, para evitar duplicados. |
| E2 | **Listar todos los involucrados** | El gestor obtiene la lista completa de personas involucradas registradas (nombre completo e identificador), para seleccionar participantes al cargar o editar un convenio. |
| E3 | **Listar involucrados disponibles para un convenio** | El gestor consulta qué personas **no están ya asociadas** a un determinado convenio, para poder agregarlas. Esto evita que se intente vincular una persona que ya participa. |
| E4 | **Consultar involucrados por carrera** | El consultante obtiene la lista detallada de personas involucradas que pertenecen a una determinada carrera/área, incluyendo su información de contacto, legajo y rol. También se informa la cantidad total. |

> [!WARNING]
> **Lógica incompleta detectada:**
> - No existe un caso de uso para **crear un involucrado de forma independiente**. Los involucrados se crean únicamente al registrar o editar un convenio específico.
> - No existe un caso de uso para **editar los datos de un involucrado** (email, teléfono, legajo, etc.).
> - No existe un caso de uso para **eliminar un involucrado** del sistema.
> - La validación de duplicados (E1) se basa solo en nombre y apellido, lo cual puede generar **falsos positivos** (personas distintas con el mismo nombre).

---

### Flujo F — Búsqueda y Consulta de Convenios (Unificada)

Este flujo permite búsquedas avanzadas sobre el universo completo de convenios (marcos y específicos combinados), con múltiples criterios de filtrado y paginación.

| # | Caso de Uso | Explicación |
|---|---|---|
| F1 | **Buscar convenios con filtros múltiples** | El consultante realiza una búsqueda combinando cualquiera de los siguientes criterios: por título, por número de convenio, por número de resolución, por empresa, por estado (borrador/vigente/finalizado), por carrera involucrada, por fecha de firma, por fecha de fin, por si es acta, por si está refrendado, por antigüedad, por próximos a vencer, por mes, por año, y por rango de fechas. Los resultados se entregan **paginados**. |
| F2 | **Consultar convenios de una empresa** | El consultante obtiene todos los convenios (marcos y/o específicos) asociados a una empresa determinada. |
| F3 | **Buscar un convenio por su número** | Tanto para convenios marco como específicos, el gestor puede localizar un convenio por su número identificatorio, obteniendo su ID interno. Esto se usa como paso intermedio para vincular convenios entre sí. |
| F4 | **Obtener estadísticas de convenios firmados por mes** | El consultante solicita un conteo de convenios firmados agrupados por mes, útil para reportes y dashboards. |
| F5 | **Obtener estadísticas de convenios firmados por rango de fechas** | El consultante solicita un conteo de convenios firmados dentro de un rango de fechas específico. |

> [!NOTE]
> Los casos F4 y F5 se deducen de la existencia de filtros `CountConvFirmadosByMesDto` y `CountConveniosFirmadosByRangoDto` en el objeto de consulta. Comparten el mismo endpoint de búsqueda (F1) pero su intención funcional es obtener **datos agregados/estadísticos**, no listados de convenios individuales.

---

## 3. Resumen de Relaciones entre Casos de Uso

| Relación | Descripción |
|---|---|
| **A1 → include → D (crear empresa implícita)** | Al registrar un convenio marco, se puede crear una empresa nueva como parte de la misma operación. |
| **B1 → include → D (crear empresa implícita)** | Al registrar un convenio específico, se puede crear una empresa nueva como parte de la misma operación. |
| **B1 → include → E (crear involucrados)** | Al registrar un convenio específico, se pueden crear involucrados nuevos como parte de la misma operación. |
| **A3 → include → A5, A6** | La edición de un convenio marco puede incluir la desvinculación de empresa y/o de convenios específicos. |
| **B3 → include → B5, B6** | La edición de un convenio específico puede incluir la desvinculación de empresa y/o del convenio marco. |
| **E1 ← precede ← B1** | Antes de registrar un convenio específico con involucrados nuevos, se puede validar si ya existen para evitar duplicados. |
| **F3 ← precede ← A1, A3** | Para vincular un convenio específico a un marco (o viceversa), primero se busca por número para obtener su identificador. |

---

## 4. Observaciones Generales

> [!CAUTION]
> **Sin autenticación ni autorización**: El sistema no protege ningún endpoint. Cualquier consumidor de la API puede crear, editar o eliminar convenios. Esto es un riesgo importante si la API está expuesta fuera de una red interna.

> [!WARNING]
> **Gestión incompleta de entidades secundarias**: Las empresas e involucrados no tienen ciclo de vida propio completo (no se pueden crear, editar ni eliminar de forma independiente en todos los casos). Esto puede generar datos huérfanos o inconsistencias a futuro.

> [!NOTE]
> **No se detectaron flujos de aprobación o workflow**: No hay lógica de transición de estados (ej. pasar un convenio de "Borrador" a "Vigente" con validaciones o aprobaciones). El cambio de estado parece ser un simple campo editable sin reglas de negocio asociadas.
