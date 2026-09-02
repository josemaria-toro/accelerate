using System;

namespace Zetatech.Accelerate.Exceptions;

public class DependencyException : Exception
{
    public DependencyException() : base()
    {
    }
    public DependencyException(String message) : base(message)
    {
    }
    public DependencyException(String message, String name) : base(message)
    {
        Name = name;
    }
    public DependencyException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public DependencyException(String message, Exception innerException, String name) : base(message, innerException)
    {
        Name = name;
    }

    public String Name { get; private set; }
}
