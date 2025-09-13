using Baked.Caching;
using Baked.Domain.Model;
using Baked.Ui;

using static Baked.Ui.Datas;

namespace Baked.Theme.Admin;

public static class DomainDatas
{
    public static InlineData MethodNameInline(MethodModel method, ComponentContext context,
        Action<InlineData>? options = default
    )
    {
        var (_, l) = context;

        return Inline(l(method.Name), options: options);
    }

    public static RemoteData MethodRemote(MethodModel method,
        Action<RemoteData>? options = default
    ) => Remote(method.GetAction().GetRoute(),
        options: rd =>
        {
            rd.Query = method.DefaultOverload.Parameters.Any()
                ? Composite(options: cd => cd.Parts.AddRange([Computed(Composables.UseQuery), Injected()]))
                : Computed(Composables.UseQuery);

            if (method.TryGet<ClientCacheAttribute>(out var clientCache))
            {
                rd.SetAttribute("client-cache", clientCache.Type);
            }

            options.Apply(rd);
        }
    );

    public static InlineData EnumInline(TypeModel type, ComponentContext context,
        bool requireLocalization = true
    )
    {
        var (_, l) = context;

        return Inline(
            value: !requireLocalization
                ? type.SkipNullable().GetEnumNames()
                : type
                    .SkipNullable()
                    .GetEnumNames()
                    .Select(name => new { text = l($"{type.SkipNullable().Name}.{name}"), value = name }),
            options: id => id.RequireLocalization = requireLocalization ? true : null
        );
    }
}