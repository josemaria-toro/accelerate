using System;

namespace Zetatech.Accelerate.Exceptions;

/// <summary>
/// Represents errors that occur during application execution related to business logic violations.
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public BusinessException() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public BusinessException(String message) : base(message)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message and business rule.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="businessRule">
    /// The business rule that was violated.
    /// </param>
    public BusinessException(String message, String businessRule) : base(message)
    {
        BusinessRule = businessRule;
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
    public BusinessException(String message, Exception innerException) : base(message, innerException)
    {
    }
    /// <summary>
    /// Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and a business rule.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    /// <param name="businessRule">
    /// The business rule that was violated.
    /// </param>
    public BusinessException(String message, Exception innerException, String businessRule) : base(message, innerException)
    {
        BusinessRule = businessRule;
    }

    /// <summary>
    /// Gets the business rule that was violated, if provided.
    /// </summary>
    public String BusinessRule { get; private set; }
}