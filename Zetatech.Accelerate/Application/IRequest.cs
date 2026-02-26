using System;
using System.Collections.Generic;
using System.Net;

namespace Zetatech.Accelerate.Application;

public interface IRequest
{
    Guid CorrelationId { get; set; }
    TimeSpan Duration { get; set; }
    String EndPoint { get; set; }
    IPAddress IpAddress { get; set; }
    IDictionary<String, String> Metadata { get; set; }
    String Name { get; set; }
    String Request { get; set; }
    String Response { get; set; }
    Int32 StatusCode { get; set; }
    Boolean Success { get; set; }
    String Type { get; set; }
}