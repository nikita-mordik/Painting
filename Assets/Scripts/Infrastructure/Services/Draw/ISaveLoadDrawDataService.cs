using Cysharp.Threading.Tasks;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public interface ISaveLoadDrawDataService
    {
        UniTask SaveTexture(string name);
        UniTask<bool> LoadTexture(string name);
    }
}