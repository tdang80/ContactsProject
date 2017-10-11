using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Billback.Global;

namespace Billback.DataLayer
{
    public class BillbackFile
    {
        public DataTable dtHDR;
        public DataTable dtPMT;
        public DataTable dtTRL;
        public DataTable dtClaimSearchData = new DataTable();
        public string ServiceType;
        public string FileNumber;
      //  string FilePath = @"C:\sample.txt";
        string FilePath = @"\\memfp02\SHARE\Any\Guardians\Billback";
        string FileName;
        public string strDate; 

        public void ExportBillbackFile(DataTable dtClaimsData, string ServiceType, string FileNumber5_Digits)
        {
            this.ServiceType = ServiceType;
            this.FileNumber = FileNumber5_Digits.ToString();
            dtClaimSearchData = dtClaimsData;
            CreateBillbackIntFile();
            // DropFileInServer();
        }

        private void CreateBillbackIntFile()
        {
           //Create Billback file in pipe delimitted format
          //  LoadFileFormatData();
            strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");
            string sHDRLine = CreateHDRLine();
            string[] sPMTLine = new string[dtClaimSearchData.Rows.Count];
            string sTRLLine = CreateTRLLine();

            int rownum = 0;
            //loop through each row in dtClaimSearchData
            string state = "";

            //get Tax ID, Tax Sub ID from DB
           Billback obj = new Billback();
           string exMessage = "";
           DataTable dt = obj.GetPayCodeLogic(this.ServiceType, out exMessage);
           string TaxID = "****NEED TAX-ID***";
            string TaxSubID  ="****NEED TAX-SUB-ID***";
            if (dt.Rows.Count > 0)
            {
                TaxID = dt.Rows[0]["TAXID"].ToString();
                TaxSubID = dt.Rows[0]["TAXSUBID"].ToString();

            }

            foreach (DataRow row in dtClaimSearchData.Rows)
            {
                
                sPMTLine[rownum] = CreatePMTLine(row, TaxID, TaxSubID);
                rownum++;
            }

            CreatePipeFile(sHDRLine, sPMTLine, sTRLLine);

           // System.Diagnostics.Process.Start("explorer", @"c:\\");
        }

        private void CreatePipeFile(string sHDR, string[] sPMT, string sTRL )
        {
            //USE THIS CODE TO WRITE TO FOLDER
            //if (!System.IO.File.Exists(FilePath))
            //{
            //    using (System.IO.FileStream fs = System.IO.File.Create(FilePath))
            //    {
            //        // Write the string to a file.

            //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath))
            //        {
            //            file.WriteLine(sHDR);

            //            foreach (string strPMT in sPMT)
            //            {
            //                file.WriteLine(sPMT);
            //            }

            //            file.WriteLine(sTRL);

            //            file.Close();
            //        }
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("File \"{0}\" already exists.", FilePath);
            //    return;
            //}

            //FOR NOW JUST OUTPUT THE FILE WITHOUT WRITING TO DISK
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
           // {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath + "\\" + FileName))
                {
                    sw.WriteLine(sHDR);

                    foreach (string strPMT in sPMT)
                    {
                        sw.WriteLine(strPMT);
                    }

                    sw.WriteLine(sTRL);
           
                }

                
              
       //     }


        }

        private string CreateTRLLine()
        {
            string[] strArray = new string[19];
            //string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");

            strArray[0] = "TRL";
            strArray[1] = "BILLBACK_" + this.FileNumber + "_" + ServiceType + "FEE_" + strDate + "_s430_999";
            strArray[2] = dtClaimSearchData.Rows.Count.ToString();
            strArray[3] = (dtClaimSearchData.Rows.Count + 2).ToString();

            for (int i = 4; i < 18; i++)
            {
                strArray[i] = "";
            }

            return String.Join("|", strArray);
        }

        private string CreateHDRLine()
        {
            string[] strArray = new string[19];
          //  string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");

            strArray[0] = "HDR";
            strArray[1] = "BILLBACK_"+this.FileNumber+"_"+ServiceType+"FEE_"+strDate +"_s430_999";
            FileName = strArray[1];
            strArray[2] = ServiceType + "FEE";

            for (int i = 3; i < 18; i++)
            {
                strArray[i] = "";
            }

            return String.Join("|",strArray);
        }

