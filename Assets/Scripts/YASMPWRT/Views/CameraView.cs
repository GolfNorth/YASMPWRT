using UnityEngine;

namespace YASMPWRT.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField]
        private float maxAspectRatio = 16f / 9f;
        
        private void Awake()
        {
            var windowAspect = Screen.width / (float)Screen.height;
            
            if (windowAspect <= maxAspectRatio) return; 
            
            var scaleHeight = windowAspect / maxAspectRatio;
            var cameraComponent = GetComponent<Camera>();
            var scaleWidth = 1.0f / scaleHeight;
            var rect = cameraComponent.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cameraComponent.rect = rect;
        }
    }
}