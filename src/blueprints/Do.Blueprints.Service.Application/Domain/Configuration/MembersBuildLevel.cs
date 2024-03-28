using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain.Configuration;

public class MembersBuildLevel : BuildLevel
{
    internal override void Set(TypeModel typeModel, Type type, DomainModelBuilder builder)
    {
        typeModel.SetMembers(
            BuildConstructorGroup(),
            type.GetProperties(builder.Options.BindingFlags.Property).Select(BuildProperty).ToModelCollection(),
            BuildMethodGroups()
        );

        ConstructorGroupModel? BuildConstructorGroup()
        {
            var constructorInfos = type.GetConstructors(builder.Options.BindingFlags.Constructor) ?? [];
            if (!constructorInfos.Any()) { return null; }

            var ctorGroup = new ConstructorGroupModel(typeModel);
            ctorGroup.Init([.. constructorInfos.Select(c => BuildConstructor(c, ctorGroup))]);

            return ctorGroup;
        }

        ConstructorModel BuildConstructor(ConstructorInfo constructorInfo, ConstructorGroupModel ctorGroup)
        {
            var result = new ConstructorModel(ctorGroup, constructorInfo.IsPublic, constructorInfo.IsFamily);

            result.Init(
                parameters: BuildParameters(constructorInfo, result),
                customAttributes: new(constructorInfo.GetCustomAttributes())
            );

            return result;
        }

        PropertyModel BuildProperty(PropertyInfo property) =>
            new(
                typeModel,
                property.Name,
                builder.Get(property.PropertyType),
                property.GetMethod?.IsPublic == true,
                property.GetMethod?.IsVirtual == true,
                new(property.GetCustomAttributes())
            );

        ModelCollection<MethodGroupModel> BuildMethodGroups()
        {
            var methods = new Dictionary<string, MethodGroupModel>();
            var methodInfos = type.GetMethods(builder.Options.BindingFlags.Method) ?? [];
            foreach (var group in methodInfos.GroupBy(m => m.Name))
            {
                var method = methods[group.Key] = new(typeModel, group.Key);

                method.Init(methods: [.. group.Select(m => BuildMethod(m, method))]);
            }

            return new(methods.Values);
        }

        MethodModel BuildMethod(MethodInfo methodInfo, MethodGroupModel group)
        {
            var result = new MethodModel(group, methodInfo.IsPublic, methodInfo.IsFamily, methodInfo.IsVirtual, builder.Get(methodInfo.ReturnType));

            result.Init(
                parameters: BuildParameters(methodInfo, result),
                customAttributes: new(methodInfo.GetCustomAttributes())
            );

            return result;
        }

        ModelCollection<ParameterModel> BuildParameters(MethodBase method, MethodBaseModel methodBase) =>
            method.GetParameters().Select(p => BuildParameter(p, methodBase)).ToModelCollection();

        ParameterModel BuildParameter(ParameterInfo parameter, MethodBaseModel overload) =>
            new(
                overload,
                parameter.Name ?? string.Empty,
                builder.Get(parameter.ParameterType),
                parameter.IsOptional,
                parameter.DefaultValue,
                new(parameter.Member.GetCustomAttributes())
            );
    }
}
