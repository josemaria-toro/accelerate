using System;
using System.Collections.Generic;

namespace Zetatech.Accelerate.Application.Abstractions;

public abstract class BaseTrackerObject
{
    public Guid? CorrelationId { get; set; }
    public IDictionary<String, String> Metadata { get; set; }
}
