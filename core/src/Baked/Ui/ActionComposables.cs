namespace Baked.Ui;

public static class ActionComposables
{
    public static LocalAction UseRedirect(string route) =>
        UseRedirect(Datas.Inline(new { route }));

    public static LocalAction UseRedirect(IData options) =>
        Actions.Local("useRedirect", o => o.Options = options);
}