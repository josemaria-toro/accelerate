# Zetatech.Accelerate.Web
## Table of contents
- [Overview](#overview)
- [Web](#web)
  - [CorrelationMiddleware](#correlationmiddleware)
  - [ErrorHandlerMiddleware](#errorhandlermiddleware)
  - [TrackRequestMiddleware](#trackrequestmiddleware)
- [Extensions](#extensions)
  - [IApplicationBuilderExtensions](#iapplicationbuilderextensions)
  - [IEndpointRouteBuilderExtensions](#iendpointroutebuilderextensions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides custom components to be used in ASP.NET Core projects.  
## Web
### CorrelationMiddleware
Represents an implementation for a custom middleware to initialize the correlation data in the http context.  
**Assembly:** Zetatech.Accelerate.Web.dll  
**Namespace**: Zetatech.Accelerate.Web.Middlewares  
```csharp
public class CorrelationMiddleware : BaseMiddleware
```
#### Constructors
| Name                                   | Description                              |
|:---------------------------------------|:-----------------------------------------|
| CorrelationMiddleware(RequestDelegate) | Initializes a new instance of the class. |
#### Methods
| Name                     | Description                                      |
|:-------------------------|:-------------------------------------------------|
| InvokeAsync(HttpContext) | Execute the logic implemented by the middleware. |
#### Examples
```csharp
app.UseMiddleware<CorrelationMiddleware>();
```
### ErrorHandlerMiddleware
Represents an implementation for a custom middleware to handle errors in the application.  
**Assembly:** Zetatech.Accelerate.Web.dll  
**Namespace**: Zetatech.Accelerate.Web.Middlewares  
```csharp
public class ErrorHandlerMiddleware : BaseMiddleware
```
#### Constructors
| Name                                    | Description                              |
|:----------------------------------------|:-----------------------------------------|
| ErrorHandlerMiddleware(RequestDelegate) | Initializes a new instance of the class. |
#### Methods
| Name                     | Description                                      |
|:-------------------------|:-------------------------------------------------|
| InvokeAsync(HttpContext) | Execute the logic implemented by the middleware. |
#### Examples
```csharp
app.UseMiddleware<ErrorHandlerMiddleware>();
```
### TrackRequestMiddleware
Represents an implementation for a custom middleware to track http requests in the telemetry service.  
**Assembly:** Zetatech.Accelerate.Web.dll  
**Namespace**: Zetatech.Accelerate.Web.Middlewares  
```csharp
public class TrackRequestMiddleware : BaseMiddleware
```
#### Constructors
| Name                                    | Description                              |
|:----------------------------------------|:-----------------------------------------|
| TrackRequestMiddleware(RequestDelegate) | Initializes a new instance of the class. |
#### Methods
| Name                     | Description                                      |
|:-------------------------|:-------------------------------------------------|
| InvokeAsync(HttpContext) | Execute the logic implemented by the middleware. |
### Example
```csharp
app.UseMiddleware<TrackRequestMiddleware>();
```
## Extensions
### IApplicationBuilderExtensions
Extensions methods for the `IApplicationBuilder` interface.  
**Assembly:** Zetatech.Accelerate.Web.dll  
**Namespace**: Zetatech.Accelerate.Web.Extensions  
```csharp
public static class IApplicationBuilderExtensions
```
#### Methods
| Name                                      | Description                                                                   |
|:------------------------------------------|:------------------------------------------------------------------------------|
| ConfigureMiddlewares(IApplicationBuilder) | Adds and configure the available middlewares in the ASP.NET request pipeline. |
### IEndpointRouteBuilderExtensions
Extensions methods for the `IEndpointRouteBuilder` interface.
**Assembly:** Zetatech.Accelerate.Web.dll  
**Namespace**: Zetatech.Accelerate.Web.Extensions  
```csharp
public static class IEndpointRouteBuilderExtensions
```
#### Methods
| Name                                        | Description                                                                         |
|:--------------------------------------------|:------------------------------------------------------------------------------------|
| ConfigureControllers(IEndpointRouteBuilder) | Adds and configure the available controllers in the ASP.NET endpoint route builder. |
## Feedback & Contributing
Zetatech.Accelerate.Web is released as open source under the [GNU General Public License](./License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
**v8.0.0**  
- Includes middlewares for initializes correlation data, handling errors and track requests in the telemetry system.
- Includes extensions methods for configuring middlewares and controllers in the dependency injection container.

```
Zeta Technologies
```