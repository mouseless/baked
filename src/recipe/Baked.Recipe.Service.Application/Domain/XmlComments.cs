using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;

namespace Baked.Domain;

public class XmlComments
{
    static ConcurrentDictionary<Assembly, XmlNode> _comments = new();

    public static XmlNode? Get(Assembly? assembly)
    {
        if (assembly is null) { return null; }
        if (_comments.TryGetValue(assembly, out var result)) { return result; }

        var document = new XmlDocument();
        string filename = assembly.Location.Replace(".dll", ".xml");
        if (!File.Exists(filename)) { return null; }

        document.Load(filename);
        if (document.DocumentElement is null) { return null; }

        _comments.TryAdd(assembly, document.DocumentElement);

        return document.DocumentElement;
    }

    public static XmlNode? Get(Type? type)
    {
        if (type is null) { return null; }

        return
            Get(type.Assembly)
            ?.SelectSingleNode(MemberPath(XmlName(type)));
    }

    public static XmlNode? Get(PropertyInfo? propertyInfo)
    {
        if (propertyInfo is null) { return null; }

        return
            Get(propertyInfo.ReflectedType?.Assembly)
            ?.SelectSingleNode(MemberPath(XmlName(propertyInfo)));
    }

    public static XmlNode? Get(MethodInfo? methodInfo)
    {
        if (methodInfo is null) { return null; }

        return
            Get(methodInfo.ReflectedType?.Assembly)
            ?.SelectSingleNode(MemberPath(XmlName(methodInfo)));
    }

    public static XmlNode? Get(ParameterInfo? parameterInfo, XmlNode? methodDocumentation)
    {
        if (parameterInfo is null) { return null; }
        if (methodDocumentation is null) { return null; }

        return methodDocumentation?.SelectSingleNode(ParamPath(parameterInfo.Name));
    }

    static string MemberPath(string name) =>
        $"members/member[@name='{name}']";

    static string ParamPath(string? name) =>
        $"param[@name='{name}']";

    static string XmlName(Type type) =>
        $"T:{type.FullName}";

    static string XmlName(MethodInfo methodInfo) =>
        $"M:{methodInfo.ReflectedType?.FullName}.{methodInfo.Name}({methodInfo.GetParameters().Select(p => p.ParameterType.FullName).Join(",")})";

    static string XmlName(PropertyInfo propertyInfo) =>
        $"P:{propertyInfo.ReflectedType?.FullName}.{propertyInfo.Name}";
}