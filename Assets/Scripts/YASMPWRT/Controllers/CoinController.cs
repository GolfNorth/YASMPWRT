using GeekBrainsInternship.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class CoinController : IController<CoinController>
    {
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
            _levelManager.CollectCoin();
            _view.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _view.gameObject.SetActive(true);
        }
    }
}