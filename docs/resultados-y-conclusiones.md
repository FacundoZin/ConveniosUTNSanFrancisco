# Resultados y Conclusiones

> Documento de cierre del proyecto Sistema de Gestión de Convenios — Secretaría de Vinculación Tecnológica, UTN San Francisco.

---

## 1. Logros alcanzados

### 1.1 Sistema completamente funcional

Se desarrolló una aplicación completa con arquitectura de dos capas (backend API REST en .NET 8 + frontend SPA en Vue 3 con TypeScript) que cubre la gestión integral de convenios. El backend expone **32 endpoints REST** organizados en 6 controladores, con 8 servicios de negocio y 8 repositorios que implementan el patrón Unit of Work con separación de lectura y escritura. El frontend cuenta con **11 vistas** y **28 componentes** organizados en 4 módulos funcionales (convenios, empresas, involucrados y componentes compartidos).

### 1.2 Centralización de la información

Se eliminó la dependencia de planillas Excel dispersas al contar con una base de datos SQLite normalizada con 6 entidades principales y sus relaciones: Convenios Marco, Convenios Específicos, Empresas, Involucrados, Carreras/Áreas y Archivos Adjuntos. Toda la información reside en un único lugar, accesible desde cualquier equipo de la red local.

### 1.3 Búsqueda y filtrado avanzado

El sistema implementa **15 criterios de búsqueda** diferentes (título, empresa, estado, número de resolución, número de convenio, áreas, fechas, antigüedad, mes, año, rango, próximos a vencer, refrendados, actas) más 2 reportes de conteo (por mes y por rango). Los resultados se presentan combinando convenios marco y específicos con paginación integrada.

### 1.4 Gestión documental

Se implementó un módulo de carga, descarga y eliminación de archivos adjuntos con transacciones atómicas que mantienen sincronizado el sistema de archivos con la base de datos. Los documentos se asocian tanto a convenios marco como a convenios específicos.

### 1.5 Automatización de estados

Un servicio background ejecutado diariamente actualiza automáticamente el estado de los convenios vencidos a `Finalizado`, sin intervención manual. El servicio es compatible con Windows Services para ejecución en segundo plano.

### 1.6 Aplicación de escritorio

Además de la interfaz web, el sistema puede empaquetarse como aplicación de escritorio multiplataforma mediante Electron, ofreciendo una experiencia de usuario más integrada.

### 1.7 Patrones de diseño aplicados

Se emplearon patrones como Unit of Work, Repository (con separación read/write), Command Pattern para operaciones complejas, Result Pattern para manejo de errores, y servicios background con `IHostedService`. Esto sienta una base sólida para el mantenimiento y la evolución futura del código.

---

## 2. Dificultades enfrentadas

### 2.1 Deuda técnica por naming inconsistente

Durante el desarrollo se introdujeron errores tipográficos que se propagaron a través del código base. El más significativo es `ConvenioEspecifco` (falta la letra "i") en el nombre de la interfaz `IConvenioEspecifcoService` y su implementación, que contamina 4 archivos clave con 15 referencias. También se encontraron `GetAllInvolucraods()` (falta la "d"), `UpdateConvenioMarcoRequetsDto` (falta la "q"), y la propiedad `numeroconvenio` en camelCase cuando la convención de .NET exige PascalCase, lo que además se expone en la API REST con minúscula inicial. Estas inconsistencias no afectan el funcionamiento pero perjudican la legibilidad y el mantenimiento del código.

### 2.2 Valores hardcodeados

