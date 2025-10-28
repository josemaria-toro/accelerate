using System;

namespace Zetatech.Accelerate.Telemetry.Messages;

/// <summary>
/// Represents the data for a page view.
/// </summary>
internal sealed class PageViewMessage : TelemetryMessage
{
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the URI of the page viewed.
    /// </summary>
    public Uri Uri { get; set; }
}