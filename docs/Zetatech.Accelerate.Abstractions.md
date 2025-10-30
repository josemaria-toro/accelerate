# Zetatech.Accelerate.Abstractions

## Table of Contents

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

---

## BaseCloneable

Represents a base class for implementing custom cloneable objects.

**Signature:**
```csharp
[Serializable]
public abstract class BaseCloneable : ICloneable
```

### Methods
| Name   | Description |
|--------|-------------|
| Clone  | Creates a shallow copy of the current object instance. |

#### Example: Extending BaseCloneable
```csharp
public class MyCloneable : BaseCloneable {
    public int Value { get; set; }
}
var original = new MyCloneable { Value = 42 };
var copy = (MyCloneable)original.Clone();
```

---

## BaseDisposable

Represents a base class for implementing custom disposable objects.

**Signature:**
```csharp
public abstract class BaseDisposable : IDisposable
```

### Methods
| Name    | Description |
|---------|-------------|
| Dispose | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Dispose | (protected virtual) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |

#### Example: Extending BaseDisposable
```csharp
public class MyDisposable : BaseDisposable {
    protected override void Dispose(bool disposing) {
        // Custom cleanup logic
        base.Dispose(disposing);
    }
}
```

---

## Application

### BaseApplicationService

Represents the base class for implementing custom application services.

**Signature:**
```csharp
public abstract class BaseApplicationService : BaseDisposable, IApplicationService
```

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |

### Methods
| Name    | Description |
|---------|-------------|
| Dispose | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |

#### Example: Extending BaseApplicationService
```csharp
public class MyService : BaseApplicationService {
    // Implement service logic
}
```

### BaseDataTransferObject

Represents the base class for implementing custom data transfer objects.

**Signature:**
```csharp
[JsonSourceGenerationOptions(...)]
public abstract class BaseDataTransferObject : BaseCloneable, IDataTransferObject
```

#### Example: Extending BaseDataTransferObject
```csharp
public class MyDto : BaseDataTransferObject {
    public string Name { get; set; }
}
```

---

## Caching

### BaseCacheService

Represents the base class for implementing custom cache services.

**Signature:**
```csharp
public abstract class BaseCacheService<TOptions> : BaseDisposable, ICacheService where TOptions : BaseCacheServiceOptions
```

### Constructors
| Name                | Description |
|---------------------|-------------|
| BaseCacheService    | The configuration options for the cache service. |

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |
| Options      | TOptions          | Gets the options for the messages publisher. |

### Methods
| Name    | Description |
|---------|-------------|
| Add     | Adds a value to the cache with the specified key. |
| Add     | Adds a value to the cache with the specified key and expiration date. |
| Clear   | Removes all entries from the cache. |
| Contains| Determines whether the cache contains a value with the specified key. |
| Dispose | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Get     | Retrieves the value associated with the specified key from the cache. |
| Remove  | Removes the value with the specified key from the cache. |

#### Example: Extending BaseCacheService
```csharp
public class MyCacheService : BaseCacheService<MyCacheOptions> {
    public override void Add<T>(string key, T value) { /* ... */ }
    public override void Add<T>(string key, T value, DateTime expiredAt) { /* ... */ }
    public override void Clear() { /* ... */ }
    public override bool Contains(string key) { /* ... */ }
    public override T Get<T>(string key) { /* ... */ }
    public override void Remove(string key) { /* ... */ }
}
```

### BaseCacheServiceOptions

Represents the base options for configuring a cache service.

**Signature:**
```csharp
public abstract class BaseCacheServiceOptions : BaseCloneable
```

### Properties
| Name                 | Data Type | Description |
|----------------------|-----------|-------------|
| DefaultExpirationTime| Int32     | Gets or sets the default expiration time (in minutes) for cache entries. |
| MaxSize              | Int32     | Gets or sets the maximum number of entries allowed in the cache. |

#### Example: Extending BaseCacheServiceOptions
```csharp
public class MyCacheOptions : BaseCacheServiceOptions {
    public MyCacheOptions() {
        DefaultExpirationTime = 60;
        MaxSize = 1000;
    }
}
```

