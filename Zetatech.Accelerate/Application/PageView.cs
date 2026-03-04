using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class PageView : BaseTrackerObject
{
    public String Device { get; set; }
    public String Name { get; set; }
    public TimeSpan? Duration { get; set; }
    public String IpAddress { get; set; }
    public Uri Uri { get; set; }
    public String UserAgent { get; set; }
}