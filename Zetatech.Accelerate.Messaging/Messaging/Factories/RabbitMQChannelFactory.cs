using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Messaging.Factories;

internal sealed class RabbitMQChannelFactory : IDisposable
{
    private IDictionary<String, IConnection> _connections;
    private Boolean _disposed;
    private static RabbitMQChannelFactory _current;
    private SemaphoreSlim _semaphore;

    public RabbitMQChannelFactory()
    {
        _connections = new Dictionary<String, IConnection>();
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public static RabbitMQChannelFactory Current => _current ??= new RabbitMQChannelFactory();

    public async Task<IChannel> CreateChannelAsync(RabbitMQOptions options)
    {
        if (options == null)
        {
            throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        }

        var connectionFactory = CreateConnectionFactory(options.ConnectionString);

        ValidateConnectionProperties(connectionFactory);

        if (options.UseSsl)
        {
            UseSsl(connectionFactory, options.SslCertIssuer, options.SslCertSerialNumber, options.SslCertSubject, options.SslCertThumbprint);
        }

        var connectionKey = $"{connectionFactory.HostName}@{connectionFactory.VirtualHost}@{connectionFactory.UserName}";

        _semaphore.Wait();

        if (!_connections.ContainsKey(connectionKey))
        {
            var connection = await connectionFactory.CreateConnectionAsync()
                                                    .ConfigureAwait(false);

            _connections.Add(connectionKey, connection);
        }

        _semaphore.Release();

        if (!_connections.TryGetValue(connectionKey, out var rabbitMQConnection))
        {
            throw new MessagingException("Error getting connection with RabbitMQ server");
        }

        return await rabbitMQConnection.CreateChannelAsync()
                                       .ConfigureAwait(false);
    }
    private static ConnectionFactory CreateConnectionFactory(String connectionString)
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

        return connectionFactory;
    }
    private static void UseSsl(ConnectionFactory connectionFactory, String sslCertIssuer, String sslCertSerialNumber, String sslCertSubject, String sslCertThumbprint)
    {
        connectionFactory.Ssl.Enabled = true;

        if (String.IsNullOrEmpty(sslCertIssuer))
        {
            throw new ArgumentException("The certificate issuer has an invalid value", nameof(sslCertIssuer));
        }

        if (String.IsNullOrEmpty(sslCertSerialNumber))
        {
            throw new ArgumentException("The certificate serial number has an invalid value", nameof(sslCertSerialNumber));
        }

        if (String.IsNullOrEmpty(sslCertSubject))
        {
            throw new ArgumentException("The certificate subject has an invalid value", nameof(sslCertSubject));
        }

        if (String.IsNullOrEmpty(sslCertThumbprint))
        {
            throw new ArgumentException("The certificate thumbprint has an invalid value", nameof(sslCertThumbprint));
        }

        ValidateSslCertificate(connectionFactory, sslCertIssuer, sslCertSerialNumber, sslCertSubject, sslCertThumbprint);
    }
    private static void ValidateConnectionProperties(ConnectionFactory connectionFactory)
    {
        if (connectionFactory == null)
        {
            throw new ArgumentException("The provided connection factory must be a valid instance", nameof(connectionFactory));
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
    }
    private static void ValidateSslCertificate(ConnectionFactory connectionFactory, String sslCertIssuer, String sslCertSerialNumber, String sslCertSubject, String sslCertThumbprint)
    {
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