---

## Data

### BaseEntity

Represents the base class for implementing custom entities.

**Signature:**
```csharp
[JsonSourceGenerationOptions(...)]
public abstract class BaseEntity : BaseCloneable, IEntity
```

#### Example: Extending BaseEntity
```csharp
public class User : BaseEntity {
    public string Username { get; set; }
}
```

### BaseRepository

Represents the base class for implementing custom repositories.

**Signature:**
```csharp
public abstract class BaseRepository<TEntity, TOptions, TContext> : BaseDisposable, IRepository<TEntity>
    where TEntity : BaseEntity
    where TOptions : BaseRepositoryOptions
    where TContext : BaseRepositoryContext<TEntity, TOptions>
```

### Constructors
| Name            | Description |
|-----------------|-------------|
| BaseRepository  | The repository options to be used. |

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Entities     | DbSet<TEntity>    | Gets the set of entities managed by this repository. |
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |
| Options      | TOptions          | Gets the configuration options of this repository. |

### Methods
| Name     | Description |
|----------|-------------|
| Commit   | Commits all pending changes to the data store. |
| Delete   | Deletes the specified entity from the data store. |
| Delete   | Deletes the specified collection of entities from the data store. |
| Delete   | Deletes entities that match the specified expression from the data store. |
| Dispose  | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| First    | Returns the first entity in the data store. |
| First    | Returns the first entity that matches the specified expression. |
| First    | Returns the first entity that matches the specified expression, skipping a given number of entities. |
| Insert   | Inserts the specified entity into the data store. |
| Insert   | Inserts the specified collection of entities into the data store. |
| OnModelCreating | Configures the model that was discovered by convention from the entity types exposed in DbSet<TEntity> properties on your derived context. |
| Rollback | Rolls back all pending changes that have not been committed. |
| Select   | Returns all entities from the data store. |
| Select   | Returns all entities that match the specified expression. |
| Select   | Returns entities that match the specified expression, skipping a given number of entities. |
| Select   | Returns entities that match the specified expression, skipping and taking a given number of entities. |
| Select   | Execute a custom query string to select entities. |
| Update   | Updates the specified entity in the data store. |
| Update   | Updates the specified collection of entities in the data store. |

#### Example: Extending BaseRepository
```csharp
public class UserRepository : BaseRepository<User, UserRepositoryOptions, UserRepositoryContext> {
    // Implement repository logic
}
```

### BaseRepositoryContext

Represents the base class for implementing custom repository context based on Entity Framework.

**Signature:**
```csharp
public abstract class BaseRepositoryContext<TEntity, TOptions> : DbContext
    where TEntity : BaseEntity
    where TOptions : BaseRepositoryOptions
```

### Constructors
| Name                  | Description |
|-----------------------|-------------|
| BaseRepositoryContext | The repository associated with this context. The configuration options for this repository context. |

### Properties
| Name   | Data Type | Description |
|--------|-----------|-------------|
| Options| TOptions  | Gets the configuration options of this repository context. |

### Methods
| Name         | Description |
|--------------|-------------|
| Dispose      | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Dispose      | (protected virtual) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| OnConfiguring| Configures the database and other options for this context. |
| OnModelCreating | Configures the model that was discovered by convention from the entity types exposed in DbSet<TEntity> properties on your derived context. |

#### Example: Extending BaseRepositoryContext
```csharp
public class UserRepositoryContext : BaseRepositoryContext<User, UserRepositoryOptions> {
    public UserRepositoryContext(IRepository<User> repo, UserRepositoryOptions options) : base(repo, options) {}
}
```

### BaseRepositoryOptions

Represents the base class for implementing custom repository options.

**Signature:**
```csharp
public abstract class BaseRepositoryOptions : BaseCloneable
```

