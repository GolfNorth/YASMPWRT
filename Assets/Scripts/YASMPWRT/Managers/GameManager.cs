using System;
using UnityEngine.SceneManagement;

namespace YASMPWRT.Managers
{
    public class GameManager : IDisposable
    {
        public GameManager()
        {
            Director.Instance.Set(this);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void LoadLevel(int level)
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }
}