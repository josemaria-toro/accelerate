using System;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class PublisherOptions
{
    public String ConnectionString { get; set; }
    public String Exchange { get; set; }
}
