using System;
using Fclp;
using Licensing.Cli.Service;
using Licensing.Commons.Data;
using Licensing.Commons.Service;

namespace Licensing.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!new CliParser().Parse(args, out var product, out var license));
            
            var exportService = new ExportService();

            Console.WriteLine(exportService.Export(product, license));
        }
    }
}
