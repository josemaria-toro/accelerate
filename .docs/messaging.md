# Zetatech.Accelerate.Messaging
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene las clases base para componentes de gestión de mensajería.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para el registro de factorías.
      ├─ Messaging
         ├─ Abstraction           ' Clases base para componentes de gestión de mensajería.
         ├─ Factories             ' Factorías para la creación de componentes de gestión de mensajería.
```
## Control de versiones
### v10.2608.0
- Versión inicial de la librería en la que se incluye:
  - Clases base para componentes de publicación y suscripción de mensajes.
  - Clases base para componentes de publicación y suscripción de mensajes especializados en RabbitMQ.