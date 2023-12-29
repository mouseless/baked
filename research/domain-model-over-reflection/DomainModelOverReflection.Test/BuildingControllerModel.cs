using Domain;
using DomainModelOverReflection.Api;
using DomainModelOverReflection.Models;
using DomainModelOverReflection.Test.Business;
using NUnit.Framework;
using Shouldly;

namespace DomainModelOverReflection.Test;

[TestFixture]
public class BuildingControllerModel
{
    [Test]
    public void Controller_model_is_built_using_reflection()
    {
        List<ControllerModel> Expected = new() {
            new(
                Name: "TestEntity",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntity/Method",
                        Method: HttpMethod.Post,
                        ReturnType: typeof(void).FullName!,
                        Parameters: new() { new("text", typeof(string).FullName!) }
                    )
                }
            ),
            new(
                Name: "TestEntities",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntities/All",
                        Method: HttpMethod.Get,
                        ReturnType: typeof(List<TestEntity>).FullName!,
                        Parameters: new()
                    )
                }
            ),
            new(
                Name: "TestOperationObject",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestOperationObject/Process",
                        Method: HttpMethod.Post,
                        ReturnType: typeof(void).FullName!,
                        Parameters: new()
                    )
                }
            )
        };

        var apiModel = ApiModel.Build(typeof(TestEntity).Assembly);
        var actual = apiModel.ControllerModels;

        actual.ShouldBeEquivalentTo(Expected.OrderBy(c => c.Name).ToList());
    }

    [Test]
    public void Controller_model_is_built_using_domain_model_with_reflection()
    {
        List<ControllerModel> Expected = new() {
            new(
                Name: "TestEntity",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntity/Method",
                        Method: HttpMethod.Post,
                        ReturnType: typeof(void).FullName!,
                        Parameters: new() { new("text", typeof(string).FullName!) }
                    )
                }
            ),
            new(
                Name: "TestEntities",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntities/All",
                        Method: HttpMethod.Get,
                        ReturnType: typeof(List<TestEntity>).FullName!,
                        Parameters: new()
                    )
                }
            ),
            new(
                Name: "TestOperationObject",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestOperationObject/Process",
                        Method: HttpMethod.Post,
                        ReturnType: typeof(void).FullName!,
                        Parameters: new()
                    )
                }
            )
        };

        var apiModel = ApiModel.Build(DomainModelWithRuntimeReflection.Build(typeof(TestEntity).Assembly));
        var actual = apiModel.ControllerModels;

        actual.ShouldBeEquivalentTo(Expected.OrderBy(c => c.Name).ToList());
    }

    [Test]
    public void Controller_model_is_built_using_domain_model()
    {
        List<ControllerModel> Expected = new() {
            new(
                Name: "TestEntity",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntity/Method",
                        Method: HttpMethod.Post,
                        ReturnType: "void",
                        Parameters: new() { new("text", "string") }
                    )
                }
            ),
            new(
                Name: "TestEntities",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestEntities/All",
                        Method: HttpMethod.Get,
                        ReturnType: "System.Collections.Generic.List<DomainModelOverReflection.Test.Business.TestEntity>",
                        Parameters: new()
                    )
                }
            ),
            new(
                Name: "TestOperationObject",
                Actions: new List<ActionModel> {
                    new(
                        Route: "TestOperationObject/Process",
                        Method: HttpMethod.Post,
                        ReturnType: "void",
                        Parameters: new()
                    )
                }
            )
        };

        var apiModel = ApiModel.Build(new DomainModelWithGeneration());
        var actual = apiModel.ControllerModels;

        actual.ShouldBeEquivalentTo(Expected.OrderBy(c => c.Name).ToList());
    }
}
