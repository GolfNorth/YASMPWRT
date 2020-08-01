using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class StartScreenController
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

        private void OnAnyKeyPressed()
        {
            _gameManager.LoadMainMenu();
            _inputManager.AnyKeyPressed -= OnAnyKeyPressed;
        }
    }
}