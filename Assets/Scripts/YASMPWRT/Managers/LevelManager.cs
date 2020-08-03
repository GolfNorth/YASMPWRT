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
        private readonly MessagesManager _messagesManager;
        private int _currentLevelIndex;
        private GameObject _currentLevel;
        private BridgeController[] _bridges;
        private CoinController[] _coins;
        private HintController[] _hints;
        private FlagController _flag;
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
            _messagesManager = Director.Instance.Get<MessagesManager>();
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
            _flag = GameObject.FindObjectOfType<FlagView>().Controller;
            _door = GameObject.FindObjectOfType<DoorView>().Controller;
            _key = GameObject.FindObjectOfType<KeyView>().Controller;
            
            _player = GameObject.FindObjectOfType<PlayerView>().Controller;
            _player.Spawn(_flag.Position);
            _player.Reset();
            
            var bridgeViews = GameObject.FindObjectsOfType<BridgeView>();
            
            _bridges = new BridgeController[bridgeViews.Length];

            for (var i = 0; i < _bridges.Length; i++)
            {
                _bridges[i] = bridgeViews[i].Controller;
            }

            var coinViews = GameObject.FindObjectsOfType<CoinView>();
            
            _coins = new CoinController[coinViews.Length];

            for (var i = 0; i < _coins.Length; i++)
            {
                _coins[i] = coinViews[i].Controller;
            }
            
            var hintsView = GameObject.FindObjectsOfType<HintView>();
            
            _hints = new HintController[hintsView.Length];

            for (var i = 0; i < _hints.Length; i++)
            {
                _hints[i] = hintsView[i].Controller;
            }
        }

        public void RestartLevel()
        {
            _playerHasKey = false;
            _score = 0;
            
            _player.Spawn(_flag.Position);
            _player.Reset();
            _door.Reset();
            _key.Reset();
            
            for (var i = 0; i < _bridges.Length; i++)
            {
                _bridges[i].Reset();
            }
            
            for (var i = 0; i < _coins.Length; i++)
            {
                _coins[i].Reset();
            }
            
            for (var i = 0; i < _hints.Length; i++)
            {
                _hints[i].Reset();
            }
        }

        private void EndLevel()
        {
            _bridges = null;
            _coins = null;
            _hints = null;
            _flag = null;
            _door = null;
            _key = null;
            
            GameObject.Destroy(_currentLevel);
            
            _scoreManager.AddScore(_score);

            _currentLevelIndex++;
            
            if (LevelsData.Instance.Levels.Length > _currentLevelIndex)
            {
                _gameManager.CurrentLevel = _currentLevelIndex;
                LoadLevel(_currentLevelIndex);
            }
            else
            {
                _gameManager.CurrentLevel = 0;
                _gameManager.GoMainMenu();
                
                _messagesManager.Show($"That's all!\nYou collected {_scoreManager.Score} coins out of 100");
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
            if (!_playerHasKey || _player.IsDead) return;

            _audioManager.PlaySoundEffect(SoundType.Success);
            
            EndLevel();
        }

        public void KillPlayer()
        {
            if (_player.IsDead) return;
            
            _player.Die();

            _audioManager.PlaySoundEffect(SoundType.Death);
        }

        public void ThrowPlayer(float direction)
        {
            _player.Jump(direction, true);
        }
    }
}