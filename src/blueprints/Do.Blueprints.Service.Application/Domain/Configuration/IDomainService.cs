namespace Do.Domain.Configuration;

public interface IDomainService
{
    static abstract IDomainService New(DomainServiceProvider sp);
}
