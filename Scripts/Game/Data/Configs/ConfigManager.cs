using System.Collections.Generic;
using Godot;
using System.Text.Json;

namespace PrismaDot.Game.Data.Configs;

public interface IConfig { }

public class ConfigManager
{
    private static IEnumerable<string> GetProbableConfigPaths<T>(string nameOrPath = null) where T : IConfig
    {
        // Simple placeholder for path generation
        yield break;
    }

    public static T LoadConfig<T>(string nameOrPath = null) where T : IConfig, new()
    {
        string path = nameOrPath ?? typeof(T).Name;
        // In Godot, we usually load .json files as TextResource or similar, 
        // but often we just read the file directly if it's a raw json.
        // If it's a Godot Resource, we load it.
        
        var resource = GD.Load(path);
        if (resource == null)
        {
            // Fallback for user:// or res://
            if (!path.StartsWith("res://") && !path.StartsWith("user://"))
            {
                path = "res://Configs/" + path + ".json";
                resource = GD.Load(path);
            }
        }

        if (resource == null)
        {
             PrismaDot.Infrastructure.Debugger.LogError($"Failed to load config: {path}");
             return default;
        }

        // Assuming it's a custom resource that has a 'Content' string or we read as text
        // For now, let's assume we can get the text content.
        // A better way in Godot is using FileAccess.
        
        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            PrismaDot.Infrastructure.Debugger.LogError($"Failed to open config file: {path}");
            return default;
        }

        string json = file.GetAsText();
        return JsonSerializer.Deserialize<T>(json);
    }
}
