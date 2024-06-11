using Baked.RestApi.Configuration;

namespace Baked.RestApi;

public class ApiModelConventionCollection : List<(IApiModelConvention Convention, int Order)>, IApiModelConventionCollection
{ }