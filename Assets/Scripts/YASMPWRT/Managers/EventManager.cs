using System;
using YASMPWRT.Enums;

namespace YASMPWRT.Managers
{
    public class EventManager : IDisposable
    {
        public delegate void MenuItemHandler(MenuItemType type);
        public event MenuItemHandler MenuItemActivated;
        
        public EventManager()
        {
            Director.Instance.Set(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }

        public void MenuItemActivate(MenuItemType type)
        {
            MenuItemActivated?.Invoke(type);
        }
    }
}