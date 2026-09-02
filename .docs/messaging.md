# Zetatech.Accelerate.RabbitMQ
## Introducción
Librería perteneciente al framework **Zetatech Accelerate**, desarrollada por **Zeta Technologies** y que contiene componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
## Estructura
```
├─ Zetatech
   ├─ Accelerate
      ├─ DependencyInjection      ' Métodos de extensión para el registro de componentes en el contenedor de dependencias.
      ├─ Messaging                ' Contratos de los componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
         ├─ Abstractions          ' Clases base para componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
         ├─ Factories             ' Factorías para la creación de componentes de publicación y suscripción a colas y tópicos de mensajería.
         ├─ Messages              ' Mensajes especializados y gestionados por los componentes de publicación y suscripción a colas y tópicos de mensajería.
```
## Control de versiones
### v10.2609.3
- Se añade el contrato para los suscriptores de mensajería.
- Se añade la clase base para los suscriptores de mensajería.
- Se añade la clase base para los suscriptores de mensajería, especializada en RabbitMQ.
- Se añade el contrato para los publicadores de mensajería.
- Se añade la clase base para los publicadores de mensajería.
- Se añade la clase base para los publicadores de mensajería, especializada en RabbitMQ.
- Se añade la factoría para la creación de conexiones con los servidores de RabbitMQ.
