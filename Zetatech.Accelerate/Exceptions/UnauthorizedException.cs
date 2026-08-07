using System;

namespace Zetatech.Accelerate.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base()
    {
    }
    public UnauthorizedException(String message) : base(message)
    {
    }
    public UnauthorizedException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
