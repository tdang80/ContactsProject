using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Billback.Global;

namespace Billback.DataLayer
{
    public class Billback
    {
        

        string ConnStrSEDGODSD = ConfigurationManager.ConnectionStrings["OracleSEDGODSD"].ConnectionString;
        string ConnStrSEDGODS = ConfigurationManager.ConnectionStrings["OracleSEDGODS"].ConnectionString;//PRD      
        string ConnStrVIAONE_MC_Interfaces = ConfigurationManager.ConnectionStrings["OracleVIAONE_MC_INTERFACES"].ConnectionString;//DEV
        string ConnStrVIAONE = ConfigurationManager.ConnectionStrings["OracleVIAONE"].ConnectionString;//PRD


        //*************PROCEDURE NOT USED  - START*******************
        #region NOT USED
        public DataTable GetOracleDataTest()
        {
            DataTable dt = new DataTable();
            OracleConnection Oconn = new OracleConnection(ConnStrSEDGODSD);
            try
            {
               
                Oconn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Oconn;
                cmd.CommandText = "SELECT CLAIM_UID, INTF_TYPE, VENDOR_ID, BANKNUM FROM BILLBACK_INTERFACE.BILLBACK_PMT WHERE rownum = 1";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();
                
                dt.Load(dr);

            }
            catch
            {

                throw;
            }
            finally
            {
                Oconn.Close();
            }
            return dt;


        }
        #endregion

        #region NOT USED
        //procedure to get claims data using param array...unfinished code
        //public DataTable GetClaimData( OracleParameter[] OclParamArray) //VIAONED DEV Database
        //{
        //    DataTable dt = new DataTable();
        //    OracleConnection Oconn = new OracleConnection(ConnStrVIAONED);
        //    try
        //    {

        //        Oconn.Open();
        //        OracleCommand cmd = new OracleCommand();
        //        cmd.Connection = Oconn;
        //        // original query and command object
        //        //string strSQL = "SELECT A.CONT_NUM AS ClientID, A.FILE_NUM AS Claim#, B.CLMNT_FNAME ClientFName, B.CLMNT_LNAME ClientLName, " +
        //        //                " A.DATE_EVENT AS DOL, PROC_UNIT AS ProcessingUnit, '' AS PayCode, '' AS AllocationAmount " +
        //        //                " FROM VIAONE.CLAIM A " +
        //        //                " INNER JOIN VIAONE.CLMNT B ON A.EVENT_NUM = B.EVENT_NUM " +
        //        //                " WHERE rownum < 50 " +
        //        //                " ORDER BY A.CLAIM_UID ";
        //        //cmd.CommandText = strSQL;
        //        //cmd.CommandType = CommandType.Text;

        //        string strSQL = "SELECT A.CONT_NUM AS ClientID, A.FILE_NUM AS Claim#, B.CLMNT_FNAME ClmntFName, B.CLMNT_LNAME ClmntLName, " +
        //                        " A.DATE_EVENT AS DOL, PROC_UNIT AS ProcessingUnit, '' AS PayCode, '' AS AllocationAmount " +
        //                        " FROM VIAONE.CLAIM A " +
        //                        " INNER JOIN VIAONE.CLMNT B ON A.EVENT_NUM = B.EVENT_NUM " +
        //                        " WHERE DATA_SET = :pDataSet " +
        //                        " AND A.CLAIM_OPEN = :pClaimStatus " +
        //                        " AND A.CONT_NUM = :pClientID " +
        //                       // " AND A.SUBTYPE NOT IN ('AC','CB','CC') " +
        //                       // " AND A.LINE_CODE NOT IN ('AD','AI', 'AL') " +
        //                       // " AND A.CLAIM_TYPE IN ('EL','IN','IO','MO') " +
        //                        " AND ROWNUM < 50 " +
        //                        " ORDER BY A.CONT_NUM, A.CLAIM_UID ";

        //        cmd.CommandText = strSQL;
        //        cmd.CommandType = CommandType.Text;




        //        OracleDataReader dr = cmd.ExecuteReader();

        //        dt.Load(dr);

        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        Oconn.Close();
        //    }
        //    return dt;


        //}
        #endregion

