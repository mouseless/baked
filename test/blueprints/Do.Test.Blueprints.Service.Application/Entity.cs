using Do.Orm;

namespace Do.Test;

public class Entity
{
    readonly IEntityContext<Entity> _context = default!;

    protected Entity() { }
    public Entity(IEntityContext<Entity> context) =>
        _context = context;

    public virtual Guid Id { get; protected set; } = default!;
    public virtual string Text { get; protected set; } = default!;
    public virtual int Numeric { get; protected set; } = default!;

    public virtual Entity With(string text, int numeric)
    {
        Text = text;
        Numeric = numeric;

        return _context.Insert(this);
    }

    public virtual void Update(string text, int numeric)
    {
        Text = text;
        Numeric = numeric;
    }

    public virtual void Delete()
    {
        _context.Delete(this);
    }
}

public class Entities
{
    readonly IQueryContext<Entity> _context = default!;

    public Entities(IQueryContext<Entity> context) =>
        _context = context;

    public List<Entity> By(string? text = default)
    {
        if (text == default) { return _context.All(); }

        return _context.By(e => e.Text == text);
    }
}
