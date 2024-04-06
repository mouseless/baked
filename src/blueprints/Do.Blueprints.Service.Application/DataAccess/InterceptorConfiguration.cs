namespace Do.DataAccess;

public class InterceptorConfiguration
{
    public Func<InstantiationContext, object, object?> Instantiator { get; set; } = (ctx, id) => null;
}