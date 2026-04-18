using System;
using System.IO;
#if !UNITY_EDITOR
using System.IO;
#endif

using ZLinq;
using System.Reflection;
using System.Threading;
using Cysharp.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using PrismaDot.GameLauncher.Infrastructure.Interfaces;
using Godot;
using VContainer;
using ZLogger;

namespace PrismaDot.GameLauncher.Boot
{
    public static class GameEntryResolver
    {
        public static IGameEntry Resolve()
        {
            var asm = LoadHotfixAssembly();
            if (asm == null)
            {
                GameBootstrapper.Logger.LogInformation("Failed to load hotfix assembly");
                return null;
            }

            var entryType = asm.GetType("PrismaDot.GameMain.MainEntry", throwOnError: true);

            return (IGameEntry)Activator.CreateInstance(entryType);
        }

        private static Assembly LoadHotfixAssembly()
        {
#if UNITY_EDITOR
            //输出全部程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            using var sb = ZString.CreateStringBuilder();

            sb.AppendLine("========== [Assembly Dump Start] ==========");
            sb.AppendFormat("Total Assemblies: {0}", assemblies.Length);
            sb.AppendLine();
            foreach (var asm in assemblies)
            {
                sb.AppendLine(asm.GetName().Name);
            }

            GameBootstrapper.Logger.LogInformation(sb.ToString());
            return assemblies.AsValueEnumerable()
                .First(a => a.GetName().Name.Contains("GameMain", StringComparison.OrdinalIgnoreCase));

#else
            var bytes = File.ReadAllBytes(GetHotfixDllPath());
            var asm = Assembly.Load(bytes);
            if(asm == null)
            { 
                GameBootstrapper.Logger.LogInformation("Failed to load hotfix assembly");
            }
            return asm;
#endif

#pragma warning disable CS8321 // 已声明本地函数，但从未使用过
            [UsedImplicitly]
            static string GetHotfixDllPath()
            {
                var path = Path.Combine(Application.streamingAssetsPath, GlobalDefinitions.MAIN_DLL_NAME + ".bytes");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Hotfix assembly not found at {path}");
                }

                return path;
            }
        }
    }
}
