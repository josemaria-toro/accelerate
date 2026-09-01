# Zetatech.Accelerate.Http
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los componentes HTTP.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ Http
         ├─ Clients               ' Clases especializadas para clientes HTTP.
```
## Configuraciones
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
         "certThumbprint": ""    // mandatory if serial number is empty
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
### v10.2609.3
- Se añade un cliente HTTP especializado en JSON.
- Se añade una clase para construir las opciones para inicializar un cliente HTTP especializado en JSON.
