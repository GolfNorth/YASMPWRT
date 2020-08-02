using GeekBrainsInternship.Interfaces;
using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public sealed class PlayerView : BaseView<PlayerController>, ILateTickable
    {
        private bool _dead;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private static readonly int VelocityXHash = Animator.StringToHash("VelocityX");
        private static readonly int VelocityYHash = Animator.StringToHash("VelocityY");
        private static readonly int DeadHash = Animator.StringToHash("Dead");

        public bool Dead
        {
            get => _dead;
            set
            {
                _animator.SetBool(DeadHash, value);
                _dead = value;
            }
        }
        
        public Vector2 Velocity
        {
            get
            {
                var _ = _rigidbody2D.velocity;
                
                return new Vector2(_.x, _.y);
            }

            set => _rigidbody2D.velocity = value;
        }

        public Vector3 Position
        {
            get
            {
                var _ = transform.position;
                
                return new Vector3(_.x, _.y, _.z);
            }

            set => transform.position = value;
        }
        
        private void Awake()
        {
            Controller = new PlayerController(this);

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            Director.Instance.Get<UpdateManager>()?.Add(this);
        }

        private void OnDisable()
        {
            Director.Instance?.Get<UpdateManager>().Remove(this);
        }

        public void LateTick()
        {
            var velocity = _rigidbody2D.velocity;
            
            _animator.SetFloat(VelocityXHash, velocity.x);
            _animator.SetFloat(VelocityYHash, velocity.y);
        }
    }
}