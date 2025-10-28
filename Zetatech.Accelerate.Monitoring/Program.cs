using System;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Monitoring.DependencyInjection;
using Zetatech.Accelerate.Telemetry.Messages;

namespace Zetatech.Accelerate.Monitoring;

public static class Program
{
    public static void Main(String[] arguments)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine("-- Zeta Technologies --");
        Console.WriteLine();
        Console.WriteLine("This software is released as open source, under the GNU General Public License");
        Console.WriteLine("All rights reserved (c) 2025");
        Console.WriteLine();
        Console.ResetColor();

        try
        {
            var mode = "unknown";

            for (var i = 0; i < arguments.Length; i++)
            {
                switch (arguments[i])
                {
                    case "--mode:analytics":
                        mode = "analytics";
                        break;
                    case "--mode:backoffice":
                        mode = "backoffice";
                        break;
                    case "--mode:processors":
                        mode = "processors";
                        break;
                    default:
                        throw new ArgumentException("Invalid arguments...");
                }
            }

            switch (mode)
            {
                case "analytics":
                    // Configurar la inyeccion de dependencias para los procesos de analítica
                    break;
                case "backoffice":
                    // Configurar la inyeccion de dependencias para el backoffice
                    // Configurar e iniciar la aplicación de backoffice
                    break;
                case "processors":
                    {
                        var serviceCollection = new ServiceCollection();

                        serviceCollection.AddConfigurationSources()
                                         .AddConsoleLogging()
                                         .AddDatabaseLogging()
                                         .AddDatabaseTelemetry()
                                         .AddJsonSerializer()
                                         .AddLoggingFactory()
                                         .AddMessagePublishers()
                                         .AddMessageSubscribers()
                                         .AddRepositories();

                        var serviceProvider = serviceCollection.BuildServiceProvider();

                        var publisher = serviceProvider.GetRequiredService<IPublisherService<AvailabilityMessage>>();
                    }
                    // Configurar la inyeccion de dependencias para los procesadores de mensajes
                    break;
                default:
                    throw new ArgumentException("Invalid working mode...");
            }
        }
        catch (ArgumentException ex)
        {
            PrintHelp(ex.Message);
        }
    }
    private static void PrintHelp(String errorMessage = "")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("USAGE: dotnet monitoring --mode:value [OPTIONS]");
        Console.WriteLine();
        Console.WriteLine("OPTIONS:");
        Console.WriteLine("");
        Console.WriteLine("--mode:value    Specify the working mode");
        Console.WriteLine("                The available modes are:");
        Console.WriteLine("                - analytics: runs the analytics processes");
        Console.WriteLine("                  example: dotnet monitoring --mode:analytics");
        Console.WriteLine("                - backoffice: run as a web application to manage the monitoring system");
        Console.WriteLine("                  example: dotnet monitoring --mode:backoffice");
        Console.WriteLine("                - processors: runs the message processors");
        Console.WriteLine("                  example: dotnet monitoring --mode:processors");
        Console.WriteLine();
        Console.ResetColor();
    }
}