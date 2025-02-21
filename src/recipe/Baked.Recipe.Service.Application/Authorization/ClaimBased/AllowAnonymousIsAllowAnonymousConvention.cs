namespace Baked.Authorization.ClaimBased;

public class AllowAnonymousIsAllowAnonymousConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<AllowAnonymousAttribute>()) { return; }

        context.Action.AdditionalAttributes.Add("AllowAnonymous");
    }
}