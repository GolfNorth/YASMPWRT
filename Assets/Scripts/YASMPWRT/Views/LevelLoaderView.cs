using UnityEngine;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public class LevelLoaderView : MonoBehaviour
    {
        public int level = 1;
        
        private void Awake()
        {
            var levelManager = Director.Instance.Get<LevelManager>();
            
            levelManager.LoadLevel(level);
        }
    }
}