# Escalabilidad del Sistema — Propuesta para Crecimiento Institucional

> Documento enfocado en la escalabilidad realista del sistema para su adopción
> dentro de UTN San Francisco. No se plantea una arquitectura multi-cliente ni
> distribuida, sino mejoras concretas para que el sistema soporte el crecimiento
> de usuarios, convenios y años de operación sin reescrituras mayores.

---

## 1. Escenario de crecimiento esperado

| Métrica | Hoy (estimado) | 3 años | 5 años |
|---------|---------------|--------|--------|
| Convenios marco | ~50 | ~200 | ~400 |
| Convenios específicos | ~200 | ~800 | ~1.500 |
| Empresas | ~40 | ~150 | ~300 |
| Involucrados | ~100 | ~400 | ~800 |
| Usuarios del sistema | 1 (físico) | 5-10 | 10-20 |
| Archivos adjuntos | ~200 | ~800 | ~2.000 |
| Operaciones por día | ~10 | ~50 | ~100 |

SQLite soporta sin problemas estos volúmenes. **No hay necesidad de migrar a SQL Server
ni PostgreSQL**. La base de datos actual está en `%AppData%`, lo cual sí es un problema
de portabilidad y administración que se aborda más abajo.

---

## 2. El cuello de botella real (y cómo resolverlo ya)

### 2.1 Paginación en memoria → en base de datos

**Problema**: `ConveniosFilterService.ApplyPagination` aplica `Skip().Take()` sobre una
colección en memoria (`IEnumerable`). Esto significa que si hay 800 convenios específicos,
la API los TRAE TODOS desde SQLite, los materializa en objetos C#, y recién ahí
selecciona los 10 de la página.

Con 200 registros es imperceptible. Con 1500 empieza a notarse. Con 5000 archivos
adjuntos cargados en el mismo convenio, la vista de detalle se vuelve lenta.

**Solución**: Llevar el `IQueryable<T>` hasta el final del pipeline de filtros y aplicar
`Skip`/`Take` recién antes de materializar con `ToListAsync()`. La diferencia está en
que `IQueryable.Skip().Take()` se traduce a `LIMIT ? OFFSET ?` en SQL, y la base de
datos solo devuelve los registros necesarios.

```
// Mal (hoy):
var results = query.ToList();       // Trae todo a memoria
return results.Skip(10).Take(10);   // Filtra en C#

// Bien:
return await query                  // Sigue siendo IQueryable
    .Skip(10)
    .Take(10)
    .ToListAsync();                 // Solo 10 registros viajan por cable
```

**Esfuerzo**: ~1 semana. **Impacto**: el más alto de esta lista.

### 2.2 Caché de listados estáticos o semi-estáticos

**Problema**: Cada vez que alguien abre un formulario de carga/edición de convenio, el
frontend pide `GET /api/Empresa/all` y `GET /api/Involucrados` y `GET /api/Areas`.
Estos listados cambian con poca frecuencia (se agrega una empresa cada semanas), pero
se consultan constantemente.

**Solución**: Cachear estos endpoints con `IMemoryCache` con expiración deslizante de
5 minutos. Cuando se crea o edita una empresa/involucrado/área, se invalida la entrada
de caché correspondiente.

```csharp
// En Program.cs
builder.Services.AddMemoryCache()

// En el service
public async Task<List<Empresa>> GetAllAsync()
{
    return await _cache.GetOrCreateAsync("empresas_all", async entry =>
    {
        entry.SlidingExpiration = TimeSpan.FromMinutes(5);
        return await _repository.GetAll().ToListAsync();
    });
}
```

**Esfuerzo**: 1 día. **Impacto**: reduce latencia en formularios y carga del servidor.

### 2.3 Índices de base de datos faltantes

**Problema**: No se revisaron los índices de las tablas. SQLite crea índice automático
en PKs y FKs declaradas en Fluent API, pero no en columnas que se usan frecuentemente
en filtros.

**Índices recomendados** (se agregan en `OnModelCreating`):

```csharp
entity.HasIndex(c => c.numeroconvenio);        // Búsqueda por número
entity.HasIndex(c => c.Estado);                 // Filtro por estado
entity.HasIndex(c => c.FechaFirmaConvenio);     // Orden y rango de fechas
entity.HasIndex(c => c.FechaFin);               // Búsqueda de próximos a vencer
entity.HasIndex(c => c.Refrendado);             // Filtro booleano

entity.HasIndex(c => c.Nombre);                  // Búsqueda por empresa
entity.HasIndex(c => c.Cuit);                     // Búsqueda exacta

entity.HasIndex(i => i.Apellido);                // Búsqueda por apellido
```

