namespace PrismaDot.Assets;

/// <summary>
/// Asset loader interface
/// </summary>
public interface IAssetLoader
{
    T Load<T>(string path) where T : class;
    void Unload(string path);
    bool Exists(string path);
}