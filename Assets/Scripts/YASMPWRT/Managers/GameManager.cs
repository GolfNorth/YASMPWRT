using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YASMPWRT.Managers
{
    public class GameManager : IDisposable
    {
        private int _lastLevel;
        private bool _isPaused;
        private bool _isLevel;

        public int LastLevel
        {
            get => _lastLevel;
            set => _lastLevel = value;
        }
        
        public bool IsContinueAvailable => _lastLevel > 1;
        public bool IsPaused => _isPaused;
        public bool IsLevel => _isLevel;

        public GameManager()
        {
            Director.Instance.Set(this);
            
            _isLevel = SceneManager.GetActiveScene().name == "GameLevel" || SceneManager.GetActiveScene().name == "TestLevel";
            _lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        }
        
        public void Dispose()
        {
            PlayerPrefs.SetInt("LastLevel", _lastLevel);
            
            Director.Instance.Remove(this);
        }

        public void TogglePause()
        {
            if (!_isPaused)
                Pause();
            else
                Unpause();
        }

        public void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            _isPaused = false;
            Time.timeScale = 1;
        }

        public void StartGame()
        {
            LoadLevel(0);
        }

        public void ContinueGame()
        {
            LoadLevel(_lastLevel);
        }

        public void GoMainMenu()
        {
            _isLevel = false;
            _isPaused = false;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        private void LoadLevel(int level)
        {
            _isLevel = true;
            _isPaused = false;
            SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            Dispose();
            
            Application.Quit();
        }
    }
}