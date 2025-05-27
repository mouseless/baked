namespace Baked.Localization;

public interface ILocalizer
{
    string this[string key] { get; }
    string this[string key, params object[] arguments] { get; }
}