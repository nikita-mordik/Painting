using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.AssetManagement
{
    public interface IAssetsProvider
    {
        GameObject LoadAsset(string assetName);
    }
}