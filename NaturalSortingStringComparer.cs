using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KSPE3Lib
{
    public class NaturalSortingStringComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            bool aNullOrEmpty = String.IsNullOrEmpty(a);
            bool bNullOrEmpty = String.IsNullOrEmpty(b);
            if (aNullOrEmpty && bNullOrEmpty)
                return 0;
            if (aNullOrEmpty && !bNullOrEmpty)
                return -1;
            if (!aNullOrEmpty && bNullOrEmpty)
                return 1;
            if (a.Equals(b, StringComparison.Ordinal))
                return 0;
            int aLength = a.Length;
            int bLength = b.Length;
            int minLength = Math.Min(aLength, bLength);
            int index = -1;
            for (int i = 0; i < minLength; i++)
                if ((a[i].Equals(b[i])))
                    index = i;
                else
                    break;
            if (index >= 0)
                if (index == (minLength - 1))
                    return aLength - bLength;
                else
                {
                    a = a.Substring(index + 1);
                    b = b.Substring(index + 1);
                }
            string numberPattern = @"^\d+";
            Match aMatch = Regex.Match(a, numberPattern);
            Match bMatch = Regex.Match(b, numberPattern);
            if (!aMatch.Success && !bMatch.Success)
                return String.Compare(a,b, StringComparison.Ordinal);
            if (aMatch.Success && !bMatch.Success)
                return -1;
            if (!aMatch.Success && bMatch.Success)
                return 1;
            int aDigits, bDigits;
            int.TryParse(aMatch.ToString(), out aDigits);
            int.TryParse(bMatch.ToString(), out bDigits);
            return aDigits - bDigits;
        }
    }
}
