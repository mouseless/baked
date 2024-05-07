using Do.Domain.Model;
using Do.RestApi.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Do.Authentication.FixedBearerToken;

public class AddRequestIdParameterToFormPostConvention(DomainModel _domainModel) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.UseForm) { return; }
        if (context.Action.Parameter.ContainsKey("requestId")) { return; }

        context.Action.Parameter.Add("requestId",
            new(
                TypeModel: _domainModel.Types[TypeModelReference.IdFrom(typeof(string))],
                From: RestApi.Model.ParameterModelFrom.BodyOrForm,
                Id: "requestId"
            )
            {
                IsInvokeMethodParameter = false,
                AdditionalAttributes = [$"{typeof(RequiredAttribute).Name}", $"{typeof(JsonPropertyAttribute).Name}(Required = {typeof(Required).Name}.{Required.Always})"]
            }
        );
    }
}