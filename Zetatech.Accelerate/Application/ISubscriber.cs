using System;

namespace Zetatech.Accelerate.Application;

public interface ISubscriber<TBody> : IDisposable where TBody : class
{
    void Subscribe(String queueName);
    void Unsubscribe(String queueName);
}