using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public interface ISaveLoadDrawDataService
    {
        UniTask SaveTexture(string name);
        UniTask<Texture2D> LoadTexture(string name);
    }
}