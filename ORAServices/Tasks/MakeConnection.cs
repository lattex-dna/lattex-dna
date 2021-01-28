using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ORAServices.Tasks
{
    public class MakeConnection
    {
        #region---Make a Connection---
        public static OracleConnection oracleConnection()
        {
            string connectString = "User ID= test; Password= z; "
                                 + "Data Source= 192.168.88.224/rpsoft; "
                                 + "Min Pool Size= 1; "
                                 + "Connection Lifetime= 120; "
                                 + "Connection Timeout= 60; "; //+ "Unicode= True";

            if (string.IsNullOrEmpty(connectString))
                return null;
            else
            {
                OracleConnection conn = new OracleConnection(connectString);
                conn.Open();
                return conn;
            }
        }//
        #endregion---Make a Connection---
    }
}