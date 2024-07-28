using FreedLOW.Painting.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace FreedLOW.Painting.Game
{
    public class ObjectRotator : MonoBehaviour
    {
        public float rotationSpeed = 100f;
        
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

#if UNITY_IOS || UNITY_ANDROID
        private void Start()
        {
            rotationSpeed = 20f;
        }
#endif

        private void Update()
        {
            _inputService.UpdateRotation();

            if (_inputService.IsRotating())
            {
                Vector2 delta = _inputService.GetInputDelta();
                float rotationX = -delta.x * rotationSpeed * Time.deltaTime;
                float rotationY = delta.y * rotationSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);
            }
        }
    }
}