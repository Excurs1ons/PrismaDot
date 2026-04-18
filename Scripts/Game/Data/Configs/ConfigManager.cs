using System.Collections.Generic;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
using ZLinq;

namespace PrismaDot.Game.Data.Configs;

public class ConfigManager
{
    private static IEnumerator<string> GetProbableConfigPath<T>(string nameOrPath = null) where T : IConfig
    {
        yield break;
    }

    public static T LoadConfig<T>(string nameOrPath = null) where T : IConfig, new()
    {
        string configStr;
        var configTextAsset = Resources.Load<TextAsset>(nameOrPath);
        if (configTextAsset == null)
        {
            configTextAsset = Addressables.LoadAssetAsync<TextAsset>(nameOrPath).WaitForCompletion();
        }

        if (configTextAsset == null)
        {
            configTextAsset = Addressables.LoadAssetAsync<TextAsset>(nameOrPath).WaitForCompletion();
        }

        configStr = configTextAsset.text;
        return JsonUtility.FromJson<T>(configStr);
    }
}
