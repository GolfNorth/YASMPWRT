using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class HintView : BaseView<HintController>
    {
        [SerializeField, Multiline]
        private string[] messages;
        
        private void Awake()
        {
            Controller = new HintController(this)
            {
                Messages = messages
            };
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            Controller.Show();
        }
    }
}