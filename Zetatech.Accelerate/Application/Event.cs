using System;
using Zetatech.Accelerate.Application.Abstractions;

namespace Zetatech.Accelerate.Application;

public sealed class Event : BaseTrackerObject
{
    public String Name { get; set; }
}