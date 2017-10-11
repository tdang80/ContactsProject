using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Billback.Global;
using Infragistics.Web.UI.GridControls;
using Billback.DataLayer;
using Oracle.DataAccess.Client;
using Infragistics.Documents.Excel;
using OfficeOpenXml.Packaging;
using OfficeOpenXml;
using System.IO;
using Infragistics.Web.UI.ListControls;


namespace Billback.Webforms
{
    public partial class ClaimSearch : System.Web.UI.Page
    {
        public bool TCMURImportFeature = true; //use to enable import feature for TCM
        public string strUserID;
        public string strUserFName;
        public string strUserLName;
        protected void Page_Load(object sender, EventArgs e)
        {
                    
            bool PopUpClosed = false;
            bool NewSvcType = false;
            bool ClientSearch = false;
            string ClientID = "";
            string ClientName = "";
            if (Session["PopUpClosed"] != null)
            {
                PopUpClosed = (bool)Session["PopUpClosed"];
            }
            if (Session["NewSvcType"] != null)
            {
                NewSvcType = (bool)Session["NewSvcType"];
            }
            if (Session["ClientSearch"] != null)
            {
                ClientSearch = (bool)Session["ClientSearch"];
            }
            if (Session["ClientID"] != null)
            {
                ClientID = (string)Session["ClientID"];
            }
            if (Session["ClientName"] != null)
            {
                ClientName = (string)Session["ClientName"];
            }

            if ((!IsPostBack) && (PopUpClosed == false))  
            {
                ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("OPEN", "0"));
                ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("CLOSED", "1"));
               

                DisableAllButtons();
                HideAllButtons();

                SetAJAXIndicator();

                WebGrp1ReqFields.Enabled = false;
                WebGrp2OptionalFields.Enabled = false;
                WebGrp3FeeAllocFields.Enabled = false;
                btnAddClaims.Enabled = false;
                btnSearchClaims.Enabled = false;
                LoadDDLServiceType();
                ddlServiceType.SelectedValue = "-1";
                
                dtValuationDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("MM") + "/25/" + DateTime.Now.AddYears(-3).ToString("yyyy");
                dtValuationDateTo.Text = DateTime.Now.AddMonths(-1).ToString("MM") + "/25/" + DateTime.Now.Year.ToString("0000");
               // txtIncludeYrsClaim.Text = "3"; //default # of years to include for claims
                ddlDataSet.SelectedValue = "-1";
              //  txtNumValidationDays.Text = "25";

                UserAuthentication();
                

                //**NOTE on # of Valuation and # of years for claim ***
               // dtValuation = valuation day in the previous month (i.e., if the valuation date is 25 then 10/25/2016)
                //dtEventBegin = dtValuation – “Include # Yrs of Clm” (i.e., if the “Invlude # Yrs of Clm” is 3 then 10/25/2013)

