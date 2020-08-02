using System;

namespace GeekBrainsInternship.Interfaces
{
    public interface IController<T> : IDisposable where T : class
    {
        
    }
}