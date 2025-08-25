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
        IData? headers = default
    ) => Remote(method.GetAction().GetRoute(),
        headers: headers,
        query: method.DefaultOverload.Parameters.Any()
            ? Composite([Computed(Composables.UseQuery), Injected()])
            : Computed(Composables.UseQuery),
        attributes: method.TryGetSingle<ClientCacheAttribute>(out var clientCache) ? [("client-cache", clientCache.Type)] : null
    );

    public static InlineData EnumInline(TypeModel type,
        NewLocaleKey? l = default
    ) => Inline(
        value: l is null
            ? type.GetEnumNames()
            : type
                .GetEnumNames()
                .Select(name => new { text = l($"{type.SkipNullable()}.{name}"), value = name }),
        requireLocalization: l is not null
    );
}