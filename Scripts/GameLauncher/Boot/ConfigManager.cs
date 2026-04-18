using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
// using UnityEngine.Networking;
using VContainer;
using PrismaDot.Infrastructure;

namespace PrismaDot.GameLauncher.Boot;

public static class ConfigManager
{
    public static T LoadConfigFromText<T>(string content) where T : class
    {
        if (string.IsNullOrEmpty(content))
        {
            return null;
        }

        try
        {
            return JsonUtility.FromJson<T>(content);
        }
        catch (Exception e)
        {
            Debugger.LogError(e);
        }

        return null;
    }

    public static T LoadConfigFromBytes<T>(byte[] data) where T : class =>
        LoadConfigFromText<T>(Encoding.Unicode.GetString(data));

    private static AppVersionManifest _appConfig;

    public static AppVersionManifest AppConfig => _appConfig ??= LoadConfig<AppVersionManifest>();

    private static T LoadConfig<T>() where T : class
    {
        if (TryLoadConfigFromResources(out T config))
        {
            Debugger.Log($"Loaded config from resources: {typeof(T).Name}");
            return config;
        }

        // Debugger.LogError($"Failed to load config from resources: {typeof(T).Name}");

        if (TryLoadConfigFromAddressables(out config))
        {
            Debugger.Log($"<color=green>Loaded config from addressables: {typeof(T).Name}");
            return config;
        }

        // Debugger.LogError($"Failed to load config from addressables: {typeof(T).Name}");

        if (TryLoadConfigFromPersistentDataPath(out config))
        {
            Debugger.Log($"<color=green>Loaded config from persistent data path: {typeof(T).Name}");
            return config;
        }

        Debugger.LogError($"Failed to load config : {typeof(T).Name}");
        return null;
    }

    private static bool TryLoadConfigFromPersistentDataPath<T>(out T config) where T : class
    {
        config = null;
        var path = Path.Combine(Application.persistentDataPath, $"{typeof(T).Name}.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            config = JsonUtility.FromJson<T>(json);
            return true;
        }

        Debugger.LogError($"Failed to load config from persistent data path: {path}");
        return false;
    }

    private static bool TryLoadConfigFromResources<T>(out T config) where T : class
    {
        config = null;
        var str = Resources.Load<TextAsset>(typeof(T).Name);
        if (str == null)
        {
            str = Resources.Load<TextAsset>("Configs/" + typeof(T).Name);
        }

        if (str != null)
        {
            config = JsonUtility.FromJson<T>(str.text);
            return true;
        }

        return false;
    }

    private static bool TryLoadConfigFromAddressables<T>(out T config) where T : class
    {
        config = null;
        var asset = Addressables.LoadAssetAsync<TextAsset>(typeof(T).Name).WaitForCompletion();
        if (asset != null)
        {
            config = JsonUtility.FromJson<T>(asset.text);
            return true;
        }

        return false;
    }
}
