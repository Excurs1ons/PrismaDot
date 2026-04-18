#if GODOT
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.Infrastructure.Network
{
    public class GodotWebRequest : IWebRequest
    {
        private readonly HttpRequest _httpRequest;
        private readonly TaskCompletionSource<bool> _tcs = new();
        private byte[] _data;
        private string _text;
        private long _responseCode;
        private string _error;

        public string Url { get; }
        public string Error => _error;
        public long ResponseCode => _responseCode;
        public bool IsSuccess => string.IsNullOrEmpty(_error) && _responseCode >= 200 && _responseCode < 300;
        public byte[] Data => _data;
        public string Text => _text;

        public GodotWebRequest(string url, string method = "GET")
        {
            Url = url;
            // Since this is a Node, it needs to be in the tree to work, 
            // or we use a global node. For Prisma, we usually use a global 
            // instance or add it to the scene.
            _httpRequest = new HttpRequest();
            Godot.Engine.GetMainLoop().Root.AddChild(_httpRequest);
            _httpRequest.RequestCompleted += OnRequestCompleted;
        }

        public void SetRequestHeader(string name, string value)
        {
            // Not implemented in this simple wrapper yet
        }

        public async Task SendAsync()
        {
            var error = _httpRequest.Request(Url);
            if (error != ErrorPlus.Ok)
            {
                _error = $"Failed to start request: {error}";
                _tcs.SetResult(false);
            }
            await _tcs.Task;
        }

        private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
        {
            _responseCode = responseCode;
            if (result != (long)HttpRequest.Result.Success)
            {
                _error = $"Request failed with result: {(HttpRequest.Result)result}";
            }
            else
            {
                _data = body;
                _text = System.Text.Encoding.UTF8.GetString(body);
            }
            _tcs.SetResult(true);
        }

        public void Dispose()
        {
            if (_httpRequest != null)
            {
                _httpRequest.RequestCompleted -= OnRequestCompleted;
                _httpRequest.QueueFree();
            }
        }
    }
}
#endif
