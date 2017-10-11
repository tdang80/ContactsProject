using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

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
       // string FilePath = @"C:\sample.txt";
        string FilePath = @"\\memfp02\SHARE\Any\Guardians\Billback";
        public string FileName;
        public string strDate; 
      

        public FileStream ExportBillbackFile(DataTable dtClaimsData, string ServiceType, string FileNumber5_Digits)
        {
            this.ServiceType = ServiceType;
            this.FileNumber = FileNumber5_Digits.ToString();
            dtClaimSearchData = dtClaimsData;
            FileStream fs = CreateBillbackIntFile();
            return fs;
            // DropFileInServer();
        }

        private FileStream CreateBillbackIntFile()
        {
           //Create Billback file in pipe delimitted format
           // LoadFileFormatData();
            strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");
            string sHDRLine = CreateHDRLine();
            string[] sPMTLine = new string[dtClaimSearchData.Rows.Count];
            string sTRLLine = CreateTRLLine();
            
            int rownum = 0;
            //loop through each row in dtClaimSearchData
            string state = "";
            foreach (DataRow row in dtClaimSearchData.Rows)
            {
                
                sPMTLine[rownum] = CreatePMTLine(row);
                rownum++;
            }

            FileStream fs = CreatePipeFile(sHDRLine, sPMTLine, sTRLLine);
            return fs;
           // System.Diagnostics.Process.Start("explorer", @"c:\\");
        }

        private FileStream CreatePipeFile(string sHDR, string[] sPMT, string sTRL )
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




           //

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath + "\\" + FileName))
                {
                    sw.WriteLine(sHDR);

                    foreach (string strPMT in sPMT)
                    {
                        sw.WriteLine(strPMT);
                    }

                    sw.WriteLine(sTRL);
                    
                }

                FileStream fs = new FileStream(FilePath, FileMode.Create);
                return fs;
            

                //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                //{
                    //FOR NOW JUST OUTPUT THE FILE WITHOUT WRITING TO DISK
                //    string filename = "BILLBACK_" + this.FileNumber + "_" + ServiceType + "FEE_" + strDate + "_s430_999";
                //    string attachment = "attachment; filename=" + filename + ".xlsx";

                //    ms.WriteLine(sHDR);

                //    foreach (string strPMT in sPMT)
                //    {
                //        sw.WriteLine(strPMT);
                //    }

                //    sw.WriteLine(sTRL);

                //   

                //}
                
              
       //     }


        }

        private string CreateTRLLine()
        {
            string[] strArray = new string[19];
           // string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");

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
           // string strDate = DateTime.Now.ToString("yyyMMdd_HHmmss");

            strArray[0] = "HDR";
            strArray[1] = "BILLBACK_"+this.FileNumber+"_"+ServiceType+"FEE_"+strDate +"_s430_999";
            FilePath = @"c:\" + strArray[1];
            FileName = strArray[1];
            strArray[2] = ServiceType + "FEE";

            for (int i = 3; i < 18; i++)
            {
                strArray[i] = "";
            }

            return String.Join("|",strArray);
        }

        private string CreatePMTLine(DataRow dr )
        {
            string[] strArray = new string[19];
            string strDate = DateTime.Now.ToString("MM/dd/yyy");
            string strDateFrom = "";
            string strDateThru = "";
            string strState_data = "";
            string strStateCode = "";
            if (Common.common.IsValidDate(dr["DATEFROM"].ToString()) )
            {
                strDateFrom = DateTime.Parse(dr["DATEFROM"].ToString()).Date.ToString("MM/dd/yyyy");
            }
            if (Common.common.IsValidDate(dr["DATETHRU"].ToString()) )
            {
                strDateThru = DateTime.Parse(dr["DATETHRU"].ToString()).Date.ToString("MM/dd/yyyy");
            }

            strArray[0] = "PMT";
            strArray[1] = strDate;
            strArray[2] = dr["CLAIM_UID"].ToString();
            strArray[3] = "P";//always P
            strArray[4] = "";//always blank
            strArray[5] = "362685608"; //constant
            switch (ServiceType) //used for P-taxsub
            {
                case "TCM":
                case "UTR":
                    strArray[6] = "404";//***P-taxsub ...from caller file or use 402 for CLC or CAT, use 404 for TCM
                    break;  
                case "CLC":
                case "CAT":
                    strArray[6] = "402";//***P-taxsub ...from caller file or use 402 for CLC or CAT, use 404 for TCM
                    break;
                default:
                    strArray[6] = "****NEED P-TAX-SUB****";//***P-taxsub ...from caller file NEED P-TAX SUB for OSA and ITK
                    break;

            }

            strState_data = dr["STATE_A"].ToString().Trim();
            if (strState_data == "CA")
                strStateCode = "3"; //if California paycode is medical and starts with 3
            else
                strStateCode = "6";//if not California paycode use expense paycode and starts with 6
            
            switch (ServiceType) //used for paycode
            {

                case "TCM":
                    strArray[7] = strStateCode + "45"; //** paycode TCM *45
                    break;
                case "UTR":
                    strArray[7] = strStateCode +"48"; //** paycode UTR *48
                    break;
                case "CLC":
                    strArray[7] = strStateCode +"32"; //** paycode CLC *32
                    break;
                case "CAT":
                    strArray[7] = strStateCode +"31"; //** paycode CAT *31
                    break;
                case "OSA":
                case "ITK":
                    strArray[7] = strStateCode + "97"; //** paycode OSA or ITK
                    break;
              
            }
           
           
         //   strArray[7] = dr["PAYCODE"].ToString().Trim();//***Pay Code from caller file
            strArray[8] = dr["BINVOICE"].ToString().Trim();//***BInvoice can be blank unless provided by caller 
            strArray[9] = "";//ICN always blank

            strArray[10] = dr["VENDOR_ID"].ToString();
            strArray[11] = dr["ALLOCATIONAMOUNT"].ToString().Trim();
            strArray[12] = strDate;//Date bill, today date
            strArray[13] = strDateFrom;//***Date Pay From provided by caller
            strArray[14] = strDateThru;//***Date Pay Thru provided by caller
            strArray[15] = strDate;//Date Print, today date
            strArray[16] = strStateCode;
            //string strPayCode = dr["PAYCODE"].ToString().Trim();
            //if (strPayCode.Length != 0)
            //    strArray[16] = strPayCode.Substring(0,1);//Pay category must match 1st digit of pay code 3 for medical, 6 for expense
            //else
            //{
            //   // strArray[16] = "****NEED PAY CATEGORY****";
            //    strArray[16] = "3";//for now hardcode, once we import from caller file we should get paycode from it and get 1st digit
            //}
            strArray[17] = ""; //blank
            strArray[18] = ServiceType + "FEE - "+ DateTime.Now.ToString("MMMM")+" " +DateTime.Now.ToString("yyyy")+" - "+dr["CLIENTID"].ToString();//comment

          

            return String.Join("|", strArray);
        }

        private void LoadFileFormatData()
        {
            DataLayer.Billback obj = new DataLayer.Billback();
            dtHDR = obj.GetHDRData();
            dtPMT = obj.GetPMTData();
            dtTRL = obj.GetTRLData();
        }
    }
}