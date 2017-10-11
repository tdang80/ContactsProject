using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billback.Global
{
    public class Common
    {
        public static bool IsNumeric(string strVal)
        {
            int n;
            bool isNumeric = int.TryParse(strVal, out n);
            return isNumeric;
        }

      

        public static bool IsValidCurrency(string strVal)
        {
            bool result = true;
            try
            {
                decimal.Parse(strVal);
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }
         

            return result;

        }

        public static string ConvertToDecimal(string strVal)
        {
            decimal AllocAmt;
            string strFormatAmount = "";
            try
            {
                AllocAmt = decimal.Parse(strVal.Replace("$","").Replace(",","")); //replace any $ or comma in original amount 
                strFormatAmount = AllocAmt.ToString("C").Replace("$","").Replace(",",""); //this converts to currency in 2 decimal places (.NET adds comma and $ back though so take it out)
            }
            catch (Exception)
            {
                
            }


            return strFormatAmount;

        }

        public static List<string> FormatClaimNumbers(List<string> ClaimNumbers)
        {
            //remove dashes '-' from ClaimNumbers Listing
            List<string> NewClaimNumbers = new List<string>();

            foreach (string strClaimNumber in ClaimNumbers)
            {

                if (!String.IsNullOrWhiteSpace(strClaimNumber))
                {
                    NewClaimNumbers.Add(RemoveDashesFromString(strClaimNumber)); //if dash '-' is found within last 3 characters of claim leave it and search claim with dash
                                                                                //have seen claims numbers... 792CN051464-1, 792CN051464-01 in VIAONE PRD
                }
                else
                {
                    NewClaimNumbers.Add(String.Empty);
                }
            }            
            return NewClaimNumbers;
        }

        public static string RemoveDashesFromString(string strTemp)
        {
            //remove dashes '-' from ClaimNumber if found in last 3 char of string
            string strNewTemp = strTemp;
  
            if (strNewTemp.Length - strNewTemp.LastIndexOf("-") <= 3) //if dash '-' is found within last 3 characters of claim leave it and search claim with dash
            {                                                                   //have seen claims numbers... 792CN051464-1, 792CN051464-01 in VIAONE PRD
                return strNewTemp;
            }
            else
            {
               return strNewTemp.Replace("-", "");
            }
        }

        public static bool IsValidDate(string strVal)
        {
            bool result = true;
            try
            {
                DateTime.Parse(strVal);
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }


            return result;

        }

        public static string GetUniqueID()
        {
            return DateTime.Now.ToString("yyMMddHHmmss.FFF");

        }

        public static string GetNewGUID()
        {
            return Guid.NewGuid().ToString(); //generates hyphenated 36 char guid, example outputs: 12345678-1234-1234-1234-123456789abc
        }

        public static string GetPreviousMonthBeginDate()
        {
            int Year, PrevMonth;
            PrevMonth = DateTime.Now.AddMonths(-1).Month;
            if (PrevMonth == 12)
            {
                Year = DateTime.Now.AddYears(-1).Year;
            }
            else
            {
                Year = DateTime.Now.Year;
            }
            
            string strFromDate = PrevMonth + "/01/" + DateTime.Now.Year;

            return strFromDate;
            
        }

        public static string GetPreviousMonthEndDate()
        {
            int Year, PrevMonth;
            PrevMonth = DateTime.Now.AddMonths(-1).Month;
            if (PrevMonth == 12)
            {
                Year = DateTime.Now.AddYears(-1).Year;
            }
            else
            {
                Year = DateTime.Now.Year;
            }

                  
            string strThruDate = PrevMonth + "/" + DateTime.DaysInMonth(Year, PrevMonth) + "/" + Year;
            return strThruDate;
        }

        public static string ConvertDateTimeToDate(string datetime)
        {
            string strDate;
            if (Common.IsValidDate(datetime))
            {
                strDate = Convert.ToDateTime(datetime).ToString("MM/dd/yyyy");
            }
            else
            {
                strDate = "";
            }

            return strDate;
        }

        public static string FormatOptionalFilter(string strOldFilter)
        {
            string strReturn = null;
            if (!string.IsNullOrWhiteSpace(strOldFilter))
            {

                char[] delimitter = new char[] { ',' };

                string[] tempArray;
                string strNewFilter = "";
                tempArray = strOldFilter.Split(delimitter, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in tempArray)
                {
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        strNewFilter += "'" + item.Trim() + "',";
                    }

                }
                strReturn = strNewFilter.Remove(strNewFilter.Length - 1, 1); //remove the last comma and return new string filter
            }


            return strReturn;
        }


        //public static string GetPostBackControlId(this Page page)
        //{
        //    if (!page.IsPostBack)
        //        return string.Empty;

        //    Control control = null;
        //    // first we will check the "__EVENTTARGET" because if post back made by the controls
        //    // which used "_doPostBack" function also available in Request.Form collection.
        //    string controlName = page.Request.Params["__EVENTTARGET"];
        //    if (!String.IsNullOrEmpty(controlName))
        //    {
        //        control = page.FindControl(controlName);
        //    }
        //    else
        //    {
        //        // if __EVENTTARGET is null, the control is a button type and we need to
        //        // iterate over the form collection to find it

        //        // ReSharper disable TooWideLocalVariableScope
        //        string controlId;
        //        Control foundControl;
        //        // ReSharper restore TooWideLocalVariableScope

        //        foreach (string ctl in page.Request.Form)
        //        {
        //            // handle ImageButton they having an additional "quasi-property" 
        //            // in their Id which identifies mouse x and y coordinates
        //            if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
        //            {
        //                controlId = ctl.Substring(0, ctl.Length - 2);
        //                foundControl = page.FindControl(controlId);
        //            }
        //            else
        //            {
        //                foundControl = page.FindControl(ctl);
        //            }

        //            if (!(foundControl is IButtonControl)) continue;

        //            control = foundControl;
        //            break;
        //        }
        //    }

        //    return control == null ? String.Empty : control.ID;
        //}



    }
}