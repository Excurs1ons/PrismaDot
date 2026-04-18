// Assets/Scripts/Network/AssetDownloader.cs

using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Godot;
// using UnityEngine.Networking;

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

    public IEnumerator DownloadFile(string url, string savePath, ulong expectedSize, string expectedHash,
        bool ssl = false)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, savePath);
        string directory = Path.GetDirectoryName(fullPath);

        if (string.IsNullOrEmpty(directory))
        {
            yield break;
        }

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 检查文件是否已存在且完�?
        if (File.Exists(fullPath))
        {
            if (VerifyFile(fullPath, expectedSize, expectedHash))
            {
                OnComplete?.Invoke(true, null);
                yield break;
            }

            File.Delete(fullPath);
        }

        var urlProtocol = ssl ? "http://" : "https://";
        string downloadUrl = $"{urlProtocol}{_assetHost}:{_port}/{url}";

        // TODO: Use Godot.HttpRequest
        /*
        using UnityWebRequest request = UnityWebRequest.Get(downloadUrl);
        request.timeout = _timeout;

        DownloadHandlerFile handler = new DownloadHandlerFile(fullPath);
        handler.removeFileOnAbort = true;
        request.downloadHandler = handler;

        var operation = request.SendWebRequest();

        ulong totalBytes = expectedSize > 0u ? expectedSize : 0u;

        while (!operation.isDone)
        {
            if (totalBytes > 0)
            {
                float progress = request.downloadedBytes / (float)totalBytes;
                OnProgress?.Invoke(progress, request.downloadedBytes, totalBytes);
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
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
        else
        {
            OnComplete?.Invoke(false, request.error);
        }
        */
        yield break;
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
        return $"sha256:{BitConverter.ToString(hash).Replace("-", "").ToLower()}";
    }
}
