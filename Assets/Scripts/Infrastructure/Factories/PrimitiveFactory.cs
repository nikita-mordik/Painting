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
            return CreateObjectAt(AssetsName.Cube, at);
        }

        public GameObject CreateSphere(Vector3 at)
        {
            DeleteActiveObject();
            return CreateObjectAt(AssetsName.Sphere, at);
        }

        public GameObject CreateCapsule(Vector3 at)
        {
            DeleteActiveObject();
            return CreateObjectAt(AssetsName.Capsule, at);
        }

        private void DeleteActiveObject()
        {
            if (_activeObject is not null) 
                Object.Destroy(_activeObject);
        }

        private GameObject CreateObjectAt(string objectName, Vector3 at)
        {
            var prefab = _assetsProvider.LoadAsset(objectName);
            var obj = _instantiator.InstantiatePrefab(prefab);
            obj.transform.position = at;
            _activeObject = obj;
            return obj;
        }
    }
}