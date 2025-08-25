using Baked.Caching;
using Baked.Domain.Model;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Ui.Datas;

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
}