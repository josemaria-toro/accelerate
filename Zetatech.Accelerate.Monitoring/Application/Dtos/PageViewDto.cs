using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the data for a page view.
/// </summary>
public sealed class PageViewDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    [JsonPropertyName("duration")]
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the URL of the page viewed.
    /// </summary>
    [JsonPropertyName("url")]
    public String Url { get; set; }
}
