using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Error : BaseTrackerObject
{
    public String Message { get; set; }
    public String Source { get; set; }
    public String StackTrace { get; set; }
    public String Type { get; set; }
}
