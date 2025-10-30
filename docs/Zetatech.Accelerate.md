# Zetatech.Accelerate
## Table of Contents

- [Overview](#overview)
- [Data](#data)
  - [IEntity](#ientity)
  - [ISpecificationExtensions](#ispecificationextensions)
- [Telemetry](#telemetry)
  - [IMetric](#imetric)
  - [IPageView](#ipageview)
  - [ITelemetryServiceExtension](#itelemetryserviceextension)

## Overview

## Data
### IEntity
Provides the interface for implementing custom data entities.
``` csharp
public interface IEntity : ICloneable
```
### ISpecificationExtensions
Extensions methods for the `ISpecification{TEntity}` interface.
``` csharp
public static class ISpecificationExtensions
```
#### Methods
| Name | Description |
| ---- | ----------- |
| IsSatisfiedByAsync<TEntity>(ISpecification<TEntity> specification, TEntity entity) where TEntity : IEntity | Determines whether the specified entity satisfies the specification criteria. Parameters: `specification` — The specification instance. `entity` — The entity to evaluate. Returns a Boolean indicating if the entity satisfies the specification. |
#### Example
```csharp
// Usage example (assumes an implementation of ISpecification<TEntity> exists):
var result = await ISpecificationExtensions.IsSatisfiedByAsync(mySpecification, myEntity);
```
## Telemetry
### IMetric
Provides the interface for implementing custom metrics.
``` csharp
public interface IMetric : ICloneable
```
#### Properties
| Name | Type | Description |
| ---- | ---- | ----------- |
| Dimension | String | Gets or sets the dimension or category associated with the metric. |
| Name | String | Gets or sets the name of the metric. |
| OperationId | Guid | Gets or sets the operation identifier used to associate related metric information. |
| Value | Double | Gets or sets the value of the metric. |

#### Example
```csharp
public class MyMetric : IMetric
{
    public string Dimension { get; set; }
    public string Name { get; set; }
    public Guid OperationId { get; set; }
    public double Value { get; set; }

    public object Clone() => MemberwiseClone();
}

// Usage with a telemetry service (ITelemetryService must be implemented elsewhere):
var metric = new MyMetric { Name = "Requests", Dimension = "API", Value = 1.0, OperationId = Guid.NewGuid() };
await telemetryService.TrackAsync(metric);
```
### IPageView
Provides the interface for implementing custom page views.
``` csharp
public interface IPageView : ICloneable
```
#### Properties
| Name | Type | Description |
| ---- | ---- | ----------- |
| Duration | TimeSpan | Gets or sets the duration of the page view. |
| Name | String | Gets or sets the name of the page viewed. |
| OperationId | Guid | Gets or sets the operation identifier used to associate related page view information. |
| Uri | Uri | Gets or sets the URI of the page viewed. |
#### Example
```csharp
public class MyPageView : IPageView
{
    public TimeSpan Duration { get; set; }
    public string Name { get; set; }
    public Guid OperationId { get; set; }
    public Uri Uri { get; set; }

    public object Clone() => MemberwiseClone();
}

// Usage with a telemetry service:
var pageView = new MyPageView { Name = "Home", Uri = new Uri("https://example.com"), Duration = TimeSpan.FromSeconds(2), OperationId = Guid.NewGuid() };
await telemetryService.TrackAsync(pageView);
```
### ITelemetryServiceExtension
Extensions methods for the `ITelemetryService` interface.
``` csharp
public static class ITelemetryServiceExtension
```
#### Methods
| Name | Description |
| ---- | ----------- |
| TrackAsync(this ITelemetryService telemetryService, IDependency dependency) | Tracks the specified dependency information. Parameter: `telemetryService` — The instance of the telemetry service. `dependency` — The dependency data to track. |
| TrackAsync(this ITelemetryService telemetryService, IEvent @event) | Tracks the specified event information. Parameter: `telemetryService` — The instance of the telemetry service. `event` — The event data to track. |
| TrackAsync(this ITelemetryService telemetryService, IMetric metric) | Tracks the specified metric information. Parameter: `telemetryService` — The instance of the telemetry service. `metric` — The metric data to track. |
| TrackAsync(this ITelemetryService telemetryService, IPageView pageView) | Tracks the specified page view information. Parameter: `telemetryService` — The instance of the telemetry service. `pageView` — The page view data to track. |
| TrackAsync(this ITelemetryService telemetryService, IRequest request) | Tracks the specified HTTP request information. Parameter: `telemetryService` — The instance of the telemetry service. `request` — The HTTP request data to track. |
| TrackAsync(this ITelemetryService telemetryService, ITest test) | Tracks the specified test information. Parameter: `telemetryService` — The instance of the telemetry service. `test` — The test data to track. |
#### Example
```csharp
// Example assumes telemetryService implements ITelemetryService and is available via DI.
await telemetryService.TrackAsync(myDependency);
await telemetryService.TrackAsync(myEvent);
await telemetryService.TrackAsync(myMetric);
await telemetryService.TrackAsync(myPageView);
```