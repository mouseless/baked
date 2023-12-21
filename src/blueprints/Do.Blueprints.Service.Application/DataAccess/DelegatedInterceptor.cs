using NHibernate;

namespace Do.DataAccess;

public class DelegatedInterceptor(IServiceProvider _serviceProvider, InterceptorConfiguration _interceptorConfiguration) : EmptyInterceptor
{
    ISessionFactory SessionFactory => _serviceProvider.GetRequiredServiceUsingRequestServices<ISessionFactory>();

    public override object Instantiate(string clazz, object id)
    {
        var metaData = SessionFactory.GetClassMetadata(clazz);
        var context = new InstantiationContext(metaData, _serviceProvider);

        return
            _interceptorConfiguration.Instantiator.Invoke(context, id) ??
            base.Instantiate(clazz, id);
    }
}
