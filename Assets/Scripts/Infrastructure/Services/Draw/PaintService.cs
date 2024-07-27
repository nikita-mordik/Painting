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

        private int _oldRayX;
        private int _oldRayY;

        public Texture2D Texture => _texture;

        public void InitializeTexture(Material objectMaterial, int textureSize = 512,
            TextureWrapMode wrapMode = TextureWrapMode.Clamp, FilterMode filterMode = FilterMode.Bilinear)
        {
            _texture ??= new Texture2D(textureSize, textureSize);
            
            if (_texture.width != textureSize) 
                _texture.Reinitialize(textureSize, textureSize);

            _textureSize = textureSize;
            
            _texture.wrapMode = wrapMode;
            _texture.filterMode = filterMode;
            objectMaterial.mainTexture = _texture;
            _texture.Apply();
        }

        public void SetBrushSize(int brushSize)
        {
            _brushSize = brushSize;
        }

        public void SetBrushColor(Color color)
        {
            _color = color;
        }

        public void InitializePaintTarget(Collider paintTargetCollider)
        {
            _collider = paintTargetCollider;
        }

        public void Draw(Ray ray)
        {
            if (!_collider.Raycast(ray, out var hit, DrawRayDistance)) 
                return;
            
            int rayX = (int)(hit.textureCoord.x * _textureSize);
            int rayY = (int)(hit.textureCoord.y * _textureSize);

            Debug.LogError($"ray data: {rayX}/{rayY}");

            if (_oldRayX != rayX || _oldRayY != rayY)
            {
                Debug.LogError("here before draw");
                DrawCircleBrush(rayX, rayY);
                _oldRayX = rayX;
                _oldRayY = rayY;
            }

            _texture.Apply();
        }

        public void SetSavedTextureData(byte[] bytes, TextureWrapMode wrapMode = TextureWrapMode.Clamp, FilterMode filterMode = FilterMode.Bilinear)
        {
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

            Debug.LogError("here draw");

            _texture.SetPixels(pixels);
            _texture.Apply();
        }
        
        /*private void DrawCircleBrush(int rayX, int rayY)
        {
            for (int y = 0; y < _brushSize; y++)
            {
                for (int x = 0; x < _brushSize; x++)
                {
                    float x2 = Mathf.Pow(x - _brushSize / 2f, 2f);
                    float y2 = Mathf.Pow(y - _brushSize / 2f, 2f);
                    float r2 = Mathf.Pow(_brushSize / 2f - 0.5f, 2f);

                    if (x2 + y2 < r2)
                    {
                        int pixelX = rayX + x - _brushSize / 2;
                        int pixelY = rayY + y - _brushSize / 2;

                        if (pixelX >= 0 && pixelX < _textureSize && pixelY >= 0 && pixelY < _textureSize)
                        {
                            Color oldColor = _texture.GetPixel(pixelX, pixelY);
                            Color resultColor = Color.Lerp(oldColor, _color, _color.a);
                            _texture.SetPixel(pixelX, pixelY, resultColor);
                        }
                    }
                }
            }
        }*/
    }
}