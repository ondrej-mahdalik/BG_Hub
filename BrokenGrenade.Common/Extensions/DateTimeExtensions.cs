using System.Globalization;
namespace BrokenGrenade.Common.Extensions;

public static class DateTimeExtensions
{
    public static string ToDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("dddd d. MMMM yyyy H:mm", CultureInfo.CreateSpecificCulture("cs-CZ"));
    }
    
    public static string ToDateString(this DateTime dateTime)
    {
        return dateTime.ToString("dddd d. MMMM yyyy", CultureInfo.CreateSpecificCulture("cs-CZ"));
    }
}
