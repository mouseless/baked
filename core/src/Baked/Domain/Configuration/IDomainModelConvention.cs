namespace Baked.Domain.Configuration;

public interface IDomainModelConvention
{
    public bool BeforeBuildingIndexes => false;
}

public interface IDomainModelConvention<TModel> : IDomainModelConvention
{
    void Apply(TModel model);
}