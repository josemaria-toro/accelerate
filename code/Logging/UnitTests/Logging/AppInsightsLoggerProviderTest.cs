﻿using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Logging;

/// <summary>
/// Class to perform unit tests of AppInsightsLoggerProvider class.
/// </summary>
[ExcludeFromCodeCoverage]
public class AppInsightsLoggerProviderTest : xUnitTest<AppInsightsLoggerTestContext>
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Test execution context.
    /// </param>
    public AppInsightsLoggerProviderTest(AppInsightsLoggerTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerProvider = new AppInsightsLoggerProvider(options);

        Assert.NotNull(loggerProvider);
    }
    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var loggerProvider = new AppInsightsLoggerProvider(null);
        });
    }
    /// <summary>
    /// Method to perform unit test of CreateLogger method.
    /// </summary>
    [Fact]
    public void CreateLogger_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerProvider = new AppInsightsLoggerProvider(options);
        var logger = loggerProvider.CreateLogger("Default");

        Assert.NotNull(logger);
    }
    /// <summary>
    /// Method to perform unit test of CreateLogger method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void CreateLogger_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerProvider = new AppInsightsLoggerProvider(options);
            var logger = loggerProvider.CreateLogger(null);
        });
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerProvider = new AppInsightsLoggerProvider(options);

        loggerProvider.Dispose();

        Assert.NotNull(loggerProvider);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerProvider = new AppInsightsLoggerProvider(options);

            loggerProvider.Dispose();
            loggerProvider.Dispose();
        });
    }
}
