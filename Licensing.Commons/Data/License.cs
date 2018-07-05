using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Licensing.Commons.Data
{
    [DataContract(Name = "License", Namespace = "http://schemas.hibernatingrhinos.com/")]
    public class License
    {
        public License()
        {
            ID = Guid.NewGuid();
            Data = new ObservableCollection<UserData>();
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string OwnerName { get; set; }

        [DataMember]
        public DateTime? ExpirationDate { get; set; }

        [DataMember]
        public LicenseType LicenseType { get; set; }

        [DataMember]
        public ObservableCollection<UserData> Data { get; private set; }
    }
}