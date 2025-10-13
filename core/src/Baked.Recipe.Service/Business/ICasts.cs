namespace Baked.Business;

public interface ICasts<TFrom, TTo>
{
    TTo To(TFrom from);
}