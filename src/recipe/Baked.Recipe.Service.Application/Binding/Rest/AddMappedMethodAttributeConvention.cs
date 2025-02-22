namespace Baked.Binding.Rest;

public class AddMappedMethodAttributeConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }

        context.Action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Controller.MappedType.FullName}\", \"{context.Action.MappedMethod.Name}\")");
    }
}