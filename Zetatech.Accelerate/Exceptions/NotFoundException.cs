using System;

namespace Zetatech.Accelerate.Exceptions;

/// <summary>
/// Represents errors that occur when a requested resource is not found during application execution.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public NotFoundException() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public NotFoundException(String message) : base(message)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    public NotFoundException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