Varios parámetros de configuración quedaron fijos en el código fuente en lugar de estar externalizados en archivos de configuración: la URL base de la API (`http://localhost:8888/api`), la ruta de almacenamiento de documentos (`C:\conveniosdocuments\`), y la ubicación de la base de datos (`%AppData%`). Esto dificulta el despliegue en diferentes entornos sin modificar el código.

### 2.3 Errores confirmados en producción

Se identificaron dos bugs concretos: una asignación incorrecta en `EmpresaRepository` que persiste el teléfono en el campo CUIT (`empresa.Cuit = dto.Telefono`), y una consulta en `ConvenioEspecificoReadRepository` que apunta a la tabla de Convenios Marcos en lugar de Convenios Específicos. Ambos errores son puntuales y corregibles, pero estuvieron presentes en el código durante el desarrollo.

### 2.4 Paginación ineficiente

El filtro de convenios aplica la paginación en memoria (`IEnumerable`) en lugar de hacerlo en la base de datos (`IQueryable`). Esto significa que todos los registros se cargan desde SQLite antes de seleccionar la página solicitada. Con el volumen actual de datos no es perceptible, pero a medida que crezca la base de convenios se volverá un cuello de botella.

### 2.5 Ausencia de autenticación

El sistema no implementa ningún mecanismo de autenticación ni autorización. La política CORS permite cualquier origen, y la llamada a `UseAuthorization()` en `Program.cs` es una declaración sin efecto al no estar registrado ningún servicio de autenticación. Esto limita seriamente la posibilidad de un despliegue institucional sin antes abordar esta carencia.

### 2.6 Sin tests automatizados

No existen proyectos de test en el repositorio. Toda la verificación se realizó de forma manual, lo que incrementa el riesgo de regressiones al introducir cambios y dificulta la incorporación de nuevos desarrolladores al proyecto.

---

## 3. Satisfacción de requerimientos

### 3.1 Cobertura funcional

De los **27 casos de uso** planificados y documentados en el análisis inicial, los **27 se implementaron completamente**, distribuidos en 5 módulos funcionales:

| Módulo | Casos de uso planificados | Implementados | Cobertura |
|--------|:-------------------------:|:-------------:|:---------:|
| Panel de Búsqueda | 6 | 6 | 100 % |
| Empresas | 4 | 4 | 100 % |
| Involucrados | 3 | 3 | 100 % |
| Convenios Marcos | 7 | 7 | 100 % |
| Convenios Específicos | 7 | 7 | 100 % |

### 3.2 Objetivos generales cumplidos

| Objetivo | Estado | Evidencia |
|----------|--------|-----------|
| Centralizar la información en una única plataforma | ✅ Logrado | 6 entidades integradas con relaciones normalizadas |
| Mejorar la trazabilidad y el seguimiento de convenios | ✅ Logrado | Estados (Borrador/Vigente/Finalizado) con actualización automática |
| Reducir la carga administrativa y errores humanos | ✅ Logrado | Interface visual que reemplaza planillas Excel |
| Sentar bases sólidas para crecimiento futuro | ✅ Logrado | Arquitectura en capas con patrones de diseño establecidos |

### 3.3 Brechas respecto al objetivo ideal

El objetivo ideal de que el sistema sea adoptado por toda la facultad con múltiples secretarías gestionando sus convenios de forma independiente no se alcanzó en esta versión. Las limitaciones principales son:

- **Sin autenticación ni roles**: el sistema opera con un único usuario físico sin restricciones.
- **Sin aislamiento de datos**: no existe una entidad que agrupe convenios por secretaría ni lógica que restrinja el acceso según pertenencia.
- **Sin tests**: la ausencia de pruebas automatizadas dificulta escalar el desarrollo.

Estas brechas eran conocidas desde el inicio y se documentaron como límites del sistema en el análisis de requisitos.

---

## 4. Impacto potencial del software desarrollado

### 4.1 Transformación del flujo de trabajo

El sistema reemplaza un proceso basado en planillas Excel almacenadas en una única máquina — frágil, sin respaldo automático y de consulta presencial — por una aplicación web accesible desde cualquier equipo de la red. El impacto inmediato es la eliminación de los siguientes problemas:

- **Pérdida de información**: las planillas ya no dependen de un solo disco ni de copias manuales en pendrive.
- **Información desactualizada**: al centralizar los datos, cualquier modificación se refleja al instante para todos los usuarios.
- **Dificultad para encontrar información**: los 15 filtros de búsqueda permiten localizar un convenio en segundos por cualquier criterio relevante.
- **Tareas manuales repetitivas**: la actualización automática de estados libera a la secretaría de una tarea administrativa periódica.

### 4.2 Beneficios cuantitativos proyectados

| Aspecto | Situación anterior | Situación con el sistema |
|---------|-------------------|-------------------------|
| Tiempo para localizar un convenio | Minutos (búsqueda manual en Excel) | Segundos (búsqueda por 15 criterios) |
| Riesgo de pérdida de datos | Alto (única máquina, sin backup automático) | Bajo (base de datos centralizada + backup programable) |
| Actualización de estados vencidos | Manual (revisión periódica de fechas) | Automática (job diario nocturno) |
| Acceso a la información | Presencial en la Secretaría | Desde cualquier equipo de la red |
| Consistencia de datos | Múltiples versiones de planillas que no coinciden | Una sola fuente de verdad |

### 4.3 Base para crecimiento institucional

Más allá de su uso inmediato en la Secretaría de Vinculación Tecnológica, el sistema fue diseñado con una arquitectura que permite su evolución hacia un despliegue institucional más amplio. Los documentos `docs/mejoras-propuestas.md` y `docs/escalabilidad-propuesta.md` detallan los pasos necesarios para:

- Agregar autenticación y roles de usuario (requisito para multi-usuario).
- Aislar datos por secretaría o unidad organizativa.
- Incorporar reportes exportables, dashboard con gráficos y notificaciones.
- Mejorar la performance con índices, caché y paginación en base de datos.
- Contenerizar la aplicación para simplificar el despliegue.

### 4.4 Limitaciones actuales que condicionan el impacto

El impacto potencial no se realiza completamente hasta que se resuelvan las limitaciones de seguridad identificadas. Un sistema institucional sin autenticación no puede ponerse en producción en una red abierta sin riesgos. La prioridad cero para cualquier paso siguiente es implementar la capa de autenticación y autorización antes de cualquier otra mejora funcional.

---

*Documento generado en julio 2026 como parte de la documentación de cierre del proyecto Sistema de Gestión de Convenios — UTN San Francisco.*
