using Baked.Business;
using Baked.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Baked;

public static class LocalizationExtensions
{
    extension(Validate v)
    {
        public Validate That(Action<Func<string, string>> validate) =>
            v.That((l, _) => validate(l));

        public Validate That(Action<Func<string, string>, IServiceProvider> validate) =>
            v.That(sp => validate(GetLocalizer(sp), sp));

        public async Task<Validate> ThatAsync(Func<Func<string, string>, Task> validate) =>
            await v.ThatAsync((l, _) => validate(l));

        public async Task<Validate> ThatAsync(Func<Func<string, string>, IServiceProvider, Task> validate) =>
            await v.ThatAsync(async sp => await validate(GetLocalizer(sp), sp));
    }

    extension(Task<Validate> task)
    {
        public async Task<Validate> That(Action<Func<string, string>> validate) =>
            (await task).That(validate);

        public async Task<Validate> That(Action<Func<string, string>, IServiceProvider> validate) =>
            (await task).That(validate);

        public async Task<Validate> ThatAsync(Func<Func<string, string>, Task> validate) =>
            await (await task).ThatAsync(validate);

        public async Task<Validate> ThatAsync(Func<Func<string, string>, IServiceProvider, Task> validate) =>
            await (await task).ThatAsync(validate);
    }

    // WARNING
    //
    // Do NOT remove this warning disable section unintentionally.
    // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0052
    static Func<string, string> GetLocalizer(IServiceProvider sp)
    {
        var l = sp.GetRequiredService<IStringLocalizer>();
        var t = sp.GetRequiredService<ITextTransformer>();

        return name =>
        {
            var cleared = ClearFieldName(name);
            if (cleared.Length == 0) { return string.Empty; }

            return l[CultureInfo.UsingInvariantCulture(() => t.Titleize(cleared))];
        };
    }

    // `_field.PropertyName` -> `PropertyName`, `_field` -> ``
    static string ClearFieldName(string name) =>
        name.StartsWith('_') ? name.Split('.')[1..].Join('.') : name;
#pragma warning restore IDE0052
}