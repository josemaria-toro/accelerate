# Zetatech.Accelerate.Data
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene las clases base para los componentes de la capa de acceso a datos.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ Data
         ├─ Abstractions          ' Clases base para los componentes de la capa de acceso a datos.
         ├─ Enums                 ' Enumeraciones
```
## Control de versiones
### v10.2608.0
- Se incluyen las clases base para repositorios y entidades.
- Se incluye soporte para repositorios basados en EntityFramework para los siguientes motores de base de datos:
  - Azure SQL
  - Azure Synapse
  - Bases de datos en memoria
  - PostgreSQL
  - SQLite
  - SQLServer
> El uso de repositorios basados en SQLite no está recomendado ya que existe una vulnerabilidad de gravedad alta conocida. Visite este [enlace](https://github.com/advisories/GHSA-2m69-gcr7-jv3q) para más información.