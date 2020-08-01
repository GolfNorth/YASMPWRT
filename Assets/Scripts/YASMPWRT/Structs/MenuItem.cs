using System;
using UnityEngine;
using YASMPWRT.Enums;
using YASMPWRT.Views;

namespace YASMPWRT.Structs
{
    [Serializable]
    public struct MenuItem
    {
        [SerializeField]
        private MenuItemType _type;
        [SerializeField]
        private MenuItemView _instance;

        public MenuItemType Type => _type;
        public MenuItemView Instance => _instance;
    }
}