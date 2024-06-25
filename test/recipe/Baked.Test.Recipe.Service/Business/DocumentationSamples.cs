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
    /// <example for="rest-api">
    /// <code for="request">
    /// {
    ///   "parameter1": "value 1",
    ///   "parameter2": "value 2"
    /// }
    /// </code>
    /// <code for="response">
    /// {
    ///   "property": "value 1 - value 2"
    /// }
    /// </code>
    /// </example>
    public DocumentedData Method(string parameter1, string parameter2) =>
        new() { Property = $"{parameter1} - {parameter2}" };

    /// <summary>
    /// Method summary
    /// </summary>
    /// <remarks>
    /// Method description
    /// </remarks>
    /// <returns>
    /// Return documentation
    /// </returns>
    public void ParameterlessMethod() { }

    /// <param name="enumerable">
    /// Enumerable description
    /// </param>
    /// <param name="array">
    /// Array description
    /// </param>
    /// <param name="dictionary">
    /// Dictionary description
    /// </param>
    public string ArrayAndGenericParameters(IEnumerable<string> enumerable, string[] array, Dictionary<string, string> dictionary) =>
        $"{enumerable.Join(", ")} - {array.Join(", ")} - {dictionary.Join(", ")}";

    /// <param name="route">
    /// route description
    /// </param>
    public string Route(string route) =>
        route;

    /// <summary>
    /// Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porta,
    /// augue ut egestas finibus, purus sem scelerisque nunc, ac hendrerit
    /// sapien ligula eget tellus.
    /// </summary>
    /// <remarks>
    /// Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porta,
    /// augue ut egestas finibus, purus sem scelerisque nunc, ac hendrerit
    /// sapien ligula eget tellus.
    ///
    /// Aenean sollicitudin elementum neque, at vehicula lacus pretium ac.
    /// Vivamus ac augue eget leo vehicula mollis. Sed vulputate molestie
    /// commodo.
    /// </remarks>
    public void Multiline() { }
}