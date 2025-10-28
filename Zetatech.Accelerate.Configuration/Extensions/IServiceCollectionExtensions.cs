using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zetatech.Accelerate.Configuration.Extensions;

/// <summary>
/// Extensions methods for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configure the service to manage the application configurations.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddConfigService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddConfigService(Guid.Empty);
    }
    /// <summary>
    /// Adds and configure the service to manage the application configurations.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    /// <param name="userSecretsId">
    /// Unique identifier of user secrets.
    /// </param>
    public static IServiceCollection AddConfigService(this IServiceCollection serviceCollection, Guid userSecretsId)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables()
                            .AddInMemoryCollection()
                            .AddJsonFile("appsettings.json", true, true);

        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");
        var configFiles = Directory.GetFiles(configPath, "*.json", SearchOption.AllDirectories);

        foreach (var configFile in configFiles)
        {
            configurationBuilder.AddJsonFile(configFile, true, true);
        }

        var configService = configurationBuilder.Build();

        if (userSecretsId == Guid.Empty)
        {
            var userSecretsIdAttributes = AppDomain.CurrentDomain.GetAssemblies()
                                                                 .SelectMany(x => x.ExportedTypes)
                                                                 .Select(x => x.GetCustomAttribute<UserSecretsIdAttribute>())
                                                                 .Where(x => x?.UserSecretsId != null)
                                                                 .Distinct();

            foreach (var userSecretsIdAttribute in userSecretsIdAttributes)
            {
                configurationBuilder.AddUserSecrets(userSecretsIdAttribute.UserSecretsId);
            }
        }
        else
        {
            configurationBuilder.AddUserSecrets($"{userSecretsId}");
        }

        serviceCollection.AddSingleton<IConfiguration>(
            configurationBuilder.Build()
        );

        return serviceCollection;
    }
}