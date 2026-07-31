using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security;
using System.Threading;
using RabbitMQ.Client;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Messaging.Factories;

internal sealed class RabbitMQChannelFactory : IRabbitMQChannelFactory
{
    private IDictionary<String, IConnection> _connections;
    private Boolean _disposed;
    private SemaphoreSlim _semaphore;

    public RabbitMQChannelFactory()
    {
        _connections = new Dictionary<String, IConnection>();
        _semaphore = new SemaphoreSlim(1, 1);
    }
    public IChannel CreateChannel(String connectionString,
                                  Boolean useSSl,
                                  String sslCertIssuer,
                                  String sslCertSerialNumber,
                                  String sslCertSubject,
                                  String sslCertThumbprint)
    {
        if (String.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("The provided connection string is invalid", nameof(connectionString));
        }

        var connectionFactory = new ConnectionFactory();
        var sections = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var parameters in sections)
        {
            var parameter = parameters.Split("=", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (parameter[0].Equals("host", StringComparison.InvariantCultureIgnoreCase))
            {
                connectionFactory.HostName = parameter[1];
            }
            else if (parameter[0].Equals("pass", StringComparison.InvariantCultureIgnoreCase))
            {
                connectionFactory.Password = parameter[1];
            }
            else if (parameter[0].Equals("port", StringComparison.InvariantCultureIgnoreCase))
            {
                connectionFactory.Port = Int32.Parse(parameter[1]);
            }
            else if (parameter[0].Equals("user", StringComparison.InvariantCultureIgnoreCase))
            {
                connectionFactory.UserName = parameter[1];
            }
            else if (parameter[0].Equals("vhost", StringComparison.InvariantCultureIgnoreCase))
            {
                connectionFactory.VirtualHost = parameter[1];
            }
        }

        if (String.IsNullOrEmpty(connectionFactory.HostName))
        {
            throw new ConfigurationException("The host name is missing", "connectionString");
        }

        if (String.IsNullOrEmpty(connectionFactory.Password))
        {
            throw new ConfigurationException("The password is missing", "connectionString");
        }

        if (String.IsNullOrEmpty(connectionFactory.UserName))
        {
            throw new ConfigurationException("The user name is missing", "connectionString");
        }

        if (String.IsNullOrEmpty(connectionFactory.VirtualHost))
        {
            throw new ConfigurationException("The virtual host is missing", "connectionString");
        }

        if (useSSl)
        {
            connectionFactory.Ssl.Enabled = true;
            connectionFactory.Ssl.CertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                var isValid = true;

                try
                {
                    if (certificate is null)
                    {
                        throw new SecurityException("The SSL certificate is invalid");
                    }

                    var effectiveDateString = certificate.GetEffectiveDateString();
                    var effectiveDateTime = DateTime.Parse(effectiveDateString);

                    if (effectiveDateTime.ToUniversalTime() > DateTime.UtcNow)
                    {
                        throw new SecurityException("The SSL certificate is not effective yet");
                    }

                    var expirationDateString = certificate.GetExpirationDateString();
                    var expirationDateTime = DateTime.Parse(expirationDateString);

                    if (expirationDateTime.ToUniversalTime() < DateTime.UtcNow)
                    {
                        throw new SecurityException("The SSL certificate is expired");
                    }

                    if (certificate.Issuer != sslCertIssuer)
                    {
                        throw new SecurityException("The SSL certificate issuer doesn't match");
                    }

                    if (certificate.Subject != sslCertSubject)
                    {
                        throw new SecurityException("The SSL certificate subject doesn't match");
                    }

                    if (certificate.GetCertHashString() != sslCertThumbprint)
                    {
                        throw new SecurityException("The SSL certificate thumbprint doesn't match");
                    }

                    if (certificate.GetSerialNumberString() != sslCertSerialNumber)
                    {
                        throw new SecurityException("The SSL certificate serial number doesn't match");
                    }

                    if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
                    {
                        throw new SecurityException("The SSL certificate chain status is invalid");
                    }

                    if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNotAvailable)
                    {
                        throw new SecurityException("The SSL certificate is not available");
                    }
                }
                catch (Exception)
                {
                    isValid = false;
                }

                return isValid;
            };
        }

        var connectionKey = $"{connectionFactory.HostName}@{connectionFactory.VirtualHost}@{connectionFactory.UserName}";

        _semaphore.Wait();

        if (!_connections.ContainsKey(connectionKey))
        {
            var connectionTask = connectionFactory.CreateConnectionAsync();

            connectionTask.Wait();

            if (!connectionTask.IsCompletedSuccessfully)
            {
                throw new MessagingException("Error creating connection with RabbitMQ server", connectionTask.Exception);
            }

            _connections.Add(connectionKey, connectionTask.Result);
        }

        _semaphore.Release();

        if (!_connections.TryGetValue(connectionKey, out var connection))
        {
            throw new MessagingException("Error getting connection with RabbitMQ server");
        }

        var channelTask = connection.CreateChannelAsync();

        channelTask.Wait();

        if (!channelTask.IsCompletedSuccessfully)
        {
            throw new MessagingException("Error creating channel with RabbitMQ server", channelTask.Exception);
        }

        return channelTask.Result;
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        foreach (var item in _connections)
        {
            item.Value.CloseAsync();
        }

        _connections = null;
        _semaphore = null;

        GC.SuppressFinalize(this);
    }
}