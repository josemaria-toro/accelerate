using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging.Abstractions;

namespace Zetatech.Accelerate.Logging.ChannelEntries;

internal sealed class ConsoleChannelEntry : BaseChannelEntry
{
    public LogLevel Severity { get; set; }
}
