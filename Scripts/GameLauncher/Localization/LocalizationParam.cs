namespace PrismaDot.GameLauncher.Localization;

public struct LocalizationParam
{
    public enum LocalizationParamType
    {
        None,
        Key,
        String,
        Int,
        Float,
    }

    public readonly LocalizationParamType Type;
    public readonly int IntVal;
    public readonly float FloatVal;

    public readonly string StringVal;

    // 空参数
    public static readonly LocalizationParam None = new(LocalizationParamType.None);

    // 私有构造，强制使用工厂或隐式转换
    private LocalizationParam(LocalizationParamType type, int i = 0, float f = 0, string s = null)
    {
        Type = type;
        IntVal = i;
        FloatVal = f;
        StringVal = s;
    }

    public static implicit operator LocalizationParam(int v) => new(LocalizationParamType.Int, v);

    public static implicit operator LocalizationParam(float v) => new(LocalizationParamType.Float, f: v);

    public static implicit operator LocalizationParam(string v) => new(LocalizationParamType.String, s: v);
}
