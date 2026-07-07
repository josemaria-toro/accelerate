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
         "userName": "",
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
## Control de versiones
### v10.2608.0
- Se incluyen las clases base para controladores.
- Se incluye un cliente HTTP especializado en contenido JSON.
- Se incluyen middlewares para el diagnóstico y la seguridad de las peticiones.