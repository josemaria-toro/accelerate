using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Provides the interface for implementing custom logging factories.
/// </summary>
public interface ILoggingFactory : IDisposable, ILoggerFactory
{
}
