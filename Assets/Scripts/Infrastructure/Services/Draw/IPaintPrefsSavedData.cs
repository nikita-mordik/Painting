namespace FreedLOW.Painting.Infrastructure.Services.Draw
{
    public interface IPaintPrefsSavedData
    {
        int LoadTextureSizeData();
        void SaveTextureSizeData(int textureSize);
    }
}