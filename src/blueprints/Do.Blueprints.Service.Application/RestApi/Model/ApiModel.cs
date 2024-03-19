namespace Do.RestApi.Model;

public record ApiModel
{
    public List<ControllerModel> Controllers { get; set; } = [];
}
