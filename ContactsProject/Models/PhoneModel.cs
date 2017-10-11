using System.Collections.Generic;
using System.Xml.Serialization;

namespace ContactsProject.Models
{
    public class Phones
    {
        [XmlElement("Phone")]
        public List<PhoneModel> PhoneList { get; set; }
    }

    public class PhoneModel
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
    }
}