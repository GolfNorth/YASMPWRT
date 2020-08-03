using UnityEngine;

namespace YASMPWRT.Managers
{
    /// <summary>
    /// Inherit from this base class to create a singleton.
    /// e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Fields

        // Check to see if we're about to be destroyed.
        private static bool _shuttingDown;
        private static object _lock = new object();
        private static T _instance;

        #endregion
        

        #region Properties

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }
 
                lock (_lock)
                {
                    if (_instance != null) return _instance;
                    
                    _instance = (T) FindObjectOfType(typeof(T));
                    
                    if (_instance != null) return _instance;
                    
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";
                    
                    DontDestroyOnLoad(singletonObject);

                    return _instance;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }
        
        private void OnDestroy()
        {
            _shuttingDown = true;
        }

        #endregion
    }
}