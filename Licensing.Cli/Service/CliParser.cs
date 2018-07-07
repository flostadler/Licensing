using System;
using System.Runtime.CompilerServices;
using Fclp;
using Licensing.Cli.Utility;
using Licensing.Commons.Data;

namespace Licensing.Cli.Service
{
    public class CliParser : ICliParser
    {
        private readonly FluentCommandLineParser _parser;
        private readonly Product _product;
        private readonly License _license;
        
        public CliParser()
        {
            _product = new Product();
            _license = new License();

            _parser = new FluentCommandLineParser
            {
                IsCaseSensitive = false
            };

            _parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            _parser.Setup<string>('k', "key")
                .Callback(privatKey => _product.PrivateKey = privatKey)
                .Required();

            _parser.Setup<string>('n', "name")
                .Callback(name => _license.OwnerName = name)
                .Required();

            _parser.Setup<string>('i', "id")
                .Callback(s =>
                {
                    if (!Guid.TryParse(s, out var guid))
                    {
                        Console.WriteLine($"\"{s}\" is not a valid Id!\n" +
                                          "Please use either of these formats for Ids:\n" +
                                          $"\t{Guid.Empty:N}\n" +
                                          $"\t{Guid.Empty:B}\n" +
                                          $"\t{Guid.Empty:D}\n" +
                                          $"\t{Guid.Empty:P}\n" +
                                          $"\t{Guid.Empty:X}\n");
                        
                        throw new Exception();
                    }

                    _license.ID = guid;
                })
                .Required();

            _parser.Setup<string>('e', "exp")
                .Callback(exp =>
                {
                    var dateParser = new DateParser();
                    if (!dateParser.Parse(exp, out var date))
                    {
                        Console.WriteLine($"\"{exp}\" is not a valid Date!\n" +
                                          "Please use either of these formats for Dates:\n" +
                                          $"{dateParser.Examples}\n");
                        
                        throw new Exception();
                    }

                    _license.ExpirationDate = date;
                });

            _parser.Setup<LicenseType>('l', "lic")
                .Callback(lic => _license.LicenseType = lic)
                .Required();
        }
        
        public bool Parse(string[] args, out Product product, out License license)
        {
            product = null;
            license = null;

            try
            {
                var result = _parser.Parse(args);

                if (result.HasErrors)
                {
                    Console.WriteLine(result.ErrorText);
                    return false;
                }
                else
                {
                    product = _product;
                    license = _license;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}