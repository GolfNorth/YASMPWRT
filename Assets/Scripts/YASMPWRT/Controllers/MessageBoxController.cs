using YASMPWRT.Interfaces;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class MessageBoxController : IController<MessageBoxController>
    {
        private readonly MessageBoxView _view;

        public string Message
        {
            get => _view.Message;
            set => _view.Message = value;
        }

        public MessageBoxController(MessageBoxView view)
        {
            _view = view;
        }
        
        public void Dispose()
        {
        }

        public void Show()
        {
            _view.SetActive(true);
        }

        public void Hide()
        {
            _view.SetActive(false);
        }
    }
}