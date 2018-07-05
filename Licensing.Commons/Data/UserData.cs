using System.Runtime.Serialization;

namespace Licensing.Commons.Data
{
    [DataContract(Name = "UserData", Namespace = "http://schemas.hibernatingrhinos.com/")]
    public class UserData
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}