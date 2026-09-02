using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IApplicationBuilder UseStaticAssets(this IApplicationBuilder applicationBuilder)
    {
        var configService = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("staticAssets:enabled", false);

        if (featureEnabled)
        {
            var compress = configService.GetValue<Boolean>("staticAssets:compress", true);
            var staticFileOptions = new StaticFileOptions
            {
                HttpsCompression = compress ? HttpsCompressionMode.Compress : HttpsCompressionMode.DoNotCompress,
                RequestPath = configService.GetValue<String>("staticAssets:requestPath", String.Empty),
                ServeUnknownFileTypes = configService.GetValue<Boolean>("staticAssets:serveUnknownFileTypes", false)
            };

            applicationBuilder.UseStaticFiles(staticFileOptions);
        }

        return applicationBuilder;
    }
}
