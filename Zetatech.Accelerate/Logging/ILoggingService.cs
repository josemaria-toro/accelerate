using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Provides the interface for implementing custom logging services.
/// </summary>
public interface ILoggingService : IDisposable, ILogger
{
}