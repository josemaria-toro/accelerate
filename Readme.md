# Zetatech Accelerate Framework
## Descripción

**Zetatech Accelerate** es un framework robusto para aplicaciones .NET que proporciona una arquitectura desacoplada y extensible. Desarrollado por **Zeta Technologies**, está diseñado para acelerar el desarrollo de aplicaciones empresariales modernas, ofreciendo patrones de diseño consolidados y componentes reutilizables.

## Proyectos

- **[Application](.docs/application.md)**: proyecto que contiene los componentes de la capa de aplicación.
- **[AspNetCore](.docs/aspnetcore.md)**: proyecto que contiene los componentes para ASP.NET Core.
- **[Cache](.docs/cache.md)**: proyecto que contiene los componentes para la gestión de la caché.
- **[Configuration](.docs/configuration.md)**: proyecto que contiene los componentes para la gestión de la configuración.
- **[Data](.docs/data.md)**: proyecto que contiene componentes de la capa de acceso a datos.
- **[Domain](.docs/domain.md)**: proyecto que contiene los componentes de la capa de dominio.
- **[Exceptions](.docs/exceptions.md)**: proyecto que contiene el catálogo de exceptiones.
- **[Http](.docs/http.md)**: proyecto que contiene los componentes HTTP.
- **[Jobs](.docs/jobs.md)**: proyecto que contiene los componentes para el procesamiento en segundo plano.
- **[Logging](.docs/logging.md)**: proyecto que contiene los componentes para registrar información de diagnóstico de las aplicaciones.
- **[Messaging](.docs/messaging.md)**: proyecto que contiene componentes que realizan la publicación y suscripción a colas y tópicos de mensajería.
- **[Serialization](.docs/serialization.md)**: proyecto que contiene los componentes para la serialización / deserialización de objetos.
- **[Telemetry](.docs/telemetry.md)**: proyecto que contiene los componentes para registrar información de telemetría de las aplicaciones.

## Matriz de impactos

Si realizas cambios en un proyecto, ¿qué otros proyectos se ven afectados?

| Proyecto          | Impacta a                   | Riesgo | Tipo de riesgo           |
|:------------------|:----------------------------|:------:|:-------------------------|
| **Application**   | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **AspNetCore**    | Telemetry                   |   🟢   | Cambios aislados por uso |
| **Cache**         | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Configuration** | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Data**          | Domain                      |   🟡   | Cambios en herencias     |
| **Domain**        | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Exceptions**    | AspNetCore, Messaging       |   🟢   | Cambios aislados por uso |
| **Http**          | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Jobs**          | Cache, Logging, Telemetry   |   🟡   | Cambios en herencias     |
| **Logging**       | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Messaging**     | *(ninguno)*                 |   🟢   | Sin riesgo               |
| **Serialization** | AspNetCore, Http, Messaging |   🟢   | Cambios aislados por uso |
| **Telemetry**     | *(ninguno)*                 |   🟢   | Sin riesgo               |

## Contribución

- **Versión actual:** 10.2609.3
- **Licencia:** Todo el código es open source disponible bajo la licencia GNU GPL
- **Repository:** [GitHub](https://github.com/josemaria-toro/accelerate.git)
