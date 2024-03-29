using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConfigurer(IDomainConfiguration _configuration)
{
    internal void Execute(DomainModel model)
    {
        _configuration.Type.Apply(model.Types);

        foreach (var properties in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().Properties))
        {
            _configuration.Property.Apply(properties);
        }

        foreach (var methods in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().MethodGroups))
        {
            _configuration.MethodGroup.Apply(methods);
        }

        foreach (var methods in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().MethodGroups))
        {
            foreach (var overloads in methods.Select(m => m.Methods))
            {
                foreach (var overload in overloads)
                {
                    _configuration.Parameter.Apply(overload.Parameters);
                }
            }
        }
    }
}
