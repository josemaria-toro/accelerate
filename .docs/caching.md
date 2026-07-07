# Zetatech.Accelerate.Caching
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene las clases para la gestión de la caché.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ Caching                  ' Clases con implementaciones especializadas.
      ├─ DependencyInjection      ' Métodos de extensión para el registro de los gestores de la caché.
```
## Configuración
### InMemory
``` json
{
   "caching": {
      "inMemory": {
         "maxItems": 4096
      }
   }
}
```
## Control de versiones
### v10.2607.0
- Se incluye una implementación especializada para la gestión de caché en memoria.