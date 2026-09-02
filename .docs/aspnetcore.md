# Zetatech.Accelerate.AspNetCore
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los componentes para ASP.NET Core.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ AspNetCore
         ├─ Abstractions          ' Clases base para los componentes de ASP.NET Core.
         ├─ Extensions            ' Métodos de extensión para componentes de ASP.NET Core.
         ├─ Middlewares           ' Clases especializadas para middlewares de aplicaciones ASP.NET Core.
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
```
## Configuraciones
### ASP.NET Core
#### Cors
``` json
{
   "cors": {
      "enabled": false,
      "policies": {
         "policy name": {
            "headers": "", // list of headers, separated by pipes '|' or * to allow any header
            "methods": "", // list of methods, separated by pipes '|' or * to allow any method
            "origins": ""  // list of origins urls, separated by pipes '|' or * to allow any origin
         }
      }
   }
}
```
#### Rate Limits
``` json
{
   "rateLimits": {
      "enabled": false,
      "maxRequests": 25,
      "queueSize": 1000
   }
}
```
#### Static Assets
``` json
{
   "staticAssets": {
      "enabled": false,
      "compress": true,
      "requestPath": "",
      "serveUnknownFileTypes": false
   }
}
```
## Control de versiones
### v10.2609.3
- Se añade la clase base para middlewares.
- Se añade la clase base para controladores api.
- Se añade la clase base para controladores web.
- Se añaden métodos de extensión para la lectura del cuerpo de peticiones y respuestas HTTP.
- Se añaden métodos de extensión para configurar CORS en las aplicaciones.
- Se añaden métodos de extensión para configurar los límites de peticiones en las aplicaciones.
- Se añaden métodos de extensión para configurar la ubicación de los recursos estáticos en las aplicaciones.
- Se añaden métodos de extensión para configurar los componentes MVC en las aplicaciones.
- Se añade un middleware para gestionar el código de respuesta en base a la excepción capturada.
- Se añade un middleware para requerir la existencia de cabeceras en las peticiones, comprobando sus valores de forma opcional.
- Se añade un middleware para añadir cabeceras de seguridad en las respuestas.
- Se añade un middleware para estandarizar la trazabilidad de la actividad de las aplicaciones.
