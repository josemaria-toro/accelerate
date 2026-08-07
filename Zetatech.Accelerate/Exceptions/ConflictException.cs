using System;

namespace Zetatech.Accelerate.Exceptions;

public class ConflictException : Exception
{
    public ConflictException() : base()
    {
    }
    public ConflictException(String message) : base(message)
    {
    }
    public ConflictException(String message, Exception innerException) : base(message, innerException)
    {
    }
}