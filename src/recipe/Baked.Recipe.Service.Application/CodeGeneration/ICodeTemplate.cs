namespace Baked.CodeGeneration;

public interface ICodeTemplate
{
    IEnumerable<string> Render();
}