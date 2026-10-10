namespace Baked.Business;

[AttributeUsage(AttributeTargets.All)]
public class Group : Attribute
{
    Dictionary<string, string> NameByContext { get; } = [];

    public string this[string context]
    {
        get
        {
            if (!NameByContext.TryGetValue(context, out var result))
            {
                return "default";
            }

            return result;
        }
        set => NameByContext[context] = value;
    }
}