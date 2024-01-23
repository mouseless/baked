using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class ParentEntitiesController
{
    [HttpGet]
    [Route("parentEntities")]
    public List<ParentEntity> All([FromServices] ParentEntities target, bool reverse = default, int? skip = default, int? take = default)
    {
        return target.All(reverse, skip, take);
    }
}
