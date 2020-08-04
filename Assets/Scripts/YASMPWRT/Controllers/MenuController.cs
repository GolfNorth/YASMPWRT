using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Structs;
using YASMPWRT.Views;
using EventType = YASMPWRT.Enums.EventType;

namespace YASMPWRT.Controllers
{
    public sealed class MenuController : IController<MenuController>
    {
        private readonly MenuView _view;
        private readonly MenuModel _model;
        private readonly GameManager _gameManager;
        private readonly LevelManager _levelManager;
        private readonly InputManager _inputManager;
        private readonly AudioManager _audioManager;
        private readonly EventManager _eventManager;
        private readonly MessagesManager _messagesManager;

        private bool IsActive => _view.gameObject.activeSelf && !_messagesManager.IsShown;

        public MenuController(MenuView view, MenuItem[] menuItems)
        {
            _view = view;
            
            _model = new MenuModel
            {
                MenuItems = menuItems,
                CurrentIndex = -1
            };

            _gameManager = Director.Instance.Get<GameManager>();
            _levelManager = Director.Instance.Get<LevelManager>();
            _audioManager = Director.Instance.Get<AudioManager>();
            _messagesManager = Director.Instance.Get<MessagesManager>();

            _audioManager.PlayMusic();

            _inputManager = Director.Instance.Get<InputManager>();
            _inputManager.JumpPressed += Action;
            _inputManager.ActionPressed += Action;
            _inputManager.UpPressed += Up;
            _inputManager.DownPressed += Down;
            _inputManager.CancelPressed += Cancel;

            _eventManager = Director.Instance.Get<EventManager>();
            _eventManager.NewEvent += OnNewEvent;
            
            _view.gameObject.SetActive(!_gameManager.IsLevel);
        }

        public void Dispose()
        {
            _inputManager.JumpPressed -= Action;
            _inputManager.ActionPressed -= Action;
            _inputManager.UpPressed -= Up;
            _inputManager.DownPressed -= Down;
            _inputManager.CancelPressed -= Cancel;
            _eventManager.NewEvent -= OnNewEvent;
        }

        private void Up()
        {
            if (!IsActive) return;
            
            ChangeIndex(_model.CurrentIndex - 1);
        }

        private void Down()
        {
            if (!IsActive) return;
            
            ChangeIndex(_model.CurrentIndex + 1);
        }

        private void ChangeIndex(int newIndex)
        {
            newIndex = newIndex < 1
                ? _model.MenuItems[0].Instance.Controller.IsActive ? 0 : 1
                : newIndex;

            if (_model.CurrentIndex == -1)
            {
                for (var i = 0; i < _model.MenuItems.Length; i++)
                {
                    if (_model.MenuItems[i].Instance.Controller.IsSelected)
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
            if (!IsActive) return;
            
            if (_model.CurrentIndex == -1) return;

            _model.MenuItems[_model.CurrentIndex].Instance.Controller.Action();
        }

        private void Cancel()
        {
            if (!_gameManager.IsLevel) return;
            
            if (_gameManager.IsPaused)
            {
                _view.gameObject.SetActive(false);
                _gameManager.Unpause();
            }
            else
            {
                _gameManager.Pause();
                _view.gameObject.SetActive(true);
            }
        }

        private void OnNewEvent(EventType type)
        {
            switch (type)
            {
                case EventType.MenuItemContinueActivated:
                    _gameManager.ContinueGame();
                    break;
                case EventType.MenuItemNewGameActivated:
                    _gameManager.StartGame();
                    break;
                case EventType.MenuItemMusicActivated:
                    _audioManager.ToggleMusic();
                    break;
                case EventType.MenuItemSoundEffectsActivated:
                    _audioManager.ToggleSoundEffects();
                    break;
                case EventType.MenuItemQuitGameActivated:
                    _gameManager.QuitGame();
                    break;
                case EventType.MenuItemReturnToMenuActivated:
                    _gameManager.GoMainMenu();
                    break;
                case EventType.MenuItemReturnToGameActivated:
                    _view.gameObject.SetActive(false);
                    _gameManager.Unpause();
                    break;
                case EventType.MenuItemResetLevelActivated:
                    _levelManager.RestartLevel();
                    _view.gameObject.SetActive(false);
                    _gameManager.Unpause();
                    break;
                case EventType.MenuItemCreditsActivated:
                    _view.GetComponent<CreditsView>()?.Controller?.Show();
                    break;
                default:
                    break;
            }
        }
    }
}