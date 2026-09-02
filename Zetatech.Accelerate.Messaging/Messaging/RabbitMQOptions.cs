using System;

namespace Zetatech.Accelerate.Messaging;

public sealed class RabbitMQOptions
{
    public String ConnectionString { get; set; }
    public String ExchangeName { get; set; }
    public String QueueName { get; set; }
    public String SslCertIssuer { get; set; }
    public String SslCertSerialNumber { get; set; }
    public String SslCertSubject { get; set; }
    public String SslCertThumbprint { get; set; }
    public Boolean UseSsl { get; set; }
}
