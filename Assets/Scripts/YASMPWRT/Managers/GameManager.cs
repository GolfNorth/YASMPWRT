using System;
using System.Collections;
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

        public bool IsPaused
        {
            get => _isPaused;
            private set
            {
                _isPaused = value;

                Time.timeScale = value ? 0 : 1;

                Cursor.visible = !_isLevel || _isPaused;
            }
        }

        public bool IsLevel
        {
            get => _isLevel;
            private set
            {
                _isLevel = value;

                Cursor.visible = !_isLevel || _isPaused;
            }
        }

        public GameManager()
        {
            Director.Instance.Set(this);
            
            IsLevel = SceneManager.GetActiveScene().name == "GameLevel" || SceneManager.GetActiveScene().name == "TestLevel";
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            
            Director.Instance.Remove(this);
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Unpause()
        {
            Director.Instance.RunCoroutine(UnpauseDelay());
        }

        public void StartGame()
        {
            LoadLevel(1);
        }

        public void ContinueGame()
        {
            LoadLevel(_currentLevel);
        }

        public void GoMainMenu()
        {
            IsLevel = false;
            
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            
            Unpause();
        }

        private void LoadLevel(int level)
        {
            IsLevel = true;
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
        
        private IEnumerator UnpauseDelay()
        {
            yield return null;
            
            IsPaused = false;
        }
    }
}