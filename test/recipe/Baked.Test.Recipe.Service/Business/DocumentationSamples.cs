namespace Baked.Test.Business;

/// <summary>
/// Class summary
/// </summary>
public class DocumentationSamples
{
    /// <summary>
    /// Method summary
    /// </summary>
    /// <remarks>
    /// Method description
    /// </remarks>
    /// <returns>
    /// Return documentation
    /// </returns>
    /// <param name="parameter1">
    /// Parameter 1 documentation
    /// </param>
    /// <param name="parameter2">
    /// Parameter 2 documentation
    /// </param>
    public DocumentedData Method(string parameter1, string parameter2) =>
        new() { Property = $"{parameter1} - {parameter2}" };
}