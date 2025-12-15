using static Baked.Ui.Datas;

namespace Baked.Ui;

public static class Actions
{
    public static Publishes Publish { get; } = new();
    public static Composables Local { get; } = new();

    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static RemoteAction Remote(string path, IAction postAction,
        Action<RemoteAction>? options = default
    ) => options.Apply(new(path, postAction));

    public class Composables
    {
        public LocalAction UseRedirect(string route) =>
            UseRedirect(options: la => la.Options = Inline(new { route }));

        public LocalAction UseRedirect(
            Action<LocalAction>? options = default
        ) => Use("Redirect", options);

        public LocalAction Use(string composable,
            Action<LocalAction>? options = default
        ) => options.Apply(new(composable.StartsWith("use") ? composable : $"use{composable}"));
    }

    public class Publishes
    {
        public PublishAction Event(string @event,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { Event = @event, Data = Context.Model() });

        public PublishAction PageContextValue(string key,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { PageContextKey = key, Data = Context.Model() });
    }
}