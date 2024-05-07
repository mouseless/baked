using Do.Business;

namespace Do.Test.Business.Casting;

public class ClassAToClassB(ClassB _to)
    : ICasts<ClassA, ClassB>
{
    ClassB ICasts<ClassA, ClassB>.To(ClassA from) =>
      _to;
}