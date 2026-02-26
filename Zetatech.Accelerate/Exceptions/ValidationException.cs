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
    public ValidationException(String message, String parameterName) : base(message)
    {
        ParameterName = parameterName;
    }
    public ValidationException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public ValidationException(String message, Exception innerException, String parameterName) : base(message, innerException)
    {
        ParameterName = parameterName;
    }

    public String ParameterName { get; private set; }
}
