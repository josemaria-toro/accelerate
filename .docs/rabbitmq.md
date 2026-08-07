# Zetatech.Accelerate.RabbitMQ
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene componentes que realizan la publicación y suscripción a colas y tópicos de mensajería, especializados en RabbitMQ.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Messaging                ' Clases para componentes especializados en RabbitMQ.
         ├─ Abstractions          ' Clases base para componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
         ├─ Factories             ' Factorías para la creación de componentes de publicación y suscripción a colas y tópicos de mensajería.
         ├─ Messages              ' Mensajes especializados y gestionados por los componentes de publicación y suscripción a colas y tópicos de mensajería.
```
## Control de versiones
### v10.2609.0
- Se incluyen las clases base para componentes de publicación y suscripción de mensajes especializados en RabbitMQ.