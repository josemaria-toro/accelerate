using System;

namespace Zetatech.Accelerate.Exceptions;

public class UnavailableException : Exception
{
    public UnavailableException() : base()
    {
    }
    public UnavailableException(String message) : base(message)
    {
    }
    public UnavailableException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
