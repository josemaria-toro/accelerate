using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection Configure(this IServiceCollection serviceCollection)
    {
        var configurationManager = new ConfigurationManager();

        return serviceCollection.AddSingleton<IConfiguration>(
            configurationManager.Configure()
        );
    }
    public static IConfigurationManager Configure(this IConfigurationManager configurationManager)
    {
        configurationManager.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables()
                            .AddInMemoryCollection()
                            .AddJsonFile("appsettings.json", false, true);

        var userSecretsId = configurationManager.GetValue<Guid>("appSettings:userSecretsId", Guid.Empty);

        if (userSecretsId == Guid.Empty)
        {
            var userSecretsIdAttributes = AppDomain.CurrentDomain.GetAssemblies()
                                                                 .SelectMany(x => x.ExportedTypes)
                                                                 .Select(x => x.GetCustomAttribute<UserSecretsIdAttribute>())
                                                                 .Where(x => x?.UserSecretsId != null)
                                                                 .Distinct();

            foreach (var userSecretsIdAttribute in userSecretsIdAttributes)
            {
                configurationManager.AddUserSecrets(userSecretsIdAttribute.UserSecretsId, true);
            }
        }
        else
        {
            configurationManager.AddUserSecrets($"{userSecretsId}", true);
        }

        return configurationManager;
    }
}