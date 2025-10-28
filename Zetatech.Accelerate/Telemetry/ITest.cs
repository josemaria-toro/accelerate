using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom tests.
/// </summary>
public interface ITest : ICloneable
{
    /// <summary>
    /// Gets or sets the duration of the test.
    /// </summary>
    TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the message associated with the test result.
    /// </summary>
    String Message { get; set; }
    /// <summary>
    /// Gets or sets the name of the test.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related test information.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the test was successful.
    /// </summary>
    Boolean Success { get; set; }
}