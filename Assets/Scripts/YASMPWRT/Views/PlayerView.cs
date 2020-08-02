using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class PlayerView : BaseView<PlayerController>
    {
        private void Awake()
        {
            Controller = new PlayerController(this);
        }
    }
}