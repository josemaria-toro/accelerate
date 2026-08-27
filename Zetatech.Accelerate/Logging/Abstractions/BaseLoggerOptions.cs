using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Logging.Abstractions;

public abstract class BaseLoggerOptions
{
    public LogLevel LogLevel { get; set; }
}
