namespace Do.Business;

public interface ICasts<TFrom, TTo>
{
    TTo To(TFrom from);
}