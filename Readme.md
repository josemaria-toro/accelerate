# Zetatech Accelerate Framework
## 📋 Descripción
**Zetatech Accelerate** es un framework robusto para aplicaciones .NET que proporciona una arquitectura desacoplada y extensible. Desarrollado por **Zeta Technologies**, está diseñado para acelerar el desarrollo de aplicaciones empresariales modernas, ofreciendo patrones de diseño consolidados y componentes reutilizables. El framework actúa como una **librería de contratos y abstracciones** que establece las bases arquitectónicas para:
- **Aplicaciones N-Capa** con separación clara de responsabilidades.
- **Sistemas distribuidos** con capacidades de publicación/suscripción de mensajes.
- **Rastreo integral** de operaciones, errores y métricas.
- **Caché** en memoria con control de expiración.
- **Acceso a datos** haciendo uso del patrón repositorio con Entity Framework.
- **Inyección de dependencias** administrada de forma centralizada.
- **Manejo robusto de excepciones** con tipos específicos según el escenario.
## 🏗️ Estructura
```
Zetatech.Accelerate
├─ Application              # Interfaces principales de la capa de aplicación
│  ├─ Abstractions          # Clases base y abstracciones de la capa de aplicación
├─ DependencyInjection      # Configuración e inyección de dependencias
├─ Domain                   # Interfaces principales de la capa de dominio
│  ├─ Abstractions          # Clases base y abstracciones de la capa de dominio
├─ Exceptions               # Excepciones específicas del framework
├─ Extensions               # Clases de extensión y utilidades
├─ Infrastructure           # Implementaciones especificas de los servicios principales del framework
└─ Web                      # Componentes de la capa web para ASP.NET
    └─ Middlewares          # Middlewares especificos del framework
```
## 🤝 Contribución
- Repositorio: [GitHub](https://github.com/josemaria-toro/accelerate.git)
- Todo el código es open source disponible bajo la licencia GNU GPL
## 🏢 Desarrollado por
Zeta Technologies - Building robust solutions for modern development challenges.
## Control de versiones
### v10.0.0
- Versión inicial del framework