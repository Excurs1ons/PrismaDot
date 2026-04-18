using System;
#if GODOT
using Godot;
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
// using UnityEngine;
#endif

namespace PrismaDot.Infrastructure
{
    public static class Debugger
    {
        public static void Log(object message)
        {
#if GODOT
            GD.Print(message);
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            Debug.Log(message);
#else
            Console.WriteLine(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if GODOT
            GD.PushWarning(message);
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            Debug.LogWarning(message);
#else
            Console.WriteLine($"[Warning] {message}");
#endif
        }

        public static void LogError(object message)
        {
#if GODOT
            GD.PushError(message);
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            Debug.LogError(message);
#else
            Console.Error.WriteLine(message);
#endif
        }

        public static void LogFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
#if GODOT
            GD.Print(message);
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            Debug.LogFormat(format, args);
#else
            Console.WriteLine(message);
#endif
        }
    }
}
