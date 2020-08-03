using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class CreditsView : BaseView<CreditsController>
    {
        [SerializeField, Multiline]
        private string message;
        
        private void Awake()
        {
            Controller = new CreditsController(this)
            {
                Message = message
            };
        }
    }
}