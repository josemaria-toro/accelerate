using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom page views.
/// </summary>
public interface IPageView : ICloneable
{
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the name of the page viewed.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related page view information.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the URI of the page viewed.
    /// </summary>
    Uri Uri { get; set; }
}