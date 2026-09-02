using System;

namespace Zetatech.Accelerate.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base()
    {
    }
    public ForbiddenException(String message) : base(message)
    {
    }
    public ForbiddenException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
