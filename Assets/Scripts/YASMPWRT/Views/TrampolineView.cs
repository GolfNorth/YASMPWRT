using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class TrampolineView : BaseView<TrampolineController>
    {
        [SerializeField]
        private float power;        
        [SerializeField]
        private float reloadSpeed;
        private Animator _animator;
        private BoxCollider2D _solidCollider;
        private BoxCollider2D _triggerCollider;
        private static readonly int UnclenchedHash = Animator.StringToHash("Unclenched");

        private void Awake()
        {
            Controller = new TrampolineController(this)
            {
                Flipped = transform.rotation.x != 0,
                ReloadSpeed = reloadSpeed,
                Power = power
            };
            
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Throw(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Throw(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;
            
            _animator.SetBool(UnclenchedHash, false);
        }

        private void Throw(Collider2D other)
        {
            if (Controller.IsReloading || other.gameObject.name != "Player") return;
            
            Controller.Throw();

            _animator.SetBool(UnclenchedHash, true);
        }
    }
}