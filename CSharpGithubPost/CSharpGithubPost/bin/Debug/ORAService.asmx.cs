using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
//using System.Xml;
//using System.Collections;
//using System.Web.Services.Protocols;
//using System.ComponentModel;
//using Soft.Utils;
//using Soft.Controls.Shared;
//using Soft.Controls.Config;
//using Soft.Framework;
//using Devart.Data.Oracle;
//using System.Reflection;
//using System.IO;
//using System.Xml.Serialization;

namespace ORAServices
{
    /// <summary>
    /// Summary description for ORAService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ORAService : System.Web.Services.WebService
    {

        #region---Make a Connection---
        private OracleConnection oracleConnection()
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
        }
        #endregion---Make a Connection---

        #region---Check Connection State---
        [WebMethod]
        public string CheckConnectionState()
        {
            if (oracleConnection().State == System.Data.ConnectionState.Open)
            {
                oracleConnection().Close();
                return "Connected";
            }
            else
            {
                oracleConnection().Close();
                return "Not Connected";
            }

        }
        #endregion---Check Connection State---

        #region---- Query All Data Commented----
        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<string> QueryAllDataListString()
        //{
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
        #endregion---- Query All Data ----

        #region---- Query Data String ----
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string QueryDataString(string email, string hashedPassword)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection();
            //string numberRow = "select COUNT(*) as LENGTH from EMPLOYEE_TABLE";
            //string queryTable = "select ID_NUMBER, LAST_NAME, FIRST_NAME, AGE, PHONE, DEPARTMENT, POSITION from EMPLOYEE_TABLE";
            //alternative index is ID_NUMBER = 0, LAST_NAME = 1, FIRST_NAME = 2, AGE = 3, PHONE = 4, DEPART = 5, POSI = 6;

            email = "22";
            string queryTable = "select * from EMPLOYEE_TABLE " +
                                  "where AGE= \'" + email + "\' " +
                                  "and FIRST_NAME= \'" + hashedPassword + "\'";
            cmd.CommandText = queryTable;
            cmd.CommandType = CommandType.Text;


