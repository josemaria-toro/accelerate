using System;

namespace Zetatech.Accelerate.Telemetry.Entities;

/// <summary>
/// Represents the data for a page view.
/// </summary>
internal sealed class PageViewEntity : TelemetryEntity
{
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the URL of the page viewed.
    /// </summary>
    public String Url { get; set; }
}
