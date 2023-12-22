#pragma warning disable SA1649 // File name should match first type name
namespace DomainModelOverReflection.Business;

public class Entity_0
{
    readonly IEntityContext<Entity_0> _context = default!;

    protected Entity_0() { }

    public Entity_0(IEntityContext<Entity_0> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_0 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_0s
{
    readonly IQueryContext<Entity_0> _queryContext;

    public Entity_0s(IQueryContext<Entity_0> queryContext) => _queryContext = queryContext;

    public List<Entity_0> By0(string name_0) => _queryContext.All();
    public List<Entity_0> By1(string name_1) => _queryContext.All();
}

public class Entity_1
{
    readonly IEntityContext<Entity_1> _context = default!;

    protected Entity_1() { }

    public Entity_1(IEntityContext<Entity_1> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_1 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_1s
{
    readonly IQueryContext<Entity_1> _queryContext;

    public Entity_1s(IQueryContext<Entity_1> queryContext) => _queryContext = queryContext;

    public List<Entity_1> By0(string name_0) => _queryContext.All();
    public List<Entity_1> By1(string name_1) => _queryContext.All();
}

public class Entity_2
{
    readonly IEntityContext<Entity_2> _context = default!;

    protected Entity_2() { }

    public Entity_2(IEntityContext<Entity_2> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_2 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_2s
{
    readonly IQueryContext<Entity_2> _queryContext;

    public Entity_2s(IQueryContext<Entity_2> queryContext) => _queryContext = queryContext;

    public List<Entity_2> By0(string name_0) => _queryContext.All();
    public List<Entity_2> By1(string name_1) => _queryContext.All();
}

public class Entity_3
{
    readonly IEntityContext<Entity_3> _context = default!;

    protected Entity_3() { }

    public Entity_3(IEntityContext<Entity_3> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_3 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_3s
{
    readonly IQueryContext<Entity_3> _queryContext;

    public Entity_3s(IQueryContext<Entity_3> queryContext) => _queryContext = queryContext;

    public List<Entity_3> By0(string name_0) => _queryContext.All();
    public List<Entity_3> By1(string name_1) => _queryContext.All();
}

public class Entity_4
{
    readonly IEntityContext<Entity_4> _context = default!;

    protected Entity_4() { }

    public Entity_4(IEntityContext<Entity_4> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_4 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_4s
{
    readonly IQueryContext<Entity_4> _queryContext;

    public Entity_4s(IQueryContext<Entity_4> queryContext) => _queryContext = queryContext;

    public List<Entity_4> By0(string name_0) => _queryContext.All();
    public List<Entity_4> By1(string name_1) => _queryContext.All();
}

public class Entity_5
{
    readonly IEntityContext<Entity_5> _context = default!;

    protected Entity_5() { }

    public Entity_5(IEntityContext<Entity_5> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_5 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_5s
{
    readonly IQueryContext<Entity_5> _queryContext;

    public Entity_5s(IQueryContext<Entity_5> queryContext) => _queryContext = queryContext;

    public List<Entity_5> By0(string name_0) => _queryContext.All();
    public List<Entity_5> By1(string name_1) => _queryContext.All();
}

public class Entity_6
{
    readonly IEntityContext<Entity_6> _context = default!;

    protected Entity_6() { }

    public Entity_6(IEntityContext<Entity_6> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_6 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_6s
{
    readonly IQueryContext<Entity_6> _queryContext;

    public Entity_6s(IQueryContext<Entity_6> queryContext) => _queryContext = queryContext;

    public List<Entity_6> By0(string name_0) => _queryContext.All();
    public List<Entity_6> By1(string name_1) => _queryContext.All();
}

public class Entity_7
{
    readonly IEntityContext<Entity_7> _context = default!;

    protected Entity_7() { }

    public Entity_7(IEntityContext<Entity_7> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_7 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_7s
{
    readonly IQueryContext<Entity_7> _queryContext;

    public Entity_7s(IQueryContext<Entity_7> queryContext) => _queryContext = queryContext;

    public List<Entity_7> By0(string name_0) => _queryContext.All();
    public List<Entity_7> By1(string name_1) => _queryContext.All();
}

public class Entity_8
{
    readonly IEntityContext<Entity_8> _context = default!;

    protected Entity_8() { }

    public Entity_8(IEntityContext<Entity_8> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_8 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_8s
{
    readonly IQueryContext<Entity_8> _queryContext;

    public Entity_8s(IQueryContext<Entity_8> queryContext) => _queryContext = queryContext;

    public List<Entity_8> By0(string name_0) => _queryContext.All();
    public List<Entity_8> By1(string name_1) => _queryContext.All();
}

public class Entity_9
{
    readonly IEntityContext<Entity_9> _context = default!;

    protected Entity_9() { }

    public Entity_9(IEntityContext<Entity_9> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_9 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_9s
{
    readonly IQueryContext<Entity_9> _queryContext;

    public Entity_9s(IQueryContext<Entity_9> queryContext) => _queryContext = queryContext;

    public List<Entity_9> By0(string name_0) => _queryContext.All();
    public List<Entity_9> By1(string name_1) => _queryContext.All();
}

public class Entity_10
{
    readonly IEntityContext<Entity_10> _context = default!;

    protected Entity_10() { }

    public Entity_10(IEntityContext<Entity_10> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_10 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_10s
{
    readonly IQueryContext<Entity_10> _queryContext;

    public Entity_10s(IQueryContext<Entity_10> queryContext) => _queryContext = queryContext;

    public List<Entity_10> By0(string name_0) => _queryContext.All();
    public List<Entity_10> By1(string name_1) => _queryContext.All();
}

public class Entity_11
{
    readonly IEntityContext<Entity_11> _context = default!;

    protected Entity_11() { }

    public Entity_11(IEntityContext<Entity_11> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_11 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_11s
{
    readonly IQueryContext<Entity_11> _queryContext;

    public Entity_11s(IQueryContext<Entity_11> queryContext) => _queryContext = queryContext;

    public List<Entity_11> By0(string name_0) => _queryContext.All();
    public List<Entity_11> By1(string name_1) => _queryContext.All();
}

public class Entity_12
{
    readonly IEntityContext<Entity_12> _context = default!;

    protected Entity_12() { }

    public Entity_12(IEntityContext<Entity_12> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_12 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_12s
{
    readonly IQueryContext<Entity_12> _queryContext;

    public Entity_12s(IQueryContext<Entity_12> queryContext) => _queryContext = queryContext;

    public List<Entity_12> By0(string name_0) => _queryContext.All();
    public List<Entity_12> By1(string name_1) => _queryContext.All();
}

public class Entity_13
{
    readonly IEntityContext<Entity_13> _context = default!;

    protected Entity_13() { }

    public Entity_13(IEntityContext<Entity_13> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_13 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_13s
{
    readonly IQueryContext<Entity_13> _queryContext;

    public Entity_13s(IQueryContext<Entity_13> queryContext) => _queryContext = queryContext;

    public List<Entity_13> By0(string name_0) => _queryContext.All();
    public List<Entity_13> By1(string name_1) => _queryContext.All();
}

public class Entity_14
{
    readonly IEntityContext<Entity_14> _context = default!;

    protected Entity_14() { }

    public Entity_14(IEntityContext<Entity_14> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_14 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_14s
{
    readonly IQueryContext<Entity_14> _queryContext;

    public Entity_14s(IQueryContext<Entity_14> queryContext) => _queryContext = queryContext;

    public List<Entity_14> By0(string name_0) => _queryContext.All();
    public List<Entity_14> By1(string name_1) => _queryContext.All();
}

public class Entity_15
{
    readonly IEntityContext<Entity_15> _context = default!;

    protected Entity_15() { }

    public Entity_15(IEntityContext<Entity_15> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_15 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_15s
{
    readonly IQueryContext<Entity_15> _queryContext;

    public Entity_15s(IQueryContext<Entity_15> queryContext) => _queryContext = queryContext;

    public List<Entity_15> By0(string name_0) => _queryContext.All();
    public List<Entity_15> By1(string name_1) => _queryContext.All();
}

public class Entity_16
{
    readonly IEntityContext<Entity_16> _context = default!;

    protected Entity_16() { }

    public Entity_16(IEntityContext<Entity_16> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_16 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_16s
{
    readonly IQueryContext<Entity_16> _queryContext;

    public Entity_16s(IQueryContext<Entity_16> queryContext) => _queryContext = queryContext;

    public List<Entity_16> By0(string name_0) => _queryContext.All();
    public List<Entity_16> By1(string name_1) => _queryContext.All();
}

public class Entity_17
{
    readonly IEntityContext<Entity_17> _context = default!;

    protected Entity_17() { }

    public Entity_17(IEntityContext<Entity_17> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_17 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_17s
{
    readonly IQueryContext<Entity_17> _queryContext;

    public Entity_17s(IQueryContext<Entity_17> queryContext) => _queryContext = queryContext;

    public List<Entity_17> By0(string name_0) => _queryContext.All();
    public List<Entity_17> By1(string name_1) => _queryContext.All();
}

public class Entity_18
{
    readonly IEntityContext<Entity_18> _context = default!;

    protected Entity_18() { }

    public Entity_18(IEntityContext<Entity_18> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_18 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_18s
{
    readonly IQueryContext<Entity_18> _queryContext;

    public Entity_18s(IQueryContext<Entity_18> queryContext) => _queryContext = queryContext;

    public List<Entity_18> By0(string name_0) => _queryContext.All();
    public List<Entity_18> By1(string name_1) => _queryContext.All();
}

public class Entity_19
{
    readonly IEntityContext<Entity_19> _context = default!;

    protected Entity_19() { }

    public Entity_19(IEntityContext<Entity_19> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_19 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_19s
{
    readonly IQueryContext<Entity_19> _queryContext;

    public Entity_19s(IQueryContext<Entity_19> queryContext) => _queryContext = queryContext;

    public List<Entity_19> By0(string name_0) => _queryContext.All();
    public List<Entity_19> By1(string name_1) => _queryContext.All();
}

public class Entity_20
{
    readonly IEntityContext<Entity_20> _context = default!;

    protected Entity_20() { }

    public Entity_20(IEntityContext<Entity_20> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_20 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_20s
{
    readonly IQueryContext<Entity_20> _queryContext;

    public Entity_20s(IQueryContext<Entity_20> queryContext) => _queryContext = queryContext;

    public List<Entity_20> By0(string name_0) => _queryContext.All();
    public List<Entity_20> By1(string name_1) => _queryContext.All();
}

public class Entity_21
{
    readonly IEntityContext<Entity_21> _context = default!;

    protected Entity_21() { }

    public Entity_21(IEntityContext<Entity_21> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_21 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_21s
{
    readonly IQueryContext<Entity_21> _queryContext;

    public Entity_21s(IQueryContext<Entity_21> queryContext) => _queryContext = queryContext;

    public List<Entity_21> By0(string name_0) => _queryContext.All();
    public List<Entity_21> By1(string name_1) => _queryContext.All();
}

public class Entity_22
{
    readonly IEntityContext<Entity_22> _context = default!;

    protected Entity_22() { }

    public Entity_22(IEntityContext<Entity_22> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_22 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_22s
{
    readonly IQueryContext<Entity_22> _queryContext;

    public Entity_22s(IQueryContext<Entity_22> queryContext) => _queryContext = queryContext;

    public List<Entity_22> By0(string name_0) => _queryContext.All();
    public List<Entity_22> By1(string name_1) => _queryContext.All();
}

public class Entity_23
{
    readonly IEntityContext<Entity_23> _context = default!;

    protected Entity_23() { }

    public Entity_23(IEntityContext<Entity_23> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_23 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_23s
{
    readonly IQueryContext<Entity_23> _queryContext;

    public Entity_23s(IQueryContext<Entity_23> queryContext) => _queryContext = queryContext;

    public List<Entity_23> By0(string name_0) => _queryContext.All();
    public List<Entity_23> By1(string name_1) => _queryContext.All();
}

public class Entity_24
{
    readonly IEntityContext<Entity_24> _context = default!;

    protected Entity_24() { }

    public Entity_24(IEntityContext<Entity_24> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_24 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_24s
{
    readonly IQueryContext<Entity_24> _queryContext;

    public Entity_24s(IQueryContext<Entity_24> queryContext) => _queryContext = queryContext;

    public List<Entity_24> By0(string name_0) => _queryContext.All();
    public List<Entity_24> By1(string name_1) => _queryContext.All();
}

public class Entity_25
{
    readonly IEntityContext<Entity_25> _context = default!;

    protected Entity_25() { }

    public Entity_25(IEntityContext<Entity_25> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_25 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_25s
{
    readonly IQueryContext<Entity_25> _queryContext;

    public Entity_25s(IQueryContext<Entity_25> queryContext) => _queryContext = queryContext;

    public List<Entity_25> By0(string name_0) => _queryContext.All();
    public List<Entity_25> By1(string name_1) => _queryContext.All();
}

public class Entity_26
{
    readonly IEntityContext<Entity_26> _context = default!;

    protected Entity_26() { }

    public Entity_26(IEntityContext<Entity_26> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_26 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_26s
{
    readonly IQueryContext<Entity_26> _queryContext;

    public Entity_26s(IQueryContext<Entity_26> queryContext) => _queryContext = queryContext;

    public List<Entity_26> By0(string name_0) => _queryContext.All();
    public List<Entity_26> By1(string name_1) => _queryContext.All();
}

public class Entity_27
{
    readonly IEntityContext<Entity_27> _context = default!;

    protected Entity_27() { }

    public Entity_27(IEntityContext<Entity_27> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_27 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_27s
{
    readonly IQueryContext<Entity_27> _queryContext;

    public Entity_27s(IQueryContext<Entity_27> queryContext) => _queryContext = queryContext;

    public List<Entity_27> By0(string name_0) => _queryContext.All();
    public List<Entity_27> By1(string name_1) => _queryContext.All();
}

public class Entity_28
{
    readonly IEntityContext<Entity_28> _context = default!;

    protected Entity_28() { }

    public Entity_28(IEntityContext<Entity_28> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_28 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_28s
{
    readonly IQueryContext<Entity_28> _queryContext;

    public Entity_28s(IQueryContext<Entity_28> queryContext) => _queryContext = queryContext;

    public List<Entity_28> By0(string name_0) => _queryContext.All();
    public List<Entity_28> By1(string name_1) => _queryContext.All();
}

public class Entity_29
{
    readonly IEntityContext<Entity_29> _context = default!;

    protected Entity_29() { }

    public Entity_29(IEntityContext<Entity_29> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_29 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_29s
{
    readonly IQueryContext<Entity_29> _queryContext;

    public Entity_29s(IQueryContext<Entity_29> queryContext) => _queryContext = queryContext;

    public List<Entity_29> By0(string name_0) => _queryContext.All();
    public List<Entity_29> By1(string name_1) => _queryContext.All();
}

public class Entity_30
{
    readonly IEntityContext<Entity_30> _context = default!;

    protected Entity_30() { }

    public Entity_30(IEntityContext<Entity_30> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_30 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_30s
{
    readonly IQueryContext<Entity_30> _queryContext;

    public Entity_30s(IQueryContext<Entity_30> queryContext) => _queryContext = queryContext;

    public List<Entity_30> By0(string name_0) => _queryContext.All();
    public List<Entity_30> By1(string name_1) => _queryContext.All();
}

public class Entity_31
{
    readonly IEntityContext<Entity_31> _context = default!;

    protected Entity_31() { }

    public Entity_31(IEntityContext<Entity_31> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_31 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_31s
{
    readonly IQueryContext<Entity_31> _queryContext;

    public Entity_31s(IQueryContext<Entity_31> queryContext) => _queryContext = queryContext;

    public List<Entity_31> By0(string name_0) => _queryContext.All();
    public List<Entity_31> By1(string name_1) => _queryContext.All();
}

public class Entity_32
{
    readonly IEntityContext<Entity_32> _context = default!;

    protected Entity_32() { }

    public Entity_32(IEntityContext<Entity_32> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_32 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_32s
{
    readonly IQueryContext<Entity_32> _queryContext;

    public Entity_32s(IQueryContext<Entity_32> queryContext) => _queryContext = queryContext;

    public List<Entity_32> By0(string name_0) => _queryContext.All();
    public List<Entity_32> By1(string name_1) => _queryContext.All();
}

public class Entity_33
{
    readonly IEntityContext<Entity_33> _context = default!;

    protected Entity_33() { }

    public Entity_33(IEntityContext<Entity_33> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_33 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_33s
{
    readonly IQueryContext<Entity_33> _queryContext;

    public Entity_33s(IQueryContext<Entity_33> queryContext) => _queryContext = queryContext;

    public List<Entity_33> By0(string name_0) => _queryContext.All();
    public List<Entity_33> By1(string name_1) => _queryContext.All();
}

public class Entity_34
{
    readonly IEntityContext<Entity_34> _context = default!;

    protected Entity_34() { }

    public Entity_34(IEntityContext<Entity_34> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_34 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_34s
{
    readonly IQueryContext<Entity_34> _queryContext;

    public Entity_34s(IQueryContext<Entity_34> queryContext) => _queryContext = queryContext;

    public List<Entity_34> By0(string name_0) => _queryContext.All();
    public List<Entity_34> By1(string name_1) => _queryContext.All();
}

public class Entity_35
{
    readonly IEntityContext<Entity_35> _context = default!;

    protected Entity_35() { }

    public Entity_35(IEntityContext<Entity_35> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_35 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_35s
{
    readonly IQueryContext<Entity_35> _queryContext;

    public Entity_35s(IQueryContext<Entity_35> queryContext) => _queryContext = queryContext;

    public List<Entity_35> By0(string name_0) => _queryContext.All();
    public List<Entity_35> By1(string name_1) => _queryContext.All();
}

public class Entity_36
{
    readonly IEntityContext<Entity_36> _context = default!;

    protected Entity_36() { }

    public Entity_36(IEntityContext<Entity_36> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_36 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_36s
{
    readonly IQueryContext<Entity_36> _queryContext;

    public Entity_36s(IQueryContext<Entity_36> queryContext) => _queryContext = queryContext;

    public List<Entity_36> By0(string name_0) => _queryContext.All();
    public List<Entity_36> By1(string name_1) => _queryContext.All();
}

public class Entity_37
{
    readonly IEntityContext<Entity_37> _context = default!;

    protected Entity_37() { }

    public Entity_37(IEntityContext<Entity_37> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_37 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_37s
{
    readonly IQueryContext<Entity_37> _queryContext;

    public Entity_37s(IQueryContext<Entity_37> queryContext) => _queryContext = queryContext;

    public List<Entity_37> By0(string name_0) => _queryContext.All();
    public List<Entity_37> By1(string name_1) => _queryContext.All();
}

public class Entity_38
{
    readonly IEntityContext<Entity_38> _context = default!;

    protected Entity_38() { }

    public Entity_38(IEntityContext<Entity_38> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_38 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_38s
{
    readonly IQueryContext<Entity_38> _queryContext;

    public Entity_38s(IQueryContext<Entity_38> queryContext) => _queryContext = queryContext;

    public List<Entity_38> By0(string name_0) => _queryContext.All();
    public List<Entity_38> By1(string name_1) => _queryContext.All();
}

public class Entity_39
{
    readonly IEntityContext<Entity_39> _context = default!;

    protected Entity_39() { }

    public Entity_39(IEntityContext<Entity_39> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_39 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_39s
{
    readonly IQueryContext<Entity_39> _queryContext;

    public Entity_39s(IQueryContext<Entity_39> queryContext) => _queryContext = queryContext;

    public List<Entity_39> By0(string name_0) => _queryContext.All();
    public List<Entity_39> By1(string name_1) => _queryContext.All();
}

public class Entity_40
{
    readonly IEntityContext<Entity_40> _context = default!;

    protected Entity_40() { }

    public Entity_40(IEntityContext<Entity_40> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_40 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_40s
{
    readonly IQueryContext<Entity_40> _queryContext;

    public Entity_40s(IQueryContext<Entity_40> queryContext) => _queryContext = queryContext;

    public List<Entity_40> By0(string name_0) => _queryContext.All();
    public List<Entity_40> By1(string name_1) => _queryContext.All();
}

public class Entity_41
{
    readonly IEntityContext<Entity_41> _context = default!;

    protected Entity_41() { }

    public Entity_41(IEntityContext<Entity_41> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_41 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_41s
{
    readonly IQueryContext<Entity_41> _queryContext;

    public Entity_41s(IQueryContext<Entity_41> queryContext) => _queryContext = queryContext;

    public List<Entity_41> By0(string name_0) => _queryContext.All();
    public List<Entity_41> By1(string name_1) => _queryContext.All();
}

public class Entity_42
{
    readonly IEntityContext<Entity_42> _context = default!;

    protected Entity_42() { }

    public Entity_42(IEntityContext<Entity_42> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_42 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_42s
{
    readonly IQueryContext<Entity_42> _queryContext;

    public Entity_42s(IQueryContext<Entity_42> queryContext) => _queryContext = queryContext;

    public List<Entity_42> By0(string name_0) => _queryContext.All();
    public List<Entity_42> By1(string name_1) => _queryContext.All();
}

public class Entity_43
{
    readonly IEntityContext<Entity_43> _context = default!;

    protected Entity_43() { }

    public Entity_43(IEntityContext<Entity_43> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_43 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_43s
{
    readonly IQueryContext<Entity_43> _queryContext;

    public Entity_43s(IQueryContext<Entity_43> queryContext) => _queryContext = queryContext;

    public List<Entity_43> By0(string name_0) => _queryContext.All();
    public List<Entity_43> By1(string name_1) => _queryContext.All();
}

public class Entity_44
{
    readonly IEntityContext<Entity_44> _context = default!;

    protected Entity_44() { }

    public Entity_44(IEntityContext<Entity_44> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_44 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_44s
{
    readonly IQueryContext<Entity_44> _queryContext;

    public Entity_44s(IQueryContext<Entity_44> queryContext) => _queryContext = queryContext;

    public List<Entity_44> By0(string name_0) => _queryContext.All();
    public List<Entity_44> By1(string name_1) => _queryContext.All();
}

public class Entity_45
{
    readonly IEntityContext<Entity_45> _context = default!;

    protected Entity_45() { }

    public Entity_45(IEntityContext<Entity_45> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_45 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_45s
{
    readonly IQueryContext<Entity_45> _queryContext;

    public Entity_45s(IQueryContext<Entity_45> queryContext) => _queryContext = queryContext;

    public List<Entity_45> By0(string name_0) => _queryContext.All();
    public List<Entity_45> By1(string name_1) => _queryContext.All();
}

public class Entity_46
{
    readonly IEntityContext<Entity_46> _context = default!;

    protected Entity_46() { }

    public Entity_46(IEntityContext<Entity_46> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_46 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_46s
{
    readonly IQueryContext<Entity_46> _queryContext;

    public Entity_46s(IQueryContext<Entity_46> queryContext) => _queryContext = queryContext;

    public List<Entity_46> By0(string name_0) => _queryContext.All();
    public List<Entity_46> By1(string name_1) => _queryContext.All();
}

public class Entity_47
{
    readonly IEntityContext<Entity_47> _context = default!;

    protected Entity_47() { }

    public Entity_47(IEntityContext<Entity_47> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_47 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_47s
{
    readonly IQueryContext<Entity_47> _queryContext;

    public Entity_47s(IQueryContext<Entity_47> queryContext) => _queryContext = queryContext;

    public List<Entity_47> By0(string name_0) => _queryContext.All();
    public List<Entity_47> By1(string name_1) => _queryContext.All();
}

public class Entity_48
{
    readonly IEntityContext<Entity_48> _context = default!;

    protected Entity_48() { }

    public Entity_48(IEntityContext<Entity_48> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_48 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_48s
{
    readonly IQueryContext<Entity_48> _queryContext;

    public Entity_48s(IQueryContext<Entity_48> queryContext) => _queryContext = queryContext;

    public List<Entity_48> By0(string name_0) => _queryContext.All();
    public List<Entity_48> By1(string name_1) => _queryContext.All();
}

public class Entity_49
{
    readonly IEntityContext<Entity_49> _context = default!;

    protected Entity_49() { }

    public Entity_49(IEntityContext<Entity_49> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_49 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_49s
{
    readonly IQueryContext<Entity_49> _queryContext;

    public Entity_49s(IQueryContext<Entity_49> queryContext) => _queryContext = queryContext;

    public List<Entity_49> By0(string name_0) => _queryContext.All();
    public List<Entity_49> By1(string name_1) => _queryContext.All();
}

public class Entity_50
{
    readonly IEntityContext<Entity_50> _context = default!;

    protected Entity_50() { }

    public Entity_50(IEntityContext<Entity_50> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_50 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_50s
{
    readonly IQueryContext<Entity_50> _queryContext;

    public Entity_50s(IQueryContext<Entity_50> queryContext) => _queryContext = queryContext;

    public List<Entity_50> By0(string name_0) => _queryContext.All();
    public List<Entity_50> By1(string name_1) => _queryContext.All();
}

public class Entity_51
{
    readonly IEntityContext<Entity_51> _context = default!;

    protected Entity_51() { }

    public Entity_51(IEntityContext<Entity_51> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_51 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_51s
{
    readonly IQueryContext<Entity_51> _queryContext;

    public Entity_51s(IQueryContext<Entity_51> queryContext) => _queryContext = queryContext;

    public List<Entity_51> By0(string name_0) => _queryContext.All();
    public List<Entity_51> By1(string name_1) => _queryContext.All();
}

public class Entity_52
{
    readonly IEntityContext<Entity_52> _context = default!;

    protected Entity_52() { }

    public Entity_52(IEntityContext<Entity_52> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_52 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_52s
{
    readonly IQueryContext<Entity_52> _queryContext;

    public Entity_52s(IQueryContext<Entity_52> queryContext) => _queryContext = queryContext;

    public List<Entity_52> By0(string name_0) => _queryContext.All();
    public List<Entity_52> By1(string name_1) => _queryContext.All();
}

public class Entity_53
{
    readonly IEntityContext<Entity_53> _context = default!;

    protected Entity_53() { }

    public Entity_53(IEntityContext<Entity_53> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_53 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_53s
{
    readonly IQueryContext<Entity_53> _queryContext;

    public Entity_53s(IQueryContext<Entity_53> queryContext) => _queryContext = queryContext;

    public List<Entity_53> By0(string name_0) => _queryContext.All();
    public List<Entity_53> By1(string name_1) => _queryContext.All();
}

public class Entity_54
{
    readonly IEntityContext<Entity_54> _context = default!;

    protected Entity_54() { }

    public Entity_54(IEntityContext<Entity_54> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_54 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_54s
{
    readonly IQueryContext<Entity_54> _queryContext;

    public Entity_54s(IQueryContext<Entity_54> queryContext) => _queryContext = queryContext;

    public List<Entity_54> By0(string name_0) => _queryContext.All();
    public List<Entity_54> By1(string name_1) => _queryContext.All();
}

public class Entity_55
{
    readonly IEntityContext<Entity_55> _context = default!;

    protected Entity_55() { }

    public Entity_55(IEntityContext<Entity_55> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_55 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_55s
{
    readonly IQueryContext<Entity_55> _queryContext;

    public Entity_55s(IQueryContext<Entity_55> queryContext) => _queryContext = queryContext;

    public List<Entity_55> By0(string name_0) => _queryContext.All();
    public List<Entity_55> By1(string name_1) => _queryContext.All();
}

public class Entity_56
{
    readonly IEntityContext<Entity_56> _context = default!;

    protected Entity_56() { }

    public Entity_56(IEntityContext<Entity_56> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_56 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_56s
{
    readonly IQueryContext<Entity_56> _queryContext;

    public Entity_56s(IQueryContext<Entity_56> queryContext) => _queryContext = queryContext;

    public List<Entity_56> By0(string name_0) => _queryContext.All();
    public List<Entity_56> By1(string name_1) => _queryContext.All();
}

public class Entity_57
{
    readonly IEntityContext<Entity_57> _context = default!;

    protected Entity_57() { }

    public Entity_57(IEntityContext<Entity_57> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_57 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_57s
{
    readonly IQueryContext<Entity_57> _queryContext;

    public Entity_57s(IQueryContext<Entity_57> queryContext) => _queryContext = queryContext;

    public List<Entity_57> By0(string name_0) => _queryContext.All();
    public List<Entity_57> By1(string name_1) => _queryContext.All();
}

public class Entity_58
{
    readonly IEntityContext<Entity_58> _context = default!;

    protected Entity_58() { }

    public Entity_58(IEntityContext<Entity_58> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_58 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_58s
{
    readonly IQueryContext<Entity_58> _queryContext;

    public Entity_58s(IQueryContext<Entity_58> queryContext) => _queryContext = queryContext;

    public List<Entity_58> By0(string name_0) => _queryContext.All();
    public List<Entity_58> By1(string name_1) => _queryContext.All();
}

public class Entity_59
{
    readonly IEntityContext<Entity_59> _context = default!;

    protected Entity_59() { }

    public Entity_59(IEntityContext<Entity_59> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_59 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_59s
{
    readonly IQueryContext<Entity_59> _queryContext;

    public Entity_59s(IQueryContext<Entity_59> queryContext) => _queryContext = queryContext;

    public List<Entity_59> By0(string name_0) => _queryContext.All();
    public List<Entity_59> By1(string name_1) => _queryContext.All();
}

public class Entity_60
{
    readonly IEntityContext<Entity_60> _context = default!;

    protected Entity_60() { }

    public Entity_60(IEntityContext<Entity_60> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_60 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_60s
{
    readonly IQueryContext<Entity_60> _queryContext;

    public Entity_60s(IQueryContext<Entity_60> queryContext) => _queryContext = queryContext;

    public List<Entity_60> By0(string name_0) => _queryContext.All();
    public List<Entity_60> By1(string name_1) => _queryContext.All();
}

public class Entity_61
{
    readonly IEntityContext<Entity_61> _context = default!;

    protected Entity_61() { }

    public Entity_61(IEntityContext<Entity_61> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_61 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_61s
{
    readonly IQueryContext<Entity_61> _queryContext;

    public Entity_61s(IQueryContext<Entity_61> queryContext) => _queryContext = queryContext;

    public List<Entity_61> By0(string name_0) => _queryContext.All();
    public List<Entity_61> By1(string name_1) => _queryContext.All();
}

public class Entity_62
{
    readonly IEntityContext<Entity_62> _context = default!;

    protected Entity_62() { }

    public Entity_62(IEntityContext<Entity_62> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_62 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_62s
{
    readonly IQueryContext<Entity_62> _queryContext;

    public Entity_62s(IQueryContext<Entity_62> queryContext) => _queryContext = queryContext;

    public List<Entity_62> By0(string name_0) => _queryContext.All();
    public List<Entity_62> By1(string name_1) => _queryContext.All();
}

public class Entity_63
{
    readonly IEntityContext<Entity_63> _context = default!;

    protected Entity_63() { }

    public Entity_63(IEntityContext<Entity_63> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_63 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_63s
{
    readonly IQueryContext<Entity_63> _queryContext;

    public Entity_63s(IQueryContext<Entity_63> queryContext) => _queryContext = queryContext;

    public List<Entity_63> By0(string name_0) => _queryContext.All();
    public List<Entity_63> By1(string name_1) => _queryContext.All();
}

public class Entity_64
{
    readonly IEntityContext<Entity_64> _context = default!;

    protected Entity_64() { }

    public Entity_64(IEntityContext<Entity_64> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_64 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_64s
{
    readonly IQueryContext<Entity_64> _queryContext;

    public Entity_64s(IQueryContext<Entity_64> queryContext) => _queryContext = queryContext;

    public List<Entity_64> By0(string name_0) => _queryContext.All();
    public List<Entity_64> By1(string name_1) => _queryContext.All();
}

public class Entity_65
{
    readonly IEntityContext<Entity_65> _context = default!;

    protected Entity_65() { }

    public Entity_65(IEntityContext<Entity_65> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_65 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_65s
{
    readonly IQueryContext<Entity_65> _queryContext;

    public Entity_65s(IQueryContext<Entity_65> queryContext) => _queryContext = queryContext;

    public List<Entity_65> By0(string name_0) => _queryContext.All();
    public List<Entity_65> By1(string name_1) => _queryContext.All();
}

public class Entity_66
{
    readonly IEntityContext<Entity_66> _context = default!;

    protected Entity_66() { }

    public Entity_66(IEntityContext<Entity_66> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_66 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_66s
{
    readonly IQueryContext<Entity_66> _queryContext;

    public Entity_66s(IQueryContext<Entity_66> queryContext) => _queryContext = queryContext;

    public List<Entity_66> By0(string name_0) => _queryContext.All();
    public List<Entity_66> By1(string name_1) => _queryContext.All();
}

public class Entity_67
{
    readonly IEntityContext<Entity_67> _context = default!;

    protected Entity_67() { }

    public Entity_67(IEntityContext<Entity_67> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_67 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_67s
{
    readonly IQueryContext<Entity_67> _queryContext;

    public Entity_67s(IQueryContext<Entity_67> queryContext) => _queryContext = queryContext;

    public List<Entity_67> By0(string name_0) => _queryContext.All();
    public List<Entity_67> By1(string name_1) => _queryContext.All();
}

public class Entity_68
{
    readonly IEntityContext<Entity_68> _context = default!;

    protected Entity_68() { }

    public Entity_68(IEntityContext<Entity_68> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_68 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_68s
{
    readonly IQueryContext<Entity_68> _queryContext;

    public Entity_68s(IQueryContext<Entity_68> queryContext) => _queryContext = queryContext;

    public List<Entity_68> By0(string name_0) => _queryContext.All();
    public List<Entity_68> By1(string name_1) => _queryContext.All();
}

public class Entity_69
{
    readonly IEntityContext<Entity_69> _context = default!;

    protected Entity_69() { }

    public Entity_69(IEntityContext<Entity_69> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_69 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_69s
{
    readonly IQueryContext<Entity_69> _queryContext;

    public Entity_69s(IQueryContext<Entity_69> queryContext) => _queryContext = queryContext;

    public List<Entity_69> By0(string name_0) => _queryContext.All();
    public List<Entity_69> By1(string name_1) => _queryContext.All();
}

public class Entity_70
{
    readonly IEntityContext<Entity_70> _context = default!;

    protected Entity_70() { }

    public Entity_70(IEntityContext<Entity_70> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_70 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_70s
{
    readonly IQueryContext<Entity_70> _queryContext;

    public Entity_70s(IQueryContext<Entity_70> queryContext) => _queryContext = queryContext;

    public List<Entity_70> By0(string name_0) => _queryContext.All();
    public List<Entity_70> By1(string name_1) => _queryContext.All();
}

public class Entity_71
{
    readonly IEntityContext<Entity_71> _context = default!;

    protected Entity_71() { }

    public Entity_71(IEntityContext<Entity_71> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_71 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_71s
{
    readonly IQueryContext<Entity_71> _queryContext;

    public Entity_71s(IQueryContext<Entity_71> queryContext) => _queryContext = queryContext;

    public List<Entity_71> By0(string name_0) => _queryContext.All();
    public List<Entity_71> By1(string name_1) => _queryContext.All();
}

public class Entity_72
{
    readonly IEntityContext<Entity_72> _context = default!;

    protected Entity_72() { }

    public Entity_72(IEntityContext<Entity_72> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_72 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_72s
{
    readonly IQueryContext<Entity_72> _queryContext;

    public Entity_72s(IQueryContext<Entity_72> queryContext) => _queryContext = queryContext;

    public List<Entity_72> By0(string name_0) => _queryContext.All();
    public List<Entity_72> By1(string name_1) => _queryContext.All();
}

public class Entity_73
{
    readonly IEntityContext<Entity_73> _context = default!;

    protected Entity_73() { }

    public Entity_73(IEntityContext<Entity_73> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_73 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_73s
{
    readonly IQueryContext<Entity_73> _queryContext;

    public Entity_73s(IQueryContext<Entity_73> queryContext) => _queryContext = queryContext;

    public List<Entity_73> By0(string name_0) => _queryContext.All();
    public List<Entity_73> By1(string name_1) => _queryContext.All();
}

public class Entity_74
{
    readonly IEntityContext<Entity_74> _context = default!;

    protected Entity_74() { }

    public Entity_74(IEntityContext<Entity_74> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_74 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_74s
{
    readonly IQueryContext<Entity_74> _queryContext;

    public Entity_74s(IQueryContext<Entity_74> queryContext) => _queryContext = queryContext;

    public List<Entity_74> By0(string name_0) => _queryContext.All();
    public List<Entity_74> By1(string name_1) => _queryContext.All();
}

public class Entity_75
{
    readonly IEntityContext<Entity_75> _context = default!;

    protected Entity_75() { }

    public Entity_75(IEntityContext<Entity_75> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_75 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_75s
{
    readonly IQueryContext<Entity_75> _queryContext;

    public Entity_75s(IQueryContext<Entity_75> queryContext) => _queryContext = queryContext;

    public List<Entity_75> By0(string name_0) => _queryContext.All();
    public List<Entity_75> By1(string name_1) => _queryContext.All();
}

public class Entity_76
{
    readonly IEntityContext<Entity_76> _context = default!;

    protected Entity_76() { }

    public Entity_76(IEntityContext<Entity_76> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_76 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_76s
{
    readonly IQueryContext<Entity_76> _queryContext;

    public Entity_76s(IQueryContext<Entity_76> queryContext) => _queryContext = queryContext;

    public List<Entity_76> By0(string name_0) => _queryContext.All();
    public List<Entity_76> By1(string name_1) => _queryContext.All();
}

public class Entity_77
{
    readonly IEntityContext<Entity_77> _context = default!;

    protected Entity_77() { }

    public Entity_77(IEntityContext<Entity_77> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_77 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_77s
{
    readonly IQueryContext<Entity_77> _queryContext;

    public Entity_77s(IQueryContext<Entity_77> queryContext) => _queryContext = queryContext;

    public List<Entity_77> By0(string name_0) => _queryContext.All();
    public List<Entity_77> By1(string name_1) => _queryContext.All();
}

public class Entity_78
{
    readonly IEntityContext<Entity_78> _context = default!;

    protected Entity_78() { }

    public Entity_78(IEntityContext<Entity_78> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_78 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_78s
{
    readonly IQueryContext<Entity_78> _queryContext;

    public Entity_78s(IQueryContext<Entity_78> queryContext) => _queryContext = queryContext;

    public List<Entity_78> By0(string name_0) => _queryContext.All();
    public List<Entity_78> By1(string name_1) => _queryContext.All();
}

public class Entity_79
{
    readonly IEntityContext<Entity_79> _context = default!;

    protected Entity_79() { }

    public Entity_79(IEntityContext<Entity_79> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_79 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_79s
{
    readonly IQueryContext<Entity_79> _queryContext;

    public Entity_79s(IQueryContext<Entity_79> queryContext) => _queryContext = queryContext;

    public List<Entity_79> By0(string name_0) => _queryContext.All();
    public List<Entity_79> By1(string name_1) => _queryContext.All();
}

public class Entity_80
{
    readonly IEntityContext<Entity_80> _context = default!;

    protected Entity_80() { }

    public Entity_80(IEntityContext<Entity_80> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_80 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_80s
{
    readonly IQueryContext<Entity_80> _queryContext;

    public Entity_80s(IQueryContext<Entity_80> queryContext) => _queryContext = queryContext;

    public List<Entity_80> By0(string name_0) => _queryContext.All();
    public List<Entity_80> By1(string name_1) => _queryContext.All();
}

public class Entity_81
{
    readonly IEntityContext<Entity_81> _context = default!;

    protected Entity_81() { }

    public Entity_81(IEntityContext<Entity_81> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_81 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_81s
{
    readonly IQueryContext<Entity_81> _queryContext;

    public Entity_81s(IQueryContext<Entity_81> queryContext) => _queryContext = queryContext;

    public List<Entity_81> By0(string name_0) => _queryContext.All();
    public List<Entity_81> By1(string name_1) => _queryContext.All();
}

public class Entity_82
{
    readonly IEntityContext<Entity_82> _context = default!;

    protected Entity_82() { }

    public Entity_82(IEntityContext<Entity_82> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_82 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_82s
{
    readonly IQueryContext<Entity_82> _queryContext;

    public Entity_82s(IQueryContext<Entity_82> queryContext) => _queryContext = queryContext;

    public List<Entity_82> By0(string name_0) => _queryContext.All();
    public List<Entity_82> By1(string name_1) => _queryContext.All();
}

public class Entity_83
{
    readonly IEntityContext<Entity_83> _context = default!;

    protected Entity_83() { }

    public Entity_83(IEntityContext<Entity_83> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_83 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_83s
{
    readonly IQueryContext<Entity_83> _queryContext;

    public Entity_83s(IQueryContext<Entity_83> queryContext) => _queryContext = queryContext;

    public List<Entity_83> By0(string name_0) => _queryContext.All();
    public List<Entity_83> By1(string name_1) => _queryContext.All();
}

public class Entity_84
{
    readonly IEntityContext<Entity_84> _context = default!;

    protected Entity_84() { }

    public Entity_84(IEntityContext<Entity_84> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_84 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_84s
{
    readonly IQueryContext<Entity_84> _queryContext;

    public Entity_84s(IQueryContext<Entity_84> queryContext) => _queryContext = queryContext;

    public List<Entity_84> By0(string name_0) => _queryContext.All();
    public List<Entity_84> By1(string name_1) => _queryContext.All();
}

public class Entity_85
{
    readonly IEntityContext<Entity_85> _context = default!;

    protected Entity_85() { }

    public Entity_85(IEntityContext<Entity_85> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_85 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_85s
{
    readonly IQueryContext<Entity_85> _queryContext;

    public Entity_85s(IQueryContext<Entity_85> queryContext) => _queryContext = queryContext;

    public List<Entity_85> By0(string name_0) => _queryContext.All();
    public List<Entity_85> By1(string name_1) => _queryContext.All();
}

public class Entity_86
{
    readonly IEntityContext<Entity_86> _context = default!;

    protected Entity_86() { }

    public Entity_86(IEntityContext<Entity_86> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_86 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_86s
{
    readonly IQueryContext<Entity_86> _queryContext;

    public Entity_86s(IQueryContext<Entity_86> queryContext) => _queryContext = queryContext;

    public List<Entity_86> By0(string name_0) => _queryContext.All();
    public List<Entity_86> By1(string name_1) => _queryContext.All();
}

public class Entity_87
{
    readonly IEntityContext<Entity_87> _context = default!;

    protected Entity_87() { }

    public Entity_87(IEntityContext<Entity_87> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_87 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_87s
{
    readonly IQueryContext<Entity_87> _queryContext;

    public Entity_87s(IQueryContext<Entity_87> queryContext) => _queryContext = queryContext;

    public List<Entity_87> By0(string name_0) => _queryContext.All();
    public List<Entity_87> By1(string name_1) => _queryContext.All();
}

public class Entity_88
{
    readonly IEntityContext<Entity_88> _context = default!;

    protected Entity_88() { }

    public Entity_88(IEntityContext<Entity_88> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_88 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_88s
{
    readonly IQueryContext<Entity_88> _queryContext;

    public Entity_88s(IQueryContext<Entity_88> queryContext) => _queryContext = queryContext;

    public List<Entity_88> By0(string name_0) => _queryContext.All();
    public List<Entity_88> By1(string name_1) => _queryContext.All();
}

public class Entity_89
{
    readonly IEntityContext<Entity_89> _context = default!;

    protected Entity_89() { }

    public Entity_89(IEntityContext<Entity_89> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_89 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_89s
{
    readonly IQueryContext<Entity_89> _queryContext;

    public Entity_89s(IQueryContext<Entity_89> queryContext) => _queryContext = queryContext;

    public List<Entity_89> By0(string name_0) => _queryContext.All();
    public List<Entity_89> By1(string name_1) => _queryContext.All();
}

public class Entity_90
{
    readonly IEntityContext<Entity_90> _context = default!;

    protected Entity_90() { }

    public Entity_90(IEntityContext<Entity_90> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_90 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_90s
{
    readonly IQueryContext<Entity_90> _queryContext;

    public Entity_90s(IQueryContext<Entity_90> queryContext) => _queryContext = queryContext;

    public List<Entity_90> By0(string name_0) => _queryContext.All();
    public List<Entity_90> By1(string name_1) => _queryContext.All();
}

public class Entity_91
{
    readonly IEntityContext<Entity_91> _context = default!;

    protected Entity_91() { }

    public Entity_91(IEntityContext<Entity_91> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_91 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_91s
{
    readonly IQueryContext<Entity_91> _queryContext;

    public Entity_91s(IQueryContext<Entity_91> queryContext) => _queryContext = queryContext;

    public List<Entity_91> By0(string name_0) => _queryContext.All();
    public List<Entity_91> By1(string name_1) => _queryContext.All();
}

public class Entity_92
{
    readonly IEntityContext<Entity_92> _context = default!;

    protected Entity_92() { }

    public Entity_92(IEntityContext<Entity_92> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_92 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_92s
{
    readonly IQueryContext<Entity_92> _queryContext;

    public Entity_92s(IQueryContext<Entity_92> queryContext) => _queryContext = queryContext;

    public List<Entity_92> By0(string name_0) => _queryContext.All();
    public List<Entity_92> By1(string name_1) => _queryContext.All();
}

public class Entity_93
{
    readonly IEntityContext<Entity_93> _context = default!;

    protected Entity_93() { }

    public Entity_93(IEntityContext<Entity_93> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_93 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_93s
{
    readonly IQueryContext<Entity_93> _queryContext;

    public Entity_93s(IQueryContext<Entity_93> queryContext) => _queryContext = queryContext;

    public List<Entity_93> By0(string name_0) => _queryContext.All();
    public List<Entity_93> By1(string name_1) => _queryContext.All();
}

public class Entity_94
{
    readonly IEntityContext<Entity_94> _context = default!;

    protected Entity_94() { }

    public Entity_94(IEntityContext<Entity_94> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_94 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_94s
{
    readonly IQueryContext<Entity_94> _queryContext;

    public Entity_94s(IQueryContext<Entity_94> queryContext) => _queryContext = queryContext;

    public List<Entity_94> By0(string name_0) => _queryContext.All();
    public List<Entity_94> By1(string name_1) => _queryContext.All();
}

public class Entity_95
{
    readonly IEntityContext<Entity_95> _context = default!;

    protected Entity_95() { }

    public Entity_95(IEntityContext<Entity_95> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_95 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_95s
{
    readonly IQueryContext<Entity_95> _queryContext;

    public Entity_95s(IQueryContext<Entity_95> queryContext) => _queryContext = queryContext;

    public List<Entity_95> By0(string name_0) => _queryContext.All();
    public List<Entity_95> By1(string name_1) => _queryContext.All();
}

public class Entity_96
{
    readonly IEntityContext<Entity_96> _context = default!;

    protected Entity_96() { }

    public Entity_96(IEntityContext<Entity_96> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_96 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_96s
{
    readonly IQueryContext<Entity_96> _queryContext;

    public Entity_96s(IQueryContext<Entity_96> queryContext) => _queryContext = queryContext;

    public List<Entity_96> By0(string name_0) => _queryContext.All();
    public List<Entity_96> By1(string name_1) => _queryContext.All();
}

public class Entity_97
{
    readonly IEntityContext<Entity_97> _context = default!;

    protected Entity_97() { }

    public Entity_97(IEntityContext<Entity_97> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_97 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_97s
{
    readonly IQueryContext<Entity_97> _queryContext;

    public Entity_97s(IQueryContext<Entity_97> queryContext) => _queryContext = queryContext;

    public List<Entity_97> By0(string name_0) => _queryContext.All();
    public List<Entity_97> By1(string name_1) => _queryContext.All();
}

public class Entity_98
{
    readonly IEntityContext<Entity_98> _context = default!;

    protected Entity_98() { }

    public Entity_98(IEntityContext<Entity_98> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_98 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_98s
{
    readonly IQueryContext<Entity_98> _queryContext;

    public Entity_98s(IQueryContext<Entity_98> queryContext) => _queryContext = queryContext;

    public List<Entity_98> By0(string name_0) => _queryContext.All();
    public List<Entity_98> By1(string name_1) => _queryContext.All();
}

public class Entity_99
{
    readonly IEntityContext<Entity_99> _context = default!;

    protected Entity_99() { }

    public Entity_99(IEntityContext<Entity_99> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_99 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_99s
{
    readonly IQueryContext<Entity_99> _queryContext;

    public Entity_99s(IQueryContext<Entity_99> queryContext) => _queryContext = queryContext;

    public List<Entity_99> By0(string name_0) => _queryContext.All();
    public List<Entity_99> By1(string name_1) => _queryContext.All();
}

public class Entity_100
{
    readonly IEntityContext<Entity_100> _context = default!;

    protected Entity_100() { }

    public Entity_100(IEntityContext<Entity_100> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_100 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_100s
{
    readonly IQueryContext<Entity_100> _queryContext;

    public Entity_100s(IQueryContext<Entity_100> queryContext) => _queryContext = queryContext;

    public List<Entity_100> By0(string name_0) => _queryContext.All();
    public List<Entity_100> By1(string name_1) => _queryContext.All();
}

public class Entity_101
{
    readonly IEntityContext<Entity_101> _context = default!;

    protected Entity_101() { }

    public Entity_101(IEntityContext<Entity_101> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_101 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_101s
{
    readonly IQueryContext<Entity_101> _queryContext;

    public Entity_101s(IQueryContext<Entity_101> queryContext) => _queryContext = queryContext;

    public List<Entity_101> By0(string name_0) => _queryContext.All();
    public List<Entity_101> By1(string name_1) => _queryContext.All();
}

public class Entity_102
{
    readonly IEntityContext<Entity_102> _context = default!;

    protected Entity_102() { }

    public Entity_102(IEntityContext<Entity_102> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_102 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_102s
{
    readonly IQueryContext<Entity_102> _queryContext;

    public Entity_102s(IQueryContext<Entity_102> queryContext) => _queryContext = queryContext;

    public List<Entity_102> By0(string name_0) => _queryContext.All();
    public List<Entity_102> By1(string name_1) => _queryContext.All();
}

public class Entity_103
{
    readonly IEntityContext<Entity_103> _context = default!;

    protected Entity_103() { }

    public Entity_103(IEntityContext<Entity_103> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_103 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_103s
{
    readonly IQueryContext<Entity_103> _queryContext;

    public Entity_103s(IQueryContext<Entity_103> queryContext) => _queryContext = queryContext;

    public List<Entity_103> By0(string name_0) => _queryContext.All();
    public List<Entity_103> By1(string name_1) => _queryContext.All();
}

public class Entity_104
{
    readonly IEntityContext<Entity_104> _context = default!;

    protected Entity_104() { }

    public Entity_104(IEntityContext<Entity_104> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_104 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_104s
{
    readonly IQueryContext<Entity_104> _queryContext;

    public Entity_104s(IQueryContext<Entity_104> queryContext) => _queryContext = queryContext;

    public List<Entity_104> By0(string name_0) => _queryContext.All();
    public List<Entity_104> By1(string name_1) => _queryContext.All();
}

public class Entity_105
{
    readonly IEntityContext<Entity_105> _context = default!;

    protected Entity_105() { }

    public Entity_105(IEntityContext<Entity_105> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_105 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_105s
{
    readonly IQueryContext<Entity_105> _queryContext;

    public Entity_105s(IQueryContext<Entity_105> queryContext) => _queryContext = queryContext;

    public List<Entity_105> By0(string name_0) => _queryContext.All();
    public List<Entity_105> By1(string name_1) => _queryContext.All();
}

public class Entity_106
{
    readonly IEntityContext<Entity_106> _context = default!;

    protected Entity_106() { }

    public Entity_106(IEntityContext<Entity_106> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_106 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_106s
{
    readonly IQueryContext<Entity_106> _queryContext;

    public Entity_106s(IQueryContext<Entity_106> queryContext) => _queryContext = queryContext;

    public List<Entity_106> By0(string name_0) => _queryContext.All();
    public List<Entity_106> By1(string name_1) => _queryContext.All();
}

public class Entity_107
{
    readonly IEntityContext<Entity_107> _context = default!;

    protected Entity_107() { }

    public Entity_107(IEntityContext<Entity_107> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_107 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_107s
{
    readonly IQueryContext<Entity_107> _queryContext;

    public Entity_107s(IQueryContext<Entity_107> queryContext) => _queryContext = queryContext;

    public List<Entity_107> By0(string name_0) => _queryContext.All();
    public List<Entity_107> By1(string name_1) => _queryContext.All();
}

public class Entity_108
{
    readonly IEntityContext<Entity_108> _context = default!;

    protected Entity_108() { }

    public Entity_108(IEntityContext<Entity_108> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_108 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_108s
{
    readonly IQueryContext<Entity_108> _queryContext;

    public Entity_108s(IQueryContext<Entity_108> queryContext) => _queryContext = queryContext;

    public List<Entity_108> By0(string name_0) => _queryContext.All();
    public List<Entity_108> By1(string name_1) => _queryContext.All();
}

public class Entity_109
{
    readonly IEntityContext<Entity_109> _context = default!;

    protected Entity_109() { }

    public Entity_109(IEntityContext<Entity_109> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_109 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_109s
{
    readonly IQueryContext<Entity_109> _queryContext;

    public Entity_109s(IQueryContext<Entity_109> queryContext) => _queryContext = queryContext;

    public List<Entity_109> By0(string name_0) => _queryContext.All();
    public List<Entity_109> By1(string name_1) => _queryContext.All();
}

public class Entity_110
{
    readonly IEntityContext<Entity_110> _context = default!;

    protected Entity_110() { }

    public Entity_110(IEntityContext<Entity_110> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_110 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_110s
{
    readonly IQueryContext<Entity_110> _queryContext;

    public Entity_110s(IQueryContext<Entity_110> queryContext) => _queryContext = queryContext;

    public List<Entity_110> By0(string name_0) => _queryContext.All();
    public List<Entity_110> By1(string name_1) => _queryContext.All();
}

public class Entity_111
{
    readonly IEntityContext<Entity_111> _context = default!;

    protected Entity_111() { }

    public Entity_111(IEntityContext<Entity_111> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_111 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_111s
{
    readonly IQueryContext<Entity_111> _queryContext;

    public Entity_111s(IQueryContext<Entity_111> queryContext) => _queryContext = queryContext;

    public List<Entity_111> By0(string name_0) => _queryContext.All();
    public List<Entity_111> By1(string name_1) => _queryContext.All();
}

public class Entity_112
{
    readonly IEntityContext<Entity_112> _context = default!;

    protected Entity_112() { }

    public Entity_112(IEntityContext<Entity_112> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_112 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_112s
{
    readonly IQueryContext<Entity_112> _queryContext;

    public Entity_112s(IQueryContext<Entity_112> queryContext) => _queryContext = queryContext;

    public List<Entity_112> By0(string name_0) => _queryContext.All();
    public List<Entity_112> By1(string name_1) => _queryContext.All();
}

public class Entity_113
{
    readonly IEntityContext<Entity_113> _context = default!;

    protected Entity_113() { }

    public Entity_113(IEntityContext<Entity_113> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_113 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_113s
{
    readonly IQueryContext<Entity_113> _queryContext;

    public Entity_113s(IQueryContext<Entity_113> queryContext) => _queryContext = queryContext;

    public List<Entity_113> By0(string name_0) => _queryContext.All();
    public List<Entity_113> By1(string name_1) => _queryContext.All();
}

public class Entity_114
{
    readonly IEntityContext<Entity_114> _context = default!;

    protected Entity_114() { }

    public Entity_114(IEntityContext<Entity_114> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_114 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_114s
{
    readonly IQueryContext<Entity_114> _queryContext;

    public Entity_114s(IQueryContext<Entity_114> queryContext) => _queryContext = queryContext;

    public List<Entity_114> By0(string name_0) => _queryContext.All();
    public List<Entity_114> By1(string name_1) => _queryContext.All();
}

public class Entity_115
{
    readonly IEntityContext<Entity_115> _context = default!;

    protected Entity_115() { }

    public Entity_115(IEntityContext<Entity_115> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_115 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_115s
{
    readonly IQueryContext<Entity_115> _queryContext;

    public Entity_115s(IQueryContext<Entity_115> queryContext) => _queryContext = queryContext;

    public List<Entity_115> By0(string name_0) => _queryContext.All();
    public List<Entity_115> By1(string name_1) => _queryContext.All();
}

public class Entity_116
{
    readonly IEntityContext<Entity_116> _context = default!;

    protected Entity_116() { }

    public Entity_116(IEntityContext<Entity_116> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_116 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_116s
{
    readonly IQueryContext<Entity_116> _queryContext;

    public Entity_116s(IQueryContext<Entity_116> queryContext) => _queryContext = queryContext;

    public List<Entity_116> By0(string name_0) => _queryContext.All();
    public List<Entity_116> By1(string name_1) => _queryContext.All();
}

public class Entity_117
{
    readonly IEntityContext<Entity_117> _context = default!;

    protected Entity_117() { }

    public Entity_117(IEntityContext<Entity_117> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_117 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_117s
{
    readonly IQueryContext<Entity_117> _queryContext;

    public Entity_117s(IQueryContext<Entity_117> queryContext) => _queryContext = queryContext;

    public List<Entity_117> By0(string name_0) => _queryContext.All();
    public List<Entity_117> By1(string name_1) => _queryContext.All();
}

public class Entity_118
{
    readonly IEntityContext<Entity_118> _context = default!;

    protected Entity_118() { }

    public Entity_118(IEntityContext<Entity_118> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_118 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_118s
{
    readonly IQueryContext<Entity_118> _queryContext;

    public Entity_118s(IQueryContext<Entity_118> queryContext) => _queryContext = queryContext;

    public List<Entity_118> By0(string name_0) => _queryContext.All();
    public List<Entity_118> By1(string name_1) => _queryContext.All();
}

public class Entity_119
{
    readonly IEntityContext<Entity_119> _context = default!;

    protected Entity_119() { }

    public Entity_119(IEntityContext<Entity_119> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_119 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_119s
{
    readonly IQueryContext<Entity_119> _queryContext;

    public Entity_119s(IQueryContext<Entity_119> queryContext) => _queryContext = queryContext;

    public List<Entity_119> By0(string name_0) => _queryContext.All();
    public List<Entity_119> By1(string name_1) => _queryContext.All();
}

public class Entity_120
{
    readonly IEntityContext<Entity_120> _context = default!;

    protected Entity_120() { }

    public Entity_120(IEntityContext<Entity_120> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_120 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_120s
{
    readonly IQueryContext<Entity_120> _queryContext;

    public Entity_120s(IQueryContext<Entity_120> queryContext) => _queryContext = queryContext;

    public List<Entity_120> By0(string name_0) => _queryContext.All();
    public List<Entity_120> By1(string name_1) => _queryContext.All();
}

public class Entity_121
{
    readonly IEntityContext<Entity_121> _context = default!;

    protected Entity_121() { }

    public Entity_121(IEntityContext<Entity_121> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_121 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_121s
{
    readonly IQueryContext<Entity_121> _queryContext;

    public Entity_121s(IQueryContext<Entity_121> queryContext) => _queryContext = queryContext;

    public List<Entity_121> By0(string name_0) => _queryContext.All();
    public List<Entity_121> By1(string name_1) => _queryContext.All();
}

public class Entity_122
{
    readonly IEntityContext<Entity_122> _context = default!;

    protected Entity_122() { }

    public Entity_122(IEntityContext<Entity_122> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_122 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_122s
{
    readonly IQueryContext<Entity_122> _queryContext;

    public Entity_122s(IQueryContext<Entity_122> queryContext) => _queryContext = queryContext;

    public List<Entity_122> By0(string name_0) => _queryContext.All();
    public List<Entity_122> By1(string name_1) => _queryContext.All();
}

public class Entity_123
{
    readonly IEntityContext<Entity_123> _context = default!;

    protected Entity_123() { }

    public Entity_123(IEntityContext<Entity_123> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_123 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_123s
{
    readonly IQueryContext<Entity_123> _queryContext;

    public Entity_123s(IQueryContext<Entity_123> queryContext) => _queryContext = queryContext;

    public List<Entity_123> By0(string name_0) => _queryContext.All();
    public List<Entity_123> By1(string name_1) => _queryContext.All();
}

public class Entity_124
{
    readonly IEntityContext<Entity_124> _context = default!;

    protected Entity_124() { }

    public Entity_124(IEntityContext<Entity_124> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_124 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_124s
{
    readonly IQueryContext<Entity_124> _queryContext;

    public Entity_124s(IQueryContext<Entity_124> queryContext) => _queryContext = queryContext;

    public List<Entity_124> By0(string name_0) => _queryContext.All();
    public List<Entity_124> By1(string name_1) => _queryContext.All();
}

public class Entity_125
{
    readonly IEntityContext<Entity_125> _context = default!;

    protected Entity_125() { }

    public Entity_125(IEntityContext<Entity_125> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_125 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_125s
{
    readonly IQueryContext<Entity_125> _queryContext;

    public Entity_125s(IQueryContext<Entity_125> queryContext) => _queryContext = queryContext;

    public List<Entity_125> By0(string name_0) => _queryContext.All();
    public List<Entity_125> By1(string name_1) => _queryContext.All();
}

public class Entity_126
{
    readonly IEntityContext<Entity_126> _context = default!;

    protected Entity_126() { }

    public Entity_126(IEntityContext<Entity_126> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_126 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_126s
{
    readonly IQueryContext<Entity_126> _queryContext;

    public Entity_126s(IQueryContext<Entity_126> queryContext) => _queryContext = queryContext;

    public List<Entity_126> By0(string name_0) => _queryContext.All();
    public List<Entity_126> By1(string name_1) => _queryContext.All();
}

public class Entity_127
{
    readonly IEntityContext<Entity_127> _context = default!;

    protected Entity_127() { }

    public Entity_127(IEntityContext<Entity_127> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_127 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_127s
{
    readonly IQueryContext<Entity_127> _queryContext;

    public Entity_127s(IQueryContext<Entity_127> queryContext) => _queryContext = queryContext;

    public List<Entity_127> By0(string name_0) => _queryContext.All();
    public List<Entity_127> By1(string name_1) => _queryContext.All();
}

public class Entity_128
{
    readonly IEntityContext<Entity_128> _context = default!;

    protected Entity_128() { }

    public Entity_128(IEntityContext<Entity_128> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_128 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_128s
{
    readonly IQueryContext<Entity_128> _queryContext;

    public Entity_128s(IQueryContext<Entity_128> queryContext) => _queryContext = queryContext;

    public List<Entity_128> By0(string name_0) => _queryContext.All();
    public List<Entity_128> By1(string name_1) => _queryContext.All();
}

public class Entity_129
{
    readonly IEntityContext<Entity_129> _context = default!;

    protected Entity_129() { }

    public Entity_129(IEntityContext<Entity_129> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_129 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_129s
{
    readonly IQueryContext<Entity_129> _queryContext;

    public Entity_129s(IQueryContext<Entity_129> queryContext) => _queryContext = queryContext;

    public List<Entity_129> By0(string name_0) => _queryContext.All();
    public List<Entity_129> By1(string name_1) => _queryContext.All();
}

public class Entity_130
{
    readonly IEntityContext<Entity_130> _context = default!;

    protected Entity_130() { }

    public Entity_130(IEntityContext<Entity_130> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_130 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_130s
{
    readonly IQueryContext<Entity_130> _queryContext;

    public Entity_130s(IQueryContext<Entity_130> queryContext) => _queryContext = queryContext;

    public List<Entity_130> By0(string name_0) => _queryContext.All();
    public List<Entity_130> By1(string name_1) => _queryContext.All();
}

public class Entity_131
{
    readonly IEntityContext<Entity_131> _context = default!;

    protected Entity_131() { }

    public Entity_131(IEntityContext<Entity_131> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_131 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_131s
{
    readonly IQueryContext<Entity_131> _queryContext;

    public Entity_131s(IQueryContext<Entity_131> queryContext) => _queryContext = queryContext;

    public List<Entity_131> By0(string name_0) => _queryContext.All();
    public List<Entity_131> By1(string name_1) => _queryContext.All();
}

public class Entity_132
{
    readonly IEntityContext<Entity_132> _context = default!;

    protected Entity_132() { }

    public Entity_132(IEntityContext<Entity_132> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_132 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_132s
{
    readonly IQueryContext<Entity_132> _queryContext;

    public Entity_132s(IQueryContext<Entity_132> queryContext) => _queryContext = queryContext;

    public List<Entity_132> By0(string name_0) => _queryContext.All();
    public List<Entity_132> By1(string name_1) => _queryContext.All();
}

public class Entity_133
{
    readonly IEntityContext<Entity_133> _context = default!;

    protected Entity_133() { }

    public Entity_133(IEntityContext<Entity_133> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_133 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_133s
{
    readonly IQueryContext<Entity_133> _queryContext;

    public Entity_133s(IQueryContext<Entity_133> queryContext) => _queryContext = queryContext;

    public List<Entity_133> By0(string name_0) => _queryContext.All();
    public List<Entity_133> By1(string name_1) => _queryContext.All();
}

public class Entity_134
{
    readonly IEntityContext<Entity_134> _context = default!;

    protected Entity_134() { }

    public Entity_134(IEntityContext<Entity_134> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_134 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_134s
{
    readonly IQueryContext<Entity_134> _queryContext;

    public Entity_134s(IQueryContext<Entity_134> queryContext) => _queryContext = queryContext;

    public List<Entity_134> By0(string name_0) => _queryContext.All();
    public List<Entity_134> By1(string name_1) => _queryContext.All();
}

public class Entity_135
{
    readonly IEntityContext<Entity_135> _context = default!;

    protected Entity_135() { }

    public Entity_135(IEntityContext<Entity_135> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_135 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_135s
{
    readonly IQueryContext<Entity_135> _queryContext;

    public Entity_135s(IQueryContext<Entity_135> queryContext) => _queryContext = queryContext;

    public List<Entity_135> By0(string name_0) => _queryContext.All();
    public List<Entity_135> By1(string name_1) => _queryContext.All();
}

public class Entity_136
{
    readonly IEntityContext<Entity_136> _context = default!;

    protected Entity_136() { }

    public Entity_136(IEntityContext<Entity_136> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_136 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_136s
{
    readonly IQueryContext<Entity_136> _queryContext;

    public Entity_136s(IQueryContext<Entity_136> queryContext) => _queryContext = queryContext;

    public List<Entity_136> By0(string name_0) => _queryContext.All();
    public List<Entity_136> By1(string name_1) => _queryContext.All();
}

public class Entity_137
{
    readonly IEntityContext<Entity_137> _context = default!;

    protected Entity_137() { }

    public Entity_137(IEntityContext<Entity_137> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_137 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_137s
{
    readonly IQueryContext<Entity_137> _queryContext;

    public Entity_137s(IQueryContext<Entity_137> queryContext) => _queryContext = queryContext;

    public List<Entity_137> By0(string name_0) => _queryContext.All();
    public List<Entity_137> By1(string name_1) => _queryContext.All();
}

public class Entity_138
{
    readonly IEntityContext<Entity_138> _context = default!;

    protected Entity_138() { }

    public Entity_138(IEntityContext<Entity_138> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_138 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_138s
{
    readonly IQueryContext<Entity_138> _queryContext;

    public Entity_138s(IQueryContext<Entity_138> queryContext) => _queryContext = queryContext;

    public List<Entity_138> By0(string name_0) => _queryContext.All();
    public List<Entity_138> By1(string name_1) => _queryContext.All();
}

public class Entity_139
{
    readonly IEntityContext<Entity_139> _context = default!;

    protected Entity_139() { }

    public Entity_139(IEntityContext<Entity_139> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_139 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_139s
{
    readonly IQueryContext<Entity_139> _queryContext;

    public Entity_139s(IQueryContext<Entity_139> queryContext) => _queryContext = queryContext;

    public List<Entity_139> By0(string name_0) => _queryContext.All();
    public List<Entity_139> By1(string name_1) => _queryContext.All();
}

public class Entity_140
{
    readonly IEntityContext<Entity_140> _context = default!;

    protected Entity_140() { }

    public Entity_140(IEntityContext<Entity_140> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_140 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_140s
{
    readonly IQueryContext<Entity_140> _queryContext;

    public Entity_140s(IQueryContext<Entity_140> queryContext) => _queryContext = queryContext;

    public List<Entity_140> By0(string name_0) => _queryContext.All();
    public List<Entity_140> By1(string name_1) => _queryContext.All();
}

public class Entity_141
{
    readonly IEntityContext<Entity_141> _context = default!;

    protected Entity_141() { }

    public Entity_141(IEntityContext<Entity_141> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_141 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_141s
{
    readonly IQueryContext<Entity_141> _queryContext;

    public Entity_141s(IQueryContext<Entity_141> queryContext) => _queryContext = queryContext;

    public List<Entity_141> By0(string name_0) => _queryContext.All();
    public List<Entity_141> By1(string name_1) => _queryContext.All();
}

public class Entity_142
{
    readonly IEntityContext<Entity_142> _context = default!;

    protected Entity_142() { }

    public Entity_142(IEntityContext<Entity_142> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_142 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_142s
{
    readonly IQueryContext<Entity_142> _queryContext;

    public Entity_142s(IQueryContext<Entity_142> queryContext) => _queryContext = queryContext;

    public List<Entity_142> By0(string name_0) => _queryContext.All();
    public List<Entity_142> By1(string name_1) => _queryContext.All();
}

public class Entity_143
{
    readonly IEntityContext<Entity_143> _context = default!;

    protected Entity_143() { }

    public Entity_143(IEntityContext<Entity_143> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_143 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_143s
{
    readonly IQueryContext<Entity_143> _queryContext;

    public Entity_143s(IQueryContext<Entity_143> queryContext) => _queryContext = queryContext;

    public List<Entity_143> By0(string name_0) => _queryContext.All();
    public List<Entity_143> By1(string name_1) => _queryContext.All();
}

public class Entity_144
{
    readonly IEntityContext<Entity_144> _context = default!;

    protected Entity_144() { }

    public Entity_144(IEntityContext<Entity_144> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_144 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_144s
{
    readonly IQueryContext<Entity_144> _queryContext;

    public Entity_144s(IQueryContext<Entity_144> queryContext) => _queryContext = queryContext;

    public List<Entity_144> By0(string name_0) => _queryContext.All();
    public List<Entity_144> By1(string name_1) => _queryContext.All();
}

public class Entity_145
{
    readonly IEntityContext<Entity_145> _context = default!;

    protected Entity_145() { }

    public Entity_145(IEntityContext<Entity_145> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_145 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_145s
{
    readonly IQueryContext<Entity_145> _queryContext;

    public Entity_145s(IQueryContext<Entity_145> queryContext) => _queryContext = queryContext;

    public List<Entity_145> By0(string name_0) => _queryContext.All();
    public List<Entity_145> By1(string name_1) => _queryContext.All();
}

public class Entity_146
{
    readonly IEntityContext<Entity_146> _context = default!;

    protected Entity_146() { }

    public Entity_146(IEntityContext<Entity_146> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_146 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_146s
{
    readonly IQueryContext<Entity_146> _queryContext;

    public Entity_146s(IQueryContext<Entity_146> queryContext) => _queryContext = queryContext;

    public List<Entity_146> By0(string name_0) => _queryContext.All();
    public List<Entity_146> By1(string name_1) => _queryContext.All();
}

public class Entity_147
{
    readonly IEntityContext<Entity_147> _context = default!;

    protected Entity_147() { }

    public Entity_147(IEntityContext<Entity_147> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_147 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_147s
{
    readonly IQueryContext<Entity_147> _queryContext;

    public Entity_147s(IQueryContext<Entity_147> queryContext) => _queryContext = queryContext;

    public List<Entity_147> By0(string name_0) => _queryContext.All();
    public List<Entity_147> By1(string name_1) => _queryContext.All();
}

public class Entity_148
{
    readonly IEntityContext<Entity_148> _context = default!;

    protected Entity_148() { }

    public Entity_148(IEntityContext<Entity_148> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_148 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_148s
{
    readonly IQueryContext<Entity_148> _queryContext;

    public Entity_148s(IQueryContext<Entity_148> queryContext) => _queryContext = queryContext;

    public List<Entity_148> By0(string name_0) => _queryContext.All();
    public List<Entity_148> By1(string name_1) => _queryContext.All();
}

public class Entity_149
{
    readonly IEntityContext<Entity_149> _context = default!;

    protected Entity_149() { }

    public Entity_149(IEntityContext<Entity_149> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_149 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_149s
{
    readonly IQueryContext<Entity_149> _queryContext;

    public Entity_149s(IQueryContext<Entity_149> queryContext) => _queryContext = queryContext;

    public List<Entity_149> By0(string name_0) => _queryContext.All();
    public List<Entity_149> By1(string name_1) => _queryContext.All();
}

public class Entity_150
{
    readonly IEntityContext<Entity_150> _context = default!;

    protected Entity_150() { }

    public Entity_150(IEntityContext<Entity_150> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_150 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_150s
{
    readonly IQueryContext<Entity_150> _queryContext;

    public Entity_150s(IQueryContext<Entity_150> queryContext) => _queryContext = queryContext;

    public List<Entity_150> By0(string name_0) => _queryContext.All();
    public List<Entity_150> By1(string name_1) => _queryContext.All();
}

public class Entity_151
{
    readonly IEntityContext<Entity_151> _context = default!;

    protected Entity_151() { }

    public Entity_151(IEntityContext<Entity_151> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_151 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_151s
{
    readonly IQueryContext<Entity_151> _queryContext;

    public Entity_151s(IQueryContext<Entity_151> queryContext) => _queryContext = queryContext;

    public List<Entity_151> By0(string name_0) => _queryContext.All();
    public List<Entity_151> By1(string name_1) => _queryContext.All();
}

public class Entity_152
{
    readonly IEntityContext<Entity_152> _context = default!;

    protected Entity_152() { }

    public Entity_152(IEntityContext<Entity_152> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_152 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_152s
{
    readonly IQueryContext<Entity_152> _queryContext;

    public Entity_152s(IQueryContext<Entity_152> queryContext) => _queryContext = queryContext;

    public List<Entity_152> By0(string name_0) => _queryContext.All();
    public List<Entity_152> By1(string name_1) => _queryContext.All();
}

public class Entity_153
{
    readonly IEntityContext<Entity_153> _context = default!;

    protected Entity_153() { }

    public Entity_153(IEntityContext<Entity_153> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_153 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_153s
{
    readonly IQueryContext<Entity_153> _queryContext;

    public Entity_153s(IQueryContext<Entity_153> queryContext) => _queryContext = queryContext;

    public List<Entity_153> By0(string name_0) => _queryContext.All();
    public List<Entity_153> By1(string name_1) => _queryContext.All();
}

public class Entity_154
{
    readonly IEntityContext<Entity_154> _context = default!;

    protected Entity_154() { }

    public Entity_154(IEntityContext<Entity_154> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_154 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_154s
{
    readonly IQueryContext<Entity_154> _queryContext;

    public Entity_154s(IQueryContext<Entity_154> queryContext) => _queryContext = queryContext;

    public List<Entity_154> By0(string name_0) => _queryContext.All();
    public List<Entity_154> By1(string name_1) => _queryContext.All();
}

public class Entity_155
{
    readonly IEntityContext<Entity_155> _context = default!;

    protected Entity_155() { }

    public Entity_155(IEntityContext<Entity_155> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_155 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_155s
{
    readonly IQueryContext<Entity_155> _queryContext;

    public Entity_155s(IQueryContext<Entity_155> queryContext) => _queryContext = queryContext;

    public List<Entity_155> By0(string name_0) => _queryContext.All();
    public List<Entity_155> By1(string name_1) => _queryContext.All();
}

public class Entity_156
{
    readonly IEntityContext<Entity_156> _context = default!;

    protected Entity_156() { }

    public Entity_156(IEntityContext<Entity_156> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_156 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_156s
{
    readonly IQueryContext<Entity_156> _queryContext;

    public Entity_156s(IQueryContext<Entity_156> queryContext) => _queryContext = queryContext;

    public List<Entity_156> By0(string name_0) => _queryContext.All();
    public List<Entity_156> By1(string name_1) => _queryContext.All();
}

public class Entity_157
{
    readonly IEntityContext<Entity_157> _context = default!;

    protected Entity_157() { }

    public Entity_157(IEntityContext<Entity_157> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_157 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_157s
{
    readonly IQueryContext<Entity_157> _queryContext;

    public Entity_157s(IQueryContext<Entity_157> queryContext) => _queryContext = queryContext;

    public List<Entity_157> By0(string name_0) => _queryContext.All();
    public List<Entity_157> By1(string name_1) => _queryContext.All();
}

public class Entity_158
{
    readonly IEntityContext<Entity_158> _context = default!;

    protected Entity_158() { }

    public Entity_158(IEntityContext<Entity_158> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_158 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_158s
{
    readonly IQueryContext<Entity_158> _queryContext;

    public Entity_158s(IQueryContext<Entity_158> queryContext) => _queryContext = queryContext;

    public List<Entity_158> By0(string name_0) => _queryContext.All();
    public List<Entity_158> By1(string name_1) => _queryContext.All();
}

public class Entity_159
{
    readonly IEntityContext<Entity_159> _context = default!;

    protected Entity_159() { }

    public Entity_159(IEntityContext<Entity_159> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_159 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_159s
{
    readonly IQueryContext<Entity_159> _queryContext;

    public Entity_159s(IQueryContext<Entity_159> queryContext) => _queryContext = queryContext;

    public List<Entity_159> By0(string name_0) => _queryContext.All();
    public List<Entity_159> By1(string name_1) => _queryContext.All();
}

public class Entity_160
{
    readonly IEntityContext<Entity_160> _context = default!;

    protected Entity_160() { }

    public Entity_160(IEntityContext<Entity_160> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_160 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_160s
{
    readonly IQueryContext<Entity_160> _queryContext;

    public Entity_160s(IQueryContext<Entity_160> queryContext) => _queryContext = queryContext;

    public List<Entity_160> By0(string name_0) => _queryContext.All();
    public List<Entity_160> By1(string name_1) => _queryContext.All();
}

public class Entity_161
{
    readonly IEntityContext<Entity_161> _context = default!;

    protected Entity_161() { }

    public Entity_161(IEntityContext<Entity_161> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_161 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_161s
{
    readonly IQueryContext<Entity_161> _queryContext;

    public Entity_161s(IQueryContext<Entity_161> queryContext) => _queryContext = queryContext;

    public List<Entity_161> By0(string name_0) => _queryContext.All();
    public List<Entity_161> By1(string name_1) => _queryContext.All();
}

public class Entity_162
{
    readonly IEntityContext<Entity_162> _context = default!;

    protected Entity_162() { }

    public Entity_162(IEntityContext<Entity_162> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_162 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_162s
{
    readonly IQueryContext<Entity_162> _queryContext;

    public Entity_162s(IQueryContext<Entity_162> queryContext) => _queryContext = queryContext;

    public List<Entity_162> By0(string name_0) => _queryContext.All();
    public List<Entity_162> By1(string name_1) => _queryContext.All();
}

public class Entity_163
{
    readonly IEntityContext<Entity_163> _context = default!;

    protected Entity_163() { }

    public Entity_163(IEntityContext<Entity_163> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_163 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_163s
{
    readonly IQueryContext<Entity_163> _queryContext;

    public Entity_163s(IQueryContext<Entity_163> queryContext) => _queryContext = queryContext;

    public List<Entity_163> By0(string name_0) => _queryContext.All();
    public List<Entity_163> By1(string name_1) => _queryContext.All();
}

public class Entity_164
{
    readonly IEntityContext<Entity_164> _context = default!;

    protected Entity_164() { }

    public Entity_164(IEntityContext<Entity_164> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_164 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_164s
{
    readonly IQueryContext<Entity_164> _queryContext;

    public Entity_164s(IQueryContext<Entity_164> queryContext) => _queryContext = queryContext;

    public List<Entity_164> By0(string name_0) => _queryContext.All();
    public List<Entity_164> By1(string name_1) => _queryContext.All();
}

public class Entity_165
{
    readonly IEntityContext<Entity_165> _context = default!;

    protected Entity_165() { }

    public Entity_165(IEntityContext<Entity_165> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_165 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_165s
{
    readonly IQueryContext<Entity_165> _queryContext;

    public Entity_165s(IQueryContext<Entity_165> queryContext) => _queryContext = queryContext;

    public List<Entity_165> By0(string name_0) => _queryContext.All();
    public List<Entity_165> By1(string name_1) => _queryContext.All();
}

public class Entity_166
{
    readonly IEntityContext<Entity_166> _context = default!;

    protected Entity_166() { }

    public Entity_166(IEntityContext<Entity_166> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_166 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_166s
{
    readonly IQueryContext<Entity_166> _queryContext;

    public Entity_166s(IQueryContext<Entity_166> queryContext) => _queryContext = queryContext;

    public List<Entity_166> By0(string name_0) => _queryContext.All();
    public List<Entity_166> By1(string name_1) => _queryContext.All();
}

public class Entity_167
{
    readonly IEntityContext<Entity_167> _context = default!;

    protected Entity_167() { }

    public Entity_167(IEntityContext<Entity_167> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_167 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_167s
{
    readonly IQueryContext<Entity_167> _queryContext;

    public Entity_167s(IQueryContext<Entity_167> queryContext) => _queryContext = queryContext;

    public List<Entity_167> By0(string name_0) => _queryContext.All();
    public List<Entity_167> By1(string name_1) => _queryContext.All();
}

public class Entity_168
{
    readonly IEntityContext<Entity_168> _context = default!;

    protected Entity_168() { }

    public Entity_168(IEntityContext<Entity_168> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_168 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_168s
{
    readonly IQueryContext<Entity_168> _queryContext;

    public Entity_168s(IQueryContext<Entity_168> queryContext) => _queryContext = queryContext;

    public List<Entity_168> By0(string name_0) => _queryContext.All();
    public List<Entity_168> By1(string name_1) => _queryContext.All();
}

public class Entity_169
{
    readonly IEntityContext<Entity_169> _context = default!;

    protected Entity_169() { }

    public Entity_169(IEntityContext<Entity_169> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_169 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_169s
{
    readonly IQueryContext<Entity_169> _queryContext;

    public Entity_169s(IQueryContext<Entity_169> queryContext) => _queryContext = queryContext;

    public List<Entity_169> By0(string name_0) => _queryContext.All();
    public List<Entity_169> By1(string name_1) => _queryContext.All();
}

public class Entity_170
{
    readonly IEntityContext<Entity_170> _context = default!;

    protected Entity_170() { }

    public Entity_170(IEntityContext<Entity_170> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_170 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_170s
{
    readonly IQueryContext<Entity_170> _queryContext;

    public Entity_170s(IQueryContext<Entity_170> queryContext) => _queryContext = queryContext;

    public List<Entity_170> By0(string name_0) => _queryContext.All();
    public List<Entity_170> By1(string name_1) => _queryContext.All();
}

public class Entity_171
{
    readonly IEntityContext<Entity_171> _context = default!;

    protected Entity_171() { }

    public Entity_171(IEntityContext<Entity_171> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_171 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_171s
{
    readonly IQueryContext<Entity_171> _queryContext;

    public Entity_171s(IQueryContext<Entity_171> queryContext) => _queryContext = queryContext;

    public List<Entity_171> By0(string name_0) => _queryContext.All();
    public List<Entity_171> By1(string name_1) => _queryContext.All();
}

public class Entity_172
{
    readonly IEntityContext<Entity_172> _context = default!;

    protected Entity_172() { }

    public Entity_172(IEntityContext<Entity_172> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_172 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_172s
{
    readonly IQueryContext<Entity_172> _queryContext;

    public Entity_172s(IQueryContext<Entity_172> queryContext) => _queryContext = queryContext;

    public List<Entity_172> By0(string name_0) => _queryContext.All();
    public List<Entity_172> By1(string name_1) => _queryContext.All();
}

public class Entity_173
{
    readonly IEntityContext<Entity_173> _context = default!;

    protected Entity_173() { }

    public Entity_173(IEntityContext<Entity_173> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_173 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_173s
{
    readonly IQueryContext<Entity_173> _queryContext;

    public Entity_173s(IQueryContext<Entity_173> queryContext) => _queryContext = queryContext;

    public List<Entity_173> By0(string name_0) => _queryContext.All();
    public List<Entity_173> By1(string name_1) => _queryContext.All();
}

public class Entity_174
{
    readonly IEntityContext<Entity_174> _context = default!;

    protected Entity_174() { }

    public Entity_174(IEntityContext<Entity_174> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_174 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_174s
{
    readonly IQueryContext<Entity_174> _queryContext;

    public Entity_174s(IQueryContext<Entity_174> queryContext) => _queryContext = queryContext;

    public List<Entity_174> By0(string name_0) => _queryContext.All();
    public List<Entity_174> By1(string name_1) => _queryContext.All();
}

public class Entity_175
{
    readonly IEntityContext<Entity_175> _context = default!;

    protected Entity_175() { }

    public Entity_175(IEntityContext<Entity_175> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_175 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_175s
{
    readonly IQueryContext<Entity_175> _queryContext;

    public Entity_175s(IQueryContext<Entity_175> queryContext) => _queryContext = queryContext;

    public List<Entity_175> By0(string name_0) => _queryContext.All();
    public List<Entity_175> By1(string name_1) => _queryContext.All();
}

public class Entity_176
{
    readonly IEntityContext<Entity_176> _context = default!;

    protected Entity_176() { }

    public Entity_176(IEntityContext<Entity_176> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_176 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_176s
{
    readonly IQueryContext<Entity_176> _queryContext;

    public Entity_176s(IQueryContext<Entity_176> queryContext) => _queryContext = queryContext;

    public List<Entity_176> By0(string name_0) => _queryContext.All();
    public List<Entity_176> By1(string name_1) => _queryContext.All();
}

public class Entity_177
{
    readonly IEntityContext<Entity_177> _context = default!;

    protected Entity_177() { }

    public Entity_177(IEntityContext<Entity_177> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_177 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_177s
{
    readonly IQueryContext<Entity_177> _queryContext;

    public Entity_177s(IQueryContext<Entity_177> queryContext) => _queryContext = queryContext;

    public List<Entity_177> By0(string name_0) => _queryContext.All();
    public List<Entity_177> By1(string name_1) => _queryContext.All();
}

public class Entity_178
{
    readonly IEntityContext<Entity_178> _context = default!;

    protected Entity_178() { }

    public Entity_178(IEntityContext<Entity_178> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_178 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_178s
{
    readonly IQueryContext<Entity_178> _queryContext;

    public Entity_178s(IQueryContext<Entity_178> queryContext) => _queryContext = queryContext;

    public List<Entity_178> By0(string name_0) => _queryContext.All();
    public List<Entity_178> By1(string name_1) => _queryContext.All();
}

public class Entity_179
{
    readonly IEntityContext<Entity_179> _context = default!;

    protected Entity_179() { }

    public Entity_179(IEntityContext<Entity_179> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_179 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_179s
{
    readonly IQueryContext<Entity_179> _queryContext;

    public Entity_179s(IQueryContext<Entity_179> queryContext) => _queryContext = queryContext;

    public List<Entity_179> By0(string name_0) => _queryContext.All();
    public List<Entity_179> By1(string name_1) => _queryContext.All();
}

public class Entity_180
{
    readonly IEntityContext<Entity_180> _context = default!;

    protected Entity_180() { }

    public Entity_180(IEntityContext<Entity_180> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_180 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_180s
{
    readonly IQueryContext<Entity_180> _queryContext;

    public Entity_180s(IQueryContext<Entity_180> queryContext) => _queryContext = queryContext;

    public List<Entity_180> By0(string name_0) => _queryContext.All();
    public List<Entity_180> By1(string name_1) => _queryContext.All();
}

public class Entity_181
{
    readonly IEntityContext<Entity_181> _context = default!;

    protected Entity_181() { }

    public Entity_181(IEntityContext<Entity_181> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_181 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_181s
{
    readonly IQueryContext<Entity_181> _queryContext;

    public Entity_181s(IQueryContext<Entity_181> queryContext) => _queryContext = queryContext;

    public List<Entity_181> By0(string name_0) => _queryContext.All();
    public List<Entity_181> By1(string name_1) => _queryContext.All();
}

public class Entity_182
{
    readonly IEntityContext<Entity_182> _context = default!;

    protected Entity_182() { }

    public Entity_182(IEntityContext<Entity_182> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_182 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_182s
{
    readonly IQueryContext<Entity_182> _queryContext;

    public Entity_182s(IQueryContext<Entity_182> queryContext) => _queryContext = queryContext;

    public List<Entity_182> By0(string name_0) => _queryContext.All();
    public List<Entity_182> By1(string name_1) => _queryContext.All();
}

public class Entity_183
{
    readonly IEntityContext<Entity_183> _context = default!;

    protected Entity_183() { }

    public Entity_183(IEntityContext<Entity_183> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_183 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_183s
{
    readonly IQueryContext<Entity_183> _queryContext;

    public Entity_183s(IQueryContext<Entity_183> queryContext) => _queryContext = queryContext;

    public List<Entity_183> By0(string name_0) => _queryContext.All();
    public List<Entity_183> By1(string name_1) => _queryContext.All();
}

public class Entity_184
{
    readonly IEntityContext<Entity_184> _context = default!;

    protected Entity_184() { }

    public Entity_184(IEntityContext<Entity_184> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_184 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_184s
{
    readonly IQueryContext<Entity_184> _queryContext;

    public Entity_184s(IQueryContext<Entity_184> queryContext) => _queryContext = queryContext;

    public List<Entity_184> By0(string name_0) => _queryContext.All();
    public List<Entity_184> By1(string name_1) => _queryContext.All();
}

public class Entity_185
{
    readonly IEntityContext<Entity_185> _context = default!;

    protected Entity_185() { }

    public Entity_185(IEntityContext<Entity_185> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_185 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_185s
{
    readonly IQueryContext<Entity_185> _queryContext;

    public Entity_185s(IQueryContext<Entity_185> queryContext) => _queryContext = queryContext;

    public List<Entity_185> By0(string name_0) => _queryContext.All();
    public List<Entity_185> By1(string name_1) => _queryContext.All();
}

public class Entity_186
{
    readonly IEntityContext<Entity_186> _context = default!;

    protected Entity_186() { }

    public Entity_186(IEntityContext<Entity_186> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_186 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_186s
{
    readonly IQueryContext<Entity_186> _queryContext;

    public Entity_186s(IQueryContext<Entity_186> queryContext) => _queryContext = queryContext;

    public List<Entity_186> By0(string name_0) => _queryContext.All();
    public List<Entity_186> By1(string name_1) => _queryContext.All();
}

public class Entity_187
{
    readonly IEntityContext<Entity_187> _context = default!;

    protected Entity_187() { }

    public Entity_187(IEntityContext<Entity_187> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_187 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_187s
{
    readonly IQueryContext<Entity_187> _queryContext;

    public Entity_187s(IQueryContext<Entity_187> queryContext) => _queryContext = queryContext;

    public List<Entity_187> By0(string name_0) => _queryContext.All();
    public List<Entity_187> By1(string name_1) => _queryContext.All();
}

public class Entity_188
{
    readonly IEntityContext<Entity_188> _context = default!;

    protected Entity_188() { }

    public Entity_188(IEntityContext<Entity_188> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_188 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_188s
{
    readonly IQueryContext<Entity_188> _queryContext;

    public Entity_188s(IQueryContext<Entity_188> queryContext) => _queryContext = queryContext;

    public List<Entity_188> By0(string name_0) => _queryContext.All();
    public List<Entity_188> By1(string name_1) => _queryContext.All();
}

public class Entity_189
{
    readonly IEntityContext<Entity_189> _context = default!;

    protected Entity_189() { }

    public Entity_189(IEntityContext<Entity_189> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_189 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_189s
{
    readonly IQueryContext<Entity_189> _queryContext;

    public Entity_189s(IQueryContext<Entity_189> queryContext) => _queryContext = queryContext;

    public List<Entity_189> By0(string name_0) => _queryContext.All();
    public List<Entity_189> By1(string name_1) => _queryContext.All();
}

public class Entity_190
{
    readonly IEntityContext<Entity_190> _context = default!;

    protected Entity_190() { }

    public Entity_190(IEntityContext<Entity_190> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_190 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_190s
{
    readonly IQueryContext<Entity_190> _queryContext;

    public Entity_190s(IQueryContext<Entity_190> queryContext) => _queryContext = queryContext;

    public List<Entity_190> By0(string name_0) => _queryContext.All();
    public List<Entity_190> By1(string name_1) => _queryContext.All();
}

public class Entity_191
{
    readonly IEntityContext<Entity_191> _context = default!;

    protected Entity_191() { }

    public Entity_191(IEntityContext<Entity_191> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_191 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_191s
{
    readonly IQueryContext<Entity_191> _queryContext;

    public Entity_191s(IQueryContext<Entity_191> queryContext) => _queryContext = queryContext;

    public List<Entity_191> By0(string name_0) => _queryContext.All();
    public List<Entity_191> By1(string name_1) => _queryContext.All();
}

public class Entity_192
{
    readonly IEntityContext<Entity_192> _context = default!;

    protected Entity_192() { }

    public Entity_192(IEntityContext<Entity_192> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_192 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_192s
{
    readonly IQueryContext<Entity_192> _queryContext;

    public Entity_192s(IQueryContext<Entity_192> queryContext) => _queryContext = queryContext;

    public List<Entity_192> By0(string name_0) => _queryContext.All();
    public List<Entity_192> By1(string name_1) => _queryContext.All();
}

public class Entity_193
{
    readonly IEntityContext<Entity_193> _context = default!;

    protected Entity_193() { }

    public Entity_193(IEntityContext<Entity_193> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_193 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_193s
{
    readonly IQueryContext<Entity_193> _queryContext;

    public Entity_193s(IQueryContext<Entity_193> queryContext) => _queryContext = queryContext;

    public List<Entity_193> By0(string name_0) => _queryContext.All();
    public List<Entity_193> By1(string name_1) => _queryContext.All();
}

public class Entity_194
{
    readonly IEntityContext<Entity_194> _context = default!;

    protected Entity_194() { }

    public Entity_194(IEntityContext<Entity_194> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_194 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_194s
{
    readonly IQueryContext<Entity_194> _queryContext;

    public Entity_194s(IQueryContext<Entity_194> queryContext) => _queryContext = queryContext;

    public List<Entity_194> By0(string name_0) => _queryContext.All();
    public List<Entity_194> By1(string name_1) => _queryContext.All();
}

public class Entity_195
{
    readonly IEntityContext<Entity_195> _context = default!;

    protected Entity_195() { }

    public Entity_195(IEntityContext<Entity_195> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_195 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_195s
{
    readonly IQueryContext<Entity_195> _queryContext;

    public Entity_195s(IQueryContext<Entity_195> queryContext) => _queryContext = queryContext;

    public List<Entity_195> By0(string name_0) => _queryContext.All();
    public List<Entity_195> By1(string name_1) => _queryContext.All();
}

public class Entity_196
{
    readonly IEntityContext<Entity_196> _context = default!;

    protected Entity_196() { }

    public Entity_196(IEntityContext<Entity_196> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_196 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_196s
{
    readonly IQueryContext<Entity_196> _queryContext;

    public Entity_196s(IQueryContext<Entity_196> queryContext) => _queryContext = queryContext;

    public List<Entity_196> By0(string name_0) => _queryContext.All();
    public List<Entity_196> By1(string name_1) => _queryContext.All();
}

public class Entity_197
{
    readonly IEntityContext<Entity_197> _context = default!;

    protected Entity_197() { }

    public Entity_197(IEntityContext<Entity_197> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_197 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_197s
{
    readonly IQueryContext<Entity_197> _queryContext;

    public Entity_197s(IQueryContext<Entity_197> queryContext) => _queryContext = queryContext;

    public List<Entity_197> By0(string name_0) => _queryContext.All();
    public List<Entity_197> By1(string name_1) => _queryContext.All();
}

public class Entity_198
{
    readonly IEntityContext<Entity_198> _context = default!;

    protected Entity_198() { }

    public Entity_198(IEntityContext<Entity_198> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_198 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_198s
{
    readonly IQueryContext<Entity_198> _queryContext;

    public Entity_198s(IQueryContext<Entity_198> queryContext) => _queryContext = queryContext;

    public List<Entity_198> By0(string name_0) => _queryContext.All();
    public List<Entity_198> By1(string name_1) => _queryContext.All();
}

public class Entity_199
{
    readonly IEntityContext<Entity_199> _context = default!;

    protected Entity_199() { }

    public Entity_199(IEntityContext<Entity_199> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_199 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_199s
{
    readonly IQueryContext<Entity_199> _queryContext;

    public Entity_199s(IQueryContext<Entity_199> queryContext) => _queryContext = queryContext;

    public List<Entity_199> By0(string name_0) => _queryContext.All();
    public List<Entity_199> By1(string name_1) => _queryContext.All();
}

public class Entity_200
{
    readonly IEntityContext<Entity_200> _context = default!;

    protected Entity_200() { }

    public Entity_200(IEntityContext<Entity_200> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_200 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_200s
{
    readonly IQueryContext<Entity_200> _queryContext;

    public Entity_200s(IQueryContext<Entity_200> queryContext) => _queryContext = queryContext;

    public List<Entity_200> By0(string name_0) => _queryContext.All();
    public List<Entity_200> By1(string name_1) => _queryContext.All();
}

public class Entity_201
{
    readonly IEntityContext<Entity_201> _context = default!;

    protected Entity_201() { }

    public Entity_201(IEntityContext<Entity_201> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_201 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_201s
{
    readonly IQueryContext<Entity_201> _queryContext;

    public Entity_201s(IQueryContext<Entity_201> queryContext) => _queryContext = queryContext;

    public List<Entity_201> By0(string name_0) => _queryContext.All();
    public List<Entity_201> By1(string name_1) => _queryContext.All();
}

public class Entity_202
{
    readonly IEntityContext<Entity_202> _context = default!;

    protected Entity_202() { }

    public Entity_202(IEntityContext<Entity_202> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_202 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_202s
{
    readonly IQueryContext<Entity_202> _queryContext;

    public Entity_202s(IQueryContext<Entity_202> queryContext) => _queryContext = queryContext;

    public List<Entity_202> By0(string name_0) => _queryContext.All();
    public List<Entity_202> By1(string name_1) => _queryContext.All();
}

public class Entity_203
{
    readonly IEntityContext<Entity_203> _context = default!;

    protected Entity_203() { }

    public Entity_203(IEntityContext<Entity_203> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_203 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_203s
{
    readonly IQueryContext<Entity_203> _queryContext;

    public Entity_203s(IQueryContext<Entity_203> queryContext) => _queryContext = queryContext;

    public List<Entity_203> By0(string name_0) => _queryContext.All();
    public List<Entity_203> By1(string name_1) => _queryContext.All();
}

public class Entity_204
{
    readonly IEntityContext<Entity_204> _context = default!;

    protected Entity_204() { }

    public Entity_204(IEntityContext<Entity_204> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_204 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_204s
{
    readonly IQueryContext<Entity_204> _queryContext;

    public Entity_204s(IQueryContext<Entity_204> queryContext) => _queryContext = queryContext;

    public List<Entity_204> By0(string name_0) => _queryContext.All();
    public List<Entity_204> By1(string name_1) => _queryContext.All();
}

public class Entity_205
{
    readonly IEntityContext<Entity_205> _context = default!;

    protected Entity_205() { }

    public Entity_205(IEntityContext<Entity_205> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_205 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_205s
{
    readonly IQueryContext<Entity_205> _queryContext;

    public Entity_205s(IQueryContext<Entity_205> queryContext) => _queryContext = queryContext;

    public List<Entity_205> By0(string name_0) => _queryContext.All();
    public List<Entity_205> By1(string name_1) => _queryContext.All();
}

public class Entity_206
{
    readonly IEntityContext<Entity_206> _context = default!;

    protected Entity_206() { }

    public Entity_206(IEntityContext<Entity_206> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_206 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_206s
{
    readonly IQueryContext<Entity_206> _queryContext;

    public Entity_206s(IQueryContext<Entity_206> queryContext) => _queryContext = queryContext;

    public List<Entity_206> By0(string name_0) => _queryContext.All();
    public List<Entity_206> By1(string name_1) => _queryContext.All();
}

public class Entity_207
{
    readonly IEntityContext<Entity_207> _context = default!;

    protected Entity_207() { }

    public Entity_207(IEntityContext<Entity_207> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_207 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_207s
{
    readonly IQueryContext<Entity_207> _queryContext;

    public Entity_207s(IQueryContext<Entity_207> queryContext) => _queryContext = queryContext;

    public List<Entity_207> By0(string name_0) => _queryContext.All();
    public List<Entity_207> By1(string name_1) => _queryContext.All();
}

public class Entity_208
{
    readonly IEntityContext<Entity_208> _context = default!;

    protected Entity_208() { }

    public Entity_208(IEntityContext<Entity_208> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_208 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_208s
{
    readonly IQueryContext<Entity_208> _queryContext;

    public Entity_208s(IQueryContext<Entity_208> queryContext) => _queryContext = queryContext;

    public List<Entity_208> By0(string name_0) => _queryContext.All();
    public List<Entity_208> By1(string name_1) => _queryContext.All();
}

public class Entity_209
{
    readonly IEntityContext<Entity_209> _context = default!;

    protected Entity_209() { }

    public Entity_209(IEntityContext<Entity_209> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_209 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_209s
{
    readonly IQueryContext<Entity_209> _queryContext;

    public Entity_209s(IQueryContext<Entity_209> queryContext) => _queryContext = queryContext;

    public List<Entity_209> By0(string name_0) => _queryContext.All();
    public List<Entity_209> By1(string name_1) => _queryContext.All();
}

public class Entity_210
{
    readonly IEntityContext<Entity_210> _context = default!;

    protected Entity_210() { }

    public Entity_210(IEntityContext<Entity_210> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_210 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_210s
{
    readonly IQueryContext<Entity_210> _queryContext;

    public Entity_210s(IQueryContext<Entity_210> queryContext) => _queryContext = queryContext;

    public List<Entity_210> By0(string name_0) => _queryContext.All();
    public List<Entity_210> By1(string name_1) => _queryContext.All();
}

public class Entity_211
{
    readonly IEntityContext<Entity_211> _context = default!;

    protected Entity_211() { }

    public Entity_211(IEntityContext<Entity_211> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_211 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_211s
{
    readonly IQueryContext<Entity_211> _queryContext;

    public Entity_211s(IQueryContext<Entity_211> queryContext) => _queryContext = queryContext;

    public List<Entity_211> By0(string name_0) => _queryContext.All();
    public List<Entity_211> By1(string name_1) => _queryContext.All();
}

public class Entity_212
{
    readonly IEntityContext<Entity_212> _context = default!;

    protected Entity_212() { }

    public Entity_212(IEntityContext<Entity_212> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_212 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_212s
{
    readonly IQueryContext<Entity_212> _queryContext;

    public Entity_212s(IQueryContext<Entity_212> queryContext) => _queryContext = queryContext;

    public List<Entity_212> By0(string name_0) => _queryContext.All();
    public List<Entity_212> By1(string name_1) => _queryContext.All();
}

public class Entity_213
{
    readonly IEntityContext<Entity_213> _context = default!;

    protected Entity_213() { }

    public Entity_213(IEntityContext<Entity_213> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_213 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_213s
{
    readonly IQueryContext<Entity_213> _queryContext;

    public Entity_213s(IQueryContext<Entity_213> queryContext) => _queryContext = queryContext;

    public List<Entity_213> By0(string name_0) => _queryContext.All();
    public List<Entity_213> By1(string name_1) => _queryContext.All();
}

public class Entity_214
{
    readonly IEntityContext<Entity_214> _context = default!;

    protected Entity_214() { }

    public Entity_214(IEntityContext<Entity_214> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_214 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_214s
{
    readonly IQueryContext<Entity_214> _queryContext;

    public Entity_214s(IQueryContext<Entity_214> queryContext) => _queryContext = queryContext;

    public List<Entity_214> By0(string name_0) => _queryContext.All();
    public List<Entity_214> By1(string name_1) => _queryContext.All();
}

public class Entity_215
{
    readonly IEntityContext<Entity_215> _context = default!;

    protected Entity_215() { }

    public Entity_215(IEntityContext<Entity_215> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_215 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_215s
{
    readonly IQueryContext<Entity_215> _queryContext;

    public Entity_215s(IQueryContext<Entity_215> queryContext) => _queryContext = queryContext;

    public List<Entity_215> By0(string name_0) => _queryContext.All();
    public List<Entity_215> By1(string name_1) => _queryContext.All();
}

public class Entity_216
{
    readonly IEntityContext<Entity_216> _context = default!;

    protected Entity_216() { }

    public Entity_216(IEntityContext<Entity_216> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_216 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_216s
{
    readonly IQueryContext<Entity_216> _queryContext;

    public Entity_216s(IQueryContext<Entity_216> queryContext) => _queryContext = queryContext;

    public List<Entity_216> By0(string name_0) => _queryContext.All();
    public List<Entity_216> By1(string name_1) => _queryContext.All();
}

public class Entity_217
{
    readonly IEntityContext<Entity_217> _context = default!;

    protected Entity_217() { }

    public Entity_217(IEntityContext<Entity_217> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_217 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_217s
{
    readonly IQueryContext<Entity_217> _queryContext;

    public Entity_217s(IQueryContext<Entity_217> queryContext) => _queryContext = queryContext;

    public List<Entity_217> By0(string name_0) => _queryContext.All();
    public List<Entity_217> By1(string name_1) => _queryContext.All();
}

public class Entity_218
{
    readonly IEntityContext<Entity_218> _context = default!;

    protected Entity_218() { }

    public Entity_218(IEntityContext<Entity_218> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_218 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_218s
{
    readonly IQueryContext<Entity_218> _queryContext;

    public Entity_218s(IQueryContext<Entity_218> queryContext) => _queryContext = queryContext;

    public List<Entity_218> By0(string name_0) => _queryContext.All();
    public List<Entity_218> By1(string name_1) => _queryContext.All();
}

public class Entity_219
{
    readonly IEntityContext<Entity_219> _context = default!;

    protected Entity_219() { }

    public Entity_219(IEntityContext<Entity_219> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_219 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_219s
{
    readonly IQueryContext<Entity_219> _queryContext;

    public Entity_219s(IQueryContext<Entity_219> queryContext) => _queryContext = queryContext;

    public List<Entity_219> By0(string name_0) => _queryContext.All();
    public List<Entity_219> By1(string name_1) => _queryContext.All();
}

public class Entity_220
{
    readonly IEntityContext<Entity_220> _context = default!;

    protected Entity_220() { }

    public Entity_220(IEntityContext<Entity_220> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_220 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_220s
{
    readonly IQueryContext<Entity_220> _queryContext;

    public Entity_220s(IQueryContext<Entity_220> queryContext) => _queryContext = queryContext;

    public List<Entity_220> By0(string name_0) => _queryContext.All();
    public List<Entity_220> By1(string name_1) => _queryContext.All();
}

public class Entity_221
{
    readonly IEntityContext<Entity_221> _context = default!;

    protected Entity_221() { }

    public Entity_221(IEntityContext<Entity_221> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_221 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_221s
{
    readonly IQueryContext<Entity_221> _queryContext;

    public Entity_221s(IQueryContext<Entity_221> queryContext) => _queryContext = queryContext;

    public List<Entity_221> By0(string name_0) => _queryContext.All();
    public List<Entity_221> By1(string name_1) => _queryContext.All();
}

public class Entity_222
{
    readonly IEntityContext<Entity_222> _context = default!;

    protected Entity_222() { }

    public Entity_222(IEntityContext<Entity_222> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_222 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_222s
{
    readonly IQueryContext<Entity_222> _queryContext;

    public Entity_222s(IQueryContext<Entity_222> queryContext) => _queryContext = queryContext;

    public List<Entity_222> By0(string name_0) => _queryContext.All();
    public List<Entity_222> By1(string name_1) => _queryContext.All();
}

public class Entity_223
{
    readonly IEntityContext<Entity_223> _context = default!;

    protected Entity_223() { }

    public Entity_223(IEntityContext<Entity_223> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_223 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_223s
{
    readonly IQueryContext<Entity_223> _queryContext;

    public Entity_223s(IQueryContext<Entity_223> queryContext) => _queryContext = queryContext;

    public List<Entity_223> By0(string name_0) => _queryContext.All();
    public List<Entity_223> By1(string name_1) => _queryContext.All();
}

public class Entity_224
{
    readonly IEntityContext<Entity_224> _context = default!;

    protected Entity_224() { }

    public Entity_224(IEntityContext<Entity_224> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_224 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_224s
{
    readonly IQueryContext<Entity_224> _queryContext;

    public Entity_224s(IQueryContext<Entity_224> queryContext) => _queryContext = queryContext;

    public List<Entity_224> By0(string name_0) => _queryContext.All();
    public List<Entity_224> By1(string name_1) => _queryContext.All();
}

public class Entity_225
{
    readonly IEntityContext<Entity_225> _context = default!;

    protected Entity_225() { }

    public Entity_225(IEntityContext<Entity_225> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_225 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_225s
{
    readonly IQueryContext<Entity_225> _queryContext;

    public Entity_225s(IQueryContext<Entity_225> queryContext) => _queryContext = queryContext;

    public List<Entity_225> By0(string name_0) => _queryContext.All();
    public List<Entity_225> By1(string name_1) => _queryContext.All();
}

public class Entity_226
{
    readonly IEntityContext<Entity_226> _context = default!;

    protected Entity_226() { }

    public Entity_226(IEntityContext<Entity_226> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_226 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_226s
{
    readonly IQueryContext<Entity_226> _queryContext;

    public Entity_226s(IQueryContext<Entity_226> queryContext) => _queryContext = queryContext;

    public List<Entity_226> By0(string name_0) => _queryContext.All();
    public List<Entity_226> By1(string name_1) => _queryContext.All();
}

public class Entity_227
{
    readonly IEntityContext<Entity_227> _context = default!;

    protected Entity_227() { }

    public Entity_227(IEntityContext<Entity_227> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_227 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_227s
{
    readonly IQueryContext<Entity_227> _queryContext;

    public Entity_227s(IQueryContext<Entity_227> queryContext) => _queryContext = queryContext;

    public List<Entity_227> By0(string name_0) => _queryContext.All();
    public List<Entity_227> By1(string name_1) => _queryContext.All();
}

public class Entity_228
{
    readonly IEntityContext<Entity_228> _context = default!;

    protected Entity_228() { }

    public Entity_228(IEntityContext<Entity_228> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_228 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_228s
{
    readonly IQueryContext<Entity_228> _queryContext;

    public Entity_228s(IQueryContext<Entity_228> queryContext) => _queryContext = queryContext;

    public List<Entity_228> By0(string name_0) => _queryContext.All();
    public List<Entity_228> By1(string name_1) => _queryContext.All();
}

public class Entity_229
{
    readonly IEntityContext<Entity_229> _context = default!;

    protected Entity_229() { }

    public Entity_229(IEntityContext<Entity_229> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_229 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_229s
{
    readonly IQueryContext<Entity_229> _queryContext;

    public Entity_229s(IQueryContext<Entity_229> queryContext) => _queryContext = queryContext;

    public List<Entity_229> By0(string name_0) => _queryContext.All();
    public List<Entity_229> By1(string name_1) => _queryContext.All();
}

public class Entity_230
{
    readonly IEntityContext<Entity_230> _context = default!;

    protected Entity_230() { }

    public Entity_230(IEntityContext<Entity_230> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_230 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_230s
{
    readonly IQueryContext<Entity_230> _queryContext;

    public Entity_230s(IQueryContext<Entity_230> queryContext) => _queryContext = queryContext;

    public List<Entity_230> By0(string name_0) => _queryContext.All();
    public List<Entity_230> By1(string name_1) => _queryContext.All();
}

public class Entity_231
{
    readonly IEntityContext<Entity_231> _context = default!;

    protected Entity_231() { }

    public Entity_231(IEntityContext<Entity_231> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_231 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_231s
{
    readonly IQueryContext<Entity_231> _queryContext;

    public Entity_231s(IQueryContext<Entity_231> queryContext) => _queryContext = queryContext;

    public List<Entity_231> By0(string name_0) => _queryContext.All();
    public List<Entity_231> By1(string name_1) => _queryContext.All();
}

public class Entity_232
{
    readonly IEntityContext<Entity_232> _context = default!;

    protected Entity_232() { }

    public Entity_232(IEntityContext<Entity_232> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_232 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_232s
{
    readonly IQueryContext<Entity_232> _queryContext;

    public Entity_232s(IQueryContext<Entity_232> queryContext) => _queryContext = queryContext;

    public List<Entity_232> By0(string name_0) => _queryContext.All();
    public List<Entity_232> By1(string name_1) => _queryContext.All();
}

public class Entity_233
{
    readonly IEntityContext<Entity_233> _context = default!;

    protected Entity_233() { }

    public Entity_233(IEntityContext<Entity_233> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_233 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_233s
{
    readonly IQueryContext<Entity_233> _queryContext;

    public Entity_233s(IQueryContext<Entity_233> queryContext) => _queryContext = queryContext;

    public List<Entity_233> By0(string name_0) => _queryContext.All();
    public List<Entity_233> By1(string name_1) => _queryContext.All();
}

public class Entity_234
{
    readonly IEntityContext<Entity_234> _context = default!;

    protected Entity_234() { }

    public Entity_234(IEntityContext<Entity_234> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_234 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_234s
{
    readonly IQueryContext<Entity_234> _queryContext;

    public Entity_234s(IQueryContext<Entity_234> queryContext) => _queryContext = queryContext;

    public List<Entity_234> By0(string name_0) => _queryContext.All();
    public List<Entity_234> By1(string name_1) => _queryContext.All();
}

public class Entity_235
{
    readonly IEntityContext<Entity_235> _context = default!;

    protected Entity_235() { }

    public Entity_235(IEntityContext<Entity_235> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_235 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_235s
{
    readonly IQueryContext<Entity_235> _queryContext;

    public Entity_235s(IQueryContext<Entity_235> queryContext) => _queryContext = queryContext;

    public List<Entity_235> By0(string name_0) => _queryContext.All();
    public List<Entity_235> By1(string name_1) => _queryContext.All();
}

public class Entity_236
{
    readonly IEntityContext<Entity_236> _context = default!;

    protected Entity_236() { }

    public Entity_236(IEntityContext<Entity_236> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_236 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_236s
{
    readonly IQueryContext<Entity_236> _queryContext;

    public Entity_236s(IQueryContext<Entity_236> queryContext) => _queryContext = queryContext;

    public List<Entity_236> By0(string name_0) => _queryContext.All();
    public List<Entity_236> By1(string name_1) => _queryContext.All();
}

public class Entity_237
{
    readonly IEntityContext<Entity_237> _context = default!;

    protected Entity_237() { }

    public Entity_237(IEntityContext<Entity_237> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_237 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_237s
{
    readonly IQueryContext<Entity_237> _queryContext;

    public Entity_237s(IQueryContext<Entity_237> queryContext) => _queryContext = queryContext;

    public List<Entity_237> By0(string name_0) => _queryContext.All();
    public List<Entity_237> By1(string name_1) => _queryContext.All();
}

public class Entity_238
{
    readonly IEntityContext<Entity_238> _context = default!;

    protected Entity_238() { }

    public Entity_238(IEntityContext<Entity_238> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_238 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_238s
{
    readonly IQueryContext<Entity_238> _queryContext;

    public Entity_238s(IQueryContext<Entity_238> queryContext) => _queryContext = queryContext;

    public List<Entity_238> By0(string name_0) => _queryContext.All();
    public List<Entity_238> By1(string name_1) => _queryContext.All();
}

public class Entity_239
{
    readonly IEntityContext<Entity_239> _context = default!;

    protected Entity_239() { }

    public Entity_239(IEntityContext<Entity_239> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_239 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_239s
{
    readonly IQueryContext<Entity_239> _queryContext;

    public Entity_239s(IQueryContext<Entity_239> queryContext) => _queryContext = queryContext;

    public List<Entity_239> By0(string name_0) => _queryContext.All();
    public List<Entity_239> By1(string name_1) => _queryContext.All();
}

public class Entity_240
{
    readonly IEntityContext<Entity_240> _context = default!;

    protected Entity_240() { }

    public Entity_240(IEntityContext<Entity_240> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_240 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_240s
{
    readonly IQueryContext<Entity_240> _queryContext;

    public Entity_240s(IQueryContext<Entity_240> queryContext) => _queryContext = queryContext;

    public List<Entity_240> By0(string name_0) => _queryContext.All();
    public List<Entity_240> By1(string name_1) => _queryContext.All();
}

public class Entity_241
{
    readonly IEntityContext<Entity_241> _context = default!;

    protected Entity_241() { }

    public Entity_241(IEntityContext<Entity_241> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_241 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_241s
{
    readonly IQueryContext<Entity_241> _queryContext;

    public Entity_241s(IQueryContext<Entity_241> queryContext) => _queryContext = queryContext;

    public List<Entity_241> By0(string name_0) => _queryContext.All();
    public List<Entity_241> By1(string name_1) => _queryContext.All();
}

public class Entity_242
{
    readonly IEntityContext<Entity_242> _context = default!;

    protected Entity_242() { }

    public Entity_242(IEntityContext<Entity_242> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_242 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_242s
{
    readonly IQueryContext<Entity_242> _queryContext;

    public Entity_242s(IQueryContext<Entity_242> queryContext) => _queryContext = queryContext;

    public List<Entity_242> By0(string name_0) => _queryContext.All();
    public List<Entity_242> By1(string name_1) => _queryContext.All();
}

public class Entity_243
{
    readonly IEntityContext<Entity_243> _context = default!;

    protected Entity_243() { }

    public Entity_243(IEntityContext<Entity_243> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_243 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_243s
{
    readonly IQueryContext<Entity_243> _queryContext;

    public Entity_243s(IQueryContext<Entity_243> queryContext) => _queryContext = queryContext;

    public List<Entity_243> By0(string name_0) => _queryContext.All();
    public List<Entity_243> By1(string name_1) => _queryContext.All();
}

public class Entity_244
{
    readonly IEntityContext<Entity_244> _context = default!;

    protected Entity_244() { }

    public Entity_244(IEntityContext<Entity_244> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_244 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_244s
{
    readonly IQueryContext<Entity_244> _queryContext;

    public Entity_244s(IQueryContext<Entity_244> queryContext) => _queryContext = queryContext;

    public List<Entity_244> By0(string name_0) => _queryContext.All();
    public List<Entity_244> By1(string name_1) => _queryContext.All();
}

public class Entity_245
{
    readonly IEntityContext<Entity_245> _context = default!;

    protected Entity_245() { }

    public Entity_245(IEntityContext<Entity_245> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_245 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_245s
{
    readonly IQueryContext<Entity_245> _queryContext;

    public Entity_245s(IQueryContext<Entity_245> queryContext) => _queryContext = queryContext;

    public List<Entity_245> By0(string name_0) => _queryContext.All();
    public List<Entity_245> By1(string name_1) => _queryContext.All();
}

public class Entity_246
{
    readonly IEntityContext<Entity_246> _context = default!;

    protected Entity_246() { }

    public Entity_246(IEntityContext<Entity_246> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_246 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_246s
{
    readonly IQueryContext<Entity_246> _queryContext;

    public Entity_246s(IQueryContext<Entity_246> queryContext) => _queryContext = queryContext;

    public List<Entity_246> By0(string name_0) => _queryContext.All();
    public List<Entity_246> By1(string name_1) => _queryContext.All();
}

public class Entity_247
{
    readonly IEntityContext<Entity_247> _context = default!;

    protected Entity_247() { }

    public Entity_247(IEntityContext<Entity_247> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_247 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_247s
{
    readonly IQueryContext<Entity_247> _queryContext;

    public Entity_247s(IQueryContext<Entity_247> queryContext) => _queryContext = queryContext;

    public List<Entity_247> By0(string name_0) => _queryContext.All();
    public List<Entity_247> By1(string name_1) => _queryContext.All();
}

public class Entity_248
{
    readonly IEntityContext<Entity_248> _context = default!;

    protected Entity_248() { }

    public Entity_248(IEntityContext<Entity_248> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_248 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_248s
{
    readonly IQueryContext<Entity_248> _queryContext;

    public Entity_248s(IQueryContext<Entity_248> queryContext) => _queryContext = queryContext;

    public List<Entity_248> By0(string name_0) => _queryContext.All();
    public List<Entity_248> By1(string name_1) => _queryContext.All();
}

public class Entity_249
{
    readonly IEntityContext<Entity_249> _context = default!;

    protected Entity_249() { }

    public Entity_249(IEntityContext<Entity_249> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_249 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_249s
{
    readonly IQueryContext<Entity_249> _queryContext;

    public Entity_249s(IQueryContext<Entity_249> queryContext) => _queryContext = queryContext;

    public List<Entity_249> By0(string name_0) => _queryContext.All();
    public List<Entity_249> By1(string name_1) => _queryContext.All();
}

public class Entity_250
{
    readonly IEntityContext<Entity_250> _context = default!;

    protected Entity_250() { }

    public Entity_250(IEntityContext<Entity_250> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_250 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_250s
{
    readonly IQueryContext<Entity_250> _queryContext;

    public Entity_250s(IQueryContext<Entity_250> queryContext) => _queryContext = queryContext;

    public List<Entity_250> By0(string name_0) => _queryContext.All();
    public List<Entity_250> By1(string name_1) => _queryContext.All();
}

public class Entity_251
{
    readonly IEntityContext<Entity_251> _context = default!;

    protected Entity_251() { }

    public Entity_251(IEntityContext<Entity_251> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_251 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_251s
{
    readonly IQueryContext<Entity_251> _queryContext;

    public Entity_251s(IQueryContext<Entity_251> queryContext) => _queryContext = queryContext;

    public List<Entity_251> By0(string name_0) => _queryContext.All();
    public List<Entity_251> By1(string name_1) => _queryContext.All();
}

public class Entity_252
{
    readonly IEntityContext<Entity_252> _context = default!;

    protected Entity_252() { }

    public Entity_252(IEntityContext<Entity_252> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_252 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_252s
{
    readonly IQueryContext<Entity_252> _queryContext;

    public Entity_252s(IQueryContext<Entity_252> queryContext) => _queryContext = queryContext;

    public List<Entity_252> By0(string name_0) => _queryContext.All();
    public List<Entity_252> By1(string name_1) => _queryContext.All();
}

public class Entity_253
{
    readonly IEntityContext<Entity_253> _context = default!;

    protected Entity_253() { }

    public Entity_253(IEntityContext<Entity_253> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_253 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_253s
{
    readonly IQueryContext<Entity_253> _queryContext;

    public Entity_253s(IQueryContext<Entity_253> queryContext) => _queryContext = queryContext;

    public List<Entity_253> By0(string name_0) => _queryContext.All();
    public List<Entity_253> By1(string name_1) => _queryContext.All();
}

public class Entity_254
{
    readonly IEntityContext<Entity_254> _context = default!;

    protected Entity_254() { }

    public Entity_254(IEntityContext<Entity_254> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_254 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_254s
{
    readonly IQueryContext<Entity_254> _queryContext;

    public Entity_254s(IQueryContext<Entity_254> queryContext) => _queryContext = queryContext;

    public List<Entity_254> By0(string name_0) => _queryContext.All();
    public List<Entity_254> By1(string name_1) => _queryContext.All();
}

public class Entity_255
{
    readonly IEntityContext<Entity_255> _context = default!;

    protected Entity_255() { }

    public Entity_255(IEntityContext<Entity_255> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_255 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_255s
{
    readonly IQueryContext<Entity_255> _queryContext;

    public Entity_255s(IQueryContext<Entity_255> queryContext) => _queryContext = queryContext;

    public List<Entity_255> By0(string name_0) => _queryContext.All();
    public List<Entity_255> By1(string name_1) => _queryContext.All();
}

public class Entity_256
{
    readonly IEntityContext<Entity_256> _context = default!;

    protected Entity_256() { }

    public Entity_256(IEntityContext<Entity_256> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_256 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_256s
{
    readonly IQueryContext<Entity_256> _queryContext;

    public Entity_256s(IQueryContext<Entity_256> queryContext) => _queryContext = queryContext;

    public List<Entity_256> By0(string name_0) => _queryContext.All();
    public List<Entity_256> By1(string name_1) => _queryContext.All();
}

public class Entity_257
{
    readonly IEntityContext<Entity_257> _context = default!;

    protected Entity_257() { }

    public Entity_257(IEntityContext<Entity_257> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_257 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_257s
{
    readonly IQueryContext<Entity_257> _queryContext;

    public Entity_257s(IQueryContext<Entity_257> queryContext) => _queryContext = queryContext;

    public List<Entity_257> By0(string name_0) => _queryContext.All();
    public List<Entity_257> By1(string name_1) => _queryContext.All();
}

public class Entity_258
{
    readonly IEntityContext<Entity_258> _context = default!;

    protected Entity_258() { }

    public Entity_258(IEntityContext<Entity_258> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_258 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_258s
{
    readonly IQueryContext<Entity_258> _queryContext;

    public Entity_258s(IQueryContext<Entity_258> queryContext) => _queryContext = queryContext;

    public List<Entity_258> By0(string name_0) => _queryContext.All();
    public List<Entity_258> By1(string name_1) => _queryContext.All();
}

public class Entity_259
{
    readonly IEntityContext<Entity_259> _context = default!;

    protected Entity_259() { }

    public Entity_259(IEntityContext<Entity_259> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_259 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_259s
{
    readonly IQueryContext<Entity_259> _queryContext;

    public Entity_259s(IQueryContext<Entity_259> queryContext) => _queryContext = queryContext;

    public List<Entity_259> By0(string name_0) => _queryContext.All();
    public List<Entity_259> By1(string name_1) => _queryContext.All();
}

public class Entity_260
{
    readonly IEntityContext<Entity_260> _context = default!;

    protected Entity_260() { }

    public Entity_260(IEntityContext<Entity_260> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_260 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_260s
{
    readonly IQueryContext<Entity_260> _queryContext;

    public Entity_260s(IQueryContext<Entity_260> queryContext) => _queryContext = queryContext;

    public List<Entity_260> By0(string name_0) => _queryContext.All();
    public List<Entity_260> By1(string name_1) => _queryContext.All();
}

public class Entity_261
{
    readonly IEntityContext<Entity_261> _context = default!;

    protected Entity_261() { }

    public Entity_261(IEntityContext<Entity_261> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_261 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_261s
{
    readonly IQueryContext<Entity_261> _queryContext;

    public Entity_261s(IQueryContext<Entity_261> queryContext) => _queryContext = queryContext;

    public List<Entity_261> By0(string name_0) => _queryContext.All();
    public List<Entity_261> By1(string name_1) => _queryContext.All();
}

public class Entity_262
{
    readonly IEntityContext<Entity_262> _context = default!;

    protected Entity_262() { }

    public Entity_262(IEntityContext<Entity_262> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_262 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_262s
{
    readonly IQueryContext<Entity_262> _queryContext;

    public Entity_262s(IQueryContext<Entity_262> queryContext) => _queryContext = queryContext;

    public List<Entity_262> By0(string name_0) => _queryContext.All();
    public List<Entity_262> By1(string name_1) => _queryContext.All();
}

public class Entity_263
{
    readonly IEntityContext<Entity_263> _context = default!;

    protected Entity_263() { }

    public Entity_263(IEntityContext<Entity_263> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_263 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_263s
{
    readonly IQueryContext<Entity_263> _queryContext;

    public Entity_263s(IQueryContext<Entity_263> queryContext) => _queryContext = queryContext;

    public List<Entity_263> By0(string name_0) => _queryContext.All();
    public List<Entity_263> By1(string name_1) => _queryContext.All();
}

public class Entity_264
{
    readonly IEntityContext<Entity_264> _context = default!;

    protected Entity_264() { }

    public Entity_264(IEntityContext<Entity_264> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_264 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_264s
{
    readonly IQueryContext<Entity_264> _queryContext;

    public Entity_264s(IQueryContext<Entity_264> queryContext) => _queryContext = queryContext;

    public List<Entity_264> By0(string name_0) => _queryContext.All();
    public List<Entity_264> By1(string name_1) => _queryContext.All();
}

public class Entity_265
{
    readonly IEntityContext<Entity_265> _context = default!;

    protected Entity_265() { }

    public Entity_265(IEntityContext<Entity_265> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_265 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_265s
{
    readonly IQueryContext<Entity_265> _queryContext;

    public Entity_265s(IQueryContext<Entity_265> queryContext) => _queryContext = queryContext;

    public List<Entity_265> By0(string name_0) => _queryContext.All();
    public List<Entity_265> By1(string name_1) => _queryContext.All();
}

public class Entity_266
{
    readonly IEntityContext<Entity_266> _context = default!;

    protected Entity_266() { }

    public Entity_266(IEntityContext<Entity_266> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_266 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_266s
{
    readonly IQueryContext<Entity_266> _queryContext;

    public Entity_266s(IQueryContext<Entity_266> queryContext) => _queryContext = queryContext;

    public List<Entity_266> By0(string name_0) => _queryContext.All();
    public List<Entity_266> By1(string name_1) => _queryContext.All();
}

public class Entity_267
{
    readonly IEntityContext<Entity_267> _context = default!;

    protected Entity_267() { }

    public Entity_267(IEntityContext<Entity_267> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_267 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_267s
{
    readonly IQueryContext<Entity_267> _queryContext;

    public Entity_267s(IQueryContext<Entity_267> queryContext) => _queryContext = queryContext;

    public List<Entity_267> By0(string name_0) => _queryContext.All();
    public List<Entity_267> By1(string name_1) => _queryContext.All();
}

public class Entity_268
{
    readonly IEntityContext<Entity_268> _context = default!;

    protected Entity_268() { }

    public Entity_268(IEntityContext<Entity_268> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_268 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_268s
{
    readonly IQueryContext<Entity_268> _queryContext;

    public Entity_268s(IQueryContext<Entity_268> queryContext) => _queryContext = queryContext;

    public List<Entity_268> By0(string name_0) => _queryContext.All();
    public List<Entity_268> By1(string name_1) => _queryContext.All();
}

public class Entity_269
{
    readonly IEntityContext<Entity_269> _context = default!;

    protected Entity_269() { }

    public Entity_269(IEntityContext<Entity_269> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_269 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_269s
{
    readonly IQueryContext<Entity_269> _queryContext;

    public Entity_269s(IQueryContext<Entity_269> queryContext) => _queryContext = queryContext;

    public List<Entity_269> By0(string name_0) => _queryContext.All();
    public List<Entity_269> By1(string name_1) => _queryContext.All();
}

public class Entity_270
{
    readonly IEntityContext<Entity_270> _context = default!;

    protected Entity_270() { }

    public Entity_270(IEntityContext<Entity_270> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_270 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_270s
{
    readonly IQueryContext<Entity_270> _queryContext;

    public Entity_270s(IQueryContext<Entity_270> queryContext) => _queryContext = queryContext;

    public List<Entity_270> By0(string name_0) => _queryContext.All();
    public List<Entity_270> By1(string name_1) => _queryContext.All();
}

public class Entity_271
{
    readonly IEntityContext<Entity_271> _context = default!;

    protected Entity_271() { }

    public Entity_271(IEntityContext<Entity_271> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_271 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_271s
{
    readonly IQueryContext<Entity_271> _queryContext;

    public Entity_271s(IQueryContext<Entity_271> queryContext) => _queryContext = queryContext;

    public List<Entity_271> By0(string name_0) => _queryContext.All();
    public List<Entity_271> By1(string name_1) => _queryContext.All();
}

public class Entity_272
{
    readonly IEntityContext<Entity_272> _context = default!;

    protected Entity_272() { }

    public Entity_272(IEntityContext<Entity_272> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_272 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_272s
{
    readonly IQueryContext<Entity_272> _queryContext;

    public Entity_272s(IQueryContext<Entity_272> queryContext) => _queryContext = queryContext;

    public List<Entity_272> By0(string name_0) => _queryContext.All();
    public List<Entity_272> By1(string name_1) => _queryContext.All();
}

public class Entity_273
{
    readonly IEntityContext<Entity_273> _context = default!;

    protected Entity_273() { }

    public Entity_273(IEntityContext<Entity_273> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_273 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_273s
{
    readonly IQueryContext<Entity_273> _queryContext;

    public Entity_273s(IQueryContext<Entity_273> queryContext) => _queryContext = queryContext;

    public List<Entity_273> By0(string name_0) => _queryContext.All();
    public List<Entity_273> By1(string name_1) => _queryContext.All();
}

public class Entity_274
{
    readonly IEntityContext<Entity_274> _context = default!;

    protected Entity_274() { }

    public Entity_274(IEntityContext<Entity_274> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_274 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_274s
{
    readonly IQueryContext<Entity_274> _queryContext;

    public Entity_274s(IQueryContext<Entity_274> queryContext) => _queryContext = queryContext;

    public List<Entity_274> By0(string name_0) => _queryContext.All();
    public List<Entity_274> By1(string name_1) => _queryContext.All();
}

public class Entity_275
{
    readonly IEntityContext<Entity_275> _context = default!;

    protected Entity_275() { }

    public Entity_275(IEntityContext<Entity_275> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_275 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_275s
{
    readonly IQueryContext<Entity_275> _queryContext;

    public Entity_275s(IQueryContext<Entity_275> queryContext) => _queryContext = queryContext;

    public List<Entity_275> By0(string name_0) => _queryContext.All();
    public List<Entity_275> By1(string name_1) => _queryContext.All();
}

public class Entity_276
{
    readonly IEntityContext<Entity_276> _context = default!;

    protected Entity_276() { }

    public Entity_276(IEntityContext<Entity_276> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_276 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_276s
{
    readonly IQueryContext<Entity_276> _queryContext;

    public Entity_276s(IQueryContext<Entity_276> queryContext) => _queryContext = queryContext;

    public List<Entity_276> By0(string name_0) => _queryContext.All();
    public List<Entity_276> By1(string name_1) => _queryContext.All();
}

public class Entity_277
{
    readonly IEntityContext<Entity_277> _context = default!;

    protected Entity_277() { }

    public Entity_277(IEntityContext<Entity_277> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_277 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_277s
{
    readonly IQueryContext<Entity_277> _queryContext;

    public Entity_277s(IQueryContext<Entity_277> queryContext) => _queryContext = queryContext;

    public List<Entity_277> By0(string name_0) => _queryContext.All();
    public List<Entity_277> By1(string name_1) => _queryContext.All();
}

public class Entity_278
{
    readonly IEntityContext<Entity_278> _context = default!;

    protected Entity_278() { }

    public Entity_278(IEntityContext<Entity_278> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_278 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_278s
{
    readonly IQueryContext<Entity_278> _queryContext;

    public Entity_278s(IQueryContext<Entity_278> queryContext) => _queryContext = queryContext;

    public List<Entity_278> By0(string name_0) => _queryContext.All();
    public List<Entity_278> By1(string name_1) => _queryContext.All();
}

public class Entity_279
{
    readonly IEntityContext<Entity_279> _context = default!;

    protected Entity_279() { }

    public Entity_279(IEntityContext<Entity_279> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_279 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_279s
{
    readonly IQueryContext<Entity_279> _queryContext;

    public Entity_279s(IQueryContext<Entity_279> queryContext) => _queryContext = queryContext;

    public List<Entity_279> By0(string name_0) => _queryContext.All();
    public List<Entity_279> By1(string name_1) => _queryContext.All();
}

public class Entity_280
{
    readonly IEntityContext<Entity_280> _context = default!;

    protected Entity_280() { }

    public Entity_280(IEntityContext<Entity_280> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_280 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_280s
{
    readonly IQueryContext<Entity_280> _queryContext;

    public Entity_280s(IQueryContext<Entity_280> queryContext) => _queryContext = queryContext;

    public List<Entity_280> By0(string name_0) => _queryContext.All();
    public List<Entity_280> By1(string name_1) => _queryContext.All();
}

public class Entity_281
{
    readonly IEntityContext<Entity_281> _context = default!;

    protected Entity_281() { }

    public Entity_281(IEntityContext<Entity_281> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_281 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_281s
{
    readonly IQueryContext<Entity_281> _queryContext;

    public Entity_281s(IQueryContext<Entity_281> queryContext) => _queryContext = queryContext;

    public List<Entity_281> By0(string name_0) => _queryContext.All();
    public List<Entity_281> By1(string name_1) => _queryContext.All();
}

public class Entity_282
{
    readonly IEntityContext<Entity_282> _context = default!;

    protected Entity_282() { }

    public Entity_282(IEntityContext<Entity_282> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_282 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_282s
{
    readonly IQueryContext<Entity_282> _queryContext;

    public Entity_282s(IQueryContext<Entity_282> queryContext) => _queryContext = queryContext;

    public List<Entity_282> By0(string name_0) => _queryContext.All();
    public List<Entity_282> By1(string name_1) => _queryContext.All();
}

public class Entity_283
{
    readonly IEntityContext<Entity_283> _context = default!;

    protected Entity_283() { }

    public Entity_283(IEntityContext<Entity_283> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_283 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_283s
{
    readonly IQueryContext<Entity_283> _queryContext;

    public Entity_283s(IQueryContext<Entity_283> queryContext) => _queryContext = queryContext;

    public List<Entity_283> By0(string name_0) => _queryContext.All();
    public List<Entity_283> By1(string name_1) => _queryContext.All();
}

public class Entity_284
{
    readonly IEntityContext<Entity_284> _context = default!;

    protected Entity_284() { }

    public Entity_284(IEntityContext<Entity_284> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_284 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_284s
{
    readonly IQueryContext<Entity_284> _queryContext;

    public Entity_284s(IQueryContext<Entity_284> queryContext) => _queryContext = queryContext;

    public List<Entity_284> By0(string name_0) => _queryContext.All();
    public List<Entity_284> By1(string name_1) => _queryContext.All();
}

public class Entity_285
{
    readonly IEntityContext<Entity_285> _context = default!;

    protected Entity_285() { }

    public Entity_285(IEntityContext<Entity_285> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_285 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_285s
{
    readonly IQueryContext<Entity_285> _queryContext;

    public Entity_285s(IQueryContext<Entity_285> queryContext) => _queryContext = queryContext;

    public List<Entity_285> By0(string name_0) => _queryContext.All();
    public List<Entity_285> By1(string name_1) => _queryContext.All();
}

public class Entity_286
{
    readonly IEntityContext<Entity_286> _context = default!;

    protected Entity_286() { }

    public Entity_286(IEntityContext<Entity_286> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_286 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_286s
{
    readonly IQueryContext<Entity_286> _queryContext;

    public Entity_286s(IQueryContext<Entity_286> queryContext) => _queryContext = queryContext;

    public List<Entity_286> By0(string name_0) => _queryContext.All();
    public List<Entity_286> By1(string name_1) => _queryContext.All();
}

public class Entity_287
{
    readonly IEntityContext<Entity_287> _context = default!;

    protected Entity_287() { }

    public Entity_287(IEntityContext<Entity_287> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_287 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_287s
{
    readonly IQueryContext<Entity_287> _queryContext;

    public Entity_287s(IQueryContext<Entity_287> queryContext) => _queryContext = queryContext;

    public List<Entity_287> By0(string name_0) => _queryContext.All();
    public List<Entity_287> By1(string name_1) => _queryContext.All();
}

public class Entity_288
{
    readonly IEntityContext<Entity_288> _context = default!;

    protected Entity_288() { }

    public Entity_288(IEntityContext<Entity_288> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_288 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_288s
{
    readonly IQueryContext<Entity_288> _queryContext;

    public Entity_288s(IQueryContext<Entity_288> queryContext) => _queryContext = queryContext;

    public List<Entity_288> By0(string name_0) => _queryContext.All();
    public List<Entity_288> By1(string name_1) => _queryContext.All();
}

public class Entity_289
{
    readonly IEntityContext<Entity_289> _context = default!;

    protected Entity_289() { }

    public Entity_289(IEntityContext<Entity_289> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_289 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_289s
{
    readonly IQueryContext<Entity_289> _queryContext;

    public Entity_289s(IQueryContext<Entity_289> queryContext) => _queryContext = queryContext;

    public List<Entity_289> By0(string name_0) => _queryContext.All();
    public List<Entity_289> By1(string name_1) => _queryContext.All();
}

public class Entity_290
{
    readonly IEntityContext<Entity_290> _context = default!;

    protected Entity_290() { }

    public Entity_290(IEntityContext<Entity_290> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_290 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_290s
{
    readonly IQueryContext<Entity_290> _queryContext;

    public Entity_290s(IQueryContext<Entity_290> queryContext) => _queryContext = queryContext;

    public List<Entity_290> By0(string name_0) => _queryContext.All();
    public List<Entity_290> By1(string name_1) => _queryContext.All();
}

public class Entity_291
{
    readonly IEntityContext<Entity_291> _context = default!;

    protected Entity_291() { }

    public Entity_291(IEntityContext<Entity_291> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_291 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_291s
{
    readonly IQueryContext<Entity_291> _queryContext;

    public Entity_291s(IQueryContext<Entity_291> queryContext) => _queryContext = queryContext;

    public List<Entity_291> By0(string name_0) => _queryContext.All();
    public List<Entity_291> By1(string name_1) => _queryContext.All();
}

public class Entity_292
{
    readonly IEntityContext<Entity_292> _context = default!;

    protected Entity_292() { }

    public Entity_292(IEntityContext<Entity_292> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_292 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_292s
{
    readonly IQueryContext<Entity_292> _queryContext;

    public Entity_292s(IQueryContext<Entity_292> queryContext) => _queryContext = queryContext;

    public List<Entity_292> By0(string name_0) => _queryContext.All();
    public List<Entity_292> By1(string name_1) => _queryContext.All();
}

public class Entity_293
{
    readonly IEntityContext<Entity_293> _context = default!;

    protected Entity_293() { }

    public Entity_293(IEntityContext<Entity_293> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_293 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_293s
{
    readonly IQueryContext<Entity_293> _queryContext;

    public Entity_293s(IQueryContext<Entity_293> queryContext) => _queryContext = queryContext;

    public List<Entity_293> By0(string name_0) => _queryContext.All();
    public List<Entity_293> By1(string name_1) => _queryContext.All();
}

public class Entity_294
{
    readonly IEntityContext<Entity_294> _context = default!;

    protected Entity_294() { }

    public Entity_294(IEntityContext<Entity_294> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_294 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_294s
{
    readonly IQueryContext<Entity_294> _queryContext;

    public Entity_294s(IQueryContext<Entity_294> queryContext) => _queryContext = queryContext;

    public List<Entity_294> By0(string name_0) => _queryContext.All();
    public List<Entity_294> By1(string name_1) => _queryContext.All();
}

public class Entity_295
{
    readonly IEntityContext<Entity_295> _context = default!;

    protected Entity_295() { }

    public Entity_295(IEntityContext<Entity_295> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_295 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_295s
{
    readonly IQueryContext<Entity_295> _queryContext;

    public Entity_295s(IQueryContext<Entity_295> queryContext) => _queryContext = queryContext;

    public List<Entity_295> By0(string name_0) => _queryContext.All();
    public List<Entity_295> By1(string name_1) => _queryContext.All();
}

public class Entity_296
{
    readonly IEntityContext<Entity_296> _context = default!;

    protected Entity_296() { }

    public Entity_296(IEntityContext<Entity_296> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_296 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_296s
{
    readonly IQueryContext<Entity_296> _queryContext;

    public Entity_296s(IQueryContext<Entity_296> queryContext) => _queryContext = queryContext;

    public List<Entity_296> By0(string name_0) => _queryContext.All();
    public List<Entity_296> By1(string name_1) => _queryContext.All();
}

public class Entity_297
{
    readonly IEntityContext<Entity_297> _context = default!;

    protected Entity_297() { }

    public Entity_297(IEntityContext<Entity_297> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_297 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_297s
{
    readonly IQueryContext<Entity_297> _queryContext;

    public Entity_297s(IQueryContext<Entity_297> queryContext) => _queryContext = queryContext;

    public List<Entity_297> By0(string name_0) => _queryContext.All();
    public List<Entity_297> By1(string name_1) => _queryContext.All();
}

public class Entity_298
{
    readonly IEntityContext<Entity_298> _context = default!;

    protected Entity_298() { }

    public Entity_298(IEntityContext<Entity_298> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_298 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_298s
{
    readonly IQueryContext<Entity_298> _queryContext;

    public Entity_298s(IQueryContext<Entity_298> queryContext) => _queryContext = queryContext;

    public List<Entity_298> By0(string name_0) => _queryContext.All();
    public List<Entity_298> By1(string name_1) => _queryContext.All();
}

public class Entity_299
{
    readonly IEntityContext<Entity_299> _context = default!;

    protected Entity_299() { }

    public Entity_299(IEntityContext<Entity_299> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_299 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_299s
{
    readonly IQueryContext<Entity_299> _queryContext;

    public Entity_299s(IQueryContext<Entity_299> queryContext) => _queryContext = queryContext;

    public List<Entity_299> By0(string name_0) => _queryContext.All();
    public List<Entity_299> By1(string name_1) => _queryContext.All();
}

public class Entity_300
{
    readonly IEntityContext<Entity_300> _context = default!;

    protected Entity_300() { }

    public Entity_300(IEntityContext<Entity_300> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_300 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_300s
{
    readonly IQueryContext<Entity_300> _queryContext;

    public Entity_300s(IQueryContext<Entity_300> queryContext) => _queryContext = queryContext;

    public List<Entity_300> By0(string name_0) => _queryContext.All();
    public List<Entity_300> By1(string name_1) => _queryContext.All();
}

public class Entity_301
{
    readonly IEntityContext<Entity_301> _context = default!;

    protected Entity_301() { }

    public Entity_301(IEntityContext<Entity_301> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_301 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_301s
{
    readonly IQueryContext<Entity_301> _queryContext;

    public Entity_301s(IQueryContext<Entity_301> queryContext) => _queryContext = queryContext;

    public List<Entity_301> By0(string name_0) => _queryContext.All();
    public List<Entity_301> By1(string name_1) => _queryContext.All();
}

public class Entity_302
{
    readonly IEntityContext<Entity_302> _context = default!;

    protected Entity_302() { }

    public Entity_302(IEntityContext<Entity_302> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_302 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_302s
{
    readonly IQueryContext<Entity_302> _queryContext;

    public Entity_302s(IQueryContext<Entity_302> queryContext) => _queryContext = queryContext;

    public List<Entity_302> By0(string name_0) => _queryContext.All();
    public List<Entity_302> By1(string name_1) => _queryContext.All();
}

public class Entity_303
{
    readonly IEntityContext<Entity_303> _context = default!;

    protected Entity_303() { }

    public Entity_303(IEntityContext<Entity_303> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_303 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_303s
{
    readonly IQueryContext<Entity_303> _queryContext;

    public Entity_303s(IQueryContext<Entity_303> queryContext) => _queryContext = queryContext;

    public List<Entity_303> By0(string name_0) => _queryContext.All();
    public List<Entity_303> By1(string name_1) => _queryContext.All();
}

public class Entity_304
{
    readonly IEntityContext<Entity_304> _context = default!;

    protected Entity_304() { }

    public Entity_304(IEntityContext<Entity_304> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_304 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_304s
{
    readonly IQueryContext<Entity_304> _queryContext;

    public Entity_304s(IQueryContext<Entity_304> queryContext) => _queryContext = queryContext;

    public List<Entity_304> By0(string name_0) => _queryContext.All();
    public List<Entity_304> By1(string name_1) => _queryContext.All();
}

public class Entity_305
{
    readonly IEntityContext<Entity_305> _context = default!;

    protected Entity_305() { }

    public Entity_305(IEntityContext<Entity_305> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_305 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_305s
{
    readonly IQueryContext<Entity_305> _queryContext;

    public Entity_305s(IQueryContext<Entity_305> queryContext) => _queryContext = queryContext;

    public List<Entity_305> By0(string name_0) => _queryContext.All();
    public List<Entity_305> By1(string name_1) => _queryContext.All();
}

public class Entity_306
{
    readonly IEntityContext<Entity_306> _context = default!;

    protected Entity_306() { }

    public Entity_306(IEntityContext<Entity_306> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_306 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_306s
{
    readonly IQueryContext<Entity_306> _queryContext;

    public Entity_306s(IQueryContext<Entity_306> queryContext) => _queryContext = queryContext;

    public List<Entity_306> By0(string name_0) => _queryContext.All();
    public List<Entity_306> By1(string name_1) => _queryContext.All();
}

public class Entity_307
{
    readonly IEntityContext<Entity_307> _context = default!;

    protected Entity_307() { }

    public Entity_307(IEntityContext<Entity_307> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_307 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_307s
{
    readonly IQueryContext<Entity_307> _queryContext;

    public Entity_307s(IQueryContext<Entity_307> queryContext) => _queryContext = queryContext;

    public List<Entity_307> By0(string name_0) => _queryContext.All();
    public List<Entity_307> By1(string name_1) => _queryContext.All();
}

public class Entity_308
{
    readonly IEntityContext<Entity_308> _context = default!;

    protected Entity_308() { }

    public Entity_308(IEntityContext<Entity_308> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_308 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_308s
{
    readonly IQueryContext<Entity_308> _queryContext;

    public Entity_308s(IQueryContext<Entity_308> queryContext) => _queryContext = queryContext;

    public List<Entity_308> By0(string name_0) => _queryContext.All();
    public List<Entity_308> By1(string name_1) => _queryContext.All();
}

public class Entity_309
{
    readonly IEntityContext<Entity_309> _context = default!;

    protected Entity_309() { }

    public Entity_309(IEntityContext<Entity_309> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_309 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_309s
{
    readonly IQueryContext<Entity_309> _queryContext;

    public Entity_309s(IQueryContext<Entity_309> queryContext) => _queryContext = queryContext;

    public List<Entity_309> By0(string name_0) => _queryContext.All();
    public List<Entity_309> By1(string name_1) => _queryContext.All();
}

public class Entity_310
{
    readonly IEntityContext<Entity_310> _context = default!;

    protected Entity_310() { }

    public Entity_310(IEntityContext<Entity_310> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_310 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_310s
{
    readonly IQueryContext<Entity_310> _queryContext;

    public Entity_310s(IQueryContext<Entity_310> queryContext) => _queryContext = queryContext;

    public List<Entity_310> By0(string name_0) => _queryContext.All();
    public List<Entity_310> By1(string name_1) => _queryContext.All();
}

public class Entity_311
{
    readonly IEntityContext<Entity_311> _context = default!;

    protected Entity_311() { }

    public Entity_311(IEntityContext<Entity_311> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_311 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_311s
{
    readonly IQueryContext<Entity_311> _queryContext;

    public Entity_311s(IQueryContext<Entity_311> queryContext) => _queryContext = queryContext;

    public List<Entity_311> By0(string name_0) => _queryContext.All();
    public List<Entity_311> By1(string name_1) => _queryContext.All();
}

public class Entity_312
{
    readonly IEntityContext<Entity_312> _context = default!;

    protected Entity_312() { }

    public Entity_312(IEntityContext<Entity_312> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_312 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_312s
{
    readonly IQueryContext<Entity_312> _queryContext;

    public Entity_312s(IQueryContext<Entity_312> queryContext) => _queryContext = queryContext;

    public List<Entity_312> By0(string name_0) => _queryContext.All();
    public List<Entity_312> By1(string name_1) => _queryContext.All();
}

public class Entity_313
{
    readonly IEntityContext<Entity_313> _context = default!;

    protected Entity_313() { }

    public Entity_313(IEntityContext<Entity_313> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_313 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_313s
{
    readonly IQueryContext<Entity_313> _queryContext;

    public Entity_313s(IQueryContext<Entity_313> queryContext) => _queryContext = queryContext;

    public List<Entity_313> By0(string name_0) => _queryContext.All();
    public List<Entity_313> By1(string name_1) => _queryContext.All();
}

public class Entity_314
{
    readonly IEntityContext<Entity_314> _context = default!;

    protected Entity_314() { }

    public Entity_314(IEntityContext<Entity_314> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_314 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_314s
{
    readonly IQueryContext<Entity_314> _queryContext;

    public Entity_314s(IQueryContext<Entity_314> queryContext) => _queryContext = queryContext;

    public List<Entity_314> By0(string name_0) => _queryContext.All();
    public List<Entity_314> By1(string name_1) => _queryContext.All();
}

public class Entity_315
{
    readonly IEntityContext<Entity_315> _context = default!;

    protected Entity_315() { }

    public Entity_315(IEntityContext<Entity_315> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_315 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_315s
{
    readonly IQueryContext<Entity_315> _queryContext;

    public Entity_315s(IQueryContext<Entity_315> queryContext) => _queryContext = queryContext;

    public List<Entity_315> By0(string name_0) => _queryContext.All();
    public List<Entity_315> By1(string name_1) => _queryContext.All();
}

public class Entity_316
{
    readonly IEntityContext<Entity_316> _context = default!;

    protected Entity_316() { }

    public Entity_316(IEntityContext<Entity_316> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_316 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_316s
{
    readonly IQueryContext<Entity_316> _queryContext;

    public Entity_316s(IQueryContext<Entity_316> queryContext) => _queryContext = queryContext;

    public List<Entity_316> By0(string name_0) => _queryContext.All();
    public List<Entity_316> By1(string name_1) => _queryContext.All();
}

public class Entity_317
{
    readonly IEntityContext<Entity_317> _context = default!;

    protected Entity_317() { }

    public Entity_317(IEntityContext<Entity_317> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_317 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_317s
{
    readonly IQueryContext<Entity_317> _queryContext;

    public Entity_317s(IQueryContext<Entity_317> queryContext) => _queryContext = queryContext;

    public List<Entity_317> By0(string name_0) => _queryContext.All();
    public List<Entity_317> By1(string name_1) => _queryContext.All();
}

public class Entity_318
{
    readonly IEntityContext<Entity_318> _context = default!;

    protected Entity_318() { }

    public Entity_318(IEntityContext<Entity_318> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_318 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_318s
{
    readonly IQueryContext<Entity_318> _queryContext;

    public Entity_318s(IQueryContext<Entity_318> queryContext) => _queryContext = queryContext;

    public List<Entity_318> By0(string name_0) => _queryContext.All();
    public List<Entity_318> By1(string name_1) => _queryContext.All();
}

public class Entity_319
{
    readonly IEntityContext<Entity_319> _context = default!;

    protected Entity_319() { }

    public Entity_319(IEntityContext<Entity_319> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_319 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_319s
{
    readonly IQueryContext<Entity_319> _queryContext;

    public Entity_319s(IQueryContext<Entity_319> queryContext) => _queryContext = queryContext;

    public List<Entity_319> By0(string name_0) => _queryContext.All();
    public List<Entity_319> By1(string name_1) => _queryContext.All();
}

public class Entity_320
{
    readonly IEntityContext<Entity_320> _context = default!;

    protected Entity_320() { }

    public Entity_320(IEntityContext<Entity_320> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_320 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_320s
{
    readonly IQueryContext<Entity_320> _queryContext;

    public Entity_320s(IQueryContext<Entity_320> queryContext) => _queryContext = queryContext;

    public List<Entity_320> By0(string name_0) => _queryContext.All();
    public List<Entity_320> By1(string name_1) => _queryContext.All();
}

public class Entity_321
{
    readonly IEntityContext<Entity_321> _context = default!;

    protected Entity_321() { }

    public Entity_321(IEntityContext<Entity_321> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_321 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_321s
{
    readonly IQueryContext<Entity_321> _queryContext;

    public Entity_321s(IQueryContext<Entity_321> queryContext) => _queryContext = queryContext;

    public List<Entity_321> By0(string name_0) => _queryContext.All();
    public List<Entity_321> By1(string name_1) => _queryContext.All();
}

public class Entity_322
{
    readonly IEntityContext<Entity_322> _context = default!;

    protected Entity_322() { }

    public Entity_322(IEntityContext<Entity_322> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_322 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_322s
{
    readonly IQueryContext<Entity_322> _queryContext;

    public Entity_322s(IQueryContext<Entity_322> queryContext) => _queryContext = queryContext;

    public List<Entity_322> By0(string name_0) => _queryContext.All();
    public List<Entity_322> By1(string name_1) => _queryContext.All();
}

public class Entity_323
{
    readonly IEntityContext<Entity_323> _context = default!;

    protected Entity_323() { }

    public Entity_323(IEntityContext<Entity_323> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_323 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_323s
{
    readonly IQueryContext<Entity_323> _queryContext;

    public Entity_323s(IQueryContext<Entity_323> queryContext) => _queryContext = queryContext;

    public List<Entity_323> By0(string name_0) => _queryContext.All();
    public List<Entity_323> By1(string name_1) => _queryContext.All();
}

public class Entity_324
{
    readonly IEntityContext<Entity_324> _context = default!;

    protected Entity_324() { }

    public Entity_324(IEntityContext<Entity_324> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_324 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_324s
{
    readonly IQueryContext<Entity_324> _queryContext;

    public Entity_324s(IQueryContext<Entity_324> queryContext) => _queryContext = queryContext;

    public List<Entity_324> By0(string name_0) => _queryContext.All();
    public List<Entity_324> By1(string name_1) => _queryContext.All();
}

public class Entity_325
{
    readonly IEntityContext<Entity_325> _context = default!;

    protected Entity_325() { }

    public Entity_325(IEntityContext<Entity_325> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_325 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_325s
{
    readonly IQueryContext<Entity_325> _queryContext;

    public Entity_325s(IQueryContext<Entity_325> queryContext) => _queryContext = queryContext;

    public List<Entity_325> By0(string name_0) => _queryContext.All();
    public List<Entity_325> By1(string name_1) => _queryContext.All();
}

public class Entity_326
{
    readonly IEntityContext<Entity_326> _context = default!;

    protected Entity_326() { }

    public Entity_326(IEntityContext<Entity_326> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_326 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_326s
{
    readonly IQueryContext<Entity_326> _queryContext;

    public Entity_326s(IQueryContext<Entity_326> queryContext) => _queryContext = queryContext;

    public List<Entity_326> By0(string name_0) => _queryContext.All();
    public List<Entity_326> By1(string name_1) => _queryContext.All();
}

public class Entity_327
{
    readonly IEntityContext<Entity_327> _context = default!;

    protected Entity_327() { }

    public Entity_327(IEntityContext<Entity_327> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_327 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_327s
{
    readonly IQueryContext<Entity_327> _queryContext;

    public Entity_327s(IQueryContext<Entity_327> queryContext) => _queryContext = queryContext;

    public List<Entity_327> By0(string name_0) => _queryContext.All();
    public List<Entity_327> By1(string name_1) => _queryContext.All();
}

public class Entity_328
{
    readonly IEntityContext<Entity_328> _context = default!;

    protected Entity_328() { }

    public Entity_328(IEntityContext<Entity_328> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_328 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_328s
{
    readonly IQueryContext<Entity_328> _queryContext;

    public Entity_328s(IQueryContext<Entity_328> queryContext) => _queryContext = queryContext;

    public List<Entity_328> By0(string name_0) => _queryContext.All();
    public List<Entity_328> By1(string name_1) => _queryContext.All();
}

public class Entity_329
{
    readonly IEntityContext<Entity_329> _context = default!;

    protected Entity_329() { }

    public Entity_329(IEntityContext<Entity_329> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_329 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_329s
{
    readonly IQueryContext<Entity_329> _queryContext;

    public Entity_329s(IQueryContext<Entity_329> queryContext) => _queryContext = queryContext;

    public List<Entity_329> By0(string name_0) => _queryContext.All();
    public List<Entity_329> By1(string name_1) => _queryContext.All();
}

public class Entity_330
{
    readonly IEntityContext<Entity_330> _context = default!;

    protected Entity_330() { }

    public Entity_330(IEntityContext<Entity_330> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_330 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_330s
{
    readonly IQueryContext<Entity_330> _queryContext;

    public Entity_330s(IQueryContext<Entity_330> queryContext) => _queryContext = queryContext;

    public List<Entity_330> By0(string name_0) => _queryContext.All();
    public List<Entity_330> By1(string name_1) => _queryContext.All();
}

public class Entity_331
{
    readonly IEntityContext<Entity_331> _context = default!;

    protected Entity_331() { }

    public Entity_331(IEntityContext<Entity_331> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_331 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_331s
{
    readonly IQueryContext<Entity_331> _queryContext;

    public Entity_331s(IQueryContext<Entity_331> queryContext) => _queryContext = queryContext;

    public List<Entity_331> By0(string name_0) => _queryContext.All();
    public List<Entity_331> By1(string name_1) => _queryContext.All();
}

public class Entity_332
{
    readonly IEntityContext<Entity_332> _context = default!;

    protected Entity_332() { }

    public Entity_332(IEntityContext<Entity_332> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_332 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_332s
{
    readonly IQueryContext<Entity_332> _queryContext;

    public Entity_332s(IQueryContext<Entity_332> queryContext) => _queryContext = queryContext;

    public List<Entity_332> By0(string name_0) => _queryContext.All();
    public List<Entity_332> By1(string name_1) => _queryContext.All();
}

public class Entity_333
{
    readonly IEntityContext<Entity_333> _context = default!;

    protected Entity_333() { }

    public Entity_333(IEntityContext<Entity_333> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_333 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_333s
{
    readonly IQueryContext<Entity_333> _queryContext;

    public Entity_333s(IQueryContext<Entity_333> queryContext) => _queryContext = queryContext;

    public List<Entity_333> By0(string name_0) => _queryContext.All();
    public List<Entity_333> By1(string name_1) => _queryContext.All();
}

public class Entity_334
{
    readonly IEntityContext<Entity_334> _context = default!;

    protected Entity_334() { }

    public Entity_334(IEntityContext<Entity_334> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_334 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_334s
{
    readonly IQueryContext<Entity_334> _queryContext;

    public Entity_334s(IQueryContext<Entity_334> queryContext) => _queryContext = queryContext;

    public List<Entity_334> By0(string name_0) => _queryContext.All();
    public List<Entity_334> By1(string name_1) => _queryContext.All();
}

public class Entity_335
{
    readonly IEntityContext<Entity_335> _context = default!;

    protected Entity_335() { }

    public Entity_335(IEntityContext<Entity_335> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_335 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_335s
{
    readonly IQueryContext<Entity_335> _queryContext;

    public Entity_335s(IQueryContext<Entity_335> queryContext) => _queryContext = queryContext;

    public List<Entity_335> By0(string name_0) => _queryContext.All();
    public List<Entity_335> By1(string name_1) => _queryContext.All();
}

public class Entity_336
{
    readonly IEntityContext<Entity_336> _context = default!;

    protected Entity_336() { }

    public Entity_336(IEntityContext<Entity_336> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_336 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_336s
{
    readonly IQueryContext<Entity_336> _queryContext;

    public Entity_336s(IQueryContext<Entity_336> queryContext) => _queryContext = queryContext;

    public List<Entity_336> By0(string name_0) => _queryContext.All();
    public List<Entity_336> By1(string name_1) => _queryContext.All();
}

public class Entity_337
{
    readonly IEntityContext<Entity_337> _context = default!;

    protected Entity_337() { }

    public Entity_337(IEntityContext<Entity_337> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_337 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_337s
{
    readonly IQueryContext<Entity_337> _queryContext;

    public Entity_337s(IQueryContext<Entity_337> queryContext) => _queryContext = queryContext;

    public List<Entity_337> By0(string name_0) => _queryContext.All();
    public List<Entity_337> By1(string name_1) => _queryContext.All();
}

public class Entity_338
{
    readonly IEntityContext<Entity_338> _context = default!;

    protected Entity_338() { }

    public Entity_338(IEntityContext<Entity_338> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_338 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_338s
{
    readonly IQueryContext<Entity_338> _queryContext;

    public Entity_338s(IQueryContext<Entity_338> queryContext) => _queryContext = queryContext;

    public List<Entity_338> By0(string name_0) => _queryContext.All();
    public List<Entity_338> By1(string name_1) => _queryContext.All();
}

public class Entity_339
{
    readonly IEntityContext<Entity_339> _context = default!;

    protected Entity_339() { }

    public Entity_339(IEntityContext<Entity_339> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_339 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_339s
{
    readonly IQueryContext<Entity_339> _queryContext;

    public Entity_339s(IQueryContext<Entity_339> queryContext) => _queryContext = queryContext;

    public List<Entity_339> By0(string name_0) => _queryContext.All();
    public List<Entity_339> By1(string name_1) => _queryContext.All();
}

public class Entity_340
{
    readonly IEntityContext<Entity_340> _context = default!;

    protected Entity_340() { }

    public Entity_340(IEntityContext<Entity_340> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_340 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_340s
{
    readonly IQueryContext<Entity_340> _queryContext;

    public Entity_340s(IQueryContext<Entity_340> queryContext) => _queryContext = queryContext;

    public List<Entity_340> By0(string name_0) => _queryContext.All();
    public List<Entity_340> By1(string name_1) => _queryContext.All();
}

public class Entity_341
{
    readonly IEntityContext<Entity_341> _context = default!;

    protected Entity_341() { }

    public Entity_341(IEntityContext<Entity_341> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_341 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_341s
{
    readonly IQueryContext<Entity_341> _queryContext;

    public Entity_341s(IQueryContext<Entity_341> queryContext) => _queryContext = queryContext;

    public List<Entity_341> By0(string name_0) => _queryContext.All();
    public List<Entity_341> By1(string name_1) => _queryContext.All();
}

public class Entity_342
{
    readonly IEntityContext<Entity_342> _context = default!;

    protected Entity_342() { }

    public Entity_342(IEntityContext<Entity_342> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_342 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_342s
{
    readonly IQueryContext<Entity_342> _queryContext;

    public Entity_342s(IQueryContext<Entity_342> queryContext) => _queryContext = queryContext;

    public List<Entity_342> By0(string name_0) => _queryContext.All();
    public List<Entity_342> By1(string name_1) => _queryContext.All();
}

public class Entity_343
{
    readonly IEntityContext<Entity_343> _context = default!;

    protected Entity_343() { }

    public Entity_343(IEntityContext<Entity_343> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_343 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_343s
{
    readonly IQueryContext<Entity_343> _queryContext;

    public Entity_343s(IQueryContext<Entity_343> queryContext) => _queryContext = queryContext;

    public List<Entity_343> By0(string name_0) => _queryContext.All();
    public List<Entity_343> By1(string name_1) => _queryContext.All();
}

public class Entity_344
{
    readonly IEntityContext<Entity_344> _context = default!;

    protected Entity_344() { }

    public Entity_344(IEntityContext<Entity_344> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_344 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_344s
{
    readonly IQueryContext<Entity_344> _queryContext;

    public Entity_344s(IQueryContext<Entity_344> queryContext) => _queryContext = queryContext;

    public List<Entity_344> By0(string name_0) => _queryContext.All();
    public List<Entity_344> By1(string name_1) => _queryContext.All();
}

public class Entity_345
{
    readonly IEntityContext<Entity_345> _context = default!;

    protected Entity_345() { }

    public Entity_345(IEntityContext<Entity_345> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_345 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_345s
{
    readonly IQueryContext<Entity_345> _queryContext;

    public Entity_345s(IQueryContext<Entity_345> queryContext) => _queryContext = queryContext;

    public List<Entity_345> By0(string name_0) => _queryContext.All();
    public List<Entity_345> By1(string name_1) => _queryContext.All();
}

public class Entity_346
{
    readonly IEntityContext<Entity_346> _context = default!;

    protected Entity_346() { }

    public Entity_346(IEntityContext<Entity_346> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_346 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_346s
{
    readonly IQueryContext<Entity_346> _queryContext;

    public Entity_346s(IQueryContext<Entity_346> queryContext) => _queryContext = queryContext;

    public List<Entity_346> By0(string name_0) => _queryContext.All();
    public List<Entity_346> By1(string name_1) => _queryContext.All();
}

public class Entity_347
{
    readonly IEntityContext<Entity_347> _context = default!;

    protected Entity_347() { }

    public Entity_347(IEntityContext<Entity_347> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_347 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_347s
{
    readonly IQueryContext<Entity_347> _queryContext;

    public Entity_347s(IQueryContext<Entity_347> queryContext) => _queryContext = queryContext;

    public List<Entity_347> By0(string name_0) => _queryContext.All();
    public List<Entity_347> By1(string name_1) => _queryContext.All();
}

public class Entity_348
{
    readonly IEntityContext<Entity_348> _context = default!;

    protected Entity_348() { }

    public Entity_348(IEntityContext<Entity_348> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_348 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_348s
{
    readonly IQueryContext<Entity_348> _queryContext;

    public Entity_348s(IQueryContext<Entity_348> queryContext) => _queryContext = queryContext;

    public List<Entity_348> By0(string name_0) => _queryContext.All();
    public List<Entity_348> By1(string name_1) => _queryContext.All();
}

public class Entity_349
{
    readonly IEntityContext<Entity_349> _context = default!;

    protected Entity_349() { }

    public Entity_349(IEntityContext<Entity_349> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_349 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_349s
{
    readonly IQueryContext<Entity_349> _queryContext;

    public Entity_349s(IQueryContext<Entity_349> queryContext) => _queryContext = queryContext;

    public List<Entity_349> By0(string name_0) => _queryContext.All();
    public List<Entity_349> By1(string name_1) => _queryContext.All();
}

public class Entity_350
{
    readonly IEntityContext<Entity_350> _context = default!;

    protected Entity_350() { }

    public Entity_350(IEntityContext<Entity_350> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_350 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_350s
{
    readonly IQueryContext<Entity_350> _queryContext;

    public Entity_350s(IQueryContext<Entity_350> queryContext) => _queryContext = queryContext;

    public List<Entity_350> By0(string name_0) => _queryContext.All();
    public List<Entity_350> By1(string name_1) => _queryContext.All();
}

public class Entity_351
{
    readonly IEntityContext<Entity_351> _context = default!;

    protected Entity_351() { }

    public Entity_351(IEntityContext<Entity_351> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_351 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_351s
{
    readonly IQueryContext<Entity_351> _queryContext;

    public Entity_351s(IQueryContext<Entity_351> queryContext) => _queryContext = queryContext;

    public List<Entity_351> By0(string name_0) => _queryContext.All();
    public List<Entity_351> By1(string name_1) => _queryContext.All();
}

public class Entity_352
{
    readonly IEntityContext<Entity_352> _context = default!;

    protected Entity_352() { }

    public Entity_352(IEntityContext<Entity_352> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_352 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_352s
{
    readonly IQueryContext<Entity_352> _queryContext;

    public Entity_352s(IQueryContext<Entity_352> queryContext) => _queryContext = queryContext;

    public List<Entity_352> By0(string name_0) => _queryContext.All();
    public List<Entity_352> By1(string name_1) => _queryContext.All();
}

public class Entity_353
{
    readonly IEntityContext<Entity_353> _context = default!;

    protected Entity_353() { }

    public Entity_353(IEntityContext<Entity_353> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_353 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_353s
{
    readonly IQueryContext<Entity_353> _queryContext;

    public Entity_353s(IQueryContext<Entity_353> queryContext) => _queryContext = queryContext;

    public List<Entity_353> By0(string name_0) => _queryContext.All();
    public List<Entity_353> By1(string name_1) => _queryContext.All();
}

public class Entity_354
{
    readonly IEntityContext<Entity_354> _context = default!;

    protected Entity_354() { }

    public Entity_354(IEntityContext<Entity_354> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_354 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_354s
{
    readonly IQueryContext<Entity_354> _queryContext;

    public Entity_354s(IQueryContext<Entity_354> queryContext) => _queryContext = queryContext;

    public List<Entity_354> By0(string name_0) => _queryContext.All();
    public List<Entity_354> By1(string name_1) => _queryContext.All();
}

public class Entity_355
{
    readonly IEntityContext<Entity_355> _context = default!;

    protected Entity_355() { }

    public Entity_355(IEntityContext<Entity_355> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_355 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_355s
{
    readonly IQueryContext<Entity_355> _queryContext;

    public Entity_355s(IQueryContext<Entity_355> queryContext) => _queryContext = queryContext;

    public List<Entity_355> By0(string name_0) => _queryContext.All();
    public List<Entity_355> By1(string name_1) => _queryContext.All();
}

public class Entity_356
{
    readonly IEntityContext<Entity_356> _context = default!;

    protected Entity_356() { }

    public Entity_356(IEntityContext<Entity_356> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_356 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_356s
{
    readonly IQueryContext<Entity_356> _queryContext;

    public Entity_356s(IQueryContext<Entity_356> queryContext) => _queryContext = queryContext;

    public List<Entity_356> By0(string name_0) => _queryContext.All();
    public List<Entity_356> By1(string name_1) => _queryContext.All();
}

public class Entity_357
{
    readonly IEntityContext<Entity_357> _context = default!;

    protected Entity_357() { }

    public Entity_357(IEntityContext<Entity_357> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_357 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_357s
{
    readonly IQueryContext<Entity_357> _queryContext;

    public Entity_357s(IQueryContext<Entity_357> queryContext) => _queryContext = queryContext;

    public List<Entity_357> By0(string name_0) => _queryContext.All();
    public List<Entity_357> By1(string name_1) => _queryContext.All();
}

public class Entity_358
{
    readonly IEntityContext<Entity_358> _context = default!;

    protected Entity_358() { }

    public Entity_358(IEntityContext<Entity_358> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_358 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_358s
{
    readonly IQueryContext<Entity_358> _queryContext;

    public Entity_358s(IQueryContext<Entity_358> queryContext) => _queryContext = queryContext;

    public List<Entity_358> By0(string name_0) => _queryContext.All();
    public List<Entity_358> By1(string name_1) => _queryContext.All();
}

public class Entity_359
{
    readonly IEntityContext<Entity_359> _context = default!;

    protected Entity_359() { }

    public Entity_359(IEntityContext<Entity_359> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_359 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_359s
{
    readonly IQueryContext<Entity_359> _queryContext;

    public Entity_359s(IQueryContext<Entity_359> queryContext) => _queryContext = queryContext;

    public List<Entity_359> By0(string name_0) => _queryContext.All();
    public List<Entity_359> By1(string name_1) => _queryContext.All();
}

public class Entity_360
{
    readonly IEntityContext<Entity_360> _context = default!;

    protected Entity_360() { }

    public Entity_360(IEntityContext<Entity_360> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_360 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_360s
{
    readonly IQueryContext<Entity_360> _queryContext;

    public Entity_360s(IQueryContext<Entity_360> queryContext) => _queryContext = queryContext;

    public List<Entity_360> By0(string name_0) => _queryContext.All();
    public List<Entity_360> By1(string name_1) => _queryContext.All();
}

public class Entity_361
{
    readonly IEntityContext<Entity_361> _context = default!;

    protected Entity_361() { }

    public Entity_361(IEntityContext<Entity_361> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_361 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_361s
{
    readonly IQueryContext<Entity_361> _queryContext;

    public Entity_361s(IQueryContext<Entity_361> queryContext) => _queryContext = queryContext;

    public List<Entity_361> By0(string name_0) => _queryContext.All();
    public List<Entity_361> By1(string name_1) => _queryContext.All();
}

public class Entity_362
{
    readonly IEntityContext<Entity_362> _context = default!;

    protected Entity_362() { }

    public Entity_362(IEntityContext<Entity_362> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_362 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_362s
{
    readonly IQueryContext<Entity_362> _queryContext;

    public Entity_362s(IQueryContext<Entity_362> queryContext) => _queryContext = queryContext;

    public List<Entity_362> By0(string name_0) => _queryContext.All();
    public List<Entity_362> By1(string name_1) => _queryContext.All();
}

public class Entity_363
{
    readonly IEntityContext<Entity_363> _context = default!;

    protected Entity_363() { }

    public Entity_363(IEntityContext<Entity_363> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_363 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_363s
{
    readonly IQueryContext<Entity_363> _queryContext;

    public Entity_363s(IQueryContext<Entity_363> queryContext) => _queryContext = queryContext;

    public List<Entity_363> By0(string name_0) => _queryContext.All();
    public List<Entity_363> By1(string name_1) => _queryContext.All();
}

public class Entity_364
{
    readonly IEntityContext<Entity_364> _context = default!;

    protected Entity_364() { }

    public Entity_364(IEntityContext<Entity_364> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_364 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_364s
{
    readonly IQueryContext<Entity_364> _queryContext;

    public Entity_364s(IQueryContext<Entity_364> queryContext) => _queryContext = queryContext;

    public List<Entity_364> By0(string name_0) => _queryContext.All();
    public List<Entity_364> By1(string name_1) => _queryContext.All();
}

public class Entity_365
{
    readonly IEntityContext<Entity_365> _context = default!;

    protected Entity_365() { }

    public Entity_365(IEntityContext<Entity_365> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_365 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_365s
{
    readonly IQueryContext<Entity_365> _queryContext;

    public Entity_365s(IQueryContext<Entity_365> queryContext) => _queryContext = queryContext;

    public List<Entity_365> By0(string name_0) => _queryContext.All();
    public List<Entity_365> By1(string name_1) => _queryContext.All();
}

public class Entity_366
{
    readonly IEntityContext<Entity_366> _context = default!;

    protected Entity_366() { }

    public Entity_366(IEntityContext<Entity_366> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_366 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_366s
{
    readonly IQueryContext<Entity_366> _queryContext;

    public Entity_366s(IQueryContext<Entity_366> queryContext) => _queryContext = queryContext;

    public List<Entity_366> By0(string name_0) => _queryContext.All();
    public List<Entity_366> By1(string name_1) => _queryContext.All();
}

public class Entity_367
{
    readonly IEntityContext<Entity_367> _context = default!;

    protected Entity_367() { }

    public Entity_367(IEntityContext<Entity_367> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_367 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_367s
{
    readonly IQueryContext<Entity_367> _queryContext;

    public Entity_367s(IQueryContext<Entity_367> queryContext) => _queryContext = queryContext;

    public List<Entity_367> By0(string name_0) => _queryContext.All();
    public List<Entity_367> By1(string name_1) => _queryContext.All();
}

public class Entity_368
{
    readonly IEntityContext<Entity_368> _context = default!;

    protected Entity_368() { }

    public Entity_368(IEntityContext<Entity_368> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_368 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_368s
{
    readonly IQueryContext<Entity_368> _queryContext;

    public Entity_368s(IQueryContext<Entity_368> queryContext) => _queryContext = queryContext;

    public List<Entity_368> By0(string name_0) => _queryContext.All();
    public List<Entity_368> By1(string name_1) => _queryContext.All();
}

public class Entity_369
{
    readonly IEntityContext<Entity_369> _context = default!;

    protected Entity_369() { }

    public Entity_369(IEntityContext<Entity_369> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_369 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_369s
{
    readonly IQueryContext<Entity_369> _queryContext;

    public Entity_369s(IQueryContext<Entity_369> queryContext) => _queryContext = queryContext;

    public List<Entity_369> By0(string name_0) => _queryContext.All();
    public List<Entity_369> By1(string name_1) => _queryContext.All();
}

public class Entity_370
{
    readonly IEntityContext<Entity_370> _context = default!;

    protected Entity_370() { }

    public Entity_370(IEntityContext<Entity_370> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_370 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_370s
{
    readonly IQueryContext<Entity_370> _queryContext;

    public Entity_370s(IQueryContext<Entity_370> queryContext) => _queryContext = queryContext;

    public List<Entity_370> By0(string name_0) => _queryContext.All();
    public List<Entity_370> By1(string name_1) => _queryContext.All();
}

public class Entity_371
{
    readonly IEntityContext<Entity_371> _context = default!;

    protected Entity_371() { }

    public Entity_371(IEntityContext<Entity_371> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_371 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_371s
{
    readonly IQueryContext<Entity_371> _queryContext;

    public Entity_371s(IQueryContext<Entity_371> queryContext) => _queryContext = queryContext;

    public List<Entity_371> By0(string name_0) => _queryContext.All();
    public List<Entity_371> By1(string name_1) => _queryContext.All();
}

public class Entity_372
{
    readonly IEntityContext<Entity_372> _context = default!;

    protected Entity_372() { }

    public Entity_372(IEntityContext<Entity_372> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_372 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_372s
{
    readonly IQueryContext<Entity_372> _queryContext;

    public Entity_372s(IQueryContext<Entity_372> queryContext) => _queryContext = queryContext;

    public List<Entity_372> By0(string name_0) => _queryContext.All();
    public List<Entity_372> By1(string name_1) => _queryContext.All();
}

public class Entity_373
{
    readonly IEntityContext<Entity_373> _context = default!;

    protected Entity_373() { }

    public Entity_373(IEntityContext<Entity_373> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_373 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_373s
{
    readonly IQueryContext<Entity_373> _queryContext;

    public Entity_373s(IQueryContext<Entity_373> queryContext) => _queryContext = queryContext;

    public List<Entity_373> By0(string name_0) => _queryContext.All();
    public List<Entity_373> By1(string name_1) => _queryContext.All();
}

public class Entity_374
{
    readonly IEntityContext<Entity_374> _context = default!;

    protected Entity_374() { }

    public Entity_374(IEntityContext<Entity_374> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_374 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_374s
{
    readonly IQueryContext<Entity_374> _queryContext;

    public Entity_374s(IQueryContext<Entity_374> queryContext) => _queryContext = queryContext;

    public List<Entity_374> By0(string name_0) => _queryContext.All();
    public List<Entity_374> By1(string name_1) => _queryContext.All();
}

public class Entity_375
{
    readonly IEntityContext<Entity_375> _context = default!;

    protected Entity_375() { }

    public Entity_375(IEntityContext<Entity_375> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_375 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_375s
{
    readonly IQueryContext<Entity_375> _queryContext;

    public Entity_375s(IQueryContext<Entity_375> queryContext) => _queryContext = queryContext;

    public List<Entity_375> By0(string name_0) => _queryContext.All();
    public List<Entity_375> By1(string name_1) => _queryContext.All();
}

public class Entity_376
{
    readonly IEntityContext<Entity_376> _context = default!;

    protected Entity_376() { }

    public Entity_376(IEntityContext<Entity_376> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_376 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_376s
{
    readonly IQueryContext<Entity_376> _queryContext;

    public Entity_376s(IQueryContext<Entity_376> queryContext) => _queryContext = queryContext;

    public List<Entity_376> By0(string name_0) => _queryContext.All();
    public List<Entity_376> By1(string name_1) => _queryContext.All();
}

public class Entity_377
{
    readonly IEntityContext<Entity_377> _context = default!;

    protected Entity_377() { }

    public Entity_377(IEntityContext<Entity_377> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_377 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_377s
{
    readonly IQueryContext<Entity_377> _queryContext;

    public Entity_377s(IQueryContext<Entity_377> queryContext) => _queryContext = queryContext;

    public List<Entity_377> By0(string name_0) => _queryContext.All();
    public List<Entity_377> By1(string name_1) => _queryContext.All();
}

public class Entity_378
{
    readonly IEntityContext<Entity_378> _context = default!;

    protected Entity_378() { }

    public Entity_378(IEntityContext<Entity_378> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_378 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_378s
{
    readonly IQueryContext<Entity_378> _queryContext;

    public Entity_378s(IQueryContext<Entity_378> queryContext) => _queryContext = queryContext;

    public List<Entity_378> By0(string name_0) => _queryContext.All();
    public List<Entity_378> By1(string name_1) => _queryContext.All();
}

public class Entity_379
{
    readonly IEntityContext<Entity_379> _context = default!;

    protected Entity_379() { }

    public Entity_379(IEntityContext<Entity_379> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_379 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_379s
{
    readonly IQueryContext<Entity_379> _queryContext;

    public Entity_379s(IQueryContext<Entity_379> queryContext) => _queryContext = queryContext;

    public List<Entity_379> By0(string name_0) => _queryContext.All();
    public List<Entity_379> By1(string name_1) => _queryContext.All();
}

public class Entity_380
{
    readonly IEntityContext<Entity_380> _context = default!;

    protected Entity_380() { }

    public Entity_380(IEntityContext<Entity_380> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_380 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_380s
{
    readonly IQueryContext<Entity_380> _queryContext;

    public Entity_380s(IQueryContext<Entity_380> queryContext) => _queryContext = queryContext;

    public List<Entity_380> By0(string name_0) => _queryContext.All();
    public List<Entity_380> By1(string name_1) => _queryContext.All();
}

public class Entity_381
{
    readonly IEntityContext<Entity_381> _context = default!;

    protected Entity_381() { }

    public Entity_381(IEntityContext<Entity_381> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_381 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_381s
{
    readonly IQueryContext<Entity_381> _queryContext;

    public Entity_381s(IQueryContext<Entity_381> queryContext) => _queryContext = queryContext;

    public List<Entity_381> By0(string name_0) => _queryContext.All();
    public List<Entity_381> By1(string name_1) => _queryContext.All();
}

public class Entity_382
{
    readonly IEntityContext<Entity_382> _context = default!;

    protected Entity_382() { }

    public Entity_382(IEntityContext<Entity_382> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_382 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_382s
{
    readonly IQueryContext<Entity_382> _queryContext;

    public Entity_382s(IQueryContext<Entity_382> queryContext) => _queryContext = queryContext;

    public List<Entity_382> By0(string name_0) => _queryContext.All();
    public List<Entity_382> By1(string name_1) => _queryContext.All();
}

public class Entity_383
{
    readonly IEntityContext<Entity_383> _context = default!;

    protected Entity_383() { }

    public Entity_383(IEntityContext<Entity_383> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_383 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_383s
{
    readonly IQueryContext<Entity_383> _queryContext;

    public Entity_383s(IQueryContext<Entity_383> queryContext) => _queryContext = queryContext;

    public List<Entity_383> By0(string name_0) => _queryContext.All();
    public List<Entity_383> By1(string name_1) => _queryContext.All();
}

public class Entity_384
{
    readonly IEntityContext<Entity_384> _context = default!;

    protected Entity_384() { }

    public Entity_384(IEntityContext<Entity_384> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_384 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_384s
{
    readonly IQueryContext<Entity_384> _queryContext;

    public Entity_384s(IQueryContext<Entity_384> queryContext) => _queryContext = queryContext;

    public List<Entity_384> By0(string name_0) => _queryContext.All();
    public List<Entity_384> By1(string name_1) => _queryContext.All();
}

public class Entity_385
{
    readonly IEntityContext<Entity_385> _context = default!;

    protected Entity_385() { }

    public Entity_385(IEntityContext<Entity_385> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_385 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_385s
{
    readonly IQueryContext<Entity_385> _queryContext;

    public Entity_385s(IQueryContext<Entity_385> queryContext) => _queryContext = queryContext;

    public List<Entity_385> By0(string name_0) => _queryContext.All();
    public List<Entity_385> By1(string name_1) => _queryContext.All();
}

public class Entity_386
{
    readonly IEntityContext<Entity_386> _context = default!;

    protected Entity_386() { }

    public Entity_386(IEntityContext<Entity_386> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_386 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_386s
{
    readonly IQueryContext<Entity_386> _queryContext;

    public Entity_386s(IQueryContext<Entity_386> queryContext) => _queryContext = queryContext;

    public List<Entity_386> By0(string name_0) => _queryContext.All();
    public List<Entity_386> By1(string name_1) => _queryContext.All();
}

public class Entity_387
{
    readonly IEntityContext<Entity_387> _context = default!;

    protected Entity_387() { }

    public Entity_387(IEntityContext<Entity_387> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_387 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_387s
{
    readonly IQueryContext<Entity_387> _queryContext;

    public Entity_387s(IQueryContext<Entity_387> queryContext) => _queryContext = queryContext;

    public List<Entity_387> By0(string name_0) => _queryContext.All();
    public List<Entity_387> By1(string name_1) => _queryContext.All();
}

public class Entity_388
{
    readonly IEntityContext<Entity_388> _context = default!;

    protected Entity_388() { }

    public Entity_388(IEntityContext<Entity_388> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_388 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_388s
{
    readonly IQueryContext<Entity_388> _queryContext;

    public Entity_388s(IQueryContext<Entity_388> queryContext) => _queryContext = queryContext;

    public List<Entity_388> By0(string name_0) => _queryContext.All();
    public List<Entity_388> By1(string name_1) => _queryContext.All();
}

public class Entity_389
{
    readonly IEntityContext<Entity_389> _context = default!;

    protected Entity_389() { }

    public Entity_389(IEntityContext<Entity_389> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_389 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_389s
{
    readonly IQueryContext<Entity_389> _queryContext;

    public Entity_389s(IQueryContext<Entity_389> queryContext) => _queryContext = queryContext;

    public List<Entity_389> By0(string name_0) => _queryContext.All();
    public List<Entity_389> By1(string name_1) => _queryContext.All();
}

public class Entity_390
{
    readonly IEntityContext<Entity_390> _context = default!;

    protected Entity_390() { }

    public Entity_390(IEntityContext<Entity_390> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_390 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_390s
{
    readonly IQueryContext<Entity_390> _queryContext;

    public Entity_390s(IQueryContext<Entity_390> queryContext) => _queryContext = queryContext;

    public List<Entity_390> By0(string name_0) => _queryContext.All();
    public List<Entity_390> By1(string name_1) => _queryContext.All();
}

public class Entity_391
{
    readonly IEntityContext<Entity_391> _context = default!;

    protected Entity_391() { }

    public Entity_391(IEntityContext<Entity_391> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_391 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_391s
{
    readonly IQueryContext<Entity_391> _queryContext;

    public Entity_391s(IQueryContext<Entity_391> queryContext) => _queryContext = queryContext;

    public List<Entity_391> By0(string name_0) => _queryContext.All();
    public List<Entity_391> By1(string name_1) => _queryContext.All();
}

public class Entity_392
{
    readonly IEntityContext<Entity_392> _context = default!;

    protected Entity_392() { }

    public Entity_392(IEntityContext<Entity_392> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_392 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_392s
{
    readonly IQueryContext<Entity_392> _queryContext;

    public Entity_392s(IQueryContext<Entity_392> queryContext) => _queryContext = queryContext;

    public List<Entity_392> By0(string name_0) => _queryContext.All();
    public List<Entity_392> By1(string name_1) => _queryContext.All();
}

public class Entity_393
{
    readonly IEntityContext<Entity_393> _context = default!;

    protected Entity_393() { }

    public Entity_393(IEntityContext<Entity_393> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_393 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_393s
{
    readonly IQueryContext<Entity_393> _queryContext;

    public Entity_393s(IQueryContext<Entity_393> queryContext) => _queryContext = queryContext;

    public List<Entity_393> By0(string name_0) => _queryContext.All();
    public List<Entity_393> By1(string name_1) => _queryContext.All();
}

public class Entity_394
{
    readonly IEntityContext<Entity_394> _context = default!;

    protected Entity_394() { }

    public Entity_394(IEntityContext<Entity_394> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_394 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_394s
{
    readonly IQueryContext<Entity_394> _queryContext;

    public Entity_394s(IQueryContext<Entity_394> queryContext) => _queryContext = queryContext;

    public List<Entity_394> By0(string name_0) => _queryContext.All();
    public List<Entity_394> By1(string name_1) => _queryContext.All();
}

public class Entity_395
{
    readonly IEntityContext<Entity_395> _context = default!;

    protected Entity_395() { }

    public Entity_395(IEntityContext<Entity_395> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_395 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_395s
{
    readonly IQueryContext<Entity_395> _queryContext;

    public Entity_395s(IQueryContext<Entity_395> queryContext) => _queryContext = queryContext;

    public List<Entity_395> By0(string name_0) => _queryContext.All();
    public List<Entity_395> By1(string name_1) => _queryContext.All();
}

public class Entity_396
{
    readonly IEntityContext<Entity_396> _context = default!;

    protected Entity_396() { }

    public Entity_396(IEntityContext<Entity_396> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_396 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_396s
{
    readonly IQueryContext<Entity_396> _queryContext;

    public Entity_396s(IQueryContext<Entity_396> queryContext) => _queryContext = queryContext;

    public List<Entity_396> By0(string name_0) => _queryContext.All();
    public List<Entity_396> By1(string name_1) => _queryContext.All();
}

public class Entity_397
{
    readonly IEntityContext<Entity_397> _context = default!;

    protected Entity_397() { }

    public Entity_397(IEntityContext<Entity_397> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_397 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_397s
{
    readonly IQueryContext<Entity_397> _queryContext;

    public Entity_397s(IQueryContext<Entity_397> queryContext) => _queryContext = queryContext;

    public List<Entity_397> By0(string name_0) => _queryContext.All();
    public List<Entity_397> By1(string name_1) => _queryContext.All();
}

public class Entity_398
{
    readonly IEntityContext<Entity_398> _context = default!;

    protected Entity_398() { }

    public Entity_398(IEntityContext<Entity_398> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_398 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_398s
{
    readonly IQueryContext<Entity_398> _queryContext;

    public Entity_398s(IQueryContext<Entity_398> queryContext) => _queryContext = queryContext;

    public List<Entity_398> By0(string name_0) => _queryContext.All();
    public List<Entity_398> By1(string name_1) => _queryContext.All();
}

public class Entity_399
{
    readonly IEntityContext<Entity_399> _context = default!;

    protected Entity_399() { }

    public Entity_399(IEntityContext<Entity_399> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_399 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_399s
{
    readonly IQueryContext<Entity_399> _queryContext;

    public Entity_399s(IQueryContext<Entity_399> queryContext) => _queryContext = queryContext;

    public List<Entity_399> By0(string name_0) => _queryContext.All();
    public List<Entity_399> By1(string name_1) => _queryContext.All();
}

public class Entity_400
{
    readonly IEntityContext<Entity_400> _context = default!;

    protected Entity_400() { }

    public Entity_400(IEntityContext<Entity_400> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_400 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_400s
{
    readonly IQueryContext<Entity_400> _queryContext;

    public Entity_400s(IQueryContext<Entity_400> queryContext) => _queryContext = queryContext;

    public List<Entity_400> By0(string name_0) => _queryContext.All();
    public List<Entity_400> By1(string name_1) => _queryContext.All();
}

public class Entity_401
{
    readonly IEntityContext<Entity_401> _context = default!;

    protected Entity_401() { }

    public Entity_401(IEntityContext<Entity_401> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_401 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_401s
{
    readonly IQueryContext<Entity_401> _queryContext;

    public Entity_401s(IQueryContext<Entity_401> queryContext) => _queryContext = queryContext;

    public List<Entity_401> By0(string name_0) => _queryContext.All();
    public List<Entity_401> By1(string name_1) => _queryContext.All();
}

public class Entity_402
{
    readonly IEntityContext<Entity_402> _context = default!;

    protected Entity_402() { }

    public Entity_402(IEntityContext<Entity_402> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_402 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_402s
{
    readonly IQueryContext<Entity_402> _queryContext;

    public Entity_402s(IQueryContext<Entity_402> queryContext) => _queryContext = queryContext;

    public List<Entity_402> By0(string name_0) => _queryContext.All();
    public List<Entity_402> By1(string name_1) => _queryContext.All();
}

public class Entity_403
{
    readonly IEntityContext<Entity_403> _context = default!;

    protected Entity_403() { }

    public Entity_403(IEntityContext<Entity_403> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_403 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_403s
{
    readonly IQueryContext<Entity_403> _queryContext;

    public Entity_403s(IQueryContext<Entity_403> queryContext) => _queryContext = queryContext;

    public List<Entity_403> By0(string name_0) => _queryContext.All();
    public List<Entity_403> By1(string name_1) => _queryContext.All();
}

public class Entity_404
{
    readonly IEntityContext<Entity_404> _context = default!;

    protected Entity_404() { }

    public Entity_404(IEntityContext<Entity_404> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_404 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_404s
{
    readonly IQueryContext<Entity_404> _queryContext;

    public Entity_404s(IQueryContext<Entity_404> queryContext) => _queryContext = queryContext;

    public List<Entity_404> By0(string name_0) => _queryContext.All();
    public List<Entity_404> By1(string name_1) => _queryContext.All();
}

public class Entity_405
{
    readonly IEntityContext<Entity_405> _context = default!;

    protected Entity_405() { }

    public Entity_405(IEntityContext<Entity_405> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_405 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_405s
{
    readonly IQueryContext<Entity_405> _queryContext;

    public Entity_405s(IQueryContext<Entity_405> queryContext) => _queryContext = queryContext;

    public List<Entity_405> By0(string name_0) => _queryContext.All();
    public List<Entity_405> By1(string name_1) => _queryContext.All();
}

public class Entity_406
{
    readonly IEntityContext<Entity_406> _context = default!;

    protected Entity_406() { }

    public Entity_406(IEntityContext<Entity_406> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_406 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_406s
{
    readonly IQueryContext<Entity_406> _queryContext;

    public Entity_406s(IQueryContext<Entity_406> queryContext) => _queryContext = queryContext;

    public List<Entity_406> By0(string name_0) => _queryContext.All();
    public List<Entity_406> By1(string name_1) => _queryContext.All();
}

public class Entity_407
{
    readonly IEntityContext<Entity_407> _context = default!;

    protected Entity_407() { }

    public Entity_407(IEntityContext<Entity_407> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_407 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_407s
{
    readonly IQueryContext<Entity_407> _queryContext;

    public Entity_407s(IQueryContext<Entity_407> queryContext) => _queryContext = queryContext;

    public List<Entity_407> By0(string name_0) => _queryContext.All();
    public List<Entity_407> By1(string name_1) => _queryContext.All();
}

public class Entity_408
{
    readonly IEntityContext<Entity_408> _context = default!;

    protected Entity_408() { }

    public Entity_408(IEntityContext<Entity_408> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_408 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_408s
{
    readonly IQueryContext<Entity_408> _queryContext;

    public Entity_408s(IQueryContext<Entity_408> queryContext) => _queryContext = queryContext;

    public List<Entity_408> By0(string name_0) => _queryContext.All();
    public List<Entity_408> By1(string name_1) => _queryContext.All();
}

public class Entity_409
{
    readonly IEntityContext<Entity_409> _context = default!;

    protected Entity_409() { }

    public Entity_409(IEntityContext<Entity_409> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_409 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_409s
{
    readonly IQueryContext<Entity_409> _queryContext;

    public Entity_409s(IQueryContext<Entity_409> queryContext) => _queryContext = queryContext;

    public List<Entity_409> By0(string name_0) => _queryContext.All();
    public List<Entity_409> By1(string name_1) => _queryContext.All();
}

public class Entity_410
{
    readonly IEntityContext<Entity_410> _context = default!;

    protected Entity_410() { }

    public Entity_410(IEntityContext<Entity_410> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_410 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_410s
{
    readonly IQueryContext<Entity_410> _queryContext;

    public Entity_410s(IQueryContext<Entity_410> queryContext) => _queryContext = queryContext;

    public List<Entity_410> By0(string name_0) => _queryContext.All();
    public List<Entity_410> By1(string name_1) => _queryContext.All();
}

public class Entity_411
{
    readonly IEntityContext<Entity_411> _context = default!;

    protected Entity_411() { }

    public Entity_411(IEntityContext<Entity_411> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_411 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_411s
{
    readonly IQueryContext<Entity_411> _queryContext;

    public Entity_411s(IQueryContext<Entity_411> queryContext) => _queryContext = queryContext;

    public List<Entity_411> By0(string name_0) => _queryContext.All();
    public List<Entity_411> By1(string name_1) => _queryContext.All();
}

public class Entity_412
{
    readonly IEntityContext<Entity_412> _context = default!;

    protected Entity_412() { }

    public Entity_412(IEntityContext<Entity_412> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_412 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_412s
{
    readonly IQueryContext<Entity_412> _queryContext;

    public Entity_412s(IQueryContext<Entity_412> queryContext) => _queryContext = queryContext;

    public List<Entity_412> By0(string name_0) => _queryContext.All();
    public List<Entity_412> By1(string name_1) => _queryContext.All();
}

public class Entity_413
{
    readonly IEntityContext<Entity_413> _context = default!;

    protected Entity_413() { }

    public Entity_413(IEntityContext<Entity_413> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_413 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_413s
{
    readonly IQueryContext<Entity_413> _queryContext;

    public Entity_413s(IQueryContext<Entity_413> queryContext) => _queryContext = queryContext;

    public List<Entity_413> By0(string name_0) => _queryContext.All();
    public List<Entity_413> By1(string name_1) => _queryContext.All();
}

public class Entity_414
{
    readonly IEntityContext<Entity_414> _context = default!;

    protected Entity_414() { }

    public Entity_414(IEntityContext<Entity_414> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_414 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_414s
{
    readonly IQueryContext<Entity_414> _queryContext;

    public Entity_414s(IQueryContext<Entity_414> queryContext) => _queryContext = queryContext;

    public List<Entity_414> By0(string name_0) => _queryContext.All();
    public List<Entity_414> By1(string name_1) => _queryContext.All();
}

public class Entity_415
{
    readonly IEntityContext<Entity_415> _context = default!;

    protected Entity_415() { }

    public Entity_415(IEntityContext<Entity_415> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_415 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_415s
{
    readonly IQueryContext<Entity_415> _queryContext;

    public Entity_415s(IQueryContext<Entity_415> queryContext) => _queryContext = queryContext;

    public List<Entity_415> By0(string name_0) => _queryContext.All();
    public List<Entity_415> By1(string name_1) => _queryContext.All();
}

public class Entity_416
{
    readonly IEntityContext<Entity_416> _context = default!;

    protected Entity_416() { }

    public Entity_416(IEntityContext<Entity_416> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_416 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_416s
{
    readonly IQueryContext<Entity_416> _queryContext;

    public Entity_416s(IQueryContext<Entity_416> queryContext) => _queryContext = queryContext;

    public List<Entity_416> By0(string name_0) => _queryContext.All();
    public List<Entity_416> By1(string name_1) => _queryContext.All();
}

public class Entity_417
{
    readonly IEntityContext<Entity_417> _context = default!;

    protected Entity_417() { }

    public Entity_417(IEntityContext<Entity_417> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_417 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_417s
{
    readonly IQueryContext<Entity_417> _queryContext;

    public Entity_417s(IQueryContext<Entity_417> queryContext) => _queryContext = queryContext;

    public List<Entity_417> By0(string name_0) => _queryContext.All();
    public List<Entity_417> By1(string name_1) => _queryContext.All();
}

public class Entity_418
{
    readonly IEntityContext<Entity_418> _context = default!;

    protected Entity_418() { }

    public Entity_418(IEntityContext<Entity_418> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_418 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_418s
{
    readonly IQueryContext<Entity_418> _queryContext;

    public Entity_418s(IQueryContext<Entity_418> queryContext) => _queryContext = queryContext;

    public List<Entity_418> By0(string name_0) => _queryContext.All();
    public List<Entity_418> By1(string name_1) => _queryContext.All();
}

public class Entity_419
{
    readonly IEntityContext<Entity_419> _context = default!;

    protected Entity_419() { }

    public Entity_419(IEntityContext<Entity_419> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_419 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_419s
{
    readonly IQueryContext<Entity_419> _queryContext;

    public Entity_419s(IQueryContext<Entity_419> queryContext) => _queryContext = queryContext;

    public List<Entity_419> By0(string name_0) => _queryContext.All();
    public List<Entity_419> By1(string name_1) => _queryContext.All();
}

public class Entity_420
{
    readonly IEntityContext<Entity_420> _context = default!;

    protected Entity_420() { }

    public Entity_420(IEntityContext<Entity_420> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_420 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_420s
{
    readonly IQueryContext<Entity_420> _queryContext;

    public Entity_420s(IQueryContext<Entity_420> queryContext) => _queryContext = queryContext;

    public List<Entity_420> By0(string name_0) => _queryContext.All();
    public List<Entity_420> By1(string name_1) => _queryContext.All();
}

public class Entity_421
{
    readonly IEntityContext<Entity_421> _context = default!;

    protected Entity_421() { }

    public Entity_421(IEntityContext<Entity_421> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_421 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_421s
{
    readonly IQueryContext<Entity_421> _queryContext;

    public Entity_421s(IQueryContext<Entity_421> queryContext) => _queryContext = queryContext;

    public List<Entity_421> By0(string name_0) => _queryContext.All();
    public List<Entity_421> By1(string name_1) => _queryContext.All();
}

public class Entity_422
{
    readonly IEntityContext<Entity_422> _context = default!;

    protected Entity_422() { }

    public Entity_422(IEntityContext<Entity_422> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_422 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_422s
{
    readonly IQueryContext<Entity_422> _queryContext;

    public Entity_422s(IQueryContext<Entity_422> queryContext) => _queryContext = queryContext;

    public List<Entity_422> By0(string name_0) => _queryContext.All();
    public List<Entity_422> By1(string name_1) => _queryContext.All();
}

public class Entity_423
{
    readonly IEntityContext<Entity_423> _context = default!;

    protected Entity_423() { }

    public Entity_423(IEntityContext<Entity_423> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_423 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_423s
{
    readonly IQueryContext<Entity_423> _queryContext;

    public Entity_423s(IQueryContext<Entity_423> queryContext) => _queryContext = queryContext;

    public List<Entity_423> By0(string name_0) => _queryContext.All();
    public List<Entity_423> By1(string name_1) => _queryContext.All();
}

public class Entity_424
{
    readonly IEntityContext<Entity_424> _context = default!;

    protected Entity_424() { }

    public Entity_424(IEntityContext<Entity_424> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_424 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_424s
{
    readonly IQueryContext<Entity_424> _queryContext;

    public Entity_424s(IQueryContext<Entity_424> queryContext) => _queryContext = queryContext;

    public List<Entity_424> By0(string name_0) => _queryContext.All();
    public List<Entity_424> By1(string name_1) => _queryContext.All();
}

public class Entity_425
{
    readonly IEntityContext<Entity_425> _context = default!;

    protected Entity_425() { }

    public Entity_425(IEntityContext<Entity_425> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_425 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_425s
{
    readonly IQueryContext<Entity_425> _queryContext;

    public Entity_425s(IQueryContext<Entity_425> queryContext) => _queryContext = queryContext;

    public List<Entity_425> By0(string name_0) => _queryContext.All();
    public List<Entity_425> By1(string name_1) => _queryContext.All();
}

public class Entity_426
{
    readonly IEntityContext<Entity_426> _context = default!;

    protected Entity_426() { }

    public Entity_426(IEntityContext<Entity_426> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_426 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_426s
{
    readonly IQueryContext<Entity_426> _queryContext;

    public Entity_426s(IQueryContext<Entity_426> queryContext) => _queryContext = queryContext;

    public List<Entity_426> By0(string name_0) => _queryContext.All();
    public List<Entity_426> By1(string name_1) => _queryContext.All();
}

public class Entity_427
{
    readonly IEntityContext<Entity_427> _context = default!;

    protected Entity_427() { }

    public Entity_427(IEntityContext<Entity_427> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_427 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_427s
{
    readonly IQueryContext<Entity_427> _queryContext;

    public Entity_427s(IQueryContext<Entity_427> queryContext) => _queryContext = queryContext;

    public List<Entity_427> By0(string name_0) => _queryContext.All();
    public List<Entity_427> By1(string name_1) => _queryContext.All();
}

public class Entity_428
{
    readonly IEntityContext<Entity_428> _context = default!;

    protected Entity_428() { }

    public Entity_428(IEntityContext<Entity_428> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_428 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_428s
{
    readonly IQueryContext<Entity_428> _queryContext;

    public Entity_428s(IQueryContext<Entity_428> queryContext) => _queryContext = queryContext;

    public List<Entity_428> By0(string name_0) => _queryContext.All();
    public List<Entity_428> By1(string name_1) => _queryContext.All();
}

public class Entity_429
{
    readonly IEntityContext<Entity_429> _context = default!;

    protected Entity_429() { }

    public Entity_429(IEntityContext<Entity_429> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_429 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_429s
{
    readonly IQueryContext<Entity_429> _queryContext;

    public Entity_429s(IQueryContext<Entity_429> queryContext) => _queryContext = queryContext;

    public List<Entity_429> By0(string name_0) => _queryContext.All();
    public List<Entity_429> By1(string name_1) => _queryContext.All();
}

public class Entity_430
{
    readonly IEntityContext<Entity_430> _context = default!;

    protected Entity_430() { }

    public Entity_430(IEntityContext<Entity_430> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_430 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_430s
{
    readonly IQueryContext<Entity_430> _queryContext;

    public Entity_430s(IQueryContext<Entity_430> queryContext) => _queryContext = queryContext;

    public List<Entity_430> By0(string name_0) => _queryContext.All();
    public List<Entity_430> By1(string name_1) => _queryContext.All();
}

public class Entity_431
{
    readonly IEntityContext<Entity_431> _context = default!;

    protected Entity_431() { }

    public Entity_431(IEntityContext<Entity_431> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_431 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_431s
{
    readonly IQueryContext<Entity_431> _queryContext;

    public Entity_431s(IQueryContext<Entity_431> queryContext) => _queryContext = queryContext;

    public List<Entity_431> By0(string name_0) => _queryContext.All();
    public List<Entity_431> By1(string name_1) => _queryContext.All();
}

public class Entity_432
{
    readonly IEntityContext<Entity_432> _context = default!;

    protected Entity_432() { }

    public Entity_432(IEntityContext<Entity_432> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_432 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_432s
{
    readonly IQueryContext<Entity_432> _queryContext;

    public Entity_432s(IQueryContext<Entity_432> queryContext) => _queryContext = queryContext;

    public List<Entity_432> By0(string name_0) => _queryContext.All();
    public List<Entity_432> By1(string name_1) => _queryContext.All();
}

public class Entity_433
{
    readonly IEntityContext<Entity_433> _context = default!;

    protected Entity_433() { }

    public Entity_433(IEntityContext<Entity_433> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_433 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_433s
{
    readonly IQueryContext<Entity_433> _queryContext;

    public Entity_433s(IQueryContext<Entity_433> queryContext) => _queryContext = queryContext;

    public List<Entity_433> By0(string name_0) => _queryContext.All();
    public List<Entity_433> By1(string name_1) => _queryContext.All();
}

public class Entity_434
{
    readonly IEntityContext<Entity_434> _context = default!;

    protected Entity_434() { }

    public Entity_434(IEntityContext<Entity_434> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_434 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_434s
{
    readonly IQueryContext<Entity_434> _queryContext;

    public Entity_434s(IQueryContext<Entity_434> queryContext) => _queryContext = queryContext;

    public List<Entity_434> By0(string name_0) => _queryContext.All();
    public List<Entity_434> By1(string name_1) => _queryContext.All();
}

public class Entity_435
{
    readonly IEntityContext<Entity_435> _context = default!;

    protected Entity_435() { }

    public Entity_435(IEntityContext<Entity_435> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_435 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_435s
{
    readonly IQueryContext<Entity_435> _queryContext;

    public Entity_435s(IQueryContext<Entity_435> queryContext) => _queryContext = queryContext;

    public List<Entity_435> By0(string name_0) => _queryContext.All();
    public List<Entity_435> By1(string name_1) => _queryContext.All();
}

public class Entity_436
{
    readonly IEntityContext<Entity_436> _context = default!;

    protected Entity_436() { }

    public Entity_436(IEntityContext<Entity_436> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_436 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_436s
{
    readonly IQueryContext<Entity_436> _queryContext;

    public Entity_436s(IQueryContext<Entity_436> queryContext) => _queryContext = queryContext;

    public List<Entity_436> By0(string name_0) => _queryContext.All();
    public List<Entity_436> By1(string name_1) => _queryContext.All();
}

public class Entity_437
{
    readonly IEntityContext<Entity_437> _context = default!;

    protected Entity_437() { }

    public Entity_437(IEntityContext<Entity_437> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_437 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_437s
{
    readonly IQueryContext<Entity_437> _queryContext;

    public Entity_437s(IQueryContext<Entity_437> queryContext) => _queryContext = queryContext;

    public List<Entity_437> By0(string name_0) => _queryContext.All();
    public List<Entity_437> By1(string name_1) => _queryContext.All();
}

public class Entity_438
{
    readonly IEntityContext<Entity_438> _context = default!;

    protected Entity_438() { }

    public Entity_438(IEntityContext<Entity_438> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_438 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_438s
{
    readonly IQueryContext<Entity_438> _queryContext;

    public Entity_438s(IQueryContext<Entity_438> queryContext) => _queryContext = queryContext;

    public List<Entity_438> By0(string name_0) => _queryContext.All();
    public List<Entity_438> By1(string name_1) => _queryContext.All();
}

public class Entity_439
{
    readonly IEntityContext<Entity_439> _context = default!;

    protected Entity_439() { }

    public Entity_439(IEntityContext<Entity_439> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_439 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_439s
{
    readonly IQueryContext<Entity_439> _queryContext;

    public Entity_439s(IQueryContext<Entity_439> queryContext) => _queryContext = queryContext;

    public List<Entity_439> By0(string name_0) => _queryContext.All();
    public List<Entity_439> By1(string name_1) => _queryContext.All();
}

public class Entity_440
{
    readonly IEntityContext<Entity_440> _context = default!;

    protected Entity_440() { }

    public Entity_440(IEntityContext<Entity_440> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_440 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_440s
{
    readonly IQueryContext<Entity_440> _queryContext;

    public Entity_440s(IQueryContext<Entity_440> queryContext) => _queryContext = queryContext;

    public List<Entity_440> By0(string name_0) => _queryContext.All();
    public List<Entity_440> By1(string name_1) => _queryContext.All();
}

public class Entity_441
{
    readonly IEntityContext<Entity_441> _context = default!;

    protected Entity_441() { }

    public Entity_441(IEntityContext<Entity_441> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_441 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_441s
{
    readonly IQueryContext<Entity_441> _queryContext;

    public Entity_441s(IQueryContext<Entity_441> queryContext) => _queryContext = queryContext;

    public List<Entity_441> By0(string name_0) => _queryContext.All();
    public List<Entity_441> By1(string name_1) => _queryContext.All();
}

public class Entity_442
{
    readonly IEntityContext<Entity_442> _context = default!;

    protected Entity_442() { }

    public Entity_442(IEntityContext<Entity_442> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_442 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_442s
{
    readonly IQueryContext<Entity_442> _queryContext;

    public Entity_442s(IQueryContext<Entity_442> queryContext) => _queryContext = queryContext;

    public List<Entity_442> By0(string name_0) => _queryContext.All();
    public List<Entity_442> By1(string name_1) => _queryContext.All();
}

public class Entity_443
{
    readonly IEntityContext<Entity_443> _context = default!;

    protected Entity_443() { }

    public Entity_443(IEntityContext<Entity_443> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_443 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_443s
{
    readonly IQueryContext<Entity_443> _queryContext;

    public Entity_443s(IQueryContext<Entity_443> queryContext) => _queryContext = queryContext;

    public List<Entity_443> By0(string name_0) => _queryContext.All();
    public List<Entity_443> By1(string name_1) => _queryContext.All();
}

public class Entity_444
{
    readonly IEntityContext<Entity_444> _context = default!;

    protected Entity_444() { }

    public Entity_444(IEntityContext<Entity_444> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_444 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_444s
{
    readonly IQueryContext<Entity_444> _queryContext;

    public Entity_444s(IQueryContext<Entity_444> queryContext) => _queryContext = queryContext;

    public List<Entity_444> By0(string name_0) => _queryContext.All();
    public List<Entity_444> By1(string name_1) => _queryContext.All();
}

public class Entity_445
{
    readonly IEntityContext<Entity_445> _context = default!;

    protected Entity_445() { }

    public Entity_445(IEntityContext<Entity_445> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_445 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_445s
{
    readonly IQueryContext<Entity_445> _queryContext;

    public Entity_445s(IQueryContext<Entity_445> queryContext) => _queryContext = queryContext;

    public List<Entity_445> By0(string name_0) => _queryContext.All();
    public List<Entity_445> By1(string name_1) => _queryContext.All();
}

public class Entity_446
{
    readonly IEntityContext<Entity_446> _context = default!;

    protected Entity_446() { }

    public Entity_446(IEntityContext<Entity_446> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_446 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_446s
{
    readonly IQueryContext<Entity_446> _queryContext;

    public Entity_446s(IQueryContext<Entity_446> queryContext) => _queryContext = queryContext;

    public List<Entity_446> By0(string name_0) => _queryContext.All();
    public List<Entity_446> By1(string name_1) => _queryContext.All();
}

public class Entity_447
{
    readonly IEntityContext<Entity_447> _context = default!;

    protected Entity_447() { }

    public Entity_447(IEntityContext<Entity_447> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_447 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_447s
{
    readonly IQueryContext<Entity_447> _queryContext;

    public Entity_447s(IQueryContext<Entity_447> queryContext) => _queryContext = queryContext;

    public List<Entity_447> By0(string name_0) => _queryContext.All();
    public List<Entity_447> By1(string name_1) => _queryContext.All();
}

public class Entity_448
{
    readonly IEntityContext<Entity_448> _context = default!;

    protected Entity_448() { }

    public Entity_448(IEntityContext<Entity_448> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_448 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_448s
{
    readonly IQueryContext<Entity_448> _queryContext;

    public Entity_448s(IQueryContext<Entity_448> queryContext) => _queryContext = queryContext;

    public List<Entity_448> By0(string name_0) => _queryContext.All();
    public List<Entity_448> By1(string name_1) => _queryContext.All();
}

public class Entity_449
{
    readonly IEntityContext<Entity_449> _context = default!;

    protected Entity_449() { }

    public Entity_449(IEntityContext<Entity_449> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_449 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_449s
{
    readonly IQueryContext<Entity_449> _queryContext;

    public Entity_449s(IQueryContext<Entity_449> queryContext) => _queryContext = queryContext;

    public List<Entity_449> By0(string name_0) => _queryContext.All();
    public List<Entity_449> By1(string name_1) => _queryContext.All();
}

public class Entity_450
{
    readonly IEntityContext<Entity_450> _context = default!;

    protected Entity_450() { }

    public Entity_450(IEntityContext<Entity_450> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_450 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_450s
{
    readonly IQueryContext<Entity_450> _queryContext;

    public Entity_450s(IQueryContext<Entity_450> queryContext) => _queryContext = queryContext;

    public List<Entity_450> By0(string name_0) => _queryContext.All();
    public List<Entity_450> By1(string name_1) => _queryContext.All();
}

public class Entity_451
{
    readonly IEntityContext<Entity_451> _context = default!;

    protected Entity_451() { }

    public Entity_451(IEntityContext<Entity_451> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_451 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_451s
{
    readonly IQueryContext<Entity_451> _queryContext;

    public Entity_451s(IQueryContext<Entity_451> queryContext) => _queryContext = queryContext;

    public List<Entity_451> By0(string name_0) => _queryContext.All();
    public List<Entity_451> By1(string name_1) => _queryContext.All();
}

public class Entity_452
{
    readonly IEntityContext<Entity_452> _context = default!;

    protected Entity_452() { }

    public Entity_452(IEntityContext<Entity_452> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_452 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_452s
{
    readonly IQueryContext<Entity_452> _queryContext;

    public Entity_452s(IQueryContext<Entity_452> queryContext) => _queryContext = queryContext;

    public List<Entity_452> By0(string name_0) => _queryContext.All();
    public List<Entity_452> By1(string name_1) => _queryContext.All();
}

public class Entity_453
{
    readonly IEntityContext<Entity_453> _context = default!;

    protected Entity_453() { }

    public Entity_453(IEntityContext<Entity_453> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_453 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_453s
{
    readonly IQueryContext<Entity_453> _queryContext;

    public Entity_453s(IQueryContext<Entity_453> queryContext) => _queryContext = queryContext;

    public List<Entity_453> By0(string name_0) => _queryContext.All();
    public List<Entity_453> By1(string name_1) => _queryContext.All();
}

public class Entity_454
{
    readonly IEntityContext<Entity_454> _context = default!;

    protected Entity_454() { }

    public Entity_454(IEntityContext<Entity_454> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_454 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_454s
{
    readonly IQueryContext<Entity_454> _queryContext;

    public Entity_454s(IQueryContext<Entity_454> queryContext) => _queryContext = queryContext;

    public List<Entity_454> By0(string name_0) => _queryContext.All();
    public List<Entity_454> By1(string name_1) => _queryContext.All();
}

public class Entity_455
{
    readonly IEntityContext<Entity_455> _context = default!;

    protected Entity_455() { }

    public Entity_455(IEntityContext<Entity_455> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_455 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_455s
{
    readonly IQueryContext<Entity_455> _queryContext;

    public Entity_455s(IQueryContext<Entity_455> queryContext) => _queryContext = queryContext;

    public List<Entity_455> By0(string name_0) => _queryContext.All();
    public List<Entity_455> By1(string name_1) => _queryContext.All();
}

public class Entity_456
{
    readonly IEntityContext<Entity_456> _context = default!;

    protected Entity_456() { }

    public Entity_456(IEntityContext<Entity_456> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_456 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_456s
{
    readonly IQueryContext<Entity_456> _queryContext;

    public Entity_456s(IQueryContext<Entity_456> queryContext) => _queryContext = queryContext;

    public List<Entity_456> By0(string name_0) => _queryContext.All();
    public List<Entity_456> By1(string name_1) => _queryContext.All();
}

public class Entity_457
{
    readonly IEntityContext<Entity_457> _context = default!;

    protected Entity_457() { }

    public Entity_457(IEntityContext<Entity_457> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_457 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_457s
{
    readonly IQueryContext<Entity_457> _queryContext;

    public Entity_457s(IQueryContext<Entity_457> queryContext) => _queryContext = queryContext;

    public List<Entity_457> By0(string name_0) => _queryContext.All();
    public List<Entity_457> By1(string name_1) => _queryContext.All();
}

public class Entity_458
{
    readonly IEntityContext<Entity_458> _context = default!;

    protected Entity_458() { }

    public Entity_458(IEntityContext<Entity_458> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_458 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_458s
{
    readonly IQueryContext<Entity_458> _queryContext;

    public Entity_458s(IQueryContext<Entity_458> queryContext) => _queryContext = queryContext;

    public List<Entity_458> By0(string name_0) => _queryContext.All();
    public List<Entity_458> By1(string name_1) => _queryContext.All();
}

public class Entity_459
{
    readonly IEntityContext<Entity_459> _context = default!;

    protected Entity_459() { }

    public Entity_459(IEntityContext<Entity_459> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_459 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_459s
{
    readonly IQueryContext<Entity_459> _queryContext;

    public Entity_459s(IQueryContext<Entity_459> queryContext) => _queryContext = queryContext;

    public List<Entity_459> By0(string name_0) => _queryContext.All();
    public List<Entity_459> By1(string name_1) => _queryContext.All();
}

public class Entity_460
{
    readonly IEntityContext<Entity_460> _context = default!;

    protected Entity_460() { }

    public Entity_460(IEntityContext<Entity_460> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_460 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_460s
{
    readonly IQueryContext<Entity_460> _queryContext;

    public Entity_460s(IQueryContext<Entity_460> queryContext) => _queryContext = queryContext;

    public List<Entity_460> By0(string name_0) => _queryContext.All();
    public List<Entity_460> By1(string name_1) => _queryContext.All();
}

public class Entity_461
{
    readonly IEntityContext<Entity_461> _context = default!;

    protected Entity_461() { }

    public Entity_461(IEntityContext<Entity_461> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_461 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_461s
{
    readonly IQueryContext<Entity_461> _queryContext;

    public Entity_461s(IQueryContext<Entity_461> queryContext) => _queryContext = queryContext;

    public List<Entity_461> By0(string name_0) => _queryContext.All();
    public List<Entity_461> By1(string name_1) => _queryContext.All();
}

public class Entity_462
{
    readonly IEntityContext<Entity_462> _context = default!;

    protected Entity_462() { }

    public Entity_462(IEntityContext<Entity_462> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_462 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_462s
{
    readonly IQueryContext<Entity_462> _queryContext;

    public Entity_462s(IQueryContext<Entity_462> queryContext) => _queryContext = queryContext;

    public List<Entity_462> By0(string name_0) => _queryContext.All();
    public List<Entity_462> By1(string name_1) => _queryContext.All();
}

public class Entity_463
{
    readonly IEntityContext<Entity_463> _context = default!;

    protected Entity_463() { }

    public Entity_463(IEntityContext<Entity_463> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_463 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_463s
{
    readonly IQueryContext<Entity_463> _queryContext;

    public Entity_463s(IQueryContext<Entity_463> queryContext) => _queryContext = queryContext;

    public List<Entity_463> By0(string name_0) => _queryContext.All();
    public List<Entity_463> By1(string name_1) => _queryContext.All();
}

public class Entity_464
{
    readonly IEntityContext<Entity_464> _context = default!;

    protected Entity_464() { }

    public Entity_464(IEntityContext<Entity_464> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_464 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_464s
{
    readonly IQueryContext<Entity_464> _queryContext;

    public Entity_464s(IQueryContext<Entity_464> queryContext) => _queryContext = queryContext;

    public List<Entity_464> By0(string name_0) => _queryContext.All();
    public List<Entity_464> By1(string name_1) => _queryContext.All();
}

public class Entity_465
{
    readonly IEntityContext<Entity_465> _context = default!;

    protected Entity_465() { }

    public Entity_465(IEntityContext<Entity_465> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_465 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_465s
{
    readonly IQueryContext<Entity_465> _queryContext;

    public Entity_465s(IQueryContext<Entity_465> queryContext) => _queryContext = queryContext;

    public List<Entity_465> By0(string name_0) => _queryContext.All();
    public List<Entity_465> By1(string name_1) => _queryContext.All();
}

public class Entity_466
{
    readonly IEntityContext<Entity_466> _context = default!;

    protected Entity_466() { }

    public Entity_466(IEntityContext<Entity_466> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_466 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_466s
{
    readonly IQueryContext<Entity_466> _queryContext;

    public Entity_466s(IQueryContext<Entity_466> queryContext) => _queryContext = queryContext;

    public List<Entity_466> By0(string name_0) => _queryContext.All();
    public List<Entity_466> By1(string name_1) => _queryContext.All();
}

public class Entity_467
{
    readonly IEntityContext<Entity_467> _context = default!;

    protected Entity_467() { }

    public Entity_467(IEntityContext<Entity_467> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_467 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_467s
{
    readonly IQueryContext<Entity_467> _queryContext;

    public Entity_467s(IQueryContext<Entity_467> queryContext) => _queryContext = queryContext;

    public List<Entity_467> By0(string name_0) => _queryContext.All();
    public List<Entity_467> By1(string name_1) => _queryContext.All();
}

public class Entity_468
{
    readonly IEntityContext<Entity_468> _context = default!;

    protected Entity_468() { }

    public Entity_468(IEntityContext<Entity_468> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_468 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_468s
{
    readonly IQueryContext<Entity_468> _queryContext;

    public Entity_468s(IQueryContext<Entity_468> queryContext) => _queryContext = queryContext;

    public List<Entity_468> By0(string name_0) => _queryContext.All();
    public List<Entity_468> By1(string name_1) => _queryContext.All();
}

public class Entity_469
{
    readonly IEntityContext<Entity_469> _context = default!;

    protected Entity_469() { }

    public Entity_469(IEntityContext<Entity_469> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_469 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_469s
{
    readonly IQueryContext<Entity_469> _queryContext;

    public Entity_469s(IQueryContext<Entity_469> queryContext) => _queryContext = queryContext;

    public List<Entity_469> By0(string name_0) => _queryContext.All();
    public List<Entity_469> By1(string name_1) => _queryContext.All();
}

public class Entity_470
{
    readonly IEntityContext<Entity_470> _context = default!;

    protected Entity_470() { }

    public Entity_470(IEntityContext<Entity_470> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_470 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_470s
{
    readonly IQueryContext<Entity_470> _queryContext;

    public Entity_470s(IQueryContext<Entity_470> queryContext) => _queryContext = queryContext;

    public List<Entity_470> By0(string name_0) => _queryContext.All();
    public List<Entity_470> By1(string name_1) => _queryContext.All();
}

public class Entity_471
{
    readonly IEntityContext<Entity_471> _context = default!;

    protected Entity_471() { }

    public Entity_471(IEntityContext<Entity_471> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_471 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_471s
{
    readonly IQueryContext<Entity_471> _queryContext;

    public Entity_471s(IQueryContext<Entity_471> queryContext) => _queryContext = queryContext;

    public List<Entity_471> By0(string name_0) => _queryContext.All();
    public List<Entity_471> By1(string name_1) => _queryContext.All();
}

public class Entity_472
{
    readonly IEntityContext<Entity_472> _context = default!;

    protected Entity_472() { }

    public Entity_472(IEntityContext<Entity_472> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_472 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_472s
{
    readonly IQueryContext<Entity_472> _queryContext;

    public Entity_472s(IQueryContext<Entity_472> queryContext) => _queryContext = queryContext;

    public List<Entity_472> By0(string name_0) => _queryContext.All();
    public List<Entity_472> By1(string name_1) => _queryContext.All();
}

public class Entity_473
{
    readonly IEntityContext<Entity_473> _context = default!;

    protected Entity_473() { }

    public Entity_473(IEntityContext<Entity_473> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_473 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_473s
{
    readonly IQueryContext<Entity_473> _queryContext;

    public Entity_473s(IQueryContext<Entity_473> queryContext) => _queryContext = queryContext;

    public List<Entity_473> By0(string name_0) => _queryContext.All();
    public List<Entity_473> By1(string name_1) => _queryContext.All();
}

public class Entity_474
{
    readonly IEntityContext<Entity_474> _context = default!;

    protected Entity_474() { }

    public Entity_474(IEntityContext<Entity_474> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_474 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_474s
{
    readonly IQueryContext<Entity_474> _queryContext;

    public Entity_474s(IQueryContext<Entity_474> queryContext) => _queryContext = queryContext;

    public List<Entity_474> By0(string name_0) => _queryContext.All();
    public List<Entity_474> By1(string name_1) => _queryContext.All();
}

public class Entity_475
{
    readonly IEntityContext<Entity_475> _context = default!;

    protected Entity_475() { }

    public Entity_475(IEntityContext<Entity_475> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_475 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_475s
{
    readonly IQueryContext<Entity_475> _queryContext;

    public Entity_475s(IQueryContext<Entity_475> queryContext) => _queryContext = queryContext;

    public List<Entity_475> By0(string name_0) => _queryContext.All();
    public List<Entity_475> By1(string name_1) => _queryContext.All();
}

public class Entity_476
{
    readonly IEntityContext<Entity_476> _context = default!;

    protected Entity_476() { }

    public Entity_476(IEntityContext<Entity_476> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_476 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_476s
{
    readonly IQueryContext<Entity_476> _queryContext;

    public Entity_476s(IQueryContext<Entity_476> queryContext) => _queryContext = queryContext;

    public List<Entity_476> By0(string name_0) => _queryContext.All();
    public List<Entity_476> By1(string name_1) => _queryContext.All();
}

public class Entity_477
{
    readonly IEntityContext<Entity_477> _context = default!;

    protected Entity_477() { }

    public Entity_477(IEntityContext<Entity_477> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_477 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_477s
{
    readonly IQueryContext<Entity_477> _queryContext;

    public Entity_477s(IQueryContext<Entity_477> queryContext) => _queryContext = queryContext;

    public List<Entity_477> By0(string name_0) => _queryContext.All();
    public List<Entity_477> By1(string name_1) => _queryContext.All();
}

public class Entity_478
{
    readonly IEntityContext<Entity_478> _context = default!;

    protected Entity_478() { }

    public Entity_478(IEntityContext<Entity_478> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_478 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_478s
{
    readonly IQueryContext<Entity_478> _queryContext;

    public Entity_478s(IQueryContext<Entity_478> queryContext) => _queryContext = queryContext;

    public List<Entity_478> By0(string name_0) => _queryContext.All();
    public List<Entity_478> By1(string name_1) => _queryContext.All();
}

public class Entity_479
{
    readonly IEntityContext<Entity_479> _context = default!;

    protected Entity_479() { }

    public Entity_479(IEntityContext<Entity_479> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_479 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_479s
{
    readonly IQueryContext<Entity_479> _queryContext;

    public Entity_479s(IQueryContext<Entity_479> queryContext) => _queryContext = queryContext;

    public List<Entity_479> By0(string name_0) => _queryContext.All();
    public List<Entity_479> By1(string name_1) => _queryContext.All();
}

public class Entity_480
{
    readonly IEntityContext<Entity_480> _context = default!;

    protected Entity_480() { }

    public Entity_480(IEntityContext<Entity_480> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_480 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_480s
{
    readonly IQueryContext<Entity_480> _queryContext;

    public Entity_480s(IQueryContext<Entity_480> queryContext) => _queryContext = queryContext;

    public List<Entity_480> By0(string name_0) => _queryContext.All();
    public List<Entity_480> By1(string name_1) => _queryContext.All();
}

public class Entity_481
{
    readonly IEntityContext<Entity_481> _context = default!;

    protected Entity_481() { }

    public Entity_481(IEntityContext<Entity_481> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_481 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_481s
{
    readonly IQueryContext<Entity_481> _queryContext;

    public Entity_481s(IQueryContext<Entity_481> queryContext) => _queryContext = queryContext;

    public List<Entity_481> By0(string name_0) => _queryContext.All();
    public List<Entity_481> By1(string name_1) => _queryContext.All();
}

public class Entity_482
{
    readonly IEntityContext<Entity_482> _context = default!;

    protected Entity_482() { }

    public Entity_482(IEntityContext<Entity_482> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_482 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_482s
{
    readonly IQueryContext<Entity_482> _queryContext;

    public Entity_482s(IQueryContext<Entity_482> queryContext) => _queryContext = queryContext;

    public List<Entity_482> By0(string name_0) => _queryContext.All();
    public List<Entity_482> By1(string name_1) => _queryContext.All();
}

public class Entity_483
{
    readonly IEntityContext<Entity_483> _context = default!;

    protected Entity_483() { }

    public Entity_483(IEntityContext<Entity_483> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_483 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_483s
{
    readonly IQueryContext<Entity_483> _queryContext;

    public Entity_483s(IQueryContext<Entity_483> queryContext) => _queryContext = queryContext;

    public List<Entity_483> By0(string name_0) => _queryContext.All();
    public List<Entity_483> By1(string name_1) => _queryContext.All();
}

public class Entity_484
{
    readonly IEntityContext<Entity_484> _context = default!;

    protected Entity_484() { }

    public Entity_484(IEntityContext<Entity_484> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_484 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_484s
{
    readonly IQueryContext<Entity_484> _queryContext;

    public Entity_484s(IQueryContext<Entity_484> queryContext) => _queryContext = queryContext;

    public List<Entity_484> By0(string name_0) => _queryContext.All();
    public List<Entity_484> By1(string name_1) => _queryContext.All();
}

public class Entity_485
{
    readonly IEntityContext<Entity_485> _context = default!;

    protected Entity_485() { }

    public Entity_485(IEntityContext<Entity_485> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_485 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_485s
{
    readonly IQueryContext<Entity_485> _queryContext;

    public Entity_485s(IQueryContext<Entity_485> queryContext) => _queryContext = queryContext;

    public List<Entity_485> By0(string name_0) => _queryContext.All();
    public List<Entity_485> By1(string name_1) => _queryContext.All();
}

public class Entity_486
{
    readonly IEntityContext<Entity_486> _context = default!;

    protected Entity_486() { }

    public Entity_486(IEntityContext<Entity_486> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_486 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_486s
{
    readonly IQueryContext<Entity_486> _queryContext;

    public Entity_486s(IQueryContext<Entity_486> queryContext) => _queryContext = queryContext;

    public List<Entity_486> By0(string name_0) => _queryContext.All();
    public List<Entity_486> By1(string name_1) => _queryContext.All();
}

public class Entity_487
{
    readonly IEntityContext<Entity_487> _context = default!;

    protected Entity_487() { }

    public Entity_487(IEntityContext<Entity_487> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_487 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_487s
{
    readonly IQueryContext<Entity_487> _queryContext;

    public Entity_487s(IQueryContext<Entity_487> queryContext) => _queryContext = queryContext;

    public List<Entity_487> By0(string name_0) => _queryContext.All();
    public List<Entity_487> By1(string name_1) => _queryContext.All();
}

public class Entity_488
{
    readonly IEntityContext<Entity_488> _context = default!;

    protected Entity_488() { }

    public Entity_488(IEntityContext<Entity_488> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_488 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_488s
{
    readonly IQueryContext<Entity_488> _queryContext;

    public Entity_488s(IQueryContext<Entity_488> queryContext) => _queryContext = queryContext;

    public List<Entity_488> By0(string name_0) => _queryContext.All();
    public List<Entity_488> By1(string name_1) => _queryContext.All();
}

public class Entity_489
{
    readonly IEntityContext<Entity_489> _context = default!;

    protected Entity_489() { }

    public Entity_489(IEntityContext<Entity_489> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_489 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_489s
{
    readonly IQueryContext<Entity_489> _queryContext;

    public Entity_489s(IQueryContext<Entity_489> queryContext) => _queryContext = queryContext;

    public List<Entity_489> By0(string name_0) => _queryContext.All();
    public List<Entity_489> By1(string name_1) => _queryContext.All();
}

public class Entity_490
{
    readonly IEntityContext<Entity_490> _context = default!;

    protected Entity_490() { }

    public Entity_490(IEntityContext<Entity_490> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_490 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_490s
{
    readonly IQueryContext<Entity_490> _queryContext;

    public Entity_490s(IQueryContext<Entity_490> queryContext) => _queryContext = queryContext;

    public List<Entity_490> By0(string name_0) => _queryContext.All();
    public List<Entity_490> By1(string name_1) => _queryContext.All();
}

public class Entity_491
{
    readonly IEntityContext<Entity_491> _context = default!;

    protected Entity_491() { }

    public Entity_491(IEntityContext<Entity_491> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_491 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_491s
{
    readonly IQueryContext<Entity_491> _queryContext;

    public Entity_491s(IQueryContext<Entity_491> queryContext) => _queryContext = queryContext;

    public List<Entity_491> By0(string name_0) => _queryContext.All();
    public List<Entity_491> By1(string name_1) => _queryContext.All();
}

public class Entity_492
{
    readonly IEntityContext<Entity_492> _context = default!;

    protected Entity_492() { }

    public Entity_492(IEntityContext<Entity_492> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_492 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_492s
{
    readonly IQueryContext<Entity_492> _queryContext;

    public Entity_492s(IQueryContext<Entity_492> queryContext) => _queryContext = queryContext;

    public List<Entity_492> By0(string name_0) => _queryContext.All();
    public List<Entity_492> By1(string name_1) => _queryContext.All();
}

public class Entity_493
{
    readonly IEntityContext<Entity_493> _context = default!;

    protected Entity_493() { }

    public Entity_493(IEntityContext<Entity_493> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_493 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_493s
{
    readonly IQueryContext<Entity_493> _queryContext;

    public Entity_493s(IQueryContext<Entity_493> queryContext) => _queryContext = queryContext;

    public List<Entity_493> By0(string name_0) => _queryContext.All();
    public List<Entity_493> By1(string name_1) => _queryContext.All();
}

public class Entity_494
{
    readonly IEntityContext<Entity_494> _context = default!;

    protected Entity_494() { }

    public Entity_494(IEntityContext<Entity_494> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_494 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_494s
{
    readonly IQueryContext<Entity_494> _queryContext;

    public Entity_494s(IQueryContext<Entity_494> queryContext) => _queryContext = queryContext;

    public List<Entity_494> By0(string name_0) => _queryContext.All();
    public List<Entity_494> By1(string name_1) => _queryContext.All();
}

public class Entity_495
{
    readonly IEntityContext<Entity_495> _context = default!;

    protected Entity_495() { }

    public Entity_495(IEntityContext<Entity_495> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_495 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_495s
{
    readonly IQueryContext<Entity_495> _queryContext;

    public Entity_495s(IQueryContext<Entity_495> queryContext) => _queryContext = queryContext;

    public List<Entity_495> By0(string name_0) => _queryContext.All();
    public List<Entity_495> By1(string name_1) => _queryContext.All();
}

public class Entity_496
{
    readonly IEntityContext<Entity_496> _context = default!;

    protected Entity_496() { }

    public Entity_496(IEntityContext<Entity_496> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_496 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_496s
{
    readonly IQueryContext<Entity_496> _queryContext;

    public Entity_496s(IQueryContext<Entity_496> queryContext) => _queryContext = queryContext;

    public List<Entity_496> By0(string name_0) => _queryContext.All();
    public List<Entity_496> By1(string name_1) => _queryContext.All();
}

public class Entity_497
{
    readonly IEntityContext<Entity_497> _context = default!;

    protected Entity_497() { }

    public Entity_497(IEntityContext<Entity_497> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_497 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_497s
{
    readonly IQueryContext<Entity_497> _queryContext;

    public Entity_497s(IQueryContext<Entity_497> queryContext) => _queryContext = queryContext;

    public List<Entity_497> By0(string name_0) => _queryContext.All();
    public List<Entity_497> By1(string name_1) => _queryContext.All();
}

public class Entity_498
{
    readonly IEntityContext<Entity_498> _context = default!;

    protected Entity_498() { }

    public Entity_498(IEntityContext<Entity_498> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_498 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_498s
{
    readonly IQueryContext<Entity_498> _queryContext;

    public Entity_498s(IQueryContext<Entity_498> queryContext) => _queryContext = queryContext;

    public List<Entity_498> By0(string name_0) => _queryContext.All();
    public List<Entity_498> By1(string name_1) => _queryContext.All();
}

public class Entity_499
{
    readonly IEntityContext<Entity_499> _context = default!;

    protected Entity_499() { }

    public Entity_499(IEntityContext<Entity_499> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual Entity_499 With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Method_0(string name_0)
    {
        Name = name_0;
    }

    public void Method_1(string name_1)
    {
        Name = name_1;
    }

    public void Method_2(string name_2)
    {
        Name = name_2;
    }

    public void Method_3(string name_3)
    {
        Name = name_3;
    }

    public void Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_499s
{
    readonly IQueryContext<Entity_499> _queryContext;

    public Entity_499s(IQueryContext<Entity_499> queryContext) => _queryContext = queryContext;

    public List<Entity_499> By0(string name_0) => _queryContext.All();
    public List<Entity_499> By1(string name_1) => _queryContext.All();
}

#pragma warning restore SA1649 // File name should match first type name
