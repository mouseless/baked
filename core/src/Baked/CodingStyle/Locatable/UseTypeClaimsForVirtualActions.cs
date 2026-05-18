using Baked.Authorization;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Locatable;

public class UseTypeClaimsForVirtualActions : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controllerModelAttribute)) { return; }
        if (!metadata.TryGet<RequireUserAttribute>(out var requireUser)) { return; }
        if (!controllerModelAttribute.Action.TryGetValue("locate", out var locateAction)) { return; }

        locateAction.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}