using Do.Domain.Model;
using Do.Orm;
using Do.Orm.AutoMap;
using Do.RestApi.Model;

namespace Do;

public static class AutoMapOrmExtensions
{
    public static AutoMapOrmFeature AutoMap(this OrmConfigurator _) =>
        new();

    public static void AddSingleById<T>(this ControllerModel controller, DomainModel domainModel) =>
        controller.Action["SingleById"] = new("SingleById", HttpMethod.Get, $"{controller.TypeModel.Name}/{{id:guid}}", new(domainModel.Types[typeof(T)]), "target")
        {
            Parameters = [
                new(domainModel.Types[typeof(IQueryContext<T>)], ParameterModelFrom.Services, "target"),
                new(domainModel.Types[typeof(Guid)], ParameterModelFrom.Route, "id"),
                new(domainModel.Types[typeof(bool)], ParameterModelFrom.Query, "throwNotFound") { IsHardCoded = true, LookupRenderer = _ => "true" }
            ]
        };
}