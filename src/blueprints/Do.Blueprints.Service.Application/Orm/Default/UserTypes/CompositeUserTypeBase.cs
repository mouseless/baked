using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;
using System.Data.Common;

namespace Do.Orm.Default.UserTypes;

public abstract class CompositeUserTypeBase : ICompositeUserType
{
    public bool IsMutable => false;

    public abstract string[] PropertyNames { get; }
    public abstract IType[] PropertyTypes { get; }
    public abstract Type ReturnedClass { get; }

    public object Assemble(object cached, ISessionImplementor session, object owner) => cached;
    public object DeepCopy(object value) => value;
    public object Disassemble(object value, ISessionImplementor session) => value;
    public new bool Equals(object x, object y) => object.Equals(x, y);
    public int GetHashCode(object x) => x?.GetHashCode() ?? 0;
    public abstract object GetPropertyValue(object component, int property);
    public abstract object? NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner);
    public abstract void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session);
    public object Replace(object original, object target, ISessionImplementor session, object owner) => original;
    public void SetPropertyValue(object component, int property, object value) { }
}