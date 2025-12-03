namespace Baked.Ui;

public static class ActionComposables
{
    public static LocalAction UseRedirect(string route) =>
        Actions.Local("useRedirect", o => o.Options = Datas.Inline(new { route }));
}