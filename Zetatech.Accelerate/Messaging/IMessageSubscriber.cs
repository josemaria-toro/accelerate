using System;

namespace Zetatech.Accelerate.Messaging;

public interface IMessageSubscriber<TBody> : IDisposable where TBody : class
{
    void Subscribe();
    void Unsubscribe();
}