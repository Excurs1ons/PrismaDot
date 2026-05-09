namespace PrismaDot.Network;

/// <summary>
/// Network response
/// </summary>
public class NetworkResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Data { get; set; }
    public string Error { get; set; }
}