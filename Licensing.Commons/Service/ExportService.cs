using System;
using System.Linq;
using Licensing.Commons.Data;
using Licensing.Commons.Utility;

namespace Licensing.Commons.Service
{
    public class ExportService : IExportService
    {
        public string Export(Product product, License license)
        {
            var generator = new LicenseGenerator(product.PrivateKey);
            var expiration = license.ExpirationDate.GetValueOrDefault(DateTime.MaxValue);
            return generator.Generate(license.OwnerName, license.ID, expiration,
                license.Data.ToDictionary(userData => userData.Key, userData => userData.Value), license.LicenseType);
        }
    }
}