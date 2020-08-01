using System;

namespace YASMPWRT.Managers
{
    public class AudioManager : IDisposable
    {
        public AudioManager()
        {
            Director.Instance.Set(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }
        
        public void PlayMusic()
        {
            
        }

        public void PlaySound()
        {
            
        }
    }
}