using Do.Testing;

namespace Do;

public static class ServiceSpecDateTimeExtensions
{
    public static DateTime ADateTime(this Stubber _,
        int year = 2023,
        int month = 9,
        int day = 17,
        int hour = 13,
        int minute = 29,
        int second = 00
    ) => new(year, month, day, hour, minute, second);
}
