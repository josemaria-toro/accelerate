# Zetatech.Accelerate
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los contratos de los principales componentes del framework.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ Application              ' Contratos de los componentes de la capa de aplicación.
         ├─ Abstractions          ' Clases base para los componentes de la capa de aplicación.
      ├─ Caching                  ' Contratos de los componentes que permiten la gestión de la caché.
         ├─ InMemory              ' Clases especializadas para la gestión de caché en memoria.
      ├─ Data                     ' Contratos de los componentes de la capa de acceso a datos.
         ├─ Abstractions          ' Clases base para los componentes de la capa de acceso a datos.
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Domain                   ' Contratos de los componentes de la capa de dominio.
         ├─ Abstractions          ' Clases base para los componentes de la capa de dominio.
      ├─ Exceptions               ' Catálogo de excepciones.
      ├─ Http
         ├─ Abstractions          ' Clases base para los componentes HTTP.
         ├─ Clients               ' Clases especializadas para clientes HTTP.
         ├─ Extensions            ' Métodos de extensión para componentes HTTP.
         ├─ Middlewares           ' Clases especializadas para middlewares HTTP.
      ├─ Jobs                     ' Contratos de los componentes que gestionan la ejecución de procesos en segundo plano.
         ├─ Abstraction           ' Clases base para los componentes que ejecutan procesos en segundo plano.
      ├─ Logging
         ├─ Abstraction           ' Clases base para los componentes que registran la actividad de diagnóstico de las aplicaciones.
         ├─ Console               ' Clases especializadas para el registro de actividad de diagnóstico en la consola del sistema.
      ├─ Messaging                ' Contratos de los componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
         ├─ Abstractions          ' Clases base para componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
      ├─ Serialization            ' Clases especializadas para la serialización / deserialización de objetos.
      ├─ Telemetry                ' Contratos de los componentes que realizan el registro de datos de telemetría.
         ├─ Abstractions          ' Clases base para componentes que realizan lel registro de datos de telemetría.
```
## Configuraciones
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
### Cors
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
#### JsonClient => AuthType: Basic
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
#### JsonClient => AuthType: Bearer Token
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
#### JsonClient => AuthType: Mutual TLS usando el almacén de certificados (sólo para sistemas Windows)
``` json
{
   "jsonClient": {
      "authType": "mTLS",
      "authorization": {
         "certSerialNumber": "", // mandatory if thumbprint is empty
         "certStoreLocation": "Machine | User",
         "certThumbprint": ""    // mandatory if serial number is empty
      }
   }
}
```
#### JsonClient => AuthType: Mutual TLS usando un archivo físico
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
#### JsonClient => AutoRedirect
``` json
{
   "jsonClient": {
      "autoRedirect": true,
      "maxRedirections": 5
   }
}
```
#### JsonClient => UseProxy
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
### InMemoryCache
``` json
{
   "caching": {
      "inMemory": {
         "maxItems": 1000
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
### v10.2609.0
- Se incluyen los contratos y clases base para los componentes de la capa de aplicación.
- Se incluyen los contratos de los componentes que permiten la gestión de la caché.
- Se incluyen las clases especializadas para la gestión de caché en memoria.
- Se incluyen los métodos de extensión para registrar los orígenes de configuración más comunes (variables de entorno, secretos, fichero appsettings.json).
- Se incluyen los contratos y clases base para los componentes de la capa de acceso a datos.
- Se inlcuyen los métodos de extensión para el registro de componentes en el contenedor de dependencias.
- Se inlcuyen los contratos y clases base para los componentes de la capa de dominio.
- Se incluye el catálogo de excepciones.
- Se incluyen las clases base y especializaciones para los componentes HTTP.
- Se incluyen métodos de extensión para componentes HTTP.
- Se incluyen clases especializadas para middlewares HTTP.
- Se incluyen los contratos y clases base para los componentes que gestionan la ejecución de procesos en segundo plano.
- Se incluyen las clases base para los componentes que registran la actividad de diagnóstico de las aplicaciones.
- Se incluyen las clases especializadas para el registro de actividad de diagnóstico en la consola del sistema.
- Se incluyen los contratos y clases base para los componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
- Se incluyen las factorías para la creación de componentes de publicación y suscripción a colas y tópicos de mensajería.
- Se incluyen las clases especializadas para la serialización / deserialización de objetos.
- Se incluyen los contratos y clases base para los componentes que realizan el registro de datos de telemetría.