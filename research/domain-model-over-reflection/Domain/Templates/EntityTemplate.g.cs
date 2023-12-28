#pragma warning disable SA1649 // File name should match first type name
namespace Domain.Business;

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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_0s
{
    readonly IQueryContext<Entity_0> _queryContext;

    public Entity_0s(IQueryContext<Entity_0> queryContext) => _queryContext = queryContext;

    public List<Entity_0> By0(string name_0) => _queryContext.All();
    internal List<Entity_0> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_0> By1(string name_1) => _queryContext.All();
    internal List<Entity_0> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_0> By2(string name_2) => _queryContext.All();
    internal List<Entity_0> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_0> By3(string name_3) => _queryContext.All();
    internal List<Entity_0> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_0> By4(string name_4) => _queryContext.All();
    internal List<Entity_0> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_1s
{
    readonly IQueryContext<Entity_1> _queryContext;

    public Entity_1s(IQueryContext<Entity_1> queryContext) => _queryContext = queryContext;

    public List<Entity_1> By0(string name_0) => _queryContext.All();
    internal List<Entity_1> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_1> By1(string name_1) => _queryContext.All();
    internal List<Entity_1> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_1> By2(string name_2) => _queryContext.All();
    internal List<Entity_1> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_1> By3(string name_3) => _queryContext.All();
    internal List<Entity_1> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_1> By4(string name_4) => _queryContext.All();
    internal List<Entity_1> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_2s
{
    readonly IQueryContext<Entity_2> _queryContext;

    public Entity_2s(IQueryContext<Entity_2> queryContext) => _queryContext = queryContext;

    public List<Entity_2> By0(string name_0) => _queryContext.All();
    internal List<Entity_2> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_2> By1(string name_1) => _queryContext.All();
    internal List<Entity_2> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_2> By2(string name_2) => _queryContext.All();
    internal List<Entity_2> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_2> By3(string name_3) => _queryContext.All();
    internal List<Entity_2> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_2> By4(string name_4) => _queryContext.All();
    internal List<Entity_2> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_3s
{
    readonly IQueryContext<Entity_3> _queryContext;

    public Entity_3s(IQueryContext<Entity_3> queryContext) => _queryContext = queryContext;

    public List<Entity_3> By0(string name_0) => _queryContext.All();
    internal List<Entity_3> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_3> By1(string name_1) => _queryContext.All();
    internal List<Entity_3> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_3> By2(string name_2) => _queryContext.All();
    internal List<Entity_3> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_3> By3(string name_3) => _queryContext.All();
    internal List<Entity_3> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_3> By4(string name_4) => _queryContext.All();
    internal List<Entity_3> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_4s
{
    readonly IQueryContext<Entity_4> _queryContext;

    public Entity_4s(IQueryContext<Entity_4> queryContext) => _queryContext = queryContext;

    public List<Entity_4> By0(string name_0) => _queryContext.All();
    internal List<Entity_4> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_4> By1(string name_1) => _queryContext.All();
    internal List<Entity_4> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_4> By2(string name_2) => _queryContext.All();
    internal List<Entity_4> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_4> By3(string name_3) => _queryContext.All();
    internal List<Entity_4> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_4> By4(string name_4) => _queryContext.All();
    internal List<Entity_4> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_5s
{
    readonly IQueryContext<Entity_5> _queryContext;

    public Entity_5s(IQueryContext<Entity_5> queryContext) => _queryContext = queryContext;

    public List<Entity_5> By0(string name_0) => _queryContext.All();
    internal List<Entity_5> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_5> By1(string name_1) => _queryContext.All();
    internal List<Entity_5> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_5> By2(string name_2) => _queryContext.All();
    internal List<Entity_5> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_5> By3(string name_3) => _queryContext.All();
    internal List<Entity_5> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_5> By4(string name_4) => _queryContext.All();
    internal List<Entity_5> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_6s
{
    readonly IQueryContext<Entity_6> _queryContext;

    public Entity_6s(IQueryContext<Entity_6> queryContext) => _queryContext = queryContext;

    public List<Entity_6> By0(string name_0) => _queryContext.All();
    internal List<Entity_6> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_6> By1(string name_1) => _queryContext.All();
    internal List<Entity_6> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_6> By2(string name_2) => _queryContext.All();
    internal List<Entity_6> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_6> By3(string name_3) => _queryContext.All();
    internal List<Entity_6> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_6> By4(string name_4) => _queryContext.All();
    internal List<Entity_6> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_7s
{
    readonly IQueryContext<Entity_7> _queryContext;

    public Entity_7s(IQueryContext<Entity_7> queryContext) => _queryContext = queryContext;

    public List<Entity_7> By0(string name_0) => _queryContext.All();
    internal List<Entity_7> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_7> By1(string name_1) => _queryContext.All();
    internal List<Entity_7> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_7> By2(string name_2) => _queryContext.All();
    internal List<Entity_7> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_7> By3(string name_3) => _queryContext.All();
    internal List<Entity_7> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_7> By4(string name_4) => _queryContext.All();
    internal List<Entity_7> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_8s
{
    readonly IQueryContext<Entity_8> _queryContext;

    public Entity_8s(IQueryContext<Entity_8> queryContext) => _queryContext = queryContext;

    public List<Entity_8> By0(string name_0) => _queryContext.All();
    internal List<Entity_8> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_8> By1(string name_1) => _queryContext.All();
    internal List<Entity_8> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_8> By2(string name_2) => _queryContext.All();
    internal List<Entity_8> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_8> By3(string name_3) => _queryContext.All();
    internal List<Entity_8> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_8> By4(string name_4) => _queryContext.All();
    internal List<Entity_8> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_9s
{
    readonly IQueryContext<Entity_9> _queryContext;

    public Entity_9s(IQueryContext<Entity_9> queryContext) => _queryContext = queryContext;

    public List<Entity_9> By0(string name_0) => _queryContext.All();
    internal List<Entity_9> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_9> By1(string name_1) => _queryContext.All();
    internal List<Entity_9> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_9> By2(string name_2) => _queryContext.All();
    internal List<Entity_9> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_9> By3(string name_3) => _queryContext.All();
    internal List<Entity_9> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_9> By4(string name_4) => _queryContext.All();
    internal List<Entity_9> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_10s
{
    readonly IQueryContext<Entity_10> _queryContext;

    public Entity_10s(IQueryContext<Entity_10> queryContext) => _queryContext = queryContext;

    public List<Entity_10> By0(string name_0) => _queryContext.All();
    internal List<Entity_10> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_10> By1(string name_1) => _queryContext.All();
    internal List<Entity_10> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_10> By2(string name_2) => _queryContext.All();
    internal List<Entity_10> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_10> By3(string name_3) => _queryContext.All();
    internal List<Entity_10> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_10> By4(string name_4) => _queryContext.All();
    internal List<Entity_10> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_11s
{
    readonly IQueryContext<Entity_11> _queryContext;

    public Entity_11s(IQueryContext<Entity_11> queryContext) => _queryContext = queryContext;

    public List<Entity_11> By0(string name_0) => _queryContext.All();
    internal List<Entity_11> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_11> By1(string name_1) => _queryContext.All();
    internal List<Entity_11> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_11> By2(string name_2) => _queryContext.All();
    internal List<Entity_11> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_11> By3(string name_3) => _queryContext.All();
    internal List<Entity_11> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_11> By4(string name_4) => _queryContext.All();
    internal List<Entity_11> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_12s
{
    readonly IQueryContext<Entity_12> _queryContext;

    public Entity_12s(IQueryContext<Entity_12> queryContext) => _queryContext = queryContext;

    public List<Entity_12> By0(string name_0) => _queryContext.All();
    internal List<Entity_12> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_12> By1(string name_1) => _queryContext.All();
    internal List<Entity_12> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_12> By2(string name_2) => _queryContext.All();
    internal List<Entity_12> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_12> By3(string name_3) => _queryContext.All();
    internal List<Entity_12> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_12> By4(string name_4) => _queryContext.All();
    internal List<Entity_12> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_13s
{
    readonly IQueryContext<Entity_13> _queryContext;

    public Entity_13s(IQueryContext<Entity_13> queryContext) => _queryContext = queryContext;

    public List<Entity_13> By0(string name_0) => _queryContext.All();
    internal List<Entity_13> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_13> By1(string name_1) => _queryContext.All();
    internal List<Entity_13> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_13> By2(string name_2) => _queryContext.All();
    internal List<Entity_13> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_13> By3(string name_3) => _queryContext.All();
    internal List<Entity_13> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_13> By4(string name_4) => _queryContext.All();
    internal List<Entity_13> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_14s
{
    readonly IQueryContext<Entity_14> _queryContext;

    public Entity_14s(IQueryContext<Entity_14> queryContext) => _queryContext = queryContext;

    public List<Entity_14> By0(string name_0) => _queryContext.All();
    internal List<Entity_14> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_14> By1(string name_1) => _queryContext.All();
    internal List<Entity_14> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_14> By2(string name_2) => _queryContext.All();
    internal List<Entity_14> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_14> By3(string name_3) => _queryContext.All();
    internal List<Entity_14> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_14> By4(string name_4) => _queryContext.All();
    internal List<Entity_14> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_15s
{
    readonly IQueryContext<Entity_15> _queryContext;

    public Entity_15s(IQueryContext<Entity_15> queryContext) => _queryContext = queryContext;

    public List<Entity_15> By0(string name_0) => _queryContext.All();
    internal List<Entity_15> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_15> By1(string name_1) => _queryContext.All();
    internal List<Entity_15> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_15> By2(string name_2) => _queryContext.All();
    internal List<Entity_15> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_15> By3(string name_3) => _queryContext.All();
    internal List<Entity_15> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_15> By4(string name_4) => _queryContext.All();
    internal List<Entity_15> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_16s
{
    readonly IQueryContext<Entity_16> _queryContext;

    public Entity_16s(IQueryContext<Entity_16> queryContext) => _queryContext = queryContext;

    public List<Entity_16> By0(string name_0) => _queryContext.All();
    internal List<Entity_16> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_16> By1(string name_1) => _queryContext.All();
    internal List<Entity_16> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_16> By2(string name_2) => _queryContext.All();
    internal List<Entity_16> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_16> By3(string name_3) => _queryContext.All();
    internal List<Entity_16> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_16> By4(string name_4) => _queryContext.All();
    internal List<Entity_16> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_17s
{
    readonly IQueryContext<Entity_17> _queryContext;

    public Entity_17s(IQueryContext<Entity_17> queryContext) => _queryContext = queryContext;

    public List<Entity_17> By0(string name_0) => _queryContext.All();
    internal List<Entity_17> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_17> By1(string name_1) => _queryContext.All();
    internal List<Entity_17> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_17> By2(string name_2) => _queryContext.All();
    internal List<Entity_17> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_17> By3(string name_3) => _queryContext.All();
    internal List<Entity_17> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_17> By4(string name_4) => _queryContext.All();
    internal List<Entity_17> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_18s
{
    readonly IQueryContext<Entity_18> _queryContext;

    public Entity_18s(IQueryContext<Entity_18> queryContext) => _queryContext = queryContext;

    public List<Entity_18> By0(string name_0) => _queryContext.All();
    internal List<Entity_18> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_18> By1(string name_1) => _queryContext.All();
    internal List<Entity_18> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_18> By2(string name_2) => _queryContext.All();
    internal List<Entity_18> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_18> By3(string name_3) => _queryContext.All();
    internal List<Entity_18> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_18> By4(string name_4) => _queryContext.All();
    internal List<Entity_18> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_19s
{
    readonly IQueryContext<Entity_19> _queryContext;

    public Entity_19s(IQueryContext<Entity_19> queryContext) => _queryContext = queryContext;

    public List<Entity_19> By0(string name_0) => _queryContext.All();
    internal List<Entity_19> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_19> By1(string name_1) => _queryContext.All();
    internal List<Entity_19> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_19> By2(string name_2) => _queryContext.All();
    internal List<Entity_19> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_19> By3(string name_3) => _queryContext.All();
    internal List<Entity_19> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_19> By4(string name_4) => _queryContext.All();
    internal List<Entity_19> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_20s
{
    readonly IQueryContext<Entity_20> _queryContext;

    public Entity_20s(IQueryContext<Entity_20> queryContext) => _queryContext = queryContext;

    public List<Entity_20> By0(string name_0) => _queryContext.All();
    internal List<Entity_20> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_20> By1(string name_1) => _queryContext.All();
    internal List<Entity_20> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_20> By2(string name_2) => _queryContext.All();
    internal List<Entity_20> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_20> By3(string name_3) => _queryContext.All();
    internal List<Entity_20> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_20> By4(string name_4) => _queryContext.All();
    internal List<Entity_20> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_21s
{
    readonly IQueryContext<Entity_21> _queryContext;

    public Entity_21s(IQueryContext<Entity_21> queryContext) => _queryContext = queryContext;

    public List<Entity_21> By0(string name_0) => _queryContext.All();
    internal List<Entity_21> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_21> By1(string name_1) => _queryContext.All();
    internal List<Entity_21> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_21> By2(string name_2) => _queryContext.All();
    internal List<Entity_21> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_21> By3(string name_3) => _queryContext.All();
    internal List<Entity_21> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_21> By4(string name_4) => _queryContext.All();
    internal List<Entity_21> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_22s
{
    readonly IQueryContext<Entity_22> _queryContext;

    public Entity_22s(IQueryContext<Entity_22> queryContext) => _queryContext = queryContext;

    public List<Entity_22> By0(string name_0) => _queryContext.All();
    internal List<Entity_22> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_22> By1(string name_1) => _queryContext.All();
    internal List<Entity_22> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_22> By2(string name_2) => _queryContext.All();
    internal List<Entity_22> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_22> By3(string name_3) => _queryContext.All();
    internal List<Entity_22> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_22> By4(string name_4) => _queryContext.All();
    internal List<Entity_22> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_23s
{
    readonly IQueryContext<Entity_23> _queryContext;

    public Entity_23s(IQueryContext<Entity_23> queryContext) => _queryContext = queryContext;

    public List<Entity_23> By0(string name_0) => _queryContext.All();
    internal List<Entity_23> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_23> By1(string name_1) => _queryContext.All();
    internal List<Entity_23> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_23> By2(string name_2) => _queryContext.All();
    internal List<Entity_23> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_23> By3(string name_3) => _queryContext.All();
    internal List<Entity_23> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_23> By4(string name_4) => _queryContext.All();
    internal List<Entity_23> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_24s
{
    readonly IQueryContext<Entity_24> _queryContext;

    public Entity_24s(IQueryContext<Entity_24> queryContext) => _queryContext = queryContext;

    public List<Entity_24> By0(string name_0) => _queryContext.All();
    internal List<Entity_24> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_24> By1(string name_1) => _queryContext.All();
    internal List<Entity_24> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_24> By2(string name_2) => _queryContext.All();
    internal List<Entity_24> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_24> By3(string name_3) => _queryContext.All();
    internal List<Entity_24> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_24> By4(string name_4) => _queryContext.All();
    internal List<Entity_24> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_25s
{
    readonly IQueryContext<Entity_25> _queryContext;

    public Entity_25s(IQueryContext<Entity_25> queryContext) => _queryContext = queryContext;

    public List<Entity_25> By0(string name_0) => _queryContext.All();
    internal List<Entity_25> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_25> By1(string name_1) => _queryContext.All();
    internal List<Entity_25> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_25> By2(string name_2) => _queryContext.All();
    internal List<Entity_25> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_25> By3(string name_3) => _queryContext.All();
    internal List<Entity_25> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_25> By4(string name_4) => _queryContext.All();
    internal List<Entity_25> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_26s
{
    readonly IQueryContext<Entity_26> _queryContext;

    public Entity_26s(IQueryContext<Entity_26> queryContext) => _queryContext = queryContext;

    public List<Entity_26> By0(string name_0) => _queryContext.All();
    internal List<Entity_26> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_26> By1(string name_1) => _queryContext.All();
    internal List<Entity_26> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_26> By2(string name_2) => _queryContext.All();
    internal List<Entity_26> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_26> By3(string name_3) => _queryContext.All();
    internal List<Entity_26> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_26> By4(string name_4) => _queryContext.All();
    internal List<Entity_26> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_27s
{
    readonly IQueryContext<Entity_27> _queryContext;

    public Entity_27s(IQueryContext<Entity_27> queryContext) => _queryContext = queryContext;

    public List<Entity_27> By0(string name_0) => _queryContext.All();
    internal List<Entity_27> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_27> By1(string name_1) => _queryContext.All();
    internal List<Entity_27> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_27> By2(string name_2) => _queryContext.All();
    internal List<Entity_27> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_27> By3(string name_3) => _queryContext.All();
    internal List<Entity_27> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_27> By4(string name_4) => _queryContext.All();
    internal List<Entity_27> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_28s
{
    readonly IQueryContext<Entity_28> _queryContext;

    public Entity_28s(IQueryContext<Entity_28> queryContext) => _queryContext = queryContext;

    public List<Entity_28> By0(string name_0) => _queryContext.All();
    internal List<Entity_28> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_28> By1(string name_1) => _queryContext.All();
    internal List<Entity_28> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_28> By2(string name_2) => _queryContext.All();
    internal List<Entity_28> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_28> By3(string name_3) => _queryContext.All();
    internal List<Entity_28> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_28> By4(string name_4) => _queryContext.All();
    internal List<Entity_28> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_29s
{
    readonly IQueryContext<Entity_29> _queryContext;

    public Entity_29s(IQueryContext<Entity_29> queryContext) => _queryContext = queryContext;

    public List<Entity_29> By0(string name_0) => _queryContext.All();
    internal List<Entity_29> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_29> By1(string name_1) => _queryContext.All();
    internal List<Entity_29> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_29> By2(string name_2) => _queryContext.All();
    internal List<Entity_29> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_29> By3(string name_3) => _queryContext.All();
    internal List<Entity_29> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_29> By4(string name_4) => _queryContext.All();
    internal List<Entity_29> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_30s
{
    readonly IQueryContext<Entity_30> _queryContext;

    public Entity_30s(IQueryContext<Entity_30> queryContext) => _queryContext = queryContext;

    public List<Entity_30> By0(string name_0) => _queryContext.All();
    internal List<Entity_30> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_30> By1(string name_1) => _queryContext.All();
    internal List<Entity_30> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_30> By2(string name_2) => _queryContext.All();
    internal List<Entity_30> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_30> By3(string name_3) => _queryContext.All();
    internal List<Entity_30> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_30> By4(string name_4) => _queryContext.All();
    internal List<Entity_30> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_31s
{
    readonly IQueryContext<Entity_31> _queryContext;

    public Entity_31s(IQueryContext<Entity_31> queryContext) => _queryContext = queryContext;

    public List<Entity_31> By0(string name_0) => _queryContext.All();
    internal List<Entity_31> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_31> By1(string name_1) => _queryContext.All();
    internal List<Entity_31> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_31> By2(string name_2) => _queryContext.All();
    internal List<Entity_31> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_31> By3(string name_3) => _queryContext.All();
    internal List<Entity_31> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_31> By4(string name_4) => _queryContext.All();
    internal List<Entity_31> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_32s
{
    readonly IQueryContext<Entity_32> _queryContext;

    public Entity_32s(IQueryContext<Entity_32> queryContext) => _queryContext = queryContext;

    public List<Entity_32> By0(string name_0) => _queryContext.All();
    internal List<Entity_32> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_32> By1(string name_1) => _queryContext.All();
    internal List<Entity_32> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_32> By2(string name_2) => _queryContext.All();
    internal List<Entity_32> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_32> By3(string name_3) => _queryContext.All();
    internal List<Entity_32> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_32> By4(string name_4) => _queryContext.All();
    internal List<Entity_32> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_33s
{
    readonly IQueryContext<Entity_33> _queryContext;

    public Entity_33s(IQueryContext<Entity_33> queryContext) => _queryContext = queryContext;

    public List<Entity_33> By0(string name_0) => _queryContext.All();
    internal List<Entity_33> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_33> By1(string name_1) => _queryContext.All();
    internal List<Entity_33> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_33> By2(string name_2) => _queryContext.All();
    internal List<Entity_33> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_33> By3(string name_3) => _queryContext.All();
    internal List<Entity_33> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_33> By4(string name_4) => _queryContext.All();
    internal List<Entity_33> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_34s
{
    readonly IQueryContext<Entity_34> _queryContext;

    public Entity_34s(IQueryContext<Entity_34> queryContext) => _queryContext = queryContext;

    public List<Entity_34> By0(string name_0) => _queryContext.All();
    internal List<Entity_34> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_34> By1(string name_1) => _queryContext.All();
    internal List<Entity_34> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_34> By2(string name_2) => _queryContext.All();
    internal List<Entity_34> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_34> By3(string name_3) => _queryContext.All();
    internal List<Entity_34> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_34> By4(string name_4) => _queryContext.All();
    internal List<Entity_34> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_35s
{
    readonly IQueryContext<Entity_35> _queryContext;

    public Entity_35s(IQueryContext<Entity_35> queryContext) => _queryContext = queryContext;

    public List<Entity_35> By0(string name_0) => _queryContext.All();
    internal List<Entity_35> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_35> By1(string name_1) => _queryContext.All();
    internal List<Entity_35> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_35> By2(string name_2) => _queryContext.All();
    internal List<Entity_35> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_35> By3(string name_3) => _queryContext.All();
    internal List<Entity_35> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_35> By4(string name_4) => _queryContext.All();
    internal List<Entity_35> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_36s
{
    readonly IQueryContext<Entity_36> _queryContext;

    public Entity_36s(IQueryContext<Entity_36> queryContext) => _queryContext = queryContext;

    public List<Entity_36> By0(string name_0) => _queryContext.All();
    internal List<Entity_36> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_36> By1(string name_1) => _queryContext.All();
    internal List<Entity_36> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_36> By2(string name_2) => _queryContext.All();
    internal List<Entity_36> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_36> By3(string name_3) => _queryContext.All();
    internal List<Entity_36> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_36> By4(string name_4) => _queryContext.All();
    internal List<Entity_36> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_37s
{
    readonly IQueryContext<Entity_37> _queryContext;

    public Entity_37s(IQueryContext<Entity_37> queryContext) => _queryContext = queryContext;

    public List<Entity_37> By0(string name_0) => _queryContext.All();
    internal List<Entity_37> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_37> By1(string name_1) => _queryContext.All();
    internal List<Entity_37> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_37> By2(string name_2) => _queryContext.All();
    internal List<Entity_37> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_37> By3(string name_3) => _queryContext.All();
    internal List<Entity_37> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_37> By4(string name_4) => _queryContext.All();
    internal List<Entity_37> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_38s
{
    readonly IQueryContext<Entity_38> _queryContext;

    public Entity_38s(IQueryContext<Entity_38> queryContext) => _queryContext = queryContext;

    public List<Entity_38> By0(string name_0) => _queryContext.All();
    internal List<Entity_38> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_38> By1(string name_1) => _queryContext.All();
    internal List<Entity_38> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_38> By2(string name_2) => _queryContext.All();
    internal List<Entity_38> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_38> By3(string name_3) => _queryContext.All();
    internal List<Entity_38> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_38> By4(string name_4) => _queryContext.All();
    internal List<Entity_38> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_39s
{
    readonly IQueryContext<Entity_39> _queryContext;

    public Entity_39s(IQueryContext<Entity_39> queryContext) => _queryContext = queryContext;

    public List<Entity_39> By0(string name_0) => _queryContext.All();
    internal List<Entity_39> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_39> By1(string name_1) => _queryContext.All();
    internal List<Entity_39> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_39> By2(string name_2) => _queryContext.All();
    internal List<Entity_39> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_39> By3(string name_3) => _queryContext.All();
    internal List<Entity_39> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_39> By4(string name_4) => _queryContext.All();
    internal List<Entity_39> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_40s
{
    readonly IQueryContext<Entity_40> _queryContext;

    public Entity_40s(IQueryContext<Entity_40> queryContext) => _queryContext = queryContext;

    public List<Entity_40> By0(string name_0) => _queryContext.All();
    internal List<Entity_40> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_40> By1(string name_1) => _queryContext.All();
    internal List<Entity_40> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_40> By2(string name_2) => _queryContext.All();
    internal List<Entity_40> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_40> By3(string name_3) => _queryContext.All();
    internal List<Entity_40> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_40> By4(string name_4) => _queryContext.All();
    internal List<Entity_40> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_41s
{
    readonly IQueryContext<Entity_41> _queryContext;

    public Entity_41s(IQueryContext<Entity_41> queryContext) => _queryContext = queryContext;

    public List<Entity_41> By0(string name_0) => _queryContext.All();
    internal List<Entity_41> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_41> By1(string name_1) => _queryContext.All();
    internal List<Entity_41> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_41> By2(string name_2) => _queryContext.All();
    internal List<Entity_41> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_41> By3(string name_3) => _queryContext.All();
    internal List<Entity_41> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_41> By4(string name_4) => _queryContext.All();
    internal List<Entity_41> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_42s
{
    readonly IQueryContext<Entity_42> _queryContext;

    public Entity_42s(IQueryContext<Entity_42> queryContext) => _queryContext = queryContext;

    public List<Entity_42> By0(string name_0) => _queryContext.All();
    internal List<Entity_42> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_42> By1(string name_1) => _queryContext.All();
    internal List<Entity_42> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_42> By2(string name_2) => _queryContext.All();
    internal List<Entity_42> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_42> By3(string name_3) => _queryContext.All();
    internal List<Entity_42> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_42> By4(string name_4) => _queryContext.All();
    internal List<Entity_42> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_43s
{
    readonly IQueryContext<Entity_43> _queryContext;

    public Entity_43s(IQueryContext<Entity_43> queryContext) => _queryContext = queryContext;

    public List<Entity_43> By0(string name_0) => _queryContext.All();
    internal List<Entity_43> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_43> By1(string name_1) => _queryContext.All();
    internal List<Entity_43> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_43> By2(string name_2) => _queryContext.All();
    internal List<Entity_43> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_43> By3(string name_3) => _queryContext.All();
    internal List<Entity_43> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_43> By4(string name_4) => _queryContext.All();
    internal List<Entity_43> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_44s
{
    readonly IQueryContext<Entity_44> _queryContext;

    public Entity_44s(IQueryContext<Entity_44> queryContext) => _queryContext = queryContext;

    public List<Entity_44> By0(string name_0) => _queryContext.All();
    internal List<Entity_44> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_44> By1(string name_1) => _queryContext.All();
    internal List<Entity_44> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_44> By2(string name_2) => _queryContext.All();
    internal List<Entity_44> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_44> By3(string name_3) => _queryContext.All();
    internal List<Entity_44> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_44> By4(string name_4) => _queryContext.All();
    internal List<Entity_44> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_45s
{
    readonly IQueryContext<Entity_45> _queryContext;

    public Entity_45s(IQueryContext<Entity_45> queryContext) => _queryContext = queryContext;

    public List<Entity_45> By0(string name_0) => _queryContext.All();
    internal List<Entity_45> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_45> By1(string name_1) => _queryContext.All();
    internal List<Entity_45> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_45> By2(string name_2) => _queryContext.All();
    internal List<Entity_45> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_45> By3(string name_3) => _queryContext.All();
    internal List<Entity_45> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_45> By4(string name_4) => _queryContext.All();
    internal List<Entity_45> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_46s
{
    readonly IQueryContext<Entity_46> _queryContext;

    public Entity_46s(IQueryContext<Entity_46> queryContext) => _queryContext = queryContext;

    public List<Entity_46> By0(string name_0) => _queryContext.All();
    internal List<Entity_46> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_46> By1(string name_1) => _queryContext.All();
    internal List<Entity_46> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_46> By2(string name_2) => _queryContext.All();
    internal List<Entity_46> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_46> By3(string name_3) => _queryContext.All();
    internal List<Entity_46> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_46> By4(string name_4) => _queryContext.All();
    internal List<Entity_46> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_47s
{
    readonly IQueryContext<Entity_47> _queryContext;

    public Entity_47s(IQueryContext<Entity_47> queryContext) => _queryContext = queryContext;

    public List<Entity_47> By0(string name_0) => _queryContext.All();
    internal List<Entity_47> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_47> By1(string name_1) => _queryContext.All();
    internal List<Entity_47> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_47> By2(string name_2) => _queryContext.All();
    internal List<Entity_47> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_47> By3(string name_3) => _queryContext.All();
    internal List<Entity_47> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_47> By4(string name_4) => _queryContext.All();
    internal List<Entity_47> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_48s
{
    readonly IQueryContext<Entity_48> _queryContext;

    public Entity_48s(IQueryContext<Entity_48> queryContext) => _queryContext = queryContext;

    public List<Entity_48> By0(string name_0) => _queryContext.All();
    internal List<Entity_48> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_48> By1(string name_1) => _queryContext.All();
    internal List<Entity_48> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_48> By2(string name_2) => _queryContext.All();
    internal List<Entity_48> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_48> By3(string name_3) => _queryContext.All();
    internal List<Entity_48> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_48> By4(string name_4) => _queryContext.All();
    internal List<Entity_48> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_49s
{
    readonly IQueryContext<Entity_49> _queryContext;

    public Entity_49s(IQueryContext<Entity_49> queryContext) => _queryContext = queryContext;

    public List<Entity_49> By0(string name_0) => _queryContext.All();
    internal List<Entity_49> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_49> By1(string name_1) => _queryContext.All();
    internal List<Entity_49> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_49> By2(string name_2) => _queryContext.All();
    internal List<Entity_49> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_49> By3(string name_3) => _queryContext.All();
    internal List<Entity_49> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_49> By4(string name_4) => _queryContext.All();
    internal List<Entity_49> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_50s
{
    readonly IQueryContext<Entity_50> _queryContext;

    public Entity_50s(IQueryContext<Entity_50> queryContext) => _queryContext = queryContext;

    public List<Entity_50> By0(string name_0) => _queryContext.All();
    internal List<Entity_50> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_50> By1(string name_1) => _queryContext.All();
    internal List<Entity_50> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_50> By2(string name_2) => _queryContext.All();
    internal List<Entity_50> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_50> By3(string name_3) => _queryContext.All();
    internal List<Entity_50> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_50> By4(string name_4) => _queryContext.All();
    internal List<Entity_50> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_51s
{
    readonly IQueryContext<Entity_51> _queryContext;

    public Entity_51s(IQueryContext<Entity_51> queryContext) => _queryContext = queryContext;

    public List<Entity_51> By0(string name_0) => _queryContext.All();
    internal List<Entity_51> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_51> By1(string name_1) => _queryContext.All();
    internal List<Entity_51> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_51> By2(string name_2) => _queryContext.All();
    internal List<Entity_51> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_51> By3(string name_3) => _queryContext.All();
    internal List<Entity_51> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_51> By4(string name_4) => _queryContext.All();
    internal List<Entity_51> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_52s
{
    readonly IQueryContext<Entity_52> _queryContext;

    public Entity_52s(IQueryContext<Entity_52> queryContext) => _queryContext = queryContext;

    public List<Entity_52> By0(string name_0) => _queryContext.All();
    internal List<Entity_52> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_52> By1(string name_1) => _queryContext.All();
    internal List<Entity_52> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_52> By2(string name_2) => _queryContext.All();
    internal List<Entity_52> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_52> By3(string name_3) => _queryContext.All();
    internal List<Entity_52> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_52> By4(string name_4) => _queryContext.All();
    internal List<Entity_52> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_53s
{
    readonly IQueryContext<Entity_53> _queryContext;

    public Entity_53s(IQueryContext<Entity_53> queryContext) => _queryContext = queryContext;

    public List<Entity_53> By0(string name_0) => _queryContext.All();
    internal List<Entity_53> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_53> By1(string name_1) => _queryContext.All();
    internal List<Entity_53> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_53> By2(string name_2) => _queryContext.All();
    internal List<Entity_53> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_53> By3(string name_3) => _queryContext.All();
    internal List<Entity_53> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_53> By4(string name_4) => _queryContext.All();
    internal List<Entity_53> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_54s
{
    readonly IQueryContext<Entity_54> _queryContext;

    public Entity_54s(IQueryContext<Entity_54> queryContext) => _queryContext = queryContext;

    public List<Entity_54> By0(string name_0) => _queryContext.All();
    internal List<Entity_54> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_54> By1(string name_1) => _queryContext.All();
    internal List<Entity_54> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_54> By2(string name_2) => _queryContext.All();
    internal List<Entity_54> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_54> By3(string name_3) => _queryContext.All();
    internal List<Entity_54> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_54> By4(string name_4) => _queryContext.All();
    internal List<Entity_54> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_55s
{
    readonly IQueryContext<Entity_55> _queryContext;

    public Entity_55s(IQueryContext<Entity_55> queryContext) => _queryContext = queryContext;

    public List<Entity_55> By0(string name_0) => _queryContext.All();
    internal List<Entity_55> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_55> By1(string name_1) => _queryContext.All();
    internal List<Entity_55> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_55> By2(string name_2) => _queryContext.All();
    internal List<Entity_55> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_55> By3(string name_3) => _queryContext.All();
    internal List<Entity_55> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_55> By4(string name_4) => _queryContext.All();
    internal List<Entity_55> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_56s
{
    readonly IQueryContext<Entity_56> _queryContext;

    public Entity_56s(IQueryContext<Entity_56> queryContext) => _queryContext = queryContext;

    public List<Entity_56> By0(string name_0) => _queryContext.All();
    internal List<Entity_56> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_56> By1(string name_1) => _queryContext.All();
    internal List<Entity_56> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_56> By2(string name_2) => _queryContext.All();
    internal List<Entity_56> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_56> By3(string name_3) => _queryContext.All();
    internal List<Entity_56> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_56> By4(string name_4) => _queryContext.All();
    internal List<Entity_56> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_57s
{
    readonly IQueryContext<Entity_57> _queryContext;

    public Entity_57s(IQueryContext<Entity_57> queryContext) => _queryContext = queryContext;

    public List<Entity_57> By0(string name_0) => _queryContext.All();
    internal List<Entity_57> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_57> By1(string name_1) => _queryContext.All();
    internal List<Entity_57> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_57> By2(string name_2) => _queryContext.All();
    internal List<Entity_57> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_57> By3(string name_3) => _queryContext.All();
    internal List<Entity_57> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_57> By4(string name_4) => _queryContext.All();
    internal List<Entity_57> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_58s
{
    readonly IQueryContext<Entity_58> _queryContext;

    public Entity_58s(IQueryContext<Entity_58> queryContext) => _queryContext = queryContext;

    public List<Entity_58> By0(string name_0) => _queryContext.All();
    internal List<Entity_58> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_58> By1(string name_1) => _queryContext.All();
    internal List<Entity_58> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_58> By2(string name_2) => _queryContext.All();
    internal List<Entity_58> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_58> By3(string name_3) => _queryContext.All();
    internal List<Entity_58> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_58> By4(string name_4) => _queryContext.All();
    internal List<Entity_58> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_59s
{
    readonly IQueryContext<Entity_59> _queryContext;

    public Entity_59s(IQueryContext<Entity_59> queryContext) => _queryContext = queryContext;

    public List<Entity_59> By0(string name_0) => _queryContext.All();
    internal List<Entity_59> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_59> By1(string name_1) => _queryContext.All();
    internal List<Entity_59> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_59> By2(string name_2) => _queryContext.All();
    internal List<Entity_59> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_59> By3(string name_3) => _queryContext.All();
    internal List<Entity_59> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_59> By4(string name_4) => _queryContext.All();
    internal List<Entity_59> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_60s
{
    readonly IQueryContext<Entity_60> _queryContext;

    public Entity_60s(IQueryContext<Entity_60> queryContext) => _queryContext = queryContext;

    public List<Entity_60> By0(string name_0) => _queryContext.All();
    internal List<Entity_60> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_60> By1(string name_1) => _queryContext.All();
    internal List<Entity_60> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_60> By2(string name_2) => _queryContext.All();
    internal List<Entity_60> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_60> By3(string name_3) => _queryContext.All();
    internal List<Entity_60> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_60> By4(string name_4) => _queryContext.All();
    internal List<Entity_60> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_61s
{
    readonly IQueryContext<Entity_61> _queryContext;

    public Entity_61s(IQueryContext<Entity_61> queryContext) => _queryContext = queryContext;

    public List<Entity_61> By0(string name_0) => _queryContext.All();
    internal List<Entity_61> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_61> By1(string name_1) => _queryContext.All();
    internal List<Entity_61> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_61> By2(string name_2) => _queryContext.All();
    internal List<Entity_61> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_61> By3(string name_3) => _queryContext.All();
    internal List<Entity_61> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_61> By4(string name_4) => _queryContext.All();
    internal List<Entity_61> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_62s
{
    readonly IQueryContext<Entity_62> _queryContext;

    public Entity_62s(IQueryContext<Entity_62> queryContext) => _queryContext = queryContext;

    public List<Entity_62> By0(string name_0) => _queryContext.All();
    internal List<Entity_62> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_62> By1(string name_1) => _queryContext.All();
    internal List<Entity_62> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_62> By2(string name_2) => _queryContext.All();
    internal List<Entity_62> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_62> By3(string name_3) => _queryContext.All();
    internal List<Entity_62> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_62> By4(string name_4) => _queryContext.All();
    internal List<Entity_62> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_63s
{
    readonly IQueryContext<Entity_63> _queryContext;

    public Entity_63s(IQueryContext<Entity_63> queryContext) => _queryContext = queryContext;

    public List<Entity_63> By0(string name_0) => _queryContext.All();
    internal List<Entity_63> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_63> By1(string name_1) => _queryContext.All();
    internal List<Entity_63> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_63> By2(string name_2) => _queryContext.All();
    internal List<Entity_63> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_63> By3(string name_3) => _queryContext.All();
    internal List<Entity_63> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_63> By4(string name_4) => _queryContext.All();
    internal List<Entity_63> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_64s
{
    readonly IQueryContext<Entity_64> _queryContext;

    public Entity_64s(IQueryContext<Entity_64> queryContext) => _queryContext = queryContext;

    public List<Entity_64> By0(string name_0) => _queryContext.All();
    internal List<Entity_64> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_64> By1(string name_1) => _queryContext.All();
    internal List<Entity_64> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_64> By2(string name_2) => _queryContext.All();
    internal List<Entity_64> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_64> By3(string name_3) => _queryContext.All();
    internal List<Entity_64> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_64> By4(string name_4) => _queryContext.All();
    internal List<Entity_64> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_65s
{
    readonly IQueryContext<Entity_65> _queryContext;

    public Entity_65s(IQueryContext<Entity_65> queryContext) => _queryContext = queryContext;

    public List<Entity_65> By0(string name_0) => _queryContext.All();
    internal List<Entity_65> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_65> By1(string name_1) => _queryContext.All();
    internal List<Entity_65> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_65> By2(string name_2) => _queryContext.All();
    internal List<Entity_65> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_65> By3(string name_3) => _queryContext.All();
    internal List<Entity_65> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_65> By4(string name_4) => _queryContext.All();
    internal List<Entity_65> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_66s
{
    readonly IQueryContext<Entity_66> _queryContext;

    public Entity_66s(IQueryContext<Entity_66> queryContext) => _queryContext = queryContext;

    public List<Entity_66> By0(string name_0) => _queryContext.All();
    internal List<Entity_66> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_66> By1(string name_1) => _queryContext.All();
    internal List<Entity_66> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_66> By2(string name_2) => _queryContext.All();
    internal List<Entity_66> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_66> By3(string name_3) => _queryContext.All();
    internal List<Entity_66> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_66> By4(string name_4) => _queryContext.All();
    internal List<Entity_66> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_67s
{
    readonly IQueryContext<Entity_67> _queryContext;

    public Entity_67s(IQueryContext<Entity_67> queryContext) => _queryContext = queryContext;

    public List<Entity_67> By0(string name_0) => _queryContext.All();
    internal List<Entity_67> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_67> By1(string name_1) => _queryContext.All();
    internal List<Entity_67> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_67> By2(string name_2) => _queryContext.All();
    internal List<Entity_67> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_67> By3(string name_3) => _queryContext.All();
    internal List<Entity_67> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_67> By4(string name_4) => _queryContext.All();
    internal List<Entity_67> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_68s
{
    readonly IQueryContext<Entity_68> _queryContext;

    public Entity_68s(IQueryContext<Entity_68> queryContext) => _queryContext = queryContext;

    public List<Entity_68> By0(string name_0) => _queryContext.All();
    internal List<Entity_68> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_68> By1(string name_1) => _queryContext.All();
    internal List<Entity_68> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_68> By2(string name_2) => _queryContext.All();
    internal List<Entity_68> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_68> By3(string name_3) => _queryContext.All();
    internal List<Entity_68> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_68> By4(string name_4) => _queryContext.All();
    internal List<Entity_68> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_69s
{
    readonly IQueryContext<Entity_69> _queryContext;

    public Entity_69s(IQueryContext<Entity_69> queryContext) => _queryContext = queryContext;

    public List<Entity_69> By0(string name_0) => _queryContext.All();
    internal List<Entity_69> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_69> By1(string name_1) => _queryContext.All();
    internal List<Entity_69> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_69> By2(string name_2) => _queryContext.All();
    internal List<Entity_69> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_69> By3(string name_3) => _queryContext.All();
    internal List<Entity_69> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_69> By4(string name_4) => _queryContext.All();
    internal List<Entity_69> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_70s
{
    readonly IQueryContext<Entity_70> _queryContext;

    public Entity_70s(IQueryContext<Entity_70> queryContext) => _queryContext = queryContext;

    public List<Entity_70> By0(string name_0) => _queryContext.All();
    internal List<Entity_70> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_70> By1(string name_1) => _queryContext.All();
    internal List<Entity_70> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_70> By2(string name_2) => _queryContext.All();
    internal List<Entity_70> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_70> By3(string name_3) => _queryContext.All();
    internal List<Entity_70> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_70> By4(string name_4) => _queryContext.All();
    internal List<Entity_70> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_71s
{
    readonly IQueryContext<Entity_71> _queryContext;

    public Entity_71s(IQueryContext<Entity_71> queryContext) => _queryContext = queryContext;

    public List<Entity_71> By0(string name_0) => _queryContext.All();
    internal List<Entity_71> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_71> By1(string name_1) => _queryContext.All();
    internal List<Entity_71> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_71> By2(string name_2) => _queryContext.All();
    internal List<Entity_71> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_71> By3(string name_3) => _queryContext.All();
    internal List<Entity_71> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_71> By4(string name_4) => _queryContext.All();
    internal List<Entity_71> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_72s
{
    readonly IQueryContext<Entity_72> _queryContext;

    public Entity_72s(IQueryContext<Entity_72> queryContext) => _queryContext = queryContext;

    public List<Entity_72> By0(string name_0) => _queryContext.All();
    internal List<Entity_72> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_72> By1(string name_1) => _queryContext.All();
    internal List<Entity_72> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_72> By2(string name_2) => _queryContext.All();
    internal List<Entity_72> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_72> By3(string name_3) => _queryContext.All();
    internal List<Entity_72> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_72> By4(string name_4) => _queryContext.All();
    internal List<Entity_72> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_73s
{
    readonly IQueryContext<Entity_73> _queryContext;

    public Entity_73s(IQueryContext<Entity_73> queryContext) => _queryContext = queryContext;

    public List<Entity_73> By0(string name_0) => _queryContext.All();
    internal List<Entity_73> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_73> By1(string name_1) => _queryContext.All();
    internal List<Entity_73> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_73> By2(string name_2) => _queryContext.All();
    internal List<Entity_73> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_73> By3(string name_3) => _queryContext.All();
    internal List<Entity_73> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_73> By4(string name_4) => _queryContext.All();
    internal List<Entity_73> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_74s
{
    readonly IQueryContext<Entity_74> _queryContext;

    public Entity_74s(IQueryContext<Entity_74> queryContext) => _queryContext = queryContext;

    public List<Entity_74> By0(string name_0) => _queryContext.All();
    internal List<Entity_74> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_74> By1(string name_1) => _queryContext.All();
    internal List<Entity_74> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_74> By2(string name_2) => _queryContext.All();
    internal List<Entity_74> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_74> By3(string name_3) => _queryContext.All();
    internal List<Entity_74> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_74> By4(string name_4) => _queryContext.All();
    internal List<Entity_74> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_75s
{
    readonly IQueryContext<Entity_75> _queryContext;

    public Entity_75s(IQueryContext<Entity_75> queryContext) => _queryContext = queryContext;

    public List<Entity_75> By0(string name_0) => _queryContext.All();
    internal List<Entity_75> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_75> By1(string name_1) => _queryContext.All();
    internal List<Entity_75> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_75> By2(string name_2) => _queryContext.All();
    internal List<Entity_75> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_75> By3(string name_3) => _queryContext.All();
    internal List<Entity_75> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_75> By4(string name_4) => _queryContext.All();
    internal List<Entity_75> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_76s
{
    readonly IQueryContext<Entity_76> _queryContext;

    public Entity_76s(IQueryContext<Entity_76> queryContext) => _queryContext = queryContext;

    public List<Entity_76> By0(string name_0) => _queryContext.All();
    internal List<Entity_76> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_76> By1(string name_1) => _queryContext.All();
    internal List<Entity_76> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_76> By2(string name_2) => _queryContext.All();
    internal List<Entity_76> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_76> By3(string name_3) => _queryContext.All();
    internal List<Entity_76> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_76> By4(string name_4) => _queryContext.All();
    internal List<Entity_76> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_77s
{
    readonly IQueryContext<Entity_77> _queryContext;

    public Entity_77s(IQueryContext<Entity_77> queryContext) => _queryContext = queryContext;

    public List<Entity_77> By0(string name_0) => _queryContext.All();
    internal List<Entity_77> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_77> By1(string name_1) => _queryContext.All();
    internal List<Entity_77> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_77> By2(string name_2) => _queryContext.All();
    internal List<Entity_77> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_77> By3(string name_3) => _queryContext.All();
    internal List<Entity_77> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_77> By4(string name_4) => _queryContext.All();
    internal List<Entity_77> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_78s
{
    readonly IQueryContext<Entity_78> _queryContext;

    public Entity_78s(IQueryContext<Entity_78> queryContext) => _queryContext = queryContext;

    public List<Entity_78> By0(string name_0) => _queryContext.All();
    internal List<Entity_78> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_78> By1(string name_1) => _queryContext.All();
    internal List<Entity_78> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_78> By2(string name_2) => _queryContext.All();
    internal List<Entity_78> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_78> By3(string name_3) => _queryContext.All();
    internal List<Entity_78> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_78> By4(string name_4) => _queryContext.All();
    internal List<Entity_78> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_79s
{
    readonly IQueryContext<Entity_79> _queryContext;

    public Entity_79s(IQueryContext<Entity_79> queryContext) => _queryContext = queryContext;

    public List<Entity_79> By0(string name_0) => _queryContext.All();
    internal List<Entity_79> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_79> By1(string name_1) => _queryContext.All();
    internal List<Entity_79> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_79> By2(string name_2) => _queryContext.All();
    internal List<Entity_79> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_79> By3(string name_3) => _queryContext.All();
    internal List<Entity_79> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_79> By4(string name_4) => _queryContext.All();
    internal List<Entity_79> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_80s
{
    readonly IQueryContext<Entity_80> _queryContext;

    public Entity_80s(IQueryContext<Entity_80> queryContext) => _queryContext = queryContext;

    public List<Entity_80> By0(string name_0) => _queryContext.All();
    internal List<Entity_80> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_80> By1(string name_1) => _queryContext.All();
    internal List<Entity_80> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_80> By2(string name_2) => _queryContext.All();
    internal List<Entity_80> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_80> By3(string name_3) => _queryContext.All();
    internal List<Entity_80> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_80> By4(string name_4) => _queryContext.All();
    internal List<Entity_80> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_81s
{
    readonly IQueryContext<Entity_81> _queryContext;

    public Entity_81s(IQueryContext<Entity_81> queryContext) => _queryContext = queryContext;

    public List<Entity_81> By0(string name_0) => _queryContext.All();
    internal List<Entity_81> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_81> By1(string name_1) => _queryContext.All();
    internal List<Entity_81> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_81> By2(string name_2) => _queryContext.All();
    internal List<Entity_81> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_81> By3(string name_3) => _queryContext.All();
    internal List<Entity_81> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_81> By4(string name_4) => _queryContext.All();
    internal List<Entity_81> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_82s
{
    readonly IQueryContext<Entity_82> _queryContext;

    public Entity_82s(IQueryContext<Entity_82> queryContext) => _queryContext = queryContext;

    public List<Entity_82> By0(string name_0) => _queryContext.All();
    internal List<Entity_82> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_82> By1(string name_1) => _queryContext.All();
    internal List<Entity_82> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_82> By2(string name_2) => _queryContext.All();
    internal List<Entity_82> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_82> By3(string name_3) => _queryContext.All();
    internal List<Entity_82> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_82> By4(string name_4) => _queryContext.All();
    internal List<Entity_82> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_83s
{
    readonly IQueryContext<Entity_83> _queryContext;

    public Entity_83s(IQueryContext<Entity_83> queryContext) => _queryContext = queryContext;

    public List<Entity_83> By0(string name_0) => _queryContext.All();
    internal List<Entity_83> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_83> By1(string name_1) => _queryContext.All();
    internal List<Entity_83> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_83> By2(string name_2) => _queryContext.All();
    internal List<Entity_83> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_83> By3(string name_3) => _queryContext.All();
    internal List<Entity_83> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_83> By4(string name_4) => _queryContext.All();
    internal List<Entity_83> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_84s
{
    readonly IQueryContext<Entity_84> _queryContext;

    public Entity_84s(IQueryContext<Entity_84> queryContext) => _queryContext = queryContext;

    public List<Entity_84> By0(string name_0) => _queryContext.All();
    internal List<Entity_84> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_84> By1(string name_1) => _queryContext.All();
    internal List<Entity_84> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_84> By2(string name_2) => _queryContext.All();
    internal List<Entity_84> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_84> By3(string name_3) => _queryContext.All();
    internal List<Entity_84> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_84> By4(string name_4) => _queryContext.All();
    internal List<Entity_84> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_85s
{
    readonly IQueryContext<Entity_85> _queryContext;

    public Entity_85s(IQueryContext<Entity_85> queryContext) => _queryContext = queryContext;

    public List<Entity_85> By0(string name_0) => _queryContext.All();
    internal List<Entity_85> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_85> By1(string name_1) => _queryContext.All();
    internal List<Entity_85> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_85> By2(string name_2) => _queryContext.All();
    internal List<Entity_85> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_85> By3(string name_3) => _queryContext.All();
    internal List<Entity_85> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_85> By4(string name_4) => _queryContext.All();
    internal List<Entity_85> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_86s
{
    readonly IQueryContext<Entity_86> _queryContext;

    public Entity_86s(IQueryContext<Entity_86> queryContext) => _queryContext = queryContext;

    public List<Entity_86> By0(string name_0) => _queryContext.All();
    internal List<Entity_86> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_86> By1(string name_1) => _queryContext.All();
    internal List<Entity_86> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_86> By2(string name_2) => _queryContext.All();
    internal List<Entity_86> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_86> By3(string name_3) => _queryContext.All();
    internal List<Entity_86> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_86> By4(string name_4) => _queryContext.All();
    internal List<Entity_86> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_87s
{
    readonly IQueryContext<Entity_87> _queryContext;

    public Entity_87s(IQueryContext<Entity_87> queryContext) => _queryContext = queryContext;

    public List<Entity_87> By0(string name_0) => _queryContext.All();
    internal List<Entity_87> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_87> By1(string name_1) => _queryContext.All();
    internal List<Entity_87> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_87> By2(string name_2) => _queryContext.All();
    internal List<Entity_87> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_87> By3(string name_3) => _queryContext.All();
    internal List<Entity_87> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_87> By4(string name_4) => _queryContext.All();
    internal List<Entity_87> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_88s
{
    readonly IQueryContext<Entity_88> _queryContext;

    public Entity_88s(IQueryContext<Entity_88> queryContext) => _queryContext = queryContext;

    public List<Entity_88> By0(string name_0) => _queryContext.All();
    internal List<Entity_88> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_88> By1(string name_1) => _queryContext.All();
    internal List<Entity_88> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_88> By2(string name_2) => _queryContext.All();
    internal List<Entity_88> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_88> By3(string name_3) => _queryContext.All();
    internal List<Entity_88> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_88> By4(string name_4) => _queryContext.All();
    internal List<Entity_88> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_89s
{
    readonly IQueryContext<Entity_89> _queryContext;

    public Entity_89s(IQueryContext<Entity_89> queryContext) => _queryContext = queryContext;

    public List<Entity_89> By0(string name_0) => _queryContext.All();
    internal List<Entity_89> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_89> By1(string name_1) => _queryContext.All();
    internal List<Entity_89> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_89> By2(string name_2) => _queryContext.All();
    internal List<Entity_89> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_89> By3(string name_3) => _queryContext.All();
    internal List<Entity_89> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_89> By4(string name_4) => _queryContext.All();
    internal List<Entity_89> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_90s
{
    readonly IQueryContext<Entity_90> _queryContext;

    public Entity_90s(IQueryContext<Entity_90> queryContext) => _queryContext = queryContext;

    public List<Entity_90> By0(string name_0) => _queryContext.All();
    internal List<Entity_90> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_90> By1(string name_1) => _queryContext.All();
    internal List<Entity_90> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_90> By2(string name_2) => _queryContext.All();
    internal List<Entity_90> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_90> By3(string name_3) => _queryContext.All();
    internal List<Entity_90> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_90> By4(string name_4) => _queryContext.All();
    internal List<Entity_90> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_91s
{
    readonly IQueryContext<Entity_91> _queryContext;

    public Entity_91s(IQueryContext<Entity_91> queryContext) => _queryContext = queryContext;

    public List<Entity_91> By0(string name_0) => _queryContext.All();
    internal List<Entity_91> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_91> By1(string name_1) => _queryContext.All();
    internal List<Entity_91> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_91> By2(string name_2) => _queryContext.All();
    internal List<Entity_91> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_91> By3(string name_3) => _queryContext.All();
    internal List<Entity_91> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_91> By4(string name_4) => _queryContext.All();
    internal List<Entity_91> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_92s
{
    readonly IQueryContext<Entity_92> _queryContext;

    public Entity_92s(IQueryContext<Entity_92> queryContext) => _queryContext = queryContext;

    public List<Entity_92> By0(string name_0) => _queryContext.All();
    internal List<Entity_92> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_92> By1(string name_1) => _queryContext.All();
    internal List<Entity_92> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_92> By2(string name_2) => _queryContext.All();
    internal List<Entity_92> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_92> By3(string name_3) => _queryContext.All();
    internal List<Entity_92> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_92> By4(string name_4) => _queryContext.All();
    internal List<Entity_92> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_93s
{
    readonly IQueryContext<Entity_93> _queryContext;

    public Entity_93s(IQueryContext<Entity_93> queryContext) => _queryContext = queryContext;

    public List<Entity_93> By0(string name_0) => _queryContext.All();
    internal List<Entity_93> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_93> By1(string name_1) => _queryContext.All();
    internal List<Entity_93> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_93> By2(string name_2) => _queryContext.All();
    internal List<Entity_93> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_93> By3(string name_3) => _queryContext.All();
    internal List<Entity_93> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_93> By4(string name_4) => _queryContext.All();
    internal List<Entity_93> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_94s
{
    readonly IQueryContext<Entity_94> _queryContext;

    public Entity_94s(IQueryContext<Entity_94> queryContext) => _queryContext = queryContext;

    public List<Entity_94> By0(string name_0) => _queryContext.All();
    internal List<Entity_94> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_94> By1(string name_1) => _queryContext.All();
    internal List<Entity_94> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_94> By2(string name_2) => _queryContext.All();
    internal List<Entity_94> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_94> By3(string name_3) => _queryContext.All();
    internal List<Entity_94> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_94> By4(string name_4) => _queryContext.All();
    internal List<Entity_94> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_95s
{
    readonly IQueryContext<Entity_95> _queryContext;

    public Entity_95s(IQueryContext<Entity_95> queryContext) => _queryContext = queryContext;

    public List<Entity_95> By0(string name_0) => _queryContext.All();
    internal List<Entity_95> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_95> By1(string name_1) => _queryContext.All();
    internal List<Entity_95> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_95> By2(string name_2) => _queryContext.All();
    internal List<Entity_95> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_95> By3(string name_3) => _queryContext.All();
    internal List<Entity_95> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_95> By4(string name_4) => _queryContext.All();
    internal List<Entity_95> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_96s
{
    readonly IQueryContext<Entity_96> _queryContext;

    public Entity_96s(IQueryContext<Entity_96> queryContext) => _queryContext = queryContext;

    public List<Entity_96> By0(string name_0) => _queryContext.All();
    internal List<Entity_96> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_96> By1(string name_1) => _queryContext.All();
    internal List<Entity_96> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_96> By2(string name_2) => _queryContext.All();
    internal List<Entity_96> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_96> By3(string name_3) => _queryContext.All();
    internal List<Entity_96> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_96> By4(string name_4) => _queryContext.All();
    internal List<Entity_96> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_97s
{
    readonly IQueryContext<Entity_97> _queryContext;

    public Entity_97s(IQueryContext<Entity_97> queryContext) => _queryContext = queryContext;

    public List<Entity_97> By0(string name_0) => _queryContext.All();
    internal List<Entity_97> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_97> By1(string name_1) => _queryContext.All();
    internal List<Entity_97> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_97> By2(string name_2) => _queryContext.All();
    internal List<Entity_97> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_97> By3(string name_3) => _queryContext.All();
    internal List<Entity_97> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_97> By4(string name_4) => _queryContext.All();
    internal List<Entity_97> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_98s
{
    readonly IQueryContext<Entity_98> _queryContext;

    public Entity_98s(IQueryContext<Entity_98> queryContext) => _queryContext = queryContext;

    public List<Entity_98> By0(string name_0) => _queryContext.All();
    internal List<Entity_98> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_98> By1(string name_1) => _queryContext.All();
    internal List<Entity_98> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_98> By2(string name_2) => _queryContext.All();
    internal List<Entity_98> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_98> By3(string name_3) => _queryContext.All();
    internal List<Entity_98> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_98> By4(string name_4) => _queryContext.All();
    internal List<Entity_98> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_99s
{
    readonly IQueryContext<Entity_99> _queryContext;

    public Entity_99s(IQueryContext<Entity_99> queryContext) => _queryContext = queryContext;

    public List<Entity_99> By0(string name_0) => _queryContext.All();
    internal List<Entity_99> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_99> By1(string name_1) => _queryContext.All();
    internal List<Entity_99> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_99> By2(string name_2) => _queryContext.All();
    internal List<Entity_99> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_99> By3(string name_3) => _queryContext.All();
    internal List<Entity_99> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_99> By4(string name_4) => _queryContext.All();
    internal List<Entity_99> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_100s
{
    readonly IQueryContext<Entity_100> _queryContext;

    public Entity_100s(IQueryContext<Entity_100> queryContext) => _queryContext = queryContext;

    public List<Entity_100> By0(string name_0) => _queryContext.All();
    internal List<Entity_100> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_100> By1(string name_1) => _queryContext.All();
    internal List<Entity_100> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_100> By2(string name_2) => _queryContext.All();
    internal List<Entity_100> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_100> By3(string name_3) => _queryContext.All();
    internal List<Entity_100> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_100> By4(string name_4) => _queryContext.All();
    internal List<Entity_100> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_101s
{
    readonly IQueryContext<Entity_101> _queryContext;

    public Entity_101s(IQueryContext<Entity_101> queryContext) => _queryContext = queryContext;

    public List<Entity_101> By0(string name_0) => _queryContext.All();
    internal List<Entity_101> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_101> By1(string name_1) => _queryContext.All();
    internal List<Entity_101> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_101> By2(string name_2) => _queryContext.All();
    internal List<Entity_101> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_101> By3(string name_3) => _queryContext.All();
    internal List<Entity_101> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_101> By4(string name_4) => _queryContext.All();
    internal List<Entity_101> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_102s
{
    readonly IQueryContext<Entity_102> _queryContext;

    public Entity_102s(IQueryContext<Entity_102> queryContext) => _queryContext = queryContext;

    public List<Entity_102> By0(string name_0) => _queryContext.All();
    internal List<Entity_102> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_102> By1(string name_1) => _queryContext.All();
    internal List<Entity_102> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_102> By2(string name_2) => _queryContext.All();
    internal List<Entity_102> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_102> By3(string name_3) => _queryContext.All();
    internal List<Entity_102> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_102> By4(string name_4) => _queryContext.All();
    internal List<Entity_102> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_103s
{
    readonly IQueryContext<Entity_103> _queryContext;

    public Entity_103s(IQueryContext<Entity_103> queryContext) => _queryContext = queryContext;

    public List<Entity_103> By0(string name_0) => _queryContext.All();
    internal List<Entity_103> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_103> By1(string name_1) => _queryContext.All();
    internal List<Entity_103> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_103> By2(string name_2) => _queryContext.All();
    internal List<Entity_103> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_103> By3(string name_3) => _queryContext.All();
    internal List<Entity_103> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_103> By4(string name_4) => _queryContext.All();
    internal List<Entity_103> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_104s
{
    readonly IQueryContext<Entity_104> _queryContext;

    public Entity_104s(IQueryContext<Entity_104> queryContext) => _queryContext = queryContext;

    public List<Entity_104> By0(string name_0) => _queryContext.All();
    internal List<Entity_104> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_104> By1(string name_1) => _queryContext.All();
    internal List<Entity_104> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_104> By2(string name_2) => _queryContext.All();
    internal List<Entity_104> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_104> By3(string name_3) => _queryContext.All();
    internal List<Entity_104> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_104> By4(string name_4) => _queryContext.All();
    internal List<Entity_104> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_105s
{
    readonly IQueryContext<Entity_105> _queryContext;

    public Entity_105s(IQueryContext<Entity_105> queryContext) => _queryContext = queryContext;

    public List<Entity_105> By0(string name_0) => _queryContext.All();
    internal List<Entity_105> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_105> By1(string name_1) => _queryContext.All();
    internal List<Entity_105> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_105> By2(string name_2) => _queryContext.All();
    internal List<Entity_105> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_105> By3(string name_3) => _queryContext.All();
    internal List<Entity_105> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_105> By4(string name_4) => _queryContext.All();
    internal List<Entity_105> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_106s
{
    readonly IQueryContext<Entity_106> _queryContext;

    public Entity_106s(IQueryContext<Entity_106> queryContext) => _queryContext = queryContext;

    public List<Entity_106> By0(string name_0) => _queryContext.All();
    internal List<Entity_106> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_106> By1(string name_1) => _queryContext.All();
    internal List<Entity_106> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_106> By2(string name_2) => _queryContext.All();
    internal List<Entity_106> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_106> By3(string name_3) => _queryContext.All();
    internal List<Entity_106> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_106> By4(string name_4) => _queryContext.All();
    internal List<Entity_106> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_107s
{
    readonly IQueryContext<Entity_107> _queryContext;

    public Entity_107s(IQueryContext<Entity_107> queryContext) => _queryContext = queryContext;

    public List<Entity_107> By0(string name_0) => _queryContext.All();
    internal List<Entity_107> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_107> By1(string name_1) => _queryContext.All();
    internal List<Entity_107> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_107> By2(string name_2) => _queryContext.All();
    internal List<Entity_107> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_107> By3(string name_3) => _queryContext.All();
    internal List<Entity_107> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_107> By4(string name_4) => _queryContext.All();
    internal List<Entity_107> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_108s
{
    readonly IQueryContext<Entity_108> _queryContext;

    public Entity_108s(IQueryContext<Entity_108> queryContext) => _queryContext = queryContext;

    public List<Entity_108> By0(string name_0) => _queryContext.All();
    internal List<Entity_108> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_108> By1(string name_1) => _queryContext.All();
    internal List<Entity_108> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_108> By2(string name_2) => _queryContext.All();
    internal List<Entity_108> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_108> By3(string name_3) => _queryContext.All();
    internal List<Entity_108> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_108> By4(string name_4) => _queryContext.All();
    internal List<Entity_108> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_109s
{
    readonly IQueryContext<Entity_109> _queryContext;

    public Entity_109s(IQueryContext<Entity_109> queryContext) => _queryContext = queryContext;

    public List<Entity_109> By0(string name_0) => _queryContext.All();
    internal List<Entity_109> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_109> By1(string name_1) => _queryContext.All();
    internal List<Entity_109> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_109> By2(string name_2) => _queryContext.All();
    internal List<Entity_109> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_109> By3(string name_3) => _queryContext.All();
    internal List<Entity_109> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_109> By4(string name_4) => _queryContext.All();
    internal List<Entity_109> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_110s
{
    readonly IQueryContext<Entity_110> _queryContext;

    public Entity_110s(IQueryContext<Entity_110> queryContext) => _queryContext = queryContext;

    public List<Entity_110> By0(string name_0) => _queryContext.All();
    internal List<Entity_110> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_110> By1(string name_1) => _queryContext.All();
    internal List<Entity_110> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_110> By2(string name_2) => _queryContext.All();
    internal List<Entity_110> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_110> By3(string name_3) => _queryContext.All();
    internal List<Entity_110> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_110> By4(string name_4) => _queryContext.All();
    internal List<Entity_110> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_111s
{
    readonly IQueryContext<Entity_111> _queryContext;

    public Entity_111s(IQueryContext<Entity_111> queryContext) => _queryContext = queryContext;

    public List<Entity_111> By0(string name_0) => _queryContext.All();
    internal List<Entity_111> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_111> By1(string name_1) => _queryContext.All();
    internal List<Entity_111> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_111> By2(string name_2) => _queryContext.All();
    internal List<Entity_111> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_111> By3(string name_3) => _queryContext.All();
    internal List<Entity_111> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_111> By4(string name_4) => _queryContext.All();
    internal List<Entity_111> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_112s
{
    readonly IQueryContext<Entity_112> _queryContext;

    public Entity_112s(IQueryContext<Entity_112> queryContext) => _queryContext = queryContext;

    public List<Entity_112> By0(string name_0) => _queryContext.All();
    internal List<Entity_112> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_112> By1(string name_1) => _queryContext.All();
    internal List<Entity_112> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_112> By2(string name_2) => _queryContext.All();
    internal List<Entity_112> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_112> By3(string name_3) => _queryContext.All();
    internal List<Entity_112> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_112> By4(string name_4) => _queryContext.All();
    internal List<Entity_112> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_113s
{
    readonly IQueryContext<Entity_113> _queryContext;

    public Entity_113s(IQueryContext<Entity_113> queryContext) => _queryContext = queryContext;

    public List<Entity_113> By0(string name_0) => _queryContext.All();
    internal List<Entity_113> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_113> By1(string name_1) => _queryContext.All();
    internal List<Entity_113> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_113> By2(string name_2) => _queryContext.All();
    internal List<Entity_113> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_113> By3(string name_3) => _queryContext.All();
    internal List<Entity_113> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_113> By4(string name_4) => _queryContext.All();
    internal List<Entity_113> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_114s
{
    readonly IQueryContext<Entity_114> _queryContext;

    public Entity_114s(IQueryContext<Entity_114> queryContext) => _queryContext = queryContext;

    public List<Entity_114> By0(string name_0) => _queryContext.All();
    internal List<Entity_114> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_114> By1(string name_1) => _queryContext.All();
    internal List<Entity_114> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_114> By2(string name_2) => _queryContext.All();
    internal List<Entity_114> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_114> By3(string name_3) => _queryContext.All();
    internal List<Entity_114> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_114> By4(string name_4) => _queryContext.All();
    internal List<Entity_114> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_115s
{
    readonly IQueryContext<Entity_115> _queryContext;

    public Entity_115s(IQueryContext<Entity_115> queryContext) => _queryContext = queryContext;

    public List<Entity_115> By0(string name_0) => _queryContext.All();
    internal List<Entity_115> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_115> By1(string name_1) => _queryContext.All();
    internal List<Entity_115> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_115> By2(string name_2) => _queryContext.All();
    internal List<Entity_115> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_115> By3(string name_3) => _queryContext.All();
    internal List<Entity_115> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_115> By4(string name_4) => _queryContext.All();
    internal List<Entity_115> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_116s
{
    readonly IQueryContext<Entity_116> _queryContext;

    public Entity_116s(IQueryContext<Entity_116> queryContext) => _queryContext = queryContext;

    public List<Entity_116> By0(string name_0) => _queryContext.All();
    internal List<Entity_116> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_116> By1(string name_1) => _queryContext.All();
    internal List<Entity_116> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_116> By2(string name_2) => _queryContext.All();
    internal List<Entity_116> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_116> By3(string name_3) => _queryContext.All();
    internal List<Entity_116> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_116> By4(string name_4) => _queryContext.All();
    internal List<Entity_116> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_117s
{
    readonly IQueryContext<Entity_117> _queryContext;

    public Entity_117s(IQueryContext<Entity_117> queryContext) => _queryContext = queryContext;

    public List<Entity_117> By0(string name_0) => _queryContext.All();
    internal List<Entity_117> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_117> By1(string name_1) => _queryContext.All();
    internal List<Entity_117> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_117> By2(string name_2) => _queryContext.All();
    internal List<Entity_117> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_117> By3(string name_3) => _queryContext.All();
    internal List<Entity_117> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_117> By4(string name_4) => _queryContext.All();
    internal List<Entity_117> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_118s
{
    readonly IQueryContext<Entity_118> _queryContext;

    public Entity_118s(IQueryContext<Entity_118> queryContext) => _queryContext = queryContext;

    public List<Entity_118> By0(string name_0) => _queryContext.All();
    internal List<Entity_118> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_118> By1(string name_1) => _queryContext.All();
    internal List<Entity_118> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_118> By2(string name_2) => _queryContext.All();
    internal List<Entity_118> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_118> By3(string name_3) => _queryContext.All();
    internal List<Entity_118> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_118> By4(string name_4) => _queryContext.All();
    internal List<Entity_118> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_119s
{
    readonly IQueryContext<Entity_119> _queryContext;

    public Entity_119s(IQueryContext<Entity_119> queryContext) => _queryContext = queryContext;

    public List<Entity_119> By0(string name_0) => _queryContext.All();
    internal List<Entity_119> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_119> By1(string name_1) => _queryContext.All();
    internal List<Entity_119> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_119> By2(string name_2) => _queryContext.All();
    internal List<Entity_119> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_119> By3(string name_3) => _queryContext.All();
    internal List<Entity_119> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_119> By4(string name_4) => _queryContext.All();
    internal List<Entity_119> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_120s
{
    readonly IQueryContext<Entity_120> _queryContext;

    public Entity_120s(IQueryContext<Entity_120> queryContext) => _queryContext = queryContext;

    public List<Entity_120> By0(string name_0) => _queryContext.All();
    internal List<Entity_120> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_120> By1(string name_1) => _queryContext.All();
    internal List<Entity_120> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_120> By2(string name_2) => _queryContext.All();
    internal List<Entity_120> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_120> By3(string name_3) => _queryContext.All();
    internal List<Entity_120> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_120> By4(string name_4) => _queryContext.All();
    internal List<Entity_120> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_121s
{
    readonly IQueryContext<Entity_121> _queryContext;

    public Entity_121s(IQueryContext<Entity_121> queryContext) => _queryContext = queryContext;

    public List<Entity_121> By0(string name_0) => _queryContext.All();
    internal List<Entity_121> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_121> By1(string name_1) => _queryContext.All();
    internal List<Entity_121> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_121> By2(string name_2) => _queryContext.All();
    internal List<Entity_121> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_121> By3(string name_3) => _queryContext.All();
    internal List<Entity_121> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_121> By4(string name_4) => _queryContext.All();
    internal List<Entity_121> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_122s
{
    readonly IQueryContext<Entity_122> _queryContext;

    public Entity_122s(IQueryContext<Entity_122> queryContext) => _queryContext = queryContext;

    public List<Entity_122> By0(string name_0) => _queryContext.All();
    internal List<Entity_122> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_122> By1(string name_1) => _queryContext.All();
    internal List<Entity_122> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_122> By2(string name_2) => _queryContext.All();
    internal List<Entity_122> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_122> By3(string name_3) => _queryContext.All();
    internal List<Entity_122> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_122> By4(string name_4) => _queryContext.All();
    internal List<Entity_122> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_123s
{
    readonly IQueryContext<Entity_123> _queryContext;

    public Entity_123s(IQueryContext<Entity_123> queryContext) => _queryContext = queryContext;

    public List<Entity_123> By0(string name_0) => _queryContext.All();
    internal List<Entity_123> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_123> By1(string name_1) => _queryContext.All();
    internal List<Entity_123> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_123> By2(string name_2) => _queryContext.All();
    internal List<Entity_123> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_123> By3(string name_3) => _queryContext.All();
    internal List<Entity_123> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_123> By4(string name_4) => _queryContext.All();
    internal List<Entity_123> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_124s
{
    readonly IQueryContext<Entity_124> _queryContext;

    public Entity_124s(IQueryContext<Entity_124> queryContext) => _queryContext = queryContext;

    public List<Entity_124> By0(string name_0) => _queryContext.All();
    internal List<Entity_124> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_124> By1(string name_1) => _queryContext.All();
    internal List<Entity_124> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_124> By2(string name_2) => _queryContext.All();
    internal List<Entity_124> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_124> By3(string name_3) => _queryContext.All();
    internal List<Entity_124> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_124> By4(string name_4) => _queryContext.All();
    internal List<Entity_124> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_125s
{
    readonly IQueryContext<Entity_125> _queryContext;

    public Entity_125s(IQueryContext<Entity_125> queryContext) => _queryContext = queryContext;

    public List<Entity_125> By0(string name_0) => _queryContext.All();
    internal List<Entity_125> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_125> By1(string name_1) => _queryContext.All();
    internal List<Entity_125> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_125> By2(string name_2) => _queryContext.All();
    internal List<Entity_125> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_125> By3(string name_3) => _queryContext.All();
    internal List<Entity_125> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_125> By4(string name_4) => _queryContext.All();
    internal List<Entity_125> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_126s
{
    readonly IQueryContext<Entity_126> _queryContext;

    public Entity_126s(IQueryContext<Entity_126> queryContext) => _queryContext = queryContext;

    public List<Entity_126> By0(string name_0) => _queryContext.All();
    internal List<Entity_126> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_126> By1(string name_1) => _queryContext.All();
    internal List<Entity_126> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_126> By2(string name_2) => _queryContext.All();
    internal List<Entity_126> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_126> By3(string name_3) => _queryContext.All();
    internal List<Entity_126> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_126> By4(string name_4) => _queryContext.All();
    internal List<Entity_126> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_127s
{
    readonly IQueryContext<Entity_127> _queryContext;

    public Entity_127s(IQueryContext<Entity_127> queryContext) => _queryContext = queryContext;

    public List<Entity_127> By0(string name_0) => _queryContext.All();
    internal List<Entity_127> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_127> By1(string name_1) => _queryContext.All();
    internal List<Entity_127> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_127> By2(string name_2) => _queryContext.All();
    internal List<Entity_127> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_127> By3(string name_3) => _queryContext.All();
    internal List<Entity_127> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_127> By4(string name_4) => _queryContext.All();
    internal List<Entity_127> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_128s
{
    readonly IQueryContext<Entity_128> _queryContext;

    public Entity_128s(IQueryContext<Entity_128> queryContext) => _queryContext = queryContext;

    public List<Entity_128> By0(string name_0) => _queryContext.All();
    internal List<Entity_128> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_128> By1(string name_1) => _queryContext.All();
    internal List<Entity_128> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_128> By2(string name_2) => _queryContext.All();
    internal List<Entity_128> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_128> By3(string name_3) => _queryContext.All();
    internal List<Entity_128> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_128> By4(string name_4) => _queryContext.All();
    internal List<Entity_128> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_129s
{
    readonly IQueryContext<Entity_129> _queryContext;

    public Entity_129s(IQueryContext<Entity_129> queryContext) => _queryContext = queryContext;

    public List<Entity_129> By0(string name_0) => _queryContext.All();
    internal List<Entity_129> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_129> By1(string name_1) => _queryContext.All();
    internal List<Entity_129> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_129> By2(string name_2) => _queryContext.All();
    internal List<Entity_129> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_129> By3(string name_3) => _queryContext.All();
    internal List<Entity_129> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_129> By4(string name_4) => _queryContext.All();
    internal List<Entity_129> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_130s
{
    readonly IQueryContext<Entity_130> _queryContext;

    public Entity_130s(IQueryContext<Entity_130> queryContext) => _queryContext = queryContext;

    public List<Entity_130> By0(string name_0) => _queryContext.All();
    internal List<Entity_130> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_130> By1(string name_1) => _queryContext.All();
    internal List<Entity_130> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_130> By2(string name_2) => _queryContext.All();
    internal List<Entity_130> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_130> By3(string name_3) => _queryContext.All();
    internal List<Entity_130> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_130> By4(string name_4) => _queryContext.All();
    internal List<Entity_130> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_131s
{
    readonly IQueryContext<Entity_131> _queryContext;

    public Entity_131s(IQueryContext<Entity_131> queryContext) => _queryContext = queryContext;

    public List<Entity_131> By0(string name_0) => _queryContext.All();
    internal List<Entity_131> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_131> By1(string name_1) => _queryContext.All();
    internal List<Entity_131> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_131> By2(string name_2) => _queryContext.All();
    internal List<Entity_131> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_131> By3(string name_3) => _queryContext.All();
    internal List<Entity_131> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_131> By4(string name_4) => _queryContext.All();
    internal List<Entity_131> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_132s
{
    readonly IQueryContext<Entity_132> _queryContext;

    public Entity_132s(IQueryContext<Entity_132> queryContext) => _queryContext = queryContext;

    public List<Entity_132> By0(string name_0) => _queryContext.All();
    internal List<Entity_132> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_132> By1(string name_1) => _queryContext.All();
    internal List<Entity_132> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_132> By2(string name_2) => _queryContext.All();
    internal List<Entity_132> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_132> By3(string name_3) => _queryContext.All();
    internal List<Entity_132> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_132> By4(string name_4) => _queryContext.All();
    internal List<Entity_132> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_133s
{
    readonly IQueryContext<Entity_133> _queryContext;

    public Entity_133s(IQueryContext<Entity_133> queryContext) => _queryContext = queryContext;

    public List<Entity_133> By0(string name_0) => _queryContext.All();
    internal List<Entity_133> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_133> By1(string name_1) => _queryContext.All();
    internal List<Entity_133> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_133> By2(string name_2) => _queryContext.All();
    internal List<Entity_133> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_133> By3(string name_3) => _queryContext.All();
    internal List<Entity_133> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_133> By4(string name_4) => _queryContext.All();
    internal List<Entity_133> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_134s
{
    readonly IQueryContext<Entity_134> _queryContext;

    public Entity_134s(IQueryContext<Entity_134> queryContext) => _queryContext = queryContext;

    public List<Entity_134> By0(string name_0) => _queryContext.All();
    internal List<Entity_134> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_134> By1(string name_1) => _queryContext.All();
    internal List<Entity_134> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_134> By2(string name_2) => _queryContext.All();
    internal List<Entity_134> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_134> By3(string name_3) => _queryContext.All();
    internal List<Entity_134> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_134> By4(string name_4) => _queryContext.All();
    internal List<Entity_134> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_135s
{
    readonly IQueryContext<Entity_135> _queryContext;

    public Entity_135s(IQueryContext<Entity_135> queryContext) => _queryContext = queryContext;

    public List<Entity_135> By0(string name_0) => _queryContext.All();
    internal List<Entity_135> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_135> By1(string name_1) => _queryContext.All();
    internal List<Entity_135> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_135> By2(string name_2) => _queryContext.All();
    internal List<Entity_135> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_135> By3(string name_3) => _queryContext.All();
    internal List<Entity_135> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_135> By4(string name_4) => _queryContext.All();
    internal List<Entity_135> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_136s
{
    readonly IQueryContext<Entity_136> _queryContext;

    public Entity_136s(IQueryContext<Entity_136> queryContext) => _queryContext = queryContext;

    public List<Entity_136> By0(string name_0) => _queryContext.All();
    internal List<Entity_136> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_136> By1(string name_1) => _queryContext.All();
    internal List<Entity_136> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_136> By2(string name_2) => _queryContext.All();
    internal List<Entity_136> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_136> By3(string name_3) => _queryContext.All();
    internal List<Entity_136> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_136> By4(string name_4) => _queryContext.All();
    internal List<Entity_136> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_137s
{
    readonly IQueryContext<Entity_137> _queryContext;

    public Entity_137s(IQueryContext<Entity_137> queryContext) => _queryContext = queryContext;

    public List<Entity_137> By0(string name_0) => _queryContext.All();
    internal List<Entity_137> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_137> By1(string name_1) => _queryContext.All();
    internal List<Entity_137> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_137> By2(string name_2) => _queryContext.All();
    internal List<Entity_137> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_137> By3(string name_3) => _queryContext.All();
    internal List<Entity_137> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_137> By4(string name_4) => _queryContext.All();
    internal List<Entity_137> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_138s
{
    readonly IQueryContext<Entity_138> _queryContext;

    public Entity_138s(IQueryContext<Entity_138> queryContext) => _queryContext = queryContext;

    public List<Entity_138> By0(string name_0) => _queryContext.All();
    internal List<Entity_138> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_138> By1(string name_1) => _queryContext.All();
    internal List<Entity_138> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_138> By2(string name_2) => _queryContext.All();
    internal List<Entity_138> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_138> By3(string name_3) => _queryContext.All();
    internal List<Entity_138> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_138> By4(string name_4) => _queryContext.All();
    internal List<Entity_138> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_139s
{
    readonly IQueryContext<Entity_139> _queryContext;

    public Entity_139s(IQueryContext<Entity_139> queryContext) => _queryContext = queryContext;

    public List<Entity_139> By0(string name_0) => _queryContext.All();
    internal List<Entity_139> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_139> By1(string name_1) => _queryContext.All();
    internal List<Entity_139> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_139> By2(string name_2) => _queryContext.All();
    internal List<Entity_139> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_139> By3(string name_3) => _queryContext.All();
    internal List<Entity_139> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_139> By4(string name_4) => _queryContext.All();
    internal List<Entity_139> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_140s
{
    readonly IQueryContext<Entity_140> _queryContext;

    public Entity_140s(IQueryContext<Entity_140> queryContext) => _queryContext = queryContext;

    public List<Entity_140> By0(string name_0) => _queryContext.All();
    internal List<Entity_140> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_140> By1(string name_1) => _queryContext.All();
    internal List<Entity_140> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_140> By2(string name_2) => _queryContext.All();
    internal List<Entity_140> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_140> By3(string name_3) => _queryContext.All();
    internal List<Entity_140> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_140> By4(string name_4) => _queryContext.All();
    internal List<Entity_140> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_141s
{
    readonly IQueryContext<Entity_141> _queryContext;

    public Entity_141s(IQueryContext<Entity_141> queryContext) => _queryContext = queryContext;

    public List<Entity_141> By0(string name_0) => _queryContext.All();
    internal List<Entity_141> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_141> By1(string name_1) => _queryContext.All();
    internal List<Entity_141> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_141> By2(string name_2) => _queryContext.All();
    internal List<Entity_141> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_141> By3(string name_3) => _queryContext.All();
    internal List<Entity_141> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_141> By4(string name_4) => _queryContext.All();
    internal List<Entity_141> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_142s
{
    readonly IQueryContext<Entity_142> _queryContext;

    public Entity_142s(IQueryContext<Entity_142> queryContext) => _queryContext = queryContext;

    public List<Entity_142> By0(string name_0) => _queryContext.All();
    internal List<Entity_142> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_142> By1(string name_1) => _queryContext.All();
    internal List<Entity_142> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_142> By2(string name_2) => _queryContext.All();
    internal List<Entity_142> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_142> By3(string name_3) => _queryContext.All();
    internal List<Entity_142> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_142> By4(string name_4) => _queryContext.All();
    internal List<Entity_142> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_143s
{
    readonly IQueryContext<Entity_143> _queryContext;

    public Entity_143s(IQueryContext<Entity_143> queryContext) => _queryContext = queryContext;

    public List<Entity_143> By0(string name_0) => _queryContext.All();
    internal List<Entity_143> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_143> By1(string name_1) => _queryContext.All();
    internal List<Entity_143> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_143> By2(string name_2) => _queryContext.All();
    internal List<Entity_143> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_143> By3(string name_3) => _queryContext.All();
    internal List<Entity_143> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_143> By4(string name_4) => _queryContext.All();
    internal List<Entity_143> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_144s
{
    readonly IQueryContext<Entity_144> _queryContext;

    public Entity_144s(IQueryContext<Entity_144> queryContext) => _queryContext = queryContext;

    public List<Entity_144> By0(string name_0) => _queryContext.All();
    internal List<Entity_144> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_144> By1(string name_1) => _queryContext.All();
    internal List<Entity_144> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_144> By2(string name_2) => _queryContext.All();
    internal List<Entity_144> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_144> By3(string name_3) => _queryContext.All();
    internal List<Entity_144> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_144> By4(string name_4) => _queryContext.All();
    internal List<Entity_144> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_145s
{
    readonly IQueryContext<Entity_145> _queryContext;

    public Entity_145s(IQueryContext<Entity_145> queryContext) => _queryContext = queryContext;

    public List<Entity_145> By0(string name_0) => _queryContext.All();
    internal List<Entity_145> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_145> By1(string name_1) => _queryContext.All();
    internal List<Entity_145> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_145> By2(string name_2) => _queryContext.All();
    internal List<Entity_145> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_145> By3(string name_3) => _queryContext.All();
    internal List<Entity_145> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_145> By4(string name_4) => _queryContext.All();
    internal List<Entity_145> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_146s
{
    readonly IQueryContext<Entity_146> _queryContext;

    public Entity_146s(IQueryContext<Entity_146> queryContext) => _queryContext = queryContext;

    public List<Entity_146> By0(string name_0) => _queryContext.All();
    internal List<Entity_146> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_146> By1(string name_1) => _queryContext.All();
    internal List<Entity_146> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_146> By2(string name_2) => _queryContext.All();
    internal List<Entity_146> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_146> By3(string name_3) => _queryContext.All();
    internal List<Entity_146> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_146> By4(string name_4) => _queryContext.All();
    internal List<Entity_146> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_147s
{
    readonly IQueryContext<Entity_147> _queryContext;

    public Entity_147s(IQueryContext<Entity_147> queryContext) => _queryContext = queryContext;

    public List<Entity_147> By0(string name_0) => _queryContext.All();
    internal List<Entity_147> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_147> By1(string name_1) => _queryContext.All();
    internal List<Entity_147> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_147> By2(string name_2) => _queryContext.All();
    internal List<Entity_147> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_147> By3(string name_3) => _queryContext.All();
    internal List<Entity_147> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_147> By4(string name_4) => _queryContext.All();
    internal List<Entity_147> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_148s
{
    readonly IQueryContext<Entity_148> _queryContext;

    public Entity_148s(IQueryContext<Entity_148> queryContext) => _queryContext = queryContext;

    public List<Entity_148> By0(string name_0) => _queryContext.All();
    internal List<Entity_148> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_148> By1(string name_1) => _queryContext.All();
    internal List<Entity_148> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_148> By2(string name_2) => _queryContext.All();
    internal List<Entity_148> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_148> By3(string name_3) => _queryContext.All();
    internal List<Entity_148> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_148> By4(string name_4) => _queryContext.All();
    internal List<Entity_148> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_149s
{
    readonly IQueryContext<Entity_149> _queryContext;

    public Entity_149s(IQueryContext<Entity_149> queryContext) => _queryContext = queryContext;

    public List<Entity_149> By0(string name_0) => _queryContext.All();
    internal List<Entity_149> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_149> By1(string name_1) => _queryContext.All();
    internal List<Entity_149> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_149> By2(string name_2) => _queryContext.All();
    internal List<Entity_149> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_149> By3(string name_3) => _queryContext.All();
    internal List<Entity_149> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_149> By4(string name_4) => _queryContext.All();
    internal List<Entity_149> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_150s
{
    readonly IQueryContext<Entity_150> _queryContext;

    public Entity_150s(IQueryContext<Entity_150> queryContext) => _queryContext = queryContext;

    public List<Entity_150> By0(string name_0) => _queryContext.All();
    internal List<Entity_150> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_150> By1(string name_1) => _queryContext.All();
    internal List<Entity_150> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_150> By2(string name_2) => _queryContext.All();
    internal List<Entity_150> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_150> By3(string name_3) => _queryContext.All();
    internal List<Entity_150> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_150> By4(string name_4) => _queryContext.All();
    internal List<Entity_150> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_151s
{
    readonly IQueryContext<Entity_151> _queryContext;

    public Entity_151s(IQueryContext<Entity_151> queryContext) => _queryContext = queryContext;

    public List<Entity_151> By0(string name_0) => _queryContext.All();
    internal List<Entity_151> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_151> By1(string name_1) => _queryContext.All();
    internal List<Entity_151> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_151> By2(string name_2) => _queryContext.All();
    internal List<Entity_151> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_151> By3(string name_3) => _queryContext.All();
    internal List<Entity_151> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_151> By4(string name_4) => _queryContext.All();
    internal List<Entity_151> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_152s
{
    readonly IQueryContext<Entity_152> _queryContext;

    public Entity_152s(IQueryContext<Entity_152> queryContext) => _queryContext = queryContext;

    public List<Entity_152> By0(string name_0) => _queryContext.All();
    internal List<Entity_152> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_152> By1(string name_1) => _queryContext.All();
    internal List<Entity_152> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_152> By2(string name_2) => _queryContext.All();
    internal List<Entity_152> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_152> By3(string name_3) => _queryContext.All();
    internal List<Entity_152> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_152> By4(string name_4) => _queryContext.All();
    internal List<Entity_152> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_153s
{
    readonly IQueryContext<Entity_153> _queryContext;

    public Entity_153s(IQueryContext<Entity_153> queryContext) => _queryContext = queryContext;

    public List<Entity_153> By0(string name_0) => _queryContext.All();
    internal List<Entity_153> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_153> By1(string name_1) => _queryContext.All();
    internal List<Entity_153> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_153> By2(string name_2) => _queryContext.All();
    internal List<Entity_153> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_153> By3(string name_3) => _queryContext.All();
    internal List<Entity_153> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_153> By4(string name_4) => _queryContext.All();
    internal List<Entity_153> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_154s
{
    readonly IQueryContext<Entity_154> _queryContext;

    public Entity_154s(IQueryContext<Entity_154> queryContext) => _queryContext = queryContext;

    public List<Entity_154> By0(string name_0) => _queryContext.All();
    internal List<Entity_154> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_154> By1(string name_1) => _queryContext.All();
    internal List<Entity_154> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_154> By2(string name_2) => _queryContext.All();
    internal List<Entity_154> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_154> By3(string name_3) => _queryContext.All();
    internal List<Entity_154> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_154> By4(string name_4) => _queryContext.All();
    internal List<Entity_154> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_155s
{
    readonly IQueryContext<Entity_155> _queryContext;

    public Entity_155s(IQueryContext<Entity_155> queryContext) => _queryContext = queryContext;

    public List<Entity_155> By0(string name_0) => _queryContext.All();
    internal List<Entity_155> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_155> By1(string name_1) => _queryContext.All();
    internal List<Entity_155> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_155> By2(string name_2) => _queryContext.All();
    internal List<Entity_155> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_155> By3(string name_3) => _queryContext.All();
    internal List<Entity_155> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_155> By4(string name_4) => _queryContext.All();
    internal List<Entity_155> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_156s
{
    readonly IQueryContext<Entity_156> _queryContext;

    public Entity_156s(IQueryContext<Entity_156> queryContext) => _queryContext = queryContext;

    public List<Entity_156> By0(string name_0) => _queryContext.All();
    internal List<Entity_156> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_156> By1(string name_1) => _queryContext.All();
    internal List<Entity_156> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_156> By2(string name_2) => _queryContext.All();
    internal List<Entity_156> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_156> By3(string name_3) => _queryContext.All();
    internal List<Entity_156> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_156> By4(string name_4) => _queryContext.All();
    internal List<Entity_156> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_157s
{
    readonly IQueryContext<Entity_157> _queryContext;

    public Entity_157s(IQueryContext<Entity_157> queryContext) => _queryContext = queryContext;

    public List<Entity_157> By0(string name_0) => _queryContext.All();
    internal List<Entity_157> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_157> By1(string name_1) => _queryContext.All();
    internal List<Entity_157> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_157> By2(string name_2) => _queryContext.All();
    internal List<Entity_157> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_157> By3(string name_3) => _queryContext.All();
    internal List<Entity_157> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_157> By4(string name_4) => _queryContext.All();
    internal List<Entity_157> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_158s
{
    readonly IQueryContext<Entity_158> _queryContext;

    public Entity_158s(IQueryContext<Entity_158> queryContext) => _queryContext = queryContext;

    public List<Entity_158> By0(string name_0) => _queryContext.All();
    internal List<Entity_158> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_158> By1(string name_1) => _queryContext.All();
    internal List<Entity_158> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_158> By2(string name_2) => _queryContext.All();
    internal List<Entity_158> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_158> By3(string name_3) => _queryContext.All();
    internal List<Entity_158> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_158> By4(string name_4) => _queryContext.All();
    internal List<Entity_158> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_159s
{
    readonly IQueryContext<Entity_159> _queryContext;

    public Entity_159s(IQueryContext<Entity_159> queryContext) => _queryContext = queryContext;

    public List<Entity_159> By0(string name_0) => _queryContext.All();
    internal List<Entity_159> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_159> By1(string name_1) => _queryContext.All();
    internal List<Entity_159> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_159> By2(string name_2) => _queryContext.All();
    internal List<Entity_159> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_159> By3(string name_3) => _queryContext.All();
    internal List<Entity_159> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_159> By4(string name_4) => _queryContext.All();
    internal List<Entity_159> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_160s
{
    readonly IQueryContext<Entity_160> _queryContext;

    public Entity_160s(IQueryContext<Entity_160> queryContext) => _queryContext = queryContext;

    public List<Entity_160> By0(string name_0) => _queryContext.All();
    internal List<Entity_160> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_160> By1(string name_1) => _queryContext.All();
    internal List<Entity_160> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_160> By2(string name_2) => _queryContext.All();
    internal List<Entity_160> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_160> By3(string name_3) => _queryContext.All();
    internal List<Entity_160> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_160> By4(string name_4) => _queryContext.All();
    internal List<Entity_160> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_161s
{
    readonly IQueryContext<Entity_161> _queryContext;

    public Entity_161s(IQueryContext<Entity_161> queryContext) => _queryContext = queryContext;

    public List<Entity_161> By0(string name_0) => _queryContext.All();
    internal List<Entity_161> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_161> By1(string name_1) => _queryContext.All();
    internal List<Entity_161> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_161> By2(string name_2) => _queryContext.All();
    internal List<Entity_161> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_161> By3(string name_3) => _queryContext.All();
    internal List<Entity_161> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_161> By4(string name_4) => _queryContext.All();
    internal List<Entity_161> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_162s
{
    readonly IQueryContext<Entity_162> _queryContext;

    public Entity_162s(IQueryContext<Entity_162> queryContext) => _queryContext = queryContext;

    public List<Entity_162> By0(string name_0) => _queryContext.All();
    internal List<Entity_162> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_162> By1(string name_1) => _queryContext.All();
    internal List<Entity_162> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_162> By2(string name_2) => _queryContext.All();
    internal List<Entity_162> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_162> By3(string name_3) => _queryContext.All();
    internal List<Entity_162> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_162> By4(string name_4) => _queryContext.All();
    internal List<Entity_162> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_163s
{
    readonly IQueryContext<Entity_163> _queryContext;

    public Entity_163s(IQueryContext<Entity_163> queryContext) => _queryContext = queryContext;

    public List<Entity_163> By0(string name_0) => _queryContext.All();
    internal List<Entity_163> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_163> By1(string name_1) => _queryContext.All();
    internal List<Entity_163> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_163> By2(string name_2) => _queryContext.All();
    internal List<Entity_163> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_163> By3(string name_3) => _queryContext.All();
    internal List<Entity_163> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_163> By4(string name_4) => _queryContext.All();
    internal List<Entity_163> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_164s
{
    readonly IQueryContext<Entity_164> _queryContext;

    public Entity_164s(IQueryContext<Entity_164> queryContext) => _queryContext = queryContext;

    public List<Entity_164> By0(string name_0) => _queryContext.All();
    internal List<Entity_164> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_164> By1(string name_1) => _queryContext.All();
    internal List<Entity_164> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_164> By2(string name_2) => _queryContext.All();
    internal List<Entity_164> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_164> By3(string name_3) => _queryContext.All();
    internal List<Entity_164> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_164> By4(string name_4) => _queryContext.All();
    internal List<Entity_164> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_165s
{
    readonly IQueryContext<Entity_165> _queryContext;

    public Entity_165s(IQueryContext<Entity_165> queryContext) => _queryContext = queryContext;

    public List<Entity_165> By0(string name_0) => _queryContext.All();
    internal List<Entity_165> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_165> By1(string name_1) => _queryContext.All();
    internal List<Entity_165> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_165> By2(string name_2) => _queryContext.All();
    internal List<Entity_165> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_165> By3(string name_3) => _queryContext.All();
    internal List<Entity_165> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_165> By4(string name_4) => _queryContext.All();
    internal List<Entity_165> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_166s
{
    readonly IQueryContext<Entity_166> _queryContext;

    public Entity_166s(IQueryContext<Entity_166> queryContext) => _queryContext = queryContext;

    public List<Entity_166> By0(string name_0) => _queryContext.All();
    internal List<Entity_166> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_166> By1(string name_1) => _queryContext.All();
    internal List<Entity_166> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_166> By2(string name_2) => _queryContext.All();
    internal List<Entity_166> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_166> By3(string name_3) => _queryContext.All();
    internal List<Entity_166> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_166> By4(string name_4) => _queryContext.All();
    internal List<Entity_166> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_167s
{
    readonly IQueryContext<Entity_167> _queryContext;

    public Entity_167s(IQueryContext<Entity_167> queryContext) => _queryContext = queryContext;

    public List<Entity_167> By0(string name_0) => _queryContext.All();
    internal List<Entity_167> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_167> By1(string name_1) => _queryContext.All();
    internal List<Entity_167> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_167> By2(string name_2) => _queryContext.All();
    internal List<Entity_167> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_167> By3(string name_3) => _queryContext.All();
    internal List<Entity_167> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_167> By4(string name_4) => _queryContext.All();
    internal List<Entity_167> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_168s
{
    readonly IQueryContext<Entity_168> _queryContext;

    public Entity_168s(IQueryContext<Entity_168> queryContext) => _queryContext = queryContext;

    public List<Entity_168> By0(string name_0) => _queryContext.All();
    internal List<Entity_168> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_168> By1(string name_1) => _queryContext.All();
    internal List<Entity_168> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_168> By2(string name_2) => _queryContext.All();
    internal List<Entity_168> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_168> By3(string name_3) => _queryContext.All();
    internal List<Entity_168> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_168> By4(string name_4) => _queryContext.All();
    internal List<Entity_168> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_169s
{
    readonly IQueryContext<Entity_169> _queryContext;

    public Entity_169s(IQueryContext<Entity_169> queryContext) => _queryContext = queryContext;

    public List<Entity_169> By0(string name_0) => _queryContext.All();
    internal List<Entity_169> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_169> By1(string name_1) => _queryContext.All();
    internal List<Entity_169> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_169> By2(string name_2) => _queryContext.All();
    internal List<Entity_169> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_169> By3(string name_3) => _queryContext.All();
    internal List<Entity_169> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_169> By4(string name_4) => _queryContext.All();
    internal List<Entity_169> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_170s
{
    readonly IQueryContext<Entity_170> _queryContext;

    public Entity_170s(IQueryContext<Entity_170> queryContext) => _queryContext = queryContext;

    public List<Entity_170> By0(string name_0) => _queryContext.All();
    internal List<Entity_170> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_170> By1(string name_1) => _queryContext.All();
    internal List<Entity_170> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_170> By2(string name_2) => _queryContext.All();
    internal List<Entity_170> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_170> By3(string name_3) => _queryContext.All();
    internal List<Entity_170> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_170> By4(string name_4) => _queryContext.All();
    internal List<Entity_170> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_171s
{
    readonly IQueryContext<Entity_171> _queryContext;

    public Entity_171s(IQueryContext<Entity_171> queryContext) => _queryContext = queryContext;

    public List<Entity_171> By0(string name_0) => _queryContext.All();
    internal List<Entity_171> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_171> By1(string name_1) => _queryContext.All();
    internal List<Entity_171> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_171> By2(string name_2) => _queryContext.All();
    internal List<Entity_171> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_171> By3(string name_3) => _queryContext.All();
    internal List<Entity_171> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_171> By4(string name_4) => _queryContext.All();
    internal List<Entity_171> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_172s
{
    readonly IQueryContext<Entity_172> _queryContext;

    public Entity_172s(IQueryContext<Entity_172> queryContext) => _queryContext = queryContext;

    public List<Entity_172> By0(string name_0) => _queryContext.All();
    internal List<Entity_172> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_172> By1(string name_1) => _queryContext.All();
    internal List<Entity_172> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_172> By2(string name_2) => _queryContext.All();
    internal List<Entity_172> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_172> By3(string name_3) => _queryContext.All();
    internal List<Entity_172> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_172> By4(string name_4) => _queryContext.All();
    internal List<Entity_172> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_173s
{
    readonly IQueryContext<Entity_173> _queryContext;

    public Entity_173s(IQueryContext<Entity_173> queryContext) => _queryContext = queryContext;

    public List<Entity_173> By0(string name_0) => _queryContext.All();
    internal List<Entity_173> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_173> By1(string name_1) => _queryContext.All();
    internal List<Entity_173> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_173> By2(string name_2) => _queryContext.All();
    internal List<Entity_173> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_173> By3(string name_3) => _queryContext.All();
    internal List<Entity_173> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_173> By4(string name_4) => _queryContext.All();
    internal List<Entity_173> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_174s
{
    readonly IQueryContext<Entity_174> _queryContext;

    public Entity_174s(IQueryContext<Entity_174> queryContext) => _queryContext = queryContext;

    public List<Entity_174> By0(string name_0) => _queryContext.All();
    internal List<Entity_174> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_174> By1(string name_1) => _queryContext.All();
    internal List<Entity_174> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_174> By2(string name_2) => _queryContext.All();
    internal List<Entity_174> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_174> By3(string name_3) => _queryContext.All();
    internal List<Entity_174> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_174> By4(string name_4) => _queryContext.All();
    internal List<Entity_174> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_175s
{
    readonly IQueryContext<Entity_175> _queryContext;

    public Entity_175s(IQueryContext<Entity_175> queryContext) => _queryContext = queryContext;

    public List<Entity_175> By0(string name_0) => _queryContext.All();
    internal List<Entity_175> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_175> By1(string name_1) => _queryContext.All();
    internal List<Entity_175> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_175> By2(string name_2) => _queryContext.All();
    internal List<Entity_175> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_175> By3(string name_3) => _queryContext.All();
    internal List<Entity_175> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_175> By4(string name_4) => _queryContext.All();
    internal List<Entity_175> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_176s
{
    readonly IQueryContext<Entity_176> _queryContext;

    public Entity_176s(IQueryContext<Entity_176> queryContext) => _queryContext = queryContext;

    public List<Entity_176> By0(string name_0) => _queryContext.All();
    internal List<Entity_176> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_176> By1(string name_1) => _queryContext.All();
    internal List<Entity_176> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_176> By2(string name_2) => _queryContext.All();
    internal List<Entity_176> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_176> By3(string name_3) => _queryContext.All();
    internal List<Entity_176> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_176> By4(string name_4) => _queryContext.All();
    internal List<Entity_176> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_177s
{
    readonly IQueryContext<Entity_177> _queryContext;

    public Entity_177s(IQueryContext<Entity_177> queryContext) => _queryContext = queryContext;

    public List<Entity_177> By0(string name_0) => _queryContext.All();
    internal List<Entity_177> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_177> By1(string name_1) => _queryContext.All();
    internal List<Entity_177> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_177> By2(string name_2) => _queryContext.All();
    internal List<Entity_177> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_177> By3(string name_3) => _queryContext.All();
    internal List<Entity_177> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_177> By4(string name_4) => _queryContext.All();
    internal List<Entity_177> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_178s
{
    readonly IQueryContext<Entity_178> _queryContext;

    public Entity_178s(IQueryContext<Entity_178> queryContext) => _queryContext = queryContext;

    public List<Entity_178> By0(string name_0) => _queryContext.All();
    internal List<Entity_178> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_178> By1(string name_1) => _queryContext.All();
    internal List<Entity_178> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_178> By2(string name_2) => _queryContext.All();
    internal List<Entity_178> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_178> By3(string name_3) => _queryContext.All();
    internal List<Entity_178> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_178> By4(string name_4) => _queryContext.All();
    internal List<Entity_178> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_179s
{
    readonly IQueryContext<Entity_179> _queryContext;

    public Entity_179s(IQueryContext<Entity_179> queryContext) => _queryContext = queryContext;

    public List<Entity_179> By0(string name_0) => _queryContext.All();
    internal List<Entity_179> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_179> By1(string name_1) => _queryContext.All();
    internal List<Entity_179> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_179> By2(string name_2) => _queryContext.All();
    internal List<Entity_179> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_179> By3(string name_3) => _queryContext.All();
    internal List<Entity_179> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_179> By4(string name_4) => _queryContext.All();
    internal List<Entity_179> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_180s
{
    readonly IQueryContext<Entity_180> _queryContext;

    public Entity_180s(IQueryContext<Entity_180> queryContext) => _queryContext = queryContext;

    public List<Entity_180> By0(string name_0) => _queryContext.All();
    internal List<Entity_180> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_180> By1(string name_1) => _queryContext.All();
    internal List<Entity_180> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_180> By2(string name_2) => _queryContext.All();
    internal List<Entity_180> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_180> By3(string name_3) => _queryContext.All();
    internal List<Entity_180> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_180> By4(string name_4) => _queryContext.All();
    internal List<Entity_180> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_181s
{
    readonly IQueryContext<Entity_181> _queryContext;

    public Entity_181s(IQueryContext<Entity_181> queryContext) => _queryContext = queryContext;

    public List<Entity_181> By0(string name_0) => _queryContext.All();
    internal List<Entity_181> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_181> By1(string name_1) => _queryContext.All();
    internal List<Entity_181> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_181> By2(string name_2) => _queryContext.All();
    internal List<Entity_181> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_181> By3(string name_3) => _queryContext.All();
    internal List<Entity_181> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_181> By4(string name_4) => _queryContext.All();
    internal List<Entity_181> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_182s
{
    readonly IQueryContext<Entity_182> _queryContext;

    public Entity_182s(IQueryContext<Entity_182> queryContext) => _queryContext = queryContext;

    public List<Entity_182> By0(string name_0) => _queryContext.All();
    internal List<Entity_182> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_182> By1(string name_1) => _queryContext.All();
    internal List<Entity_182> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_182> By2(string name_2) => _queryContext.All();
    internal List<Entity_182> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_182> By3(string name_3) => _queryContext.All();
    internal List<Entity_182> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_182> By4(string name_4) => _queryContext.All();
    internal List<Entity_182> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_183s
{
    readonly IQueryContext<Entity_183> _queryContext;

    public Entity_183s(IQueryContext<Entity_183> queryContext) => _queryContext = queryContext;

    public List<Entity_183> By0(string name_0) => _queryContext.All();
    internal List<Entity_183> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_183> By1(string name_1) => _queryContext.All();
    internal List<Entity_183> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_183> By2(string name_2) => _queryContext.All();
    internal List<Entity_183> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_183> By3(string name_3) => _queryContext.All();
    internal List<Entity_183> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_183> By4(string name_4) => _queryContext.All();
    internal List<Entity_183> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_184s
{
    readonly IQueryContext<Entity_184> _queryContext;

    public Entity_184s(IQueryContext<Entity_184> queryContext) => _queryContext = queryContext;

    public List<Entity_184> By0(string name_0) => _queryContext.All();
    internal List<Entity_184> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_184> By1(string name_1) => _queryContext.All();
    internal List<Entity_184> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_184> By2(string name_2) => _queryContext.All();
    internal List<Entity_184> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_184> By3(string name_3) => _queryContext.All();
    internal List<Entity_184> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_184> By4(string name_4) => _queryContext.All();
    internal List<Entity_184> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_185s
{
    readonly IQueryContext<Entity_185> _queryContext;

    public Entity_185s(IQueryContext<Entity_185> queryContext) => _queryContext = queryContext;

    public List<Entity_185> By0(string name_0) => _queryContext.All();
    internal List<Entity_185> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_185> By1(string name_1) => _queryContext.All();
    internal List<Entity_185> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_185> By2(string name_2) => _queryContext.All();
    internal List<Entity_185> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_185> By3(string name_3) => _queryContext.All();
    internal List<Entity_185> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_185> By4(string name_4) => _queryContext.All();
    internal List<Entity_185> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_186s
{
    readonly IQueryContext<Entity_186> _queryContext;

    public Entity_186s(IQueryContext<Entity_186> queryContext) => _queryContext = queryContext;

    public List<Entity_186> By0(string name_0) => _queryContext.All();
    internal List<Entity_186> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_186> By1(string name_1) => _queryContext.All();
    internal List<Entity_186> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_186> By2(string name_2) => _queryContext.All();
    internal List<Entity_186> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_186> By3(string name_3) => _queryContext.All();
    internal List<Entity_186> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_186> By4(string name_4) => _queryContext.All();
    internal List<Entity_186> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_187s
{
    readonly IQueryContext<Entity_187> _queryContext;

    public Entity_187s(IQueryContext<Entity_187> queryContext) => _queryContext = queryContext;

    public List<Entity_187> By0(string name_0) => _queryContext.All();
    internal List<Entity_187> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_187> By1(string name_1) => _queryContext.All();
    internal List<Entity_187> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_187> By2(string name_2) => _queryContext.All();
    internal List<Entity_187> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_187> By3(string name_3) => _queryContext.All();
    internal List<Entity_187> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_187> By4(string name_4) => _queryContext.All();
    internal List<Entity_187> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_188s
{
    readonly IQueryContext<Entity_188> _queryContext;

    public Entity_188s(IQueryContext<Entity_188> queryContext) => _queryContext = queryContext;

    public List<Entity_188> By0(string name_0) => _queryContext.All();
    internal List<Entity_188> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_188> By1(string name_1) => _queryContext.All();
    internal List<Entity_188> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_188> By2(string name_2) => _queryContext.All();
    internal List<Entity_188> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_188> By3(string name_3) => _queryContext.All();
    internal List<Entity_188> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_188> By4(string name_4) => _queryContext.All();
    internal List<Entity_188> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_189s
{
    readonly IQueryContext<Entity_189> _queryContext;

    public Entity_189s(IQueryContext<Entity_189> queryContext) => _queryContext = queryContext;

    public List<Entity_189> By0(string name_0) => _queryContext.All();
    internal List<Entity_189> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_189> By1(string name_1) => _queryContext.All();
    internal List<Entity_189> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_189> By2(string name_2) => _queryContext.All();
    internal List<Entity_189> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_189> By3(string name_3) => _queryContext.All();
    internal List<Entity_189> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_189> By4(string name_4) => _queryContext.All();
    internal List<Entity_189> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_190s
{
    readonly IQueryContext<Entity_190> _queryContext;

    public Entity_190s(IQueryContext<Entity_190> queryContext) => _queryContext = queryContext;

    public List<Entity_190> By0(string name_0) => _queryContext.All();
    internal List<Entity_190> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_190> By1(string name_1) => _queryContext.All();
    internal List<Entity_190> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_190> By2(string name_2) => _queryContext.All();
    internal List<Entity_190> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_190> By3(string name_3) => _queryContext.All();
    internal List<Entity_190> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_190> By4(string name_4) => _queryContext.All();
    internal List<Entity_190> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_191s
{
    readonly IQueryContext<Entity_191> _queryContext;

    public Entity_191s(IQueryContext<Entity_191> queryContext) => _queryContext = queryContext;

    public List<Entity_191> By0(string name_0) => _queryContext.All();
    internal List<Entity_191> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_191> By1(string name_1) => _queryContext.All();
    internal List<Entity_191> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_191> By2(string name_2) => _queryContext.All();
    internal List<Entity_191> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_191> By3(string name_3) => _queryContext.All();
    internal List<Entity_191> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_191> By4(string name_4) => _queryContext.All();
    internal List<Entity_191> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_192s
{
    readonly IQueryContext<Entity_192> _queryContext;

    public Entity_192s(IQueryContext<Entity_192> queryContext) => _queryContext = queryContext;

    public List<Entity_192> By0(string name_0) => _queryContext.All();
    internal List<Entity_192> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_192> By1(string name_1) => _queryContext.All();
    internal List<Entity_192> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_192> By2(string name_2) => _queryContext.All();
    internal List<Entity_192> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_192> By3(string name_3) => _queryContext.All();
    internal List<Entity_192> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_192> By4(string name_4) => _queryContext.All();
    internal List<Entity_192> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_193s
{
    readonly IQueryContext<Entity_193> _queryContext;

    public Entity_193s(IQueryContext<Entity_193> queryContext) => _queryContext = queryContext;

    public List<Entity_193> By0(string name_0) => _queryContext.All();
    internal List<Entity_193> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_193> By1(string name_1) => _queryContext.All();
    internal List<Entity_193> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_193> By2(string name_2) => _queryContext.All();
    internal List<Entity_193> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_193> By3(string name_3) => _queryContext.All();
    internal List<Entity_193> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_193> By4(string name_4) => _queryContext.All();
    internal List<Entity_193> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_194s
{
    readonly IQueryContext<Entity_194> _queryContext;

    public Entity_194s(IQueryContext<Entity_194> queryContext) => _queryContext = queryContext;

    public List<Entity_194> By0(string name_0) => _queryContext.All();
    internal List<Entity_194> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_194> By1(string name_1) => _queryContext.All();
    internal List<Entity_194> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_194> By2(string name_2) => _queryContext.All();
    internal List<Entity_194> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_194> By3(string name_3) => _queryContext.All();
    internal List<Entity_194> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_194> By4(string name_4) => _queryContext.All();
    internal List<Entity_194> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_195s
{
    readonly IQueryContext<Entity_195> _queryContext;

    public Entity_195s(IQueryContext<Entity_195> queryContext) => _queryContext = queryContext;

    public List<Entity_195> By0(string name_0) => _queryContext.All();
    internal List<Entity_195> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_195> By1(string name_1) => _queryContext.All();
    internal List<Entity_195> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_195> By2(string name_2) => _queryContext.All();
    internal List<Entity_195> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_195> By3(string name_3) => _queryContext.All();
    internal List<Entity_195> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_195> By4(string name_4) => _queryContext.All();
    internal List<Entity_195> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_196s
{
    readonly IQueryContext<Entity_196> _queryContext;

    public Entity_196s(IQueryContext<Entity_196> queryContext) => _queryContext = queryContext;

    public List<Entity_196> By0(string name_0) => _queryContext.All();
    internal List<Entity_196> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_196> By1(string name_1) => _queryContext.All();
    internal List<Entity_196> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_196> By2(string name_2) => _queryContext.All();
    internal List<Entity_196> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_196> By3(string name_3) => _queryContext.All();
    internal List<Entity_196> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_196> By4(string name_4) => _queryContext.All();
    internal List<Entity_196> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_197s
{
    readonly IQueryContext<Entity_197> _queryContext;

    public Entity_197s(IQueryContext<Entity_197> queryContext) => _queryContext = queryContext;

    public List<Entity_197> By0(string name_0) => _queryContext.All();
    internal List<Entity_197> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_197> By1(string name_1) => _queryContext.All();
    internal List<Entity_197> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_197> By2(string name_2) => _queryContext.All();
    internal List<Entity_197> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_197> By3(string name_3) => _queryContext.All();
    internal List<Entity_197> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_197> By4(string name_4) => _queryContext.All();
    internal List<Entity_197> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_198s
{
    readonly IQueryContext<Entity_198> _queryContext;

    public Entity_198s(IQueryContext<Entity_198> queryContext) => _queryContext = queryContext;

    public List<Entity_198> By0(string name_0) => _queryContext.All();
    internal List<Entity_198> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_198> By1(string name_1) => _queryContext.All();
    internal List<Entity_198> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_198> By2(string name_2) => _queryContext.All();
    internal List<Entity_198> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_198> By3(string name_3) => _queryContext.All();
    internal List<Entity_198> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_198> By4(string name_4) => _queryContext.All();
    internal List<Entity_198> Internal_By4(string name_4) => _queryContext.All();
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

    public virtual void Method_0(string name_0)
    {
        Name = name_0;
    }

    protected internal virtual void Internal_Method_0(string name_0)
    {
        Name = name_0;
    }

    public virtual void Method_1(string name_1)
    {
        Name = name_1;
    }

    protected internal virtual void Internal_Method_1(string name_1)
    {
        Name = name_1;
    }

    public virtual void Method_2(string name_2)
    {
        Name = name_2;
    }

    protected internal virtual void Internal_Method_2(string name_2)
    {
        Name = name_2;
    }

    public virtual void Method_3(string name_3)
    {
        Name = name_3;
    }

    protected internal virtual void Internal_Method_3(string name_3)
    {
        Name = name_3;
    }

    public virtual void Method_4(string name_4)
    {
        Name = name_4;
    }

    protected internal virtual void Internal_Method_4(string name_4)
    {
        Name = name_4;
    }
}

public class Entity_199s
{
    readonly IQueryContext<Entity_199> _queryContext;

    public Entity_199s(IQueryContext<Entity_199> queryContext) => _queryContext = queryContext;

    public List<Entity_199> By0(string name_0) => _queryContext.All();
    internal List<Entity_199> Internal_By0(string name_0) => _queryContext.All();
    public List<Entity_199> By1(string name_1) => _queryContext.All();
    internal List<Entity_199> Internal_By1(string name_1) => _queryContext.All();
    public List<Entity_199> By2(string name_2) => _queryContext.All();
    internal List<Entity_199> Internal_By2(string name_2) => _queryContext.All();
    public List<Entity_199> By3(string name_3) => _queryContext.All();
    internal List<Entity_199> Internal_By3(string name_3) => _queryContext.All();
    public List<Entity_199> By4(string name_4) => _queryContext.All();
    internal List<Entity_199> Internal_By4(string name_4) => _queryContext.All();
}
#pragma warning restore SA1649 // File name should match first type name
