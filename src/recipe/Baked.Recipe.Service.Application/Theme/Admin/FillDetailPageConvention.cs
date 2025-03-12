using Baked.Domain.Configuration;
using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public class FillDetailPageConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGetSingle<ComponentDescriptorAttribute<DetailPage>>(out var detail)) { return; }
        if (!members.TryGetInitializerActionModel(out var action)) { return; }

        detail.Schema.Header = Components.PageTitle(context.Type.Name.Humanize());
        foreach (var property in members.Properties.Where(p => p.IsPublic))
        {
            detail.Schema.Props.Add(new(property.Name.Camelize()));
        }

        detail.Name = action.GetRoutePart(0);
        detail.Data = Datas.Remote($"/{action.GetRoute().Replace("{id}", "{0}")}");
    }
}