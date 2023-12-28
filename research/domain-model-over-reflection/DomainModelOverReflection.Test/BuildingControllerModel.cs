using DomainModelOverReflection.Api;
using DomainModelOverReflection.Test.Business;
using NUnit.Framework;
using Shouldly;

namespace DomainModelOverReflection.Test;

[TestFixture]
public class BuildingControllerModel
{
    static List<ControllerModel> Expected => new() {
            new(
                Name: "TestEntity",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntity/Method",
                        Method: HttpMethod.Post,
                        ReturnType: typeof(void),
                        Parameters: new() { new("text", typeof(string)) }
                    )
                }
            ),
            new(
                Name: "TestEntities",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntities/All",
                        Method: HttpMethod.Get,
                        ReturnType: typeof(List<TestEntity>),
                        Parameters: new()
                    )
                }
            )
        };

    [Test]
    public void Controller_model_is_built_using_reflection()
    {
        var apiModel = ApiModel.Build(typeof(TestEntity).Assembly);
        var actual = apiModel.ControllerModels;

        actual.ShouldBeEquivalentTo(Expected.OrderBy(c => c.Name).ToList());
    }

    [Test]
    public void Controller_model_is_built_using_domain_model()
    {
        var apiModel = ApiModel.Build(new DomainModel());
        var actual = apiModel.ControllerModels;

        actual.ShouldBeEquivalentTo(Expected.OrderBy(c => c.Name).ToList());
    }
}
