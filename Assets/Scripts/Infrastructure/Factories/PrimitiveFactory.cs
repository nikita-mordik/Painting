using FreedLOW.Painting.Game;
using UnityEngine;
using Zenject;

namespace FreedLOW.Painting.Infrastructure.Factories
{
    public class PrimitiveFactory : IPrimitiveFactory
    {
        private readonly IInstantiator _instantiator;

        private GameObject _activeObject;

        public PrimitiveFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public GameObject CreateCube(Vector3 at)
        {
            DeleteActiveObject();
            
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(cube);
            _activeObject = cube;
            return cube;
        }

        public GameObject CreateSphere(Vector3 at)
        {
            DeleteActiveObject();
            
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(sphere);
            _activeObject = sphere;
            return sphere;
        }

        public GameObject CreateCapsule(Vector3 at)
        {
            DeleteActiveObject();
            
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = at;
            _instantiator.InstantiateComponent<ObjectRotator>(capsule);
            _activeObject = capsule;
            return capsule;
        }

        private void DeleteActiveObject()
        {
            if (_activeObject is not null) 
                Object.Destroy(_activeObject);
        }
    }
}