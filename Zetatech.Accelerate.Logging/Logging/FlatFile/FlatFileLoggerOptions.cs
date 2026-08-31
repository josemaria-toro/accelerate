using System;
using Zetatech.Accelerate.Logging.Abstractions;

namespace Zetatech.Accelerate.Logging.FlatFile;

public sealed class FlatFileLoggerOptions : BaseLoggerOptions
{
    public String FileName { get; set; }
    public Int64 MaxSize { get; set; }
    public String Path { get; set; }
}
