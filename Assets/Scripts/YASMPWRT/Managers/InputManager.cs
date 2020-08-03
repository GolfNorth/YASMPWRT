using System;
using UnityEngine;
using YASMPWRT.Interfaces;

namespace YASMPWRT.Managers
{
    public sealed class InputManager : IDisposable, ITickable
    {
        public delegate void InputHandler();
        
        public event InputHandler AnyKeyPressed;
        public event InputHandler JumpPressed;
        public event InputHandler ActionPressed;
        public event InputHandler MousePressed;
        public event InputHandler UpPressed;
        public event InputHandler DownPressed;
        public event InputHandler CancelPressed;
        public event InputHandler RewindPressed;
        public event InputHandler RewindUnpressed;

        public float HorizontalAxis => Input.GetAxisRaw("Horizontal");

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
            if (Input.GetKeyDown(KeyCode.Space))
                JumpPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.Return))
                ActionPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.Escape))
                CancelPressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                MousePressed?.Invoke();
            if (Input.GetKeyDown(KeyCode.R))
                RewindPressed?.Invoke();
            if (Input.GetKeyUp(KeyCode.R))
                RewindUnpressed?.Invoke();
        }
    }
}