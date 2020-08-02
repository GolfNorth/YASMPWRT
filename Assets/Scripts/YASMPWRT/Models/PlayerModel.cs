using System.Collections.Generic;
using YASMPWRT.Structs;

namespace YASMPWRT.Models
{
    public class PlayerModel
    {
        private LinkedList<RewindFrame> _rewind = new LinkedList<RewindFrame>();
        private int _maxRewindTime = 8;
        private bool _hasKey;
        private float _speed = 10f;
        private float _jumpForce = 10f;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float JumpForce
        {
            get => _jumpForce;
            set => _jumpForce = value;
        }

        public bool HasKey
        {
            get => _hasKey;
            set => _hasKey = value;
        }

        public LinkedList<RewindFrame> Rewind
        {
            get => _rewind;
            set => _rewind = value;
        }

        public int MaxRewindTime
        {
            get => _maxRewindTime;
            set => _maxRewindTime = value;
        }
    }
}