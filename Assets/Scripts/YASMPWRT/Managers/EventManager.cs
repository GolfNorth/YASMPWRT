using System;
using YASMPWRT.Enums;

namespace YASMPWRT.Managers
{
    public sealed class EventManager : IDisposable
    {
        public delegate void EventManagerHandler(EventType type);

        public event EventManagerHandler NewEvent;
        
        public EventManager()
        {
            Director.Instance.Set(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }

        public void NewEventInvoke(EventType type)
        {
            NewEvent?.Invoke(type);
        }
    }
}