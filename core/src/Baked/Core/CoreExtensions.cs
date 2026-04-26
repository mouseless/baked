using Baked.Architecture;
using Baked.Core;
using Baked.Testing;
using Newtonsoft.Json;
using Shouldly;
using Spectre.Console;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Baked;

public static class CoreExtensions
{
    extension(List<IFeature> features)
    {
        public void AddCore(FeatureFunc<CoreConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(Dictionary<string, string>? dictionary)
    {
        public Dictionary<string, string> Merge(Dictionary<string, string>? input)
        {
            dictionary ??= [];
            input ??= [];

            foreach (var (key, value) in input)
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
            }

            return dictionary;
        }
    }

    extension(string guidStr)
    {
        public Guid ToGuid() =>
            Guid.Parse(guidStr);
    }

    extension(Stubber giveMe)
    {
        public DateTime ADateTime(
            int year = 2023,
            int month = 9,
            int day = 17,
            int hour = 13,
            int minute = 29,
            int second = 00
        ) => new(year, month, day, hour, minute, second);

        public Dictionary<string, string> ADictionary() =>
            giveMe.ADictionary<string, string>();

        public Dictionary<TKey, TValue> ADictionary<TKey, TValue>(params IEnumerable<(TKey, TValue)> pairs)
            where TKey : notnull =>
            pairs.ToDictionary(pair => pair.Item1, pair => pair.Item2);

        public string AnEmail() =>
            "info@test.com";

        public Guid AGuid(
            string? starts = default
        )
        {
            starts ??= string.Empty;

            const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

            return Guid.Parse($"{starts}{template[starts.Length..]}");
        }

        public int AnInteger() =>
            42;

        public long ALong() =>
            1982329438L;

        public string AString(
            string? value = default,
            int? length = default
        )
        {
            if (length is not null)
            {
                return new('x', length.Value);
            }

            return value ?? "test string";
        }

        public Uri AUrl(
            string? url = default
        )
        {
            url ??= $"https://www.{Regex.Replace(giveMe.AGuid().ToString(), "[0-9]", "x")}.com";

            return new(url);
        }

        public PropertyInfo? ThePropertyOf<T>(string name) =>
            typeof(T).GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        public MethodInfo? TheMethodOf<T>(string name) =>
            typeof(T).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
    }

    extension(string? @string)
    {
        public void ShouldBe(object? expected, string format) =>
            @string.ShouldBe(string.Format(format, expected));
    }

    extension(Uri? uri)
    {
        public void ShouldBe(string urlString) =>
            uri?.ToString().ShouldBe(urlString);
    }

    extension<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        public void ShouldContainKeys(params TKey[] keys)
        {
            foreach (var key in keys)
            {
                dictionary.ShouldContainKey(key);
            }
        }

        public void ValuesShouldBe(params TValue[] values)
        {
            foreach (var value in dictionary.Values)
            {
                value.ShouldBe(values.FirstOrDefault());
                values = [.. values.Skip(1)];
            }
        }
    }

    extension<T>(T valueType) where T : struct, IParsable<T>
    {
        public void ShouldBe(string? @string)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                valueType.ShouldBe(default(T));
            }
            else
            {
                valueType.ToString().ShouldBe(@string);
            }
        }
    }

    extension<T>(T? valueType) where T : struct, IParsable<T>
    {
        public void ShouldBe(string? @string) =>
            valueType?.ToString().ShouldBe(@string);
    }

    extension(object? payload)
    {
        public void ShouldDeeplyBe(object? json,
            bool useSystemTextJson = false
        ) => payload
            .ToJsonString(useSystemTextJson: useSystemTextJson)
            .ShouldBe(json.ToJsonString(useSystemTextJson: useSystemTextJson));

        [return: NotNullIfNotNull("payload")]
        public string? ToJsonString(
            bool useSystemTextJson = false
        ) => payload is null ? null :
            useSystemTextJson ?
            System.Text.Json.JsonSerializer.Serialize(payload) :
            JsonConvert.SerializeObject(payload);

        [return: NotNullIfNotNull("payload")]
        public object? ToJsonObject() =>
            JsonConvert.DeserializeObject(payload.ToJsonString() ?? string.Empty);
    }

    extension(Type type)
    {
        public void ShouldBe<T>() =>
            type.ShouldBe(typeof(T));

        internal string GetName(bool includeDeclaringTypes)
        {
            if (!includeDeclaringTypes) { return type.Name; }

            var result = type.Name;
            for (var cur = type.DeclaringType; cur is not null; cur = cur.DeclaringType)
            {
                result = $"{cur.Name}.{result}";
            }

            return result;
        }
    }

    extension(PropertyInfo property)
    {
        public void ShouldBeAbstract()
        {
            var getMethod = property.GetGetMethod(true);

            getMethod.ShouldNotBeNull();
            getMethod.ShouldBeAbstract();
        }

        public void ShouldBeVirtual()
        {
            var getMethod = property.GetGetMethod(true);

            getMethod.ShouldNotBeNull();
            getMethod.ShouldBeVirtual();
        }
    }

    extension(MethodInfo method)
    {
        public void ShouldBeAbstract()
        {
            method.IsAbstract.ShouldBeTrue();
        }

        public void ShouldBeVirtual()
        {
            method.IsVirtual.ShouldBeTrue();
        }

        public void ShouldHaveOneParameter<T>()
        {
            method.GetParameters().Length.ShouldBe(1);
            method.GetParameters().First().ParameterType.ShouldBe<T>();
        }
    }

    // WARNING
    //
    // Do NOT remove this warning disable section unintentionally.
    // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0052
    static readonly IAnsiConsole _buildConsole = AnsiConsole.Create(new() { Out = new AnsiConsoleOutput(new EscapeFixTextWriter(Console.Out)) });
#pragma warning restore IDE0052

    extension(Console)
    {
        internal static IAnsiConsole Build => _buildConsole;
    }
}