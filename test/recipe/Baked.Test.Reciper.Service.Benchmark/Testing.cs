using BenchmarkDotNet.Attributes;
using System.Net.Http.Headers;

namespace Baked.Test;

[SimpleJob(warmupCount: 1, iterationCount: 1, invocationCount: 1)]
public class Testing
{
    HttpClient _client = default!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _client = new HttpClient()
        {
            BaseAddress = new("http://localhost:5151")
        };
        _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("token-jane");
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _client.Dispose();
    }

    [Benchmark]
    public void TestRateLimiter()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 50; i++)
        {
            tasks.Add(_client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/over-async")));
        }

        try
        {
            Task.WaitAll([.. tasks]);
        }
        catch (AggregateException e)
        {
            Console.WriteLine("\nThe following exceptions have been thrown:");
            for (int j = 0; j < e.InnerExceptions.Count; j++)
            {
                Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
            }
        }
    }
}