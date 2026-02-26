using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Test : BaseTrackerObject
{
    public TimeSpan? Duration { get; set; }
    public String Message { get; set; }
    public String Name { get; set; }
    public Boolean? Success { get; set; }
}