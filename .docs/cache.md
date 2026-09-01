# Zetatech.Accelerate.Cache
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene los componentes para la gestión de la caché.
## Espacios de nombres
```
├─ Zetatech
   ├─ Accelerate
      ├─ Cache                    ' Contratos para los componentes de gestión de la caché.
         ├─ InMemory              ' Componentes especializados para la gestión de la caché en memoria.
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Jobs                     ' Procesos en segundo plano que sirven de apoyo a los componentes principales.
```
## Configuraciones
### Cache
#### InMemoryCache
``` json
{
   "inMemoryCache": {
      "maxItems": 1000
   }
}
```
## Control de versiones
### v10.2609.3
- Se añade el contrato para los componentes que permiten la gestión de la caché.
- Se añaden las clases especializadas para la gestión de caché en memoria.
- Se añade un proceso en segundo plano para realizar las labores de limpieza sobre el componente de gestión de la caché en memoria.
