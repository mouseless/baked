namespace Do.Domain.Model;

public interface IModel { }

public interface ICustomAttributesModel : IModel
{
    AttributeCollection CustomAttributes { get; }
}

public interface IKeyedModel : IModel
{
    string Id { get; }
}
