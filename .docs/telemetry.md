# Zetatech.Accelerate
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los componentes para registrar información de telemetría de las aplicaciones.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Telemetry                ' Contratos de los componentes que realizan el registro de datos de telemetría.
         ├─ Abstractions          ' Clases base para componentes que realizan lel registro de datos de telemetría.
         ├─ Collectors            ' Clases para recopilar datos sobre la telemetría de las aplicaciones.
```
## Control de versiones
### v10.2609.3
- Se añade el contrato para los componentes que realizan el registro de datos de telemetría.
- Se añade la clase base para los componentes que realizan el registro de datos de telemetría.
- Se añade un colector para registrar el consumo de CPU del proceso asociado a las aplicaciones.
- Se añade un colector para registrar el consumo de RAM del proceso asociado a las aplicaciones.
- Se añade un colector para registrar información sobre las peticiones recibidas por una aplicación ASP.NET Core.
