using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public class PaintPrefsSavedData : IPaintPrefsSavedData
    {
        private const string TextureSizeKey = "TextureSize";
        private const int DefaultTextureSize = 512;

        public int LoadTextureSizeData() => 
            PlayerPrefs.HasKey(TextureSizeKey) ? PlayerPrefs.GetInt(TextureSizeKey) : DefaultTextureSize;

        public void SaveTextureSizeData(int textureSize)
        {
            PlayerPrefs.SetInt(TextureSizeKey, textureSize);
            PlayerPrefs.Save();
        }
    }
}