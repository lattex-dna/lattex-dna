using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace TestApp
{
    class AutoRegex
    {
        public static int[] GetPosition(string input, string pattern) //Position to Paste
        {
            string matchedLine;
            int patternLength = pattern.Length;
            int beginPoint, endPoint;

            if (Regex.IsMatch(pattern, @":\w{3}:{2}\w+/{2}\w+/", RegexOptions.Singleline))  // --- :36B::SETT//UNIT/ 
            {
                if (input.Contains(pattern))
                {
                    string regexPattern = @":\w{3}:{2}\w+/{2}\w+/";
                    matchedLine = Regex.Match(input, regexPattern + @"[\w|\s|,|/|+|*|\r\n]*", RegexOptions.Singleline).Value;
                }
                else
                {
                    patternLength = Regex.Match(input, @":\w{3}:{2}\w+/{2}\w+/", RegexOptions.Singleline).Value.Length;
                    pattern = @":\w{3}:{2}\w+/{2}\w+/";
                    matchedLine = Regex.Match(input, pattern + @"[\w|\s|+|/|,|*]+", RegexOptions.Singleline).Value;
                }
            }
            else if (Regex.IsMatch(pattern, @":\w{3}:/\w+/{2}\w+/", RegexOptions.Singleline)) 
            {
                if (input.Contains(pattern))                                                //--- :35B:/VN//RHTS/
                {
                    string regexPattern = @":\w{3}:/\w+/{2}\w+/";
                    matchedLine = Regex.Match(input, regexPattern + @"\w+[\r\n]*", RegexOptions.Singleline).Value;
                }
                else                                                                        //--- :35B:/VN/
                {
                    pattern = Regex.Replace(pattern, @"//\w+/", "/");
                    patternLength = pattern.Length;
                    string regexPattern = @":\w{3}:/\w+/";
                    matchedLine = Regex.Match(input, regexPattern + @"\w+", RegexOptions.Singleline).Value;
                }
            }
            else if (Regex.IsMatch(pattern, @":\w{3}:{2}\w+/{2}", RegexOptions.Singleline)) // --- :20C::SEME//
            {
                string[] patternSplit = Regex.Split(pattern, @"[:|/]{2}");
                pattern = "[:]" + Regex.Replace(patternSplit[0], "[:|/]{1}", string.Empty) + "[:|/]{2}" +
                                  Regex.Replace(patternSplit[1], "[:|/]{1}", string.Empty) + @"[/]{2}";
                matchedLine = Regex.Match(input, pattern + @"[\w|\s|/|+|*|\r\n]*", RegexOptions.Singleline).Value;
            }
            else                                                                            // --- :35B:/VN/ --- 
            {
                string[] patternSplit = Regex.Split(pattern, @"[:|/]{1}");
                pattern = "[:]" + patternSplit[1] + "[:|/]{1}" + patternSplit[2] + @"[/]{1}";
                matchedLine = Regex.Match(input, pattern + @"[\w|\s|/|+|*|\r\n]*", RegexOptions.Singleline).Value;
            }

            //Note: "patternLength" = before it changed!
            beginPoint = Regex.Match(input, pattern, RegexOptions.Singleline).Index + patternLength;
            endPoint = beginPoint + matchedLine.Length - patternLength;

            return new int[] { beginPoint, endPoint };

        }

        public static string GetData(string input, string pattern) //Data to Copy
        {
            int[] point = GetPosition(input, pattern);

            int beginPoint = point[0];
            int endPoint = point[1];

            string data = input.Substring(beginPoint, endPoint - beginPoint);

            return data;
            
        }

        public static string FindAndReplace(string fileCopy, string filePaste, string[] pattern)
        {
            foreach (string p in pattern)
            {
                if (fileCopy.Contains(p) || filePaste.Contains(p))
                {
                    try
                    {
                        string oldValue = GetData(filePaste, p);
                        string newValue = GetData(fileCopy, p).Replace("\r\n", string.Empty).Replace(" ", string.Empty);

                        if (Regex.IsMatch(oldValue, ",", RegexOptions.Singleline))
                        {
                            newValue = newValue.Insert(newValue.Length, ",");
                        }

                        newValue = newValue.Insert(newValue.Length, "\r\n");

                        if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(newValue)) { }
                        else filePaste = filePaste.Replace(oldValue, newValue);

                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                }
            }
            return filePaste;
            
        }

        public static string RemoveNote(string input)
        {
            input = Regex.Replace(input, @"([*][\w|\s|*|\r\n|/|(|)]+[*])|(\s+[*][\s|\w]+[\r\n]+)|(\s+[*][\w|\s|\r\n]+[*])", "\r\n", RegexOptions.Singleline);
            input = Regex.Replace(input, @"[\r\n]{2,10}", "\r\n", RegexOptions.Singleline);
            return input;
        }

        #region---CSV file---
        public static string ConvertTableToString(string txtTable)
        {
            //remove Column title //(?<!.).*?(?=\r\n\d+)
            txtTable = Regex.Replace(txtTable, @"(?<!.)(.|\r\n)*?(?=\r\n\d+)", string.Empty, RegexOptions.Singleline);
            //remove any break at first index_string
            txtTable = Regex.Replace(txtTable, @"(?<!.)\r\n", string.Empty, RegexOptions.Singleline);
            //replace whitespace by ";"
            txtTable = Regex.Replace(txtTable, @"(?<=\w)\s{2,}?(?=\r\n)|\r\n|\s+\t|\t\s+|\t", ";", RegexOptions.Singleline);
            //remove last row
            txtTable = Regex.Replace(txtTable, @";{3}.+;{2}", string.Empty, RegexOptions.Singleline);

            return txtTable;
        }

        private static string[] csvFields = new string[]
        {
            /*[0]*/ "SYMBOL",
            /*[1]*/ "DEPOSITORY_MEMBER_ID",
            /*[2]*/ "VOLUME",
            /*[3]*/ "MARGIN_VOLUME",
            /*[4]*/ "RECEIVE",
            /*[5]*/ "ODD",
            /*[6]*/ "TOTAL_VOLUME",
            /*[7]*/ "NOTE",
            /*[8]*/ "CUSTODY_NAME",
            /*[9]*/ "ID_NO",
            /*[10]*/ "ISSUED_DATE",
            /*[11]*/ "ADDRESS",
            /*[12]*/ "ACCOUNT_TYPE",
            /*[13]*/ "CUSTODY_KIND",
            /*[14]*/ "NATIONALITY",
            /*[15]*/ "CUSTODY_CODE",
            /*[16]*/ "CASH",
            /*[17]*/ "SECURITY_NAME",
            /*[18]*/ "VOUCHER_NO"
        };

        private static string MakeCa014CSV(string input, string[] dataToReplace)
        {
            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[0] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[0] + @"\)).*?(?=;)", dataToReplace[2]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[2] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[2] + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[3] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[3] + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[4] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[4] + @"\)).*?(?=;)", dataToReplace[7]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[9] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[9] + @"\)).*?(?=;)", dataToReplace[6]);
            }

            if (Regex.IsMatch(input, @"(?<=\(" + csvFields[15] + @"\)).*?(?=;)", RegexOptions.Singleline))
            {
                input = Regex.Replace(input, @"(?<=\(" + csvFields[15] + @"\)).*?(?=;)", dataToReplace[6]);
            }

            return input;
        }

        public static string MakeCSVData(string txtTable, string templateData)
        {
            string[] arrayData = Regex.Split(txtTable, ";;", RegexOptions.Singleline);

            string data = string.Empty;

            for (int i = 0; i < arrayData.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrayData[i]))
                {
                    string[] fields = Regex.Split(arrayData[i], ";", RegexOptions.Singleline);
                    data = data.Insert(data.Length, MakeCa014CSV(templateData, fields));
                }
            }

            data = Regex.Replace(data, @";\r\n", ";", RegexOptions.Singleline);
            data = Regex.Replace(data, @"\(\w+\)|(?<=\d)\.+?(?=\d)", string.Empty);

            return data;
        }
        #endregion---CSV file---
    }

    class FilesListPath
    {
        public static string GetFile(int caseNumber)
        {
            string[] file = new string[]
            {
                "1.MT598_AccountManagementFINISH.txt",
                "2.MT598_AccountManagementREJECT.txt",
                "3.MT544_InstrumentDepositFINISH.txt",
                "4.MT548_InstrumentDepositREJECT.txt",
                "5.MT546_InstrumentUndepositFINISH.txt",
                "6.MT548_InstrumentUndepositREJECT.txt",
                "7.MT546_InstrumentTransferFINISH.txt",
                "8.MT548_InstrumentTransferREJECT.txt",
                "9.MT546_SettlementTransferFINISH.txt",
                "10.MT548_SettlementTransferREJECT.txt",
                "11.MT544_InstrumentTransferReceive.txt",
                "12.MT544_SettlementTransferReceive.txt",
                "13.MT508_InstrumentBlockFINISH.txt",
                "14.MT548_InstrumentBlockREJECT.txt",
                "15.MT508_AdjustInstrumentType.txt",
                "16.MT564_DividendInfoReceive.txt",
                "17.MT598_ConfirmDistributionRightsRECEIVEConfirm.txt",
                "18.MT598_ConfirmDistributionRightsRECEIVEReject.txt",
                "19.MT598_ConfirmDistributionRightsCONFIRMFinish.txt",
                "20.MT598_ConfirmDistributionRightsCONFIRMReject.txt",
                "21.MT546_DividendRightTransferFINISH.txt",
                "22.MT548_DividendRightTransferREJECT.txt",
                "23.MT544_DividendRightTransferReceive.txt",
                "24.MT566_DividendBuyRightFINISH.txt",
                "25.MT567_DividendBuyRightREJECT.txt"
            };

            try
            {
                return Environment.CurrentDirectory.Replace("\\bin\\Debug",
                                                            "\\MTFiles\\" + file[caseNumber - 1].Replace(caseNumber + ".", string.Empty));

            }
            catch (Exception)
            {
                return "File does not exist!";
            }

        }
    }

    class AutoFindAndReplaceString
    {
        private static int[] GetStringIndex(string input, string fromString, string toString, string orToString, bool includeFromTo)
        {
            if (fromString != toString)
            {
                if (includeFromTo == true)
                {
                    int fromIndex, endIndex, orEndIndex;
                    fromIndex = input.IndexOf(fromString);
                    endIndex = input.IndexOf(toString, fromIndex) + toString.Length;
                    orEndIndex = input.IndexOf(orToString, fromIndex) + orToString.Length;
                    if (endIndex == -1 || orEndIndex < endIndex)
                    {
                        return new int[] { fromIndex, orEndIndex };
                    }
                    return new int[] { fromIndex, endIndex };
                }
                else
                {
                    int fromIndex, endIndex, orEndIndex;
                    fromIndex = input.IndexOf(fromString) + fromString.Length;
                    endIndex = input.IndexOf(toString, fromIndex);
                    orEndIndex = input.IndexOf(orToString, fromIndex);
                    if (endIndex == -1 || orEndIndex < endIndex)
                    {
                        return new int[] { fromIndex, orEndIndex };
                    }
                    return new int[] { fromIndex, endIndex };
                }
            }
            else
            {
                if (includeFromTo == true)
                {
                    int fromIndex, endIndex, orEndIndex;
                    fromIndex = input.IndexOf(fromString);
                    endIndex = input.IndexOf(toString, fromIndex + 1) + toString.Length;
                    orEndIndex = input.IndexOf(orToString, fromIndex + 1) + orToString.Length;
                    if (endIndex == -1 || orEndIndex < endIndex)
                    {
                        return new int[] { fromIndex, orEndIndex };
                    }
                    return new int[] { fromIndex, endIndex };
                }
                else
                {
                    int fromIndex, endIndex, orEndIndex;
                    fromIndex = input.IndexOf(fromString) + fromString.Length;
                    endIndex = input.IndexOf(toString, fromIndex + 1);
                    orEndIndex = input.IndexOf(orToString, fromIndex + 1);
                    if (endIndex == -1 || orEndIndex < endIndex)
                    {
                        return new int[] { fromIndex, orEndIndex };
                    }
                    return new int[] { fromIndex, endIndex };
                }
            }
        }

        public static string FindReplaceString(string input, string fromChar, string endChar, string orEndChar, string replacedChar, bool delFormTo, bool replace)
        {
            int[] findString;

            if (delFormTo == true)
            {
                findString = GetStringIndex(input, fromChar, endChar, orEndChar, true);
            }
            else
            {
                findString = GetStringIndex(input, fromChar, endChar, orEndChar, false);
            }

            //Return String or Return selected area
            if (replace == true)
            {
                return input.Replace(input.Substring(findString[0], findString[1]), replacedChar);
            }
            else return input.Substring(findString[0], findString[1]);
        }

        public static string FindReplaceData(string fromString, string toString, string contentCopy, string contentPaste, string addMoreAfter)
        {
            if (fromString.Contains(contentCopy) && toString.Contains(contentPaste))
            {
                string copyData = string.Empty;

                if (contentCopy.Contains("{2:"))
                {
                    toString = FindReplaceString(toString, contentPaste, "\r\n:", "\r\n-}", addMoreAfter, false, true);
                }
                else
                {
                    copyData = FindReplaceString(fromString, contentCopy, "\r\n:", "\r\n-}", string.Empty, false, false);
                    toString = FindReplaceString(toString, contentPaste, "\r\n:", "\r\n-}", copyData + addMoreAfter, false, true);
                }

                return toString;
            }
            else return toString;
        }

        public static string FindReplaceFiles(string fileCopy, string filePaste, string[] contents)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i].Contains("{2:"))
                {
                    filePaste = FindReplaceData(fileCopy, filePaste, contents[i], ":13A::LINK//", "524"); //MT524
                    filePaste = FindReplaceData(fileCopy, filePaste, contents[i], ":13A::LINK//", "542"); //MT542
                }

                if (contents[i].Contains("//UNIT/"))
                {
                    filePaste = FindReplaceData(fileCopy, filePaste, "//UNIT/", "//UNIT/", ",");
                }

                if (contents[i].Contains(":35B:"))
                {
                    filePaste = FindReplaceData(fileCopy, filePaste, ":35B:/VN/", ":35B:/VN//RHTS/", string.Empty);
                }

                if (contents[i].Contains(":20C:"))
                {
                    filePaste = FindReplaceData(fileCopy, filePaste, ":20C::SEME//", ":20C::RELA//", string.Empty);
                }

                filePaste = FindReplaceData(fileCopy, filePaste, contents[i], contents[i], string.Empty);

                if (i == contents.Length - 1)
                {
                    return filePaste;
                }
            }
            return filePaste;
        }

    }
}
