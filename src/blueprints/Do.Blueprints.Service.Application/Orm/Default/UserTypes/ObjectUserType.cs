using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.Type;
using System.Data.Common;

namespace Do.Orm.Default.UserTypes;

public class ObjectUserType : CompositeUserTypeBase
{
    public override string[] PropertyNames => new[] { "Value" };
    public override IType[] PropertyTypes => new[] { new SerializedObjectType() };
    public override Type ReturnedClass => typeof(object);

    public override object GetPropertyValue(object component, int property) => JsonConvert.SerializeObject(component);

    public override object? NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
    {
        if (PropertyTypes[0].NullSafeGet(dr, names, session, owner) is string stringClob)
        {
            return JsonConvert.DeserializeObject(stringClob);
        }

        return null;
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
    {
        PropertyTypes[0].NullSafeSet(cmd, GetPropertyValue(value, 0), index, settable, session);
    }
}
