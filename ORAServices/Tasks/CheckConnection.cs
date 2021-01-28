using System.Data;

namespace ORAServices.Tasks
{
    public class CheckConnection
    {
        #region---Check Connection State---
        public static string CheckConnectionState()
        {
            if (MakeConnection.oracleConnection().State == ConnectionState.Open)
            {
                MakeConnection.oracleConnection().Close();
                return "Connected";
            }
            else
            {
                MakeConnection.oracleConnection().Close();
                return "Not Connected";
            }

        }//
        #endregion---Check Connection State---
    }
}