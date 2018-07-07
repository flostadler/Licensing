using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Licensing.Cli.Utility
{
    public class DateParser : IDateParser
    {
        private static readonly string[] Formats =
        {
            "D",
            "d",
            "F",
            "f",
            "G",
            "g",
            "M/d/yy",
            "MM/dd/yyyy",
            "yy-MM-dd",
            "yy-MMM-dd ddd",
            "yyyy-M-d dddd",
            "yyyy MMMM dd"
        };

        public string Examples
        {
            get
            {
                var date = DateTime.Now;

                var builder = new StringBuilder();
                Formats.ToList().ForEach(x => builder.Append($"\t- {date.ToString(x)}\n"));

                return builder.ToString();
            }
        }

        public bool Parse(string s, out DateTime date)
        {
            return DateTime.TryParseExact(s, Formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
        }
    }
}