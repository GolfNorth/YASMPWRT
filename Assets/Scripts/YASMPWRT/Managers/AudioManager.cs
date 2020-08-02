using System;
using UnityEngine;
using YASMPWRT.Data;

namespace YASMPWRT.Managers
{
    public class AudioManager : IDisposable
    {
        private bool _isRewind;
        private bool _isMusicOn;
        private bool _isSoundEffectsOn;
        private AudioSource _audioSource;

        public bool IsMusicOn
        {
            get => _isMusicOn;
            private set
            {
                PlayerPrefs.SetInt("IsMusicOn", value ? 1 : 0);
                
                _isMusicOn = value;
            }
        }
        
        public bool IsSoundEffectsOn
        {
            get => _isSoundEffectsOn;
            private set
            {
                PlayerPrefs.SetInt("IsSoundEffectsOn", value ? 1 : 0);
                
                _isSoundEffectsOn = value;
            }
        }

        public delegate void AudioHandler();
        public event AudioHandler MusicToggled;
        public event AudioHandler SoundEffectsToggled;
        
        public AudioManager()
        {
            Director.Instance.Set(this);
            
            _isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
            _isSoundEffectsOn = PlayerPrefs.GetInt("IsSoundEffectsOn", 1) == 1;
            
            var gameObject = new GameObject(GetType().ToString());
            gameObject.transform.SetParent(Director.Instance.transform);
            
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = AudioClipsData.Instance.MusicLoop;
            _audioSource.loop = true;
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }
        
        public void PlayMusic()
        {
            if (!_isMusicOn || _audioSource.isPlaying) return;
            
            _audioSource.Play();
        }

        public void PlaySoundEffect(AudioClip audioClip)
        {
            if (!_isSoundEffectsOn) return;
            
            _audioSource.PlayOneShot(audioClip);
        }

        public bool ToggleMusic()
        {
            IsMusicOn = !_isMusicOn;

            if (_isMusicOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
            
            MusicToggled?.Invoke();

            return _isMusicOn;
        }

        public bool ToggleSoundEffects()
        {
            IsSoundEffectsOn = !_isSoundEffectsOn;
            
            SoundEffectsToggled?.Invoke();

            return _isSoundEffectsOn;
        }

        public bool ToggleRewind()
        {
            _isRewind = !_isRewind;

            _audioSource.pitch = _isRewind ? -1 : 1;

            return _isRewind;
        }
    }
}