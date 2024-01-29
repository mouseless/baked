namespace Do.Test;

public class OperationWithGenericParameter<TEntity>
{
    public OperationWithGenericParameter<TEntity> With() => this;

    public void Execute() { }
}