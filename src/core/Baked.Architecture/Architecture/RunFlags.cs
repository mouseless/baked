namespace Baked.Architecture;

[Flags]
public enum RunFlags
{
    Start = 1 << 0,
    Generate = 1 << 1
}