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

        public bool IsCollapsing
        {
            get => _isCollapsing;
            set => _isCollapsing = value;
        }
        
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
            
            while(duration < shakeDuration && _isCollapsing)
            {
                spriteTransform.localPosition = _startPosition + Random.insideUnitSphere * shakeAmount;
                
                duration += Time.deltaTime;
                
                yield return null;
            }
            
            spriteTransform.localPosition = _startPosition;

            Controller.Collapse();
            
            _isCollapsing = false;
        }
    }
}