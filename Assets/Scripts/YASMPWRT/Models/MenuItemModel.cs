namespace YASMPWRT.Models
{
    public class MenuItemModel
    {
        private bool _active = true;
        private bool _selected;
        
        public bool Selected
        {
            get => _selected;
            set => _selected = value;
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }
    }
}