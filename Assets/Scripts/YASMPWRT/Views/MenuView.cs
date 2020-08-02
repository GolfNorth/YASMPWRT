using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Structs;

namespace YASMPWRT.Views
{
    public sealed class MenuView : BaseView<MenuController>
    {
        [SerializeField]
        private MenuItem[] menuItems;

        private void Awake()
        {
            Controller = new MenuController(this, menuItems);
        }
    }
}