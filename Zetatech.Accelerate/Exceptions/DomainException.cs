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
    public DomainException(String message,
                           String specification) : base(message)
    {
        Specification = specification;
    }
    public DomainException(String message,
                           Exception innerException) : base(message, innerException)
    {
    }
    public DomainException(String message,
                           Exception innerException,
                           String specification) : base(message, innerException)
    {
        Specification = specification;
    }

    public String Specification { get; private set; }
}
