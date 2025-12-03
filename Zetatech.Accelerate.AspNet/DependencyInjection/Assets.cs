using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using System;

namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Serves the static files inside of wwwroot folder.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseStaticAssets(this WebApplication webApplication)
    {
        var featureEnabled = webApplication.Configuration.GetValue<Boolean>("features:staticAssets", false);

        if (featureEnabled)
        {
            var compress = webApplication.Configuration.GetValue<Boolean>("staticAssets:compress", true);
            var staticFileOptions = new StaticFileOptions
            {
                HttpsCompression = compress ? HttpsCompressionMode.Compress : HttpsCompressionMode.DoNotCompress,
                RequestPath = webApplication.Configuration.GetValue<String>("staticAssets:requestPath", "/"),
                ServeUnknownFileTypes = webApplication.Configuration.GetValue<Boolean>("staticAssets:serveUnknownFileTypes", false)
            };

            webApplication.UseStaticFiles(staticFileOptions);
        }

        return webApplication;
    }
}
