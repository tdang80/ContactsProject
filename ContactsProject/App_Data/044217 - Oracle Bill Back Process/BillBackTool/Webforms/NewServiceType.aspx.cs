using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Billback.Global;
using Billback.DataLayer;

namespace Billback.Webforms
{
    public partial class NewServiceType : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //find the next Display Order ID and fill in text box
                DataLayer.Billback obj = new DataLayer.Billback();
                string exMessage = "";
                txtDisplayOrdID.Text = obj.FindNextDisplayOrderID(out exMessage).ToString();
                rdPayCodeLogic.SelectedIndex = 1;
            }
            
            //have them enter paycode suffix last 2 digits (e.g *45 )

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (ValidateRequiredFields() == true)
            {
                //if all checks pass then save to databaes

                string strServiceType = txtServiceType.Text.Trim().ToUpper();
                string strDescription = txtDescription.Text.Trim();
                string strDisplay = txtDisplay.Text.Trim();
                string strDisplayOrdID = txtDisplayOrdID.Text.Trim();
                string strTaxID = txtTaxID.Text.Trim();
                string strTaxSubID = txtTaxSubID.Text.Trim();
                int Search_Enable = BooltoInt(chSearchEnable.Checked);
                int Add_Enable = BooltoInt(chAddEnable.Checked);
                int Import_Enable = BooltoInt(chImportEnable.Checked);
                int Remove_Enable = BooltoInt(chRemoveEnable.Checked);
                int Process_Enable = BooltoInt(chProcessEnable.Checked);
                int PayCodeLogic = rdPayCodeLogic.SelectedIndex;
                string strPayCodeLogicDesc = rdPayCodeLogic.SelectedItem.Text.ToUpper();
                string strPayCode = txtPayCodeSuffix.Text.Trim();

                DataLayer.Billback obj = new DataLayer.Billback();
                string exMessage = "";
                obj.InsertNewServiceType(strServiceType, strDescription, strDisplay, strDisplayOrdID, strTaxID, strTaxSubID, Search_Enable, Add_Enable, 
                    Import_Enable, Remove_Enable, Process_Enable, PayCodeLogic, strPayCodeLogicDesc, strPayCode, out exMessage );
                if (String.IsNullOrWhiteSpace(exMessage))
                {
                    LogActivity("CREATE NEW SERVICE TYPE", "NEW SERVICE TYPE CREATED - Svc Type: " + strServiceType + " Desc: " + strDescription +
                        " Display: " + strDisplay + " Display Order: " + strDisplayOrdID + " TaxID: " + strTaxID + " Tax SubID: " + strTaxSubID + " SearchEnable: " + Search_Enable +
                        " AddEnable: " + Add_Enable + " ImportEnable: " + Import_Enable + " RemoveEnable: " + Remove_Enable + " ProcessEnable: " + Process_Enable +
                        " PayCodeLogic: " + PayCodeLogic + " PayCodeLogicDesc: " + strPayCodeLogicDesc + " PayCode: " + strPayCode);
                    Session["PopUpClosed"] = true;
                    Session["NewSvcType"] = true;
                    Session["ClientSearch"] = false;
                    ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "closePage", "<script type='text/javascript'>closeAndRefresh();</script>");
                }
                else
                {
                    LogActivity("APPLICATION EXCEPTION", " ERROR ADDING NEW SERVICE TYPE: " + strServiceType + " Exception: " + exMessage);
                    lblStatusMsg.Text = " ERROR ADDING NEW SERVICE TYPE: " + strServiceType + " Check Activity Log";
                }
                
            }
        }

        private void LogActivity(string strLogCategory, string strLog)
        {
            string UserID = (string)Session["UserID"];
            string SessionID = (string)Session["SessionID"];
            string fName = (string)Session["fName"];
            string lName = (string)Session["lName"];
            UserAuthenticate.LogActivity(strLogCategory, strLog, UserID, fName, lName, SessionID);
        }

        private int BooltoInt(bool p)
        {
           int ReturnVal = 0;
            if (p == true)
            {
                ReturnVal = 1;
            }
            else
            {
                ReturnVal = 0;
            }

            return ReturnVal;
        }

        private bool ValidateRequiredFields()
        {
            //validate the information entered
            bool CheckPassed = true;
            if (String.IsNullOrWhiteSpace( txtServiceType.Text ))
            {
                lblStatusMsg.Text = "Service Type is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;

            }
            else if (txtServiceType.Text.Trim().Length != 3)
            {
                lblStatusMsg.Text = "Service Type has to  be 3 characters";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (String.IsNullOrWhiteSpace(txtDisplay.Text))
            {
                lblStatusMsg.Text = "Display is required - This will appear in Service Type Selection";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (String.IsNullOrWhiteSpace(txtTaxID.Text))
            {
                lblStatusMsg.Text = "Tax ID is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (Common.IsNumeric(txtTaxID.Text.Trim()) == false)
            {
                lblStatusMsg.Text = "Tax ID Needs to be numeric";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (Common.IsNumeric(txtTaxSubID.Text.Trim()) == false)
            {
                lblStatusMsg.Text = "Tax Sub ID Needs to be numeric";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if (String.IsNullOrWhiteSpace(txtTaxSubID.Text))
            {
                lblStatusMsg.Text = "Tax Sub ID is required";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if ((chSearchEnable.Checked == false) && (chImportEnable.Checked == false))
            {
                lblStatusMsg.Text = "Either Search or Import Checkbox needs to be checked";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            DataLayer.Billback obj = new DataLayer.Billback();
            string exMessage = "";
            if (obj.CheckIfServiceTypeExists(txtServiceType.Text.Trim().ToUpper(), out exMessage) == true)
            {
                lblStatusMsg.Text = "Service Type " + txtServiceType.Text + " Already Exists";
                lblStatusMsg.ForeColor = System.Drawing.Color.White;
                lblStatusMsg.BackColor = System.Drawing.Color.Red;
                CheckPassed = false;
            }

            if ((rdPayCodeLogic.SelectedIndex == 0) || (rdPayCodeLogic.SelectedIndex == 1))
            {
                if (txtPayCodeSuffix.Text.Trim().Length != 3)
                {
                    lblStatusMsg.Text = "Pay Code Suffix must be 3 characters starting with * ";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;
                }
                else if (txtPayCodeSuffix.Text.StartsWith("*") == false)
                {
                    lblStatusMsg.Text = "Pay Code Suffix must start with * ";
                    lblStatusMsg.ForeColor = System.Drawing.Color.White;
                    lblStatusMsg.BackColor = System.Drawing.Color.Red;
                    CheckPassed = false;

                }
            }


            return CheckPassed;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }

        protected void chSearchEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chSearchEnable.Checked)
            {
                chImportEnable.Checked = false;
            }
        }

        protected void chImportEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chImportEnable.Checked)
            {
                chSearchEnable.Checked = false;
            }
        }
    }
}