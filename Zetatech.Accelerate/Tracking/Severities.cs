namespace Zetatech.Accelerate.Tracking;

/// <summary>
/// Represent the severities of a trace.
/// </summary>
public enum Severities
{
    /// <summary>
    /// Represent a trace that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.
    /// </summary>
    Critical = 1,
    /// <summary>
    /// Represent a trace that are used for interactive investigation during development.
    /// </summary>
    Debug = 2,
    /// <summary>
    /// Represent a trace that highlight when the current flow of execution is stopped due to a failure.
    /// </summary>
    Error = 3,
    /// <summary>
    /// Represent a trace that track the general flow of the application.
    /// </summary>
    Information = 4,
    /// <summary>
    /// Represent a trace that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.
    /// </summary>
    Warning = 5
}
