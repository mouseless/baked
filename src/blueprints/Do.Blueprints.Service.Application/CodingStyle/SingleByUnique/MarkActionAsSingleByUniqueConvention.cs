using Do.RestApi.Configuration;

namespace Do.CodingStyle.SingleByUnique;

public class MarkActionAsSingleByUniqueConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.Id == "SingleById")
        {
            context.Action.AdditionalAttributes.Add($"{typeof(SingleByUniqueAttribute).FullName}(\"Id\", typeof(Guid))");
        }

        if (context.Action.MethodModel is null) { return; }
        if (context.Action.MethodModel.TryGetSingle<SingleByUniqueAttribute>(out var unique))
        {
            context.Action.AdditionalAttributes
                .Add($"{typeof(SingleByUniqueAttribute).FullName}(\"{unique.PropertyName}\", typeof({unique.PropertyType.FullName}))");
        }
    }
}