namespace Do.Architecture;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(
            "Given type could not be found in ApplicationContext." +
            $" Did you mean ? {message}"
        )
    { }
}
