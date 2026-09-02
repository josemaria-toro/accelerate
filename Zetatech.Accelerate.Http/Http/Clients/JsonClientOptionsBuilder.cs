using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Zetatech.Accelerate.Http.Clients;

public static class JsonClientOptionsBuilder
{
    public static JsonClientOptions Build(IConfiguration configService, String key = null)
    {
        var rootKey = String.IsNullOrEmpty(key) ? "jsonClient" : $"{key}:jsonClient";
        var jsonClientOptions = new JsonClientOptions
        {
            AutoRedirect = configService.GetValue<Boolean>($"{rootKey}:autoRedirect", false),
            Encoding = GetEncoding(configService, rootKey),
            MaxRedirections = configService.GetValue<Int32>($"{rootKey}:maxRedirections", 5),
            UseProxy = configService.GetValue<Boolean>($"{rootKey}:useProxy", false)
        };

        var authType = configService.GetValue<String>($"{rootKey}:authType", "none");

        switch (authType)
        {
            case "basic":
                jsonClientOptions.BasicCredentials = BuildNetworkCredentials(configService, $"{rootKey}:authorization");
                break;
            case "bearerToken":
                jsonClientOptions.BearerToken = configService.GetValue<String>($"{rootKey}:authorization:bearerToken", String.Empty);
                break;
            case "mTLS":
                jsonClientOptions.ClientCertificate = BuildClientCertificate(configService, rootKey);
                break;
        }

        if (jsonClientOptions.UseProxy)
        {
            jsonClientOptions.ProxyCredentials = BuildNetworkCredentials(configService, $"{rootKey}:proxy");
            jsonClientOptions.ProxyUri = configService.GetValue<Uri>($"{rootKey}:proxy:url", null);
        }

        return jsonClientOptions;
    }
    private static X509Certificate2 BuildClientCertificate(IConfiguration configService, String key)
    {
        X509Certificate2 clientCertificate = null;

        var certFileName = configService.GetValue<String>($"{key}:authorization:certFileName", String.Empty);

        if (String.IsNullOrEmpty(certFileName))
        {
            var x509CertStoreLocation = StoreLocation.CurrentUser;
            var certStoreLocation = configService.GetValue<String>($"{key}:authorization:storeLocation", "User");

            if (certStoreLocation.Equals("Machine", StringComparison.InvariantCultureIgnoreCase))
            {
                x509CertStoreLocation = StoreLocation.LocalMachine;
            }

            var certStore = new X509Store(StoreName.My, x509CertStoreLocation);

            certStore.Open(OpenFlags.ReadOnly);

            var certSerialNumber = configService.GetValue<String>($"{key}:authorization:certSerialNumber", String.Empty);

            if (String.IsNullOrEmpty(certSerialNumber))
            {
                var certThumbprint = configService.GetValue<String>($"{key}:authorization:certThumbprint", String.Empty);

                if (!String.IsNullOrEmpty(certThumbprint))
                {
                    clientCertificate = certStore.Certificates.FirstOrDefault(x => x.Thumbprint == certThumbprint);
                }
            }
            else
            {
                clientCertificate = certStore.Certificates.FirstOrDefault(x => x.SerialNumber == certSerialNumber);
            }
        }
        else
        {
            var certPassword = configService.GetValue<String>($"{key}:authorization:certPassword", String.Empty);
            var certKeyFileName = configService.GetValue<String>($"{key}:authorization:certKeyFileName", null);

            if (String.IsNullOrEmpty(certPassword))
            {
                clientCertificate = X509Certificate2.CreateFromPemFile(certFileName, certKeyFileName);
            }
            else
            {
                clientCertificate = X509Certificate2.CreateFromEncryptedPemFile(certFileName, certPassword, certKeyFileName);
            }
        }

        return clientCertificate;
    }
    private static NetworkCredential BuildNetworkCredentials(IConfiguration configService, String key)
    {
        return new NetworkCredential
        {
            Domain = configService.GetValue<String>($"{key}:domain", String.Empty),
            Password = configService.GetValue<String>($"{key}:password", String.Empty),
            UserName = configService.GetValue<String>($"{key}:userName", String.Empty)
        };
    }
    private static Encoding GetEncoding(IConfiguration configService, String key)
    {
        var encodingName = configService.GetValue<String>($"{key}:encoding");

        return encodingName switch
        {
            "ascii" => Encoding.ASCII,
            "latin" => Encoding.Latin1,
            "unicode" => Encoding.Unicode,
            "utf-8" => Encoding.UTF8,
            "utf-16" => Encoding.BigEndianUnicode,
            "utf-32" => Encoding.UTF32,
            _ => Encoding.Default
        };
    }
}
