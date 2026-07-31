using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Factories;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddRabbitMQChannelFactory(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IRabbitMQChannelFactory, RabbitMQChannelFactory>();
    }
}