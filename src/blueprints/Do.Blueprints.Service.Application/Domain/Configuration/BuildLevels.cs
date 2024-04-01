using Do.Domain.Model;

namespace Do.Domain.Configuration;

public static class BuildLevels
{
    public static readonly TypeModel.Factory Basics = new TypeModel.Factory();
    public static readonly TypeModel.Factory Generics = new TypeModelGenerics.Factory();
    public static readonly TypeModel.Factory Inheritance = new TypeModelInheritance.Factory();
    public static readonly TypeModel.Factory Metadata = new TypeModelMetadata.Factory();
    public static readonly TypeModel.Factory Members = new TypeModelMembers.Factory();
}
