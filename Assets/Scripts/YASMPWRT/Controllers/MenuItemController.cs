using UnityEngine.UI;
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
        private AudioManager _audioManager;
        private Text _textComponent;

        public MenuItemController(MenuItemView view, MenuItemType type)
        {
            _view = view;
            _type = type;
            _model = new MenuItemModel();

            _eventManager = Director.Instance.Get<EventManager>();

            if (type == MenuItemType.Continue)
            {
                var _gameManager = Director.Instance.Get<GameManager>();
                
                if (!_gameManager.IsContinueAvailable)
                    _view.gameObject.SetActive(false);
            }

            if (type == MenuItemType.Music || type == MenuItemType.SoundEffects)
            {
                _audioManager = Director.Instance.Get<AudioManager>();
                _textComponent = _view.GetComponent<Text>();
                
                switch (type)
                {
                    case MenuItemType.Music:
                        OnMusicToggled();
                
                        _audioManager.MusicToggled += OnMusicToggled;
                        break;
                    case MenuItemType.SoundEffects:
                        OnSoundEffectsToggled();

                        _audioManager.SoundEffectsToggled += OnSoundEffectsToggled;
                        break;
                }
            }
        }

        private void OnMusicToggled()
        {
            var status = _audioManager.IsMusicOn ? "On" : "Off";
                        
            _textComponent.text = $"Music: {status}";
        }
        
        private void OnSoundEffectsToggled()
        {
            var status = _audioManager.IsSoundEffectsOn ? "On" : "Off";
                        
            _textComponent.text = $"SFX: {status}";
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