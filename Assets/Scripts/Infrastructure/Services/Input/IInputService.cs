using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Input
{
    public interface IInputService
    {
        bool IsRotating();
        Vector2 GetInputDelta();
        void UpdateRotation();
    }
}