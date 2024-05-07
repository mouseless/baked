using Do.RestApi.Configuration;

namespace Do.CodingStyle.SingleByUnique;

public class MarkActionAsSingleByUniqueConvention : IApiModelConvention<ActionModelContext>, IApiModelConvention<ApiModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.Id == "SingleById")
        {
            context.Action.AdditionalAttributes.Add($"{nameof(SingleByUniqueAttribute)}(\"Id\", typeof(Guid))");

            return;
        }

        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.TryGetSingle<SingleByUniqueAttribute>(out var unique))
        {
            context.Action.AdditionalAttributes
                .Add($"{nameof(SingleByUniqueAttribute)}(\"{unique.PropertyName}\", typeof({unique.PropertyType.FullName}))");
        }
    }

    public void Apply(ApiModelContext context)
    {
        context.Api.Usings.Add("Do.CodingStyle.SingleByUnique");
    }
}