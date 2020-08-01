using System;
using GeekBrainsInternship.Interfaces;
using UnityEngine;

namespace YASMPWRT.Managers
{
    public class InputManager : IDisposable, ITickable
    {
        public delegate void InputHandler();
        
        public event InputHandler AnyKeyPressed;
        public event InputHandler JumpPressed;
        public event InputHandler ActionPressed;
        public event InputHandler UpPressed;
        public event InputHandler DownPressed;
        public event InputHandler LeftPressed;
        public event InputHandler RightPressed;

        public InputManager()
        {
            Director.Instance.Set(this);
            Director.Instance.Get<UpdateManager>()?.Add(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
            Director.Instance.Get<UpdateManager>()?.Remove(this);
        }
        
        public void Tick()
        {
            if (Input.anyKeyDown)
                AnyKeyPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                UpPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                DownPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                LeftPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                RightPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space))
                JumpPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.Return))
                ActionPressed?.Invoke();
        }
    }
}