using Do.Architecture;
using Do.Branding;
using Moq;

namespace Do.Test;

public class BenchmarkSpec
{
    public static ApplicationContext Init(
        Action<ApplicationDescriptor>? describe = default
    )
    {
        var result = new ApplicationContext();

        new Forge(new Mock<IBanner>().Object, () => new(result))
            .Application(describe ?? (_ => { }))
            .Run();

        return result;
    }
}
