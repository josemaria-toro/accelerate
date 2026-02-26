using System;

namespace Zetatech.Accelerate.Application;

public interface IPublisher : IDisposable
{
    void Publish<TBody>(TBody body, String queueName) where TBody : class;
}
