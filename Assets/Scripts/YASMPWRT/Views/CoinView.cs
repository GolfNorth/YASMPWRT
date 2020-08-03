using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class CoinView : BaseView<CoinController>
    {
        private void Awake()
        {
            Controller = new CoinController(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            Controller.Collect();
        }
    }
}