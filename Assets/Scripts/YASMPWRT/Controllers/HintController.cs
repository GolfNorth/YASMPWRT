using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class HintController : IController<HintController>
    {
        private string[] _messages;
        private readonly HintView _view;
        private readonly MessagesManager _messagesManager;

        public string[] Messages
        {
            get => _messages;
            set => _messages = value;
        }

        public HintController(HintView view)
        {
            _view = view;
            _messagesManager = Director.Instance.Get<MessagesManager>();
        }
        
        public void Dispose()
        {
        }

        public void Show()
        {
            _messagesManager.Show(_messages);
            
            _view.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _view.gameObject.SetActive(true);
        }
    }
}