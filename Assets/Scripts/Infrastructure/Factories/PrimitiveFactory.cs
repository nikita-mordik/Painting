using FreedLOW.Painting.Game;
using UnityEngine;
using Zenject;

namespace FreedLOW.Painting.Infrastructure.Factories
{
    public class PrimitiveFactory : IPrimitiveFactory
    {
        private IInstantiator _instantiator;

        public PrimitiveFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public GameObject CreateCube(Vector3 at)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(cube);
            return cube;
        }

        public GameObject CreateSphere(Vector3 at)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(sphere);
            return sphere;
        }

        public GameObject CreateCapsule(Vector3 at)
        {
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(capsule);
            return capsule;
        }
    }
}