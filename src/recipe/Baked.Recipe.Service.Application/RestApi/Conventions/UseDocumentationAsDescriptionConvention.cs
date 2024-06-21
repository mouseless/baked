using Baked.Business.DomainAssemblies;
using Baked.RestApi.Configuration;

namespace Baked.RestApi.Conventions;

public class UseDocumentationAsDescriptionConvention(TagDescriptions _descriptions)
    : IApiModelConvention<ControllerModelContext>, IApiModelConvention<ActionModelContext>, IApiModelConvention<ParameterModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (context.Controller.MappedType is null) { return; }
        if (!context.Controller.MappedType.TryGetMembers(out var controllerMembers)) { return; }

        var summary = controllerMembers.Documentation?["summary"]?.InnerText.Trim();

        _descriptions[context.Controller.GroupName] = summary ?? string.Empty;
    }

    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Documentation is null) { return; }

        var summary = context.Action.MappedMethod.Documentation["summary"]?.InnerText.Trim();
        var remarks = context.Action.MappedMethod.Documentation["remarks"]?.InnerText.Trim();
        if (summary is null && remarks is null) { return; }

        context.Action.AdditionalAttributes.Add($"SwaggerOperation(Summary = \"{summary}\", Description = \"{remarks}\")");
    }

    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.MappedParameter is null) { return; }
        if (context.Parameter.MappedParameter.Documentation is null) { return; }

        var description = context.Parameter.MappedParameter.Documentation.InnerText.Trim();
        if (description is null) { return; }

        context.Parameter.AdditionalAttributes.Add($"SwaggerSchema(\"{description}\")");
    }
}