# Zetatech.Accelerate.Abstractions
## Table of contents
- [Overview](#overview)
- [Root](#root)
  - [BaseCloneable](#basecloneable)
  - [BaseDisposable](#basedisposable)
- [Application](#application)
  - [BaseApplicationService](#baseapplicationservice)
  - [BaseDataTransferObject](#basedatatransferobject)
- [Caching](#caching)
  - [BaseCacheService](#basecacheservice)
  - [BaseCacheServiceOptions](#basecacheserviceoptions)
- [Data](#data)
  - [BaseEntity](#baseentity)
  - [BaseRepository](#baserepository)
  - [BaseRepositoryContext](#baserepositorycontext)
  - [BaseRepositoryOptions](#baserepositoryoptions)
  - [BaseSpecification](#basespecification)
- [Logging](#logging)
  - [BaseLoggingFactory](#baseloggingfactory)
  - [BaseLoggingFactoryOptions](#baseloggingfactoryoptions)
  - [BaseLoggingProvider](#baseloggingprovider)
  - [BaseLoggingProviderOptions](#baseloggingprovideroptions)
  - [BaseLoggingService](#baseloggingservice)
  - [BaseLoggingServiceOptions](#baseloggingserviceoptions)
- [Messaging](#messaging)
  - [BaseMessage](#basemessage)
  - [BasePublisherService](#basepublisherservice)
  - [BasePublisherServiceOptions](#basepublisherserviceoptions)
  - [BaseSubscriberService](#basesubscriberservice)
  - [BaseSubscriberServiceOptions](#basesubscriberserviceoptions)
- [Telemetry](#telemetry)
  - [BaseTelemetryService](#basetelemetryservice)
  - [BaseTelemetryServiceOptions](#basetelemetryserviceoptions)
  - [Dependency](#dependency)
  - [Event](#event)
  - [Metric](#metric)
  - [PageView](#pageview)
  - [Request](#request)
  - [Test](#test)
- [Web](#web)
  - [BaseActionFilterAttribute](#baseactionfilterattribute)
  - [BaseApiController](#baseapicontroller)
  - [BaseAuthorizationFilterAttribute](#baseauthorizationfilterattribute)
  - [BaseMiddleware](#basemiddleware)
  - [BaseWebController](#basewebcontroller)
## Overview
This library provides the foundational building blocks for the Zetatech Accelerate Framework.  
## Root
### BaseCloneable
Represents a base class for implementing custom cloneable objects.
```csharp
[Serializable]
public abstract class BaseCloneable : ICloneable
```
#### Constructors
| Name            | Description                          |
|:----------------|:-------------------------------------|
| BaseCloneable() | Initializes a new instance of class. |
### Methods
| Name    | Description                                            |
|:--------|:-------------------------------------------------------|
| Clone() | Creates a shallow copy of the current object instance. |
## BaseDisposable
Represents a base class for implementing custom disposable objects.
```csharp
public abstract class BaseDisposable : IDisposable
```
### Constructors
| Name             | Description                          |
|:-----------------|:-------------------------------------|
| BaseDisposable() | Initializes a new instance of class. |
### Methods
| Name             | Description                                                                                              |
|:-----------------|:---------------------------------------------------------------------------------------------------------|
| Dispose()        | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Dispose(Boolean) | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
## Application
### BaseApplicationService
Represents the base class for implementing custom application services.
```csharp
public abstract class BaseApplicationService : BaseDisposable, IApplicationService
```
#### Constructors
| Name                     | Description                          |
|:-------------------------|:-------------------------------------|
| BaseApplicationService() | Initializes a new instance of class. |
#### Properties
| Name          | Data Type      | Description                                              |
|:--------------|:---------------|:---------------------------------------------------------|
| Logger        | ILogger        | Gets the instance of the logger.                         |
| LoggerFactory | ILoggerFactory | Gets or sets the factory to create instances of loggers. |
### BaseDataTransferObject
Represents the base class for implementing custom data transfer objects.
```csharp
public abstract class BaseDataTransferObject : BaseCloneable, IDataTransferObject
```
#### Constructors
| Name                     | Description                          |
|:-------------------------|:-------------------------------------|
| BaseDataTransferObject() | Initializes a new instance of class. |
## Caching
### BaseCacheService
Represents the base class for implementing custom cache services.
```csharp
public abstract class BaseCacheService<TOptions> : BaseDisposable, ICacheService where TOptions : BaseCacheServiceOptions
```
#### Constructors
| Name                       | Description                          |
|:---------------------------|:-------------------------------------|
| BaseCacheService(IOptions) | Initializes a new instance of class. |
### Properties
| Name          | Data Type      | Description                                              |
|:--------------|:---------------|:---------------------------------------------------------|
| Logger        | ILogger        | Gets the instance of the logger.                         |
| LoggerFactory | ILoggerFactory | Gets or sets the factory to create instances of loggers. |
| Options       | TOptions       | Gets the options for the messages publisher.             |
### BaseCacheServiceOptions
Represents the base options for configuring a cache service.
```csharp
public abstract class BaseCacheServiceOptions : BaseCloneable
```
#### Properties
| Name                  | Data Type | Description                                                              |
|:----------------------|:----------|:-------------------------------------------------------------------------|
| DefaultExpirationTime | Int32     | Gets or sets the default expiration time (in minutes) for cache entries. |
| MaxSize               | Int32     | Gets or sets the maximum number of entries allowed in the cache.         |
## Data
### BaseEntity
Represents the base class for implementing custom entities.
```csharp
public abstract class BaseEntity : BaseCloneable, IEntity
```
#### Constructors
| Name         | Description                          |
|:-------------|:-------------------------------------|
| BaseEntity() | Initializes a new instance of class. |
### BaseRepository
Represents the base class for implementing custom repositories.
```csharp
public abstract class BaseRepository<TEntity, TOptions, TContext> : BaseDisposable, IRepository<TEntity> where TEntity : BaseEntity
                                                                                                         where TOptions : BaseRepositoryOptions
                                                                                                         where TContext : BaseRepositoryContext<TEntity, TOptions>
```
#### Constructors
| Name                     | Description                         |
|:-------------------------|:------------------------------------|
| BaseRepository(IOptions) |Initializes a new instance of class. |
### BaseRepositoryContext
Represents the base class for implementing custom repository context based on Entity Framework.
```csharp
public abstract class BaseRepositoryContext<TEntity, TOptions> : DbContext where TEntity : BaseEntity
                                                                           where TOptions : BaseRepositoryOptions
```
#### Constructors
| Name                                         | Description                          |
|:---------------------------------------------|:-------------------------------------|
| BaseRepositoryContext(IRepository, IOptions) | Initializes a new instance of class. |
### BaseRepositoryOptions
Represents the base class for implementing custom repository options.
```csharp
public abstract class BaseRepositoryOptions : BaseCloneable
```
#### Properties
| Name                 | Data Type | Description                                                                                     |
|:---------------------|:----------|:------------------------------------------------------------------------------------------------|
| ConnectionString     | String    | Gets or sets the connection string used to connect with the data source.                        |
| DetailedErrors       | Boolean   | Gets or sets a value indicating whether detailed error messages are enabled for the repository. |
| LazyLoading          | Boolean   | Gets or sets a value indicating whether lazy loading is enabled for related entities.           |
| Schema               | String    | Gets or sets the schema where database is stored.                                               |
| SensitiveDataLogging | Boolean   | Gets or sets a value indicating whether sensitive data logging is enabled for the repository.   |
| Timeout              | Int32     | Gets or sets the timeout value, in seconds, for repository operations.                          |
| TrackChanges         | Boolean   | Gets or sets a value indicating whether change tracking is enabled for entities.                |
### BaseSpecification
Represents the base class for implementing custom specifications for evaluating whether an entity of type TEntity satisfies certain criteria.
```csharp
public abstract class BaseSpecification<TEntity> : BaseDisposable where TEntity : IEntity
```
#### Methods
| Name                   | Description                                                                   |
|:-----------------------|:------------------------------------------------------------------------------|
| IsSatisfiedBy(TEntity) | Determines whether the specified entity satisfies the specification criteria. |
## Logging
### BaseLoggingFactory
Represents the base class for implementing custom logging factories.
```csharp
public abstract class BaseLoggingFactory<TOptions> : BaseDisposable, ILoggingFactory where TOptions : BaseLoggingFactoryOptions
```
#### Constructors
| Name                         | Description                          |
|:-----------------------------|:-------------------------------------|
| BaseLoggingFactory(IOptions) | Initializes a new instance of class. |
#### Properties
| Name      | Data Type             | Description                                      |
|:----------|:----------------------|:-------------------------------------------------|
| Options   | TOptions              | Gets the configuration options.                  |
| Providers | List<ILoggerProvider> | Gets the logging providers added to the factory. |
#### Methods
| Name                         | Description                                                 |
|:-----------------------------|:------------------------------------------------------------|
| AddProvider(ILoggerProvider) | Adds an instance of logging provider to the logging system. |
### BaseLoggingFactoryOptions
Represents the base options for configuring a logging factory.
```csharp
public abstract class BaseLoggingFactoryOptions : BaseCloneable
```
#### Properties
| Name        | Data Type | Description                                                                   |
|:------------|:----------|:------------------------------------------------------------------------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging provider.         |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled.    |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled.       |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled.       |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled.     |
### BaseLoggingProvider
Represents the base class for implementing custom logging providers.
```csharp
public abstract class BaseLoggingProvider<TOptions> : BaseDisposable, ILoggingProvider where TOptions : BaseLoggingProviderOptions
```
#### Constructors
| Name                          | Description                          |
|:------------------------------|:-------------------------------------|
| BaseLoggingProvider(IOptions) | Initializes a new instance of class. |
#### Properties
| Name    | Data Type                             | Description                       |
|:--------|:--------------------------------------|:----------------------------------|
| Loggers | ConcurrentDictionary<String, ILogger> | Gets the loggers already created. |
| Options | TOptions                              | Gets the configuration options.   |
#### Methods
| Name                 | Description                                               |
|:---------------------|:----------------------------------------------------------|
| CreateLogger(String) | Creates a new logger instance for the specified category. |
### BaseLoggingProviderOptions
Represents the base options for configuring a logging provider.
```csharp
public abstract class BaseLoggingProviderOptions : BaseCloneable
```
#### Properties
| Name        | Data Type | Description                                                                   |
|:------------|:----------|:------------------------------------------------------------------------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging provider.         |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled.    |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled.       |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled.       |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled.     |
### BaseLoggingService
Represents the base class for implementing custom logging services.
```csharp
public abstract class BaseLoggingService<TOptions> : BaseDisposable, ILoggingService where TOptions : BaseLoggingServiceOptions
```
#### Constructors
| Name                                 | Description                          |
|:-------------------------------------|:-------------------------------------|
| BaseLoggingService(IOptions, String) | Initializes a new instance of class. |
#### Properties
| Name     | Data Type | Description                     |
|:---------|:----------|:--------------------------------|
| Category | String    | Gets the name of the category.  |
| Options  | TOptions  | Gets the configuration options. |
### BaseLoggingServiceOptions
Represents the base options for configuring a logging service.
```csharp
public abstract class BaseLoggingServiceOptions : BaseCloneable
```
#### Properties
| Name        | Data Type | Description                                                                   |
|:------------|:----------|:------------------------------------------------------------------------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging provider.         |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled.    |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled.       |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled.       |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled.     |
## Messaging
### BaseMessage
Represents the base class for implementing custom messages.
```csharp
public class BaseMessage : BaseCloneable, IMessage
```
#### Properties
| Name        | Data Type | Description                                                              |
|:------------|:----------|:-------------------------------------------------------------------------|
| Id          | Guid      | Gets or sets the unique identifier of the message.                       |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related message. |
| Timestamp   | DateTime  | Gets or sets the timestamp when the message was published.               |
## BasePublisherService
Represents the base class for implementing custom publishers for broadcasting messages to subscribers.
```csharp
public abstract class BasePublisherService<TMessage, TOptions> : BaseDisposable, IPublisherService<TMessage> where TMessage : BaseMessage
                                                                                                             where TOptions : BasePublisherServiceOptions
```
#### Constructors
| Name                           | Description                          |
|:-------------------------------|:-------------------------------------|
| BasePublisherService(IOptions) | Initializes a new instance of class. |
### BasePublisherServiceOptions
Represents the base options for configuring a message publisher.
```csharp
public abstract class BasePublisherServiceOptions : BaseCloneable
```
### BaseSubscriberService
Represents a base class for implementing custom subscribers for receiving messages from a publisher.
```csharp
public abstract class BaseSubscriberService<TMessage, TOptions> : BaseDisposable, ISubscriberService<TMessage> where TMessage : BaseMessage
                                                                                                               where TOptions : BaseSubscriberServiceOptions
```
#### Constructors
| Name                            | Description                          |
|:--------------------------------|:-------------------------------------|
| BaseSubscriberService(IOptions) | Initializes a new instance of class. |
### BaseSubscriberServiceOptions
Represents the base options for configuring a message subscriber.
```csharp
public abstract class BaseSubscriberServiceOptions : BaseCloneable
```
## Telemetry
### BaseTelemetryService
Represents a base class for implementing custom telemetry services.
```csharp
public abstract class BaseTelemetryService<TOptions> : BaseDisposable, ITelemetryService where TOptions : BaseTelemetryServiceOptions
```
#### Constructors
| Name                           | Description                          |
|:-------------------------------|:-------------------------------------|
| BaseTelemetryService(IOptions) | Initializes a new instance of class. |
#### Properties
| Name          | Data Type      | Description                                              |
|:--------------|:---------------|:---------------------------------------------------------|
| Logger        | ILogger        | Gets the instance of the logger.                         |
| LoggerFactory | ILoggerFactory | Gets or sets the factory to create instances of loggers. |
| Options       | TOptions       | Gets the options for the telemetry service.              |
### BaseTelemetryServiceOptions
Represents the base options for configuring a telemetry service.
```csharp
public abstract class BaseTelemetryServiceOptions : BaseCloneable
```
#### Properties
| Name  | Data Type | Description                                                            |
|:------|:----------|:-----------------------------------------------------------------------|
| AppId | Guid      | Gets or sets the application id associated with the telemetry service. |
### Dependency
Represents the results for a dependency call.
```csharp
public class Dependency : BaseCloneable, IDependency
```
#### Properties
| Name        | Data Type | Description                                                                                  |
|:------------|:----------|:---------------------------------------------------------------------------------------------|
| Duration    | TimeSpan  | Gets or sets the duration of the dependency call.                                            |
| InputData   | String    | Gets or sets the command or data sent to the dependency call.                                |
| Name        | String    | Gets or sets the name of the dependency call.                                                |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related dependency call information. |
| OutputData  | String    | Gets or sets the output data returned by the dependency call.                                |
| Success     | Boolean   | Gets or sets a value indicating whether the dependency call was successful.                  |
| Target      | String    | Gets or sets the target system or endpoint of the dependency call.                           |
| Type        | String    | Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).                        |
### Event
Represents the data for an event.
```csharp
public class Event : BaseCloneable, IEvent
```
#### Properties
| Name        | Data Type | Description                                                                        |
|:------------|:----------|:-----------------------------------------------------------------------------------|
| Name        | String    | Gets or sets the name of the event.                                                |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related event information. |
### Metric
Represents the data for a metric.
```csharp
public class Metric : BaseCloneable, IMetric
```
#### Properties
| Name        | Data Type | Description                                                                         |
|:------------|:----------|:------------------------------------------------------------------------------------|
| Dimension   | String    | Gets or sets the dimension or category associated with the metric.                  |
| Name        | String    | Gets or sets the name of the metric.                                                |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related metric information. |
| Value       | Double    | Gets or sets the value of the metric.                                               |
### PageView
Represents the data for a page view.
```csharp
public class PageView : BaseCloneable, IPageView
```
#### Properties
| Name        | Data Type | Description                                                                            |
|:------------|:----------|:---------------------------------------------------------------------------------------|
| Duration    | TimeSpan  | Gets or sets the duration of the page view.                                            |
| Name        | String    | Gets or sets the name of the page viewed.                                              |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related page view information. |
| Uri         | Uri       | Gets or sets the URI of the page viewed.                                               |
### Request
Represents the data for an HTTP request.
```csharp
public class Request : BaseCloneable, IRequest
```
#### Properties
| Name         | Data Type      | Description                                                                               |
|:-------------|:---------------|:------------------------------------------------------------------------------------------|
| Duration     | TimeSpan       | Gets or sets the duration of the HTTP request.                                            |
| IpAddress    | IPAddress      | Gets or sets the IP address from which the HTTP request originated.                       |
| Name         | String         | Gets or sets the name or identifier of the HTTP request.                                  |
| OperationId  | Guid           | Gets or sets the operation identifier used to associate related HTTP request information. |
| ResponseCode | HttpStatusCode | Gets or sets the HTTP response code returned for the HTTP request.                        |
| Success      | Boolean        | Gets or sets a value indicating whether the HTTP request was successful.                  |
| Timestamp    | DateTime       | Gets or sets the timestamp indicating when the HTTP request is tracked.                   |
| Uri          | Uri            | Gets or sets the URI associated with the HTTP request.                                    |
### Test
Represents the result of a test.
```csharp
public class Test : BaseCloneable, ITest
```
#### Properties
| Name        | Data Type | Description                                                                       |
|:------------|:----------|:----------------------------------------------------------------------------------|
| Duration    | TimeSpan  | Gets or sets the duration of the test.                                            |
| Message     | String    | Gets or sets the message associated with the test result.                         |
| Name        | String    | Gets or sets the name of the test.                                                |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related test information. |
| Success     | Boolean   | Gets or sets a value indicating whether the test was successful.                  |
## Web
### BaseActionFilterAttribute
Represents a base class for implementing custom action filters in ASP.NET Core MVC.
```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class BaseActionFilterAttribute : Attribute, IActionFilter
```
#### Methods
| Name                                      | Description                                  |
|:------------------------------------------|:---------------------------------------------|
| OnActionExecuted(ActionExecutedContext)   | Called after the action method is executed.  |
| OnActionExecuting(ActionExecutingContext) | Called before the action method is executed. |
### BaseApiController
Represents a base class for implementing custom api controllers in ASP.NET Core MVC.
```csharp
[ApiController]
public abstract class BaseApiController : ControllerBase, IDisposable
```
### BaseAuthorizationFilterAttribute
Represents a base class for implementing custom authorization filters in ASP.NET Core MVC.
```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
```
#### Methods
| Name                                        | Description                                                                                   |
|:--------------------------------------------|:----------------------------------------------------------------------------------------------|
| OnAuthorization(AuthorizationFilterContext) | Called during the authorization phase to determine whether the current request is authorized. |
### BaseMiddleware
Represents a base class for implementing custom middlewares in ASP.NET Core MVC.
```csharp
public abstract class BaseMiddleware : BaseDisposable
```
#### Constructors
| Name                     | Description                         |
|:-------------------------|:------------------------------------|
| BaseMiddleware(Delegate) | Initialize a new instance of class. |
#### Methods
| Name                     | Description                                      |
|:-------------------------|:-------------------------------------------------|
| InvokeAsync(HttpContext) | Execute the logic implemented by the middleware. |
### BaseWebController
Represents a base class for implementing custom web controllers in ASP.NET Core MVC.
```csharp
public abstract class BaseWebController : Controller, IDisposable
```
### BaseTelemetryService
Represents a base class for implementing custom telemetry services.
```csharp
public abstract class BaseTelemetryService<TOptions> : BaseDisposable, ITelemetryService where TOptions : BaseTelemetryServiceOptions
```
#### Constructors
| Name                           | Description                         |
|:-------------------------------|:------------------------------------|
| BaseTelemetryService(IOptions) | Initialize a new instance of class. |
#### Properties
| Name          | Data Type      | Description                                              |
|:--------------|:---------------|:---------------------------------------------------------|
| Logger        | ILogger        | Gets the instance of the logger.                         |
| LoggerFactory | ILoggerFactory | Gets or sets the factory to create instances of loggers. |
| Options       | TOptions       | Gets the options for the telemetry service.              |
### BaseTelemetryServiceOptions
Represents the base options for configuring a telemetry service.
```csharp
public abstract class BaseTelemetryServiceOptions : BaseCloneable
```
#### Properties
| Name  | Data Type | Description                                                            |
|:------|:----------|:-----------------------------------------------------------------------|
| AppId | Guid      | Gets or sets the application id associated with the telemetry service. |
## Feedback & Contributing
Zetatech.Accelerate.Abstractions is released as open source under the [GNU General Public License](../License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
**v8.0.0**  
- Includes the base classes for application layer components.
- Includes the base classes for caching components.
- Includes the base classes for data access components.
- Includes the base classes for logging components.
- Includes the base classes for messaging components.
- Includes the base classes for telemetry components.
- Includes the base classes for web components.

```
Zeta Technologies
```