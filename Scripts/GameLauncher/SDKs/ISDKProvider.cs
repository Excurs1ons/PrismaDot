using System.Threading.Tasks;

namespace PrismaDot.GameLauncher.SDKs;

public interface ISDKProvider
{
    public static ISDKProvider Instance { get; protected set; }

    // 1. ���� (������־)
    string ProviderName { get; }

    // 2. ��ʼ�� (ͳһΪ�첽����ʹ SDK ��ͬ����)
    Task<bool> InitializeAsync();

    // 3. ��ѯ (��� Steam ��Ҫÿ֡ RunCallbacks ������)
    void OnUpdate();

    // 4. ��ͣ/�ָ� (����ƶ����к�̨���ӶϿ�������)
    void OnApplicationPause(bool pauseStatus);

    // 5. ���� (�ͷž��)
    void Shutdown();
}
