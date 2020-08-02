﻿using GeekBrainsInternship.Interfaces;
using UnityEngine;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Structs;
using YASMPWRT.Views;
using EventType = YASMPWRT.Enums.EventType;

namespace YASMPWRT.Controllers
{
    public class MenuController : IController<MenuController>

    {
    private MenuView _view;
    private MenuModel _model;
    private readonly GameManager _gameManager;
    private readonly InputManager _inputManager;
    private readonly AudioManager _audioManager;
    private readonly EventManager _eventManager;

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

        _inputManager = Director.Instance.Get<InputManager>();
        _inputManager.JumpPressed += Action;
        _inputManager.ActionPressed += Action;
        _inputManager.UpPressed += Up;
        _inputManager.DownPressed += Down;

        _eventManager = Director.Instance.Get<EventManager>();
        _eventManager.NewEvent += OnNewEvent;
    }

    public void Dispose()
    {
        _inputManager.JumpPressed -= Action;
        _inputManager.ActionPressed -= Action;
        _inputManager.UpPressed -= Up;
        _inputManager.DownPressed -= Down;
        _eventManager.NewEvent -= OnNewEvent;
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
        if (_model.CurrentIndex == -1) return;

        _model.MenuItems[_model.CurrentIndex].Instance.Controller.Action();
    }

    private void OnNewEvent(EventType type)
    {
        switch (type)
        {
            case EventType.MenuItemContinueActivated:
                _gameManager.LoadLastLevel();
                break;
            case EventType.MenuItemNewGameActivated:
                _gameManager.LoadLevel(1);
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
                _gameManager.LoadMainMenu();
                break;
            case EventType.MenuItemReturnToGameActivated:
                _gameManager.TogglePause();
                break;
            default:
                break;
        }
    }
    }
}