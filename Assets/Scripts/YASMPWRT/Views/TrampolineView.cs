using System.Collections;
using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public class TrampolineView : MonoBehaviour
    {
        private bool _unclenched;
        private Animator _animator;
        private BoxCollider2D _solidCollider;
        private BoxCollider2D _triggerCollider;
        private static readonly int UnclenchedHash = Animator.StringToHash("Unclenched");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ThrowUp(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            ThrowUp(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;
            
            _animator.SetBool(UnclenchedHash, false);
        }
        
        private IEnumerator Countdown()
        {
            var duration = 2f;
            var normalizedTime = 0f;
            
            while(normalizedTime <= 1f)
            {
                normalizedTime += Time.deltaTime / duration;
                
                yield return null;
            }
            
            _unclenched = false;
        }

        private void ThrowUp(Collider2D other)
        {
            if (_unclenched || other.gameObject.name != "Player") return;
            
            var playerController = Director.Instance.Get<PlayerController>();
            playerController?.ThrowUp();

            _animator.SetBool(UnclenchedHash, true);
            
            _unclenched = true;
            
            StartCoroutine(Countdown());
        }
    }
}