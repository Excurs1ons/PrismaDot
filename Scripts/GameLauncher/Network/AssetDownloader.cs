using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Godot;
using PrismaDot.Infrastructure;
using PrismaDot.Infrastructure.Network;

namespace PrismaDot.GameLauncher.Network;

public class AssetDownloader : Node
{
    public delegate void DownloadProgress(float progress, ulong downloaded, ulong total);
    public event DownloadProgress OnProgress;

    public delegate void DownloadComplete(bool success, string error);
    public event DownloadComplete OnComplete;

    private string _assetHost;
    private int _port;
    private int _timeout;
    private int _chunkSize;

    public void Configure(string host, int port, int timeout, int chunkSize)
    {
        _assetHost = host;
        _port = port;
        _timeout = timeout;
        _chunkSize = chunkSize;
    }

    public async Task DownloadFileAsync(string url, string savePath, ulong expectedSize, string expectedHash, bool ssl = false)
    {
        // Use user:// for Godot persistent data path
        string fullPath = ProjectSettings.GlobalizePath("user://" + savePath);
        string directory = Path.GetDirectoryName(fullPath);

        if (string.IsNullOrEmpty(directory)) return;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(fullPath))
        {
            if (VerifyFile(fullPath, expectedSize, expectedHash))
            {
                OnComplete?.Invoke(true, null);
                return;
            }
            File.Delete(fullPath);
        }

        var urlProtocol = ssl ? "https://" : "http://";
        string downloadUrl = $"{urlProtocol}{_assetHost}:{_port}/{url}";

        Debugger.Log($"Downloading {downloadUrl} to {fullPath}");

        using var request = WebRequest.Get(downloadUrl);
        // Note: Currently IWebRequest doesn't support streaming to file directly in this simple abstraction.
        // For production, we should add DownloadToFile support to IWebRequest.
        
        await request.SendAsync();

        if (request.IsSuccess)
        {
            try 
            {
                await File.WriteAllBytesAsync(fullPath, request.Data);
                if (VerifyFile(fullPath, expectedSize, expectedHash))
                {
                    OnComplete?.Invoke(true, null);
                }
                else
                {
                    File.Delete(fullPath);
                    OnComplete?.Invoke(false, "File verification failed");
                }
            }
            catch (Exception ex)
            {
                OnComplete?.Invoke(false, $"IO Error: {ex.Message}");
            }
        }
        else
        {
            OnComplete?.Invoke(false, request.Error);
        }
    }

    private bool VerifyFile(string path, ulong expectedSize, string expectedHash)
    {
        if (!File.Exists(path)) return false;

        FileInfo info = new FileInfo(path);
        if (expectedSize > 0 && (ulong)info.Length != expectedSize)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(expectedHash))
        {
            string actualHash = ComputeFileHash(path);
            return actualHash.Equals(expectedHash, StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    private string ComputeFileHash(string path)
    {
        using SHA256 sha256 = SHA256.Create();
        using FileStream stream = File.OpenRead(path);
        byte[] hash = sha256.ComputeHash(stream);
        // Ensure format matches expected (e.g., "sha256:...")
        string hashStr = BitConverter.ToString(hash).Replace("-", "").ToLower();
        return expectedHash.StartsWith("sha256:") ? $"sha256:{hashStr}" : hashStr;
    }
}