### Properties
| Name            | Data Type | Description |
|-----------------|-----------|-------------|
| ConnectionString| String    | Gets or sets the connection string used to connect with the data source. |
| DetailedErrors  | Boolean   | Gets or sets a value indicating whether detailed error messages are enabled for the repository. |
| LazyLoading     | Boolean   | Gets or sets a value indicating whether lazy loading is enabled for related entities. |
| Schema          | String    | Gets or sets the schema where database is stored. |
| SensitiveDataLogging | Boolean | Gets or sets a value indicating whether sensitive data logging is enabled for the repository. |
| Timeout         | Int32     | Gets or sets the timeout value, in seconds, for repository operations. |
| TrackChanges    | Boolean   | Gets or sets a value indicating whether change tracking is enabled for entities. |

#### Example: Extending BaseRepositoryOptions
```csharp
public class UserRepositoryOptions : BaseRepositoryOptions {
    public UserRepositoryOptions() {
        ConnectionString = "...";
        TrackChanges = true;
    }
}
```

### BaseSpecification

Represents the base class for implementing custom specifications for evaluating whether an entity of type TEntity satisfies certain criteria.

**Signature:**
```csharp
public abstract class BaseSpecification<TEntity> : BaseDisposable where TEntity : IEntity
```

### Methods
| Name         | Description |
|--------------|-------------|
| IsSatisfiedBy| Determines whether the specified entity satisfies the specification criteria. |

#### Example: Extending BaseSpecification
```csharp
public class ActiveUserSpecification : BaseSpecification<User> {
    public override bool IsSatisfiedBy(User entity) => entity.IsActive;
}
```

---

## Logging

### BaseLoggingFactory

Represents the base class for implementing custom logging factories.

**Signature:**
```csharp
public abstract class BaseLoggingFactory<TOptions> : BaseDisposable, ILoggingFactory where TOptions : BaseLoggingFactoryOptions
```

### Constructors
| Name                | Description |
|---------------------|-------------|
| BaseLoggingFactory  | The logger options to be used by the provider. |

### Properties
| Name     | Data Type                  | Description |
|----------|---------------------------|-------------|
| Options  | TOptions                   | Gets the configuration options. |
| Providers| List<ILoggerProvider>      | Gets the logging providers added to the factory. |

### Methods
| Name        | Description |
|-------------|-------------|
| AddProvider | Adds an instance of logging provider to the logging system. |
| CreateLogger| Creates a new logger instance for the specified category. |
| Dispose     | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |

#### Example: Extending BaseLoggingFactory
```csharp
public class MyLoggingFactory : BaseLoggingFactory<MyLoggingFactoryOptions> {
    public override ILogger CreateLogger(string categoryName) { /* ... */ }
}
```

### BaseLoggingFactoryOptions

Represents the base options for configuring a logging factory.

**Signature:**
```csharp
public abstract class BaseLoggingFactoryOptions : BaseCloneable
```

### Properties
| Name        | Data Type | Description |
|-------------|-----------|-------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging provider. |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled. |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled. |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled. |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled. |

#### Example: Extending BaseLoggingFactoryOptions
```csharp
public class MyLoggingFactoryOptions : BaseLoggingFactoryOptions {
    public MyLoggingFactoryOptions() {
        AppId = Guid.NewGuid();
        Debug = true;
    }
}
```

### BaseLoggingProvider

Represents the base class for implementing custom logging providers.

**Signature:**
```csharp
public abstract class BaseLoggingProvider<TOptions> : BaseDisposable, ILoggingProvider where TOptions : BaseLoggingProviderOptions
```

### Constructors
| Name                | Description |
|---------------------|-------------|
| BaseLoggingProvider | The logger options to be used by the provider. |

### Properties
| Name     | Data Type                        | Description |
|----------|----------------------------------|-------------|
| Loggers  | ConcurrentDictionary<string,ILogger>| Gets the loggers already created. |
| Options  | TOptions                         | Gets the configuration options. |

### Methods
| Name        | Description |
|-------------|-------------|
| CreateLogger| Creates a new logger instance for the specified category. |
| Dispose     | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |

#### Example: Extending BaseLoggingProvider
```csharp
public class MyLoggingProvider : BaseLoggingProvider<MyLoggingProviderOptions> {
    public override ILogger CreateLogger(string categoryName) { /* ... */ }
}
```

### BaseLoggingProviderOptions

Represents the base options for configuring a logging provider.

