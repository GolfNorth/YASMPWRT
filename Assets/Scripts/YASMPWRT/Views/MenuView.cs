using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Structs;

namespace YASMPWRT.Views
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private MenuItem[] menuItems;
        private MenuController _controller;

        public MenuController Controller => _controller;

        private void Awake()
        {
            _controller = new MenuController(this, menuItems);
        }
    }
}