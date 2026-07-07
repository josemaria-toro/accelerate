using System;

namespace Zetatech.Accelerate.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException() : base()
    {
    }
    public ConfigurationException(String message) : base(message)
    {
    }
    public ConfigurationException(String message, String parameter) : base(message)
    {
        Parameter = parameter;
    }
    public ConfigurationException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public ConfigurationException(String message, Exception innerException, String parameter) : base(message, innerException)
    {
        Parameter = parameter;
    }

    public String Parameter { get; private set; }
}
