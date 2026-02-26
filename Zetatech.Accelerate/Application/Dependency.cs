using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Dependency : BaseTrackerObject
{
    public TimeSpan? Duration { get; set; }
    public String DataInput { get; set; }
    public String DataOutput { get; set; }
    public String Name { get; set; }
    public Boolean? Success { get; set; }
    public String Target { get; set; }
    public String Type { get; set; }
}