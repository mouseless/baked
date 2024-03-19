namespace Do.CodeGeneration;

public interface ICodeTemplate
{
    IEnumerable<string> Render();
}
