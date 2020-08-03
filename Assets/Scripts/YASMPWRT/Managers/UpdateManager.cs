using System;
using System.Collections.Generic;
using YASMPWRT.Interfaces;

namespace YASMPWRT.Managers
{
    public sealed class UpdateManager : IDisposable
    {
        private readonly List<ITickable> _ticks = new List<ITickable>();
        private readonly List<ILateTickable> _lateTicks = new List<ILateTickable>();
        private readonly List<IFixedTickable> _fixedTicks = new List<IFixedTickable>();

        public UpdateManager()
        {
            Director.Instance.Set(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }
        
        public void Add(object updatable)
        {
            if (updatable is IInitializable initializable)
            {
                initializable.Initialize();
            }
            
            if (updatable is ITickable tick)
            {
                _ticks.Add(tick);
            }
            
            if (updatable is ILateTickable lateTick)
            {
                _lateTicks.Add(lateTick);
            }
            
            if (updatable is IFixedTickable fixedTick)
            {
                _fixedTicks.Add(fixedTick);
            }
        }
        
        public void Remove(object updatable)
        {
            if (updatable is ITickable tick)
            {
                _ticks.Remove(tick);
            }
            
            if (updatable is ILateTickable lateTick)
            {
                _lateTicks.Remove(lateTick);
            }
            
            if (updatable is IFixedTickable fixedTick)
            {
                _fixedTicks.Remove(fixedTick);
            }
        }

        public void Tick()
        {
            for (var i = 0; i < _ticks.Count; i++)
            {
                _ticks[i].Tick();
            }
        }

        public void TickLate()
        {
            for (var i = 0; i < _lateTicks.Count; i++)
            {
                _lateTicks[i].LateTick();
            }
        }

        public void TickFixed()
        {
            for (var i = 0; i < _fixedTicks.Count; i++)
            {
                _fixedTicks[i].FixedTick();
            }
        }
    }
}