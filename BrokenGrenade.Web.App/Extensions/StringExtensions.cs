namespace BrokenGrenade.Web.App.Extensions;

public static class StringExtensions
{
    public static int Rows(this string? text)
    {
        var rows = Math.Max(text?.Split('\n').Length ?? 1, text?.Split('\r').Length ?? 1);
        return Math.Max(rows, 3); // Minimum of 3 rows
    }
}
