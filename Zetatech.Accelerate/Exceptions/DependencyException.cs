using System;

namespace Zetatech.Accelerate.Exceptions;

/// <summary>
/// Represents errors that occur when a dependency fails or is unavailable during application execution.
/// </summary>
public class DependencyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public DependencyException() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public DependencyException(String message) : base(message)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message and dependency name.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="dependencyName">
    /// The name of the dependency that caused the exception.
    /// </param>
    public DependencyException(String message, String dependencyName) : base(message)
    {
        DependencyName = dependencyName;
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
    public DependencyException(String message, Exception innerException) : base(message, innerException)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and the dependency name.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    /// <param name="dependencyName">
    /// The name of the dependency that caused the exception.
    /// </param>
    public DependencyException(String message, Exception innerException, String dependencyName) : base(message, innerException)
    {
        DependencyName = dependencyName;
    }

    /// <summary>
    /// Gets the name of the dependency that caused the exception, if provided.
    /// </summary>
    public String DependencyName { get; private set; }
}
