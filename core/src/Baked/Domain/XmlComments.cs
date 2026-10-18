using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;

namespace Baked.Domain;

public class XmlComments
{
    static readonly ConcurrentDictionary<Assembly, XmlNode> _comments = new();
    static readonly ConcurrentDictionary<Assembly, Dictionary<string, XmlNode>> _members = new();

    public static string? GetPath(Assembly? assembly)
    {
        if (assembly is null) { return null; }

        string result = assembly.Location.Replace(".dll", ".xml");
        if (!File.Exists(result)) { return null; }

        return result;
    }

    public static XmlNode? Get(Assembly? assembly)
    {
        if (assembly is null) { return null; }
        if (_comments.TryGetValue(assembly, out var result)) { return result; }

        var path = GetPath(assembly);
        if (path is null) { return null; }

        var document = new XmlDocument();
        document.Load(path);

        if (document.DocumentElement is null) { return null; }

        _comments.TryAdd(assembly, document.DocumentElement);

        return document.DocumentElement;
    }

    public static XmlNode? Get(Type? type)
    {
        if (type is null) { return null; }

        return GetMember(type.Assembly, XmlName(type));
    }

    public static XmlNode? Get(PropertyInfo? propertyInfo)
    {
        if (propertyInfo is null) { return null; }

        return GetMember(propertyInfo.ReflectedType?.Assembly, XmlName(propertyInfo));
    }

    public static XmlNode? Get(MethodInfo? methodInfo)
    {
        if (methodInfo is null) { return null; }

        return GetMember(methodInfo.ReflectedType?.Assembly, XmlName(methodInfo));
    }

    public static XmlNode? Get(ParameterInfo? parameterInfo, XmlNode? methodDocumentation)
    {
        if (parameterInfo is null) { return null; }
        if (methodDocumentation is null) { return null; }

        return methodDocumentation?.SelectSingleNode(ParamPath(parameterInfo.Name));
    }

    static XmlNode? GetMember(Assembly? assembly, string name)
    {
        if (assembly is null) { return null; }

        return _members.GetOrAdd(assembly, static a => IndexMembers(Get(a))).GetValueOrDefault(name);
    }

    static Dictionary<string, XmlNode> IndexMembers(XmlNode? document)
    {
        var result = new Dictionary<string, XmlNode>();
        foreach (var member in document?.SelectNodes("members/member")?.OfType<XmlNode>() ?? [])
        {
            var name = member.Attributes?["name"]?.Value;
            if (name is null) { continue; }

            result.TryAdd(name, member);
        }

        return result;
    }

    static string ParamPath(string? name) =>
        $"param[@name='{name}']";

    static string XmlName(Type type) =>
        $"T:{type.FullName}";

    static string XmlName(MethodInfo methodInfo)
    {
        var parameters = methodInfo.GetParameters();
        if (!parameters.Any())
        {
            return $"M:{methodInfo.ReflectedType?.FullName}.{methodInfo.Name}";
        }

        return $"M:{methodInfo.ReflectedType?.FullName}.{methodInfo.Name}({parameters.Select(p => XmlTypeName(p.ParameterType)).Join(",")})";
    }

    static string XmlTypeName(Type? type)
    {
        if (type is null) { return string.Empty; }

        if (type.IsArray)
        {
            return $"{XmlTypeName(type.GetElementType())}[]";
        }

        var fullName = type.FullName ?? $"{type}";

        if (type.IsGenericType)
        {
            return $"{fullName[..fullName.IndexOf('`')]}{{{type.GenericTypeArguments.Select(XmlTypeName).Join(",")}}}";
        }

        return fullName;
    }

    static string XmlName(PropertyInfo propertyInfo) =>
        $"P:{propertyInfo.ReflectedType?.FullName}.{propertyInfo.Name}";
}