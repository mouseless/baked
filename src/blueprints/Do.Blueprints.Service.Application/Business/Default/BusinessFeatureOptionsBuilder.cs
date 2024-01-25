using System.Reflection;

namespace Do.Business.Default;

public class BusinessFeatureOptionsBuilder
{
    public static BusinessFeatureOptions Build(Action<BusinessFeatureOptionsBuilder> optionsBuilder)
    {
        BusinessFeatureOptionsBuilder builder = new();

        optionsBuilder(builder);

        return builder.Build();
    }

    readonly List<Assembly> _businessAssemblies = [];
    readonly List<Assembly> _applicationParts = [];
    readonly Assembly? _entryAssembly = Assembly.GetEntryAssembly();

    public void AddBusinessAssembly<T>() =>
        AddBusinessAssembly(typeof(T).Assembly);
    public void AddBusinessAssembly(Assembly assembly) =>
        _businessAssemblies.Add(assembly);
    public void AddApplicationPart<T>() =>
        _applicationParts.Add(typeof(T).Assembly);
    public void AddApplicationPart(Assembly assembly) =>
        _applicationParts.Add(assembly);

    BusinessFeatureOptions Build() =>
        new(_businessAssemblies, _entryAssembly != null ? [_entryAssembly, .. _applicationParts] : [.. _applicationParts]);
}
