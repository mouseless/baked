using Baked.Orm;
using Baked.Playground.Orm;

namespace Baked.Playground.Override.RestApi;

public class ParentJsonConverter(IQueryContext<Parent> _queryContext)
    : EntityJsonConverter<Parent, string>(_queryContext)
{
    protected override string IdProp => "id";
    protected override IEnumerable<string> LabelProps => ["name"];

    protected override string GetId(Parent entity) =>
        entity.Id.ToString();

    protected override string GetLabel(Parent entity, string labelProp) =>
        labelProp switch
        {
            "name" => entity.Name,
            _ => throw new InvalidOperationException($"`{labelProp}` is not a label property for `{typeof(Parent).Name}`")
        };
}