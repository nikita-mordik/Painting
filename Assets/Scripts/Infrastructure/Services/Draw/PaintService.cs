using UnityEngine;

namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public class PaintService : IPaintService
    {
        private const float DrawRayDistance = 100f;
        
        private Texture2D _texture;
        private int _textureSize;
        
        private int _brushSize;
        private Color _color;
        
        private Collider _collider;

        private RaycastHit _hit;
        private int _oldRayX;
        private int _oldRayY;

        public Texture2D Texture => _texture;

        public bool IsCanDraw() => 
            _texture is not null && _collider is not null;

        public void InitializeTexture(int textureSize = 512, TextureWrapMode wrapMode = TextureWrapMode.Clamp,
            FilterMode filterMode = FilterMode.Bilinear)
        {
            DisposeTexture();
            
            _texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
            
            if (_texture.width != textureSize) 
                _texture.Reinitialize(textureSize, textureSize);

            _textureSize = textureSize;
            
            _texture.wrapMode = wrapMode;
            _texture.filterMode = filterMode;
            _texture.Apply();
        }

        public void SetBrushSize(int brushSize) => _brushSize = brushSize;

        public void SetBrushColor(Color color) => _color = color;

        public void InitializePaintTarget(Collider paintTargetCollider) => _collider = paintTargetCollider;

        public void Draw(Ray ray)
        {
            if (!_collider.Raycast(ray, out _hit, DrawRayDistance)) 
                return;
            
            int rayX = (int)(_hit.textureCoord.x * _textureSize);
            int rayY = (int)(_hit.textureCoord.y * _textureSize);
            
            if (_oldRayX != rayX || _oldRayY != rayY)
            {
                DrawCircleBrush(rayX, rayY);
                _oldRayX = rayX;
                _oldRayY = rayY;
            }
        }

        public void SetSavedTextureData(byte[] bytes, int textureSize,
            TextureWrapMode wrapMode = TextureWrapMode.Clamp, FilterMode filterMode = FilterMode.Bilinear)
        {
            DisposeTexture();

            _textureSize = textureSize;
            _texture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, false);
            _texture.LoadImage(bytes);
            _texture.wrapMode = wrapMode;
            _texture.filterMode = filterMode;
            _texture.Apply();
        }

        public void ClearTexture()
        {
            Color clearColor = new Color(0, 0, 0, 0);
            Color[] clearPixels = new Color[_texture.width * _texture.height];

            for (int i = 0; i < clearPixels.Length; i++)
            {
                clearPixels[i] = clearColor;
            }

            _texture.SetPixels(clearPixels);
            _texture.Apply();
        }

        private void DrawCircleBrush(int rayX, int rayY)
        {
            int halfBrushSize = _brushSize / 2;
            float r2 = Mathf.Pow(halfBrushSize - 0.5f, 2f);

            Color[] pixels = _texture.GetPixels();
            int textureWidth = _texture.width;

            for (int y = -halfBrushSize; y < halfBrushSize; y++)
            {
                for (int x = -halfBrushSize; x < halfBrushSize; x++)
                {
                    float x2 = Mathf.Pow(x, 2f);
                    float y2 = Mathf.Pow(y, 2f);

                    if (x2 + y2 < r2)
                    {
                        int pixelX = rayX + x;
                        int pixelY = rayY + y;

                        if (pixelX >= 0 && pixelX < _textureSize && pixelY >= 0 && pixelY < _textureSize)
                        {
                            int pixelIndex = pixelY * textureWidth + pixelX;
                            Color oldColor = pixels[pixelIndex];
                            pixels[pixelIndex] = Color.Lerp(oldColor, _color, _color.a);
                        }
                    }
                }
            }
            
            _texture.SetPixels(pixels);
            _texture.Apply();
        }

        private void DisposeTexture()
        {
            if (_texture is not null) 
                Object.Destroy(_texture);
        }
    }
}