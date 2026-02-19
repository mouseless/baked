using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Playground.Override.Ui;

public class FormSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                attribute: (a, c) => a.RoutePathBack = "/form-sample"
            );
            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name.Contains("Child"),
                attribute: a => a.HideInLists = true
            );

            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                component: dp =>
                {
                    dp.Schema.Inputs.RemoveAt(dp.Schema.Inputs.FindIndex(i => i.Name == "take"));
                    dp.Schema.Inputs.RemoveAt(dp.Schema.Inputs.FindIndex(i => i.Name == "skip"));
                }
            );

            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                component: dt =>
                {
                    dt.ReloadOn(nameof(FormSample.ClearParents).Kebaberize());
                    dt.ReloadOn("page-changed");
                    dt.Schema.Paginator = false;
                });

            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ServerPaginatorOptions)),
                schema: (c, cc) => B.DataTableServerPaginator(options: dtsp =>
                {
                    var (_, l) = cc;

                    dtsp.Take = B.Select(l("Take"), Inline(new[] { 10, 20, 50, 100 }, options: i => i.RequireLocalization = false),
                        options: s => s.Stateful = true
                    );
                })
            );
        });
    }
}