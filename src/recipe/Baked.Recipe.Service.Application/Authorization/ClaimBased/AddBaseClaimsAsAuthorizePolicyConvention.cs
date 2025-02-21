namespace Baked.Authorization.ClaimBased;

public class AddBaseClaimsAsAuthorizePolicyConvention(IEnumerable<string> _baseClaims)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<AllowAnonymousAttribute>()) { return; }
        if (context.Action.MappedMethod.TryGetSingle<RequireUserAttribute>(out var requireUser) && requireUser.Override) { return; }

        context.Action.AdditionalAttributes.AddRange(_baseClaims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}