using YASMPWRT.Enums;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class MenuItemController
    {
        private MenuItemModel _model;
        private MenuItemView _view;
        private MenuItemType _type;
        private EventManager _eventManager;

        public MenuItemController(MenuItemView view, MenuItemType type)
        {
            _view = view;
            _type = type;
            _model = new MenuItemModel();

            _eventManager = Director.Instance.Get<EventManager>();
        }
        
        public void Enter()
        {
            if (_model.Selected) return;
            
            _model.Selected = true;
            _view.StartAnimation();
        }

        public void Exit()
        {
            if (!_model.Selected) return;
            
            _model.Selected = false;
            _view.StopAnimation();
        }

        public void Action()
        {
            _eventManager.MenuItemActivate(_type);
        }

        public bool IsSelected()
        {
            return _model.Selected;
        }
    }
}