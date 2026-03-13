namespace Baked.Ui;

public interface IComponentOverride<TSchema> where TSchema : IComponentSchema
{
    TSchema Base { set; }
}