using Do.Architecture;
using Do.Domain.Model;

namespace Do.Test.Domain;

public abstract class DomainTestSpec : ServiceSpec
{
    static ApplicationContext _context = default!;

    static DomainTestSpec()
    {
        _context = Init(
            business: c => c.Default(assemblies: [typeof(Entity).Assembly]),
            communication: c => c.Mock(defaultResponses: response =>
            {
                response.ForClient<Singleton>(response: "test result");
                response.ForClient<Operation>(response: "path1 response", when: r => r.UrlOrPath.Equals("path1"));
                response.ForClient<Operation>(response: "path2 response", when: r => r.UrlOrPath.Equals("path2"));
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );
    }

    protected DomainModel DomainModel => _context.GetDomainModel();
}
