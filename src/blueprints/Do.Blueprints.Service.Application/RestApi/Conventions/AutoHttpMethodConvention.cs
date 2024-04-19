﻿using Do.RestApi.Configuration;
using System.Text.RegularExpressions;

namespace Do.RestApi.Conventions;

public class AutoHttpMethodConvention(
    Regex? get = default,
    Regex? delete = default,
    Regex? put = default,
    Regex? patch = default
) : IApiModelConvention<ActionModelContext>
{
    Regex _get = get ?? Regexes.GetMethod();
    Regex _delete = delete ?? Regexes.DeleteMethod();
    Regex _put = put ?? Regexes.PutMethod();
    Regex _patch = patch ?? Regexes.PatchMethod();

    public void Apply(ActionModelContext context)
    {
        if (_get.IsMatch(context.Action.Id))
        {
            context.Action.Method = HttpMethod.Get;
        }
        else if (_delete.IsMatch(context.Action.Id))
        {
            context.Action.Method = HttpMethod.Delete;
        }
        else if (_put.IsMatch(context.Action.Id))
        {
            context.Action.Method = HttpMethod.Put;
        }
        else if (_patch.IsMatch(context.Action.Id))
        {
            context.Action.Method = HttpMethod.Patch;
        }
    }
}