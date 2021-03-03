using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    class AutoFindAndReplace
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
