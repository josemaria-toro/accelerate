using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddConfigurationSources(this IServiceCollection serviceCollection)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables()
                            .AddInMemoryCollection()
                            .AddJsonFile("appsettings.json", true, true);

        var configService = configurationBuilder.Build();
        var userSecretsId = configService.GetValue<Guid>("appSettings:appId", Guid.Empty);

        if (userSecretsId != Guid.Empty)
        {
            configurationBuilder.AddUserSecrets($"{userSecretsId}");
        }

        var userSecretsIdAttributes = AppDomain.CurrentDomain.GetAssemblies()
                                                             .SelectMany(x => x.ExportedTypes)
                                                             .Select(x => x.GetCustomAttribute<UserSecretsIdAttribute>())
                                                             .Where(x => x?.UserSecretsId != null);

        foreach (var userSecretsIdAttribute in userSecretsIdAttributes)
        {
            configurationBuilder.AddUserSecrets(userSecretsIdAttribute.UserSecretsId);
        }

        serviceCollection.AddSingleton<IConfiguration>(
            configurationBuilder.Build()
        );

        return serviceCollection;
    }
}