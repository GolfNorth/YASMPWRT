using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class KeyController : IController<KeyController>
    {
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
            _levelManager.CollectKey();
            _view.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _view.gameObject.SetActive(true);
        }
    }
}