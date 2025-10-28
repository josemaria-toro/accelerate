using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Logging.Entities;
using Zetatech.Accelerate.Logging.Messages;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Monitoring.Subscribers;
using Zetatech.Accelerate.Telemetry.Entities;
using Zetatech.Accelerate.Telemetry.Messages;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Subscribers
{
    public static IServiceCollection AddMessageSubscribers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISubscriberService<AvailabilityMessage>>(serviceProvider =>
        {
            var availabilityRepository = serviceProvider.GetRequiredService<IRepository<AvailabilityEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new AvailabilitySubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, availabilityRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<DependencyMessage>>(serviceProvider =>
        {
            var dependenciesRepository = serviceProvider.GetRequiredService<IRepository<DependencyEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new DependenciesSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, dependenciesRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<ErrorMessage>>(serviceProvider =>
        {
            var errorsRepository = serviceProvider.GetRequiredService<IRepository<ErrorEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new ErrorsSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, errorsRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<EventMessage>>(serviceProvider =>
        {
            var eventsRepository = serviceProvider.GetRequiredService<IRepository<EventEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new EventsSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, eventsRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<MetricMessage>>(serviceProvider =>
        {
            var metricsRepository = serviceProvider.GetRequiredService<IRepository<MetricEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new MetricsSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, metricsRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<PageViewMessage>>(serviceProvider =>
        {
            var pageViewsRepository = serviceProvider.GetRequiredService<IRepository<PageViewEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new PageViewsSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, pageViewsRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<RequestMessage>>(serviceProvider =>
        {
            var requestsRepository = serviceProvider.GetRequiredService<IRepository<RequestEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new RequestsSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, requestsRepository);
        });

        serviceCollection.AddSingleton<ISubscriberService<TraceMessage>>(serviceProvider =>
        {
            var tracesRepository = serviceProvider.GetRequiredService<IRepository<TraceEntity>>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var subscriberServiceOptions = new SubscriberServiceOptions();

            return new TracesSubscriberService(Options.Create(subscriberServiceOptions), loggingFactory, tracesRepository);
        });

        return serviceCollection;
    }
}