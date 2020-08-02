using GeekBrainsInternship.Interfaces;
using UnityEngine;

namespace YASMPWRT.Views
{
    public abstract class BaseView<TController> : MonoBehaviour where TController : class
    {
        public TController Controller { get; protected set; }

        private void OnDestroy()
        {
            ((IController<TController>) Controller)?.Dispose();
        }
    }
}