using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Baked.Binding.Rest;

public class RevalidateValidatableObjectsFilter(IOptions<ApiBehaviorOptions> _apiBehaviorOptions)
    : IActionFilter
{
    static readonly ConcurrentDictionary<Type, PropertyInfo[]> _validatableProperties = new();

    internal static bool IsValidatable(Type type) =>
        type.IsAssignableTo(typeof(IValidatableObject)) ||
        type.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
            i.GetGenericArguments()[0].IsAssignableTo(typeof(IValidatableObject))
        );

    static IEnumerable<IValidatableObject> FindValidatables(object? argument)
    {
        if (argument is null) { return []; }

        var validatables = ValidatablesIn(argument).ToList();
        if (validatables.Any()) { return validatables; }

        // NOTE body parameters are wrapped in a request class when there are
        // more than one of them, so their values are one level deeper
        var properties = _validatableProperties.GetOrAdd(argument.GetType(), type =>
            [.. type.GetProperties().Where(property => property.CanRead && property.GetIndexParameters().Length == 0 && IsValidatable(property.PropertyType))]
        );

        return properties.SelectMany(property => ValidatablesIn(property.GetValue(argument)));
    }

    static IEnumerable<IValidatableObject> ValidatablesIn(object? value)
    {
        if (value is IValidatableObject validatable) { return [validatable]; }
        if (value is string) { return []; }
        if (value is IEnumerable items) { return items.OfType<IValidatableObject>(); }

        return [];
    }

    // NOTE property attributes are already validated by mvc during model
    // binding, only object level validation is run again here
    static void Validate(ActionExecutingContext context, IValidatableObject validatable)
    {
        var validationContext = new ValidationContext(validatable, context.HttpContext.RequestServices, null);
        foreach (var result in validatable.Validate(validationContext))
        {
            if (result == ValidationResult.Success) { continue; }

            var memberNames = result.MemberNames.Any() ? result.MemberNames : [string.Empty];
            foreach (var memberName in memberNames)
            {
                context.ModelState.AddModelError(memberName, result.ErrorMessage ?? string.Empty);
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            var source = parameter.BindingInfo?.BindingSource;
            if (source != BindingSource.Body && source != BindingSource.Form) { continue; }
            if (!context.ActionArguments.TryGetValue(parameter.Name, out var argument)) { continue; }

            foreach (var validatable in FindValidatables(argument))
            {
                Validate(context, validatable);
            }
        }

        if (context.ModelState.IsValid) { return; }

        context.Result = _apiBehaviorOptions.Value.InvalidModelStateResponseFactory(context);
    }

    public void OnActionExecuted(ActionExecutedContext _) { }
}