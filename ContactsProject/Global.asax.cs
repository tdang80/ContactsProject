using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ContactsProject.Controllers;
using ContactsProject.App_Code;

namespace ContactsProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LoadRefData();
        }

        private void LoadRefData()
        {
            var refController = new RefController();
            RefData refData = RefDataManager.Get();
            refData.ContactList =   refController.GetContactList();
            RefDataManager.Save(refData);
        }
    }
}
