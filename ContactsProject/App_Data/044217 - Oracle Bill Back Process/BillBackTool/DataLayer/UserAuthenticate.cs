using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace Billback.DataLayer
{
    public class UserAuthenticate
    {
        
        static string ConnStrVIAONE_MC_Interfaces = ConfigurationManager.ConnectionStrings["OracleVIAONE_MC_INTERFACES"].ConnectionString;//DEV

        public static void LogActivity(string strLogCategory, string strLog, string strUserID, string strFName, string strLName, string strSessionID) //VIAONED DEV Database
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "INSERT_LOG";

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
                        OracleParameter IN_V_LOG_CATEGORY = new OracleParameter("V_LOG_CATEGORY", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_LOG = new OracleParameter("V_LOG", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_USER_ID = new OracleParameter("V_USER_ID", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_FIRST_NAME = new OracleParameter("V_F_NAME", OracleDbType.Varchar2, ParameterDirection.Input);
                        OracleParameter IN_V_LAST_NAME = new OracleParameter("V_L_NAME", OracleDbType.Varchar2, ParameterDirection.Input);

                        
                        cmd.Parameters.Add(IN_V_LOG_CATEGORY);
                        cmd.Parameters.Add(IN_V_LOG);
                        cmd.Parameters.Add(IN_V_USER_ID);
                        cmd.Parameters.Add(IN_V_FIRST_NAME);
                        cmd.Parameters.Add(IN_V_LAST_NAME);
                        cmd.Parameters.Add(IN_V_SESSION_ID);

                        IN_V_SESSION_ID.Size = 36;
                        IN_V_SESSION_ID.Value = strSessionID;

                        IN_V_LOG_CATEGORY.Size = 200;
                        IN_V_LOG_CATEGORY.Value = strLogCategory;

                        IN_V_LOG.Size = 1000;
                        IN_V_LOG.Value = strLog;

                        IN_V_USER_ID.Size = 60;
                        IN_V_USER_ID.Value = strUserID;

                        IN_V_FIRST_NAME.Size = 60;
                        IN_V_FIRST_NAME.Value = strFName;

                        IN_V_LAST_NAME.Size = 60;
                        IN_V_LAST_NAME.Value = strLName;

                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        
                        LogActivity("APPLICATION EXCEPTION", e.Message, strUserID, "", "", "");
                        throw;
                    }

                }
            }

        }


        public static bool AuthenticateUser(string strUserID) //VIAONE DEV Database
        {
            string schema = "MC_INTERFACES";
            string package = "BILLBACK_INT";
            string procedure = "GET_APP_USER";
            string outRefCusror = "OUT_REFCURSOR";
            bool Authenticated = false;
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

                        OracleParameter IN_V_USER_ID = new OracleParameter("V_USER_ID", OracleDbType.Varchar2, ParameterDirection.Input);
                        cmd.Parameters.Add(IN_V_USER_ID);
                        IN_V_USER_ID.Size = 60;
                        IN_V_USER_ID.Value = strUserID;

                        cmd.Parameters.Add(outRefCusror, OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader dataReader = cmd.ExecuteReader())
                        {
                            dt.Load(dataReader);
                            if (dt.Rows.Count > 0)
                            {
                                Authenticated = true;
                            }
                            else
                            {
                                Authenticated = false;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        LogActivity("APPLICATION EXCEPTION", e.Message, strUserID, "", "", "");
                        throw;
                    }


                }

            }



            return Authenticated;

        }


      
    }
}