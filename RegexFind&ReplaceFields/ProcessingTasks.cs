using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace VSDv2TestApp
{
    class ProcessingTasks
    {
        public static string Processing(string fromData, string destinationData, string jsonSubstringBeginning)
        {
            DateTime fDate = new DateTime(2022, 03, 01, 00, 00, 00);
            if (DateTime.Compare(DateTime.Now, fDate) <= 0)
            {
                Dictionary<string, string> allFields = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSubstringBeginning);

                foreach (KeyValuePair<string, string> field in allFields)
                {
                    string copyData = string.Empty;
                    if (!string.IsNullOrEmpty(field.Key) && !string.IsNullOrEmpty(field.Value))
                    {
                        copyData = Regex.Match(fromData, string.Format("(?<={0}).*?(?=\r\n)", field.Key), RegexOptions.Singleline).Value;

                        string[] values = Regex.Split(field.Value, ",", RegexOptions.Singleline);
                        foreach (string val in values)
                        {
                            if (string.IsNullOrEmpty(val))
                                destinationData = Regex.Replace(destinationData, string.Format("(?<={0}).*?(?=\r\n)", field.Key), copyData, RegexOptions.Singleline);
                            else
                                destinationData = Regex.Replace(destinationData, string.Format("(?<={0}).*?(?=\r\n)", val), copyData, RegexOptions.Singleline);
                        }

                    }
                    else
                    {
                        copyData = Regex.Match(fromData, string.Format("(?<={0}).*?(?=\r\n)", field.Key), RegexOptions.Singleline).Value;
                        destinationData = Regex.Replace(destinationData, string.Format("(?<={0}).*?(?=\r\n)", field.Key), copyData, RegexOptions.Singleline);
                    }

                }
                return destinationData;
            }

            return "No Data!";
        }

        public static string ConvertTableToString(string txtTable)
        {
            DateTime fDate = new DateTime(2022, 03, 01, 00, 00, 00);
            if (DateTime.Compare(DateTime.Now, fDate) <= 0)
            {
                //remove Column title //(?<!.).*?(?=\r\n\d+)
                txtTable = Regex.Replace(txtTable, @"(?<!.)(.|\r\n)*?(?=\r\n\d+)", string.Empty, RegexOptions.Singleline);
                //remove any break at first index_string
                txtTable = Regex.Replace(txtTable, @"(?<!.)\r\n", string.Empty, RegexOptions.Singleline);
                //replace whitespace by ";"
                txtTable = Regex.Replace(txtTable, @"(?<=\w)\s{2,}?(?=\r\n)|\r\n|\s+\t|\t\s+|\t", ";", RegexOptions.Singleline);
                //remove last row (total row)
                txtTable = Regex.Replace(txtTable, @";{3}.+;{2}", string.Empty, RegexOptions.Singleline);
            }
            return txtTable;
        }

        public static readonly string symbol = "SYMBOL";
        public static readonly string volume = "VOLUME";
        public static readonly string right = "RIGHT";
        public static readonly string note = "NOTE";
        public static readonly string cusName = "CUSTODY_NAME";
        public static readonly string idNo = "ID_NO";
        public static readonly string issDate = "ISSUED_DATE";
        public static readonly string address = "ADDRESS";
        public static readonly string accType = "ACCOUNT_TYPE";
        public static readonly string cusKind = "CUSTODY_KIND";
        public static readonly string nationality = "NATIONALITY";
        public static readonly string cusType = "CUSTODY_TYPE";
        public static readonly string cusCode = "CUSTODY_CODE";
        public static readonly string securityName = "SECURITY_NAME";
        public static readonly string depMemberID = "DEPOSITORY_MEMBER_ID";
        public static readonly string magrinVolume = "MARGIN_VOLUME";
        public static readonly string totalVolume = "TOTAL_VOLUME";
        public static readonly string voucherNo = "VOUCHER_NO";
        public static readonly string receive = "RECEIVE";
        public static readonly string odd = "ODD";
        public static readonly string cash = "CASH";

        private static string CSV005RawData(string input, string[] dataToReplace)
        {
            if (Regex.IsMatch(input, @"(?<=\(" + symbol + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + symbol + @"\)).*?(?=;)", dataToReplace[2]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + volume + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + volume + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + right + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + right + @"\)).*?(?=;)", dataToReplace[8]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + note + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + note + @"\)).*?(?=;)", dataToReplace[1]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + idNo + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + idNo + @"\)).*?(?=;)", dataToReplace[6]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + cusCode + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + cusCode + @"\)).*?(?=;)", dataToReplace[6]);
            }

            return input;
        }

        public static string MakeCA005CSVData(string txtTable, string templateData)
        {
            DateTime fDate = new DateTime(2022, 03, 01, 00, 00, 00);

            string[] arrayData = Regex.Split(txtTable, ";;", RegexOptions.Singleline);

            string data = string.Empty;
            if (DateTime.Compare(DateTime.Now, fDate) <= 0)
            {
                for (int i = 0; i < arrayData.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrayData[i]))
                    {
                        string[] fields = Regex.Split(arrayData[i], ";", RegexOptions.Singleline);
                        data = data.Insert(data.Length, CSV005RawData(templateData, fields));
                    }
                }

                data = Regex.Replace(data, @";\r\n", ";", RegexOptions.Singleline);
                data = Regex.Replace(data, @"(\s|\r\n){2,}", "\r\n", RegexOptions.Singleline);
                data = Regex.Replace(data, @"\(\w+\)|(?<=\d)\.+?(?=\d)", string.Empty);
            }

            return data;
        }

        private static string CA014RawData(string input, string[] dataToReplace)
        {
            if (Regex.IsMatch(input, @"(?<=\(" + symbol + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + symbol + @"\)).*?(?=;)", dataToReplace[2]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + volume + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + volume + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + magrinVolume + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + magrinVolume + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + receive + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + receive + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + idNo + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + idNo + @"\)).*?(?=;)", dataToReplace[6]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + cusCode + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + cusCode + @"\)).*?(?=;)", dataToReplace[6]);
            }

            return input;
        }

        public static string MakeCA014CSVData(string txtTable, string templateData)
        {
            string[] arrayData = Regex.Split(txtTable, ";;", RegexOptions.Singleline);

            string data = string.Empty;

            DateTime fDate = new DateTime(2022, 03, 01, 00, 00, 00);
            if (DateTime.Compare(DateTime.Now, fDate) <= 0)
            {
                for (int i = 0; i < arrayData.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrayData[i]))
                    {
                        string[] fields = Regex.Split(arrayData[i], ";", RegexOptions.Singleline);
                        data = data.Insert(data.Length, CA014RawData(templateData, fields));
                    }
                }

                data = Regex.Replace(data, @";\r\n", ";", RegexOptions.Singleline);
                data = Regex.Replace(data, @"\(\w+\)|(?<=\d)\.+?(?=\d)", string.Empty);
            }
            return data;
        }
    }

    class SubstringBeginningRecovery
    {
        private string keys, values;
        public string Key { get { return keys; } set { keys = value; } }
        public string Value { get { return values; } set { values = value; } }
       
    }

    class DllAssemblyLoad
    {
        public static void Load(string assemblyPath)
        {
            //string assemblyPath = @"D:\Machines\LinesFindAndReplace\LinesFindAndReplace\bin\Debug\LinesFindAndReplace.dll";
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            foreach (Type type in assembly.GetExportedTypes())
            {
                object target = Activator.CreateInstance(type);
                type.InvokeMember("Output", BindingFlags.InvokeMethod, null, target, new object[] { @"Hello" });
            }
        }
    }
}
