using Do.Architecture;
using Do.Branding;
using Do.Domain.Model;
using Do.Orm;
using System.Reflection;

namespace Do.Test.Domain;

public class BuildingNonBusinessTypes
{
    /*
     *  This is a temporary test fixture for testing domain model for
     *  validating scenerios which cannot be tested with using outcomes
     *  of current layers and features of the test project
     */

    #region Setup & Helpers

    static readonly MethodInfo _idFrom = typeof(TypeModel).GetMethod(name: "IdFrom", bindingAttr: BindingFlags.Static | BindingFlags.NonPublic) ??
        throw new("'IdFrom' hould have existed");

    [OneTimeSetUp]
    public void Setup()
    {
        var context = new ApplicationContext();
        new Forge(new Mock<IBanner>().Object, () => new(context))
            .Application(app =>
            {
                app.Layers.AddConfiguration();
                app.Layers.AddDependencyInjection();
                app.Layers.AddDomain();
                app.Layers.AddTesting();

                app.Features.AddBusiness(c => c.Default(assemblies: [typeof(Entity).Assembly]));
            })
            .Run();

        DomainModel = context.GetDomainModel();
    }

    DomainModel DomainModel { get; set; } = default!;
    string IdFrom<T>() => IdFrom(typeof(T));
    string IdFrom(Type type) => $"{_idFrom.Invoke(null, [type])}";

    #endregion

    [Test]
    public void Non_business_types_are_added_to_type_collection([Values(typeof(string), typeof(int), typeof(Task), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        DomainModel.Types.TryGetValue(IdFrom(type), out var model);

        model.ShouldNotBeNull();
    }

    [Test]
    public void Non_business_types_with_no_generic_parameters_are_initialized_with_empty_collections([Values(typeof(string), typeof(int), typeof(Task))] Type type)
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

        model.Constructor.ShouldBeNull();
    }

    [Test]
    public void Non_business_types_with_generic_parameters_are_initialized_with_generic_arguments([Values(typeof(List<Entity>), typeof(Func<Entity>), typeof(IQueryContext<Entity>))] Type type)
    {
        var entityModel = DomainModel.Types[IdFrom<Entity>()];
        var listEntity = DomainModel.Types[IdFrom(type)];

        listEntity.ShouldNotBeNull();
        listEntity.GenericTypeArguments.Count().ShouldBe(1);
        listEntity.GenericTypeArguments.First().ShouldBe(entityModel);
    }

    [Test]
    public void Base_type_is_added_for_task()
    {
        var model = DomainModel.Types[IdFrom<Task<TransientWithTask>>()];

        model.ShouldNotBeNull();
        model.BaseType.ShouldNotBeNull();
        model.BaseType.IsAssignableTo<Task>();
    }
}
