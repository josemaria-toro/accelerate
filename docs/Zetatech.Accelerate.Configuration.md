## Zetatech.Accelerate.Configuration

Table of Contents

- [Zetatech.Accelerate.Configuration.Extensions.IServiceCollectionExtensions](#zetatechaccelerateconfigurationextensionsiservicecollectionextensions)
  - [Description](#description)
  - [Signature](#signature)
  - [Constructors](#constructors)
  - [Properties](#properties)
  - [Methods](#methods)
  - [Examples](#examples)

---

### Zetatech.Accelerate.Configuration.Extensions.IServiceCollectionExtensions

Description

Extensions methods for the `IServiceCollection` interface.

Signature

```csharp
public static class IServiceCollectionExtensions
```

Constructors

| name | description |
|---|---|
| No public constructors | The type is a static class and has no public constructors. |

Properties

| name | type | description |
|---|---:|---|
| No public properties | - | This type declares no public properties. |

Methods

| name | description |
|---|---|
| AddConfigService(this IServiceCollection serviceCollection) | Adds and configure the service to manage the application configurations. The `serviceCollection` parameter is a collection of service descriptors. |
| AddConfigService(this IServiceCollection serviceCollection, Guid userSecretsId) | Adds and configure the service to manage the application configurations. The `serviceCollection` parameter is a collection of service descriptors. The `userSecretsId` parameter is a unique identifier of user secrets. |

Notes on method parameter names: parameter types are shown without namespaces per documentation rules.

Examples

1) Example: Registering the configuration service in an application startup using the parameterless overload

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Adds and configures the IConfiguration instance by loading environment variables, appsettings.json
// and JSON files found under a `Configuration` folder; it will also add user secrets discovered via
// `UserSecretsIdAttribute` on loaded assemblies when available.
services.AddConfigService();

var serviceProvider = services.BuildServiceProvider();
var configuration = serviceProvider.GetRequiredService<IConfiguration>();
```

2) Example: Registering the configuration service specifying a user secrets id

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var userSecretsId = new Guid("00000000-0000-0000-0000-000000000000");
services.AddConfigService(userSecretsId);

var serviceProvider = services.BuildServiceProvider();
var configuration = serviceProvider.GetRequiredService<IConfiguration>();
```

Extending abstract classes

There are no abstract classes declared in the `Zetatech.Accelerate.Configuration` project to extend.

---

File reference: `Zetatech.Accelerate.Configuration/Extensions/IServiceCollectionExtensions.cs`
