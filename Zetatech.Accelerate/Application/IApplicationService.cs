using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Application;

/// <summary>
/// Provides the interface for implementing custom application services.
/// </summary>
public interface IApplicationService : IDisposable
{
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    ILoggerFactory LoggerFactory { get; set; }
}