using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;

namespace ORAServices.Tasks
{
    public class QueryData
    {
        #region---- Query Data----
        public static string QueryDataStr(string email, string hashedPassword)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = MakeConnection.oracleConnection();
            

            string queryTable = "select * from EMPLOYEE_TABLE "
                                        + "where EMAIL= \'" + email + "\' " + "and FIRST_NAME= \'" + hashedPassword + "\'";
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
                    MakeConnection.oracleConnection().Dispose();
                    return null;
                }
            }//
        }
        #endregion---- Query Data ----

        #region---Comment---
        //string numberRow = "select COUNT(*) as LENGTH from EMPLOYEE_TABLE";
        //string queryTable = "select ID_NUMBER, LAST_NAME, FIRST_NAME, AGE, PHONE, DEPARTMENT, POSITION from EMPLOYEE_TABLE";
        //alternative index is ID_NUMBER = 0, LAST_NAME = 1, FIRST_NAME = 2, AGE = 3, PHONE = 4, DEPART = 5, POSI = 6;
        #endregion---Comment---
    }
}