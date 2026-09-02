using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Zetatech.Accelerate.Http.Clients;

public sealed class JsonClientOptions
{
    public Boolean AutoRedirect { get; set; }
    public NetworkCredential BasicCredentials { get; set; }
    public String BearerToken { get; set; }
    public X509Certificate ClientCertificate { get; set; }
    public Encoding Encoding { get; set; }
    public Int32 MaxRedirections { get; set; }
    public NetworkCredential ProxyCredentials { get; set; }
    public Uri ProxyUri { get; set; }
    public Boolean UseProxy { get; set; }
}
