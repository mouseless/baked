using Do.Domain.Model;
using Do.RestApi.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Do.Authentication.FixedBearerToken;

public class AddHashParameterToFormPostConvention(DomainModel _domainModel) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.UseForm) { return; }
        if (context.Action.Parameter.ContainsKey("hash")) { return; }

        context.Action.Parameter.Add("hash",
            new(
                TypeModel: _domainModel.Types[TypeModelReference.IdFrom(typeof(string))],
                From: RestApi.Model.ParameterModelFrom.BodyOrForm,
                Id: "hash"
            )
            {
                IsInvokeMethodParameter = false,
                AdditionalAttributes = [$"{nameof(RequiredAttribute)}", $"{nameof(JsonPropertyAttribute)}(Required = {nameof(Required)}.{Required.Always})"]
            }
        );
    }
}