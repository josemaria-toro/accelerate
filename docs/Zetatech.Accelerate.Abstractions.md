# Zetatech.Accelerate.Abstractions Documentation

This document provides an overview of the public, non-abstract classes in the `Zetatech.Accelerate.Abstractions` project, including their constructors, public properties, and public methods. It also includes examples for dependency injection and usage.

---

## Classes

### Test
Represents the result of a test.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `TimeSpan Duration`: Gets or sets the duration of the test.
- `string Message`: Gets or sets the message associated with the test result.
- `string Name`: Gets or sets the name of the test.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related test information.
- `bool Success`: Gets or sets a value indicating whether the test was successful.

---

### Request
Represents the data for an HTTP request.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `TimeSpan Duration`: Gets or sets the duration of the HTTP request.
- `IPAddress IpAddress`: Gets or sets the IP address from which the HTTP request originated.
- `string Name`: Gets or sets the name or identifier of the HTTP request.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related HTTP request information.
- `HttpStatusCode ResponseCode`: Gets or sets the HTTP response code returned for the HTTP request.
- `bool Success`: Gets or sets a value indicating whether the HTTP request was successful.
- `DateTime Timestamp`: Gets or sets the timestamp indicating when the HTTP request is tracked.
- `Uri Uri`: Gets or sets the URI associated with the HTTP request.

---

### Metric
Represents the data for a metric.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `string Dimension`: Gets or sets the dimension or category associated with the metric.
- `string Name`: Gets or sets the name of the metric.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related metric information.
- `double Value`: Gets or sets the value of the metric.

---

### Event
Represents the data for an event.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `string Name`: Gets or sets the name of the event.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related event information.

---

### PageView
Represents the data for a page view.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `TimeSpan Duration`: Gets or sets the duration of the page view.
- `string Name`: Gets or sets the name of the page viewed.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related page view information.
- `Uri Uri`: Gets or sets the URI of the page viewed.

---

### Dependency
Represents the results for a dependency call.

**Namespace:** `Zetatech.Accelerate.Telemetry`

#### Properties
- `TimeSpan Duration`: Gets or sets the duration of the dependency call.
- `string InputData`: Gets or sets the command or data sent to the dependency call.
- `string Name`: Gets or sets the name of the dependency call.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related dependency call information.
- `string OutputData`: Gets or sets the output data returned by the dependency call.
- `bool Success`: Gets or sets a value indicating whether the dependency call was successful.
- `string Target`: Gets or sets the target system or endpoint of the dependency call.
- `string Type`: Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).

---

### BaseMessage
Represents the base class for implementing custom messages.

**Namespace:** `Zetatech.Accelerate.Messaging`

#### Properties
- `Guid Id`: Gets or sets the unique identifier of the message.
- `Guid OperationId`: Gets or sets the operation identifier used to associate related message.
- `DateTime Timestamp`: Gets or sets the timestamp when the message was published.

---

### AuthorizationFilterAttribute
Represents a base class for implementing custom authorization filters in ASP.NET Core MVC.

**Namespace:** `Zetatech.Accelerate.Web`

#### Methods
- `void OnAuthorization(AuthorizationFilterContext context)`: Called during the authorization phase to determine whether the current request is authorized.

---

## Dependency Injection Example

```csharp
// Example using Microsoft.Extensions.DependencyInjection
services.AddTransient<Test>();
services.AddTransient<Request>();
services.AddTransient<Metric>();
services.AddTransient<Event>();
services.AddTransient<PageView>();
services.AddTransient<Dependency>();
services.AddTransient<BaseMessage>();
services.AddTransient<AuthorizationFilterAttribute>();
```

---

## Usage Examples

### Creating and Using a Test
```csharp
var test = new Test {
    Name = "UnitTest1",
    Duration = TimeSpan.FromSeconds(2),
    Success = true,
    Message = "Test passed successfully",
    OperationId = Guid.NewGuid()
};
```

### Using Request
```csharp
var request = new Request {
    Name = "GetUser",
    IpAddress = IPAddress.Parse("127.0.0.1"),
    Duration = TimeSpan.FromMilliseconds(150),
    Success = true,
    ResponseCode = HttpStatusCode.OK,
    Timestamp = DateTime.UtcNow,
    Uri = new Uri("https://api.example.com/user"),
    OperationId = Guid.NewGuid()
};
```

### Using Metric
```csharp
var metric = new Metric {
    Name = "CPU_Usage",
    Dimension = "Performance",
    Value = 75.5,
    OperationId = Guid.NewGuid()
};
```

### Using AuthorizationFilterAttribute
```csharp
public class CustomAuthorizationFilter : AuthorizationFilterAttribute {
    public override void OnAuthorization(AuthorizationFilterContext context) {
        // Custom logic
    }
}
```

---

## Notes
- All classes are designed to be used with dependency injection for easier testing and extensibility.
- For more advanced scenarios, refer to the source code and extend the base classes as needed.
