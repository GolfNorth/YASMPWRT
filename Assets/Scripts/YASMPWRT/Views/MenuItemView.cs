using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YASMPWRT.Controllers;
using YASMPWRT.Enums;

namespace YASMPWRT.Views
{
    public sealed class MenuItemView : BaseView<MenuItemController>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] 
        private float selectedFontSizeFactor = 1.2f;
        private int _defaultFontSize;
        [SerializeField]
        private MenuItemType type;
        private Animator _animator;
        private Text _textComponent;
        private static readonly int SelectedHash = Animator.StringToHash("Selected");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _textComponent = GetComponent<Text>();
            _defaultFontSize = _textComponent.fontSize;
            
            Controller = new MenuItemController(this, type);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Controller.Enter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Controller.Exit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Controller.Action();
        }

        public void StartAnimation()
        {
            _textComponent.fontSize = (int) (_defaultFontSize * selectedFontSizeFactor);
            _animator = GetComponent<Animator>();
            _animator.SetBool(SelectedHash, true);
        }

        public void StopAnimation()
        {
            _textComponent.fontSize = _defaultFontSize;
            _animator = GetComponent<Animator>();
            _animator.SetBool(SelectedHash, false);
        }

        public void ChangeText(string text)
        {
            _textComponent.text = text;
        }
    }
}