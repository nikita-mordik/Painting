using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Factories
{
    public class PrimitiveFactory : IPrimitiveFactory
    {
        public GameObject CreateCube(Vector3 at)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = at;
            return cube;
        }

        public GameObject CreateSphere(Vector3 at)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = at;
            return sphere;
        }

        public GameObject CreateCapsule(Vector3 at)
        {
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = at;
            return capsule;
        }
    }
}