        private string CreatePMTLine(DataRow dr, string TaxID, string TaxSubID )
        {
            string[] strArray = new string[19];
            string strDate = DateTime.Now.ToString("MM/dd/yyy");
            string strDateFrom = "";
            string strDateThru = "";
            string strState_data = "";
            string strStateCode = "";
            string strMonth = "";
            string strYear = "";
            if (Common.IsValidDate(dr["DATEFROM"].ToString()) )
            {
                strDateFrom = DateTime.Parse(dr["DATEFROM"].ToString()).Date.ToString("MM/dd/yyyy");
                strMonth = DateTime.Parse(dr["DATEFROM"].ToString()).Date.ToString("MMMM");
                strYear = DateTime.Parse(dr["DATEFROM"].ToString()).Date.ToString("yyyy");
            }
            if (Common.IsValidDate(dr["DATETHRU"].ToString()) )
            {
                strDateThru = DateTime.Parse(dr["DATETHRU"].ToString()).Date.ToString("MM/dd/yyyy");
            }

            strArray[0] = "PMT";
            strArray[1] = strDate;
            strArray[2] = dr["CLAIM_UID"].ToString();
            strArray[3] = "P";//always P
            strArray[4] = "";//always blank
            strArray[5] = TaxID;
            strArray[6] = TaxSubID; 

            //switch (ServiceType) //used for P-taxsub
            //{
            //    case "TCM":
            //    case "UTR":
            //        strArray[6] = "404";//***P-taxsub ...from caller file or use 402 for CLC or CAT, use 404 for TCM
            //        break;  
            //    case "CLC":
            //    case "CAT":
            //        strArray[6] = "402";//***P-taxsub ...from caller file or use 402 for CLC or CAT, use 404 for TCM
            //        break;
            //    case "OSA":
            //    case "ITK":
            //         strArray[6] = "402";//**P-taxsub ...from caller file 402 for OSA and ITK
            //        break;
            //    default:
            //        strArray[6] = "****NEED P-TAX-SUB****";//***P-taxsub ...from caller file
            //        break;

            //}

            //strState_data = dr["STATE_A"].ToString().Trim();
            //if (strState_data == "CA")
            //    strStateCode = "3"; //if California paycode is medical and starts with 3
            //else
            //    strStateCode = "6";//if not California paycode use expense paycode and starts with 6

            
            strArray[7] = dr["PAYCODE"].ToString();

            //switch (ServiceType) //used for paycode
            //{

            //    case "TCM":
            //       // strArray[7] = strStateCode + "45"; //** paycode TCM *45
            //        strArray[7] = dr["PAYCODE"].ToString(); // for now TCM and UR get paycocde from template
            //        break;
            //    case "UTR":
            //       // strArray[7] = strStateCode +"48"; //** paycode UTR *48
            //        strArray[7] = dr["PAYCODE"].ToString(); // for now TCM and UR get paycocde from template
            //        break;
            //    case "CLC":
            //        //strArray[7] = strStateCode +"32"; //** paycode CLC *32
            //        strArray[7] = dr["PAYCODE"].ToString(); //get paycocde from template 2/10/2017
            //        break;
            //    case "CAT":
            //        //strArray[7] = strStateCode +"31"; //** paycode CAT *31
            //        strArray[7] = dr["PAYCODE"].ToString(); //get paycocde from template 2/10/2017
            //        break;
            //    case "OSA":
            //    case "ITK":
            //        //strArray[7] = strStateCode + "97"; //** paycode OSA or ITK
            //        strArray[7] = dr["PAYCODE"].ToString(); //get paycocde from template 2/10/2017
            //        break;
              
            //}
           
           
         //   strArray[7] = dr["PAYCODE"].ToString().Trim();//***Pay Code from caller file
            strArray[8] = dr["BINVOICE"].ToString().Trim();//***BInvoice can be blank unless provided by caller 
            strArray[9] = "";//ICN always blank

            strArray[10] = dr["VENDOR_ID"].ToString();
            strArray[11] = dr["ALLOCATIONAMOUNT"].ToString().Trim();
            strArray[12] = strDate;//Date bill, today date
            strArray[13] = strDateFrom;//***Date Pay From provided by caller
            strArray[14] = strDateThru;//***Date Pay Thru provided by caller
            strArray[15] = strDate;//Date Print, today date
            if (strArray[7] != "")
                strArray[16] = strArray[7].Substring(0, 1); //take the first digit of paycode 3 or 6

            //string strPayCode = dr["PAYCODE"].ToString().Trim();
            //if (strPayCode.Length != 0)
            //    strArray[16] = strPayCode.Substring(0,1);//Pay category must match 1st digit of pay code 3 for medical, 6 for expense
            //else
            //{
            //   // strArray[16] = "****NEED PAY CATEGORY****";
            //    strArray[16] = "3";//for now hardcode, once we import from caller file we should get paycode from it and get 1st digit
            //}
            strArray[17] = ""; //blank
            if (strMonth == "")
            {
                strArray[18] = ServiceType + "FEE - " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("yyyy") + " - " + dr["CLIENTID"].ToString();//comment
            }
            else
            {
                strArray[18] = ServiceType + "FEE - " + strMonth + " " + strYear + " - " + dr["CLIENTID"].ToString();//comment
            }
            
          

            return String.Join("|", strArray);
        }

        private void LoadFileFormatData()
        {
            DataLayer.Billback obj = new DataLayer.Billback();
          //  dtHDR = obj.GetHDRData();
          //  dtPMT = obj.GetPMTData();
          //  dtTRL = obj.GetTRLData();
        }
    }
}