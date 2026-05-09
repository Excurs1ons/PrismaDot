using System;
using System.Threading.Tasks;
using MessagePipe;
using Microsoft.Extensions.Logging;
using PrismaDot.GameMain.Network;
using PrismaDot.GameMain.Network.Protocol;
using VContainer;

namespace PrismaDot.GameMain.UI;

/// <summary>
/// 服务器列表控制器
/// 使用MessagePipe订阅网络事件
/// </summary>
public class ServerListController : IPresenter<ServerListView>
{
    public ServerListView View { get; set; }
    private ServerConf _serverConf;
    private readonly UIService _uiService;
    private readonly NetworkService _networkService;
    private readonly ILogger<ServerListController> _logger;
    private readonly ISubscriber<LoginResponseEvent> _loginResponseSub;
    private readonly ISubscriber<NetworkDisconnectedEvent> _disconnectedSub;
    private readonly ISubscriber<NetworkErrorEvent> _errorSub;
    private readonly DisposableBagBuilder _disposables;

    private LoginWindow _loginWindow;

    [Inject]
    public ServerListController(
        UIService uiService,
        NetworkService networkService,
        ILogger<ServerListController> logger,
        ISubscriber<LoginResponseEvent> loginResponseSub,
        ISubscriber<NetworkDisconnectedEvent> disconnectedSub,
        ISubscriber<NetworkErrorEvent> errorSub)
    {
        _uiService = uiService;
        _networkService = networkService;
        _logger = logger;
        _loginResponseSub = loginResponseSub;
        _disconnectedSub = disconnectedSub;
        _errorSub = errorSub;
        _disposables = DisposableBag.CreateBuilder();

        // 订阅网络事件
        SubscribeNetworkEvents();
    }

    /// <summary>
    /// 手动初始化（用于非DI场景�?
    /// </summary>
    public void Initialize(ServerListView view, ServerConf serverConf)
    {
        View = view;
        _serverConf = serverConf;
        InitializeView();
    }

    private void SubscribeNetworkEvents()
    {
        // 订阅登录响应事件
        _loginResponseSub.Subscribe(response => { HandleLoginResponse(response.Response); }).AddTo(_disposables);

        // 订阅断开连接事件
        _disconnectedSub.Subscribe(disconnected => { HandleDisconnected(disconnected.Reason); }).AddTo(_disposables);

        // 订阅网络错误事件
        _errorSub.Subscribe(error => { HandleNetworkError(error.Message, error.ErrorType); }).AddTo(_disposables);
    }

    private void InitializeView()
    {
        if (View == null || _serverConf == null)
        {
            _logger.LogWarning("ServerListController not properly initialized");
            return;
        }

        var serverNames = new System.Collections.Generic.List<string>();
        foreach (var server in _serverConf.LobbyServers)
        {
            serverNames.Add(server.ToString());
        }

        View.SetServerList(serverNames);
        View.OnConnectClicked += HandleConnectClicked;
    }

    private async void HandleConnectClicked()
    {
        if (View == null || _serverConf == null) return;

        int selectedIndex = View.SelectedServerIndex;
        var server = _serverConf.LobbyServers[selectedIndex];

        View.SetInteractable(false);
        View.SetStatusText($"正在连接�?{server.Name}...");

        var result = await _networkService.ConnectAsync(server.Address, server.Port);

        if (result.IsSuccess)
        {
            View.SetStatusText("连接成功");
            await ShowLoginWindow();
        }
        else
        {
            View.SetInteractable(true);
            await ShowErrorWindow(result.ErrorType.Value, result.ErrorMessage);
        }
    }

    private async Task ShowLoginWindow()
    {
        _loginWindow = await _uiService.OpenAsync<LoginWindow>();
        _loginWindow.OnLoginConfirmed += HandleLoginConfirmed;
        _loginWindow.OnCanceled += HandleLoginCanceled;
    }

    private async void HandleLoginConfirmed(string username, string password)
    {
        try
        {
            // 发送登录请求，响应通过事件订阅处理
            await _networkService.SendLoginRequestAsync(username, password);
            View?.SetStatusText("正在登录...");
            _logger.LogInformation("Login request sent for user: {Username}", username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send login request: {Message}", ex.Message);
            await ShowErrorWindow(ConnectErrorType.NetworkError, ex.Message);
        }
    }

    /// <summary>
    /// 处理登录响应（通过MessagePipe事件接收�?
    /// </summary>
    private async void HandleLoginResponse(LoginResponse response)
    {
        _logger.LogInformation("Login response received: {Success}, Message: {Message}",
            response.Success, response.Message);

        if (response.Success)
        {
            _logger.LogInformation("Login successful: {PlayerId}", response.PlayerId);
            View?.SetStatusText("登录成功");
            // TODO: 进入游戏
            if (_loginWindow != null)
            {
                _uiService.Close(_loginWindow.GetInstanceID());
            }
        }
        else
        {
            await _uiService.OpenAsync<ErrorWindow>();
            var errorWindow = UIService.Instance.GetWindow<ErrorWindow>();
            errorWindow?.Setup("登录失败", response.Message,
                () => _uiService.Close(errorWindow.GetInstanceID()));
        }
    }

    /// <summary>
    /// 处理网络断开事件
    /// </summary>
    private void HandleDisconnected(string reason)
    {
        _logger.LogWarning("Network disconnected: {Reason}", reason);
        View?.SetInteractable(true);
        View?.SetStatusText("连接已断开");
    }

    /// <summary>
    /// 处理网络错误事件
    /// </summary>
    private async void HandleNetworkError(string message, ConnectErrorType errorType)
    {
        _logger.LogError("Network error: {Message}", message);
        await ShowErrorWindow(errorType, message);
    }

    private void HandleLoginCanceled()
    {
        _networkService.Disconnect();
        if (View != null)
        {
            View.SetInteractable(true);
            View.SetStatusText("已断开连接");
        }
    }

    private async Task ShowErrorWindow(ConnectErrorType errorType, string message)
    {
        await _uiService.OpenAsync<ErrorWindow>();
        var errorWindow = UIService.Instance.GetWindow<ErrorWindow>();

var (title, detail) = errorType switch
        {
            ConnectErrorType.Timeout => ("Timeout", "Server response timeout, please check network"),
            ConnectErrorType.ServerClosed => ("Server Closed", "Target server is not available, please try later"),
            ConnectErrorType.NetworkError => ("Network Error", message),
            ConnectErrorType.VersionTooOld => ("Version Too Old", "Client version is too old, please update"),
            _ => ("Unknown Error", message)
        };

        errorWindow?.Setup(title, detail,
            () => _uiService.Close(errorWindow.GetInstanceID()),
            () =>
            {
                _uiService.Close(errorWindow.GetInstanceID());
                HandleConnectClicked();
            });
    }

    public void Initialize()
    {
        
    }

    public void Cleanup()
    {
        
    }

    public void Dispose()
    {
        if (_loginWindow != null)
        {
            _loginWindow.OnLoginConfirmed -= HandleLoginConfirmed;
            _loginWindow.OnCanceled -= HandleLoginCanceled;
        }

        if (View != null)
        {
            View.OnConnectClicked -= HandleConnectClicked;
        }

        _disposables?.Build().Dispose();
    }
}
