namespace PrismaDot;

public class ReactiveProperty<T> { public T Value { get; set; } public ReactiveProperty(T defaultValue = default) => Value = defaultValue; }