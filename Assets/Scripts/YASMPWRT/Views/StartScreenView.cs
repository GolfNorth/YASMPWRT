using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class StartScreenView : MonoBehaviour
    {
        private StartScreenController _controller;

        public StartScreenController Controller => _controller;

        private void Awake()
        {
            _controller = new StartScreenController(this);
        }
    }
}