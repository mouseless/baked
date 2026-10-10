namespace Baked.Business;

[AttributeUsage(AttributeTargets.Parameter)]
public class Paging(Paging.Role roleOption)
    : Attribute()
{
    public Role RoleOption { get; set; } = roleOption;

    public bool IsTake => RoleOption == Role.Take;
    public bool IsSkip => RoleOption == Role.Skip;

    public enum Role
    {
        Take,
        Skip
    }
}