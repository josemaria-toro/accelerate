using System;

namespace Zetatech.Accelerate.Application;

public interface ITracker : IDisposable
{
    void Track(Dependency dependency);
    void Track(Error error);
    void Track(Event @event);
    void Track(Metric metric);
    void Track(PageView pageView);
    void Track(Request request);
    void Track(Test test);
    void Track(Trace trace);
}
