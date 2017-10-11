using System.Web;

namespace ContactsProject.App_Code
{
    public class RefDataManager
    {
        #region Get
        /// <summary>
        /// Get all refData from cache
        /// </summary>
        /// <returns>RefData</returns>
        public static RefData Get()
        {
            RefData refData = (RefData)HttpContext.Current.Cache["RefData"];

            if (refData == null)
            {
                refData = new RefData();
                HttpContext.Current.Cache["RefData"] = refData;
            }
            return refData;
        }
        #endregion


        #region Save
        /// <summary>
        /// Save refData
        /// </summary>
        /// <param name="refData">RefData</param>
        public static void Save(RefData refData)
        {
            HttpContext.Current.Cache["RefData"] = refData;
        }
        #endregion
    }
}