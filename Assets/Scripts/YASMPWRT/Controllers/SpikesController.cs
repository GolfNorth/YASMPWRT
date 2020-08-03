using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class SpikesController : IController<SpikesController>
    {
        private readonly LevelManager _levelManager;

        public SpikesController(SpikesView view)
        {
            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
        }

        public void Prick()
        {
            _levelManager.KillPlayer();
        }
    }
}