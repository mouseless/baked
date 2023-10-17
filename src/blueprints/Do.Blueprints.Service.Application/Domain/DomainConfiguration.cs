namespace Do.Domain;

public record DomainConfiguration
{
    public IDomainModel Model { get; set; } = new DomainModel();
}
