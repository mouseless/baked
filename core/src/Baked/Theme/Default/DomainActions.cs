using Baked.Domain.Model;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Actions;
using static Baked.Ui.Datas;

namespace Baked.Theme.Default;

public static class DomainActions
{
    public static RemoteAction MethodRemote(MethodModel method,
        Action<RemoteAction>? options = default
    ) => Remote(method.GetAction().GetRoute(),
        postAction: Publish.Event(method.Name.Kebaberize(),
            options: ea => ea.Data = method.DefaultOverload.ReturnsVoid() ? null : Context.Response()
        ),
        options: ra =>
        {
            ra.Method = method.GetAction().Method.Method;

            options.Apply(ra);
        }
    );
}