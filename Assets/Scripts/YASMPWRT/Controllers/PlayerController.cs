using GeekBrainsInternship.Interfaces;
using YASMPWRT.Models;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class PlayerController : IController<PlayerController>
    {
        private PlayerView _view;
        private PlayerModel _model;
        
        public PlayerController(PlayerView view)
        {
            _view = view;
            _model = new PlayerModel();
        }
        
        public void Dispose()
        {
        }
    }
}