SQLite los usa en consultas `WHERE` y `ORDER BY`. No ralentizan las escrituras de
forma perceptible para el volumen del sistema.

**Esfuerzo**: Medio día. **Impacto**: las búsquedas no empeoran a medida que crecen los datos.

---

## 3. Escalamiento horizontal mínimo (lo justo y necesario)

No se necesita un cluster ni balanceador de carga. Pero sí conviene separar
responsabilidades en procesos distintos para no bloquear la UI del usuario:

### 3.1 Separar el job de vencimientos del proceso principal

**Hoy**: `BackgroundSetConvStateService` corre dentro del mismo proceso que la API.
Si el job tuviera que procesar 500 convencios y enviar emails, podría demorar la
respuesta de la API mientras corre (aunque sea en segundo plano).

**Propuesta**: Mover el job a un proyecto separado `APIconvenios.Worker` como
`BackgroundService` independiente. Se ejecuta como una consola o servicio de Windows
separado. Se comunican solo por base de datos compartida.

```
Un solo proceso suficiente:
┌──────────────────────────────────┐
│  dotnet APIconvenios.dll         │
│  ├── Web API (endpoints HTTP)   │
│  └── BackgroundService           │
│      (estados + notificaciones)  │
└──────────────────────────────────┘
         │
         ▼
┌──────────────────┐
│  SQLite (DB)     │
│  Archivos (FS)   │
└──────────────────┘
```

Para el volumen del sistema, un solo proceso es más que suficiente. Solo separar si
se agregan tareas pesadas como generación de reportes PDF masivos o envío de emails.

### 3.2 Base de datos en ruta configurable y respaldable

**Hoy**: `%AppData%/SistemaConveniosUTNv3/SistemaConveniosUTN.db`. Esto es problemático
para escalar porque:
- Está atada al perfil del usuario que ejecuta la API.
- No se puede apuntar a una ruta de red compartida.
- Las copias de seguridad requieren acceso al perfil de usuario.

**Propuesta**: La cadena de conexión se lee de `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=C:\\SistemaConvenios\\database.db"
  }
}
```

Esto permite:
- Una ubicación predecible para backups automáticos.
- Posibilidad de migrar a SQL Server sin cambiar código si algún día se necesita (EF
  Core abstrae el proveedor).
- Ejecutar la API como servicio de Windows con una cuenta de servicio dedicada.

Además, agregar un script de backup básico:

```bash
# backup.ps1 — programable como tarea de Windows
$date = Get-Date -Format "yyyy-MM-dd"
Copy-Item "C:\SistemaConvenios\database.db" "D:\Backups\convenios-$date.db"
```

**Esfuerzo**: 1 día. **Impacto**: elimina el riesgo de pérdida de datos por perfil de usuario.

---

## 4. Escalamiento de usuarios concurrentes

### 4.1 Pool de conexiones SQLite

**Hoy**: `Program.cs` configura `UseSqlite()` con la cadena por defecto. SQLite no
maneja bien escrituras concurrentes (bloquea a nivel de archivo).

**Propuesta**: En la cadena de conexión, agregar `Cache=Shared` y usar el modo WAL
(Write-Ahead Logging) que permite lecturas concurrentes mientras se escribe:

```
Data Source=...;Cache=Shared
```

Y en el `DbContext`:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseSqlite(connectionString, o =>
        o.CommandTimeout(30));
}

