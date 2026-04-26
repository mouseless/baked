using Baked.Testing;
using Baked.Theme;

namespace Baked.Test;

public static class ThemeExtensions
{
    extension(Stubber giveMe)
    {
        public string APath() =>
            "/test/path";

        public Route ARoute(
            bool buildFails = false,
            string? buildFailMessage = default
        )
        {
            buildFailMessage ??= "build fails";

            var result = new Route(giveMe.APath(), giveMe.AString());

            if (buildFails)
            {
                result.Page = p => p.Described(_ =>
                    _ => throw giveMe.ADiagnosticCode().Exception(buildFailMessage)
                );
            }

            return result;
        }
    }
}