            using (OracleDataReader oracleReader = cmd.ExecuteReader())
            {
                if (oracleReader.HasRows)
                {
                    string data = string.Empty;
                    //data.Remove(0, data.Length);
                    while (oracleReader.Read())
                    {
                        //int idNumberColumn = oracleReader.GetOrdinal("ID_NUMBER");
                        //long idNumberValue = Convert.ToInt64(oracleReader.GetValue(idNumberColumn));

                        //int lastNameColumn = oracleReader.GetOrdinal("LAST_NAME");
                        //string lastNameValue = oracleReader.GetString(lastNameColumn);

                        //int firstNameColumn = oracleReader.GetOrdinal("FIRST_NAME");
                        //string firstNameValue = oracleReader.GetString(firstNameColumn);

                        //data.Append(idNumberColumn + "/" + idNumberValue + ", " +
                        //              lastNameColumn + "/" + lastNameValue + ", " +
                        //              firstNameColumn + "/" + firstNameValue + ", ");

                        data += oracleReader.GetOrdinal("ID_NUMBER") + "/" + oracleReader["ID_NUMBER"].ToString() + ", " +
                                      oracleReader.GetOrdinal("LAST_NAME") + "/" + oracleReader["LAST_NAME"].ToString() + ", " +
                                      oracleReader.GetOrdinal("FIRST_NAME") + "/" + oracleReader["FIRST_NAME"].ToString() + ", ";

                        return data;

                    }
                    cmd.Connection.Dispose();
                    oracleConnection().Dispose();
                    return null;

                }
                else
                {
                    oracleConnection().Dispose();
                    return null;
                }
            }

        }
        #endregion

        #region---Data Hashing Commented---
        //public const int SALT_BYTE_SIZE = 24;
        //public const int HASH_BYTE_SIZE = 24;
        //public const int PBKDF2_ITERATIONS = 1000;

        //public const int ITERATION_INDEX = 0;
        //public const int SALT_INDEX = 1;
        //public const int PBKDF2_INDEX = 2;

        //public static string CreateHash(string password)
        //{
        //    // Generate a random salt
        //    RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[SALT_BYTE_SIZE];
        //    csprng.GetBytes(salt);

        //    // Hash the password and encode the parameters
        //    byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
        //    return PBKDF2_ITERATIONS + ":" +
        //        Convert.ToBase64String(salt) + ":" +
        //        Convert.ToBase64String(hash);
        //}

        //private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        //{
        //    Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
        //    pbkdf2.IterationCount = iterations;
        //    return pbkdf2.GetBytes(outputBytes);
        //}

        //public static bool ValidatePassword(string password, string correctHash)
        //{
        //    // Extract the parameters from the hash
        //    char[] delimiter = { ':' };
        //    string[] split = correctHash.Split(delimiter);
        //    int iterations = Int32.Parse(split[ITERATION_INDEX]);
        //    byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
        //    byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

        //    byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
        //    return SlowEquals(hash, testHash);
        //}

        //private static bool SlowEquals(byte[] a, byte[] b)
        //{
        //    uint diff = (uint)a.Length ^ (uint)b.Length;
        //    for (int i = 0; i < a.Length && i < b.Length; i++)
        //        diff |= (uint)(a[i] ^ b[i]);
        //    return diff == 0;
        //}

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public bool HashingTest(string myPassword)
        //{
        //    string hashedPass = CreateHash(myPassword);
        //    bool tf = ValidatePassword(myPassword, hashedPass);
        //    return tf;
        //}
        #endregion---Data Hashing Commented---

        #region---Hasing---
        static string ComputeSHA512Hash(string rawData)
        {
            // Create a SHA512
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); //x2
                }
                return builder.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> HashingTest(string plainText)
        {
            plainText = "MrA"; //TestValue
            string hashedData = ComputeSHA512Hash(plainText);

            bool tf = false;
            if (hashedData == ComputeSHA512Hash("MrA"))
            {
                tf = true;
            }

            List<string> data = new List<string>
            {
                string.Format("Raw data: {0}", plainText),
                string.Format("Hash: {0}", hashedData),
                string.Format("SHA512Hash: {0}", ComputeSHA512Hash("MrA")),
                string.Format("Comparation {0} Result", tf.ToString())
            };
            return data;
        }
        #endregion

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RegN(string user, string pass)
        {

            return null;
        }

        #region---RefferenceCodeCommented---
        //string userHostName
        //{
        //    get
        //    {
        //        object val = Session["userHostName"];
        //        if (val != null)
        //            return (string)val;
        //        else return "";
        //    }
        //    set { Session["userHostName"] = value; }
        //}
        //public override OracleConnection DbConn
        //{
        //    get
        //    {
        //        if (_dbConn == null)
        //        {
        //            _dbConn = new OracleConnection(ConnectString);
        //            _dbConn.AutoCommit = false;
        //            _dbConn.Open();

        //            OracleCommand cmd = new OracleCommand("trans_pkg.set_identification", _dbConn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            HttpRequest request = Context.Request;

        //            cmd.Parameters.Add("p_user_name", Username);
        //            cmd.Parameters.Add("p_sid", Session.SessionID);
        //            cmd.Parameters.Add("p_host_name", UserHostName);
        //            cmd.Parameters.Add("p_host_address", request.UserHostAddress);
        //            cmd.ExecuteNonQuery();

        //            //Log.Write("UserHostName=" + UserHostName);
        //        }
        //        return _dbConn;
        //    }
        //}

        //[WebMethod]
        //public DateTime Ping()
        //{
        //    return DateTime.Now;
        //}

        //public ActionResult GetTime()
        //{
        //    return new ActionResult(new object[] { DateTime.Now });
        //}
        #endregion---RefferenceCodeCommented---
    }
}
