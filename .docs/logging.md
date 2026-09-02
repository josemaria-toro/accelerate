# Zetatech.Accelerate
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los componentes para registrar información de diagnóstico de las aplicaciones.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Logging                  ' Componentes principales para registrar información de diagnóstico de las aplicaciones.
         ├─ Abstraction           ' Clases base para los componentes que registran la actividad de diagnóstico de las aplicaciones.
         ├─ ChannelEntries        ' Entradas utilizadas para registrar y leer información de los canales de comunicación.
         ├─ Jobs                  ' Procesos en segundo plano para escribir la información de diagnóstico de las aplicaciones.
         ├─ Loggers               ' Clases especializadas para el registro de actividad de diagnóstico de las aplicaciones.
```
## Configuraciones
### Logging
#### Console
``` json
{
   "logging": {
      "logLevel": {
         "console": "trace | debug | information | warning | error | critical"
      }
   }
}
```
#### FlatFile
``` json
{
   "logging": {
      "flatFile": {
         "fileName": "", // file name without extension
         "maxSize": 0, // max size in megabytes
         "path": ""
      },
      "logLevel": {
         "flatFile": "trace | debug | information | warning | error | critical"
      }
   }
}
```
## Control de versiones
### v10.2609.3
- Se incluyen las clases base para los componentes que registran la actividad de diagnóstico de las aplicaciones.
- Se incluye un proveedor especializado en la escritura de trazas en la consola del sistema
- Se incluye un proveedor especializado en la escritura de trazas en ficheros de texto plano.