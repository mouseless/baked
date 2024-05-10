using Do.RestApi.Configuration;

namespace Do.RestApi;

public class ApiModelConventionCollection : List<IApiModelConvention>, IApiModelConventionCollection
{
    public new void Add(IApiModelConvention convention)
    {
        var index = Count % 2 == 0 ? Count / 2 : (Count + 1) / 2;
        Insert(index, convention);
    }
}