
namespace Do.Domain.Model;

public class TypeModelProcessors : IEnumerable<AddAttributeProcessor>
{
    readonly List<AddAttributeProcessor> _addAttributeProcessors = [];

    public void Add(Attribute add, Func<TypeModel, bool> when, int order)
    {
        _addAttributeProcessors.Add(new(add, when, order));
    }

    public IEnumerator<AddAttributeProcessor> GetEnumerator() =>
        ((IEnumerable<AddAttributeProcessor>)_addAttributeProcessors).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_addAttributeProcessors).GetEnumerator();
}
