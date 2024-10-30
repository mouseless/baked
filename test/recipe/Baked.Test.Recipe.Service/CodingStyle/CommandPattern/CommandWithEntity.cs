using Baked.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Test.CodingStyle.CommandPattern;

public class CommandWithEntity(ILogger<CommandWithEntity> _logger)
{
    Parent _parent = default!;
    string _string = default!;

    public CommandWithEntity With(Parent parent, string text)
    {
        _parent = parent;
        _string = text;

        return this;
    }

    public Parent Execute()
    {
        _logger.LogInformation(_string);

        return _parent;
    }
}