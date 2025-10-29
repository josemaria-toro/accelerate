# Zetatech Accelerate Framework
## 📋 Overview
Zetatech Accelerate Framework is a collection of .NET libraries providing essential building blocks for application development. Built on a modular architecture, it supports multiple service providers. All libraries are developed using .NET 8 and are distributed under the GNU General Public License.
## 🏗️ Framework Structure
### Core Libraries
- **Zetatech.Accelerate:** The framework's main library containing base contracts and components for application layer, caching, data access, messaging, and telemetry.
- **Zetatech.Accelerate.Abstractions:** Contains base classes for all components across application layer, caching, data, logging, messaging, telemetry, and web.
### Functional Libraries
- **Zetatech.Accelerate.Caching:** Features a custom implementation of a memory-based caching service.
- **Zetatech.Accelerate.Configuration:** Provides extension methods to configure the configuration management service in the dependency container.
- **Zetatech.Accelerate.Logging:** Includes the following custom implementations:
  - Factory for creating trace writer instances
  - Trace providers based on console, PostgreSQL databases, and RabbitMQ messaging
- **Zetatech.Accelerate.Messaging:** Implements a custom RabbitMQ-based message publisher.
- **Zetatech.Accelerate.Telemetry:** Provides custom telemetry services using PostgreSQL databases and RabbitMQ messaging.
## 🔧 Key Features
- Modular architecture for flexible implementation
- Multiple provider support
- Seamless integration with PostgreSQL and RabbitMQ
- Extensible through dependency injection
- Built with modern .NET 8 practices
## 🤝 Contributing
- Repository: [GitHub](https://github.com/josemaria-toro/accelerate.git)
- Bug reports and pull requests are welcome
- All code is open source under GNU GPL
## 🏢 Developed By
Zeta Technologies - Building robust solutions for modern development challenges.
