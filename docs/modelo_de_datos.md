# Modelo de Datos - Sistema de Gestión de Convenios

Este documento describe la estructura de la base de datos utilizada para el Sistema de Gestión de Convenios de la UTN San Francisco.

## Diagrama de Entidad-Relación

A continuación se presenta el diagrama visual que representa las tablas y sus relaciones:

![Diagrama de Base de Datos](./Diagrama%20DB.svg)

## Descripción de las Entidades

El sistema se basa en las siguientes entidades principales:

### 1. Empresas
Representa a las organizaciones (públicas o privadas) con las que la Facultad firma acuerdos. Almacena datos de contacto, CUIT y razón social.

### 2. Convenio Marco
Es el acuerdo general que sienta las bases de la relación entre la UTN y una Empresa. No suele especificar tareas concretas, sino que permite la existencia de convenios más detallados.
- Relación: Una empresa puede tener uno o varios convenios marco.

### 3. Convenio Específico
Son acuerdos detallados que se desprenden de un Convenio Marco o se firman de forma independiente para un fin particular (pasantías, investigación, servicios, etc.).
- Relación: Pertenece a un **Convenio Marco** y a una **Empresa**.

### 4. Involucrados
Almacena la información de las personas físicas que participan o son responsables de un convenio específico.

### 5. Carreras
Identifica qué programas académicos de la UTN San Francisco están relacionados con un convenio particular (por ejemplo, una pasantía para Ingeniería en Sistemas).

### 6. Archivos Adjuntos
Maneja la documentación física digitalizada (escaneos de convenios firmados, resoluciones, etc.) vinculada tanto a convenios marco como específicos.