**Signature:**
```csharp
public abstract class BaseLoggingProviderOptions : BaseCloneable
```

### Properties
| Name        | Data Type | Description |
|-------------|-----------|-------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging provider. |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled. |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled. |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled. |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled. |

#### Example: Extending BaseLoggingProviderOptions
```csharp
public class MyLoggingProviderOptions : BaseLoggingProviderOptions {
    public MyLoggingProviderOptions() {
        AppId = Guid.NewGuid();
        Error = true;
    }
}
```

### BaseLoggingService

Represents the base class for implementing custom logging services.

**Signature:**
```csharp
public abstract class BaseLoggingService<TOptions> : BaseDisposable, ILoggingService where TOptions : BaseLoggingServiceOptions
```

### Constructors
| Name                | Description |
|---------------------|-------------|
| BaseLoggingService  | The configuration options for the logging service. The category name for the logger. |

### Properties
| Name     | Data Type | Description |
|----------|-----------|-------------|
| Category | String    | Gets the name of the category. |
| Options  | TOptions  | Gets the configuration options. |
| Platform | String    | Gets the name of the device platform. |

### Methods
| Name     | Description |
|----------|-------------|
| BeginScope| Begins a logical operation scope. |
| Dispose  | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| IsEnabled| Determines whether the logger is enabled for the specified log level. |
| Log      | Writes a log entry. |

#### Example: Extending BaseLoggingService
```csharp
public class MyLoggingService : BaseLoggingService<MyLoggingServiceOptions> {
    public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        // Custom log logic
    }
}
```

### BaseLoggingServiceOptions

Represents the base options for configuring a logging service.

**Signature:**
```csharp
public abstract class BaseLoggingServiceOptions : BaseCloneable
```

### Properties
| Name        | Data Type | Description |
|-------------|-----------|-------------|
| AppId       | Guid      | Gets or sets the application id associated with the logging service. |
| Critical    | Boolean   | Gets or sets a value indicating whether critical level logging is enabled. |
| Debug       | Boolean   | Gets or sets a value indicating whether debug level logging is enabled. |
| Error       | Boolean   | Gets or sets a value indicating whether error level logging is enabled. |
| Information | Boolean   | Gets or sets a value indicating whether information level logging is enabled. |
| Warning     | Boolean   | Gets or sets a value indicating whether warning level logging is enabled. |

#### Example: Extending BaseLoggingServiceOptions
```csharp
public class MyLoggingServiceOptions : BaseLoggingServiceOptions {
    public MyLoggingServiceOptions() {
        AppId = Guid.NewGuid();
        Information = true;
    }
}
```

---

## Messaging

### BaseMessage

Represents the base class for implementing custom messages.

**Signature:**
```csharp
[JsonSourceGenerationOptions(...)]
public class BaseMessage : BaseCloneable, IMessage
```

### Properties
| Name        | Data Type | Description |
|-------------|-----------|-------------|
| Id          | Guid      | Gets or sets the unique identifier of the message. |
| OperationId | Guid      | Gets or sets the operation identifier used to associate related message. |
| Timestamp   | DateTime  | Gets or sets the timestamp when the message was published. |

#### Example: Using BaseMessage
```csharp
var msg = new BaseMessage {
    Id = Guid.NewGuid(),
    OperationId = Guid.NewGuid(),
    Timestamp = DateTime.UtcNow
};
```

### BasePublisherService

Represents the base class for implementing custom publishers for broadcasting messages to subscribers.

**Signature:**
```csharp
public abstract class BasePublisherService<TMessage, TOptions> : BaseDisposable, IPublisherService<TMessage>
    where TMessage : BaseMessage
    where TOptions : BasePublisherServiceOptions
```

### Constructors
| Name                  | Description |
|-----------------------|-------------|
| BasePublisherService  | The configuration options for the messages publisher. |

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |
| Options      | TOptions          | Gets the options for the messages publisher. |

### Methods
| Name      | Description |
|-----------|-------------|
| Dispose   | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Publish   | Publishes a message to all subscribed instances. |
| Subscribe | Subscribes the specified subscriber to receive published messages. |
| Unsubscribe| Unsubscribes the specified subscriber from receiving published messages. |

