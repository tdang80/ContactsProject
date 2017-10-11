using System.Collections.Generic;
using System.Xml.Serialization;

namespace ContactsProject.Models
{
    public class Contacts
    {
        [XmlElement("Contact")]
        public List<ContactModel> ContactList { get; set; }
    }

    public class ContactModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        [XmlElement("Address")]
        public AddressModel Address { get; set; }

        [XmlElement("Phones")]
        public Phones Phones { get; set; }
    }
}