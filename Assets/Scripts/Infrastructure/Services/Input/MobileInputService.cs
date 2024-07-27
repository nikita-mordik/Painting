using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const float DoubleTapTime = 0.3f;
        
        private bool _isRotating;
        private float _lastTapTime;
        
        public bool IsRotating() => 
            _isRotating && UnityEngine.Input.touchCount > 1 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved;

        public bool HasClick()
        {
            return UnityEngine.Input.touchCount > 0;
        }

        public Vector3 GetInput()
        {
            return UnityEngine.Input.GetTouch(0).position;
        }

        public Vector2 GetInputDelta()
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            return touch.deltaPosition;
        }

        public void UpdateRotation()
        {
            if (UnityEngine.Input.touchCount > 1)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    float currentTime = Time.time;
                    
                    if (currentTime - _lastTapTime < DoubleTapTime) 
                        _isRotating = !_isRotating;
                    
                    _lastTapTime = currentTime;
                }
            }
        }
    }
}