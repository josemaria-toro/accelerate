using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Logging.Entities;
using Zetatech.Accelerate.Telemetry.Entities;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IRepository<AvailabilityEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<AvailabilityEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<DependencyEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<DependencyEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<ErrorEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<ErrorEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<EventEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<EventEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<MetricEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<MetricEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<PageViewEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<PageViewEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<RequestEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<RequestEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        serviceCollection.AddTransient<IRepository<TraceEntity>>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var repositoryOptions = new PostgreSqlRepositoryOptions
            {
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                DetailedErrors = configService.GetValue<Boolean>("monitoring:database:detailedErrors", false),
                LazyLoading = configService.GetValue<Boolean>("monitoring:database:lazyLoading", true),
                Schema = configService.GetValue<String>("monitoring:database:schema", "monitoring"),
                SensitiveDataLogging = configService.GetValue<Boolean>("monitoring:database:sensitiveDataLogging", false),
                Timeout = configService.GetValue<Int32>("monitoring:database:timeout", 30),
                TrackChanges = configService.GetValue<Boolean>("monitoring:database:trackChanges", true)
            };

            return new PostgreSqlRepository<TraceEntity, PostgreSqlRepositoryOptions>(Options.Create(repositoryOptions));
        });

        return serviceCollection;
    }
}