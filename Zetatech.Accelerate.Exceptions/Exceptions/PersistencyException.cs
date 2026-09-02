using System;

namespace Zetatech.Accelerate.Exceptions;

public class PersistencyException : Exception
{
    public PersistencyException() : base()
    {
    }
    public PersistencyException(String message) : base(message)
    {
    }
    public PersistencyException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
