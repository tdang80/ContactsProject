using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;

namespace Billback.DataLayer
{
    public class ADinfo
    {
        string LDAP_Server = ConfigurationManager.AppSettings["LDAP_Server"];
        string LDAP_UserName = ConfigurationManager.AppSettings["LDAP_UserName"];
        string LDAP_Password = ConfigurationManager.AppSettings["LDAP_Password"];

        #region Private Values
        private string _samAccount;
        private string _name;
        private string _office;
        private string _title;
        private string _email;
        private string _phone;
        private string _hasDirectReports;
        #endregion

        #region Properties
        public string SamAccount
        { get { return _samAccount; } }

        public string Name
        { get { return _name; } }

        public string Office
        { get { return _office; } }

        public string Title
        { get { return _title; } }

        public string Email
        { get { return _email; } }

        public string Phone
        { get { return _phone; } }

        public string HasDirectReports
        { get { return _hasDirectReports; } }
        #endregion

        #region Constructors
        public ADinfo(string userID)
        {
            _samAccount = userID;
            if (!string.IsNullOrWhiteSpace(_samAccount))
                Load();
        }

        public ADinfo(string userID, string name, string office, string title, string email, string phone, string hasDirectReports)
        {
            _samAccount = userID;
            _name = name;
            _office = office;
            _title = title;
            _email = email;
            _phone = phone;
            _hasDirectReports = hasDirectReports;
        }
        #endregion

         #region Methods
        private void Load()
        {
            // find the userid in the AD
            string ldap = LDAP_Server;
            System.DirectoryServices.DirectoryEntry colleagues = new System.DirectoryServices.DirectoryEntry(ldap, LDAP_UserName, LDAP_Password);
            System.DirectoryServices.DirectorySearcher searcher = new System.DirectoryServices.DirectorySearcher(colleagues);
            searcher.Filter = "(&(objectClass=user)(samAccountName=" + _samAccount + "))";
            searcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            searcher.PageSize = 9999999;
            searcher.CacheResults = true;

            System.DirectoryServices.SearchResultCollection results = null;

            results = searcher.FindAll();

            if (results.Count > 0)
            {
                System.DirectoryServices.DirectoryEntry entry = results[0].GetDirectoryEntry();
                _name = GetProperty(entry, "displayName");
                _office = GetProperty(entry, "physicalDeliveryOfficeName");
                _title = GetProperty(entry, "title");
                _email = GetProperty(entry, "mail");
                _phone = GetProperty(entry, "telephoneNumber");
                _hasDirectReports = GetProperty(entry, "extensionAttribute5");
            }
        }

        private string GetProperty(System.DirectoryServices.DirectoryEntry entry, string propertyName)
        {
            string value = string.Empty;

            try
            {
                if (entry.Properties.Contains(propertyName))
                    value = Convert.ToString(entry.Properties[propertyName].Value);
            }
            catch
            {
                value = string.Empty;
            }

            return value;
        }
        #endregion
    }
}