using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Factories
{
    public interface IPrimitiveFactory
    {
        GameObject CreateCube(Vector3 at);
        GameObject CreateSphere(Vector3 at);
        GameObject CreateCapsule(Vector3 at);
    }
}