#### Example: Extending BasePublisherService
```csharp
public class MyPublisher : BasePublisherService<BaseMessage, MyPublisherOptions> {
    // Implement publish logic
}
```

### BasePublisherServiceOptions

Represents the base options for configuring a message publisher.

**Signature:**
```csharp
public abstract class BasePublisherServiceOptions : BaseCloneable
```

#### Example: Extending BasePublisherServiceOptions
```csharp
public class MyPublisherOptions : BasePublisherServiceOptions {
    // Custom options
}
```

### BaseSubscriberService

Represents a base class for implementing custom subscribers for receiving messages from a publisher.

**Signature:**
```csharp
public abstract class BaseSubscriberService<TMessage, TOptions> : BaseDisposable, ISubscriberService<TMessage>
    where TMessage : BaseMessage
    where TOptions : BaseSubscriberServiceOptions
```

### Constructors
| Name                  | Description |
|-----------------------|-------------|
| BaseSubscriberService | The configuration options for the messages subscriber. |

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |
| Options      | TOptions          | Gets the options for the messages subscriber. |

### Methods
| Name    | Description |
|---------|-------------|
| Dispose | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Receive | Receives a message from the publisher. |

#### Example: Extending BaseSubscriberService
```csharp
public class MySubscriber : BaseSubscriberService<BaseMessage, MySubscriberOptions> {
    public override void Receive(BaseMessage message) {
        // Handle received message
    }
}
```

### BaseSubscriberServiceOptions

Represents the base options for configuring a message subscriber.

**Signature:**
```csharp
public abstract class BaseSubscriberServiceOptions : BaseCloneable
```

#### Example: Extending BaseSubscriberServiceOptions
```csharp
public class MySubscriberOptions : BaseSubscriberServiceOptions {
    // Custom options
}
```

---

## Telemetry

### BaseTelemetryService

Represents a base class for implementing custom telemetry services.

**Signature:**
```csharp
public abstract class BaseTelemetryService<TOptions> : BaseDisposable, ITelemetryService where TOptions : BaseTelemetryServiceOptions
```

### Constructors
| Name                  | Description |
|-----------------------|-------------|
| BaseTelemetryService  | The configuration options for the telemetry service. |

### Properties
| Name         | Data Type         | Description |
|--------------|------------------|-------------|
| Logger       | ILogger           | Gets the instance of the logger. |
| LoggerFactory| ILoggerFactory    | Gets or sets the factory to create instances of loggers. |
| Options      | TOptions          | Gets the options for the telemetry service. |

### Methods
| Name    | Description |
|---------|-------------|
| Dispose | (protected override) Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. |
| Track   | Tracks the specified dependency information. |
| Track   | Tracks the specified event information. |
| Track   | Tracks the specified metric information. |
| Track   | Tracks the specified page view information. |
| Track   | Tracks the specified HTTP request information. |
| Track   | Tracks the specified test information. |

#### Example: Extending BaseTelemetryService
```csharp
public class MyTelemetryService : BaseTelemetryService<MyTelemetryOptions> {
    public override void Track(IDependency dependency) { /* ... */ }
    public override void Track(IEvent @event) { /* ... */ }
    public override void Track(IMetric metric) { /* ... */ }
    public override void Track(IPageView pageView) { /* ... */ }
    public override void Track(IRequest request) { /* ... */ }
    public override void Track(ITest test) { /* ... */ }
}
```

### BaseTelemetryServiceOptions

Represents the base options for configuring a telemetry service.

**Signature:**
```csharp
public abstract class BaseTelemetryServiceOptions : BaseCloneable
```

### Properties
| Name  | Data Type | Description |
|-------|-----------|-------------|
| AppId | Guid      | Gets or sets the application id associated with the telemetry service. |

#### Example: Extending BaseTelemetryServiceOptions
```csharp
public class MyTelemetryOptions : BaseTelemetryServiceOptions {
    public MyTelemetryOptions() {
        AppId = Guid.NewGuid();
    }
}
```

---

*Documentation generated from source code comments as of 2025-10-29.*
