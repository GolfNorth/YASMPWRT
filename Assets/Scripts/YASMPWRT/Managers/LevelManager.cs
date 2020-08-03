using System;
using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Data;
using YASMPWRT.Enums;
using YASMPWRT.Views;

namespace YASMPWRT.Managers
{
    public sealed class LevelManager : IDisposable
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;
        private readonly ScoreManager _scoreManager;
        private int _currentLevelIndex;
        private GameObject _currentLevel;
        private CoinController[] _coins;
        private DoorController _door;
        private KeyController _key;
        private PlayerController _player;
        private bool _playerHasKey;
        private int _score;
        
        public LevelManager()
        {
            Director.Instance.Set(this);

            _gameManager = Director.Instance.Get<GameManager>();
            _audioManager = Director.Instance.Get<AudioManager>();
            _scoreManager = Director.Instance.Get<ScoreManager>();
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }

        public void LoadLevel(int index)
        {
            if (LevelsData.Instance.Levels.Length <= index) return;
            
            _currentLevelIndex = index;
            
            _playerHasKey = false;
            _score = 0;

            _currentLevel = GameObject.Instantiate(Resources.Load<GameObject>(LevelsData.Instance.Levels[index]));
            _door = GameObject.FindObjectOfType<DoorView>().Controller;
            _key = GameObject.FindObjectOfType<KeyView>().Controller;
            
            _player = GameObject.FindObjectOfType<PlayerView>().Controller;
            _player.Reset();

            var coinViews = GameObject.FindObjectsOfType<CoinView>();
            
            _coins = new CoinController[coinViews.Length];

            for (var i = 0; i < _coins.Length; i++)
            {
                _coins[i] = coinViews[i].Controller;
            }
        }

        public void ResetLevel()
        {
            _playerHasKey = false;
            _score = 0;

            _player.Reset();
            _door.Reset();
            _key.Reset();
            
            for (var i = 0; i < _coins.Length; i++)
            {
                _coins[i].Reset();
            }
        }
        
        public void EndLevel()
        {
            GameObject.Destroy(_currentLevel);
            
            _scoreManager.AddScore(_score);

            if (LevelsData.Instance.Levels.Length > _currentLevelIndex + 1)
            {
                LoadLevel(_currentLevelIndex + 1);
            }
            else
            {
                _gameManager.LastLevel = _currentLevelIndex;
                _gameManager.GoMainMenu();
            }
        }
        
        public void CollectCoin()
        {
            _score++;
            
            _audioManager.PlaySoundEffect(SoundType.Coin);
        }
        
        public void CollectKey()
        {
            _playerHasKey = true;
            
            _door.Open();
            
            _audioManager.PlaySoundEffect(SoundType.Key);
        }

        public void EnterDoor()
        {
            if (!_playerHasKey) return;
            
            Debug.Log(_playerHasKey);
            
            _audioManager.PlaySoundEffect(SoundType.Success);
            
            EndLevel();
        }

        public void KillPlayer()
        {
            _player.Die();
            
            _audioManager.PlaySoundEffect(SoundType.Death);
        }

        public void ThrowPlayer(float direction)
        {
            _player.Jump(direction, true);
        }
    }
}