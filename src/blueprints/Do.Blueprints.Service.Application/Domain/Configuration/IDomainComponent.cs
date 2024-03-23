namespace Do.Domain.Configuration;

public interface IDomainComponent
{
    abstract static IDomainComponent New(BuildDomainContext domainBuilderContext);
}