// En Program.cs, después de BuildServiceProvider:
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
```

WAL permite que hasta **lectores simultáneos**: mientras un hilo escribe, otros
leen sin bloqueo. Para 10-20 usuarios concurrentes es más que suficiente.

### 4.2 Transacciones cortas

**Hoy**: Los comandos y services llaman `await _UnitOfWork.Save()` una sola vez al
final, lo cual está bien. Pero hay operaciones que dentro del mismo request hacen
varias lecturas antes de escribir. Conviene verificar que ninguna lectura quede
dentro de una transacción explícita.

Regla: las transacciones solo envuelven escrituras, nunca lecturas.

### 4.3 Archivos fuera de la base de datos

**Hoy**: Los archivos se guardan en disco y solo la metadata va a DB. Esto es
correcto y no debe cambiar. Si en el futuro los archivos crecen mucho (~100 GB),
se puede apuntar `FileStorage:BasePath` a un NAS o unidad de red compartida sin
cambiar código.

---

## 5. Escalamiento de funcionalidad (módulos futuros)

### 5.1 Arquitectura de módulos desacoplados

**Hoy**: Todo el backend está en un solo proyecto `APIconvenios` con carpetas por
capa (Controllers, Services, Repositorio). No hay separación por módulo.

**Propuesta simple**: Sin llegar a microservicios, organizar el proyecto actual en
carpetas que reflejen módulos funcionales autocontenidos:

```
APIconvenios/
├── Modules/
│   ├── Convenios/           # ConveniosMarco + ConveniosEspecifico
│   │   ├── Controllers/
│   │   ├── Services/
│   │   ├── Repositories/
│   │   └── DTOs/
│   ├── Empresas/
│   │   ├── Controllers/
│   │   ├── Services/
│   │   └── ...
│   ├── Involucrados/
│   ├── Documentos/
│   └── Auth/
├── Shared/                   # Cosas compartidas
│   ├── Interfaces/
│   ├── Models/
│   └── Helpers/
```

No es una reescritura — es una reorganización de archivos existentes que mejora la
navegación y hace obvia la frontera entre módulos. Cuando alguien nuevo toca el
código, sabe exactamente dónde está cada cosa.

**Esfuerzo**: 2-3 días (solo mover archivos y ajustar namespaces).

### 5.2 Separación de responsabilidades en el frontend

**Hoy**: El frontend ya está modularizado por `modules/` (convenios, empresas,
involucrados, shared). Eso está bien.

**Propuesta**: Agregar lazy-loading de rutas por módulo para que el bundle inicial
no incluya todo:

```typescript
// router.ts (hoy)
const routes = [
  { path: '/empresas', component: () => import('@/modules/empresas/views/DashboardEmpresas.vue') }
]
```

Vite ya hace code splitting automático con `() => import()`. Solo hay que asegurarse
de que las rutas estén lazy. Esto no da beneficio hoy (pocos KB), pero sí cuando se
agreguen más módulos.

---

## 6. Lo que NO hay que hacer (para este contexto)

| Lo que NO se necesita | Por qué |
|-----------------------|---------|
| Migrar a SQL Server o PostgreSQL | SQLite + WAL soporta millones de filas y decenas de usuarios concurrentes. Para el volumen institucional es óptimo. |
| Microservicios | Agregan latencia de red, complejidad de deployment, necesidad de orquestación. Un monolito bien modularizado es más simple y más rápido de desarrollar. |
| Caché distribuida (Redis) | `IMemoryCache` es suficiente para el volumen de datos. Redis solo agrega un servicio más que administrar. |
| Balanceador de carga | Con 10-20 usuarios concurrentes no hay necesidad. Si se necesita en el futuro, solo dockerizar y poner un nginx adelante. |
| Base de datos separada por módulo | Complica joins y consistencia. Una sola BD con esquema bien definido alcanza. |
| Cola de mensajes (RabbitMQ) | El único job async es el de vencimientos diario. No hay eventos que encolar. |

---

## 7. Plan de acción recomendado (priorizado)

| # | Qué hacer | Tiempo | Depende de |
|---|-----------|--------|-----------|
| 1 | Paginación en `IQueryable` en vez de `IEnumerable` | 1 semana | Nada |
| 2 | Índices faltantes en migración | ½ día | Nada |
| 3 | Cadena de conexión configurable + backup automático | 1 día | Nada |
| 4 | Caché con `IMemoryCache` para listados estáticos | 1 día | Nada |
| 5 | Modo WAL para SQLite | ½ día | Nada |
| 6 | Reorganizar backend en `Modules/` | 2-3 días | Nada |
| 7 | Lazy loading de rutas frontend | ½ día | Nada |

Ninguna de estas tareas requiere cambios en la UI ni en los endpoints que consumen
los clientes. Son cambios internos que el usuario final no nota hasta que los datos
crecen y el sistema sigue respondiendo igual de rápido.

---

## 8. Conclusión

La arquitectura actual está bien plantada. Los problemas de escalabilidad reales
—no los teóricos— se resuelven con cambios pequeños y localizados:

1. **Hacer que la paginación ocurra en SQL** (el mayor impacto).
2. **Ponerle índices a las columnas que se filtran** (para que no empeore con los años).
3. **Agregar caché a las consultas repetitivas** (para mantener la fluidez en formularios).
4. **Externalizar la configuración** (para que no dependa del perfil de Windows).

Con estas cuatro cosas, el sistema aguanta los 5 años del escenario planteado sin
necesidad de reescrituras ni cambios arquitectónicos mayores. Cuando se acerque el
año 5, se evalúa si vale la pena migrar la BD a SQL Server (por facilidad de backups
y herramientas de administración, no por performance), pero hasta entonces SQLite
sobra.
