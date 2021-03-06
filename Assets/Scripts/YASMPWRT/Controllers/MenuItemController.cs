﻿using UnityEngine.UI;
using YASMPWRT.Enums;
using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Views;
using EventType = YASMPWRT.Enums.EventType;

namespace YASMPWRT.Controllers
{
    public sealed class MenuItemController : IController<MenuItemController>
    {
        private readonly MenuItemModel _model;
        private readonly MenuItemView _view;
        private readonly MenuItemType _type;
        private readonly EventManager _eventManager;
        private readonly AudioManager _audioManager;
        private readonly MessagesManager _messagesManager;
        private readonly Text _textComponent;

        public bool IsActive => _model.Active;
        public bool IsSelected => _model.Selected;

        public MenuItemController(MenuItemView view, MenuItemType type)
        {
            _view = view;
            _type = type;
            _model = new MenuItemModel();

            _eventManager = Director.Instance.Get<EventManager>();
            _messagesManager = Director.Instance.Get<MessagesManager>();

            if (type == MenuItemType.Continue)
            {
                var gameManager = Director.Instance.Get<GameManager>();
                
                if (gameManager.CurrentLevel < 2)
                {
                    _view.gameObject.SetActive(false);

                    _model.Active = false;
                }
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

        public void Dispose()
        {
            switch (_type)
            {
                case MenuItemType.Music:
                    _audioManager.MusicToggled -= OnMusicToggled;
                    break;
                case MenuItemType.SoundEffects:
                    _audioManager.SoundEffectsToggled -= OnSoundEffectsToggled;
                    break;
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
            if (_messagesManager.IsShown) return;
            
            switch (_type)
            {
                case MenuItemType.Continue:
                    _eventManager.NewEventInvoke(EventType.MenuItemContinueActivated);
                    break;
                case MenuItemType.NewGame:
                    _eventManager.NewEventInvoke(EventType.MenuItemNewGameActivated);
                    break;
                case MenuItemType.Music:
                    _eventManager.NewEventInvoke(EventType.MenuItemMusicActivated);
                    break;
                case MenuItemType.SoundEffects:
                    _eventManager.NewEventInvoke(EventType.MenuItemSoundEffectsActivated);
                    break;
                case MenuItemType.QuitGame:
                    _eventManager.NewEventInvoke(EventType.MenuItemQuitGameActivated);
                    break;
                case MenuItemType.ReturnToMenu:
                    _eventManager.NewEventInvoke(EventType.MenuItemReturnToMenuActivated);
                    break;
                case MenuItemType.ReturnToGame:
                    _eventManager.NewEventInvoke(EventType.MenuItemReturnToGameActivated);
                    break;
                case MenuItemType.RestartLevel:
                    _eventManager.NewEventInvoke(EventType.MenuItemResetLevelActivated);
                    break;
                case MenuItemType.Credits:
                    _eventManager.NewEventInvoke(EventType.MenuItemCreditsActivated);
                    break;
                default:
                    break;
            }
        }
    }
}