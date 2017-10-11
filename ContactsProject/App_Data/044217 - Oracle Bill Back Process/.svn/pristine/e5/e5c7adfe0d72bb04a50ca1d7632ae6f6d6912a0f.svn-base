using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Billback.DataLayer;
using System.Data;
using System.Web.Script.Serialization;
namespace Billback
{
    /// <summary>
    /// Summary description for ClientHandler
    /// </summary>
    public class ClientHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strClientID = context.Request["term"] ?? ""; //return context.Request["term"] if it's not null otherwise return ""
            List<string> listClients = new List<string>();
            string strExMsg = "";

            DataLayer.Billback obj = new DataLayer.Billback();
            DataTable dtClients = obj.SearchClientData_By_ID_SP(strClientID, 0, out strExMsg);
            foreach (DataRow dr in dtClients.Rows)
            {
                listClients.Add(dr["CONT_NUM"].ToString());
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(listClients));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}