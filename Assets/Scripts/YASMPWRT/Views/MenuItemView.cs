using UnityEngine;
using UnityEngine.EventSystems;
using YASMPWRT.Controllers;
using YASMPWRT.Enums;

namespace YASMPWRT.Views
{
    public class MenuItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private MenuItemType type;
        private Animator _animator;
        private MenuItemController _controller;
        private static readonly int Selected = Animator.StringToHash("Selected");

        public MenuItemController Controller => _controller;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = new MenuItemController(this, type);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _controller.Enter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _controller.Exit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _controller.Action();
        }

        public void StartAnimation()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Selected, true);
        }

        public void StopAnimation()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Selected, false);
        }
    }
}