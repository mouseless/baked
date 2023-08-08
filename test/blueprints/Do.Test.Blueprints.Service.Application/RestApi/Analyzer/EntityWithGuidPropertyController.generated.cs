// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class EntityWithGuidPropertyController
{
    readonly Func<EntityWithGuidProperty> _newTarget;
    readonly EntityWithGuidProperties _targets;

    public EntityWithGuidPropertyController(Func<EntityWithGuidProperty> newTarget, EntityWithGuidProperties targets) =>
      (_newTarget, _targets) = (newTarget, targets);


    [HttpPost]
    [Route("entity-with-guid-property")]
    public EntityWithGuidProperty New(Guid guid)
    {
        var target = _newTarget();

        return target.With(guid);
    }
}
