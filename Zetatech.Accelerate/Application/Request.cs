using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Request : BaseTrackerObject
{
    public String Body { get; set; }
    public TimeSpan? Duration { get; set; }
    public String EndPoint { get; set; }
    public String IpAddress { get; set; }
    public String Name { get; set; }
    public String Response { get; set; }
    public Int32? StatusCode { get; set; }
    public Boolean? Success { get; set; }
    public String Type { get; set; }
}