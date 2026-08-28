using System;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Logging.Console;

public sealed class ConsoleLoggerEntry
{
    public String Message { get; set; }
    public LogLevel Severity { get; set; }
}
