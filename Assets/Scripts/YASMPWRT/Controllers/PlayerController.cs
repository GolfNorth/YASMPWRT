using UnityEngine;
using YASMPWRT.Enums;
using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Structs;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class PlayerController : IController<PlayerController>, ITickable, IFixedTickable
    {
        private bool _isRewind;
        private PlayerView _view;
        private PlayerModel _model;
        private float _rewindTimer;
        private GameManager _gameManager;
        private InputManager _inputManager;
        private AudioManager _audioManager;

        public bool IsDead => _model.Dead;

        public PlayerController(PlayerView view)
        {
            _view = view;
            _model = new PlayerModel();

            _gameManager = Director.Instance.Get<GameManager>();
            _inputManager = Director.Instance.Get<InputManager>();
            _audioManager = Director.Instance.Get<AudioManager>();
            
            _inputManager.RewindPressed += OnRewindPressed;
            _inputManager.RewindUnpressed += OnRewindUnpressed;
            _inputManager.JumpPressed += OnJumpPressed;
            
            Director.Instance.Get<UpdateManager>().Add(this);
        }

        public void Dispose()
        {
            _inputManager.RewindPressed -= OnRewindPressed;
            _inputManager.RewindUnpressed -= OnRewindUnpressed;
            _inputManager.JumpPressed -= OnJumpPressed;
            
            Director.Instance?.Get<UpdateManager>().Remove(this);
        }

        public void Tick()
        {
            if (_model.Dead || _gameManager.IsPaused) return;
            
            _view.Run(_inputManager.HorizontalAxis);
        }

        public void FixedTick()
        {
            if (_gameManager.IsPaused) return;

            if (_isRewind)
                ExecuteRewind();
            else
                RecordRewind();
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
        
        private void OnJumpPressed()
        {
            if (_model.Dead || _gameManager.IsPaused) return;
            
            Jump(Vector2.up.y);
        }

        public void Jump(float direction, bool certainly = false)
        {
            if (_view.Jump(direction, certainly))
            {
                _audioManager.PlaySoundEffect(SoundType.Jump);
            }
        }
        
        public void Reset()
        {
            _rewindTimer = 0;
            _model.Dead = false;
        }

        public void Spawn(Vector3 position)
        {
            _view.Spawn(position);
        }
        
        public void Die()
        {
            _model.Dead = true;
            _view.Run(0);
            _audioManager.PlaySoundEffect(SoundType.Death);
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

            _model.Dead = false;
            _view.Velocity = frame.Velocity;
            _view.Position = frame.Position;
            _rewindTimer -= frame.DeltaTime;

            _model.Rewind.RemoveLast();
        }
        
        private void RecordRewind()
        {
            if (_model.Dead) return;
            
            _rewindTimer += Time.fixedDeltaTime;

            while (_rewindTimer > _model.MaxRewindTime)
            {
                _rewindTimer -= _model.Rewind.First.Value.DeltaTime;
                
                _model.Rewind.RemoveFirst();
            }

            var newRewind = new RewindFrame()
            {
                DeltaTime = Time.fixedDeltaTime,
                Position = _view.Position,
                Velocity = _view.Velocity
            };

            _model.Rewind.AddLast(newRewind);
        }
    }
}