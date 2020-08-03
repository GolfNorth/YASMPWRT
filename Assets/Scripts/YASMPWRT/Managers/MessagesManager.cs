using System;
using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Views;

namespace YASMPWRT.Managers
{
    public sealed class MessagesManager : IDisposable
    {
        private int _index;
        private string[] _messages;
        private readonly GameManager _gameManager;
        private readonly InputManager _inputManager;
        private readonly MessageBoxController _messageBox;
        
        
        public MessagesManager()
        {
            Director.Instance.Set(this);
            
            _gameManager = Director.Instance.Get<GameManager>();
            _inputManager = Director.Instance.Get<InputManager>();

            var gameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MessageBox"));

            _messageBox = gameObject?.GetComponent<MessageBoxView>()?.Controller;
            
            _inputManager.ActionPressed += OnActionPressed;
            _inputManager.JumpPressed += OnActionPressed;
        }

        public void Dispose()
        {
            _inputManager.ActionPressed -= OnActionPressed;
            _inputManager.JumpPressed -= OnActionPressed;
            
            Director.Instance.Remove(this);
        }

        private void OnActionPressed()
        {
            Hide();
        }
        
        public void Show(string message)
        {
            if (_messageBox is null) return;
            
            _messageBox.Message = message;
            _messageBox.Show();
        }
        
        public void Show(string[] messages)
        {
            if (_messageBox is null || messages.Length == 0) return;

            _gameManager.Pause();

            _index = 0;
            _messages = messages;
            
            Show(messages[_index]);
        }

        public void Hide()
        {
            if (_messages is null || _messages.Length == 1)
            {
                _messageBox?.Hide();
            
                _gameManager.Unpause();
            }
            else if (_messages.Length > _index + 1)
            {
                _index++;
                
                Show(_messages[_index]);
            }
            else
            {
                _index = 0;
                _messages = null;
                
                _messageBox?.Hide();
        
                _gameManager.Unpause();
            }
            
        }
    }
}