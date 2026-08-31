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
- Se añade las clases base para controladores api.
- Se añade las clases base para controladores web.
- Se incluyen métodos de extensión para la lectura del cuerpo de peticiones y respuestas HTTP.
