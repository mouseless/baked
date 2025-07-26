namespace Baked.CodeGeneration;

public class UnixLineEndingStringWriter : StringWriter
{
    public override string NewLine => "\n";
}