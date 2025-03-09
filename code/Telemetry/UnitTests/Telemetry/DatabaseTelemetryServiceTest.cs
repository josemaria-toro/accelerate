﻿using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Accelerate.Telemetry;

/// <summary>
/// Class to perform unit tests of DatabaseTelemetryService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class DatabaseTelemetryServiceTest : xUnitTest<DatabaseTelemetryServiceTestContext>
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Test execution context.
    /// </param>
    public DatabaseTelemetryServiceTest(DatabaseTelemetryServiceTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var telemetryService = new DatabaseTelemetryService(null, Context.Connection);
        });
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Dispose();

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Dispose();
            telemetryService.Dispose();
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public async Task Track_Availability_SuccessAsync()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new Availability
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Duration = TimeSpan.FromSeconds(0),
            Message = String.Empty,
            Metadata = new Dictionary<String, Object>
            {
                { "key", "value" }
            },
            Name = String.Empty,
            Success = true,
            Timestamp = DateTime.UtcNow
        });

        await Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Availability_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(Availability));
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public void Track_Dependency_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new Dependency
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Data = String.Empty,
            Duration = TimeSpan.FromSeconds(0),
            Metadata = new Dictionary<String, Object>
            {
                { "key", "value" }
            },
            Name = String.Empty,
            Results = String.Empty,
            Success = true,
            Target = String.Empty,
            Timestamp = DateTime.UtcNow,
            Type = String.Empty
        });

        Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Dependency_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(Dependency));
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public void Track_Event_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new Event
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Metadata = new Dictionary<String, Object>
            {
                { "key", "value" }
            },
            Name = String.Empty,
            Timestamp = DateTime.UtcNow
        });

        Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Event_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(Event));
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public void Track_Metric_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new Metric
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Dimension = String.Empty,
            Name = String.Empty,
            Value = 0D
        });
        telemetryService.Track(new Metric
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Dimension = "dimension",
            Name = String.Empty,
            Value = 0D
        });

        Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Metric_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(Metric));
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public void Track_Page_View_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new PageView
        {
            CorrelationId = $"{Guid.NewGuid()}",
            Duration = TimeSpan.FromSeconds(0),
            Metadata = new Dictionary<String, Object>
            {
                { "key", "value" }
            },
            Name = String.Empty,
            Timestamp = DateTime.UtcNow,
            Uri = new Uri("http://localhost")
        });

        Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Page_View_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(PageView));
        });
    }
    /// <summary>
    /// Method to perform test of track method.
    /// </summary>
    [Fact]
    public void Track_Request_Success()
    {
        var options = Options.Create(Context.TelemetryServiceOptions);
        var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

        telemetryService.Track(new Request
        {
            Client = String.Empty,
            CorrelationId = $"{Guid.NewGuid()}",
            Duration = TimeSpan.FromSeconds(0),
            Metadata = new Dictionary<String, Object>
            {
                { "key", "value" }
            },
            Name = String.Empty,
            ResponseCode = HttpStatusCode.OK,
            Success = true,
            Timestamp = DateTime.UtcNow,
            Uri = new Uri("http://localhost")
        });

        Task.Delay(1000);

        Assert.NotNull(telemetryService);
    }
    /// <summary>
    /// Method to perform test of track method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Track_Request_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.TelemetryServiceOptions);
            var telemetryService = new DatabaseTelemetryService(options, Context.Connection);

            telemetryService.Track(default(Request));
        });
    }
}
