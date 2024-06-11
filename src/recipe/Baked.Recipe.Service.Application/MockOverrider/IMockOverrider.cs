namespace Baked.MockOverrider;

public interface IMockOverrider
{
    void Override(object mocked);
    void Reset();
}