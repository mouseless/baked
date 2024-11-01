using Baked.Architecture;
using Baked.Reporting;
using Baked.Testing;
using Moq;

namespace Baked;

public static class ReportingExtensions
{
    public static void AddReporting(this List<IFeature> features, Func<ReportingConfigurator, IFeature<ReportingConfigurator>> configure) =>
        features.Add(configure(new()));

    public static IReportContext TheReportContext(this Mocker mockMe,
        object?[][]? data = default,
        bool? queryNotFound = default
    )
    {
        data ??= [];

        var result = Mock.Get(mockMe.Spec.GiveMe.The<IReportContext>());

        if (data is not null)
        {
            result
                .Setup(df => df.Execute(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(data);
        }

        if (queryNotFound == true)
        {
            result
                .Setup(c => c.Execute(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Throws((string queryName, Dictionary<string, object> _) => new QueryNotFoundException(queryName));
        }

        return result.Object;
    }

    public static void VerifyExecute(this IReportContext dataFetcher,
        string? queryName = default,
        (string key, object value)? parameter = default,
        List<(string key, object value)>? parameters = default
    )
    {
        parameters ??= parameter is not null ? [parameter.Value] : [];

        Mock.Get(dataFetcher).Verify(
            df => df.Execute(
                It.Is<string>(q => queryName == null || q == queryName),
                It.Is<Dictionary<string, object>>(p =>
                    parameters.All((kvp) => p.ContainsKey(kvp.key) && Equals(p[kvp.key], kvp.value))
                )
            )
        );
    }
}