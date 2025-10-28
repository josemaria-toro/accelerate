using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Provides the interface for implementing custom logging providers.
/// </summary>
public interface ILoggingProvider : IDisposable, ILoggerProvider
{
}