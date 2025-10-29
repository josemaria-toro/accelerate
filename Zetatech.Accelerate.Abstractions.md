# Zetatech.Accelerate.Abstractions
This library provides the foundational building blocks for the Zetatech Accelerate Framework.  
## Technical documentation
### AuthorizationFilterAttribute
Represents a base class for implementing custom authorization filters in ASP.NET Core MVC.  
**Namespace:** `Zetatech.Accelerate.Web`
#### Methods
| Name                    | Description                                                                                   |
|:------------------------|:----------------------------------------------------------------------------------------------|
|OnAuthorization(context) | Called during the authorization phase to determine whether the current request is authorized. |
### BaseMessage
Represents the base class for implementing custom messages.  
**Namespace:** `Zetatech.Accelerate.Messaging`
#### Properties
| Name        | Description                                                              |
|:------------|:-------------------------------------------------------------------------|
| Id          | Gets or sets the unique identifier of the message.                       |
| OperationId | Gets or sets the operation identifier used to associate related message. |
| Timestamp   | Gets or sets the timestamp when the message was published.               |
### Dependency
Represents the results for a dependency call.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name         | Description                                                                                  |
|:-------------|:---------------------------------------------------------------------------------------------|
| Duration     | Gets or sets the duration of the dependency call.                                            |
| InputData    | Gets or sets the command or data sent to the dependency call.                                |
| Name         | Gets or sets the name of the dependency call.                                                |
| OperationId  | Gets or sets the operation identifier used to associate related dependency call information. |
| OutputData   | Gets or sets the output data returned by the dependency call.                                |
| Success      | Gets or sets a value indicating whether the dependency call was successful.                  |
| Target       | Gets or sets the target system or endpoint of the dependency call.                           |
| Type         | Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).                        |
### Event
Represents the data for an event.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name         | Description                                                                        |
|:-------------|:-----------------------------------------------------------------------------------|
| Name         | Gets or sets the name of the event.                                                |
| OperationId  | Gets or sets the operation identifier used to associate related event information. |
### Metric
Represents the data for a metric.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name         | Description                                                                         |
|:-------------|:------------------------------------------------------------------------------------|
| Dimension    | Gets or sets the dimension or category associated with the metric.                  |
| Name         | Gets or sets the name of the metric.                                                |
| OperationId  | Gets or sets the operation identifier used to associate related metric information. |
| Value        | Gets or sets the value of the metric.                                               |
### PageView
Represents the data for a page view.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name         | Description                                                                           |
|:-------------|:--------------------------------------------------------------------------------------|
| Duration    | Gets or sets the duration of the page view.                                            |
| Name        | Gets or sets the name of the page viewed.                                              |
| OperationId | Gets or sets the operation identifier used to associate related page view information. |
| Uri         | Gets or sets the URI of the page viewed.                                               |
### Request
Represents the data for an HTTP request.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name         | Description                                                                               |
|:-------------|:------------------------------------------------------------------------------------------|
| Duration     | Gets or sets the duration of the HTTP request.                                            |
| IpAddress    | Gets or sets the IP address from which the HTTP request originated.                       |
| Name         | Gets or sets the name or identifier of the HTTP request.                                  |
| OperationId  | Gets or sets the operation identifier used to associate related HTTP request information. |
| ResponseCode | Gets or sets the HTTP response code returned for the HTTP request.                        |
| Success      | Gets or sets a value indicating whether the HTTP request was successful.                  |
| Timestamp    | Gets or sets the timestamp indicating when the HTTP request is tracked.                   |
| Uri          | Gets or sets the URI associated with the HTTP request.                                    |
### Test
Represents the result of a test.  
**Namespace:** `Zetatech.Accelerate.Telemetry`
#### Properties
| Name        | Description                                                                       |
|:------------|:----------------------------------------------------------------------------------|
| Duration    | Gets or sets the duration of the test.                                            |
| Message     | Gets or sets the message associated with the test result.                         |
| Name        | Gets or sets the name of the test.                                                |
| OperationId | Gets or sets the operation identifier used to associate related test information. |
| Success     | Gets or sets a value indicating whether the test was successful.                  |
## Examples
### Creating and using a dependency
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new Dependency {
    Duration = TimeSpan.FromMilliseconds(150),
    InputData = "DELETE * FROM users WHERE name='username'",
    Name = "Delete User",
    OperationId = Guid.NewGuid(),
    OutputData = String.Empty,
    Success = true,
    Target = "Database",
    Type = "SQL"
});
```
### Creating and using an event
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new Event {
    Name = "Test done",
    OperationId = Guid.NewGuid()
});
```
### Creating and using a metric
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new Metric {
    Name = "CPU_Usage",
    Dimension = "Performance",
    Value = 75.5,
    OperationId = Guid.NewGuid()
});
```
### Creating and using a page view
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new PageView {
    Duration = 0.001,
    Name = "Home",
    OperationId = Guid.NewGuid()
    Uri = "http://localhost/home"
});
```
### Creating and using a request
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new Request {
    Name = "GetUser",
    IpAddress = IPAddress.Parse("127.0.0.1"),
    Duration = TimeSpan.FromMilliseconds(150),
    Success = true,
    ResponseCode = HttpStatusCode.OK,
    Uri = new Uri("https://api.example.com/user"),
    OperationId = Guid.NewGuid()
});
```
### Creating and using a test
```csharp
var telemetryService = serviceProvider.GetRequiredService<ITelemetryService>();

telemetryService.Track(new Test {
    Name = "UnitTest1",
    Duration = TimeSpan.FromSeconds(2),
    Success = true,
    Message = "Test passed successfully",
    OperationId = Guid.NewGuid()
});
```
### Extending AuthorizationFilterAttribute
```csharp
public class CustomAuthorizationFilter : AuthorizationFilterAttribute
{
    public override void OnAuthorization(AuthorizationFilterContext context)
    {
        // Custom logic
    }
}
```
## Feedback & Contributing
Zetatech.Accelerate.Abstractions is released as open source under the [GNU General Public License](./License.txt).  
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