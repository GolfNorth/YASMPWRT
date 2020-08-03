using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class KeyView : BaseView<KeyController>
    {
        private void Awake()
        {
            Controller = new KeyController(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            Controller.Collect();
        }
    }
}