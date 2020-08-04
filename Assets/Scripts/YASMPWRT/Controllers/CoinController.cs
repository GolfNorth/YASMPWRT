using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class CoinController : IController<CoinController>
    {
        private bool _collected;
        private readonly CoinView _view;
        private readonly LevelManager _levelManager;

        public CoinController(CoinView view)
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
            
            _levelManager.CollectCoin();
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