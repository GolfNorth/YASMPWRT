using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YASMPWRT.Controllers;
using YASMPWRT.Enums;

namespace YASMPWRT.Views
{
    public class MenuItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] 
        private float selectedFontSizeFactor = 1.2f;
        private int _defaultFontSize;
        [SerializeField]
        private MenuItemType type;
        private Animator _animator;
        private Text _textComponent;
        private MenuItemController _controller;
        private static readonly int Selected = Animator.StringToHash("Selected");

        public MenuItemController Controller => _controller;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _textComponent = GetComponent<Text>();
            _defaultFontSize = _textComponent.fontSize;
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
            _textComponent.fontSize = (int) (_defaultFontSize * selectedFontSizeFactor);
            _animator = GetComponent<Animator>();
            _animator.SetBool(Selected, true);
        }

        public void StopAnimation()
        {
            _textComponent.fontSize = _defaultFontSize;
            _animator = GetComponent<Animator>();
            _animator.SetBool(Selected, false);
        }

        public void ChangeText(string text)
        {
            _textComponent.text = text;
        }
    }
}