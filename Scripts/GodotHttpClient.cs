using System;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.Network;

/// <summary>
/// HTTP request helper
/// </summary>
public class GodotHttpClient
{
    private string _baseUrl = "";

    public GodotHttpClient()
    {
        GD.Print("[HttpClient] Initialized");
    }

    public void SetBaseUrl(string url) => _baseUrl = url;

    // Async request - returns placeholder for now
    public async Task<NetworkResponse> RequestAsync(
        string endpoint,
        HttpMethod method = HttpMethod.Get,
        string body = null)
    {
        var response = new NetworkResponse();

        try
        {
            var url = _baseUrl + endpoint;
            GD.Print($"[HttpClient] {method} {url}");

            // Placeholder: use Godot's HTTPClient in actual implementation
            await Task.Delay(100);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Error = ex.Message;
        }

        return response;
    }
}