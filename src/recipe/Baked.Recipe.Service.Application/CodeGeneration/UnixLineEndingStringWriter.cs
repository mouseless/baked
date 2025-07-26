namespace Baked;

public class UnixLineEndingStringWriter : StringWriter
{
    public override string NewLine => "\n";
}