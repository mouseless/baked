using Baked.Caching;
using Baked.Domain.Model;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Ui.Datas;
using static Baked.Ui.UiLayer;

namespace Baked.Test.Theme.Custom;

public static class DomainDatas
{
    public static IData ActionRemote(MethodModel method,
        Action<RemoteData>? options = default
    ) => Remote(method.GetAction().GetRoute(),
        options: rd =>
        {
            rd.Query = method.DefaultOverload.Parameters.Any()
                ? Composite(options: cd => cd.Parts.AddRange([Computed(Composables.UseQuery), Injected()]))
                : Computed(Composables.UseQuery);

            if (method.TryGetSingle<ClientCacheAttribute>(out var clientCache))
            {
                rd.SetAttribute("client-cache", clientCache.Type);
            }

            options.Apply(rd);
        }
    );

    public static InlineData EnumInline(TypeModel type,
        NewLocaleKey? l = default
    ) => Inline(
        value: l is null
            ? type.GetEnumNames()
            : type
                .GetEnumNames()
                .Select(name => new { text = l($"{type.SkipNullable()}.{name}"), value = name }),
        options: id => id.SetRequireLocalization(l is not null)
    );
}