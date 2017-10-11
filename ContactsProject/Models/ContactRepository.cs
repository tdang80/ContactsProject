using ContactsProject.App_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace ContactsProject.Models
{
    public class ContactRepository : IContactRepository
    {       
        public ContactRepository()
        {            
        }

        #region GetContactList
        /// <summary>
        /// Deserialize the Contacts.xml file and return the list of contacts.
        /// </summary>
        /// <returns>List of ContactModel</returns>
        public List<ContactModel> GetContactList()
        {
            List<ContactModel> allContacts = new List<ContactModel>();

            try
            {               
                XmlSerializer deserializer = new XmlSerializer(typeof(Contacts));
                string xmlPath = HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml");              
                TextReader reader = new StreamReader(xmlPath);
                object obj = deserializer.Deserialize(reader);
                Contacts xmlData = (Contacts)obj;
                xmlData.ContactList.Sort((p, q) => p.LastName.CompareTo(q.LastName));
                allContacts = xmlData.ContactList;
                reader.Close();              
            }
            catch (Exception e)
            {
                throw e;
            }
            return allContacts;
        }
        #endregion


        #region GetContactById
        /// <summary>
        /// Return a specific contact based on the contact id
        /// </summary>
        /// <param name="ContactId">int</param>
        /// <returns>ContactModel</returns>
        public ContactModel GetContactById(int ContactId)
        {
            RefData refData = RefDataManager.Get();
            List<ContactModel> contactsList = refData.ContactList;
            return contactsList.Find(item => item.ContactId == ContactId);
        }
        #endregion

    }
}