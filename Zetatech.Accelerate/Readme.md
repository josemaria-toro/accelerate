# Zetatech.Accelerate
## Table of contents
- [Overview](#overview)
- [Application](#application)
  - [IApplicationService](#iapplicationservice)
  - [IDataTransferObject](#idatatransferobject)
- [Caching](#caching)
  - [ICacheService](#icacheservice)
- [Data](#data)
  - [IEntity](#ientity)
  - [IRepository](#irepository)
  - [ISpecification](#ispecification)
- [Logging](#logging)
  - [ILoggingFactory](#iloggingfactory)
  - [ILoggingProvider](#iloggingprovider)
  - [ILoggingService](#iloggingservice)
- [Messaging](#messaging)
  - [IMessage](#imessage)
  - [IPublisherService](#ipublisherservice)
  - [ISubscriberService](#isubscriberservice)
- [Telemetry](#telemetry)
  - [IDependency](#idependency)
  - [IEvent](#ievent)
  - [IMetric](#imetric)
  - [IPageView](#ipageview)
  - [IRequest](#irequest)
  - [ITelemetryService](#itelemetryservice)
  - [ITest](#itest)
- [Extensions](#extensions)
  - [ICacheServiceExtensions](#icacheserviceextensions)
  - [ILoggingFactoryExtensions](#iloggingfactoryextensions)
  - [ILoggingProviderExtensions](#iloggingproviderextensions)
  - [ILoggingServiceExtensions](#iloggingserviceextensions)
  - [IPublisherServiceExtensions](#ipublisherserviceextensions)
  - [IRepositoryExtensions](#irepositoryextensions)
  - [ISpecificationExtensions](#ispecificationextensions)
  - [ISubscriberServiceExtensions](#isubscriberserviceextensions)
  - [ITelemetryServiceExtension](#itelemetryserviceextension)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the foundational building blocks for the Zetatech Accelerate Framework.  
## Application
### IApplicationService
Provides the interface for implementing custom application services.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Application  
```csharp
public interface IApplicationService : IDisposable
```
### IDataTransferObject
Provides the interface for implementing custom data transfer objects.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Application  
```csharp
public interface IDataTransferObject : ICloneable
```
## Caching
### ICacheService
Provides the interface for implementing custom cache services.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Caching  
```csharp
public interface ICacheService : IDisposable
```
#### Methods
| Name                                  | Description                                                                                                                                                   |
|:--------------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Add<TValue>(String, TValue)           | Adds a value to the cache with the specified key.                                                                                                             |
| Add<TValue>(String, TValue, DateTime) | Adds a value to the cache with the specified key and expiration time.                                                                                         |
| Clear()                               | Removes all items from the cache.                                                                                                                             |
| Contains(String)                      | Determines whether the cache contains an item with the specified key. Returns true if the cache contains an item with the specified key; otherwise, false.    |
| Get<TValue>(String)                   | Retrieves the value associated with the specified key from the cache. Returns the value associated with the specified key, or the default value if not found. |
| Remove(String)                        | Removes the item with the specified key from the cache.                                                                                                       |
## Data
### IEntity
Provides the interface for implementing custom data entities.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Data  
``` csharp
public interface IEntity : ICloneable
```
### IRepository
Provides the interface for implementing custom data access repositories.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Data  
```csharp
public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
```
#### Methods
| Name                               | Description                                                                                           |
|:-----------------------------------|:------------------------------------------------------------------------------------------------------|
| ApplyChangesInTableSchema()        | Apply the pending changes in the table schema.                                                        |
| Commit()                           | Commits all pending changes to the data store. Returns the number of affected records.                |
| Delete(TEntity)                    | Deletes the specified entity from the data store.                                                     |
| Delete(IEnumerable)                | Deletes the specified collection of entities from the data store.                                     |
| Delete(Expression)                 | Deletes entities that match the specified expression from the data store.                             |
| First()                            | Returns the first entity in the data store.                                                           |
| First(Expression)                  | Returns the first entity that matches the specified expression.                                       |
| First(Expression, Int32)           | Returns the first entity that matches the specified expression, skipping a given number of entities.  |
| Insert(TEntity)                    | Inserts the specified entity into the data store.                                                     |
| Insert(IEnumerable)                | Inserts the specified collection of entities into the data store.                                     |
| Rollback()                         | Rolls back all pending changes that have not been committed.                                          |
| Select()                           | Returns all entities from the data store.                                                             |
| Select(Expression)                 | Returns all entities that match the specified expression.                                             |
| Select(Expression, Int32)          | Returns entities that match the specified expression, skipping a given number of entities.            |
| Select(Expression, Int32, Int32)   | Returns entities that match the specified expression, skipping and taking a given number of entities. |
| Select(String, IDbDataParameter[]) | Execute a custom query string to select entities.                                                     |
| Update(TEntity)                    | Updates the specified entity in the data store.                                                       |
| Update(IEnumerable)                | Updates the specified collection of entities in the data store.                                       |

### ISpecification
Defines a specification interface for evaluating whether an entity of type `TEntity` satisfies certain criteria.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Data  
```csharp
public interface ISpecification<TEntity> : IDisposable where TEntity : IEntity
```
#### Methods
| Name                   | Description                                                                   |
|:-----------------------|:------------------------------------------------------------------------------|
| IsSatisfiedBy(TEntity) | Determines whether the specified entity satisfies the specification criteria. |
## Logging
### ILoggingFactory
Provides the interface for implementing custom logging factories.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public interface ILoggingFactory : IDisposable, ILoggerFactory
```
### ILoggingProvider
Provides the interface for implementing custom logging providers.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public interface ILoggingProvider : IDisposable, ILoggerProvider
```
### ILoggingService
Provides the interface for implementing custom logging services.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public interface ILoggingService : IDisposable, ILogger
```
## Messaging
### IMessage
Provides the interface for implementing custom message.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public interface IMessage : ICloneable
```
#### Properties
| Name        | Type     | Description                                                              |
|:------------|:---------|:-------------------------------------------------------------------------|
| Id          | Guid     | Gets or sets the unique identifier of the message.                       |
| OperationId | Guid     | Gets or sets the operation identifier used to associate related message. |
| Timestamp   | DateTime | Gets or sets the timestamp when the message was published.               |
### IPublisherService
Provides the interface for implementing custom publisher services for broadcasting messages to subscribers.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public interface IPublisherService<TMessage> : IDisposable where TMessage : IMessage
```
#### Methods
| Name                            | Description                                                              |
|:--------------------------------|:-------------------------------------------------------------------------|
| Publish(TMessage)               | Publishes a message to all subscribed instances.                         |
| Subscribe(ISubscriberService)   | Subscribes the specified subscriber to receive published messages.       |
| Unsubscribe(ISubscriberService) | Unsubscribes the specified subscriber from receiving published messages. |
### ISubscriberService
Provides the interface for implementing custom subscriber services for receiving messages from a publishers.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public interface ISubscriberService<TMessage> : IDisposable where TMessage : IMessage
```
#### Methods
| Name              | Description                            |
|:------------------|:---------------------------------------|
| Receive(TMessage) | Receives a message from the publisher. |
## Telemetry
### IDependency
Provides the interface for implementing custom dependency calls.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public interface IDependency : ICloneable
```
#### Properties
| Name        | Type     | Description                                                                                  |
|:------------|:---------|:---------------------------------------------------------------------------------------------|
| Duration    | TimeSpan | Gets or sets the duration of the dependency call.                                            |
| InputData   | String   | Gets or sets the command or data sended to the dependency call.                              |
| Name        | String   | Gets or sets the name of the dependency call.                                                |
| OperationId | Guid     | Gets or sets the operation identifier used to associate related dependency call information. |
| OutputData  | String   | Gets or sets the output data returned by the dependency call.                                |
| Success     | Boolean  | Gets or sets a value indicating whether the dependency call was successful.                  |
| Target      | String   | Gets or sets the target system or endpoint of the dependency call.                           |
| Type        | String   | Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).                        |

### IEvent
Provides the interface for implementing custom events.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public interface IEvent : ICloneable
```
#### Properties
| Name        | Type   | Description                                                                        |
|:------------|:-------|:-----------------------------------------------------------------------------------|
| Name        | String | Gets or sets the name of the event.                                                |
| OperationId | Guid   | Gets or sets the operation identifier used to associate related event information. |
### IMetric
Provides the interface for implementing custom metrics.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
``` csharp
public interface IMetric : ICloneable
```
#### Properties
| Name        | Type   | Description                                                                         |
|:------------|:-------|:------------------------------------------------------------------------------------|
| Dimension   | String | Gets or sets the dimension or category associated with the metric.                  |
| Name        | String | Gets or sets the name of the metric.                                                |
| OperationId | Guid   | Gets or sets the operation identifier used to associate related metric information. |
| Value       | Double | Gets or sets the value of the metric.                                               |
### IPageView
Provides the interface for implementing custom page views.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
``` csharp
public interface IPageView : ICloneable
```
#### Properties
| Name        | Type     | Description                                                                            |
|:------------|:---------|:---------------------------------------------------------------------------------------|
| Duration    | TimeSpan | Gets or sets the duration of the page view.                                            |
| Name        | String   | Gets or sets the name of the page viewed.                                              |
| OperationId | Guid     | Gets or sets the operation identifier used to associate related page view information. |
| Uri         | Uri      | Gets or sets the URI of the page viewed.                                               |
### IRequest
Provides the interface for implementing custom HTTP requests.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public interface IRequest : ICloneable
```
#### Properties
| Name         | Type           | Description                                                                               |
|:-------------|:---------------|:------------------------------------------------------------------------------------------|
| Duration     | TimeSpan       | Gets or sets the duration of the HTTP request.                                            |
| IpAddress    | IPAddress      | Gets or sets the IP address from which the HTTP request originated.                       |
| Name         | String         | Gets or sets the name or identifier of the HTTP request.                                  |
| OperationId  | Guid           | Gets or sets the operation identifier used to associate related HTTP request information. |
| ResponseCode | HttpStatusCode | Gets or sets the HTTP response code returned for the HTTP request.                        |
| Success      | Boolean        | Gets or sets a value indicating whether the HTTP request was successful.                  |
| Timestamp    | DateTime       | Gets or sets the timestamp indicating when the HTTP request is tracked.                   |
| Uri          | Uri            | Gets or sets the URI associated with the HTTP request.                                    |
### ITelemetryService
Provides the interface for implementing custom telemetry services.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public interface ITelemetryService : IDisposable
```
#### Methods
| Name               | Description                                    |
|:-------------------|:-----------------------------------------------|
| Track(IDependency) | Tracks the specified dependency information.   |
| Track(IEvent)      | Tracks the specified event information.        |
| Track(IMetric)     | Tracks the specified metric information.       |
| Track(IPageView)   | Tracks the specified page view information.    |
| Track(IRequest)    | Tracks the specified HTTP request information. |
| Track(ITest)       | Tracks the specified test information.         |
### ITest
Provides the interface for implementing custom tests.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
```csharp
public interface ITest : ICloneable
```
#### Properties
| Name        | Type     | Description                                                                       |
|:------------|:---------|:----------------------------------------------------------------------------------|
| Duration    | TimeSpan | Gets or sets the duration of the test.                                            |
| Message     | String   | Gets or sets the message associated with the test result.                         |
| Name        | String   | Gets or sets the name of the test.                                                |
| OperationId | Guid     | Gets or sets the operation identifier used to associate related test information. |
| Success     | Boolean  | Gets or sets a value indicating whether the test was successful.                  |
## Extensions
### ICacheServiceExtensions
Extensions methods for the `ICacheService` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Caching  
```csharp
public static class ICacheServiceExtensions
```
#### Methods
| Name                                                      | Description                                                                                                                                                   |
|:----------------------------------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| AddAsync<TValue>(ICacheService, String, TValue)           | Adds a value to the cache with the specified key.                                                                                                             |
| AddAsync<TValue>(ICacheService, String, TValue, DateTime) | Adds a value to the cache with the specified key and expiration time.                                                                                         |
| ClearAsync(ICacheService)                                 | Removes all items from the cache.                                                                                                                             |
| ContainsAsync(ICacheService, String)                      | Determines whether the cache contains an item with the specified key. Returns true if the cache contains an item with the specified key; otherwise, false.    |
| GetAsync<TValue>(ICacheService, String)                   | Retrieves the value associated with the specified key from the cache. Returns the value associated with the specified key, or the default value if not found. |
| RemoveAsync(ICacheService, String)                        | Removes the item with the specified key from the cache.                                                                                                       |
### ILoggingFactoryExtensions
Extensions methods for the `ILoggingFactory` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public static class ILoggingFactoryExtensions
```
#### Methods
| Name                                          | Description                                                  |
|:----------------------------------------------|:-------------------------------------------------------------|
| AddProvider(ILoggingFactory, ILoggerProvider) | Adds an instance of `ILoggerProvider` to the logging system. |
| CreateLoggerAsync(ILoggingFactory, String)    | Creates a new instance of `ILogger`.                         |
### ILoggingProviderExtensions
Extensions methods for the `ILoggingProvider` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public static class ILoggingProviderExtensions
```
#### Methods
| Name                                        | Description                          |
|:--------------------------------------------|:-------------------------------------|
| CreateLoggerAsync(ILoggingProvider, String) | Creates a new instance of `ILogger`. |
### ILoggingServiceExtensions
Extensions methods for the `ILoggingService` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Logging  
```csharp
public static class ILoggingServiceExtensions
```
#### Methods
| Name                                                                          | Description                                |
|:------------------------------------------------------------------------------|:-------------------------------------------|
| LogAsync<TState>(ILoggingService, LogLevel, EventId, TState, Exception, Func) | Writes a log entry.                        |
| IsEnabledAsync(ILoggingService, LogLevel)                                     | Checks if the given `logLevel` is enabled. |
| BeginScopeAsync<TState>(ILoggingService, TState)                              | Begins a logical operation scope.          |
### IPublisherServiceExtensions
Extensions methods for the `IPublisherService<TMessage>` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public static class IPublisherServiceExtensions
```
#### Methods
| Name                                                    | Description                                                              |
|:--------------------------------------------------------|:-------------------------------------------------------------------------|
| PublishAsync(IPublisherService, TMessage)               | Publishes a message to all subscribed instances.                         |
| SubscribeAsync(IPublisherService, ISubscriberService)   | Subscribes the specified subscriber to receive published messages.       |
| UnsubscribeAsync(IPublisherService, ISubscriberService) | Unsubscribes the specified subscriber from receiving published messages. |
## IRepositoryExtensions
Extensions methods for the `IRepository<TEntity>` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Data  
```csharp
public static class IRepositoryExtensions
```
#### Methods
| Name                                                 | Description                                                                                           |
|:-----------------------------------------------------|:------------------------------------------------------------------------------------------------------|
| CommitAsync(IRepository)                             | Commits all pending changes to the data store. Returns the number of affected records.                |
| DeleteAsync(IRepository, TEntity)                    | Deletes the specified entity from the data store.                                                     |
| DeleteAsync(IRepository, IEnumerable)                | Deletes the specified collection of entities from the data store.                                     |
| DeleteAsync(IRepository, Expression)                 | Deletes entities that match the specified expression from the data store.                             |
| FirstAsync(IRepository)                              | Returns the first entity in the data store.                                                           |
| FirstAsync(IRepository, Expression)                  | Returns the first entity that matches the specified expression.                                       |
| FirstAsync(IRepository, Expression, Int32)           | Returns the first entity that matches the specified expression, skipping a given number of entities.  |
| InsertAsync(IRepository, TEntity)                    | Inserts the specified entity into the data store.                                                     |
| InsertAsync(IRepository, IEnumerable)                | Inserts the specified collection of entities into the data store.                                     |
| RollbackAsync(IRepository)                           | Rolls back all pending changes that have not been committed.                                          |
| SelectAsync(IRepository)                             | Returns all entities from the data store.                                                             |
| SelectAsync(IRepository, Expression)                 | Returns all entities that match the specified expression.                                             |
| SelectAsync(IRepository, Expression, Int32)          | Returns entities that match the specified expression, skipping a given number of entities.            |
| SelectAsync(IRepository, Expression, Int32, Int32)   | Returns entities that match the specified expression, skipping and taking a given number of entities. |
| SelectAsync(IRepository, String, IDbDataParameter[]) | Execute a custom query string to select entities.                                                     |
| UpdateAsync(IRepository, TEntity)                    | Updates the specified entity in the data store.                                                       |
| UpdateAsync(IRepository, IEnumerable)                | Updates the specified collection of entities in the data store.                                       |
### ISpecificationExtensions
Extensions methods for the `ISpecification<TEntity>` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Data  
``` csharp
public static class ISpecificationExtensions
```
#### Methods
| Name                                                 | Description                                                                   |
|:-----------------------------------------------------|:------------------------------------------------------------------------------|
| IsSatisfiedByAsync<TEntity>(ISpecification, TEntity) | Determines whether the specified entity satisfies the specification criteria. |

### ISubscriberServiceExtensions
Extensions methods for the `ISubscriberService<TMessage>` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public static class ISubscriberServiceExtensions
```
#### Methods
| Name                                       | Description                            |
|:-------------------------------------------|:---------------------------------------|
| ReceiveAsync(ISubscriberService, TMessage) | Receives a message from the publisher. |
### ITelemetryServiceExtension
Extensions methods for the `ITelemetryService` interface.  
**Assembly:** Zetatech.Accelerate.dll  
**Namespace**: Zetatech.Accelerate.Telemetry  
``` csharp
public static class ITelemetryServiceExtension
```
#### Methods
| Name                                       | Description                                    |
|:-------------------------------------------|:-----------------------------------------------|
| TrackAsync(ITelemetryService, IDependency) | Tracks the specified dependency information.   |
| TrackAsync(ITelemetryService, IEvent)      | Tracks the specified event information.        |
| TrackAsync(ITelemetryService, IMetric)     | Tracks the specified metric information.       |
| TrackAsync(ITelemetryService, IPageView)   | Tracks the specified page view information.    |
| TrackAsync(ITelemetryService, IRequest)    | Tracks the specified HTTP request information. |
| TrackAsync(ITelemetryService, ITest)       | Tracks the specified test information.         |
## Feedback & Contributing
Zetatech.Accelerate is released as open source under the [GNU General Public License](../License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes the contracts for application layer components.
- Includes the contracts and extensions methods for caching components.
- Includes the contracts and extensions methods for data access components.
- Includes the contracts and extensions methods for messaging components.
- Includes the contracts and extensions methods for telemetry components.

```
Zeta Technologies
```