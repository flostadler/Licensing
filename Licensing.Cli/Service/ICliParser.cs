using Licensing.Commons.Data;

namespace Licensing.Cli.Service
{
    public interface ICliParser
    {
        bool Parse(string[] args, out Product product, out License license);
    }
}