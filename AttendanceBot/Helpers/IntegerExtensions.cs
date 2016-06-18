using System;
using System.Linq;

namespace AttendanceBot.Helpers
{
    public static class IntegerExtensions
    {
        public static string Lines(this int noOfLines)
        {
            return string.Join("", Enumerable.Repeat(Environment.NewLine, noOfLines));
        }
    }
}