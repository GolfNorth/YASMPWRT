using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class CreditsController : IController<CreditsController>
    {
        private string _message;
        private readonly CreditsView _view;
        private readonly MessagesManager _messagesManager;

        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public CreditsController(CreditsView view)
        {
            _view = view;
            _messagesManager = Director.Instance.Get<MessagesManager>();
        }
        
        public void Dispose()
        {
        }
        
        public void Show()
        {
            _messagesManager.Show(_message);
        }
    }
}