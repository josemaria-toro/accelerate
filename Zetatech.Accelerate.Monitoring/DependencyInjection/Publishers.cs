using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Messages;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Publishers;
using Zetatech.Accelerate.Telemetry.Messages;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Publishers
{
    public static IServiceCollection AddMessagePublishers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPublisherService<AvailabilityMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:availability", "availability")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<AvailabilityMessage>>();
            var publisherService = new RabbitMqPublisherService<AvailabilityMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<DependencyMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:dependencies", "dependencies")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<DependencyMessage>>();
            var publisherService = new RabbitMqPublisherService<DependencyMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<ErrorMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:errors", "errors")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<ErrorMessage>>();
            var publisherService = new RabbitMqPublisherService<ErrorMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<EventMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:events", "events")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<EventMessage>>();
            var publisherService = new RabbitMqPublisherService<EventMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<MetricMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:metrics", "metrics")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<MetricMessage>>();
            var publisherService = new RabbitMqPublisherService<MetricMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<PageViewMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:pageViews", "page-views")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<PageViewMessage>>();
            var publisherService = new RabbitMqPublisherService<PageViewMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<RequestMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:requests", "requests")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<RequestMessage>>();
            var publisherService = new RabbitMqPublisherService<RequestMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        serviceCollection.AddSingleton<IPublisherService<TraceMessage>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var jsonSerializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();
            var publisherServiceOptions = new RabbitMqPublisherServiceOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:rabbitMq", String.Empty),
                Exchange = configService.GetValue<String>("monitoring:rabbitMq:exchange", "default"),
                RoutingKey = configService.GetValue<String>("monitoring:rabbitMq:routingKeys:traces", "traces")
            };
            var subscriberService = serviceProvider.GetRequiredService<ISubscriberService<TraceMessage>>();
            var publisherService = new RabbitMqPublisherService<TraceMessage, RabbitMqPublisherServiceOptions>(Options.Create(publisherServiceOptions), jsonSerializerOptions);

            publisherService.Subscribe(subscriberService);

            return publisherService;
        });

        return serviceCollection;
    }
}