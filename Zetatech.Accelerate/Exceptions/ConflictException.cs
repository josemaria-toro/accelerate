using System;

namespace Zetatech.Accelerate.Exceptions;

/// <summary>
/// Represents errors that occur when a conflict is detected during application execution.
/// </summary>
public class ConflictException : Exception
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public ConflictException() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public ConflictException(String message) : base(message)
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
    public ConflictException(String message, Exception innerException) : base(message, innerException)
    {
    }
}