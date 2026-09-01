using System;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Logging.Abstractions;

internal abstract class BaseChannelEntry
{
    public String Message { get; set; }
    public LogLevel Severity { get; set; }
}
