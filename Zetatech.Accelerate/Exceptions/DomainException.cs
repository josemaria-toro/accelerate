using System;

namespace Zetatech.Accelerate.Exceptions;

public class DomainException : Exception
{
    public DomainException() : base()
    {
    }
    public DomainException(String message) : base(message)
    {
    }
    public DomainException(String message, String rule) : base(message)
    {
        Rule = rule;
    }
    public DomainException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public DomainException(String message, Exception innerException, String rule) : base(message, innerException)
    {
        Rule = rule;
    }

    public String Rule { get; private set; }
}