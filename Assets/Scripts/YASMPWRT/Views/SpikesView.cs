using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class SpikesView : BaseView<SpikesController>
    {
        private void Awake()
        {
            Controller = new SpikesController(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            Controller.Prick();
        }
    }
}