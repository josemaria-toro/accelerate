using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;

namespace Zetatech.Accelerate.DependencyInjection;

public static class Assets
{
    public static WebApplication UseStaticAssets(this WebApplication webApplication)
    {
        var featureEnabled = webApplication.Configuration.GetValue<Boolean>("appSettings:staticAssets", false);

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
