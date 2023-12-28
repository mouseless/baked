namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public interface IDomainModel
{
    TypeModel[] TypeModels { get; }
}
