using Baked.Runtime.Configuration;

namespace Baked.DataAccess;

public class PersistenceConfiguration
{
    public Setting<bool> AutoExportSchema { get; set; } = false;
    public Setting<bool> AutoUpdateSchema { get; set; } = false;
}