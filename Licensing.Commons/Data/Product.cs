using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security.Cryptography;

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
                PublicKey = key.ToXmlString(false);
                PrivateKey = key.ToXmlString(true);
            }

            [DataMember]
            public Guid Id { get; set; }

            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public string PrivateKey { get; private set; }

            [DataMember]
            public string PublicKey { get; private set; }

            [DataMember]
            public ObservableCollection<License> IssuedLicenses { get; private set; }
    }
}