        #region OLD GET CLAIM DATA QUERY NOT USED
        //public DataTable GetClaimData(string DataSet, string ClaimStatus, string ClientID) //VIAONED DEV Database
        //{
        //    DataTable dt = new DataTable();
        //    OracleConnection Oconn = new OracleConnection(ConnStrVIAONED);
        //    try
        //    {
        //        Oconn.Open();
        //        OracleCommand cmd = new OracleCommand();
        //        cmd.Connection = Oconn;

        //        string strSQL = " SELECT A.CLAIM_UID, B.CLMNT_UID, A.CONT_NUM AS ClientID, A.FILE_NUM AS Claim#, B.CLMNT_FNAME ClmntFName, B.CLMNT_LNAME ClmntLName, " +
        //                        " A.DATE_EVENT AS DOL, A.PROC_UNIT AS ProcessingUnit, '' AS PayCode, '' AS AllocationAmount, " +
        //                        " A.VENDOR_ID, A.DATA_SET, A.CLAIM_TYPE, A.DELETED, A.STATE_PAYROLL, ST.STATE_A " +
        //                        " FROM VIAONE.CLAIM A " +
        //                        " INNER JOIN VIAONE.CLMNT B ON A.EVENT_NUM = B.EVENT_NUM " +
        //                        " INNER JOIN VIAONE.CLAIMDWC CW ON A.EVENT_NUM = CW.EVENT_NUM " +
        //                        "   AND CW.CLMNT_NUM = A.CLMNT_NUM " +
        //                        "   AND CW.CLAIM_NUM = A.CLAIM_NUM " +
        //                        "   AND CW.PROC_UNIT = A.PROC_UNIT " +
        //                        " INNER JOIN VIAONE.STATE ST ON ST.STATE_NUM = CAST(CW.STATE_JURIS AS INT) " +
        //                        " WHERE rownum < 50 " +
        //                        " AND DATA_SET = '" + DataSet + "'" +
        //                        " AND A.CLAIM_OPEN = '" + ClaimStatus + "'" +
        //                       " AND A.CONT_NUM = '" + ClientID + "'" +
        //                        " ORDER BY A.CLAIM_UID ";

        //        cmd.CommandText = strSQL;
        //        cmd.CommandType = CommandType.Text;
        //        OracleDataReader dr = cmd.ExecuteReader();
        //        dt.Load(dr);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Oconn.Close();
        //    }
        //    return dt;
        //}
        #endregion

        //*************PROCEDURE NOT USED  - END*******************
    




       

       


        #region VIAONE PRODUCTION - Dynamic Query
        //*****PLEASE NOTE***********
        //GetClaimInfoPRD uses Dynamic Query, GetClaimInfo uses Stored Procedure -- Both accomplish same except GetClaimInfoPRD accesses PRD.
        //This procedure GetClaimInfoPRD will be replaced with Stored Procedure in GetClaimInfo(List<string> ClaimNumbers) once we move to PRD
        //The tool needs to get Claim information from VIAONE Production to process Billback tickets.  
        //*****PLEASE NOTE***********

        public DataTable GetClaimInfoPRD(List<string> ClaimNumbers) //VIAONE PRD Database
        {
            DataTable dt = new DataTable();

            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        string strSQL = " SELECT  " +
                                        " C.FILE_NUM AS CLAIM_NUMBER, C.CONT_NUM AS CLIENT_ID, '' AS TAX_ID, '' AS TAX_SUB, '' AS BINVOICE, " +
                                        " '' AS AMOUNT, '' AS DATE_PAY_FROM, '' AS DATE_PAY_THRU, '' AS SERVICE_TYPE, " +
                                        " C.CLAIM_UID, C.VENDOR_ID, C.STATE_PAYROLL, ST.STATE_A, C.DATA_SET, C.CLAIM_TYPE, C.DELETED, " +
                                        " B.CLMNT_FNAME AS CLMNTFNAME, B.CLMNT_LNAME AS CLMNTLNAME, C.DATE_EVENT AS DOL " +

                                        " FROM VIAONE.CLAIM C  " +
                                        " INNER JOIN VIAONE.CLMNT B ON C.EVENT_NUM = B.EVENT_NUM  " +
                                        " INNER JOIN VIAONE.CLAIMDWC CW " +
                                        "       ON CW.EVENT_NUM = C.EVENT_NUM " +
                                        "           AND CW.CLMNT_NUM = C.CLMNT_NUM " +
                                        "           AND CW.CLAIM_NUM = C.CLAIM_NUM " +
                                        "           AND CW.PROC_UNIT = C.PROC_UNIT " +
                                        " INNER JOIN VIAONE.STATE ST " +
                                        "       ON ST.STATE_NUM = CAST(CW.STATE_JURIS AS INT) " +
                                        "   AND C.FILE_NUM IN ( ";

                        foreach (string strClaim in ClaimNumbers)
                        {
                            if (!String.IsNullOrWhiteSpace(strClaim))
                            {
                                if (strClaim.Trim() != "")
                                {
                                    strSQL += " '" + strClaim + "', ";
                                }
                            }
                        }

                        strSQL = strSQL.Remove(strSQL.Length - 2, 2);  //remove last comma and one space           
                        strSQL += "   ) " +
                        "   ORDER BY C.FILE_NUM ";

                        cmd.CommandText = strSQL;
                        cmd.CommandType = CommandType.Text;
                        OracleDataReader dr = cmd.ExecuteReader();
                        dt.Load(dr);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return dt;
        }


