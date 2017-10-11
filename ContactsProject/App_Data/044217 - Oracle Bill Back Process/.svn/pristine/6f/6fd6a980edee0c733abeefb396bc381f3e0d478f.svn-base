using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Billback.Global;
using Billback.DataLayer;


namespace Billback.Webforms
{
    public partial class AddClaim : System.Web.UI.Page
    {
        public DataTable dtClaimSearchData = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
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
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            //add new claim to database and refresh grid
            string FailField = "";
            string Reason = "";
            if (ValidatePass( out FailField, out Reason ) == true)
            {


                if (dtClaimSearchData.Rows.Count == 0)
                {
                  dtClaimSearchData = BuildEmptyGridColumns();
                }
                dtClaimSearchData.PrimaryKey = new DataColumn[] { dtClaimSearchData.Columns["GUID"] };
                DataRow drClaim = dtClaimSearchData.NewRow();
                string strFromDate =  Common.GetPreviousMonthBeginDate();
                string strThruDate = Common.GetPreviousMonthEndDate();
                               
                drClaim["Claim#"] = txtClaimNumber.Text.Trim();
                drClaim["ClientID"] = txtClientID.Text.Trim();
                drClaim["ProcessingUnit"] = txtProcessingUnit.Text.Trim();
               // drClaim["CLAIM_UID"] = Common.GetUniqueID();
                drClaim["CLAIM_UID"] = GetClaimUIDForClaimClientID(txtClaimNumber.Text.Trim(), txtClientID.Text.Trim());
                drClaim["DOL"] = strFromDate;
                drClaim["DATEFROM"] = strFromDate;
                drClaim["DATETHRU"] = strThruDate;
                drClaim["GUID"] = Common.GetNewGUID();
                dtClaimSearchData.Rows.Add(drClaim);
                Session["dtClaimsSearchData"] = dtClaimSearchData;
                lblStatus.Visible = false;
                Session["PopUpClosed"] = true;
                Session["NewSvcType"] = false;
                ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "closePage", "<script type='text/javascript'>closeAndRefresh();</script>");

               // this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            else
            {
                lblStatus.Text =  Reason;
               
                lblStatus.Visible = true;
            }
        }

       

        private DataTable BuildEmptyGridColumns()
        {
            DataTable dtGridData = new DataTable();
            DataColumn column = new DataColumn();
            DataColumn[] keys = new DataColumn[1];
            column.ColumnName = "GUID";
            column.DataType = System.Type.GetType("System.String");

            dtGridData.Columns.Add("CLIENTID");
            dtGridData.Columns.Add("CLAIM#");
            dtGridData.Columns.Add("CLMNTFNAME");
            dtGridData.Columns.Add("CLMNTLNAME");
            dtGridData.Columns.Add("DOL", Type.GetType("System.DateTime"));
            dtGridData.Columns.Add("BINVOICE");
            dtGridData.Columns.Add("DATEFROM",Type.GetType("System.DateTime"));
            dtGridData.Columns.Add("DATETHRU", Type.GetType("System.DateTime"));            
            dtGridData.Columns.Add("PROCESSINGUNIT");
            dtGridData.Columns.Add("PAYCODE");
            dtGridData.Columns.Add("ALLOCATIONAMOUNT");
            dtGridData.Columns.Add("CLAIM_UID");           
            dtGridData.Columns.Add("VENDOR_ID");
            dtGridData.Columns.Add("DATA_SET");
            dtGridData.Columns.Add("CLAIM_TYPE");
            dtGridData.Columns.Add("STATE_PAYROLL");
            dtGridData.Columns.Add("STATE_A");
            dtGridData.Columns.Add("SUB_TYPE");
            dtGridData.Columns.Add("LINE_CODE");
            dtGridData.Columns.Add(column);    //GUID
            
            keys[0] = column;
            dtGridData.PrimaryKey = keys;

            return dtGridData;
        }

        private bool ValidatePass( out string FailField, out string Reason)
        {
            bool Pass = true;
            FailField = "";
            Reason = "";

            if (String.IsNullOrWhiteSpace(txtClaimNumber.Text))
            {
                Pass = false;
                FailField = " Claim# ";
                Reason += " - Claim# cannot be Blank - ";
            }
            else
            {
                txtClaimNumber.Text = txtClaimNumber.Text.Trim();
            }

            if (String.IsNullOrWhiteSpace(txtClientID.Text))
            {
                Pass = false;
                FailField = " Client ID ";
                Reason += " - Client ID cannot be Blank - ";
            }
            else
            {
                txtClientID.Text = txtClientID.Text.Trim();
            }

            if (String.IsNullOrWhiteSpace(txtProcessingUnit.Text))
            {
                Pass = false;
                FailField = " Processing Unit ";
                Reason += " - Processing Unit Cannot be Blank - ";
            }
            else
            {
                txtProcessingUnit.Text = txtProcessingUnit.Text.Trim();
            }

            if (Pass == true) // if above passed then get claim uid using claim# and client id
            {

                List<string> ClaimList = new List<string>();
                ClaimList.Add(txtClaimNumber.Text.Trim());
                DataTable dt = GetClaimUIDForClaim(ClaimList);


                if (dt.Rows.Count == 0)
                {
                    Pass = false;
                    FailField = " CLAIM UID ";
                    Reason += " - CLAIM UID Not Found for Claim# - " + txtClaimNumber.Text;
                }
                else
                {
                    bool foundClientID = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CLIENT_ID"].ToString() == txtClientID.Text.Trim())
                        {
                            foundClientID = true;
                        }
                    }

                    if (foundClientID == false)
                    {
                        Pass = false;
                        FailField = " CLIENT ID ";
                        Reason += " - CLIENT ID Not Found for Claim# - " + txtClaimNumber.Text;
                    }
                }

            }

        

            return Pass;


        }

        private DataTable GetClaimUIDForClaim(List<string> ClaimList)
        {
           
            Billback.DataLayer.Billback obj = new Billback.DataLayer.Billback();                  
            DataTable dt = obj.GetClaimInfoPRD(ClaimList);

            return dt;

        }

        private string GetClaimUIDForClaimClientID(string Claim, string ClientID)
        {
            Billback.DataLayer.Billback obj = new Billback.DataLayer.Billback();
            List<string> ClaimList = new List<string>();
            ClaimList.Add(Claim);
            DataTable dt = obj.GetClaimInfoPRD(ClaimList);
            string ClaimUID = "";
             foreach (DataRow dr in dt.Rows)
             {
                 if (dr["CLIENT_ID"].ToString() == ClientID)
                 {
                     ClaimUID = dr["CLAIM_UID"].ToString();
                 }
             }
             return ClaimUID;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}