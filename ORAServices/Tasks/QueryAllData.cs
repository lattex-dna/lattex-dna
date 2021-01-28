using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Oracle.DataAccess.Client;
using ORAServices.Tasks;

namespace ORAServices.Tasks
{
    public class QueryAllData
    {
        #region---- Query All Data----
        public static string QueryAllDataStr(string email, string hashedPassword)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = MakeConnection.oracleConnection();
            
            if (!string.IsNullOrEmpty(EmailServices.ValidEmail(email)))
            {
                string emailStandard = email;
                string queryTable = "select * from EMPLOYEE_TABLE "
                                            + "where AGE= \'" + emailStandard + "\' " + "and FIRST_NAME= \'" + hashedPassword + "\'";
                cmd.CommandText = queryTable;
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader oracleReader = cmd.ExecuteReader())
                {
                    if (oracleReader.HasRows)
                    {
                        StringBuilder data = new StringBuilder(string.Empty);
                        data.Remove(0, data.Length);
                        while (oracleReader.Read())
                        {
                            int idNumberColumn = oracleReader.GetOrdinal("ID_NUMBER"); //Column Number
                            long idNumberValue = Convert.ToInt64(oracleReader.GetValue(idNumberColumn)); //Value of Coulumn

                            int lastNameColumn = oracleReader.GetOrdinal("LAST_NAME");
                            string lastNameValue = oracleReader.GetString(lastNameColumn);

                            int firstNameColumn = oracleReader.GetOrdinal("FIRST_NAME");
                            string firstNameValue = oracleReader.GetString(firstNameColumn);

                            data.Append(idNumberColumn + "/" + idNumberValue + ", " +
                                          lastNameColumn + "/" + lastNameValue + ", " +
                                          firstNameColumn + "/" + firstNameValue + ", " +
                                          oracleReader["ID_NUMBER"].ToString());

                        }
                        cmd.Connection.Dispose();
                        MakeConnection.oracleConnection().Dispose();
                        return data.ToString();
                    }
                    else
                    {
                        cmd.Connection.Dispose();
                        MakeConnection.oracleConnection().Dispose();
                        return "No data!";
                    }
                }
            }
            else
                return "Email error!";
        }//
        #endregion---- Query All Data ----

        #region---Comment---
        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<string> QueryAllDataListString()
        //{
        //    string numberRow = "select COUNT(*) as LENGTH from EMPLOYEE_TABLE";
        //    string queryTable = "select ID_NUMBER, EMAIL, PASSWORD, LAST_NAME, FIRST_NAME, BIRTHDAY, PHONE, DEPARTMENT, POSITION from EMPLOYEE_TABLE";
        //    alternative index is ID_NUMBER = 0, LAST_NAME = 1, FIRST_NAME = 2, AGE = 3, PHONE = 4, DEPART = 5, POSI = 6;
        //    string sql = "select ID_NUMBER, LAST_NAME, FIRST_NAME, AGE, PHONE, DEPARTMENT, POSITION from EMPLOYEE_TABLE";
        //    //alternative index is ID_NUMBER = 0, LAST_NAME = 1, FIRST_NAME = 2, AGE = 3, PHONE = 4, DEPART = 5, POSI = 6;

        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = oracleConnection();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = CommandType.Text;

        //    using (OracleDataReader oracleReader = cmd.ExecuteReader())
        //    {
        //        if (oracleReader.HasRows)
        //        {
        //            while (oracleReader.Read())
        //            {
        //                //Get ID_NUMBER index column in SQL cmd (if don't know)
        //                int idNumberColumn = oracleReader.GetOrdinal("ID_NUMBER");
        //                //Get Value if knew SQL index column is 0
        //                long idNumberValue = Convert.ToInt64(oracleReader.GetValue(0));
        //                //string idNumberVal = reader.GetValue(0).ToString();

        //                //Get value if knew LAST_NAME index column is 1
        //                //string lastName = oracleReader.GetString(1);
        //                //____
        //                //Get LAST_NAME index column in SQL cmd (if don't know)
        //                int lastNameColumn = oracleReader.GetOrdinal("LAST_NAME");
        //                //Get value by SQL string name "LAST_NAME"
        //                string lastNameValue = oracleReader.GetString(lastNameColumn);

        //                List<string> data = new List<string>
        //                {
        //                    "id Number SQL Column: " + idNumberColumn,
        //                    "id Number Value: " + idNumberValue,

        //                    //"Last Name Value (num): " + lastName,
        //                    "Last Name SQL Column: "+ lastNameColumn,
        //                    "Last Name Value: " +  lastNameValue
        //                };

        //                cmd.Connection.Dispose();
        //                return data;

        //                #region----Console(Commented)----
        //                //int mngIdIndex = reader.GetOrdinal("LAST_NAME");

        //                //long? mngId = null;

        //                //if (!reader.IsDBNull(mngIdIndex))
        //                //{
        //                //    mngId = Convert.ToInt64(reader.GetValue(mngIdIndex));
        //                //}
        //                //Console.WriteLine("--------------------");
        //                //Console.WriteLine("empIdIndex:" + empIdIndex);
        //                //Console.WriteLine("EmpId:" + empId);
        //                //Console.WriteLine("EmpNo:" + empNo);
        //                //Console.WriteLine("EmpName:" + empName);
        //                //Console.WriteLine("MngId:" + mngId);
        //                #endregion
        //            }
        //            oracleConnection().Dispose();
        //            return null;

        //        }
        //        else
        //        {
        //            oracleConnection().Dispose();
        //            return null;
        //        }
        //    }
        //}
        #endregion---Comment---
    }
}