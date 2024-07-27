using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public class SaveLoadDrawDataService : ISaveLoadDrawDataService
    {
        private readonly IPaintService _paintService;
        private readonly IPaintPrefsSavedData _paintPrefsSavedData;

        private string _savePath;

        public SaveLoadDrawDataService(IPaintService paintService, IPaintPrefsSavedData paintPrefsSavedData)
        {
            _paintService = paintService;
            _paintPrefsSavedData = paintPrefsSavedData;
        }
        
        public async UniTask SaveTexture(string name)
        {
            _savePath = Path.Combine(Application.persistentDataPath, $"{name}.png");
            
            DeleteSavedTexture();

            byte[] bytes = _paintService.Texture.EncodeToPNG();
            await File.WriteAllBytesAsync(_savePath, bytes);
            _paintPrefsSavedData.SaveTextureSizeData(_paintService.Texture.width);
            Debug.Log($"Texture saved to {_savePath}");
        }

        public async UniTask<bool> LoadTexture(string name)
        {
            _savePath = Path.Combine(Application.persistentDataPath, $"{name}.png");

            if (File.Exists(_savePath))
            {
                byte[] bytes = await File.ReadAllBytesAsync(_savePath);
                _paintService.SetSavedTextureData(bytes, _paintPrefsSavedData.LoadTextureSizeData());
                Debug.Log($"Texture loaded from {_savePath}");
                return bytes.Length > 0;
            }

            Debug.LogError($"No saved texture found at {_savePath}");
            return false;
        }

        private void DeleteSavedTexture()
        {
            if (File.Exists(_savePath)) 
                File.Delete(_savePath);
        }
    }
}