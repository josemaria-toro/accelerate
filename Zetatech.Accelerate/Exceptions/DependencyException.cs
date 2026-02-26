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
    public DependencyException(String message, String dependencyName) : base(message)
    {
        DependencyName = dependencyName;
    }
    public DependencyException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public DependencyException(String message, Exception innerException, String dependencyName) : base(message, innerException)
    {
        DependencyName = dependencyName;
    }

    public String DependencyName { get; private set; }
}
