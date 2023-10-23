using System.Reflection;

namespace Do.Domain.Model;

public record AssemblyModel(Assembly Assembly, List<TypeModel> TypeModels);
