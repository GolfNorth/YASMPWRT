using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Interfaces;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public sealed class PlayerView : BaseView<PlayerController>, ILateTickable
    {
        private bool _dead;
        private bool _grounded;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int FallHash = Animator.StringToHash("Fall");
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
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Director.Instance.Get<UpdateManager>()?.Add(this);
        }

        private void OnDisable()
        {
            Director.Instance?.Get<UpdateManager>().Remove(this);
        }

        public void Move(float direction, float speed)
        {
            var velocity = _rigidbody2D.velocity;

            if (_grounded)
            {
                velocity.x = direction * speed;
            }
            else if (velocity.y != 0) 
            {
                
                velocity.x += direction * speed * Time.deltaTime * 2f;
                velocity.x = Mathf.Abs(velocity.x) > speed ? Mathf.Sign(velocity.x) * speed : velocity.x;
            }
            
            _rigidbody2D.velocity = velocity;
        }

        public bool Jump(float force, bool certainly)
        {
            if (_grounded || certainly)
            {
                _rigidbody2D.AddForce(Vector2.up * force);

                return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            
            _grounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.isTrigger) return;
            
            _grounded = false;
        }

        public void LateTick()
        {
            if (Dead)
            {
                _animator.SetBool(IdleHash, false);
                _animator.SetBool(RunHash, false);
                _animator.SetBool(JumpHash, false);
                _animator.SetBool(FallHash, false);
                _animator.SetBool(DeadHash, true);
                
                return;
            }
            
            var velocity = _rigidbody2D.velocity;
            
            _animator.SetBool(IdleHash, velocity == Vector2.zero);
            _animator.SetBool(RunHash, velocity.y == 0 && velocity.x != 0);
            _animator.SetBool(JumpHash, velocity.y < 0);
            _animator.SetBool(FallHash, velocity.y > 0);
            _animator.SetBool(DeadHash, false);

            _spriteRenderer.flipX = velocity.x < 0;
        }
    }
}