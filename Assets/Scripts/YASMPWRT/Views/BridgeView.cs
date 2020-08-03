using System.Collections;
using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public sealed class BridgeView : BaseView<BridgeController>
    {
        [SerializeField] 
        private Transform spriteTransform;
        [SerializeField]
        private float shakeDuration;
        [SerializeField]
        private float shakeAmount;

        private Vector3 _startPosition;
        private bool _isCollapsing;
        
        private void Awake()
        {
            Controller = new BridgeController(this);

            _startPosition = spriteTransform.localPosition;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player") return;

            if (!_isCollapsing)
                StartCoroutine(Shake());
        }
        
        private IEnumerator Shake()
        {
            _isCollapsing = true;
 
            var duration = 0f;
            
            while(duration < shakeDuration)
            {
                spriteTransform.localPosition = _startPosition + Random.insideUnitSphere * shakeAmount;
                
                duration += Time.deltaTime;
                
                yield return null;
            }

            _isCollapsing = false;
            spriteTransform.localPosition = _startPosition;

            Controller.Collapse();
        }
    }
}