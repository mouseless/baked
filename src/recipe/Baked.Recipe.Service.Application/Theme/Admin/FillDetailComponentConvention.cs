using Baked.Domain.Configuration;
using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public class FillDetailComponentConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGetSingle<ComponentDescriptorAttribute<Detail>>(out var detail)) { return; }
        if (!members.TryGetActionModel(out var action)) { return; }

        foreach (var property in members.Properties.Where(p => p.IsPublic))
        {
            detail.Schema.Props.Add(new(property.Name.Camelize()));
        }

        detail.Name = action.GetRoutePart(0);
        detail.Data = new RemoteData { Path = $"/{action.GetRoute().Replace("{id}", "{0}")}" };
    }
}