using ContactsProject.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactsProject.Controllers
{
    public class RefController : Controller
    { 
        public RefController()
        { }

        #region GetContactList
        /// <summary>
        /// Get the whole contact list
        /// </summary>
        /// <returns>contact list</returns>
        public List<ContactModel> GetContactList()
        {
            /*Per requirements, user should NOT be able to modify contacts. 
             * That means contact list remain unchange. It only needs to call once 
             * and save to refData for later usage. It doesn't need to be 
             * deserialize the contact.xml file every single time
             * user hits the Index page.
             */            
            ContactRepository contactRep = new ContactRepository();
            return contactRep.GetContactList();
        }
        #endregion GetContactList
    }
}