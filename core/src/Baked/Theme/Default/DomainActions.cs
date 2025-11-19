using Baked.Domain.Model;
using Baked.Ui;

using static Baked.Ui.Actions;

namespace Baked.Theme.Default;

public static class DomainActions
{
    public static RemoteAction MethodRemoteAction(MethodModel method,
        Action<RemoteAction>? options = default
    ) => Remote(method.GetAction().GetRoute(),
        options: ra =>
        {
            ra.Method = method.GetAction().Method.Method;

            options.Apply(ra);
        }
    );
}