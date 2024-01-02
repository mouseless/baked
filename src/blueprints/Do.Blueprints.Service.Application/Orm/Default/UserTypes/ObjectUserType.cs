using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.Type;
using System.Data.Common;

namespace Do.Orm.Default.UserTypes;

public class ObjectUserType : CompositeUserTypeBase
{
    public override string[] PropertyNames => ["Value"];
    public override IType[] PropertyTypes => [new JsonObjectStringType()];
    public override Type ReturnedClass => typeof(object);

    public override object GetPropertyValue(object component, int property) => JsonConvert.SerializeObject(component);

    public override object? NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
    {
        if (PropertyTypes[0].NullSafeGet(dr, names, session, owner) is string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Data could not be deserialized to object. Data: {jsonString}", e);
            }
        }

        return null;
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
    {
        PropertyTypes[0].NullSafeSet(cmd, GetPropertyValue(value, 0), index, settable, session);
    }
}
