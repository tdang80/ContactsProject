using ContactsProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ContactsProject.App_Code;

namespace ContactsProject.Controllers
{
    public class HomeController : Controller
    {
        #region Index
        /// <summary>
        /// Display the contact list by 10
        /// </summary>
        /// <param name="page">int</param>
        /// <returns>contact list</returns>
        public ActionResult Index(int page = 0)
        {
            ViewBag.Message = "Your contact page.";
            
            RefData refData = RefDataManager.Get();
            IEnumerable<ContactModel> contactsList = refData.ContactList;

            //Pagination calcuation
            int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["Pagination"]);
            var count = contactsList.Count();
            var data = contactsList.Skip(page * PageSize).Take(PageSize).ToList();
            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;

            return this.View(data);
        }
        #endregion

        #region ViewContactDetails
        /// <summary>
        /// View the detail of a contact
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="page">int</param>
        /// <returns>Contact details</returns>
        [HttpGet]
        public ActionResult ViewContactDetails(int id, int page)
        {
            ContactRepository contactRep = new ContactRepository();
            ContactModel contact = contactRep.GetContactById(id);
            contact.Phones.PhoneList.Sort((p, q) => p.PhoneType.CompareTo(q.PhoneType));
            this.ViewBag.Page = page;
            return View(contact);
        }
        #endregion
    }
}