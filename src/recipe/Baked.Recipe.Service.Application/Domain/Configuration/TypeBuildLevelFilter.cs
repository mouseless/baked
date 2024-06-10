using Do.Domain.Model;

namespace Do.Domain.Configuration;

public record TypeBuildLevelFilter(
    Func<TypeModelBuildContext, bool> Filter,
    TypeModel.Factory BuildLevel
);