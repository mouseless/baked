using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public record TypeBuildLevelFilter(
    Func<TypeModelBuildContext, bool> Filter,
    TypeModel.Factory BuildLevel
);