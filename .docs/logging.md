# Zetatech.Accelerate.Logging
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene las clases base para componentes que registran la actividad de diagnóstico de las aplicaciones.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ Logging
         ├─ Abstraction           ' Clases base para componentes que registran la actividad de diagnóstico de las aplicaciones.
```
## Configuración
### Console
``` json
{
   "logging": {
      "console": {
         "logLevel": "debug | information | warning | error | critical"
      }
   }
}
```
### DeepSight
``` json
{
   "logging": {
      "deepSight": {
         "appName": "",
         "appVersion": "x.x.x",
         "logLevel": "debug | information | warning | error | critical",
         "tenant": "",
         "url": ""
      }
   }
}
```
## Control de versiones
### v10.2608.0
- Se incluyen las clases base para componentes que registran la actividad de diagnóstico de las aplicaciones.
- Se incluye una implementación especializada para la escritura en consola.
- Se incluye una implementación especializada para el envío de información al sistema DeepSight.