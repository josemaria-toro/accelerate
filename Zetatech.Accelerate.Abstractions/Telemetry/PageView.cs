using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the data for a page view.
/// </summary>
public class PageView : BaseCloneable, IPageView
{
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the name of the page viewed.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related page view information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the URI of the page viewed.
    /// </summary>
    public Uri Uri { get; set; }
}