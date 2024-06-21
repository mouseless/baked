using System.Xml;

namespace Baked.Domain.Model;

public interface IDocumentedModel
{
    XmlNode? Documentation { get; }
}