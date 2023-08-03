using NHibernate;

namespace Do.DataAccess;

public class DelegatedInterceptor : EmptyInterceptor
{
    readonly IServiceProvider _serviceProvider;
    readonly InterceptorConfiguration _interceptorConfiguration;

    public DelegatedInterceptor(IServiceProvider serviceProvider, InterceptorConfiguration interceptorConfiguration) =>
        (_serviceProvider, _interceptorConfiguration) = (serviceProvider, interceptorConfiguration);

    private ISessionFactory SessionFactory => _serviceProvider.GetRequiredServiceUsingRequestServices<ISessionFactory>();

    public override object Instantiate(string clazz, object id)
    {
        var metaData = SessionFactory.GetClassMetadata(clazz);
        var context = new InstantiationContext(metaData, _serviceProvider);

        return
            _interceptorConfiguration.Instantiator.Invoke(context, id) ??
            base.Instantiate(clazz, id);
    }
}
