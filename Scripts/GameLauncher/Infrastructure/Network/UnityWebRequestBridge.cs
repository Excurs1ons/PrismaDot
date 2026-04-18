#if UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismaDot.Infrastructure.Network
{
    public class UnityWebRequestBridge : IWebRequest
    {
        public string Url { get; }
        public string Error { get; private set; }
        public long ResponseCode { get; private set; }
        public bool IsSuccess { get; private set; }
        public byte[] Data { get; private set; }
        public string Text { get; private set; }

        public UnityWebRequestBridge(string url, string method = "GET")
        {
            Url = url;
        }

        public async Task SendAsync()
        {
            // Implementation would use UnityEngine.Networking.UnityWebRequest
            // But we don't have it here, so this is just a bridge.
            await Task.Yield();
        }

        public void SetRequestHeader(string name, string value)
        {
        }

        public void Dispose()
        {
        }
    }
}
#endif
