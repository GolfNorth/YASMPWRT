using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class FlagView : BaseView<FlagController>
    {
        public Vector3 Position => transform.position;
        
        private void Awake()
        {
            Controller = new FlagController(this);
        }
    }
}