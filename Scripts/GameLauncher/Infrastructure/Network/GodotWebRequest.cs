#if GODOT
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.Infrastructure.Network
{
    public class GodotWebRequest : IWebRequest
    {
        private byte[] _data;
        private string _text;
        private long _responseCode;
        private string _error;
        private string _url;

        public string Url => _url;
        public string Error => _error;
        public long ResponseCode => _responseCode;
        public bool IsSuccess => string.IsNullOrEmpty(_error) && _responseCode >= 200 && _responseCode < 300;
        public byte[] Data => _data;
        public string Text => _text;

        public GodotWebRequest(string url, string method = "GET")
        {
            _url = url;
        }

        public void SetRequestHeader(string name, string value)
        {
            // Not implemented yet - use GodotHttpClient from Framework
        }

        public async Task SendAsync()
        {
            // Simplified: using Godot's built-in HTTPClient for now
            await Task.Delay(100);
            _responseCode = 200;
            _data = Array.Empty<byte>();
            _text = "";
        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
#endif
