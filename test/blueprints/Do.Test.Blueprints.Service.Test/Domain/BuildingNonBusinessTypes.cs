using Do.Domain.Model;
using Do.Orm;

namespace Do.Test.Domain;

public class BuildingNonBusinessTypes : TestServiceSpec
{
    [Test]
    public void Non_business_types_are_added_to_type_collection([Values(typeof(string), typeof(int), typeof(Task), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        DomainModel.Types.Contains(type).ShouldBeTrue();
    }

    [Test]
    public void Non_business_types_value_types_with_no_generic_parameters_have_no_build_level([Values(typeof(int), typeof(char))] Type type)
    {
        var model = DomainModel.Types[type];

        model.ShouldBeOfType<TypeModel>();
        model.ShouldNotBeOfType<TypeModelGenerics>();
    }

    [Test]
    public void Non_business_types_have_inheritance_build_level([Values(typeof(string))] Type type)
    {
        var model = DomainModel.Types[type];

        model.ShouldBeOfType<TypeModelInheritance>();
    }

    [Test]
    public void Non_business_types_with_generic_parameters_are_initialized_with_generic_type_definition()
    {
        var entityType = DomainModel.Types[typeof(Entity)];
        var genericType = DomainModel.Types[typeof(List<Entity>)];

        genericType.ShouldBeAssignableTo<TypeModelGenerics>();
        genericType.GetGenerics().GenericTypeDefinition.ShouldNotBeNull();
        genericType.GetGenerics().GenericTypeDefinition?.IsAssignableTo(typeof(List<>)).ShouldBeTrue();
    }

    [Test]
    public void Non_business_types_with_non_business_generic_parameters_are_initialized_with_generic_type_definition()
    {
        var stringModel = DomainModel.Types[typeof(string)];
        var genericModel = DomainModel.Types[typeof(List<string>)];

        genericModel.ShouldBeAssignableTo<TypeModelGenerics>();
        genericModel.GetGenerics().GenericTypeDefinition.ShouldNotBeNull();
        genericModel.GetGenerics().GenericTypeDefinition?.IsAssignableTo(typeof(List<>)).ShouldBeTrue();
        genericModel.GetGenerics().GenericTypeArguments.First().Model.IsAssignableTo<string>().ShouldBeTrue();
    }

    [Test]
    public void Non_business_types_with_generic_parameters_are_initialized_with_generic_arguments([Values(typeof(List<Entity>), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var entityModel = DomainModel.Types[typeof(Entity)];
        var genericModel = DomainModel.Types[type];

        genericModel.ShouldBeAssignableTo<TypeModelGenerics>();
        genericModel.GetGenerics().GenericTypeArguments.Count.ShouldBe(1);
        genericModel.GetGenerics().GenericTypeArguments.First().Model.ShouldBe(entityModel);
    }

    [Test]
    public void Base_type_is_added_for_task()
    {
        var model = DomainModel.Types[typeof(Task<TransientWithTask>)];

        model.ShouldNotBeNull();
        model.ShouldBeOfType<TypeModelInheritance>();
        model.GetInheritance().BaseType?.IsAssignableTo<Task>().ShouldBeTrue();
    }

    [Test]
    public void IsAssignableTo_checks_generic_type_definition_of_interfaces_as_well([Values(typeof(List<string>), typeof(int[]), typeof(IEnumerable<DateTime>))] Type type)
    {
        var model = DomainModel.Types[type];

        model.IsAssignableTo(typeof(IEnumerable<>)).ShouldBeTrue();
    }
}
