using Baked.Business;

namespace Baked.Playground.Business.Casting;

public class ClassAToClassB(ClassB _to)
    : ICasts<ClassA, ClassB>
{
    ClassB ICasts<ClassA, ClassB>.To(ClassA from) =>
      _to;
}