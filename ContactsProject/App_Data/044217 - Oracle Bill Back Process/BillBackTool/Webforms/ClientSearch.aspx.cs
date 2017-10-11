using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Billback.DataLayer;
using System.Data;
using Infragistics.Web.UI.GridControls;


namespace Billback.Webforms
{
    public partial class ClientSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStatusMsg.Visible = true;
                Session["ClientID"] = null;
            }

        }


        
        
        protected void btnSearchClientName_Click(object sender, EventArgs e)
        {
            //code to search Client by name
            DataLayer.Billback obj = new DataLayer.Billback();

            string strClientName = txtClientName.Text;
            string exMessage = "";
            DataTable dtClient = new DataTable();
            if (!String.IsNullOrWhiteSpace(strClientName))
            {
                strClientName = strClientName.Trim().ToUpper();
                dtClient = obj.SearchClientData_By_Name_SP(strClientName, out exMessage);
              //  grdClientSelect.DataSource = dtClient;
              //  grdClientSelect.DataBind();

                grdClientSelect2.DataSource = dtClient;
                grdClientSelect2.DataBind();

                updClientSearch.Update();
                txtClientName.Text = "";
                lblStatusMsg.Text = "Row Count: " + dtClient.Rows.Count;
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Blue;    
            }
            else
            {
                lblStatusMsg.Text = "Client Name Cannot be Blank";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;          
            }
            
        }


        protected void btnSearchClientID_Click(object sender, EventArgs e)
        {
            //code to search Client by ID
            DataLayer.Billback obj = new DataLayer.Billback();

            string strClientID = txtClientID.Text;
            string exMessage = "";
            DataTable dtClient = new DataTable();
            if (!String.IsNullOrWhiteSpace(strClientID))
            {
                dtClient = obj.SearchClientData_By_ID_SP(strClientID, 0, out exMessage);
                grdClientSelect2.DataSource = dtClient;
                grdClientSelect2.DataBind();
                updClientSearch.Update();
                txtClientID.Text = "";
                lblStatusMsg.Text = "Row Count: " + dtClient.Rows.Count;
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Blue;    
            }
            else
            {
                lblStatusMsg.Text = "Client ID Cannot be Blank";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
            }
        }

     

        protected void grdClientSelect_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
        {

            string strContNum = e.CurrentSelectedRows[0].DataKey[0].ToString();

            lblSelectedClient.Text = strContNum;
            lblSelectedClient.ForeColor = System.Drawing.Color.White;
            lblSelectedClient.BackColor = System.Drawing.Color.Green;
           
            updClientSearch.Update();
            
        }

   

        protected void grdClientSelect2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdClientSelect2.SelectedRow;
            string strContNum = row.Cells[1].Text; // Client ID
            string strName = row.Cells[2].Text; // Client Name

            string exMsg = "";
            lblSelectedClient.Text = "Selected Client ID: " + strContNum + "     Client Name: " + strName;
            lblSelectedClient.ForeColor = System.Drawing.Color.White;
            lblSelectedClient.BackColor = System.Drawing.Color.Green;
            lblStatusMsg.Visible = false;

            Session["ClientID"] = strContNum;
            Session["ClientName"] = strName;
            updClientSearch.Update();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Session["ClientID"] != null)
            {
                Session["PopUpClosed"] = true;
                Session["NewSvcType"] = false;
                Session["ClientSearch"] = true;
                ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "closePage", "<script type='text/javascript'>closeAndRefresh();</script>");
            }
            else
            {
                lblStatusMsg.Text = "Please Select a Client";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}