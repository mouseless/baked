using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ExceptionHandlerAdderTemplate(IEnumerable<TypeModel> _types) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace ProblemDetailsFeature;

        public class ExceptionHandlerAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_types, exceptionHandler => $$"""
                services.AddSingleton<IExceptionHandler,{{exceptionHandler.CSharpFriendlyFullName}}>(forward: true);
            """)}}
            }
        }
    """;
}