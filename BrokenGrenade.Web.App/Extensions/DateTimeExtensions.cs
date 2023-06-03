namespace BrokenGrenade.Web.App.Extensions;

public static class DateTimeExtensions
{
    public static string ToShortString(this DateTime dateTime)
    {
        return dateTime.ToString("d. M. yyyy H:mm");
    }
}
