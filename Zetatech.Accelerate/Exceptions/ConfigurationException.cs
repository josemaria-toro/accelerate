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
    public ConfigurationException(String message, String parameterName) : base(message)
    {
        ParameterName = parameterName;
    }
    public ConfigurationException(String message, Exception innerException) : base(message, innerException)
    {
    }
    public ConfigurationException(String message, Exception innerException, String parameterName) : base(message, innerException)
    {
        ParameterName = parameterName;
    }

    public String ParameterName { get; private set; }
}
