using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Licensing.Commons.Extension;

namespace Licensing.Commons.Data
{
    [DataContract(Name = "Product", Namespace = "http://schemas.hibernatingrhinos.com/")]
    public class Product
    {
            public Product()
            {
                var key = RSA.Create();
                Id = Guid.NewGuid();
                IssuedLicenses = new ObservableCollection<License>();
                PublicKey = key.ToXmlStringCustom(false);
                PrivateKey = key.ToXmlStringCustom(true);
            }

            [DataMember]
            public Guid Id { get; set; }

            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public string PrivateKey { get; set; }

            [DataMember]
            public string PublicKey { get; set; }

            [DataMember]
            public ObservableCollection<License> IssuedLicenses { get; set; }
    }
}