using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Billback.Global;
using OfficeOpenXml.Packaging;
using OfficeOpenXml;
using Billback.DataLayer;


namespace Billback.Webforms
{
    public partial class ImportClaims : System.Web.UI.Page
    {
        public string DefaultFileName = "Upload/"; // is the folder where files are uploaded to

        public System.Data.DataTable dtClaimSearchData = new System.Data.DataTable();
     

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }



        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            if (FileUploader.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploader.FileName);
                    FileUploader.SaveAs(Server.MapPath("~/Upload/") + filename);
                    
                    string FileFullPath = Server.MapPath("~/Upload/") + filename;
                    Session["FileFullPath"] = FileFullPath;
                    FileInfo existingFile = new FileInfo(FileFullPath);
                    if (ImportExcelSheetNames(existingFile, FileFullPath) == true)
                    {
                        btnImportSheet.Enabled = true;
                        lblStatus.Text = "Upload status: Excel File loaded...Select Tab Sheet to load";
                        lblStatus.ForeColor = System.Drawing.Color.Blue;
                        lblStatus.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        btnImportSheet.Enabled = false;
                        lblStatus.Text = "Upload status: Error - Excel File Not loaded";
                        lblStatus.ForeColor = System.Drawing.Color.White;
                        lblStatus.BackColor = System.Drawing.Color.Red;
                    }
                 

                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Upload status: The Excel tab sheets could not be uploaded. The following error occured: " + ex.Message;
                    btnImportSheet.Enabled = false;
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    lblStatus.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }



    protected void btnImportSheet_Click(object sender, EventArgs e)
        {
            if (Session["FileFullPath"] != null)
            {
                string FileFullPath = (string)Session["FileFullPath"];
                DataSet dsExcelClaim = new DataSet();

                bool hasHeader = true;
                FileInfo existingFile = new FileInfo(FileFullPath);
                DataTable dtExcelData = GetDataTableFromExcel(existingFile, hasHeader, FileFullPath, ddlExcelSheets.SelectedItem.Text);

                //dsExcelClaim = LoadExcel(FileFullPath); //this loads all tabsheets into dataset
                Session["dtExcelClaims"] = dtExcelData;
             //   ImportClaimsData(dtExcelData, FileFullPath); //this imports selected tab sheet, formatting to claims datagrid requirements on ClaimSearch parent page

                if (ImportClaimsDataNew(dtExcelData, FileFullPath) == false) //this imports selected tab sheet, formatting to claims datagrid requirements on ClaimSearch parent page
                {
                    //if return false then at least one claim uid not found for claim#
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('CLAIM_UID was not found for at least one claim#...CHECK LOG FILE');", true);
                    LogActivity("CLAIMS IMPORT FAIL", "CLAIM_UID NOT FOUND FOR ONE OR MORE CLAIMS - " + ddlExcelSheets.SelectedItem.Text);
                }
                else
                {
                    LogActivity("CLAIMS IMPORT SUCCESS", "CLAIM_UID FOUND FOR ALL CLAIMS - " + ddlExcelSheets.SelectedItem.Text);
                }
                
               // ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "closePage", "window.close();", true);
                Session["PopUpClosed"] = true;
                Session["NewSvcType"] = false;
                Session["ClientSearch"] = false;
                ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "closePage", "<script type='text/javascript'>closeAndRefresh();</script>");
            }
            else
            {
                lblStatus.Text = "Error: Can not find File Path - Session is null";
                lblStatus.ForeColor = System.Drawing.Color.White;
                lblStatus.BackColor = System.Drawing.Color.Red;
            }
        }


    private DataTable GetDataTableFromExcel(FileInfo existingFile, bool HasHeader, string Filepath, string Tabsheet)
        {
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //this code not necessary since excel file seems to be closing after using statement closes
                using (var stream = File.OpenRead(Filepath))
                {
                    package.Load(stream);
                }

                int wscount = package.Workbook.Worksheets.Count;
                DataTable dtExcel = new DataTable();
                if (wscount > 0)
                {

                    // get the selected worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[Tabsheet];
                    

                    foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                    {
                        dtExcel.Columns.Add(HasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }

                    var startRow = HasHeader ? 2 : 1;
                    for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                        var row = dtExcel.NewRow();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                        dtExcel.Rows.Add(row);
                    }
                    package.Dispose();
                   
                }
                else
                {
                    lblStatus.Text = "Your Excel file does not contain any work sheets";
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    lblStatus.BackColor = System.Drawing.Color.Red;
                }

                return dtExcel;
            }
        }

    #region OLD CODE
    // private void ImportClaimsData(DataTable dtExcelClaimData, string FileFullPath)
       // {
       //     //import claims data from datatable inputting defaults and temporary claim uids
       ////     System.Data.DataTable dtExcelData = dt; //import first table in dataset (sheet 1 from excel import)

       //     string FileName = Path.GetFileName(FileFullPath);
       //     string TemplateType = "A";
       //     if (FileName.Contains("TEMPLATE_A"))
       //     {
       //         TemplateType = "A";
       //     }
       //     else
       //     {
       //         TemplateType = "B";
       //     }

       //     if (dtClaimSearchData.Rows.Count == 0)
       //     {
       //         dtClaimSearchData = BuildEmptyGridColumns();
       //     }

       //   //  dtClaimSearchData.PrimaryKey = new DataColumn[] { dtClaimSearchData.Columns["CLAIM_UID"] };
       //     string strFromDate = common.GetPreviousMonthBeginDate();
       //     string strThruDate = common.GetPreviousMonthEndDate();
       //     string ServiceType = "";
       //     string PayCode = "";
       //     string AllocAmt = "";
       //     foreach (DataRow dr in dtExcelClaimData.Rows)
       //     {
       //         if (dr["CLAIM_NUMBER"].ToString().Trim() != "")
       //         {
       //             DataRow drClaim = dtClaimSearchData.NewRow();


       //             drClaim["CLAIM#"] = dr["CLAIM_NUMBER"].ToString().Trim();
       //             drClaim["CLIENTID"] = dr["CLIENT_ID"].ToString().Trim();
       //             drClaim["PROCESSINGUNIT"] = "";
       //             drClaim["CLAIM_UID"] = common.GetUniqueID();
       //             drClaim["DOL"] = strFromDate;
       //             if (common.IsValidDate(dr["DATE_PAY_FROM"].ToString().Trim()) == true)
       //             {
       //                 drClaim["DATEFROM"] = dr["DATE_PAY_FROM"].ToString().Trim();
       //             }
       //             else
       //             {
       //                 drClaim["DATEFROM"] = strFromDate;
       //             }
       //             if (common.IsValidDate(dr["DATE_PAY_THRU"].ToString().Trim()) == true)
       //             {
       //                 drClaim["DATETHRU"] = dr["DATE_PAY_THRU"].ToString().Trim();
       //             }
       //             else
       //             {
       //                 drClaim["DATETHRU"] = strThruDate;
       //             }
       //             AllocAmt = dr["AMOUNT"].ToString().Trim().Replace("$","");
       //             if (common.IsValidCurrency(AllocAmt) == true)
       //             {

       //                 drClaim["ALLOCATIONAMOUNT"] = common.ConvertToDecimal(AllocAmt); //converts to 2 decimal format
       //             }
       //             else
       //             {
       //                 drClaim["ALLOCATIONAMOUNT"] = "";
       //             }
       //             drClaim["BINVOICE"] = dr["BINVOICE"].ToString().Trim();

       //             //*** we need to get state and paycode from database (paycode is depends on service type and state)
       //             ServiceType = dr["SERVICE_TYPE"].ToString().Trim();
       //             drClaim["GUID"] = common.GetNewGUID(); //always get GUID regardless of template used
                     
                    

       //             if (TemplateType == "B") // Template B has additional columns such as CLAIM_UID, VENDOR_ID etc.
       //             {
       //                 drClaim["CLAIM_UID"] = dr["CLAIM_UID"].ToString().Trim();
       //                 drClaim["VENDOR_ID"] = dr["VENDOR_ID"].ToString().Trim();
       //                 drClaim["STATE_PAYROLL"] = dr["STATE_PAYROLL"].ToString().Trim();
       //                 drClaim["STATE_A"] = dr["STATE_A"].ToString().Trim();
       //                 drClaim["DATA_SET"] = dr["DATA_SET"].ToString().Trim();
       //                 drClaim["CLAIM_TYPE"] = dr["CLAIM_TYPE"].ToString().Trim();
       //                 if (dr["STATE_A"].ToString().Trim() != "")
       //                 {
       //                     if ((ServiceType == "TCM") || (ServiceType == "UTR"))
       //                     {
       //                         drClaim["PAYCODE"] = dr["PAYCODE"].ToString().Trim();
       //                     }
       //                     else
       //                     {
       //                         drClaim["PAYCODE"] = GetPayCode(ServiceType, dr["STATE_A"].ToString().Trim());
       //                     }

       //                 }
       //                 drClaim["SUB_TYPE"] = "";
       //                 drClaim["LINE_CODE"] = "";
                             
                        
       //             }



       //             dtClaimSearchData.Rows.Add(drClaim);
       //         }
       //     }

            
       //     Session["dtClaimsSearchData"] = dtClaimSearchData;
       //     Session["PopUpClosed"] = true;
          
       //     lblStatus.Visible = false;

           
       //     this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);




    // }
    #endregion

    private bool ImportClaimsDataNew(DataTable dtExcelClaimData, string FileFullPath)
        {
            //import claims data from datatable inputting defaults and temporary claim uids
            //     System.Data.DataTable dtExcelData = dt; //import first table in dataset (sheet 1 from excel import)

            string FileName = Path.GetFileName(FileFullPath);
            DataLayer.Billback obj = new DataLayer.Billback();
            //do validation here and find claim uid and GUIDs that don't  pass validation, then mark those with red x image
            List<string> claimGUIDFailed = new List<string>();

            if (dtClaimSearchData.Rows.Count == 0)
            {
                dtClaimSearchData = BuildEmptyGridColumns();
            }

            //  dtClaimSearchData.PrimaryKey = new DataColumn[] { dtClaimSearchData.Columns["CLAIM_UID"] };
            string strFromDate = Common.GetPreviousMonthBeginDate();
            string strThruDate = Common.GetPreviousMonthEndDate();
            string ServiceType = "";
            string PayCode = "";
            string AllocAmt = "";

            //load all claim numbers into string list
            List<string> RawClaimNumberList = new List<string>();
            foreach (DataRow dr in dtExcelClaimData.Rows)
            {
                RawClaimNumberList.Add(dr["CLAIM_NUMBER"].ToString().Trim());
            }
            List<string> ClaimNumberList = Common.FormatClaimNumbers(RawClaimNumberList);
            
            //*******testing new GetClaimInfo Stor PRoc*************
            DataTable dtClaimInfo = obj.GetClaimInfoPRD(ClaimNumberList); //gets the claim information for all claim numbers (i.e. CLAIM_UID, VENDOR_ID, STATE_A  etc.)
            string ExMsg = "";
            //DataTable dtClaimInfo = obj.GetClaimInfo(ClaimNumberList, out ExMsg); //gets the claim information for all claim numbers (i.e. CLAIM_UID, VENDOR_ID, STATE_A  etc.)
            //*******testing new GetClaimInfo Stor PRoc*************

            DataRow[] foundClaim;
            bool All_ClaimUID_Found = true;
            //Note:  We are merging data from VIAONE (dtClaimInfo, found[]) and data from spreadsheet (dtExcelClaimData, dr[]) into new datatable dtClaimSearchData, drClaim[]
            foreach (DataRow dr in dtExcelClaimData.Rows) 
            {
                if (!String.IsNullOrWhiteSpace(dr["CLAIM_NUMBER"].ToString()))
                {
                    DataRow drClaim = dtClaimSearchData.NewRow();


                    drClaim["CLAIM#"] = dr["CLAIM_NUMBER"].ToString().Trim();
                    drClaim["CLIENTID"] = dr["CLIENT_ID"].ToString().Trim();
                    drClaim["PROCESSINGUNIT"] = "";
                    drClaim["CLAIM_UID"] = Common.GetUniqueID();
                   // drClaim["DOL"] = strFromDate;
                    if (Common.IsValidDate(dr["DATE_PAY_FROM"].ToString().Trim()) == true)
                    {
                        drClaim["DATEFROM"] = dr["DATE_PAY_FROM"].ToString().Trim();
                    }
                    else
                    {
                        drClaim["DATEFROM"] = strFromDate;
                    }
                    if (Common.IsValidDate(dr["DATE_PAY_THRU"].ToString().Trim()) == true)
                    {
                        drClaim["DATETHRU"] = dr["DATE_PAY_THRU"].ToString().Trim();
                    }
                    else
                    {
                        drClaim["DATETHRU"] = strThruDate;
                    }
                    AllocAmt = dr["AMOUNT"].ToString().Trim().Replace("$", "");
                    if (Common.IsValidCurrency(AllocAmt) == true)
                    {

                        drClaim["ALLOCATIONAMOUNT"] = Common.ConvertToDecimal(AllocAmt); //converts to 2 decimal format
                    }
                    else
                    {
                        drClaim["ALLOCATIONAMOUNT"] = "";
                    }
                    drClaim["BINVOICE"] = dr["BINVOICE"].ToString().Trim();

                    //*** we need to get state and paycode from database (paycode is depends on service type and state)
                    ServiceType = dr["SERVICE_TYPE"].ToString().Trim();
                    drClaim["GUID"] = Common.GetNewGUID(); //always get GUID regardless of template used

                    /* START - doing some debugging to find if multiple claim uid exists for a claim number */
                    //foundClaim = dtClaimInfo.Select("CLAIM_NUMBER = '" + common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()) + "'");
                    //if (foundClaim.Count() > 1)
                    //{
                    //    LogActivity("MULTIPLE CLAIM UIDs", "MULTIPLE CLAIM UIDs for Claim# : " + common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()) + " #claim uids found: " + foundClaim.Count().ToString() );
                    //}

                    /* END - doing some debugging to find if multiple claim uid exists for a claim number*/

                    //filter by claim number AND client id  (e.g. 6400)
                    foundClaim = dtClaimInfo.Select("CLAIM_NUMBER = '" + Common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()) + "' AND CLIENT_ID = '" + dr["CLIENT_ID"].ToString().Trim() + "'");
                    if ( foundClaim.Count() > 0)
                    {
                        drClaim["CLAIM_UID"] = foundClaim[0]["CLAIM_UID"]; //foundClaim...data coming from VIAONE filtered by claim#
                        drClaim["VENDOR_ID"] = foundClaim[0]["VENDOR_ID"];
                        drClaim["STATE_PAYROLL"] = foundClaim[0]["STATE_PAYROLL"];
                        drClaim["STATE_A"] = foundClaim[0]["STATE_A"];
                        drClaim["DATA_SET"] = foundClaim[0]["DATA_SET"];
                        drClaim["CLAIM_TYPE"] = foundClaim[0]["CLAIM_TYPE"];
                        drClaim["CLMNTFNAME"] = foundClaim[0]["CLMNTFNAME"];
                        drClaim["CLMNTLNAME"] = foundClaim[0]["CLMNTLNAME"];
                        drClaim["DOL"] = foundClaim[0]["DOL"];
                        if ((string)foundClaim[0]["CLAIM_TYPE"].ToString().Trim() == "IO") //flag claim_type IO, do not process IO claims
                        {
                            LogActivity("CLAIM_TYPE = IO", "CLAIM_TYPE = IO FOR CLAIM# " + Common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()));
                            claimGUIDFailed.Add(drClaim["GUID"].ToString());
                            All_ClaimUID_Found = false;
                        }

                        if ((string)foundClaim[0]["DELETED"].ToString().Trim() == "Y") //flag deleted claims, do not process deleted claims
                        {
                            LogActivity("CLAIM DELETED = Y", "CLAIM DELETED = Y FOR CLAIM# " + Common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()));
                            claimGUIDFailed.Add(drClaim["GUID"].ToString());
                            All_ClaimUID_Found = false;
                        }

                        if (!String.IsNullOrWhiteSpace(foundClaim[0]["STATE_A"].ToString()))
                        {
                            //if ((ServiceType == "TCM") || (ServiceType == "UTR"))
                            if (ServiceType == "TCM") //only TCM should be manual input for now, UR follows same logic as other service types
                            {
                                drClaim["PAYCODE"] = dr["PAYCODE"].ToString().Trim();
                            }
                            else
                            {
                                //SV 2/24/2017 - corrected again use paycode logic based on service type
                                drClaim["PAYCODE"] = GetPayCode(ServiceType, foundClaim[0]["STATE_A"].ToString().Trim(), dr["PAYCODE"].ToString().Trim());
                                
                               
                             
                            }

                        }
                        drClaim["SUB_TYPE"] = "";
                        drClaim["LINE_CODE"] = "";
                    }
                    else
                    {
                        LogActivity("CLAIM_UID NOT FOUND","CLAIM_UID NOT FOUND FOR CLAIM# "+Common.RemoveDashesFromString(dr["CLAIM_NUMBER"].ToString().Trim()));
                        claimGUIDFailed.Add(drClaim["GUID"].ToString());
                        All_ClaimUID_Found = false;
                    }

                    dtClaimSearchData.Rows.Add(drClaim);
                }
            }

            Session["ListOfGUIDErrored"] = claimGUIDFailed;
            Session["dtClaimsSearchData"] = dtClaimSearchData;
            Session["PopUpClosed"] = true;
            Session["ServiceType"] = ServiceType;
            lblStatus.Visible = false;


        //    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            return All_ClaimUID_Found;


        }

        private string GetPayCode(string ServiceType, string strState_data, string OrigPayCode)
        {
           
            string strStateCode = "";
            string paycode = "";
            if (strState_data == "CA")
                strStateCode = "3"; //if California paycode is medical and starts with 3
            else
                strStateCode = "6";//if not California paycode use expense paycode and starts with 6

            //procedure to get Paycode logic based on service type
            DataLayer.Billback obj = new DataLayer.Billback();
            string exMessage = "";
            DataTable dtPayCode = obj.GetPayCodeLogic(ServiceType, out exMessage);
            if (dtPayCode.Rows.Count > 0)
            {
                int PAYCODE_LOGIC = int.Parse(dtPayCode.Rows[0]["PAYCODE_LOGIC"].ToString());
                string PAYCODE = dtPayCode.Rows[0]["PAYCODE"].ToString().Replace("*","");
                switch (PAYCODE_LOGIC)
                {
                    case 0: //USE SERVICE INTERACTION LOGIC
                        paycode = OrigPayCode; //for now TCM/UR use paycode supplied
                        break;
                    case 1: //USE CA PAY CODE FLIP LOGIC
                        paycode = strState_data + PAYCODE;
                        break;
                    case 2: //USE PAY CODE SUPPLIED
                        if (!String.IsNullOrWhiteSpace(OrigPayCode))
                        {
                            paycode = OrigPayCode;
                        }
                        else
                        {
                            paycode = "6" + PAYCODE; //if paycode not supplied use expense
                        }
                        break;                                      
                    default:
                        paycode = OrigPayCode;
                        break;
                }


            }

            switch (ServiceType) //used for paycode
            {

                   


               // case "TCM":
               //    paycode = strStateCode + "45"; //** paycode TCM *45
                //    break;
                //case "UTR":
                //    paycode = strStateCode + "48"; //** paycode UTR *48
                //    break;
                case "CLC":
                    paycode = strStateCode + "32"; //** paycode CLC *32
                    break;
                case "CAT":
                    paycode = strStateCode + "31"; //** paycode CAT *31
                    break;
                case "OSA":
                    paycode = "697"; //** paycode OSA or ITK
                    break;
                case "ITK":
                    paycode = "697"; //** paycode OSA or ITK
                    break;
            }

            return paycode;
        }

        private System.Data.DataTable BuildEmptyGridColumns()
        {
            System.Data.DataTable dtGridData = new System.Data.DataTable();
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
            dtGridData.Columns.Add("DATEFROM", Type.GetType("System.DateTime"));
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

        private bool ImportExcelSheetNames(FileInfo existingFile, string FilePath)
        {
            bool ReturnVal = true;

            if (!File.Exists(FilePath))
            {
                lblStatus.Text = "Error: File does not exist";
                lblStatus.ForeColor = System.Drawing.Color.White;
                lblStatus.BackColor = System.Drawing.Color.Red;
                return false;
            };

            //***********Load tab sheets into drop down list ************
            string strError = "";
            try
            {
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        strError = "Your Excel file does not contain any work sheets";
                        lblStatus.ForeColor = System.Drawing.Color.White;
                        lblStatus.BackColor = System.Drawing.Color.Red;
                        ReturnVal = false;
                    }
                    else
                    {
                        foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                        {
                            Infragistics.Web.UI.ListControls.DropDownItem item = new Infragistics.Web.UI.ListControls.DropDownItem(worksheet.Name);
                            ddlExcelSheets.Items.Add(item);
                        }

                        ddlExcelSheets.SelectedItemIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {

                lblStatus.Text = "Upload status: The Excel tab sheets could not be uploaded. The following error occured: " + strError + " - " + ex.Message;
                lblStatus.ForeColor = System.Drawing.Color.White;
                lblStatus.BackColor = System.Drawing.Color.Red;
                ReturnVal = false;
            }

            return ReturnVal;
      
        }

        private void LogActivity(string strLogCategory, string strLog)
        {
            string UserID = (string)Session["UserID"];
            string SessionID = (string)Session["SessionID"];
            string fName = (string)Session["fName"];
            string lName = (string)Session["lName"];
            UserAuthenticate.LogActivity(strLogCategory, strLog, UserID, fName, lName, SessionID);
        }


        static DataSet LoadExcel(string fileName)
        {
            
            
            
            //string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'", fileName);


            //DataSet data = new DataSet();

            //foreach (var sheetName in GetExcelSheetNames(connectionString))
            //{
            //    using (OleDbConnection con = new OleDbConnection(connectionString))
            //    {
            //        var dataTable = new System.Data.DataTable();
            //        string query = string.Format("SELECT * FROM [{0}]", sheetName);
            //        con.Open();
            //        OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            //        adapter.Fill(dataTable);
            //        data.Tables.Add(dataTable);
            //    }
            //}



          /// return data;
            return null;
        }

   
    }
}