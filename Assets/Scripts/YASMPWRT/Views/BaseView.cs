using UnityEngine;
using YASMPWRT.Interfaces;

namespace YASMPWRT.Views
{
    public abstract class BaseView<TController> : MonoBehaviour where TController : class, IController<TController>
    {
        public TController Controller { get; protected set; }

        private void OnDestroy()
        {
            (Controller)?.Dispose();
        }
    }
}