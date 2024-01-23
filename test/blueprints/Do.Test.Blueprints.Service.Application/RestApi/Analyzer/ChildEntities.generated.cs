using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class ChildEntitiesController
{
    [HttpGet]
    [Route("childEntities")]
    public List<ChildEntity> All([FromServices] ChildEntities target, int? skip = default, int? take = default)
    {
        return target.All(skip, take);
    }

    [HttpGet]
    [Route("childEntities/{parent}")]
    public List<ChildEntity> ByParent([FromServices] ChildEntities target,
        Guid parentId, bool reverse = default, int? skip = default, int? take = default)
    {
        return target.ByParent(parentId: parentId, reverse: reverse, skip: skip, take: take);
    }

    [HttpGet]
    [Route("childEntities/{parent}/first")]
    public ChildEntity FirstByParent([FromServices] ChildEntities target,
        Guid parentId, bool reverse = default)
    {
        return target.FirstByParent(parentId: parentId, reverse: reverse);
    }
}
