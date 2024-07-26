using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private bool _isRotating;
        private Vector3 _lastMousePosition;
        
        public bool IsRotating() => _isRotating;

        public Vector2 GetInputDelta()
        {
            Vector3 delta = UnityEngine.Input.mousePosition - _lastMousePosition;
            _lastMousePosition = UnityEngine.Input.mousePosition;
            return new Vector2(delta.x, delta.y);
        }

        public void UpdateRotation()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                _isRotating = true;
                _lastMousePosition = UnityEngine.Input.mousePosition;
            }
            
            if (UnityEngine.Input.GetMouseButtonUp(1)) 
                _isRotating = false;
        }
    }
}