namespace Baked.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowAnonymous : Attribute;