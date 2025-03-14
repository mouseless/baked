namespace Baked.Ui;

public class AppSettings : Dictionary<string, INamedSettings>
{
    public void Add(INamedSettings setting) => Add(setting.Name, setting);
}