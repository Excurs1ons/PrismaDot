using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.Text;
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
            // Godot 4.x: Load assembly from plugins folder
            var asm = LoadHotfixAssembly();
            if (asm == null)
            {
                GameBootstrapper.Logger.LogInformation("No hotfix assembly found, using built-in entry");
                return null;
            }

            var entryType = asm.GetType("PrismaDot.GameMain.MainEntry", throwOnError: true);
            return (IGameEntry)Activator.CreateInstance(entryType);
        }

        private static Assembly LoadHotfixAssembly()
        {
            // Look for DLL in plugins folder
            var pluginsPath = Path.Combine("res://", "plugins");
            if (!DirAccess.Exists(pluginsPath))
            {
                return null;
            }

            var dllPath = Path.Combine(pluginsPath, "GameMain.dll");
            if (!FileAccess.Exists(dllPath))
            {
                return null;
            }

            try
            {
                var bytes = FileAccess.GetFileAsBytes(dllPath);
                return Assembly.Load(bytes);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Failed to load hotfix assembly: {ex.Message}");
                return null;
            }
        }
    }
}
