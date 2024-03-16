using Do.Domain.Model;
using Do.Orm;
using System.Reflection;

namespace Do.Test.Domain;

public class BuildingDomain : DomainTestSpec
{
    static readonly MethodInfo _idFrom = typeof(TypeModel).GetMethod(name: "IdFrom", bindingAttr: BindingFlags.Static | BindingFlags.NonPublic) ??
            throw new("'IdFrom' hould have existed");

    string IdFrom<T>() =>
        IdFrom(typeof(T));
    string IdFrom(Type type) =>
        $"{_idFrom.Invoke(null, [type])}";

    [Test]
    public void Non_business_types_are_added_to_type_collection([Values(typeof(string), typeof(int), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var action = () => DomainModel.Types[IdFrom(type)];

        action.ShouldNotThrow();
    }

    [Test]
    public void Non_business_types_are_initialized_with_empty_collections([Values(typeof(string), typeof(int))] Type type)
    {
        var model = DomainModel.Types[IdFrom(type)];

        model.Properties.ShouldNotBeNull();
        model.Properties.Count().ShouldBe(0);

        model.GenericTypeArguments.ShouldNotBeNull();
        model.GenericTypeArguments.Count().ShouldBe(0);

        model.Methods.ShouldNotBeNull();
        model.Methods.Count().ShouldBe(0);

        model.Interfaces.ShouldNotBeNull();
        model.Interfaces.Count().ShouldBe(0);

        model.CustomAttributes.ShouldNotBeNull();
        model.CustomAttributes.Count().ShouldBe(0);
    }

    [Test]
    public void Generic_arguments_are_built_for_generic_non_business_types([Values(typeof(List<Entity>), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var entityModel = DomainModel.Types[IdFrom<Entity>()];
        var listEntity = DomainModel.Types[IdFrom(type)];

        listEntity.ShouldNotBeNull();
        listEntity.GenericTypeArguments.Count().ShouldBe(1);
        listEntity.GenericTypeArguments.First().ShouldBe(entityModel);
    }

    [Test]
    public void Base_type_is_added_for_task([Values(typeof(Task<TransientWithTask>))] Type type)
    {
        var model = DomainModel.Types[IdFrom<Task<TransientWithTask>>()];

        model.ShouldNotBeNull();
        model.BaseType.ShouldNotBeNull();
    }
}
