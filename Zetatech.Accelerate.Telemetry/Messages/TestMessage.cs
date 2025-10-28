using System;

namespace Zetatech.Accelerate.Telemetry.Messages;

/// <summary>
/// Represents the result of a test.
/// </summary>
internal sealed class TestMessage : TelemetryMessage
{
    /// <summary>
    /// Gets or sets the duration of the test.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the message associated with the test result.
    /// </summary>
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the test was successful.
    /// </summary>
    public Boolean Success { get; set; }
}