# AGENTS.md

## Project overview

Monorepo for a convention management system (UTN San Francisco). Two modules:

- **`APIconvenios/`** — .NET 8 ASP.NET Core Web API (SQLite + EF Core)
- **`UserInterface/`** — Vue 3 + TypeScript SPA (Vite, Pinia, Bootstrap 5), also packaged as Electron desktop app

No test projects exist in this repository.

## Commands

### Backend (from repo root)

```bash
dotnet run --project APIconvenios              # start API on http://localhost:8888
dotnet run --project APIconvenios -- --seed    # populate DB with test data, then exit
```

### Frontend (from `UserInterface/`)

```bash
npm install
npm run dev            # Vite dev server (web mode)
npm run build          # type-check + production build
npm run type-check     # vue-tsc --build (no emit)
npm run lint           # eslint . --fix
npm run format         # prettier --write src/
npm run electron:serve # run as Electron desktop app
```

### Verification order

`npm run type-check` → `npm run lint` (frontend). Backend has no lint/test — just build: `dotnet build APIconvenios`.

## Architecture notes

### Backend (`APIconvenios/`)

Layered architecture: **Controllers → Services → Repositories** (coordinated by `_UnitOfWork`).

| Directory | Role |
|---|---|
| `Controllers/` | API endpoints (6 controllers) |
| `Services/` | Business logic |
| `Repositorio/` | Data access (read/write split: e.g. `ConvenioEspecificoRepository` vs `ConvenioEspecificoReadRepository`) |
| `Interfaces/Repositorio/` | Repository contracts |
| `Interfaces/Servicios/` | Service contracts |
| `DTOs/` | Request/response shapes, grouped by domain |
| `Models/` | EF Core entities |
| `Data/` | `ApplicationDbContext` + `DbSeeder` |
| `Commands/` | Command objects for create/update operations |
| `Helpers/` | JSON converters, file logger, mappers, query helpers, validators |
| `Middlewares/` | `GlobalExceptionHandler` |
| `Background/` | `BackgroundSetConvStateService` — hosted service for auto-updating convention states |
| `UnitOfWork/` | `_UnitOfWork` — coordinates repositories in a single scope |

Key facts:
- **DB location**: `%AppData%/SistemaConveniosUTNv3/SistemaConveniosUTN.db` (not in project dir)
- **Auto-migration**: `dbContext.Database.Migrate()` runs on every startup — no manual `dotnet ef` commands needed for deployment
- **Logging**: Custom `FileLogger` writing to `%AppData%/SistemaConveniosUTNv3/Logs/` (not Serilog/NLog)
- **Windows Service**: configured via `UseWindowsService` — can run as a background Windows service
- **Swagger**: available at `/swagger` in Development mode only
- **Domain language**: Spanish (ConvenioMarco, ConvenioEspecifico, Empresa, Involucrados, Carreras)
- **Nested solution**: `APIconvenios/SolutionConvenios/SolutionConvenios.sln` exists but is vestigial — use root `APIconvenios.sln`

### Frontend (`UserInterface/`)

Vue 3 SPA organized by **domain modules** under `src/modules/`:

- `convenios/` — convention CRUD (marco + especifico)
- `empresas/` — company management
- `involucrados/` — involved people management
- `shared/` — shared components and services

Each module follows: `components/`, `composables/`, `services/`, `stores/`, `views/`.

Key facts:
- **Routing**: Hash-based (`createWebHashHistory`) — URLs use `/#/` prefix
- **API base URL**: hardcoded in `src/Services/apiBaseService.ts` as `http://localhost:8888/api`
- **Path alias**: `@` → `./src` (configured in `vite.config.ts`)
- **State management**: Pinia (stores per module)
- **UI framework**: Bootstrap 5 + Bootstrap Icons (no component library like Vuetify)
- **Electron**: `electron/main.js` loads the built `dist/` — `npm run electron:serve` for dev
- **No env files**: API URL is hardcoded, not loaded from `.env`

## Code style

- **Frontend**: 2-space indent, LF line endings, no semicolons, single quotes, 100 char print width (Prettier + EditorConfig)
- **Backend**: Standard .NET conventions, nullable enabled, implicit usings enabled
- **Frontend language**: UI strings, comments, and identifiers in the codebase are in Spanish — follow this convention
