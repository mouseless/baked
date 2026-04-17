namespace Baked.Domain.Configuration;

public record DomainModelBuilderDiagnostics
{
    public List<Exception> Errors { get; } = [];
}