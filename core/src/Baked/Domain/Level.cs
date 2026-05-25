namespace Baked.Domain;

public class Level(int _base)
{
    int Base => _base;

    public Order Min => _base - 1000;
    public Order Max => _base + 1000;

    public static implicit operator Order(Level level) =>
        level.Base;
}
