using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismaDot.Infrastructure.Network
{
    public interface IWebRequest : IDisposable
    {
        string Url { get; }
        string Error { get; }
        long ResponseCode { get; }
        bool IsSuccess { get; }
        byte[] Data { get; }
        string Text { get; }

        Task SendAsync();
        void SetRequestHeader(string name, string value);
    }

    public static class WebRequest
    {
        public static IWebRequest Get(string url)
        {
#if GODOT
            return new GodotWebRequest(url, "GET");
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            return new UnityWebRequestBridge(url, "GET");
#else
            throw new NotImplementedException("Platform not supported");
#endif
        }
    }
}
