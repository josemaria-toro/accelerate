# Zetatech.Accelerate.Data
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene componentes de la capa de acceso a datos.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ Data                     ' Contratos de los componentes de la capa de acceso a datos.
         ├─ Abstractions          ' Clases base para los componentes de la capa de acceso a datos.
         ├─ Contexts              ' Contexto de base de datos especializado en EntityFramework.
         ├─ Enums                 ' Enumeraciones utilizadas por los componentes de la capa de acceso a datos.
```
## Control de versiones
### v10.2609.3
- Se añade el contrato para la entidades de datos.
- Se añade el contrato para los repositorios de acceso a datos.
- Se añade el contrato para las unidades de trabajo para el acceso a datos.
- Se añade la clase base para repositorios basados en EntityFramework con soporte para los siguientes motores de base de datos:
  - Azure SQL
  - Azure Synapse
  - Bases de datos en memoria
  - PostgreSQL
  - SQLite
  - SQLServer
