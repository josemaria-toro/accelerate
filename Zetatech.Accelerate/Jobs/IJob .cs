using System;
using Microsoft.Extensions.Hosting;

namespace Zetatech.Accelerate.Jobs;

public interface IJob : IHostedService, IDisposable
{
}