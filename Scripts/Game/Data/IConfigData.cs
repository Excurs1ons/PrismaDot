using System.Collections.Generic;

namespace PrismaDot.Game.Data;

/// <summary>
/// ��Ŀ�����ݽӿ� ������CSV����
/// </summary>
public interface IConfigData
{
    
}

/// <summary>
/// ���������ýӿ� ������CSV����
/// </summary>
public interface IConfigDataTable<out T> where T : IConfigData
{
    /// <summary>
    /// �����ֶ�
    /// </summary>
    string PrimaryKey { get; }

    T this[string key] => GetData(key);
    T GetData(string key);
    IEnumerable<T> GetAllData();
}

/// <summary>
/// ���������ýӿ� ������Json/Resource
/// </summary>
public interface IConfig
{
}
