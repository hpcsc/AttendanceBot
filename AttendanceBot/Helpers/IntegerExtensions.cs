using System;
using System.Linq;

namespace AttendanceBot.Helpers
{
    public static class IntegerExtensions
    {
        public static string Lines(this int noOfLines)
        {
            if (noOfLines <= 0)
            {
                throw new ArgumentException("noOfLines");
            }

            return string.Join("", Enumerable.Repeat(Environment.NewLine, noOfLines));
        }
    }
}