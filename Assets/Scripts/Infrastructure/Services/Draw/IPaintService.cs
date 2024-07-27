using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public interface IPaintService
    {
        Texture2D Texture { get; }

        bool IsCanDraw();
        void InitializeTexture(int textureSize = 512, TextureWrapMode wrapMode = TextureWrapMode.Clamp,
            FilterMode filterMode = FilterMode.Bilinear);
        void SetBrushSize(int brushSize);
        void SetBrushColor(Color color);
        void InitializePaintTarget(Collider paintTargetCollider);
        void Draw(Ray ray);
        void SetSavedTextureData(byte[] bytes, int textureSize,
            TextureWrapMode wrapMode = TextureWrapMode.Clamp, FilterMode filterMode = FilterMode.Bilinear);
        void ClearTexture();
    }
}