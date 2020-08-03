using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YASMPWRT.Managers
{
    public sealed class GameManager : IDisposable
    {
        private int _currentLevel;
        private bool _isPaused;
        private bool _isLevel;
        private readonly LevelManager _levelManager;

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }
        
        public bool IsContinueAvailable => _currentLevel > 1;

        public bool IsPaused
        {
            get => _isPaused;
            private set
            {
                _isPaused = value;

                Time.timeScale = value ? 0 : 1;
            }
        }
        public bool IsLevel => _isLevel;

        public GameManager()
        {
            Director.Instance.Set(this);
            
            _isLevel = SceneManager.GetActiveScene().name == "GameLevel" || SceneManager.GetActiveScene().name == "TestLevel";
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            
            Director.Instance.Remove(this);
        }

        public void TogglePause()
        {
            IsPaused = !IsPaused;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Unpause()
        {
            IsPaused = false;
        }

        public void StartGame()
        {
            LoadLevel(0);
        }

        public void ContinueGame()
        {
            LoadLevel(_currentLevel);
        }

        public void GoMainMenu()
        {
            _isLevel = false;
            
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            
            Unpause();
        }

        private void LoadLevel(int level)
        {
            _isLevel = true;
            _currentLevel = level;
            
            SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Single).completed += OnGameLevelLoaded;
        }

        private void OnGameLevelLoaded(AsyncOperation obj)
        {
            obj.completed -= OnGameLevelLoaded;
            
            _levelManager.LoadLevel(_currentLevel);
            
            Unpause();
        }

        public void QuitGame()
        {
            Dispose();
            
            Application.Quit();
        }
    }
}