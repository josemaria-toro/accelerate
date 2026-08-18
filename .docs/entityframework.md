# Zetatech.Accelerate.EntityFramework
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene componentes de la capa de acceso a datos, especializados en EntityFramework.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ Data                     ' Clases para componentes especializados en EntityFramework.
         ├─ Abstractions          ' Clases base para los componentes de la capa de acceso a datos.
         ├─ Contexts              ' Contexto de base de datos especializado en EntityFramework.
         ├─ Enums                 ' Enumeraciones utilizadas por los componentes de la capa de acceso a datos.
```
## Control de versiones
### v10.2609.0
- Se incluyen propiedades en los repositorios para facilitar la extensión de funcionalidades.
- Se revisan las implementaciones de los métodos asíncronos.
### v10.2608.0
- Se incluyen las clases base para repositorios basados en EntityFramework con soporte para los siguientes motores de base de datos:
  - Azure SQL
  - Azure Synapse
  - Bases de datos en memoria
  - PostgreSQL
  - SQLite
  - SQLServer
