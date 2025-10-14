using Baked.Binding;
using Baked.Binding.Rest;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class UseDocumentationAsDescriptionConvention(TagDescriptions _descriptions, RequestResponseExamples _examples)
    : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>, IDomainModelConvention<ParameterModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ControllerModelAttribute>(out var controller)) { return; }

        var summary = members.Documentation.GetSummary();

        _descriptions[controller.GroupName] = summary ?? string.Empty;
        _examples.TryAdd($"{context.Type.FullName}", new(
            members.Documentation.GetExampleCode("request"),
            members.Documentation.GetExampleCode("response")
        ));
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Documentation is null) { return; }

        _examples.TryAdd($"{context.Type.FullName}.{context.Method.Name}", new(
            context.Method.Documentation.GetExampleCode("request"),
            context.Method.Documentation.GetExampleCode("response")
        ));

        var summary = context.Method.Documentation.GetSummary();
        var remarks = context.Method.Documentation.GetRemarks();
        if (summary is null && remarks is null) { return; }

        action.AdditionalAttributes.Add($"SwaggerOperation(Summary = \"{summary.EscapeNewLines()}\", Description = \"{remarks.EscapeNewLines()}\")");
    }

    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (context.Parameter is null) { return; }
        if (context.Parameter.Documentation is null) { return; }

        var description = context.Parameter.Documentation.InnerText.Trim();
        if (description is null) { return; }

        parameter.AdditionalAttributes.Add($"SwaggerSchema(\"{description.EscapeNewLines()}\")");
    }
}