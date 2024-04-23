using Do.Test.Business;

namespace Do.Test;

public class Operation : IOperation
{
    string _query = default!;

    public Operation With(string query)
    {
        _query = query;

        return this;
    }

    public string Execute(string body) =>
        $"{_query}:{body}";
}