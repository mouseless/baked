namespace Baked.Ui.Configuration;

public interface IPlugin
{
    bool Module { get; }
    string Name { get; }
}