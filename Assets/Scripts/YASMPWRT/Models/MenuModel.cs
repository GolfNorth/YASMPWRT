using YASMPWRT.Structs;

namespace YASMPWRT.Models
{
    public class MenuModel
    {
        private int _currentIndex;
        private MenuItem[] _menuItems;

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (value < -1 || value == _menuItems.Length) return;

                _currentIndex = value;
            }
        }

        public MenuItem[] MenuItems
        {
            get => _menuItems;
            set => _menuItems = value;
        }
    }
}