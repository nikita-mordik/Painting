using FreedLOW.Painting.Game;
using FreedLOW.Painting.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace FreedLOW.Painting.Infrastructure.Factories
{
    public class PrimitiveFactory : IPrimitiveFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IInstantiator _instantiator;

        private GameObject _activeObject;

        public PrimitiveFactory(IAssetsProvider assetsProvider, IInstantiator instantiator)
        {
            _assetsProvider = assetsProvider;
            _instantiator = instantiator;
        }
        
        public GameObject CreateCube(Vector3 at)
        {
            DeleteActiveObject();
            
            var prefab = _assetsProvider.LoadAsset(AssetsName.Cube);
            var cube = _instantiator.InstantiatePrefab(prefab);
            cube.transform.position = at;
            _activeObject = cube;
            return cube;
        }

        public GameObject CreateSphere(Vector3 at)
        {
            DeleteActiveObject();
            
            var prefab = _assetsProvider.LoadAsset(AssetsName.Sphere);
            var sphere = _instantiator.InstantiatePrefab(prefab);
            sphere.transform.position = at;
            _activeObject = sphere;
            return sphere;
        }

        public GameObject CreateCapsule(Vector3 at)
        {
            DeleteActiveObject();
            
            var prefab = _assetsProvider.LoadAsset(AssetsName.Capsule);
            var capsule = _instantiator.InstantiatePrefab(prefab);
            capsule.transform.position = at;
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