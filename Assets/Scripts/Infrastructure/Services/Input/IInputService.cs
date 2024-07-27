using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Input
{
    public interface IInputService
    {
        bool IsRotating();
        bool HasClick();
        Vector3 GetInput();
        Vector2 GetInputDelta();
        void UpdateRotation();
    }
}