using Do.Testing;

namespace Do.Test;

public abstract class TestServiceNfr : ServiceNfr<TestServiceNfr>, IEntryPoint
{
    public static void Main(string[] args) => Init(args);
}
