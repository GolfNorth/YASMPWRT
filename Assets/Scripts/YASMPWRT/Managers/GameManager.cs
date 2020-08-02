using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YASMPWRT.Managers
{
    public class GameManager : IDisposable
    {
        private int _currentLevel;
        private bool _isPaused;

        public bool IsContinueAvailable => _currentLevel > 1;
        public bool IsPaused => _isPaused;

        public GameManager()
        {
            Director.Instance.Set(this);

            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        
        public void Dispose()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            
            Director.Instance.Remove(this);
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void LoadLevel(int level)
        {
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