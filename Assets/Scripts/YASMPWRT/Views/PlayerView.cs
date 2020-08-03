using UnityEngine;
using YASMPWRT.Controllers;
using YASMPWRT.Interfaces;
using YASMPWRT.Managers;

namespace YASMPWRT.Views
{
    public sealed class PlayerView : BaseView<PlayerController>, ILateTickable
    {
        [SerializeField]
        private Vector2 speed;
        private bool _grounded;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int FallHash = Animator.StringToHash("Fall");
        private static readonly int DeadHash = Animator.StringToHash("Dead");

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

        public void Spawn(Vector3 position)
        {
            transform.position = position;
        }

        public void Run(float direction)
        {
            var velocity = _rigidbody2D.velocity;

            velocity.x = _grounded
                ? direction * speed.x
                : velocity.x + direction * speed.x * Time.deltaTime * 2f;
            velocity.x = Mathf.Abs(velocity.x) > speed.x 
                ? Mathf.Sign(velocity.x) * speed.x
                : velocity.x;

            _rigidbody2D.velocity = velocity;
        }

        public bool Jump(float direction, bool certainly)
        {
            if (!_grounded && !certainly) return false;
            
            var velocity = _rigidbody2D.velocity;
            
            velocity.y = direction * speed.y;

            _rigidbody2D.velocity = velocity;
                
            return true;
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
            if (Controller.IsDead)
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