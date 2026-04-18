using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Godot;
using PrismaDot.Infrastructure;

namespace PrismaDot.GameLauncher.Boot;

public static class ConfigManager
{
    public static AppConfig AppConfig { get; private set; }

    public static async Task InitializeAsync()
    {
        AppConfig = await LoadConfigAsync<AppConfig>();
        if (AppConfig == null)
        {
            Debugger.LogError("Failed to load AppConfig.");
        }
    }

    public static async Task<T> LoadConfigAsync<T>() where T : class, new()
    {
        T config = null;

        // 1. Try Load from user:// (Persistent Data Path)
        if (TryLoadConfigFromPersistentDataPath(out config))
        {
            Debugger.Log($"Loaded config from user path: {typeof(T).Name}");
            return config;
        }

        // 2. Try Load from res:// (Resources)
        if (TryLoadConfigFromResources(out config))
        {
            Debugger.Log($"Loaded config from resources: {typeof(T).Name}");
            return config;
        }

        Debugger.LogError($"Failed to load config: {typeof(T).Name}");
        return null;
    }

    public static T LoadConfigFromBytes<T>(byte[] data) where T : class
    {
        try
        {
            var json = System.Text.Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception e)
        {
            Debugger.LogError(e);
            return null;
        }
    }

    private static bool TryLoadConfigFromResources<T>(out T config) where T : class
    {
        config = null;
        var name = typeof(T).Name;
        string[] paths = { $"res://Configs/{name}.json", $"res://{name}.json" };

        foreach (var path in paths)
        {
            if (FileAccess.FileExists(path))
            {
                using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
                if (file != null)
                {
                    var json = file.GetAsText();
                    config = JsonSerializer.Deserialize<T>(json);
                    return true;
                }
            }
        }
        return false;
    }

    private static bool TryLoadConfigFromPersistentDataPath<T>(out T config) where T : class
    {
        config = null;
        var path = ProjectSettings.GlobalizePath($"user://{typeof(T).Name}.json");
        if (File.Exists(path))
        {
            try
            {
                var json = File.ReadAllText(path);
                config = JsonSerializer.Deserialize<T>(json);
                return true;
            }
            catch (Exception e)
            {
                Debugger.LogError($"Failed to load config from persistent data path: {path}. Error: {e.Message}");
            }
        }
        return false;
    }
}
