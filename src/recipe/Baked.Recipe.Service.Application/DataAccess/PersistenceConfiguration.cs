﻿using Baked.Configuration;
using FluentNHibernate.Cfg.Db;

namespace Baked.DataAccess;

public class PersistenceConfiguration
{
    public IPersistenceConfigurer Configurer { get; set; } = SQLiteConfiguration.Standard.InMemory();
    public Setting<bool> AutoExportSchema { get; set; } = false;
    public Setting<bool> AutoUpdateSchema { get; set; } = false;
}