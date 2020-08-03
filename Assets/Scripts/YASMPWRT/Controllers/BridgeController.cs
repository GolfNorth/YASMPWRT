using YASMPWRT.Interfaces;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class BridgeController : IController<BridgeController>
    {
        private readonly BridgeView _view;
        
        public BridgeController(BridgeView view)
        {
            _view = view;
        }
        
        public void Dispose()
        {
        }

        public void Collapse()
        {
            _view.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _view.gameObject.SetActive(true);
        }
    }
}