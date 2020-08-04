using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class KeyController : IController<KeyController>
    {
        private bool _collected;
        private readonly KeyView _view;
        private readonly LevelManager _levelManager;

        public KeyController(KeyView view)
        {
            _view = view;
            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
        }

        public void Collect()
        {
            if (_collected) return;

            _levelManager.CollectKey();
            _view.gameObject.SetActive(false);

            _collected = true;
        }

        public void Reset()
        {
            _collected = false;
            
            _view.gameObject.SetActive(true);
        }
    }
}