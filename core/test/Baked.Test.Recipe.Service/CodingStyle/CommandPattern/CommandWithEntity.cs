using Baked.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Test.CodingStyle.CommandPattern;

public class CommandWithEntity(ILogger<CommandWithEntity> _logger)
{
    Entity _entity = default!;
    string _string = default!;

    public CommandWithEntity With(Entity entity, string text)
    {
        _entity = entity;
        _string = text;

        return this;
    }

    public Entity Execute()
    {
        _logger.LogInformation(_string);

        return _entity;
    }
}