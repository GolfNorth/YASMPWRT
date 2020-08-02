using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class StartScreenView : BaseView<StartScreenController>
    {
        private void Awake()
        {
            Controller = new StartScreenController(this);
        }
    }
}