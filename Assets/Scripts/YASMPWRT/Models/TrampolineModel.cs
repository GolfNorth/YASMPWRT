namespace YASMPWRT.Models
{
    public sealed class TrampolineModel
    {
        private float _power;
        private bool _flipped;
        private bool _reloading;
        private float _reloadSpeed;

        public float Power
        {
            get => _power;
            set => _power = value;
        }

        public bool Flipped
        {
            get => _flipped;
            set => _flipped = value;
        }

        public bool Reloading
        {
            get => _reloading;
            set => _reloading = value;
        }

        public float ReloadSpeed
        {
            get => _reloadSpeed;
            set => _reloadSpeed = value;
        }
    }
}