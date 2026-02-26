using System;

namespace Zetatech.Accelerate.Exceptions;

public class BusinessException : Exception
{
    public BusinessException() : base()
    {
    }
    public BusinessException(String message) : base(message)
    {
    }
    public BusinessException(String message, String businessRule) : base(message)
    {
        BusinessRule = businessRule;
    }
    public BusinessException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public BusinessException(String message, Exception innerException, String businessRule) : base(message, innerException)
    {
        BusinessRule = businessRule;
    }

    public String BusinessRule { get; private set; }
}