using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddConfigurationSources(this IServiceCollection serviceCollection)
    {
        var configurationManager = new ConfigurationManager();

        return serviceCollection.AddSingleton<IConfiguration>(
            configurationManager.AddConfigurationSources()
        );
    }
    public static IConfigurationManager AddConfigurationSources(this IConfigurationManager configurationManager)
    {
        configurationManager.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables()
                            .AddInMemoryCollection()
                            .AddJsonFile("appsettings.json", false, false);

        var userSecretsId = configurationManager.GetValue<Guid>("appSettings:userSecretsId", Guid.Empty);

        if (userSecretsId == Guid.Empty)
        {
            var userSecretsIdAttributes = AppDomain.CurrentDomain.GetAssemblies()
                                                                 .Select(x => x.GetCustomAttribute<UserSecretsIdAttribute>())
                                                                 .Where(x => x?.UserSecretsId != null)
                                                                 .Distinct();

            foreach (var userSecretsIdAttribute in userSecretsIdAttributes)
            {
                configurationManager.AddUserSecrets(userSecretsIdAttribute.UserSecretsId, false);
            }
        }
        else
        {
            configurationManager.AddUserSecrets($"{userSecretsId}", false);
        }

        return configurationManager;
    }
}