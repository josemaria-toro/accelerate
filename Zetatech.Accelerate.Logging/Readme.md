# Zetatech.Accelerate.Logging
## Table of contents
- [Overview](#overview)
- [Logging](#application)
  - [LoggingFactory](#loggingfactory)
  - [LoggingFactoryOptions](#loggingfactoryoptions)
  - [ConsoleLoggingService](#consoleloggingservice)
  - [ConsoleLoggingServiceOptions](#consoleloggingserviceoptions)
  - [ConsoleLoggingProvider](#consoleloggingprovider)
  - [ConsoleLoggingProviderOptions](#consoleloggingprovideroptions)
  - [PostgreSqlLoggingProvider](#postgresqlloggingprovider)
  - [PostgreSqlLoggingProviderOptions](#postgresqlloggingprovideroptions)
  - [PostgreSqlLoggingService](#postgresqlloggingservice)
  - [PostgreSqlLoggingServiceOptions](#postgresqlloggingserviceoptions)
  - [RabbitMqLoggingProvider](#rabbitmqloggingprovider)
  - [RabbitMqLoggingProviderOptions](#rabbitmqloggingprovideroptions)
  - [RabbitMqLoggingService](#rabbitmqloggingservice)
  - [RabbitMqLoggingServiceOptions](#rabbitmqloggingserviceoptions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for logging capabilities.  
## Logging
### LoggingFactory
Represents an implementation for a custom logging factory.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Factories  
```csharp
public sealed class LoggingFactory : BaseLoggingFactory<LoggingFactoryOptions>, ILoggingService
```
#### Constructors
| Name                                  | Description                              |
|:--------------------------------------|:-----------------------------------------|
| LoggingFactory(IOptions)              | Initializes a new instance of the class. |
| LoggingFactory(IOptions, IEnumerable) | Initializes a new instance of the class. |
#### Methods
| Name                       | Description                       |
|:---------------------------|:----------------------------------|
| BeginScope<TState>(TState) | Begins a logical operation scope. |
### LoggingFactoryOptions
Represents the options for configuring the logging factory.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Factories  
```csharp
public sealed class LoggingFactoryOptions : BaseLoggingFactoryOptions
```
#### Constructors
| Name                    | Description                              |
|:------------------------|:-----------------------------------------|
| LoggingFactoryOptions() | Initializes a new instance of the class. |
### ConsoleLoggingService
Represents an implementation for a custom console-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class ConsoleLoggingService : BaseLoggingService<ConsoleLoggingServiceOptions>
```
#### Constructors
| Name                                    | Description                              |
|:----------------------------------------|:-----------------------------------------|
| ConsoleLoggingService(IOptions, String) | Initializes a new instance of the class. |
#### Example
```csharp
var options = Options.Create(new ConsoleLoggingServiceOptions());
var logger = new ConsoleLoggingService(options, "AppCategory");
```
### ConsoleLoggingServiceOptions
Represents the options for configuring the console-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class ConsoleLoggingServiceOptions : BaseLoggingServiceOptions
```
#### Constructors
| Name                           | Description                              |
|:-------------------------------|:-----------------------------------------|
| ConsoleLoggingServiceOptions() | Initializes a new instance of the class. |
### ConsoleLoggingProvider
Represents an implementation for a custom console-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class ConsoleLoggingProvider : BaseLoggingProvider<ConsoleLoggingProviderOptions>
```
#### Constructors
| Name                             | Description                              |
|:---------------------------------|:-----------------------------------------|
| ConsoleLoggingProvider(IOptions) | Initializes a new instance of the class. |
#### Methods
| Name                 | Description                                               |
|:---------------------|:----------------------------------------------------------|
| CreateLogger(String) | Creates a new logger instance for the specified category. |
#### Example
```csharp
var options = Options.Create(new ConsoleLoggingProviderOptions());
var provider = new ConsoleLoggingProvider(options);
var logger = provider.CreateLogger("AppCategory");
```
### ConsoleLoggingProviderOptions
Represents the options for configuring the console-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class ConsoleLoggingProviderOptions : BaseLoggingProviderOptions
```
#### Constructors
| Name                            | Description                              |
|:--------------------------------|:-----------------------------------------|
| ConsoleLoggingProviderOptions() | Initializes a new instance of the class. |
### PostgreSqlLoggingProvider
Represents an implementation for a custom PostgreSQL-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class PostgreSqlLoggingProvider : BaseLoggingProvider<PostgreSqlLoggingProviderOptions>
```
#### Constructors
| Name                                | Description                              |
|:------------------------------------|:-----------------------------------------|
| PostgreSqlLoggingProvider(IOptions) | Initializes a new instance of the class. |
#### Methods
| Name                 | Description                                               |
|:---------------------|:----------------------------------------------------------|
| CreateLogger(String) | Creates a new logger instance for the specified category. |
#### Example
```csharp
var options = Options.Create(new PostgreSqlLoggingProviderOptions());
var provider = new PostgreSqlLoggingProvider(options);
var logger = provider.CreateLogger("AppCategory");
```
### PostgreSqlLoggingProviderOptions
Represents the options for configuring the PostgreSQL-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class PostgreSqlLoggingProviderOptions : BaseLoggingProviderOptions
```
#### Constructors
| Name                               | Description                              |
|:-----------------------------------|:-----------------------------------------|
| PostgreSqlLoggingProviderOptions() | Initializes a new instance of the class. |
#### Properties
| Name             | Type   | Description                                                      |
|:-----------------|:-------|:-----------------------------------------------------------------|
| ConnectionString | String | Gets or sets the connection string to connect with the database. |
### PostgreSqlLoggingService
Represents an implementation for a custom PostgreSQL-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class PostgreSqlLoggingService : BaseLoggingService<PostgreSqlLoggingServiceOptions>
```
#### Constructors
| Name                                        | Description                              |
|:--------------------------------------------|:-----------------------------------------|
| PostgreSqlLoggingService(IOptions, String)  | Initializes a new instance of the class. |
#### Example
```csharp
var options = Options.Create(new PostgreSqlLoggingServiceOptions());
var logger = new PostgreSqlLoggingService(options, "AppCategory");
```
### PostgreSqlLoggingServiceOptions
Represents the options for configuring the PostgreSQL-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class PostgreSqlLoggingServiceOptions : BaseLoggingServiceOptions
```
#### Constructors
| Name                              | Description                              |
|:----------------------------------|:-----------------------------------------|
| PostgreSqlLoggingServiceOptions() | Initializes a new instance of the class. |
#### Properties
| Name                 | Type    | Description                                                                                     |
|:---------------------|:--------|:------------------------------------------------------------------------------------------------|
| ConnectionString     | String  | Gets or sets the connection string used to connect with the data source.                        |
| DetailedErrors       | Boolean | Gets or sets a value indicating whether detailed error messages are enabled for the repository. |
| LazyLoading          | Boolean | Gets or sets a value indicating whether lazy loading is enabled for related entities.           |
| Schema               | String  | Gets or sets the schema where database is stored.                                               |
| SensitiveDataLogging | Boolean | Gets or sets a value indicating whether sensitive data logging is enabled for the repository.   |
| Timeout              | Int32   | Gets or sets the timeout value, in seconds, for repository operations.                          |
| TrackChanges         | Boolean | Gets or sets a value indicating whether change tracking is enabled for entities.                |
### RabbitMqLoggingProvider
Represents an implementation for a custom RabbitMQ-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class RabbitMqLoggingProvider : BaseLoggingProvider<RabbitMqLoggingProviderOptions>
```
#### Constructors
| Name                              | Description                              |
|:----------------------------------|:-----------------------------------------|
| RabbitMqLoggingProvider(IOptions) | Initializes a new instance of the class. |
#### Methods
| Name                 | Description                                               |
|:---------------------|:----------------------------------------------------------|
| CreateLogger(String) | Creates a new logger instance for the specified category. |
#### Example
```csharp
var options = Options.Create(new RabbitMqLoggingProviderOptions());
var provider = new RabbitMqLoggingProvider(options);
var logger = provider.CreateLogger("AppCategory");
```
### RabbitMqLoggingProviderOptions
Represents the options for configuring the RabbitMQ-based logging provider.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Providers  
```csharp
public sealed class RabbitMqLoggingProviderOptions : BaseLoggingProviderOptions
```
#### Constructors
| Name                             | Description                              |
|:---------------------------------|:-----------------------------------------|
| RabbitMqLoggingProviderOptions() | Initializes a new instance of the class. |
#### Properties
| Name             | Type   | Description                                                             |
|:-----------------|:-------|:------------------------------------------------------------------------|
| ConnectionString | String | Gets or sets the connection string to connect with the RabbitMQ server. |
| ErrorsRk         | String | Gets or sets the routing key for the errors messages.                   |
| Exchange         | String | Gets or sets the exchange name.                                         |
| TracesRk         | String | Gets or sets the routing key for the traces messages.                   |
### RabbitMqLoggingService
Represents an implementation for a custom RabbitMQ-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class RabbitMqLoggingService : BaseLoggingService<RabbitMqLoggingServiceOptions>
```
#### Constructors
| Name                                       | Description                              |
|:-------------------------------------------|:-----------------------------------------|
| RabbitMqLoggingService(IOptions<>, String) | Initializes a new instance of the class. |
#### Example
```csharp
var options = Options.Create(new RabbitMqLoggingServiceOptions());
var logger = new RabbitMqLoggingService(options, "AppCategory");
```
### RabbitMqLoggingServiceOptions
Represents the options for configuring the RabbitMQ-based logging service.  
**Assembly:** Zetatech.Accelerate.Logging.dll  
**Namespace**: Zetatech.Accelerate.Logging.Services  
```csharp
public sealed class RabbitMqLoggingServiceOptions : BaseLoggingServiceOptions
```
#### Constructors
| Name                            | Description                              |
|:--------------------------------|:-----------------------------------------|
| RabbitMqLoggingServiceOptions() | Initializes a new instance of the class. |
#### Properties
| Name             | Type   | Description                                                             |
|:-----------------|:-------|:------------------------------------------------------------------------|
| ConnectionString | String | Gets or sets the connection string to connect with the RabbitMQ server. |
| ErrorsRk         | String | Gets or sets the routing key for the errors messages.                   |
| Exchange         | String | Gets or sets the exchange name.                                         |
| TracesRk         | String | Gets or sets the routing key for the traces messages.                   |
## Feedback & Contributing
Zetatech.Accelerate.Logging is released as open source under the [GNU General Public License](./License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes a custom logging factory.
- Includes a custom console-based logging service and provider.
- Includes a custom PostgreSQL-based logging service and provider.
- Includes a custom RabbitMQ-based logging service and provider.

```
Zeta Technologies
```