                //used for testing
              //  txtClientID.Text = "2419";
                
                
                ddlIncludeClaimStatus.SelectedItemIndex = 0;
                ddlDataSet.SelectedItemIndex = 0;
                Session["PopUpClosed"] = false;
                Session["NewSvcType"] = false;
            }
            else
            {
                //if (GetPostBackControlId(Page.Page) == "")
                //{
                //    btnSearchClaims_Click(new object(), new EventArgs());
                //}

                
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnExportToExcel);
                scriptManager.RegisterPostBackControl(this.btnExportLog);
                if ((Session["dtClaimsSearchData"] != null) || (( NewSvcType == true ) || (ClientSearch == true)))
                {
                
                    DataTable dtClaimsData = new DataTable();
                    dtClaimsData = (DataTable)Session["dtClaimsSearchData"];
                    //this script is for a pop up child window closed 
                    if ((PopUpClosed == true) || (NewSvcType == true) || (ClientSearch == true))
                    {
                        if (ddlServiceType.Items.Count == 0)
                        {
                            LoadDDLServiceType();
                        }
                        string ServiceType = (string)Session["ServiceType"];
                        ddlServiceType.SelectedValue = GetServiceTypeIndex(ServiceType).ToString();

                        if (ddlServiceType.SelectedValue != "-1")
                        {
                            ddlServiceType_SelectionChanged(sender, null);
                        }
                        else
                        {
                            DisableAllButtons();
                            HideAllButtons();
                        }

                        if (ClientSearch == true)
                        {
                            txtClientID.Text = ClientID;
                            if (ClientName.Length > 50) //if client name is greater than 50, truncate to 50 chars to fit
                            {
                                lblSelectedClientName.Text = ClientName.Substring(0, 50); 
                            }
                            else
                            {
                                lblSelectedClientName.Text = ClientName;
                            }
                        }

                        Session["PopUpClosed"] = false;
                        Session["NewSvcType"] = false;
                        Session["ClientSearch"] = false;
                        Session["ClientID"] = null;
                        Session["ClientName"] = null;
                        SetAJAXIndicator();
                        Session["dtClaimsSearchData"] = dtClaimsData;
                    }

                                           
                    grdClaimSelect.DataSource = dtClaimsData;                   
                    grdClaimSelect.DataBind();
                    
                    
                    updSearchParameter.Update();
                    updPanelTab0.Update();
                    
                   
                }
                else
                {
                    grdClaimSelect.DataSource = null;

                }

            }
        }

       

        private void LoadDDLServiceType()
        {
            DataLayer.Billback obj = new DataLayer.Billback();
            string exMessage = "";
            DataTable dtServiceTypes = obj.GetAllServiceTypes(out exMessage);
            if (!String.IsNullOrWhiteSpace(exMessage))
            {
                LogActivity("APPLICATION EXCEPTION", exMessage);
            }

            ddlServiceType.Items.Clear();

            Infragistics.Web.UI.ListControls.DropDownItem listItem = new Infragistics.Web.UI.ListControls.DropDownItem();
            listItem.Text = "Select Service Type ";
            listItem.Value = "-1";
            listItem.Selected = false;
            ddlServiceType.Items.Add(listItem);
            
            foreach (DataRow dr in dtServiceTypes.Rows)
            {
                listItem = new DropDownItem();
                listItem.Text = dr["DISPLAY"].ToString();
                listItem.Value = dr["DISPLAY_ORDER_ID"].ToString();
                listItem.Key = dr["SERVICE_TYPE"].ToString();
                listItem.Selected = false;
                if (dr["SERVICE_TYPE"].ToString() == "-")
                {
                    listItem.Enabled = false; //disable separator
                }
                ddlServiceType.Items.Add(listItem);
            }

          
           
        }

        private void SetAJAXIndicator()
        {
            grdClaimSelect.AjaxIndicator.Enabled = Infragistics.Web.UI.DefaultableBoolean.True;
            this.grdClaimSelect.AjaxIndicator.Text = "Please wait...";
            this.grdClaimSelect.AjaxIndicator.CssClass = "AjaxInd";
            this.grdClaimSelect.AjaxIndicator.Location = Infragistics.Web.UI.RelativeLocation.AboveCenter;

            // Set the location of the Ajax Indicator specific to the control.
            this.grdClaimSelect.AjaxIndicator.RelativeToControl = Infragistics.Web.UI.DefaultableBoolean.True;

            // Blocks or Grey out the control during Ajax call
            this.grdClaimSelect.AjaxIndicator.BlockArea = Infragistics.Web.UI.AjaxIndicatorBlockArea.Control;
        }

        private int GetServiceTypeIndex(string ServiceType)
        {
            DataLayer.Billback obj = new DataLayer.Billback();
            string exMessage = "";
            int ServiceIndex =  obj.GetDisplayOrdIDByServiceType(ServiceType, out exMessage);
      
            return ServiceIndex;
           
        }

        private void LogActivity(string strLogCategory, string strLog)
        {
            string UserID = (string)Session["UserID"];
            string SessionID = (string)Session["SessionID"];
            string fName = (string)Session["fName"];
            string lName = (string)Session["lName"];
            UserAuthenticate.LogActivity(strLogCategory, strLog, UserID, fName, lName, SessionID);
        }

        private bool UserAuthentication()
        {
            //********** User Authentication **********
            try
            {
               // UserAuthenticate.LogActivity("DEBUG", "BEFORE USER IDENTIFICATION", "", "", "", "");
                string logon = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf("\\") + 1);
              //  UserAuthenticate.LogActivity("DEBUG", "BEFORE USER IDENTIFICATION", logon, "", "", "");


                // ADInfo ADinfo = new ADInfo();
                ADinfo user = new ADinfo(logon);
            //    UserAuthenticate.LogActivity("DEBUG", "AFTER ADINFO() ", logon, user.Name, "", "");
                string str = user.Name;
               
                string lName = str.Split(',')[0];
                string fName = str.Split(',')[1];

                strUserID = user.SamAccount;
                strUserFName = fName;
                strUserLName = lName;

               // lblStatusMsg.Text = "USER " + strUserFName + " " + strUserLName + " Logged On ";

               // UserAuthenticate.LogActivity("DEBUG", "BEFORE CREATEUSERSESSION", strUserID, strUserFName, strUserLName, "");
                CreateUserSession(fName, lName, logon);
              //  UserAuthenticate.LogActivity("DEBUG", "AFTER CREATEUSERSESSION", strUserID, strUserFName, strUserLName, "");
                if (UserAuthenticate.AuthenticateUser(strUserID.ToUpper()) == true)
                {
                    LogActivity("LOGON", "USER LOGGED ON");
                    ddlServiceType.Enabled = true;
                    return true;
                }
                else
                {
                    LogActivity("LOGON", "USER FAILED AUTHENTICATION");
                    lblStatusMsg.Text = "USER " + strUserFName + " " + strUserLName + " NOT AUTHORIZED TO USE THIS APPLICATION ";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    ddlServiceType.Enabled = false;
                    updPanelTab0.Update();
                    updSearchParameter.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('User is not Authorized to use this application');", true);

                    return false;
                }

                     
            }
            catch (Exception ex)
            {
                LogActivity("LOGON EXCEPTION", "AUTHENTICATION EXCEPTION: " + ex.Message);
                throw ex;
            }

            //********** User Authentication **********
        }

        private void CreateUserSession(string fName, string lName, string logon)
        {          
            Session.Add("UserID", logon);
            Session.Add("SessionID", Common.GetNewGUID());
            Session.Add("fName", fName);
            Session.Add("lName", lName);

        }

       

       

        private void DisableAllButtons()
        {
            btnSaveImage.Enabled = false;
            btnZoomGrid.Enabled = false;
            btnAddClaims.Enabled = false;
            btnRemoveClaims.Enabled = false;
            btnValidateProcClaims.Enabled = false;
            btnExportToExcel.Enabled = false;
            chkExportAllColumns.Enabled = false;
            btnSearchClaims.Enabled = false;
            btnRefreshImg.Enabled = false;
            btnImportClaims.Enabled = false;
            btnExportLog.Enabled = false;
            WebGrp1ReqFields.Enabled = false;
            WebGrp2OptionalFields.Enabled = false;
            WebGrp3FeeAllocFields.Enabled = false;
        }

        private void HideAllButtons()
        {
            btnSaveImage.Visible = false;
            btnZoomGrid.Visible = false;
            btnAddClaims.Visible = false;
            btnRemoveClaims.Visible = false;
            btnValidateProcClaims.Visible = false;
            btnExportToExcel.Visible = false;
            chkExportAllColumns.Visible = false;
            btnSearchClaims.Visible = false;
            btnRefreshImg.Visible = false;
            btnImportClaims.Visible = false;
            WebGrpGridBtns.Visible = false;
            btnExportLog.Visible = false;

        }

        private void EnableButtons()
        {
            //enable the common buttons for all service type
            btnZoomGrid.Enabled = true;
            btnAddClaims.Enabled = true;
            //   btnRemoveClaims.Enabled = true;
            btnValidateProcClaims.Enabled = true;
            btnExportToExcel.Enabled = true;
            chkExportAllColumns.Enabled = true;
            btnExportLog.Enabled = true;
        }


        private bool ValidateRequiredFields()
        {


            bool CheckPassed = true;

            if (String.IsNullOrWhiteSpace(txtClientID.Text))
            {
                lblStatusMsg.Text = "Client ID is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            

            if (String.IsNullOrWhiteSpace(txtClientID.Text) == false)
            {
                //make sure Client ID is numeric
                if (Common.IsNumeric(txtClientID.Text.Trim()) == false)
                {
                    lblStatusMsg.Text = "Client ID " + txtClientID.Text.Trim() + " must be numeric";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;
                }

                if (CheckPassed == true)
                {
                    //make sure Client ID exists and is active
                    DataLayer.Billback obj = new DataLayer.Billback();
                    string exMsg = "";
                    DataTable dtClient = obj.SearchClientData_By_ID_SP(txtClientID.Text.Trim(), 1, out exMsg);
                    if (dtClient.Rows.Count == 0)
                    {
                        lblStatusMsg.Text = "Client ID " + txtClientID.Text.Trim() + " does not exist";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        CheckPassed = false;
                    }
                }
            }

            if (ddlDataSet.SelectedItemIndex == -1)
            {
                lblStatusMsg.Text = "Data Set is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            //if (String.IsNullOrWhiteSpace(txtNumValidationDays.Text.Trim()) == true)
            //{
            //    lblStatusMsg.Text = "Validation Days is required";
            //    lblStatusMsg.ForeColor = System.Drawing.Color.White;
            //    lblStatusMsg.BackColor = System.Drawing.Color.Red;
            //    CheckPassed = false;
            //}

            if (ddlIncludeClaimStatus.SelectedItemIndex == -1)
            {
                lblStatusMsg.Text = "Include Claim Status is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (ddlServiceType.SelectedItemIndex == -1)
            {
                lblStatusMsg.Text = "Service Type is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (CheckPassed == true) //do this if all the other checks have passed
            {
                if ((String.IsNullOrWhiteSpace(dtpDatePayFrom.Text) == false) && (String.IsNullOrWhiteSpace(dtpDatePayThru.Text) == false))
                {
                    if (dtpDatePayFrom.Date > dtpDatePayThru.Date)
                    {
                        lblStatusMsg.Text = "'Date Pay From' cannot be greater than 'Date Pay Thru'";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;

                        CheckPassed = false;
                    }


                }

                if ((String.IsNullOrWhiteSpace(dtValuationDateFrom.Text) == true) || (String.IsNullOrWhiteSpace(dtValuationDateTo.Text) == true))
                {
                    lblStatusMsg.Text = "'Valuation Date From' and 'Valuation Date To' are required for Claim Search";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;
                }

                if (Common.IsValidDate(dtValuationDateFrom.Text.Trim()) == false)
                {
                    lblStatusMsg.Text = "'Valuation Date From' is Invalid";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;
                }

                if (Common.IsValidDate(dtValuationDateTo.Text.Trim()) == false)
                {
                    lblStatusMsg.Text = "'Valuation Date To' is Invalid";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;
                }

                if ((String.IsNullOrWhiteSpace(dtValuationDateFrom.Text) == false) && (String.IsNullOrWhiteSpace(dtValuationDateTo.Text) == false))
                {
                    if (dtValuationDateFrom.Date > dtValuationDateTo.Date)
                    {
                        lblStatusMsg.Text = "'Valuation Date From' cannot be greater than 'Valuation Date To'";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        CheckPassed = false;
                    }

                    if (dtValuationDateFrom.Date > DateTime.Now)
                    {
                        lblStatusMsg.Text = "'Valuation Date From' cannot be in the Future";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        CheckPassed = false;
                    }

                    if (dtValuationDateTo.Date > DateTime.Now)
                    {
                        lblStatusMsg.Text = "'Valuation Date To' cannot be in the Future";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        CheckPassed = false;
                    }

                    if (DateTime.Compare(dtValuationDateFrom.Date, DateTime.Parse("01/01/1901 00:00:00")) < 0)
                    {
                        lblStatusMsg.Text = "'Valuation Date From' cannot be prior to year 1901";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        CheckPassed = false;
                    }

                }

            }



            if (CheckPassed == true)
            {
                lblStatusMsg.Text = "Fill Search Criteria and Click Search Button";
                lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
            }

            return CheckPassed;
        }

        #region OLD CODE
        //private void LoadGridSampleData()
        //{
        //    DataTable dtSample = new DataTable();
        //    dtSample.Columns.Add("ClaimSelect");
        //    dtSample.Columns.Add("Error");
        //    dtSample.Columns.Add("ClientID");
        //    dtSample.Columns.Add("Claim#");
        //    dtSample.Columns.Add("ClientFName");
        //    dtSample.Columns.Add("ClientLName");
        //    dtSample.Columns.Add("DOL");
        //    dtSample.Columns.Add("ProcessingUnit");
        //    dtSample.Columns.Add("PayCode");
        //    dtSample.Columns.Add("AllocationAmount");

        //    dtSample.Rows.Add("", "", "4054", "16944243.338", "H", "Smith", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.339", "F", "Atticus", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.340", "P", "Darcy", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.341", "D", "Eloise", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.342", "A", "Esm", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.343", "P", "Gatsby", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.344", "C", "Holden", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.345", "Q", "Kairl", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.346", "B", "Zyzy", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.347", "E", "Rhet", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.348", "L", "Wully", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.438", "H", "Smith", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.439", "F", "Atticus", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.440", "P", "Darcy", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.441", "D", "Eloise", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.442", "A", "Esm", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.443", "P", "Gatsby", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.444", "C", "Holden", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.445", "Q", "Kairl", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.446", "B", "Zyzy", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.447", "E", "Rhet", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.448", "L", "Wully", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.538", "H", "Smith", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.539", "F", "Atticus", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.540", "P", "Darcy", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.541", "D", "Eloise", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.542", "A", "Esm", "", "PU33", "331", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.543", "P", "Gatsby", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.544", "C", "Holden", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.545", "Q", "Kairl", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.546", "B", "Zyzy", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.547", "E", "Rhet", "", "PU33", "165", "");
        //    dtSample.Rows.Add("", "", "4054", "16944243.648", "L", "Wully", "", "PU33", "165", "");
        //    grdClaimSelect.DataSource = dtSample;
        //    grdClaimSelect.DataBind();

        //    foreach (Infragistics.Web.UI.GridControls.GridRecord rec in grdClaimSelect.Rows)
        //    {
        //        //this gets the value of cell in webdatagrid
        //        Object value = ((DataRowView)rec.DataItem)["ClientFName"];



        //        // ((CheckBox)rec.FindControl("Error")).Checked = true;
        //    }



        //}
        #endregion



        protected void btnSearchClaims_Click(object sender, EventArgs e)
        {
            if (ValidateRequiredFields() == true)
            {
                // LoadGridSampleData();

                //OracleParameterCollection oclParamCollection = new OracleParameterCollection();
                //oclParamCollection.Add( new OracleParameter("pDataSet",OracleDbType.Varchar2));
                //oclParamCollection.Add( new OracleParameter("pClaimStatus",OracleDbType.Varchar2));
                //oclParamCollection.Add( new OracleParameter("pClientID",OracleDbType.Varchar2));

                // oclParamArray.Add(ddlIncludeClaimStatus.SelectedItem.Text,string);
                // oclParamArray.Add(txtClientID.Text,string);

                // GET DATA FROM VIAONE.CLAIM
                string ClaimStatus = "Y";
                string CLM_STATUS = "O";
                if (ddlIncludeClaimStatus.SelectedItemIndex == 0)
                {
                    ClaimStatus = "Y"; //claim status OPEN
                    CLM_STATUS = "O";
                }
                else
                {
                    ClaimStatus = "N"; //claim status CLOSED
                    CLM_STATUS = "C";
                }

                DataLayer.Billback obj = new DataLayer.Billback();
                string ServiceType = ddlServiceType.SelectedItem.Key;
                string exMessage = "";
                DataTable dtQuery = new DataTable();
                DataTable dt = new DataTable();
                try
                {
                    //DataTable dt = obj.GetClaimData(ddlDataSet.SelectedItem.Text.Trim(),ClaimStatus, txtClientID.Text.Trim()); //OLD NOT USED

                    string ValuationDateFrom, ValuationDateTo;
                   // int NumYrsToInclude = (int)txtIncludeYrsClaim.Value;
                  //  NumYrsToInclude = NumYrsToInclude * -1;
                  //  int ValuationDays = (int)txtNumValidationDays.Value;
                 //   string strValuationDays = ValuationDays.ToString("00");
                    ValuationDateFrom = dtValuationDateFrom.Text;
                    ValuationDateTo = dtValuationDateTo.Text;
                    
                    dtQuery = obj.GetClaimDataProc(ddlDataSet.SelectedItem.Text.Trim(), ClaimStatus, txtClientID.Text.Trim(), ValuationDateFrom, ValuationDateTo, ServiceType, CLM_STATUS, out exMessage); //CURRENT WITHOUT OPTIONAL PARAMS
                    string ExcludeClaimSubTypes = Common.FormatOptionalFilter(txtExcludeClaimSubTypes.Text.Trim());
                    string ExcludeLineCodes = Common.FormatOptionalFilter(txtExcludeLineCodes.Text.Trim());
                    string IncludeClaimTypes = Common.FormatOptionalFilter(txtIncludeClaimTypes.Text.Trim());
                    if (String.IsNullOrWhiteSpace(ExcludeClaimSubTypes) & String.IsNullOrWhiteSpace(ExcludeLineCodes)
                        & String.IsNullOrWhiteSpace(IncludeClaimTypes))
                    {
                        dt = dtQuery; //if optional search params are all blank keep original query
                    }
                    else
                    {
                        //comment this part out because code is not finished 12/12/2016
                        dt = obj.GetClaimDataWithOptSearchFilter(dtQuery, ExcludeClaimSubTypes, ExcludeLineCodes, IncludeClaimTypes);
                        //dt = dtQuery;
                    }

                    
                    //DataTable dt = obj.GetClaimDataWithOptParam(ddlDataSet.SelectedItem.Text.Trim(), ClaimStatus, txtClientID.Text.Trim(), "'AC','CB','CC'", "'AD','AI','AL'", "'EL','IN','IO','MO'"); //WITH OPT PARAMS
                    if (!String.IsNullOrWhiteSpace(exMessage))
                    {
                        Type cstype = this.GetType();
                        string script = "alert('" + exMessage + "');";
                        if (!ClientScript.IsStartupScriptRegistered("ErrorScript"))
                        {
                            Page.ClientScript.RegisterStartupScript(cstype, "ErrorScript", script,true);
                        }
                    }
                }
                catch
                {

                }

                //START***update paycode based on state of claim logic, datepayfrom and datepaythru,  updates GUID
                string strState_data = "";
                string strStateCode = "";
              
                

                string strFromDate = Common.GetPreviousMonthBeginDate();
                string strThruDate = Common.GetPreviousMonthEndDate();

                foreach (DataRow dr in dt.Rows)
                {
                    strState_data = dr["STATE_A"].ToString().Trim();
                    if (strState_data == "CA")
                        strStateCode = "3"; //if California paycode is medical and starts with 3
                    else
                        strStateCode = "6";//if not California paycode use expense paycode and starts with 6

                    switch (ServiceType) //used for paycode
                    {

                        case "TCM":
                            dr["PAYCODE"] = strStateCode + "45"; //** paycode TCM *45                             
                            break;
                        case "UTR":
                            dr["PAYCODE"] = strStateCode + "48"; //** paycode UTR *48
                            break;
                        case "CLC":
                            dr["PAYCODE"] = strStateCode + "32"; //** paycode CLC *32
                            break;
                        case "CAT":
                            dr["PAYCODE"] = strStateCode + "31"; //** paycode CAT *31
                            break;
                        default:
                            dr["PAYCODE"] = strStateCode + "97"; //** paycode OSA or ITK
                            break;
                    }

                    dr["DATEFROM"] = strFromDate;
                    dr["DATETHRU"] = strThruDate;

                    // UPDATE GUID - globally unique identifier that is unique for each row of grid
                    dr["GUID"] = Common.GetNewGUID();
                }
                

                //END***update paycode based on state of claim logic, updates GUID

                if (dt.Rows.Count == 0)
                {
                    lblStatusMsg.Text = "-- Search did not return any results --";
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    lblStatusMsg.Text = "Claim Count: " + dt.Rows.Count;
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                }

                //DataColumn column = new DataColumn();
                DataColumn[] keys = new DataColumn[1];
                //column.ColumnName = "GUID";
               // column.DataType = System.Type.GetType("System.String");
                keys[0] = dt.Columns["GUID"];
                dt.PrimaryKey = keys;

                grdClaimSelect.DataSource = dt;
                grdClaimSelect.DataKeyFields = "GUID";
                grdClaimSelect.DataBind();
                if (dt.Rows.Count > 0)
                {
                    SelectedRowCollection selectedRow = this.grdClaimSelect.Behaviors.Selection.SelectedRows;
                    selectedRow.Add(this.grdClaimSelect.Rows[0]);
                }
        

                Session["dtClaimsSearchData"] = dt; //set the session variable to searched claims
               // EnableNonSearchButtons();
             


                

            }
            updPanelTab0.Update();
            updSearchParameter.Update();
        }

        private string ServiceTypeSelected()
        {
            string ServiceType =  ddlServiceType.SelectedItem.Key;
           
           
                
            //switch (ddlServiceType.SelectedValue)
            //{
            //    case "0":
            //        ServiceType = "TCM";
            //        break;
            //    case "1":
            //        ServiceType = "UTR";
            //        break;
            //    case "2":
            //        ServiceType = "CAT";
            //        break;
            //    case "3":
            //        ServiceType = "CLC";
            //        break;
            //    case "4":
            //        ServiceType = "OSA";
            //        break;
            //    case "5":
            //        ServiceType = "ITK";
            //        break;

            //}

            return ServiceType;
        }

        #region OLD CODE
        //private void HideExtraBBFileColumns()
        //{
        //    this.grdClaimSelect.Columns["VENDOR_ID"].Hidden = true;
        //    this.grdClaimSelect.Columns["DATA_SET"].Hidden = true;
        //    this.grdClaimSelect.Columns["CLAIM_TYPE"].Hidden = true;
        //    this.grdClaimSelect.Columns["CLAIM_DELETED"].Hidden = true;
        //    this.grdClaimSelect.Columns["STATE_PAYROLL"].Hidden = true;
        //    this.grdClaimSelect.Columns["STATE_A"].Hidden = true;
        //}

        //private void ShowExtraBBFileColumns()
        //{
        //    this.grdClaimSelect.Columns["CLMNT_EXISTS"].Hidden = false;
        //    this.grdClaimSelect.Columns["EVENT_EXISTS"].Hidden = false;
        //    this.grdClaimSelect.Columns["RSV_STAT_ACCEPT"].Hidden = false;
        //    this.grdClaimSelect.Columns["CLAIM_DELETED"].Hidden = false;
        //    this.grdClaimSelect.Columns["SIR_REC_EXISTS"].Hidden = false;
        //    this.grdClaimSelect.Columns["SIR_LINE_REC_EXISTS"].Hidden = false;
        //}

        //private bool ValidateGridData(out List<string> UIDArray)
        //{
        //    bool ValFlag = true;

        //    if (Session["dtClaimsSearchData"] != null)
        //    {

        //        //this is to mark a few rows in grid as error for demo...sends back Claim_UID for sample of rows
        //        DataTable dtClaimsSearch = (DataTable)Session["dtClaimsSearchData"];
        //        int[] RowArray = new int[5] {1,3,7,11,12};
        //        UIDArray = new List<string>();
        //        UIDArray.Add("");
        //        int RowNumber = 0;
        //        foreach (DataRow dr in dtClaimsSearch.Rows)
        //        {
        //            RowNumber++;

        //            if (RowArray.Contains(RowNumber))
        //            {
        //                UIDArray.Add(dr["CLAIM_UID"].ToString()); 
        //            }
        //        }
        //    }


        //    return ValFlag;


        //}
        #endregion

        protected void btnAddClaims_Click(object sender, EventArgs e)
        {
            //Add Claim to grid - claims added with Onclientclick event javascript.
        }

        protected void btnRemoveClaims_Click(object sender, EventArgs e)
        {
            //remove claim from grid
            if ((grdClaimSelect.Rows.Count == 0) || (grdClaimSelect.Rows.Count == -1))
            {
                return;
            }

            DataTable dtClaimsData = new DataTable();

            dtClaimsData = (DataTable)Session["dtClaimsSearchData"];


            List<string> GUIDSelected = FindGridRowsSelected();
            string saveGUID;
            int ColCount = grdClaimSelect.Columns.Count;

            foreach (string strGUID in GUIDSelected)
            {
                saveGUID = strGUID;
             
                DataRow[] rows = dtClaimsData.Select("GUID = '" + saveGUID +"'" );
                foreach (DataRow dr in rows)
                {
                    dr.Delete();
                }
            }
            dtClaimsData.AcceptChanges();
            
            Session["dtClaimsSearchData"] = dtClaimsData;
            grdClaimSelect.DataSource = dtClaimsData;
            grdClaimSelect.DataBind();
            lblStatusMsg.Text = "Claim Count: " + dtClaimsData.Rows.Count;         
            updPanelTab0.Update();
            updSearchParameter.Update();

            //*******this code works to iterate infragistics grid records, but can't use it here
            //foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)
            //{
            //    saveClaimUID = gridRecord.Items[ColCount - 6].Text.ToUpper();
            //    var claimFound = claimUIDSelected.FirstOrDefault(claimToSearch => claimToSearch.Contains(saveClaimUID));
            //    if (claimFound != null)
            //    {

            //    }
            //}
        }



        private List<string> FindGridRowsSelected()
        {
            //GUID is unique id for for each row, add all GUIDs that have row selected
            List<string> GUIDList = new List<string>();



            //Now iterate through the grid rows to find which rows are selected
            foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)
            {
                int ColCount = gridRecord.Items.Count;
                // theWorkSheet.Rows[iRow].Cells[iCell].Value = gridRecord.Items[iCell - 1].Text;
                if (gridRecord.Items[1].Text.ToUpper() == "TRUE")
                {
                    GUIDList.Add(gridRecord.Items[ColCount - 1].Text); //add GUID to list
                }

            }





            return GUIDList;
        }

        protected void btnValidateProcClaims_Click(object sender, EventArgs e)
        {
            //save grid data before validation
            btnSaveImage_Click(null, null);
        

            //validate and process selected claims
            bool ValidationPass = ValidateClaimsData();
             



           // grdClaimSelect.DataSource = (DataTable)Session["dtClaimsSearchData"];
           // grdClaimSelect.DataBind();
           // updPanelTab0.Update();

            if (ValidationPass == true)
            {

                //Need to work on loading data in grid to datatable for now load from sessionstate
                // DataTable dtClaimsData = LoadDataFromGrid();
                DataTable dtClaimsData = new DataTable();
                if (Session["dtClaimsSearchData"] != null)
                {
                    dtClaimsData = (DataTable)Session["dtClaimsSearchData"];


                }
                string ServiceType = "";
               
                ServiceType = ddlServiceType.SelectedItem.Key;

                //switch (ddlServiceType.SelectedValue)
                //{
                //    case "0":
                //         ServiceType = "TCM";
                //        break;
                //    case "1":
                //        ServiceType = "UTR";
                //        break;
                //    case "2":
                //        ServiceType = "CAT";
                //        break;
                //    case "3":
                //        ServiceType = "CLC";
                //        break;
                //    case "4":
                //        ServiceType = "OSA";
                //        break;
                //    case "5":
                //        ServiceType = "ITK";
                //        break;
                    
                //}
                string FileNumber = "00001"; //for now hard code file number, later when we produce each file increment by 1
                BillbackFile obj = new BillbackFile();
                obj.ExportBillbackFile(dtClaimsData,ServiceType,FileNumber);

                //using (var memoryStream = new MemoryStream())
                //{
                //    string filename = "BILLBACK_" + "EXPORT" + "_" + ServiceType + "FEE_" + strDate + "_s430_999";
                //    string attachment = "attachment; filename=" + filename + ".xlsx";
                //    Response.ContentType = "text/plain";
                //    Response.AddHeader("content-disposition", attachment);
                //    fs.CopyTo(memoryStream);
                //    memoryStream.WriteTo(Response.OutputStream);


                //    // Page.ClientScript.RegisterStartupScript(this.GetType(), "HideAjax", "HideAjaxIndicator()", true);
                //    Response.Flush();
                //    Response.End();
                //}

                lblStatusMsg.Text = "Billback File Created";       
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Green;
                updPanelTab0.Update();
                updSearchParameter.Update();
                LogActivity("LOG", "Billback File Created for " + ServiceType);
               // Response.Write("<script>alert('Billback File Created');</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Billback File Created');", true);
            }
            else
            {
                lblStatusMsg.Text = "ONE OR MORE ROWS HAVE VALIDATION ERRORS";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                updPanelTab0.Update();
                updSearchParameter.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('ONE OR MORE ROWS HAVE VALIDATION ERRORS');", true);
               // Response.Write("<script>alert('ONE OR MORE ROWS HAVE VALIDATION ERRORS');</script>");
                
            }



        }

        private bool ValidateClaimsData()
        {
            DataTable dtClaimsData = new DataTable();
            bool ValidationPass = true;
            if (Session["dtClaimsSearchData"] != null)
            {
                dtClaimsData = (DataTable)Session["dtClaimsSearchData"];

                //do validation here and find claim uid and GUIDs that don't  pass validation, then mark those with red x image
                List<string> claimGUIDFailed = new List<string>();
                foreach (DataRow dr in dtClaimsData.Rows)
                {
                    if (ValidateFromToDate(dr) == false)
                    {
                        claimGUIDFailed.Add(dr["GUID"].ToString());
                        LogActivity("ERROR", "CLAIM# - " + dr["CLAIM#"].ToString() + " FROM / TO DATE INVALID ");
                        ValidationPass = false;
                    }

                    if (ValidateAllocAmt(dr) == false)
                    {
                        claimGUIDFailed.Add(dr["GUID"].ToString());
                        LogActivity("ERROR", "CLAIM# - " + dr["CLAIM#"].ToString() + " ALLOCATION AMOUNT REQUIRED ");
                        ValidationPass = false;
                    }
               


                }

                if (ValidationPass == true) //do bank validation only after all other validation is passed
                {
                    foreach (DataRow dr in dtClaimsData.Rows)
                    {

                        // bank number validation function returns an error--- PLS-00201: ...... identifier must be declared
                        if (ValidateBankNumber(dr) == false)
                        {
                            claimGUIDFailed.Add(dr["GUID"].ToString());
                            ValidationPass = false; // set to false so it will generate billback file for now, but still give warning
                            LogActivity("BANK VAL FAILED", "Bank Validation Failed for Claim UID: " + dr["CLAIM_UID"].ToString() + " and Claim#: "
                                + dr["CLAIM#"].ToString());

                        }
                    }

                    if (ValidationPass == true)
                    {
                        LogActivity("BANK VAL PASSED", "Bank Validation Passed for all claims ");                              
                    }
                }

             
                dtClaimsData.AcceptChanges();
                Session["ListOfGUIDErrored"] = claimGUIDFailed;
                Session["dtClaimsSearchData"] = dtClaimsData;
                grdClaimSelect.DataSource = dtClaimsData;
                grdClaimSelect.DataBind();
                updPanelTab0.Update();
           
            }

            return ValidationPass;
        }

        private bool ValidateBankNumber(DataRow dr)
        {
            bool ValPass = true;
            Billback.DataLayer.Billback obj = new DataLayer.Billback();
           string strTest = obj.CheckBankNumber_For_ClaimUID(dr["CLAIM_UID"].ToString(), dr["CLIENTID"].ToString(), null, null);

            // Billback.DataLayer.Billback obj = new DataLayer.Billback();
            // string strTest = obj.CheckBankNumber_For_ClaimUID("116005.331", "1954", null, null);
             if (strTest.Contains("BANK NOT FOUND") == true)
                 ValPass = false;
             else
                 ValPass = true;

            return ValPass;

        }

        private bool ValidateAllocAmt(DataRow dr)
        {
            string saveAllocAmt = dr["ALLOCATIONAMOUNT"].ToString();
            if (  saveAllocAmt.Trim() == "") return false;

            return true;
        }

        private bool ValidateBInvoice(DataRow dr)
        {
            string BInvoice = dr["BINVOICE"].ToString();
            if (String.IsNullOrWhiteSpace(BInvoice)) return false;

            return true;
        }

        private bool ValidateFromToDate(DataRow dr)
        {
            
            
            if  (dr["DATEFROM"]  == "") return false;
            if (dr["DATETHRU"] == "") return false;
            string DateFrom = dr["DATEFROM"].ToString();
            string DateTo = dr["DATETHRU"].ToString();
            if (Common.IsValidDate(DateFrom) == false) return false;
            if (Common.IsValidDate(DateTo) == false) return false;

           if ( DateTime.Parse(DateFrom) > DateTime.Parse(DateTo)) return false;


           return true;

        }

       

        private DataTable LoadDataFromGrid()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < grdClaimSelect.Columns.Count; i++)
            {
                dt.Columns.Add("column" + i.ToString());
            }

            foreach (Infragistics.Web.UI.GridControls.GridRecord rec in grdClaimSelect.Rows)
            {
                //this gets the value of cell in webdatagrid
                Object value = ((DataRowView)rec.DataItem)["CLMNTFNAME"];



                // ((CheckBox)rec.FindControl("Error")).Checked = true;
            }

            foreach (Infragistics.Web.UI.GridControls.GridRecord rec in grdClaimSelect.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < grdClaimSelect.Columns.Count; j++)
                {
                    //  dr["column" + j.ToString()] = row.Columns[j].Text;
                    Object value = ((DataRowView)rec.DataItem)["CLMNTFNAME"];
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }



        protected void btnFirstRow_Click(object sender, ImageClickEventArgs e)
        {
            SelectedRowCollection selectedRows = new SelectedRowCollection(grdClaimSelect);
            selectedRows.Clear();

            selectedRows.Add(grdClaimSelect.Rows[0]);

        }

        protected void btnLastRow_Click(object sender, ImageClickEventArgs e)
        {
            SelectedRowCollection selectedRows = new SelectedRowCollection(grdClaimSelect);
            selectedRows.Clear();

            selectedRows.Add(grdClaimSelect.Rows[grdClaimSelect.Rows.Count - 1]);
        }

        protected void grdClaimSelect_InitializeRow(object sender, RowEventArgs e)
        {



            //use this to mark rows that have validation errors --WORKS

            //Image image = e.Row.Items[0].FindControl("imgValidationError") as Image;
            //if (e.Row.Index < 5)
            //{
            //    image.ImageUrl = "~/Images/RedX.jpg";
            //}
            //else
            //{
            //    image.ImageUrl = "";
            //}


            Image image = e.Row.Items[0].FindControl("imgValidationError") as Image;
            List<string> GUIDList = new List<string>();

            if (Session["ListOfGUIDErrored"] != null)
            {
                GUIDList = (List<string>)Session["ListOfGUIDErrored"];
            }
            string saveGUID = e.Row.Items[grdClaimSelect.Columns.Count - 1].Text;


            if (GUIDList.IndexOf(saveGUID) != -1)  // if GUID found in list that means error flag set for that claim                  
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

        private void ExportLogToExcel(DataTable dtLog, string Category)
        {
            //with the datatable log given export to excel using EPPLUS.dll
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add(Category);
            ws.DefaultColWidth = 20;

            ws.Cells["A1"].LoadFromDataTable(dtLog, true, OfficeOpenXml.Table.TableStyles.Light11);

            //this is for fixing date fields in export, so they don't show as numbers
            int colNumber = 0;
            foreach (DataColumn col in dtLog.Columns) 
            {
                colNumber++;
                if (col.DataType == typeof(DateTime))
                {
                   
                    ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
                }          
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
  
            string filename = "BILLBACK_" + "LOG" + "_" + Category;
            string attachment = "attachment; filename=" + filename + ".xlsx";

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", attachment);
                pck.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);


                // Page.ClientScript.RegisterStartupScript(this.GetType(), "HideAjax", "HideAjaxIndicator()", true);
                Response.Flush();
                Response.End();
            }

            lblStatusMsg.Text = "Export File created";
            lblStatusMsg.ForeColor = System.Drawing.Color.White;
            lblStatusMsg.BackColor = System.Drawing.Color.Green;
            pck = null;
        }

        private void ExportToExcel() //using EPPLUS.dll
        {
            string ServiceType = "";
            string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");
            ServiceType = ddlServiceType.SelectedItem.Key;

            //switch (ddlServiceType.SelectedValue)
            //{
            //    case "0":
            //        ServiceType = "TCM";
            //        break;
            //    case "1":
            //        ServiceType = "UTR";
            //        break;
            //    case "2":
            //        ServiceType = "CAT";
            //        break;
            //    case "3":
            //        ServiceType = "CLC";
            //        break;
            //    case "4":
            //        ServiceType = "OSA";
            //        break;
            //    case "5":
            //        ServiceType = "ITK";
            //        break;

            //}

            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add(ServiceType);
            ws.DefaultColWidth = 20;

          

            int iRow = 1;
            int iCell = 1;
            bool ShowAllColumns = false;
            if (chkExportAllColumns.Checked)
            {
                ShowAllColumns = true;
            }
            else
            {
                ShowAllColumns = false;
            }

            //Iterate through the rows/columns of the WebDataGrid and create
            // columns within the worksheet that will be exported.

            List<int> ignoreColList = new List<int>();
            ignoreColList.Add(0);
            ignoreColList.Add(1);
            ignoreColList.Add(21);//don't export GUID to excel

            iCell = 1;
            iRow = 1;
            
            // // this is for loading heaaders into excel export
            foreach (GridField gridField in this.grdClaimSelect.Columns)
            {
                if (ShowAllColumns)
                {
                    if ((ignoreColList.IndexOf(gridField.Index) == -1))
                    {
                        ws.Cells[iRow,iCell].Value = gridField.Key;
                        ws.Cells[iRow, iCell].Style.Font.Bold = true;
                        //ws.Cells[iRow, iCell].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                        iCell += 1;
                        
                    }
                }
                else
                {
                    if ((gridField.Hidden == false) && (ignoreColList.IndexOf(gridField.Index) == -1))
                    {
                        ws.Cells[iRow, iCell].Value = gridField.Header.Text;
                        ws.Cells[iRow, iCell].Style.Font.Bold = true;
                      //  ws.Cells[iRow, iCell].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);                     
                        iCell += 1;
                    }
                }
            }
            iRow = 2;
            bool isDateField = false;
            string saveDate;
            //iterate through grid rows
            foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)
            {

                iCell = 1;

                //Now iterate through the grid columns to add columns to the worksheet
                foreach (GridField gridField in this.grdClaimSelect.Columns)
                {
                    if ((gridRecord.Items[gridField.Index].Column.Key == "DATEFROM") || (gridRecord.Items[gridField.Index].Column.Key == "DATETHRU")
                        || (gridRecord.Items[gridField.Index].Column.Key == "DOL"))
                    {
                        isDateField = true;
                    }
                    else
                    {
                        isDateField = false;
                    }

                    if (ShowAllColumns)
                    {
                       

                        if (ignoreColList.IndexOf(gridField.Index) == -1)
                        {
                            if (isDateField == true)
                            {
                                ws.Cells[iRow, iCell].Value = Common.ConvertDateTimeToDate(gridRecord.Items[gridField.Index].Text);
                            }
                            else
                            {
                                ws.Cells[iRow, iCell].Value = gridRecord.Items[gridField.Index].Text;
                            }
                            
                            
                            iCell += 1;
                        }
                    }
                    else
                    {
                        if ((gridField.Hidden == false) && (ignoreColList.IndexOf(gridField.Index) == -1))
                        {
                            if (isDateField == true)
                            {
                                ws.Cells[iRow, iCell].Value = Common.ConvertDateTimeToDate(gridRecord.Items[gridField.Index].Text);
                            }
                            else
                            {
                                ws.Cells[iRow, iCell].Value = gridRecord.Items[gridField.Index].Text;
                            }
                           
                            iCell += 1;
                        }
                    }
                }

                iRow += 1;

            }

            
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            string filename = "BILLBACK_" + "EXPORT" + "_" + ServiceType + "FEE_" + strDate + "_s430_999";
            string attachment = "attachment; filename=" + filename + ".xlsx";
          
          

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("content-disposition", attachment);
               pck.SaveAs(memoryStream);
               memoryStream.WriteTo(Response.OutputStream);
               
               
              // Page.ClientScript.RegisterStartupScript(this.GetType(), "HideAjax", "HideAjaxIndicator()", true);
               Response.Flush();
               Response.End();
            }

            lblStatusMsg.Text = "Export File created";
            lblStatusMsg.ForeColor = System.Drawing.Color.White;
            lblStatusMsg.BackColor = System.Drawing.Color.Green;


        }


        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            ExportToExcel();

            updSearchParameter.Update();
            updPanelTab0.Update();
           
            return;

            #region OLD EXCEL EXPORT CODE
            // //Create workbook and worksheet object for Excel
           // Workbook theWorkbook = new Workbook();
           // Worksheet theWorkSheet = theWorkbook.Worksheets.Add("WorkSheet1");

           // int iRow = 1;
           // int iCell = 1;
           // bool ShowAllColumns = false;
           // if (chkExportAllColumns.Checked)
           // {
           //     ShowAllColumns = true;
           // }
           // else
           // {
           //     ShowAllColumns = false;
           // }

           // //Iterate through the rows/columns of the WebDataGrid and create
           // // columns within the worksheet that will be exported.

           // List<int> ignoreColList = new List<int>();
           // ignoreColList.Add(0);
           // ignoreColList.Add(1);
          
           // iCell = 0;
           // iRow = 0;
           // // this is for loading heaaders into excel export
           // foreach (GridField gridField in this.grdClaimSelect.Columns)
           // {
           //     if (ShowAllColumns)
           //     {
           //         if ((ignoreColList.IndexOf(gridField.Index) == -1))
           //         {
           //             theWorkSheet.Rows[iRow].Cells[iCell].Value = gridField.Key;
           //             theWorkSheet.Columns[iCell].Width = 5000;
           //             iCell += 1;
           //             ;
           //         }
           //     }
           //     else
           //     {
           //         if ((gridField.Hidden == false) && (ignoreColList.IndexOf(gridField.Index) == -1))
           //         {
           //             theWorkSheet.Rows[iRow].Cells[iCell].Value = gridField.Header.Text;
           //             theWorkSheet.Columns[iCell].Width = 5000;
           //             iCell += 1;
           //         }
           //     }
           // }
           // iRow = 1;
           // //iterate through grid rows
           // foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)        
           // {
              
           //     iCell = 0;

           //         //Now iterate through the grid columns to add columns to the worksheet
           //         foreach (GridField gridField in this.grdClaimSelect.Columns)
           //         {
           //             if (ShowAllColumns)
           //             {

           //                 if (ignoreColList.IndexOf(gridField.Index) == -1)
           //                 {
           //                     theWorkSheet.Rows[iRow].Cells[iCell].Value = gridRecord.Items[gridField.Index].Text;
           //                     iCell += 1;
           //                 }
           //             }
           //             else
           //             {
           //                 if ((gridField.Hidden == false) && (ignoreColList.IndexOf(gridField.Index) == -1))
           //                 {
           //                     theWorkSheet.Rows[iRow].Cells[iCell].Value = gridRecord.Items[gridField.Index].Text;
           //                     iCell += 1;
           //                 }
           //             }
           //         }

           //         iRow += 1;
        
           // }

           //  string ServiceType = "";
           // string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");
           //     switch (ddlServiceType.SelectedValue)
           //     {
           //         case "0":
           //              ServiceType = "TCM";
           //             break;
           //         case "1":
           //             ServiceType = "UTR";
           //             break;
           //         case "2":
           //             ServiceType = "CAT";
           //             break;
           //         case "3":
           //             ServiceType = "CLC";
           //             break;
           //         case "4":
           //             ServiceType = "OSA";
           //             break;
           //         case "5":
           //             ServiceType = "ITK";
           //             break;
                    
           //     }

           // string filename = "BILLBACK_" +"EXPORT" + "_" + ServiceType + "FEE_" + strDate + "_s430_999";
           // string attachment = "attachment; filename=" + filename + ".xls";
           // Response.ClearContent();
           // Response.AddHeader("content-disposition", attachment);
           // Response.ContentType = "application/vnd.ms-excel";
           // theWorkbook.Save(Response.OutputStream);
           //// theWorkbook.Save(@"c:\"+filename+".xls");
           //// Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
           //// Response.AddHeader("content-disposition", "attachment;  filename=" + filename);

           // lblStatusMsg.Text = "Export File created";
           // lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
            // lblStatusMsg.BackColor = System.Drawing.Color.Transparent
            #endregion
        }

        protected void btnSettingsImg_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void txtCurMonthlyFee_CustomButtonClick(object sender, EventArgs e)
        {

        }

        protected void txtCurMonthlyFee_CustomButtonClick1(object sender, EventArgs e)
        {
            //udpate Allocation amount to each record...allocation amount divided by # of claim records (distribute evenly among all records)


            if (!string.IsNullOrEmpty(txtCurMonthlyFee.Text.ToString()))
            {
                string SaveAllocAmt = txtCurMonthlyFee.ValueDecimal.ToString();
                if (Common.IsValidCurrency(SaveAllocAmt))
                {

                    if (Session["dtClaimsSearchData"] != null)
                    {
                        DataTable dtClaims = new DataTable();
                        dtClaims = (DataTable)Session["dtClaimsSearchData"];
                        int TotalRec = dtClaims.Rows.Count;
                        decimal TotalAllocAmt = decimal.Parse(SaveAllocAmt);
                        #region OLD CODE
                        /* OLD CODE  this uses Round method and sometimes total of claims is more than original amount*/
                        //Decimal NewAllocAmt = (AllocAmt / TotalRec);
                        //NewAllocAmt = Decimal.Round(NewAllocAmt, 2);
                        //dtClaims.Columns["ALLOCATIONAMOUNT"].MaxLength = 50;
                        //foreach (DataRow dr in dtClaims.Rows)
                        //{
                        //    dr["ALLOCATIONAMOUNT"] = NewAllocAmt.ToString();

                        //}
                        /* OLD CODE */
                        #endregion

                        /* NEW CODE */
                        //split integral part and factional part, limit decimal part to 2 digits
                        //(this method is equivalent to of rounding down always)
                        Decimal NewAllocAmt = (TotalAllocAmt / TotalRec);
                        int IntegralPart = (int)decimal.Truncate(NewAllocAmt);
                        double FractionalPart = (double)(NewAllocAmt - IntegralPart);
                        //added 1/24/2017 because frac part was sometimes equal to 0 or .2 and getting exception because not 2 decimal places
                        string saveFrac = string.Format("{0:f2}", FractionalPart); //format to 2 decimals
                        string strFractionalPart = saveFrac.ToString().Substring(0, 4);
                        string strIntegralPart = IntegralPart.ToString();

                        decimal AllocAmountEachClaim = decimal.Parse(strFractionalPart) + int.Parse(strIntegralPart);
                        decimal TotalAllClaims = AllocAmountEachClaim * TotalRec;
                        decimal AllocAmountLastClaim = AllocAmountEachClaim + (TotalAllocAmt - (AllocAmountEachClaim * TotalRec));
                        dtClaims.Columns["ALLOCATIONAMOUNT"].MaxLength = 50;
                        //foreach (DataRow dr in dtClaims.Rows)
                        for (int i = 0; i < dtClaims.Rows.Count; i++)		
                        {
                            if (i == dtClaims.Rows.Count - 1)
                            {
                                //if last claim 
                                dtClaims.Rows[i]["ALLOCATIONAMOUNT"] = AllocAmountLastClaim.ToString();
                            }
                            else
                            {
                                //all other claims
                                dtClaims.Rows[i]["ALLOCATIONAMOUNT"] = AllocAmountEachClaim.ToString();
                            }

                        }
                        /* NEW CODE */
                        

                        grdClaimSelect.DataSource = dtClaims;
                        grdClaimSelect.DataBind();
                        lblStatusMsg.Text = "Allocation Amount distributed to all claims";
                        lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                        lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                        txtCurMonthlyFee.Text = "";
                    }
                }
                else
                {
                    lblStatusMsg.Text = "Currency Amount is invalid";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblStatusMsg.Text = "Currency Amount cannot be blank";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
            }
            

            updPanelTab0.Update();


        }

        protected void btnZoomGrid_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlServiceType_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {
            int DisplayOrdID = int.Parse( ddlServiceType.SelectedValue);
            if (DisplayOrdID != -1)
            {
                grdClaimSelect.DataSource = null;
                Session["dtClaimsSearchData"] = null;
                //grdClaimSelect.ClearDataSource();
                Billback.DataLayer.Billback obj = new DataLayer.Billback();
                string exMessage = "";
                DataTable dtServiceType = obj.GetServiceTypeByDispID(DisplayOrdID, out exMessage);
                if (!String.IsNullOrWhiteSpace(exMessage))
                {
                    LogActivity("APPLICATION EXCEPTION", exMessage);
                    lblStatusMsg.Text = "Could not set Application buttons - Check Log File ";
                    updPanelTab0.Update();
                    return;
                }


                //COMMON PROCEDURES
                DisableAllButtons();
                HideAllButtons();
                EnableCommonButtons();

                int SEARCH_ENABLE = int.Parse(dtServiceType.Rows[0]["SEARCH_ENABLE"].ToString());
                int ADD_ENABLE = int.Parse(dtServiceType.Rows[0]["ADD_ENABLE"].ToString());
                int IMPORT_ENABLE = int.Parse(dtServiceType.Rows[0]["IMPORT_ENABLE"].ToString());
                int REMOVE_ENABLE = int.Parse(dtServiceType.Rows[0]["REMOVE_ENABLE"].ToString());
                int PROCESS_ENABLE = int.Parse(dtServiceType.Rows[0]["PROCESS_ENABLE"].ToString());
                string SERVICE_TYPE = dtServiceType.Rows[0]["SERVICE_TYPE"].ToString();

                if (SEARCH_ENABLE == 1)
                {
                    WebGrp1ReqFields.Enabled = true;
                    WebGrp2OptionalFields.Enabled = true;

                    ddlIncludeClaimStatus.Items.Clear();
                    ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("OPEN", "0"));
                    ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("CLOSED", "1"));

                    dtValuationDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("MM") + "/25/" + DateTime.Now.AddYears(-3).ToString("yyyy");
                    dtValuationDateTo.Text = DateTime.Now.AddMonths(-1).ToString("MM") + "/25/" + DateTime.Now.Year.ToString("0000");
                   // txtIncludeYrsClaim.Text = "3"; //default # of years to include for claims
                    ddlDataSet.SelectedValue = "-1";
                   // txtNumValidationDays.Text = "25";
                    ddlIncludeClaimStatus.SelectedItemIndex = 0;
                    ddlDataSet.SelectedItemIndex = 0;

                    lblStatusMsg.Text = "Fill in Search Criteria and Click Search";
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                    btnSearchClaims.Enabled = true;
                    btnSearchClaims.Visible = true;
                }

                if (ADD_ENABLE == 1)
                {
                    lblStatusMsg.Text = "Choose Import / Add Claims";
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;

                    btnAddClaims.Enabled = true;
                    btnAddClaims.Visible = true;
                }

                if (IMPORT_ENABLE == 1)
                {
                    lblStatusMsg.Text = "Choose Import / Add Claims";
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;

                    btnImportClaims.Enabled = true;
                    btnImportClaims.Visible = true;
                }

#region OLD CODE
                //if ((ddlServiceType.SelectedValue == "0") || (ddlServiceType.SelectedValue == "1")) //if TCM or UR enable these panes, use search feature
                //{
                //    WebGrp1ReqFields.Enabled = true;
                //    WebGrp2OptionalFields.Enabled = true;
                //    WebGrp3FeeAllocFields.Enabled = true;



                //    ddlIncludeClaimStatus.Items.Clear();
                //    ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("OPEN", "0"));
                //    ddlIncludeClaimStatus.Items.Add(new Infragistics.Web.UI.ListControls.DropDownItem("CLOSED", "1"));

                //    txtIncludeYrsClaim.Text = "3"; //default # of years to include for claims
                //    ddlDataSet.SelectedValue = "-1";
                //    txtNumValidationDays.Text = "25";
                //    ddlIncludeClaimStatus.SelectedItemIndex = 0;
                //    ddlDataSet.SelectedItemIndex = 0;

                //    lblStatusMsg.Text = "Fill in Search Criteria and Click Search";
                //    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                //    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                //    btnSearchClaims.Enabled = true;
                //    btnSearchClaims.Visible = true;



                //    if (TCMURImportFeature == true) //used to import TCM/UR until we move billback utility to production (search will be used in future instead of import)
                //    {
                //        btnImportClaims.Enabled = true;
                //        btnImportClaims.Visible = true;
                //        btnAddClaims.Enabled = true;
                //        btnAddClaims.Visible = true;   
                //    }




                //}
                //else if ((ddlServiceType.SelectedValue == "2") || (ddlServiceType.SelectedValue == "3")) //for CAT and CC need to add individual claims with add button
                //{
                //    WebGrp1ReqFields.Enabled = false;
                //    WebGrp2OptionalFields.Enabled = false;
                //    WebGrp3FeeAllocFields.Enabled = true;



                //    lblStatusMsg.Text = "Choose Import or Add Claims";
                //    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                //    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;

                //    btnImportClaims.Enabled = true;
                //    btnImportClaims.Visible = true;
                //    btnAddClaims.Enabled = true;
                //    btnAddClaims.Visible = true;



                //}
                //else if ((ddlServiceType.SelectedValue == "4") || (ddlServiceType.SelectedValue == "5")) //for OSHA AND INTAKE need to add individual claims with add button
                //{
                //    WebGrp1ReqFields.Enabled = false;
                //    WebGrp2OptionalFields.Enabled = false;
                //    WebGrp3FeeAllocFields.Enabled = true;



                //    lblStatusMsg.Text = "Choose Import or Add Claims";
                //    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                //    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;

                //    btnImportClaims.Enabled = true;
                //    btnImportClaims.Visible = true;
                //    btnAddClaims.Enabled = true;
                //    btnAddClaims.Visible = true;



                //}
                //else
                //{
                //    WebGrp1ReqFields.Enabled = false;
                //    WebGrp2OptionalFields.Enabled = false;
                //    WebGrp3FeeAllocFields.Enabled = false;
                //    DisableAllButtons();
                //    HideAllButtons();

                //}
#endregion

                Session["ServiceType"] = SERVICE_TYPE;
                updSearchParameter.Update();
                updPanelTab0.Update();
            }

        }

        private void EnableCommonButtons()
        {
            btnRemoveClaims.Enabled = true;
            btnRemoveClaims.Visible = true;
            btnSaveImage.Enabled = true;
            btnSaveImage.Visible = true;
            btnRefreshImg.Enabled = true;
            btnRefreshImg.Visible = true;
            btnZoomGrid.Enabled = true;
            btnZoomGrid.Visible = true;
            btnExportToExcel.Enabled = true;
            btnExportToExcel.Visible = true;
            chkExportAllColumns.Enabled = true;
            chkExportAllColumns.Visible = true;
            btnValidateProcClaims.Enabled = true;
            btnValidateProcClaims.Visible = true;
            WebGrpGridBtns.Visible = true;
            btnExportLog.Enabled = true;
            btnExportLog.Visible = true;
            WebGrp3FeeAllocFields.Enabled = true;
        }



        protected void grdClaimSelect_RowDeleting(object sender, Infragistics.Web.UI.GridControls.RowDeletingEventArgs e)
        {
            //string result = string.Empty;
            //foreach (string key in e.Row.DataKey)
            //{
            //    //if (key.Equals("LONEP"))
            //    //{
            //    //    e.Cancel = true;
            //    //    result = this.GetGlobalResourceObject("WebDataGrid", "RowDelete_ServerMessageFail").ToString();
            //    //    break;
            //    //}
            //    //else
            //    //{
            //        result += key + " ";
            //  //  }
            //}

            //if (!e.Cancel)
            //{
            //    result = string.Format(this.GetGlobalResourceObject("WebDataGrid", "RowDelete_ServerMessageSuccess").ToString(), result.Trim());
            //}

            //grdClaimSelect.CustomAJAXResponse.Message += result + "\n<br />";

            lblStatusMsg.Text = "Deleted " + e.Row.Items[grdClaimSelect.Columns.Count - 6].Value.ToString();
            updPanelTab0.Update();

        }

        protected void btnLogImage_Click(object sender, ImageClickEventArgs e)
        {   DataLayer.Billback obj = new DataLayer.Billback();
            string SessionID = (string)Session["SessionID"];
            string exMessage = "";
            DataTable dtLog = obj.GetLogData_SP ("ALL", SessionID, out exMessage);
            ExportLogToExcel(dtLog, "ALL_ACTIVITY");
        }

        protected void btnSaveImage_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtClaimsdata = new DataTable();
            DataTable dtClaimsTemp = new DataTable();
            dtClaimsTemp = (DataTable)Session["dtClaimsSearchData"];
            dtClaimsdata = dtClaimsTemp.Clone();
            DataRow dr;
            // iterate through the grid rows
            try
            {
                string DateFrom;
                string DateTo;
                List<string> ClaimGUIDFailed = new List<string>();
                foreach (GridRecord gridRecord in this.grdClaimSelect.Rows)
                {
                    DateFrom = gridRecord.Items.FindItemByKey("DATEFROM").Text;
                    DateTo = gridRecord.Items.FindItemByKey("DATETHRU").Text;
                    if (Common.IsValidDate(DateFrom) == false)
                    {
                        lblStatusMsg.Text = "Date From is Invalid";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        ClaimGUIDFailed.Add(gridRecord.Items.FindItemByKey("GUID").Text);
                        Session["ListOfGUIDErrored"] = ClaimGUIDFailed;
                        return;
                    }
                    if (Common.IsValidDate(DateTo) == false)
                    {
                        lblStatusMsg.Text = "Date To is Invalid";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        ClaimGUIDFailed.Add(gridRecord.Items.FindItemByKey("GUID").Text);
                        Session["ListOfGUIDErrored"] = ClaimGUIDFailed;
                        return;             
                    }

                    if (DateTime.Parse(DateFrom) > DateTime.Parse(DateTo))
                    {
                        lblStatusMsg.Text = "Date From is Invalid";
                        lblStatusMsg.ForeColor = System.Drawing.Color.White;
                        lblStatusMsg.BackColor = System.Drawing.Color.Red;
                        ClaimGUIDFailed.Add(gridRecord.Items.FindItemByKey("GUID").Text);
                        Session["ListOfGUIDErrored"] = ClaimGUIDFailed;
                        return;
                    }

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



                    // theWorkSheet.Rows[iRow].Cells[iCell].Value = gridRecord.Items[iCell - 1].Text;
                    //if (gridRecord.Items[1].Text.ToUpper() == "TRUE")
                    //{
                    //    claimUIDList.Add(gridRecord.Items[ColCount - 6].Text);
                    //}


                }

                lblStatusMsg.Text = "All Claims Saved";
                lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
            }
            catch 
            {
                return;
            }
            Session["dtClaimsSearchData"] = dtClaimsdata;

        }

        protected void btnUpdateFeeAllocDetails_Click(object sender, EventArgs e)
        {
            //udpate Allocation amount, Tax ID and Date From / To  and BInvoice to each record...
            // allocation amount divided by # of claim records (distribute evenly among all records)

            bool updateAllocAmt = false;
            bool updateDates = false;
            bool updateTaxID = false;
            bool updateBInvoice = false;
            DateTime DateFrom = new DateTime();
            DateTime DateThru = new DateTime();
            decimal AllocAmountEachClaim = 0;
            decimal AllocAmountLastClaim = 0;

            if (!string.IsNullOrEmpty(dtpDatePayFrom.Text.ToString()) && !string.IsNullOrEmpty(dtpDatePayThru.Text.ToString()))
            {
                if (dtpDatePayFrom.Date > dtpDatePayThru.Date)
                {
                    lblStatusMsg.Text = "Date Pay From cannot be more recent than Date Pay Thru";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    updPanelTab0.Update();
                    updSearchParameter.Update();
                    return;
                }

                if (!Common.IsValidDate(dtpDatePayFrom.Text) || (!Common.IsValidDate(dtpDatePayThru.Text)))
                {
                    lblStatusMsg.Text = "Date Pay From and Thru must be valid dates";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    updPanelTab0.Update();
                    updSearchParameter.Update();
                    return;
                }
                else
                {
                    DateFrom = dtpDatePayFrom.Date;
                    DateThru = dtpDatePayThru.Date;
                    updateDates = true;
                }

            }

            if (!string.IsNullOrEmpty(txtCurMonthlyFee.Text.ToString()))
            {
                string SaveAllocAmt = txtCurMonthlyFee.ValueDecimal.ToString();
                if (!Common.IsValidCurrency(SaveAllocAmt))
                {
                    lblStatusMsg.Text = "Currency Amount is invalid";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    updPanelTab0.Update();
                    updSearchParameter.Update();
                    return;
                }
            }

            if (!String.IsNullOrWhiteSpace(txtBInvoice.Text))
            {
                txtBInvoice.Text = txtBInvoice.Text.Trim();
                updateBInvoice = true;
            }


            if (!String.IsNullOrWhiteSpace(txtCurMonthlyFee.Text) || !String.IsNullOrWhiteSpace(dtpDatePayThru.Text) ||
                !String.IsNullOrWhiteSpace(dtpDatePayFrom.Text) || !String.IsNullOrWhiteSpace(txtTaxID.Text.Trim()) || !String.IsNullOrWhiteSpace(txtBInvoice.Text.Trim()))
            {


                if (Session["dtClaimsSearchData"] != null)
                {
                    DataTable dtClaims = new DataTable();

                    dtClaims = (DataTable)Session["dtClaimsSearchData"];
                    int TotalRec = dtClaims.Rows.Count;
                    decimal NewAllocAmt = 0;

                    if (!String.IsNullOrWhiteSpace(txtCurMonthlyFee.Text))
                    {
                        string SaveAllocAmt = txtCurMonthlyFee.ValueDecimal.ToString();                       
                        decimal TotalAllocAmt = decimal.Parse(SaveAllocAmt);

                        NewAllocAmt = (TotalAllocAmt / TotalRec);
                        int IntegralPart = (int)decimal.Truncate(NewAllocAmt);
                        double FractionalPart = (double)(NewAllocAmt - IntegralPart);
                        //added 1/24/2017 because frac part was sometimes equal to 0 or .2 and getting exception because not 2 decimal places
                        string saveFrac = string.Format("{0:f2}", FractionalPart); //format to 2 decimals
                        string strFractionalPart = saveFrac.ToString().Substring(0, 4);
                        string strIntegralPart = IntegralPart.ToString();

                        AllocAmountEachClaim = decimal.Parse(strFractionalPart) + int.Parse(strIntegralPart);
                        decimal TotalAllClaims = AllocAmountEachClaim * TotalRec;
                        AllocAmountLastClaim = AllocAmountEachClaim + (TotalAllocAmt - (AllocAmountEachClaim * TotalRec));
                      
                        updateAllocAmt = true;
                    }
                    dtClaims.Columns["ALLOCATIONAMOUNT"].MaxLength = 50;


                    for (int i = 0; i < dtClaims.Rows.Count; i++)	
                    {
                        if (updateAllocAmt == true)
                        {
                            if (i == dtClaims.Rows.Count - 1)
                            {
                                //if last claim 
                                dtClaims.Rows[i]["ALLOCATIONAMOUNT"] = AllocAmountLastClaim.ToString();
                            }
                            else
                            {
                                //all other claims
                                dtClaims.Rows[i]["ALLOCATIONAMOUNT"] = AllocAmountEachClaim.ToString();
                            }
                        }

                        if (updateDates == true)
                        {
                            dtClaims.Rows[i]["DATEFROM"] = DateFrom;
                            dtClaims.Rows[i]["DATETHRU"] = DateThru;
                        }

                        if (updateBInvoice == true)
                        {
                            dtClaims.Rows[i]["BINVOICE"] = txtBInvoice.Text.ToString();
                        }
                    }

                    grdClaimSelect.DataSource = dtClaims;
                    grdClaimSelect.DataBind();
                    lblStatusMsg.Text = "Allocation details updated to all claims";
                    lblStatusMsg.ForeColor = System.Drawing.Color.Blue;
                    lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
                    txtCurMonthlyFee.Text = "";
                    txtBInvoice.Text = "";
                    txtTaxID.Text = "";
                }


            }


            updPanelTab0.Update();
            updSearchParameter.Update();

        }

        protected void btnRefreshImg_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["dtClaimsSearchData"] != null)
            {
                DataTable dtClaims = (DataTable)Session["dtClaimsSearchData"];
                grdClaimSelect.DataSource = dtClaims;
                grdClaimSelect.DataBind();
                lblStatusMsg.Text = "Data Refreshed Successfully";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Green;
                updSearchParameter.Update();
                updPanelTab0.Update();
            }
        }

        public void txtClientID_TextChanged(object sender, EventArgs e)
        {
            //Get Client Name by ID
            DataLayer.Billback obj = new DataLayer.Billback();

            string strClientID = txtClientID.Text;
            string exMessage = "";
            DataTable dtClient = new DataTable();
            if (!String.IsNullOrWhiteSpace(strClientID))
            {
                string strClientName = "";
                dtClient = obj.SearchClientData_By_ID_SP(strClientID.Trim(), 1, out exMessage);
                if (dtClient.Rows.Count > 0)
                {
                    strClientName = dtClient.Rows[0]["NAME"].ToString();
                    lblSelectedClientName.BackColor = System.Drawing.Color.Transparent;
                    lblSelectedClientName.ForeColor = System.Drawing.Color.Black;
                    if (strClientName.Length > 50)
                    {
                        lblSelectedClientName.Text = strClientName.Substring(0, 50);
                    }
                    else
                    {
                        lblSelectedClientName.Text = strClientName;
                    }
                }
                else
                {
                    lblSelectedClientName.Text = "Client ID Not Found";
                    lblSelectedClientName.BackColor = System.Drawing.Color.Red;
                    lblSelectedClientName.ForeColor = System.Drawing.Color.White;
                    txtClientID.Focus();
                    txtClientID.Attributes.Add("onfocusin", "select();");
                    
                        

                }
                updSearchParameter.Update();
                          
            }
           
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}