# Zetatech.Accelerate.Http
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene el catálogo de excepciones.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para configurar la inyección de dependencias.
      ├─ Http
         ├─ Abstractions          ' Clases base para los componentes HTTP.
         ├─ Clients               ' Clientes HTTP especializados.
         ├─ Extensions            ' Métodos de extensión para la gestión de peticiones y respuestas HTTP.
         ├─ Middlewares           ' Middlewares para el diagnóstico y el control de la seguridad.
```
## Configuración
### Cors
``` json
{
   "cors": {
      "enabled": false,
      "policies": {
         "policy name": {
            "headers": "", // list of headers, separated by pipes '|' or * to allow any header
            "methods": "", // list of methods, separated by pipes '|' or * to allow any method
            "origins": "" // list of origins urls, separated by pipes '|' or * to allow any origin
         }
      }
   }
}
### JsonClient
``` json
{
   "jsonClient": {
      "authType": "none | basic | bearerToken | mTLS",
      "autoRedirect": false,
      "encoding": "ascii | latin | unicode | utf-8 | utf-16 | utf-32",
      "useProxy": false
   }
}
```
#### AuthType: Basic
``` json
{
   "jsonClient": {
      "authType": "basic",
      "authorization": {
         "domain": "",
         "password": "",
         "userName": ""
      }
   }
}
```
#### AuthType: Bearer Token
``` json
{
   "jsonClient": {
      "authType": "bearerToken",
      "authorization": {
         "bearerToken": ""
      }
   }
}
```
#### AuthType: Mutual TLS usando el almacén de certificados (sólo para sistemas Windows)
``` json
{
   "jsonClient": {
      "authType": "mTLS",
      "authorization": {
         "certSerialNumber": "", // mandatory if thumbprint is empty
         "certStoreLocation": "Machine | User",
         "certThumbprint": "" // mandatory if serial number is empty
      }
   }
}
```
#### AuthType: Mutual TLS usando un archivo físico
``` json
{
   "jsonClient": {
      "authType": "mTLS",
      "authorization": {
         "certFileName": "",
         "certKeyFileName": "",
         "certPassword": ""
      }
   }
}
```
#### AutoRedirect
``` json
{
   "jsonClient": {
      "autoRedirect": true,
      "maxRedirections": 5
   }
}
```
#### UseProxy
``` json
{
   "jsonClient": {
      "useProxy": true,
      "proxy": {
         "domain": "",
         "password": "",
         "userName": "",
         "url": ""
      }
   }
}
```
### Rate Limits
``` json
{
   "rateLimits": {
      "enabled": false,
      "maxRequests": 25,
      "queueSize": 1000
   }
}
```
### Static Assets
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
### v10.2608.0
- Versión inicial de la librería en la que se incluye:
  - Clases base para controladores.
  - Cliente HTTP especializado en contenido JSON.
  - Middleware para comprobar la existencia de ciertas cabeceras.
  - Middleware para incluir en las respuestas, cabeceras de seguridad.
  - Middleware para habilitar el registro de datos de diagnóstico.