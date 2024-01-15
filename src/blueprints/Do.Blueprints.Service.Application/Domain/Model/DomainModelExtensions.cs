namespace System.Reflection;

public static class DomainModelExtensions
{
    public static bool IsPublic(this PropertyInfo source) => source.GetMethod?.IsPublic == true;
    public static bool IsVirtual(this PropertyInfo source) => source.GetMethod?.IsVirtual == true;
}
