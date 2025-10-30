# Zetatech.Accelerate.Configuration
## Table of contents
- [Overview](#overview)
- [Extensions](#configuration)
  - [IServiceCollectionExtensions](#iservicecollectionextensions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for configuration capabilities.  
## Extensions
### IServiceCollectionExtensions
Provides extension methods for the `IServiceCollection` interface to manage application configuration services.  
**Assembly:** Zetatech.Accelerate.Configuration.dll  
**Namespace**: Zetatech.Accelerate.Configuration.Extensions  
```csharp
public static class IServiceCollectionExtensions
```
#### Methods
| Name                   | Description                                                                                                         |
|:-----------------------|:--------------------------------------------------------------------------------------------------------------------|
| AddConfigService()     | Adds and configures the service to manage the application configurations.                                           |
| AddConfigService(Guid) | Adds and configures the service to manage the application configurations, using a specific user secrets identifier. |
## Feedback & Contributing
Zetatech.Accelerate.Configuration is released as open source under the [GNU General Public License](../License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes the extension methods to add the configuration service into dependency injection container.

```
Zeta Technologies
```