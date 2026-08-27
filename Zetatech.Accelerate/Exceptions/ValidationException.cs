using System;

namespace Zetatech.Accelerate.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base()
    {
    }
    public ValidationException(String message) : base(message)
    {
    }
    public ValidationException(String message,
                               String property) : base(message)
    {
        Property = property;
    }
    public ValidationException(String message,
                               Exception innerException) : base(message, innerException)
    {
    }
    public ValidationException(String message,
                               Exception innerException,
                               String property) : base(message, innerException)
    {
        Property = property;
    }

    public String Property { get; private set; }
}