        public DataTable GetClaimInfo(List<string> ClaimList, out string exMessage) //VIAONE
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "BBK_GET_CLAIM_INFO";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dtClaims = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        int numberOfClaims = 0;
                        List<int> Bindsizes = new List<int>();

                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;
                     
                        OracleParameter IN_V_CLAIM_LIST = new OracleParameter("V_CLAIM_LIST", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_CLAIM_LIST);
                        IN_V_CLAIM_LIST.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                        //Create some memory for the size of each claim
                        if (ClaimList != null)
                        {
                            numberOfClaims = ClaimList.Count;
                            for (int i = 0; i < ClaimList.Count; i++)
                            {
                                Bindsizes.Add(30);
                            }
                        }

                        //Concert the claims to an array
                        IN_V_CLAIM_LIST.Value = ClaimList == null ? null : ClaimList.ToArray();
                        IN_V_CLAIM_LIST.Size = numberOfClaims;
                        IN_V_CLAIM_LIST.ArrayBindSize = Bindsizes == null ? null : Bindsizes.ToArray();

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dtClaims.Load(dataReader);                          
                        }
                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }

                }
            }

            return dtClaims;
            
        }

        #endregion

        #region VIAONE MC_INTERFACES - STORED PROCEDURE 
        //************ VIAONE DEV STORED PROCEDURE   - START *****************************
        public DataTable GetLogData_SP(string strCategory, string strSessionID, out string exMessage) //VIAONED 
        {

            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_LOGDATA";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dtLogData = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_SESSION_ID = new OracleParameter("V_SESSION_ID", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_SESSION_ID);
                        IN_V_SESSION_ID.Size = 36;
                        IN_V_SESSION_ID.Value = strSessionID;


                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dtLogData.Load(dataReader);

                            if (strCategory != "ALL")
                            {
                               
                                if (dtLogData.Rows.Count > 0)
                                {
                                    DataView dvLogData = new DataView( dtLogData, "CATEGORY = '" + strCategory + "'","",DataViewRowState.CurrentRows);
                                    dtLogData = dvLogData.ToTable();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }

                }
            }

            return dtLogData;
        }

        public DataTable SearchClientData_By_Name_SP(string strClientName, out string exMessage) //VIAONED 
        {

            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "BBK_GET_CLIENT_BY_NAME";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dtClientData = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_CLIENTNAME = new OracleParameter("V_CLIENTNAME", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_CLIENTNAME);
                        IN_V_CLIENTNAME.Size = 36;
                        IN_V_CLIENTNAME.Value = strClientName;


                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dtClientData.Load(dataReader);                          
                        }
                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }

                }
            }

            return dtClientData;
        }

        public DataTable SearchClientData_By_ID_SP(string strClientID, int SearchExact, out string exMessage) //VIAONED 
        {

            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "BBK_GET_CLIENT_BY_ID";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dtClientData = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_CLIENTID = new OracleParameter("V_CLIENTID", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_CLIENTID);
                        IN_V_CLIENTID.Size = 36;
                        IN_V_CLIENTID.Value = strClientID;

                        OracleParameter IN_V_SEARCH_EXACT = new OracleParameter("V_SEARCH_EXACT", OracleDbType.Int32, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_SEARCH_EXACT);
                        IN_V_SEARCH_EXACT.Size = 36;
                        IN_V_SEARCH_EXACT.Value = SearchExact;

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dtClientData.Load(dataReader);
                        }
                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }

                }
            }

            return dtClientData;
        }

        public DataTable GetClaimDataProc(string DataSet, string ClaimStatus, string ClientID, string ValuationDateFrom, string ValuationDateTo, string ServiceType, string CLM_STATUS, out string exMessage)
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_CLAIMS_PROC2";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dtClaims = new DataTable();
            exMessage = "";
            try
            {

                using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand())
                    {

                        try
                        {
                            command.Connection = connection;
                            command.CommandTimeout = connection.ConnectionTimeout;
                            command.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                            command.CommandType = CommandType.StoredProcedure;
              
                            OracleParameter IN_V_DataSet = new OracleParameter("V_DATA_SET", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_DataSet);
                            IN_V_DataSet.Size = 30;
                            IN_V_DataSet.Value = DataSet;

                            OracleParameter IN_V_Claim_Open = new OracleParameter("V_CLAIM_OPEN", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_Claim_Open);
                            IN_V_Claim_Open.Size = 30;
                            IN_V_Claim_Open.Value = ClaimStatus;

                            OracleParameter IN_V_Cont_Num = new OracleParameter("V_CONT_NUM", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_Cont_Num);
                            IN_V_Cont_Num.Size = 30;
                            IN_V_Cont_Num.Value = ClientID;

                            //valuation date from - used to filter by VIAONE.CLAIMS.DATE_EVENT
                            OracleParameter IN_V_Val_Date_From = new OracleParameter("V_VAL_DATE_FROM", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_Val_Date_From);
                            IN_V_Val_Date_From.Size = 30;
                            IN_V_Val_Date_From.Value = ValuationDateFrom;

                            //valuation date to - used to filter by VIAONE.CLAIMS.DATE_EVENT
                            OracleParameter IN_V_Val_Date_To = new OracleParameter("V_VAL_DATE_TO", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_Val_Date_To);
                            IN_V_Val_Date_To.Size = 30;
                            IN_V_Val_Date_To.Value = ValuationDateTo;

                            //MC_REFER TYPE = SERVICE TYPE  (FOR TCM = "CASM" OR "TCM") (FOR UTR = "UTIL" OR "UR")

                            OracleParameter IN_V_SVC_TYPE1 = new OracleParameter("V_SVC_TYPE1", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_SVC_TYPE1);
                            IN_V_SVC_TYPE1.Size = 30;
                            if (ServiceType == "TCM")
                            {
                                IN_V_SVC_TYPE1.Value = "TCM";
                            }
                            else
	                        {
                                IN_V_SVC_TYPE1.Value = "UR";
                            }

                            OracleParameter IN_V_CLM_STATUS = new OracleParameter("V_CLM_STATUS", OracleDbType.Varchar2, ParameterDirection.Input);
                            command.Parameters.Add(IN_V_CLM_STATUS);
                            IN_V_CLM_STATUS.Size = 30;
                            IN_V_CLM_STATUS.Value = CLM_STATUS;
                            
                          
                            command.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                            using (OracleDataReader dataReader = command.ExecuteReader())
                            {
                                dtClaims.Load(dataReader);

                            }
                        }
                        catch 
                        {
                            throw;
                        }
                    }
                }
            }

            catch (OracleException ex)
            {
                exMessage += "Oracle Exception Message - ";
                exMessage += "Exception Message: " + ex.Message;
                exMessage += "Exception Source: " + ex.Source;
            }
            catch (Exception ex)
            {
                exMessage += "Exception Message - ";
                exMessage += "Exception Message: " + ex.Message;
                exMessage += "Exception Source: " + ex.Source;
            }


            return dtClaims;

        }

        public DataTable GetClaimDataWithOptSearchFilter(DataTable dtOriginal, string OptExcSubTypes, string OptExcLineCodes, string OptIncClaimTypes)
        {
            //this filters given datatable with optional search filters in comma delimited format
            DataTable dtOptExcSubTypes = new DataTable();
            DataTable dtOptExcLineCodes = new DataTable();
            DataTable dtOptIncClaimTypes = new DataTable();

            if (!String.IsNullOrWhiteSpace(OptExcSubTypes))
            {
                DataView dvExcSubTypes = new DataView(dtOriginal);
                dvExcSubTypes.RowFilter = "SUB_TYPE NOT IN (" + OptExcSubTypes + ")";
                dtOptExcSubTypes = dvExcSubTypes.ToTable();
            }
            else
            {
                dtOptExcSubTypes = dtOriginal;
            }

            if (!String.IsNullOrWhiteSpace(OptExcLineCodes))
            {
                DataView dvOptExcLineCodes = new DataView(dtOptExcSubTypes);
                dvOptExcLineCodes.RowFilter = "LINE_CODE NOT IN (" + OptExcLineCodes + ")";
                dtOptExcLineCodes = dvOptExcLineCodes.ToTable();
            }
            else
            {
                dtOptExcLineCodes = dtOptExcSubTypes;
            }

            if (!String.IsNullOrWhiteSpace(OptIncClaimTypes))
            {

                DataView dvOptIncClaimTypes = new DataView(dtOptExcLineCodes);
                dvOptIncClaimTypes.RowFilter = "CLAIM_TYPE IN (" + OptIncClaimTypes + ")";
                dtOptIncClaimTypes = dvOptIncClaimTypes.ToTable();
            }
            else
            {
                dtOptIncClaimTypes = dtOptExcLineCodes;
            }

            return dtOptIncClaimTypes;
        }

        public DataTable GetAllServiceTypes(out string exMessage) //VIAONED DEV Database
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_ALL_SERVICE_TYPES";
            string outRefCusror = "OUT_REFCURSOR";
            exMessage = "";
            DataTable dt = new DataTable();

            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dt.Load(dataReader);
                        }


                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                        throw;
                    }

                }

            }

            return dt;
        }

        public DataTable GetServiceTypeByDispID(int DisplayOrderID, out string exMessage) //VIAONED DEV Database
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_SERVICE_TYPE_BY_ID";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dt = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_DISPLAY_ORDER_ID = new OracleParameter("V_DISPLAY_ORDER_ID", OracleDbType.Int32, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_DISPLAY_ORDER_ID);
                        IN_V_DISPLAY_ORDER_ID.Value = DisplayOrderID;

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dt.Load(dataReader);

                        }

                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }


                }

            }


            return dt;
        }

        public int GetDisplayOrdIDByServiceType(string ServiceType, out string exMessage) //VIAONED DEV Database
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_ID_BY_SERVICE_TYPE";          
            exMessage = "";
           
            int DisplayOrdID = -1;

            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_SERVICE_TYPE = new OracleParameter("V_SERVICE_TYPE", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_SERVICE_TYPE);
                        IN_V_SERVICE_TYPE.Size = 5;
                        IN_V_SERVICE_TYPE.Value = ServiceType;


                        cmd.Parameters.Add("V_DISPLAY_ORD_ID", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.ExecuteNonQuery();

                        DisplayOrdID = Convert.ToInt32(cmd.Parameters["V_DISPLAY_ORD_ID"].Value.ToString());

                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                        throw;
                    }
                }
            }
            return DisplayOrdID;
        }

        internal bool CheckIfServiceTypeExists(string ServiceType, out string exMessage)
        {
            // confirm if service type already exists
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "CHECK_SERVICE_TYPE_EXISTS";
            exMessage = "";

            int DisplayOrdID = -1;
            bool returnVal = false;
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_SERVICE_TYPE = new OracleParameter("V_SERVICE_TYPE", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_SERVICE_TYPE);
                        IN_V_SERVICE_TYPE.Size = 5;
                        IN_V_SERVICE_TYPE.Value = ServiceType;


                        cmd.Parameters.Add("V_DISPLAY_ORD_ID", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.ExecuteNonQuery();

                        DisplayOrdID = Convert.ToInt32(cmd.Parameters["V_DISPLAY_ORD_ID"].Value.ToString());

                        if (DisplayOrdID == -1)
                        {
                            returnVal = false;
                        }
                        else
                        {
                            returnVal = true;
                        }
                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                        throw;
                    }
                }
            }

            return returnVal;
        }

        internal DataTable GetPayCodeLogic(string ServiceType, out string exMessage)
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_PAYCODE_LOGIC";
            string outRefCusror = "OUT_REFCURSOR";
            DataTable dt = new DataTable();
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_SERVICE_TYPE = new OracleParameter("V_SERVICE_TYPE", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_SERVICE_TYPE);
                        IN_V_SERVICE_TYPE.Size = 5;
                        IN_V_SERVICE_TYPE.Value = ServiceType;

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dt.Load(dataReader);
                        }

                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                    }
                }

            }

            return dt;
        }

        internal int FindNextDisplayOrderID(out string exMessage)
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_NEXT_DISPLAY_ORDER";
            exMessage = "";
            int returnVal = 0;

            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_DISPLAY_ORD_ID", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.ExecuteNonQuery();

                        returnVal = Convert.ToInt32(cmd.Parameters["V_DISPLAY_ORD_ID"].Value.ToString());

                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                        returnVal = 200;
                        throw;
                    }
                }
            }
            return returnVal;
        }

        internal void InsertNewServiceType(string strServiceType, string strDescription, string strDisplay, string strDisplayOrdID, string strTaxID, string strTaxSubID,
            int Search_Enable, int Add_Enable, int Import_Enable, int Remove_Enable, int Process_Enable, int PayCodeLogic, string strPayCodeLogicDesc, string strPayCode, out string exMessage)
        {
            //Insert New Service Type
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "INSERT_NEW_SERVICE_TYPE";
            exMessage = "";
            using (OracleConnection connection = new OracleConnection(ConnStrVIAONE_MC_Interfaces))
            {
                connection.Open();
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandTimeout = connection.ConnectionTimeout;
                        cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter IN_V_SERVICE_TYPE = new OracleParameter("V_SERVICE_TYPE", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_DESCRIPTION = new OracleParameter("V_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_DISPLAY = new OracleParameter("V_DISPLAY", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_DISPLAY_ORDER_ID = new OracleParameter("V_DISPLAY_ORDER_ID", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_TAXID = new OracleParameter("V_TAXID", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_TAXSUBID = new OracleParameter("V_TAXSUBID", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_SEARCH_ENABLE = new OracleParameter("V_SEARCH_ENABLE", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_ADD_ENABLE = new OracleParameter("V_ADD_ENABLE", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_IMPORT_ENABLE = new OracleParameter("V_IMPORT_ENABLE", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_REMOVE_ENABLE = new OracleParameter("V_REMOVE_ENABLE", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_PROCESS_ENABLE = new OracleParameter("V_PROCESS_ENABLE", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_PAYCODE_LOGIC = new OracleParameter("V_PAYCODE_LOGIC", OracleDbType.Int32, ParameterDirection.Input);
                        OracleParameter IN_V_PAYCODE_LOGIC_DESC = new OracleParameter("V_PAYCODE_LOGIC_DESC", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_PAYCODE = new OracleParameter("V_PAYCODE", OracleDbType.Varchar2, ParameterDirection.Input);


                        cmd.Parameters.Add(IN_V_SERVICE_TYPE);
                        cmd.Parameters.Add(IN_V_DESCRIPTION);
                        cmd.Parameters.Add(IN_V_DISPLAY);
                        cmd.Parameters.Add(IN_V_DISPLAY_ORDER_ID);
                        cmd.Parameters.Add(IN_V_TAXID);
                        cmd.Parameters.Add(IN_V_TAXSUBID);
                        cmd.Parameters.Add(IN_V_SEARCH_ENABLE);
                        cmd.Parameters.Add(IN_V_ADD_ENABLE);
                        cmd.Parameters.Add(IN_V_IMPORT_ENABLE);
                        cmd.Parameters.Add(IN_V_REMOVE_ENABLE);
                        cmd.Parameters.Add(IN_V_PROCESS_ENABLE);
                        cmd.Parameters.Add(IN_V_PAYCODE_LOGIC);
                        cmd.Parameters.Add(IN_V_PAYCODE_LOGIC_DESC);
                        cmd.Parameters.Add(IN_V_PAYCODE);

                        IN_V_SERVICE_TYPE.Size = 5;
                        IN_V_SERVICE_TYPE.Value = strServiceType;

                        IN_V_DESCRIPTION.Size = 200;
                        IN_V_DESCRIPTION.Value = strDescription;

                        IN_V_DISPLAY.Size = 200;
                        IN_V_DISPLAY.Value = strDisplay;

                        IN_V_DISPLAY_ORDER_ID.Value = strDisplayOrdID;

                        IN_V_TAXID.Size = 25;
                        IN_V_TAXID.Value = strTaxID;

                        IN_V_TAXSUBID.Size = 25;
                        IN_V_TAXSUBID.Value = strTaxSubID;

                        IN_V_DISPLAY.Size = 200;
                        IN_V_DISPLAY.Value = strDisplay;

                        IN_V_SEARCH_ENABLE.Value = Search_Enable;
                        IN_V_ADD_ENABLE.Value = Add_Enable;
                        IN_V_IMPORT_ENABLE.Value = Import_Enable;
                        IN_V_REMOVE_ENABLE.Value = Remove_Enable;
                        IN_V_PROCESS_ENABLE.Value = Process_Enable;
                        IN_V_PAYCODE_LOGIC.Value = PayCodeLogic;

                        IN_V_PAYCODE_LOGIC_DESC.Size = 200;
                        IN_V_PAYCODE_LOGIC_DESC.Value = strPayCodeLogicDesc;

                        IN_V_PAYCODE.Size = 3;
                        IN_V_PAYCODE.Value = strPayCode;

                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        exMessage = e.Message;
                        throw;
                    }

                }
            }




        }

        //************ VIAONE  STORED PROCEDURE   - END *****************************
        #endregion

        #region SEDGODS BILLBACK_INTERFACE - STORED PROCEDURE
        //*****PLEASE NOTE***********
        //This accesses stored proc in SEDGODS PRD to check if bank exists for Claim
        //this is needed to process Billback Tickets
        //************ SEDGODS STORED PROCEDURE   - START *****************************
        public string CheckBankNumber_For_ClaimUID(string ClaimUID, string ClientID, string BatchID, string PayCode)
        {
            string RetVal = "";
            string schema = "BILLBACK_INTERFACE";
            string package = "PKG_BILLBACK_PROCESSING";
            string procedure = "GETBANKNUM_F";
            using (OracleConnection connection = new OracleConnection(ConnStrSEDGODS))
            {

                using (OracleCommand cmd = connection.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = string.Format("{0}.{1}.{2}", schema, package, procedure);
                    // cmd.CommandText = "PKG_BILLBACK_PROCESSING.GETBANKNUM_F";

                    cmd.BindByName = true;


                    cmd.Parameters.Add("P_CLAIM_UID", OracleDbType.Decimal, 80,
                        ClaimUID, ParameterDirection.Input);
                    cmd.Parameters.Add("P_CONTNUM", OracleDbType.Decimal, 80,
                        ClientID, ParameterDirection.Input);
                    cmd.Parameters.Add("P_BATCHID", OracleDbType.Decimal, 80,
                        BatchID, ParameterDirection.Input);
                    cmd.Parameters.Add("P_PAYCODE", OracleDbType.Decimal, 80,
                        PayCode, ParameterDirection.Input);

                    cmd.Parameters.Add("tmpvar", OracleDbType.Varchar2);
                    cmd.Parameters["tmpvar"].Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters["tmpvar"].Size = 255;

                    //cmd.Parameters.Add("tmpvar", OracleDbType.Varchar2,
                    //   ParameterDirection.Output);
                    //cmd.Parameters.Add("v_paycd_type", OracleDbType.Varchar2,
                    //   ParameterDirection.Output);
                    connection.Open();

                    //using (OracleDataReader dr = cmd.ExecuteReader())
                    //{
                    //    // do some work here
                    //    RetVal = dr[0].ToString() + "  " + dr[1].ToString();
                    //}
                    try
                    {
                        cmd.ExecuteNonQuery();

                        RetVal = Convert.ToString(cmd.Parameters["tmpvar"].Value);
                    }
                    catch (Exception)
                    {

                        RetVal = "BANK NOT FOUND";
                    }


                }

            }



            return RetVal;


        }
        //************ SEDGODS STORED PROCEDURE   - END *****************************
        #endregion











    }
}