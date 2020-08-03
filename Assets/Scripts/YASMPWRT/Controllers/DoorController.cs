using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class DoorController : IController<DoorController>
    {
        private readonly DoorView _view;
        private readonly LevelManager _levelManager;

        public DoorController(DoorView view)
        {
            _view = view;
            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
        }

        public void Enter()
        {
            _levelManager.EnterDoor();
        }
        
        public void Open()
        {
            _view.Open();
        }

        public void Reset()
        {
            _view.Close();
        }
    }
}