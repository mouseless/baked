namespace Baked.Architecture;

[Flags]
public enum RunFlags
{
    Start = 1 << 0,
    Bake = 1 << 1
}