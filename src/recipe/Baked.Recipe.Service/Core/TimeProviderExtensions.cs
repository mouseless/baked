namespace System;

public static class TimeProviderExtensions
{
    public static DateTime GetNow(this TimeProvider timeProvider) =>
        timeProvider.GetLocalNow().DateTime;
}