using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

namespace Do.CodingStyle.SingleByUnique;

public class UniquePropertyMatcherPolicy : MatcherPolicy, IEndpointSelectorPolicy
{
    public override int Order => int.MaxValue;

    public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            if (endpoint.Metadata.GetMetadata<SingleByUniqueAttribute>() is not null)
            {
                return true;
            }
        }

        return false;
    }

    public Task ApplyAsync(HttpContext httpContext, CandidateSet candidateSet)
    {
        CandidateItem? fallback = null;
        var candidates = BuildCandidates(candidateSet);
        foreach (var candidate in candidates)
        {
            if (candidate.Unique.PropertyType == typeof(string))
            {
                fallback = candidate;

                continue;
            }

            if (candidate.Unique.PropertyType == typeof(Guid) && Guid.TryParse($"{candidate.Value}", out var _) ||
                candidate.Unique.PropertyType.IsEnum && Enum.TryParse(candidate.Unique.PropertyType, $"{candidate.Value}", true, out var _))
            {
                candidateSet.SetValidity(candidate.Index, true);

                return Task.CompletedTask;
            }
        }

        if (fallback is not null)
        {
            candidateSet.SetValidity(fallback.Index, true);

            return Task.CompletedTask;
        }

        throw new RouteParameterIsNotValidException(
            candidates.Select(c => c.Unique.PropertyName).Join("Or").Camelize(),
            candidates.FirstOrDefault()?.Value
        );
    }

    List<CandidateItem> BuildCandidates(CandidateSet candidates)
    {
        var result = new List<CandidateItem>();
        for (var i = 0; i < candidates.Count; i++)
        {
            candidates.SetValidity(i, false);

            var candidate = candidates[i];
            var unique = candidate.Endpoint.Metadata.GetMetadata<SingleByUniqueAttribute>();
            if (unique is null) { continue; }
            if (candidate.Values is null) { continue; }
            if (!candidate.Values.TryGetValue(unique.PropertyName.Camelize(), out var value)) { continue; }

            result.Add(new(candidate, i, unique, value));
        }

        return result;
    }

    record CandidateItem(CandidateState Candidate, int Index, SingleByUniqueAttribute Unique, object? Value);
}