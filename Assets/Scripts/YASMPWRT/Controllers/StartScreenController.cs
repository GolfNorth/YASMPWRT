using GeekBrainsInternship.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class StartScreenController : IController<StartScreenController>
    {
        private StartScreenView _view;
        private InputManager _inputManager;
        private GameManager _gameManager;

        public StartScreenController(StartScreenView view)
        {
            _view = view;
            _inputManager = Director.Instance.Get<InputManager>();
            _gameManager = Director.Instance.Get<GameManager>();
            
            _inputManager.AnyKeyPressed += OnAnyKeyPressed;
        }

        public void Dispose()
        {
            _inputManager.AnyKeyPressed -= OnAnyKeyPressed;
        }

        private void OnAnyKeyPressed()
        {
            _gameManager.GoMainMenu();
            _inputManager.AnyKeyPressed -= OnAnyKeyPressed;
        }
    }
}