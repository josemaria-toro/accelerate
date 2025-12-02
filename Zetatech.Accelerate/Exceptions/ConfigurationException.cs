using System;

namespace Zetatech.Accelerate.Exceptions;

/// <summary>
/// Represents errors that occur when there is a configuration issue during application execution.
/// </summary>
public class ConfigurationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public ConfigurationException() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public ConfigurationException(String message) : base(message)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message and parameter name.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="parameterName">
    /// The name of the configuration parameter that caused the exception.
    /// </param>
    public ConfigurationException(String message, String parameterName) : base(message)
    {
        ParameterName = parameterName;
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
    public ConfigurationException(String message, Exception innerException) : base(message, innerException)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and the parameter name.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    /// <param name="parameterName">
    /// The name of the configuration parameter that caused the exception.
    /// </param>
    public ConfigurationException(String message, Exception innerException, String parameterName) : base(message, innerException)
    {
        ParameterName = parameterName;
    }

    /// <summary>
    /// Gets the name of the configuration parameter that caused the exception, if provided.
    /// </summary>
    public String ParameterName { get; private set; }
}
