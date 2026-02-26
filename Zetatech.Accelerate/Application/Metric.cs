using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Metric : BaseTrackerObject
{
    public String Dimension { get; set; }
    public String Name { get; set; }
    public Double? Value { get; set; }
}