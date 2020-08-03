using System;
using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Views;

namespace YASMPWRT.Managers
{
    public sealed class MessagesManager : IDisposable
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private MessageBoxController _messageBox;
        
        public MessagesManager()
        {
            Director.Instance.Set(this);
            
            _gameManager = Director.Instance.Get<GameManager>();
            _inputManager = Director.Instance.Get<InputManager>();

            var gameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MessageBox"));

            _messageBox = gameObject?.GetComponent<MessageBoxView>()?.Controller;
            
            _inputManager.AnyKeyPressed += OnAnyKeyPressed;
        }

        public void Dispose()
        {
            _inputManager.AnyKeyPressed -= OnAnyKeyPressed;
            
            Director.Instance.Remove(this);
        }

        private void OnAnyKeyPressed()
        {
            Hide();
        }
        
        public void Show(string message)
        {
            if (_messageBox is null) return;
            
            _gameManager.Pause();
            
            _messageBox.Message = message;
            _messageBox.Show();
        }

        public void Hide()
        {
            _gameManager.Unpause();
            
            _messageBox?.Hide();
        }
    }
}