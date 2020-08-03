using System.Collections.Generic;
using YASMPWRT.Structs;

namespace YASMPWRT.Models
{
    public sealed class PlayerModel
    {
        private LinkedList<RewindFrame> _rewind = new LinkedList<RewindFrame>();
        private int _maxRewindTime = 8;
        private bool _dead = false;

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

        public bool Dead
        {
            get => _dead;
            set => _dead = value;
        }
    }
}