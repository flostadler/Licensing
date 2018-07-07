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
            if (!new CliParser().Parse(args, out var product, out var license))
            {
                return;
            }
            
            var exportService = new ExportService();

            try
            {
                Console.WriteLine(exportService.Export(product, license));
            }
            catch (Exception e)
            {
                if (e is System.Security.Cryptography.CryptographicException)
                {
                    Console.WriteLine("The key is not complete\n" +
                                      "\tMaybe the public key was provided!");
                }
                else if (e is System.Xml.XmlException)
                {
                    Console.WriteLine("The key is not a valid RSA xml key!");
                }
                else
                {
                    Console.WriteLine($"Unknown error\nError message:\n{e.Message}");
                }
                
            }
        }
    }
}
