using System.Text;

namespace Baked.Core;

public class EscapeFixTextWriter(TextWriter inner)
    : TextWriter
{
    public override Encoding Encoding => inner.Encoding;

    public override void Write(char value) =>
        inner.Write(value == '\e' ? '\x1b' : value);

    public override void Write(string? value) =>
        inner.Write(value?.Replace('\e', '\x1b'));

    public override void Flush() =>
        inner.Flush();
}