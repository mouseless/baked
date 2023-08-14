namespace Do.Architecture;

public class NotFoundException : Exception
{
    public NotFoundException(string? message)
        : base( message: message == null ? "Context is empty" : 
            "Given type could not be found in ApplicationContext." +
            $" Did you mean ? {message}"
        )
    { }
}
