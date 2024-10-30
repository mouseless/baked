using Baked.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Test.CodingStyle.CommandPattern;

public class CommandWithEntity(ILogger<CommandWithEntity> _logger)
{
    Parent _entity = default!;
    string _string = default!;

    public CommandWithEntity With(Parent entity, string @string)
    {
        _entity = entity;
        _string = @string;

        return this;
    }

    public Parent Execute()
    {
        _logger.LogInformation(_string);

        return _entity;
    }
}