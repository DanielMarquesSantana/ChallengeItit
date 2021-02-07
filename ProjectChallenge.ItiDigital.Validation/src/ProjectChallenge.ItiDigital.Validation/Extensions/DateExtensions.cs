using System;
using System.Globalization;

namespace ProjectChallenge.ItiDigital.Validation
{
    public static class DateExtensions
    {
        public static string ToISOString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
    }
}
