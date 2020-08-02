using GeekBrainsInternship.Interfaces;
using UnityEngine;

namespace YASMPWRT.Views
{
    public abstract class BaseView<TController> : MonoBehaviour where TController : class
    {
        public TController Controller { get; protected set; }

        private void OnDisable()
        {
            ((IController<TController>) Controller)?.Dispose();
            
            //var controller = Controller as TController;
            
            //controller.Dispose();
        }
    }
}