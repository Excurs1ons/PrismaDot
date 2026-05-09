namespace PrismaDot;

public class ReadOnlyReactiveProperty<T> { public T Value { get; } public ReadOnlyReactiveProperty(T value) => Value = value; }