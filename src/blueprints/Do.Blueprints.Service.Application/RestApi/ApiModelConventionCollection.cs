using Do.RestApi.Configuration;

namespace Do.RestApi;

public class ApiModelConventionCollection : List<(IApiModelConvention Convention, int Order)>, IApiModelConventionCollection
{ }