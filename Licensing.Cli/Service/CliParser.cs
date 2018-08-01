using System;
using System.IO;
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
        private bool _keySet;
        private string _keyPath = null;
        
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
                .Callback(privatKey =>
                {
                    _product.PrivateKey = privatKey;
                    _keySet = true;
                });

            _parser.Setup<string>('p', "key-path")
                .Callback(path => _keyPath = path);

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
                });

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
                    if (!_keySet && String.IsNullOrEmpty(_keyPath))
                    {
                        Console.WriteLine("No key provided, please add a key!");
                        return false;
                    }
                    if (_keySet && !String.IsNullOrEmpty(_keyPath))
                    {
                        Console.WriteLine("Key and path to key was set, please provide only one of these options!");
                        return false;
                    }
                    if (!_keySet)
                    {
                        if (!File.Exists(_keyPath))
                        {
                            Console.WriteLine("Key path doesn't point to an existing file");
                            return false;
                        }

                        try
                        {
                            _product.PrivateKey = File.ReadAllText(_keyPath);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error happened while reading the key file\n" +
                                              "Detailed error message:\n" +
                                              e);
                            return false;
                        }
                    }
                    
                    product = _product;
                    license = _license;
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An unknown error happed!\n" +
                                  "Detailed error message:\n" +
                                  e);
                return false;
            }
        }
    }
}