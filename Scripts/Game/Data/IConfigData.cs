using System.Collections.Generic;
using ZLinq;

namespace PrismaDot.Game.Data;

/// <summary>
/// 条目型数据接口 适用于CSV数据
/// </summary>
public interface IConfigData
{
    
}

/// <summary>
/// 表格型配置接口 适用于CSV表格
/// </summary>
public interface IConfigDataTable<out T> where T : IConfigData
{
    /// <summary>
    /// 主键字段
    /// </summary>
    string PrimaryKey { get; }

    T this[string key] => GetData(key);
    T GetData(string key);
    IEnumerable<T> GetAllData();
}

/// <summary>
/// 对象型配置接口 适用于Json/Resource
/// </summary>
public interface IConfig
{
}
