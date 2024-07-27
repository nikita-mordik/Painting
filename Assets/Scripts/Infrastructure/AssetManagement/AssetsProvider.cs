using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.AssetManagement
{
    public class AssetsProvider : IAssetsProvider
    {
        public GameObject LoadAsset(string assetName)
        {
            return Resources.Load<GameObject>(assetName);
        }
    }
}