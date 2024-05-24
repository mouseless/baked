using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class AddRequireUserClaimsAsAuthorizePolicyConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.TryGetSingle<RequireUserAttribute>(out var requireUser)) { return; }

        context.Action.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}