using Baked.Domain.Model;
using Baked.Orm;
using Baked.Test.Lifetime;
using Baked.Test.Orm;

namespace Baked.Test.Business;

public class BuildingNonDomainTypes : TestServiceSpec
{
    [Test]
    public void Non_domain_types_are_added_to_domain_model([Values(typeof(string), typeof(int), typeof(Task), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var domainModel = GiveMe.TheDomainModel();

        domainModel.Types.Contains(type).ShouldBeTrue();
    }

    [Test]
    public void Non_domain_types_should_have_metadata_build_level([Values(typeof(int), typeof(char), typeof(string))] Type type)
    {
        var domainModel = GiveMe.TheDomainModel();
        var model = domainModel.Types[type];

        model.ShouldBeOfType<TypeModelMetadata>();
        model.ShouldNotBeOfType<TypeModelMembers>();
    }

    [Test]
    public void Non_domain_types_with_generic_parameters_are_initialized_with_generic_type_definition()
    {
        var domainModel = GiveMe.TheDomainModel();
        var genericType = domainModel.Types[typeof(List<Entity>)];

        genericType.ShouldBeAssignableTo<TypeModelGenerics>();
        genericType.GetGenerics().GenericTypeDefinition.ShouldNotBeNull();
        genericType.GetGenerics().GenericTypeDefinition?.IsAssignableTo(typeof(List<>)).ShouldBeTrue();
    }

    [Test]
    public void Non_domain_types_with_non_business_generic_parameters_are_initialized_with_generic_type_definition()
    {
        var domainModel = GiveMe.TheDomainModel();
        var genericModel = domainModel.Types[typeof(List<string>)];

        genericModel.ShouldBeAssignableTo<TypeModelGenerics>();
        genericModel.GetGenerics().GenericTypeDefinition.ShouldNotBeNull();
        genericModel.GetGenerics().GenericTypeDefinition?.IsAssignableTo(typeof(List<>)).ShouldBeTrue();
        genericModel.GetGenerics().GenericTypeArguments.First().Model.IsAssignableTo<string>().ShouldBeTrue();
    }

    [Test]
    public void Non_domain_types_with_generic_parameters_are_initialized_with_generic_arguments([Values(typeof(List<Entity>), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var domainModel = GiveMe.TheDomainModel();
        var entityModel = domainModel.Types[typeof(Entity)];
        var genericModel = domainModel.Types[type];

        genericModel.ShouldBeAssignableTo<TypeModelGenerics>();
        genericModel.GetGenerics().GenericTypeArguments.Count.ShouldBe(1);
        genericModel.GetGenerics().GenericTypeArguments.First().Model.ShouldBe(entityModel);
    }

    [Test]
    public void Base_type_is_added_for_task()
    {
        var domainModel = GiveMe.TheDomainModel();
        var model = domainModel.Types[typeof(Task<TransientAsync>)];

        model.ShouldNotBeNull();
        model.ShouldBeAssignableTo<TypeModelInheritance>();
        model.GetInheritance().BaseType?.IsAssignableTo<Task>().ShouldBeTrue();
    }

    [Test]
    public void IsAssignableTo_checks_generic_type_definition_of_interfaces_as_well([Values(typeof(List<string>), typeof(int[]), typeof(IEnumerable<DateTime>))] Type type)
    {
        var domainModel = GiveMe.TheDomainModel();
        var model = domainModel.Types[type];

        model.IsAssignableTo(typeof(IEnumerable<>)).ShouldBeTrue();
    }
}