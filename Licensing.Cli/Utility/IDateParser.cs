using System;

namespace Licensing.Cli.Utility
{
    public interface IDateParser
    {
        bool Parse(string s, out DateTime date);
    }
}