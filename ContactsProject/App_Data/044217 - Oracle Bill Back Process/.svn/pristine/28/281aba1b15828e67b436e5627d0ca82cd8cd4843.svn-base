using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Billback.DataLayer;
using Infragistics.Web.UI.GridControls;

namespace Billback.Webforms
{
    public partial class ChildGridPage : System.Web.UI.Page
    {
        public DataTable dtClaimSearchData = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            // LoadGridSampleData();

            if (!IsPostBack)
            {
                // get session state data if not null so we can add claim record to it
                if (Session["dtClaimsSearchData"] != null)
                {
                    dtClaimSearchData = (DataTable)Session["dtClaimsSearchData"];
                }
                else
                {
                    //create the session variable if doesn't exist and add claim to it
                }


                // GET DATA FROM VIAONE.CLAIM
                //Billback.DataLayer.Billback obj = new Billback.DataLayer.Billback();
                //DataTable dt = obj.GetClaimData();

                grdClaimSelect.AjaxIndicator.Enabled = Infragistics.Web.UI.DefaultableBoolean.True;
                this.grdClaimSelect.AjaxIndicator.Text = "Please wait...";
                this.grdClaimSelect.AjaxIndicator.CssClass = "AjaxInd";
                this.grdClaimSelect.AjaxIndicator.Location = Infragistics.Web.UI.RelativeLocation.AboveCenter;

                // Set the location of the Ajax Indicator specific to the control.
                this.grdClaimSelect.AjaxIndicator.RelativeToControl = Infragistics.Web.UI.DefaultableBoolean.True;

                // Blocks or Grey out the control during Ajax call
                this.grdClaimSelect.AjaxIndicator.BlockArea = Infragistics.Web.UI.AjaxIndicatorBlockArea.Control;

                grdClaimSelect.DataSource = dtClaimSearchData;
                grdClaimSelect.DataBind();
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {

            DataTable dtClaimsdata = new DataTable();
            DataTable dtClaimsTemp = new DataTable();
            dtClaimsTemp = (DataTable)Session["dtClaimsSearchData"];
            dtClaimsdata = dtClaimsTemp.Clone();
            DataRow dr;
            // iterate through the grid rows
            foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)
            {
                int ColCount = gridRecord.Items.Count;

                dr = dtClaimsdata.NewRow();

                dr["CLAIM_UID"] = gridRecord.Items.FindItemByKey("CLAIM_UID").Text;
                dr["CLAIM#"] = gridRecord.Items.FindItemByKey("CLAIM#").Text;
                dr["CLIENTID"] = gridRecord.Items.FindItemByKey("CLIENTID").Text;
                dr["CLMNTFNAME"] = gridRecord.Items.FindItemByKey("CLMNTFNAME").Text;
                dr["CLMNTLNAME"] = gridRecord.Items.FindItemByKey("CLMNTLNAME").Text;
                dr["DOL"] = gridRecord.Items.FindItemByKey("DOL").Text;
                dr["BINVOICE"] = gridRecord.Items.FindItemByKey("BINVOICE").Text;
                dr["DATEFROM"] = gridRecord.Items.FindItemByKey("DATEFROM").Text;
                dr["DATETHRU"] = gridRecord.Items.FindItemByKey("DATETHRU").Text;
                dr["PROCESSINGUNIT"] = gridRecord.Items.FindItemByKey("PROCESSINGUNIT").Text;
                dr["PAYCODE"] = gridRecord.Items.FindItemByKey("PAYCODE").Text;
                dr["ALLOCATIONAMOUNT"] = gridRecord.Items.FindItemByKey("ALLOCATIONAMOUNT").Text;
                dr["VENDOR_ID"] = gridRecord.Items.FindItemByKey("VENDOR_ID").Text;
                dr["DATA_SET"] = gridRecord.Items.FindItemByKey("DATA_SET").Text;
                dr["CLAIM_TYPE"] = gridRecord.Items.FindItemByKey("CLAIM_TYPE").Text;
                dr["STATE_PAYROLL"] = gridRecord.Items.FindItemByKey("STATE_PAYROLL").Text;
                dr["STATE_A"] = gridRecord.Items.FindItemByKey("STATE_A").Text;
                dr["SUB_TYPE"] = gridRecord.Items.FindItemByKey("SUB_TYPE").Text;
                dr["LINE_CODE"] = gridRecord.Items.FindItemByKey("LINE_CODE").Text;
                dr["GUID"] = gridRecord.Items.FindItemByKey("GUID").Text;
                dtClaimsdata.Rows.Add(dr);



            }
            Session["dtClaimsSearchData"] = dtClaimsdata;

            //add headers of grid first
            //for (int i = 0; i < grdClaimSelect.Columns.Count; i++)
            //{
            //    dtNewGridData.Columns.Add(grdClaimSelect.Columns[i].Key.ToString());
            //}

            ////add data rows
            //foreach (GridRecord row in grdClaimSelect.Rows)
            //{
            //    DataRow dr = dtNewGridData.NewRow();
            //    for (int j = 0; j < grdClaimSelect.Columns.Count; j++)
            //    {
            //        dr[grdClaimSelect.Columns[j].Key.ToString()] = row.Items[j].Text.ToString();
            //    }

            //    dtNewGridData.Rows.Add(dr);
            //}

            //Session["dtClaimsSearchData"] = dtNewGridData;

         //   Response.Redirect("ClaimSearch.aspx");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
          //  ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.opener.location.reload(true);self.close();", true);
        }

        protected void grdClaimSelect_InitializeRow(object sender, RowEventArgs e)
        {
            Image image = e.Row.Items[0].FindControl("imgValidationError") as Image;
            List<string> ClaimUIDList = new List<string>();

            if (Session["ListOfGUIDErrored"] != null)
            {
                ClaimUIDList = (List<string>)Session["ListOfGUIDErrored"];
            }
            string saveClaimUID = e.Row.Items[grdClaimSelect.Columns.Count - 6].Text;


            if (ClaimUIDList.IndexOf(saveClaimUID) != -1)  // if claim uid found in list that means error flag set for that claim uid                    
            {

                image.ImageUrl = "~/Images/ErrorImgX.jpg";

                //use this to highlight errored cell, below highlights 5 column --working good
                //e.Row.Items[5].CssClass = "ErroredCell";

            }
            else
            {
                image.ImageUrl = "";
            }
        }
    
    }
}