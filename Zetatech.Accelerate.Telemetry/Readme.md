# Zetatech.Accelerate.Telemetry
## Table of contents
- [Overview](#overview)
- [Telemetry](#telemetry)
  - [RabbitMqTelemetryService](#rabbitmqtelemetryservice)
  - [RabbitMqTelemetryServiceOptions](#rabbitmqtelemetryserviceoptions)
  - [PostgreSqlTelemetryService](#postgresqltelemetryservice)
  - [PostgreSqlTelemetryServiceOptions](#postgresqltelemetryserviceoptions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for telemetry capabilities.  
## Telemetry
### RabbitMqTelemetryService
Represents an implementation for a custom telemetry service.  
**Assembly:** Zetatech.Accelerate.Telemetry.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public sealed class RabbitMqTelemetryService : BaseTelemetryService<RabbitMqTelemetryServiceOptions>
```
#### Constructors
| Name                                               | Description                              |
|:---------------------------------------------------|:-----------------------------------------|
| RabbitMqTelemetryService(IOptions, ILoggerFactory) | Initializes a new instance of the class. |
#### Examples
```csharp
var options = Options.Create(new RabbitMqTelemetryServiceOptions { /* set properties */ });
var service = new RabbitMqTelemetryService(options);
```
### RabbitMqTelemetryServiceOptions
Represents the options for configuring RabbitMQ-based telemetry service.  
**Assembly:** Zetatech.Accelerate.Telemetry.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public sealed class RabbitMqTelemetryServiceOptions : BaseTelemetryServiceOptions
```
### Properties
| Name             | Type   | Description                                                             |
|:-----------------|:-------|:------------------------------------------------------------------------|
| AvailabilityRk   | String | Gets or sets the routing key for availability tests.                    |
| ConnectionString | String | Gets or sets the connection string to connect with the RabbitMQ server. |
| DependenciesRk   | String | Gets or sets the routing key for dependencies.                          |
| EventsRk         | String | Gets or sets the routing key for events.                                |
| Exchange         | String | Gets or sets the exchange name.                                         |
| MetricsRk        | String | Gets or sets the routing key for metrics.                               |
| PageViewsRk      | String | Gets or sets the routing key for page views.                            |
| RequestsRk       | String | Gets or sets the routing key for HTTP requests.                         |
### PostgreSqlTelemetryService
Represents an implementation for a custom PostgeSQL-based telemetry service.  
**Assembly:** Zetatech.Accelerate.Telemetry.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public sealed class PostgreSqlTelemetryService : BaseTelemetryService<PostgreSqlTelemetryServiceOptions>
```
#### Constructors
| Name                                                 | Description                              |
|:-----------------------------------------------------|:-----------------------------------------|
| PostgreSqlTelemetryService(IOptions, ILoggerFactory) | Initializes a new instance of the class. |
#### Examples
```csharp
var options = Options.Create(new PostgreSqlTelemetryServiceOptions { /* set properties */ });
var service = new PostgreSqlTelemetryService(options);
```
### PostgreSqlTelemetryServiceOptions
Represents the options for configuring PostgreSQL-based telemetry service.  
**Assembly:** Zetatech.Accelerate.Telemetry.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public sealed class PostgreSqlTelemetryServiceOptions : BaseTelemetryServiceOptions
```
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
## Feedback & Contributing
Zetatech.Accelerate.Telemetry is released as open source under the [GNU General Public License](./License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
**v8.0.0**  
- Includes a custom PostgreSQL-based telemetry service.
- Includes a custom RabbitMQ-based telemetry service.

```
Zeta Technologies
```