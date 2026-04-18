using System.Collections.Generic;

namespace PrismaDot;

public interface IServerListView
{
    void SetServerList(List<string> serverNames);
    void SelectServer(int index);
}
