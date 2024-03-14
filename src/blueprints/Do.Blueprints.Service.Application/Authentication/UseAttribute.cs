namespace Do.Authentication;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class UseAttribute<T> : Attribute { }
