using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YASMPWRT.Managers
{
    public class GameManager : IDisposable
    {
        private int _currentLevel;
        private bool _isPaused;
        private bool _isLevel;

        public bool IsContinueAvailable => _currentLevel > 1;
        public bool IsPaused => _isPaused;
        public bool IsLevel => _isLevel;

        public GameManager()
        {
            Director.Instance.Set(this);

            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

            _isLevel = SceneManager.GetActiveScene().name == "GameLevel";
        }
        
        public void Dispose()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            
            Director.Instance.Remove(this);
        }

        public void TogglePause()
        {
            if (!_isPaused)
                Pause();
            else
                UnPause();
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void UnPause()
        {
            _isPaused = false;
        }

        public void LoadMainMenu()
        {
            _isLevel = false;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void LoadLevel(int level)
        {
            _isLevel = true;
            SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
        }
        
        public void LoadLastLevel()
        {
            LoadLevel(_currentLevel);
        }

        public void LoadNextLevel()
        {
            LoadLevel(_currentLevel + 1);
        }

        public void QuitGame()
        {
            Dispose();
            
            Application.Quit();
        }
    }
}