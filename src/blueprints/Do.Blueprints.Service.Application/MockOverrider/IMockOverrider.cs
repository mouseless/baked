namespace Do.MockOverrider;

public interface IMockOverrider
{
    void Override(object mocked);
    void Reset();
}