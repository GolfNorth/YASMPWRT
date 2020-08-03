using UnityEngine;
using UnityEngine.UI;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class MessageBoxView : BaseView<MessageBoxController>
    {
        [SerializeField]
        private Text textComponent;
        [SerializeField]
        private GameObject messagePanel;

        public string Message
        {
            get => textComponent.text;
            set => textComponent.text = value;
        }
        
        private void Awake()
        {
            Controller = new MessageBoxController(this);
            
            DontDestroyOnLoad(gameObject);
        }

        public void SetActive(bool value)
        {
            messagePanel.SetActive(value);
        }
    }
}