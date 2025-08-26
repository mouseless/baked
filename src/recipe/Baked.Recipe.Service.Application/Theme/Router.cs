namespace Baked.Theme;

public class Router
{
    public Route Create(string path, string title) =>
        new(path, title);
}