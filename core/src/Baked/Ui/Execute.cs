namespace Baked.Ui;

public static class Execute
{
    public static Publishes Publish { get; } = new();
    public static Composables Local { get; } = new();

    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static RemoteAction Remote(string path, IAction postAction,
        Action<RemoteAction>? options = default
    ) => options.Apply(new() { Path = path, PostAction = postAction });

    public class Composables
    {
        public LocalAction UseRedirectBack(
            string? expected = null
        ) => UseRedirect(
            expected: expected,
            options: la => la.Options = Fetch.Inline(new { back = true })
        );

        public LocalAction UseRedirect(string route,
            bool? query = default,
            IEnumerable<string>? include = default,
            IEnumerable<string>? exclude = default,
            string? expected = null
        ) => UseRedirect(
            query: query,
            include: include,
            exclude: exclude,
            expected: expected,
            options: la => la.Options = Fetch.Inline(new { route })
        );

        public LocalAction UseRedirect(Action<LocalAction>? options,
            bool? query = default,
            IEnumerable<string>? include = default,
            IEnumerable<string>? exclude = default,
            string? expected = null
        ) => Use("Redirect", la =>
            {
                options.Apply(la);

                if (expected is not null)
                {
                    la.Options += Fetch.Inline(new { expected });
                    la.Options += Fetch.Context.Model(options: m => m.TargetProp = "actual");
                }

                if (query is not null && query.Value)
                {
                    la.Options += Fetch.Inline(new { query, include, exclude });
                }
            });

        public LocalAction Use(string composable,
            Action<LocalAction>? options = default
        ) => options.Apply(new() { Composable = composable.StartsWith("use") ? composable : $"use{composable}" });
    }

    public class Publishes
    {
        public PublishAction CloseDialog() =>
            Event("close-dialog", empty: true);

        public PublishAction Event(string @event,
            bool empty = default,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { Event = @event, Data = empty ? null : Fetch.Context.Model() });

        public PublishAction PageContextValue(string key,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { PageContextKey = key, Data = Fetch.Context.Model() });
    }
}