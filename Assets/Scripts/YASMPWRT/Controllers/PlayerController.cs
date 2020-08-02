using GeekBrainsInternship.Interfaces;
using UnityEngine;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Structs;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class PlayerController : IController<PlayerController>, IFixedTickable
    {
        private bool _isRewind;
        private PlayerView _view;
        private PlayerModel _model;
        private float _rewindTimer;
        private GameManager _gameManager;
        private InputManager _inputManager;
        private AudioManager _audioManager;

        public PlayerController(PlayerView view)
        {
            _view = view;
            _model = new PlayerModel();

            _gameManager = Director.Instance.Get<GameManager>();
            _inputManager = Director.Instance.Get<InputManager>();
            _audioManager = Director.Instance.Get<AudioManager>();
            
            _inputManager.RewindPressed += OnRewindPressed;
            _inputManager.RewindUnpressed += OnRewindUnpressed;
            
            Director.Instance.Get<UpdateManager>().Add(this);
        }

        public void Dispose()
        {
            _inputManager.RewindPressed -= OnRewindPressed;
        }

        private void OnRewindPressed()
        {
            if (_gameManager.IsPaused) return;
            
            _isRewind = _model.Rewind.Count > 0;
            
            if (_isRewind)
                _audioManager.StartRewind();
        }
        
        private void OnRewindUnpressed()
        {
            _isRewind = false;
            
            _audioManager.StopRewind();
        }

        public void Restart()
        {
            _rewindTimer = 0;
            _model.HasKey = false;
        }

        public void FixedTick()
        {
            if (_gameManager.IsPaused) return;

            if (_isRewind)
                ExecuteRewind();
            else
                RecordRewind();
        }

        private void ExecuteRewind()
        {
            if (_model.Rewind.Count == 0)
            {
                _isRewind = false;
                _audioManager.StopRewind();
                
                return;
            }
            
            var frame = _model.Rewind.Last.Value;

            _view.Dead = frame.Dead;
            _view.Velocity = frame.Velocity;
            _view.Position = frame.Position;
            _rewindTimer -= frame.DeltaTime;

            _model.Rewind.RemoveLast();
        }
        
        private void RecordRewind()
        {
            _rewindTimer += Time.fixedDeltaTime;

            while (_rewindTimer > _model.MaxRewindTime)
            {
                _rewindTimer -= _model.Rewind.First.Value.DeltaTime;
                
                _model.Rewind.RemoveFirst();
            }

            var newRewind = new RewindFrame()
            {
                Dead = _view.Dead,
                DeltaTime = Time.fixedDeltaTime,
                Position = _view.Position,
                Velocity = _view.Velocity
            };

            _model.Rewind.AddLast(newRewind);
        }
    }
}