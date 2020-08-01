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

        public MenuController(MenuView view, MenuItem[] menuItems)
        {
            _view = view;
            _model = new MenuModel
            {
                MenuItems = menuItems,
                CurrentIndex = -1
            };

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
            if (_model.CurrentIndex == -1)
            {
                for (var i = 0; i < _model.MenuItems.Length; i++)
                {
                    if (_model.MenuItems[i].Instance.Controller.IsSelected())
                        _model.CurrentIndex = i;
                }

                if (_model.CurrentIndex == -1)
                {
                    newIndex = 0;
                    _model.CurrentIndex = 0;
                }
                else
                {
                    newIndex = newIndex == 0
                        ? _model.CurrentIndex + 1
                        : _model.CurrentIndex - 1;
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
            _model.MenuItems[_model.CurrentIndex].Instance.Controller.Action();
        }
        
        private void OnMenuItemActivated(MenuItemType type)
        {
            switch (type)
            {
                case MenuItemType.Continue:
                    break;
                case MenuItemType.NewGame:
                    break;
                case MenuItemType.Music:
                    break;
                case MenuItemType.SFX:
                    break;
                case MenuItemType.QuitGame:
                    break;
                case MenuItemType.MainMenu:
                    break;
                default:
                    break;
            }
        }
    }
}