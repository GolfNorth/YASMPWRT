using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public class CoinView : BaseView<CoinController>
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            var playerController = Director.Instance.Get<PlayerController>();
            playerController?.GetCoin();
            
            gameObject.SetActive(false);
        }
    }
}