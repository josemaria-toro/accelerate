# Zetatech.Accelerate.Telemetry
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene las clases base para componentes que registran información sobre la telemetría de las aplicaciones.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ Http
         ├─ Middlewares           ' Middlewares para registrar la telemetría de las aplicaciones.
```
## Configuración
### DeepSight
``` json
{
   "logging": {
      "deepSight": {
         "appName": "",
         "appVersion": "x.x.x",
         "tenant": "",
         "url": ""
      }
   }
}
```
## Control de versiones
### v10.2608.0
- Versión inicial de la librería en la que se incluye:
  - Implementación especializada para el envío de información al sistema DeepSight.
  - Middleware para registrar información sobre las peticiones HTTP recibidas en un api o una aplicación web.