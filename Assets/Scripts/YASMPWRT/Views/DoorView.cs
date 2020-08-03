using UnityEngine;
using YASMPWRT.Controllers;

namespace YASMPWRT.Views
{
    public class DoorView : BaseView<DoorController>
    {
        [SerializeField] 
        private Sprite closedDoor;
        [SerializeField] 
        private Sprite openedDoor;

        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            Controller = new DoorController(this);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Player") return;

            Controller.Enter();
        }

        public void Open()
        {
            _spriteRenderer.sprite = openedDoor;
        }
        
        public void Close()
        {
            _spriteRenderer.sprite = closedDoor;
        }
    }
}