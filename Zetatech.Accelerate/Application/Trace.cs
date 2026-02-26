using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Trace : BaseTrackerObject
{
    public String Message { get; set; }
    public String Source { get; set; }
}
