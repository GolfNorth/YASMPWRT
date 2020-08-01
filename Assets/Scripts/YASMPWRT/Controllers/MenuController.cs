using YASMPWRT.Enums;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Structs;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class MenuController
    {
        private MenuView _view;
        private MenuModel _model;
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public MenuController(MenuView view, MenuItem[] menuItems)
        {
            _view = view;
            _model = new MenuModel
            {
                MenuItems = menuItems,
                CurrentIndex = -1
            };
            
            _gameManager = Director.Instance.Get<GameManager>();
            _audioManager = Director.Instance.Get<AudioManager>();
            
            _audioManager.PlayMusic();

            var inputManager = Director.Instance.Get<InputManager>();
            inputManager.JumpPressed += Action;
            inputManager.ActionPressed += Action;
            inputManager.UpPressed += Up;
            inputManager.DownPressed += Down;

            var eventManager = Director.Instance.Get<EventManager>();
            eventManager.MenuItemActivated += OnMenuItemActivated;
        }

        private void Up()
        {
            ChangeIndex(_model.CurrentIndex - 1);
        }

        private void Down()
        {
            ChangeIndex(_model.CurrentIndex + 1);
        }

        private void ChangeIndex(int newIndex)
        {
            newIndex = newIndex < 1 
                ? _gameManager.IsContinueAvailable ? 0 : 1
                : newIndex;
            
            if (_model.CurrentIndex == -1)
            {
                for (var i = 0; i < _model.MenuItems.Length; i++)
                {
                    if (_model.MenuItems[i].Instance.Controller.IsSelected())
                        _model.CurrentIndex = i;
                }

                if (_model.CurrentIndex == -1)
                {
                    _model.CurrentIndex = newIndex;
                }
            }
            
            DeselectMenuItem();

            _model.CurrentIndex = newIndex;
            
            SelectMenuItem();
        }

        private void SelectMenuItem()
        {
            var menuItem = _model.MenuItems[_model.CurrentIndex];
            menuItem.Instance.Controller.Enter();
        }
        
        private void DeselectMenuItem()
        {
            var menuItem = _model.MenuItems[_model.CurrentIndex]; 
            menuItem.Instance.Controller.Exit();
        }

        private void Action()
        {
            if (_model.CurrentIndex == -1) return;
            
            _model.MenuItems[_model.CurrentIndex].Instance.Controller.Action();
        }
        
        private void OnMenuItemActivated(MenuItemType type)
        {
            switch (type)
            {
                case MenuItemType.Continue:
                    _gameManager.LoadLastLevel();
                    break;
                case MenuItemType.NewGame:
                    _gameManager.LoadLevel(1);
                    break;
                case MenuItemType.Music:
                    _audioManager.ToggleMusic();
                    break;
                case MenuItemType.SoundEffects:
                    _audioManager.ToggleSoundEffects();
                    break;
                case MenuItemType.QuitGame:
                    _gameManager.QuitGame();
                    break;
                case MenuItemType.MainMenu:
                    _gameManager.LoadMainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}