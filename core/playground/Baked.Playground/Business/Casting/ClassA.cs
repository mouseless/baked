using Baked.Business;

namespace Baked.Playground.Business.Casting;

public class ClassA
{
    public static implicit operator ClassB(ClassA classA) =>
        classA.Cast().To<ClassB>();
}