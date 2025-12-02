using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zetatech.Accelerate.Application.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds the configuration service into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddConfigurationService(this IServiceCollection serviceCollection)
    {
        var configurationBuilder = new ConfigurationBuilder();
        var configService = configurationBuilder.Configure()
                                                .Build();

        return serviceCollection.AddSingleton<IConfiguration>(configService);
    }
    /// <summary>
    /// Configure the type used to build application configuration.
    /// </summary>
    /// <param name="configurationBuilder">
    /// Type used to build application configuration.
    /// </param>
    public static IConfigurationBuilder Configure(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables()
                            .AddInMemoryCollection()
                            .AddJsonFile("appsettings.json", true, true);

        var configPath = configurationBuilder.Build()
                                             .GetValue<String>("appSettings:configPath", "Configuration");

        if (!Path.IsPathRooted(configPath))
        {
            configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
        }

        if (Directory.Exists(configPath))
        {
            var configFiles = Directory.GetFiles(configPath, "*.json", SearchOption.AllDirectories);

            foreach (var configFile in configFiles)
            {
                configurationBuilder.AddJsonFile(configFile, true, true);
            }
        }

        var configService = configurationBuilder.Build();
        var userSecretsId = configService.GetValue<Guid>("appSettings:userSecretsId", Guid.Empty);

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

        return configurationBuilder;
    }
}
