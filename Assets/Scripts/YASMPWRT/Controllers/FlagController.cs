using UnityEngine;
using YASMPWRT.Interfaces;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public class FlagController : IController<FlagController>
    {
        private readonly FlagView _view;

        public Vector3 Position => _view.Position;
        
        public FlagController(FlagView view)
        {
            _view = view;
        }
        
        public void Dispose()
        {
        }
    }
}