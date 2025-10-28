using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the result of a test.
/// </summary>
public class Test : BaseCloneable, ITest
{
    /// <summary>
    /// Gets or sets the duration of the test.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the message associated with the test result.
    /// </summary>
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets the name of the test.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related test information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the test was successful.
    /// </summary>
    public Boolean Success { get; set; }
}