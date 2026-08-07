using System;
using RabbitMQ.Client;

namespace Zetatech.Accelerate.Messaging;

public interface IRabbitMQChannelFactory : IDisposable
{
    IChannel CreateChannel(String connectionString,
                           Boolean useSSl,
                           String sslCertIssuer = null,
                           String sslCertSerialNumber = null,
                           String sslCertSubject = null,
                           String sslCertThumbprint = null);
}