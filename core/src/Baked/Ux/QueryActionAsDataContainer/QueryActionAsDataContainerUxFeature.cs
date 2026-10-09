using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Ui;

using static Baked.Ui.Actions;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.QueryActionAsDataContainer;

public class QueryActionAsDataContainerUxFeature(int[] _pageSizeOptions)
    : IFeature<UxConfigurator>
{
    static readonly string _lengthContextKeySuffix = "length";
    static readonly string _takeContextKeySuffix = "take";

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // Order is set to -10 to allow DataPanel override
            conventions.AddMethodComponent(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component"),
                component: () => B.DataContainer(),
                order: -10
            );
            conventions.AddMethodComponent(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith("data-panel", "content"),
                component: () => B.DataContainer()
            );

            // Add sort and paging parameters to RemoteData query
            conventions.EditMethodSchema<RemoteData>(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith("data-container", "content", "*", "data"),
                schema: rd => rd.Query += Context.Parent(options: cd => cd.Prop = "container-parameters"),
                order: 20
            );

            // Add all inputs to DataContainer
            conventions.EditMethodComponent<DataContainer>(
                component: (dc, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateSchema<Input>(cc.Drill("data-container", "inputs"));
                        if (input is null) { continue; }

                        dc.Schema.Inputs.Add(input);
                    }
                }
            );

            // Set paging inputs to be required and numeric
            conventions.EditParameterSchema<Input>(
                when: c => c.Parameter.Has<PagingAttribute>(),
                schema: input =>
                {
                    input.Required = true;
                    input.Numeric = true;
                },
                order: 10
            );

            // Split inputs between `DataPanel` and `DataContainer` when
            // container is under a panel, keeping only sorting and paging in
            // container while keeping the rest in panel
            conventions.EditMethodComponent<DataPanel>(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                component: (dp, c) =>
                {
                    if (dp.Schema.Content.Schema is not DataContainer dc) { return; }

                    var dpInputs = dp.Schema.Inputs.ToDictionary(i => i.Name, i => i);
                    var dcInputs = dc.Inputs.ToDictionary(i => i.Name, i => i);
                    foreach (var parameter in c.Method.DefaultOverload.Parameters.Having<ParameterModelAttribute>())
                    {
                        var api = parameter.Get<ParameterModelAttribute>();
                        if (parameter.Has<SortingAttribute>() || parameter.Has<PagingAttribute>())
                        {
                            if (!dpInputs.TryGetValue(api.Name, out var input)) { continue; }

                            dp.Schema.Inputs.Remove(input);
                        }
                        else
                        {
                            if (!dcInputs.TryGetValue(api.Name, out var input)) { continue; }

                            dc.Inputs.Remove(input);
                        }
                    }
                },
                order: 10
            );

            // Disable virtual scroll, configure paginator and publish
            // data length when skip parameter exists
            conventions.EditMethodComponent<DataTable>(
                where: cc => cc.Path.Contains("data-container"),
                component: (dt, c) =>
                {
                    dt.Schema.VirtualScrollerOptions = default;

                    if (c.Method.DefaultOverload.Parameters.Any(p => p.TryGet<PagingAttribute>(out var paging) && paging.IsSkip))
                    {
                        dt.Schema.Paginator = default;
                        dt.Schema.DataLengthContextKey = $"{c.Type.Name}:{c.Method.Name}:{_lengthContextKeySuffix}";
                    }
                },
                order: 10
            );

            // Skip
            conventions.AddParameterComponent(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsSkip,
                component: () => B.Paginator()
            );
            conventions.EditParameterComponent<Paginator>(
                component: (p, c) =>
                {
                    var prop = $"{c.Type.Name}:{c.Method.Name}:{_lengthContextKeySuffix}";
                    p.Data = Context.Page(o =>
                    {
                        o.Prop = prop;
                        o.TargetProp = "length";
                    });

                    p.ReloadWhen(prop);
                }
            );
            // When there is no take parameter, set take to 10
            conventions.EditParameterComponent<Paginator>(
                when: c => !c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().Any(p => p.Get<PagingAttribute>().IsTake),
                component: p => p.Data += Inline(new { take = 10 })
            );
            // When there is take parameter, use take parameter's value from page context
            conventions.EditParameterComponent<Paginator>(
                when: c => c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().Any(p => p.Get<PagingAttribute>().IsTake),
                component: (p, c) =>
                {
                    var prop = $"{c.Type.Name}:{c.Method.Name}:{_takeContextKeySuffix}";
                    p.Data += Context.Page(o =>
                    {
                        o.Prop = prop;
                        o.TargetProp = "take";
                    });

                    p.ReloadWhen(prop);
                }
            );

            // Take
            conventions.AddParameterComponent(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: () => B.Select()
            );
            conventions.EditParameterComponent<Select>(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: s =>
                {
                    s.Data = Inline(_pageSizeOptions, options: i => i.RequireLocalization = false);
                    s.Override(B.PageSize());
                }
            );
            conventions.EditParameterComponent<Select>(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: (s, c) =>
                {
                    s.Schema.ShowClear = null;
                    s.Schema.Stateful = true;
                    s.Action = Publish.PageContextValue(
                        $"{c.Type.Name}:{c.Method.Name}:{_takeContextKeySuffix}",
                        o => o.Data = Context.Model()
                    );
                },
                order: 10
            );
        });
    }
}