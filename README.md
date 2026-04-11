# Sistema de Convenios UTN San Francisco

Este proyecto es un sistema integral para la gestión de Convenios (Marcos y Específicos) en la Universidad Tecnológica Nacional (UTN), Facultad Regional San Francisco. Permite la administración de las empresas contrapartes, carreras, personas involucradas (alumnos, docentes) y el manejo de archivos digitalizados asociados a cada convenio.

## Arquitectura del Proyecto

El sistema está dividido en dos grandes módulos:

- **[`APIconvenios`](./APIconvenios/) (Backend):** Una API REST robusta construida con **.NET 8**. Gestiona toda la lógica de negocio y se conecta a una base de datos local SQLite utilizando Entity Framework Core.
- **[`UserInterface`](./UserInterface/) (Frontend):** Una interfaz de usuario de tipo SPA desarrollada con **Vue 3**, **Vite** y **Pinia**. Además, está pensada para ser compilada como una aplicación de escritorio multiplataforma utilizando **Electron**.

---

## Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) para ejecutar el backend.
- [Node.js](https://nodejs.org/) (recomendado v20+) para el frontend y entorno Electron.

---

## Guía de Inicio Rápido

### 1. Iniciar el Backend (API)

Abre una terminal, navega a la carpeta de la API e inicia el proceso:

```bash
cd APIconvenios
dotnet run
```

La base de datos SQLite y las carpetas para Logs se auto-configurarán en la carpeta `AppData/Roaming/SistemaConveniosUTNv3` de tu usuario. Las migraciones de base de datos se aplicarán de forma automática al iniciar y la API normalmente se alojará en el puerto `http://localhost:8888`.

#### 📌 Poblar Base de Datos (Seeder)

Si es la primera vez que levantas el proyecto (o si quieres tener datos ficticios para realizar pruebas), el backend cuenta con un **Seeder** que inyectará Empresas, Convenios y datos relacionados.

Para ejecutarlo, corre este comando desde la raíz del proyecto global (donde está tu `.sln`):

```bash
dotnet run --project APIconvenios -- --seed
```

> Al ejecutar el comando con la flag `--seed`, el sistema conectará con la DB, insertará los registros de prueba y finalizará el proceso inmediatamente. Luego de eso, puedes usar `dotnet run` para iniciarla en modo habitual.

### 2. Iniciar el Frontend (UI)

Abre una **segunda terminal**, navega a la carpeta de interfaz de usuario e instala las dependencias de Node:

```bash
cd UserInterface
npm install
```

Una vez instaladas las dependencias, dispones de dos enfoques para correrlo en modo desarrollo:

**Opción A: Levantar como aplicación Web (Vite):**
```bash
npm run dev
```

**Opción B: Levantar como aplicación de Escritorio (Electron):**
```bash
npm run electron:serve
```

---

## Estructura de Base de Datos

El diseño principal contempla:

- `Empresa`: Empresas u organizaciones externas que pactan los acuerdos.
- `ConvenioMarco`: Un convenio general 1 a 1 con la Empresa.
- `ConvenioEspecifico`: Múltiples actividades, actas o pasantías específicas (pueden ser muchas por cada Empresa/Convenio Marco).
- `Involucrados` y `Carreras`: Relaciones Muchos a Muchos (M:N) ligadas al Convenio Específico.
- `ArchivosAdjuntos`: Documentos PDF / Scaneos atados a convenios.
