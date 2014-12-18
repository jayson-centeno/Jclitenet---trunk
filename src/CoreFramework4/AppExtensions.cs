using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using CoreFramework4.Implementations.Security;
using System.Web;
using System.Web.Mvc;

namespace CoreFramework4
{

    public static class AppExtensions
    {
        //Format sql quote string
        public static string FormatSqlQuote(this string s)
        {
            if (s == null) return "''";

            int len = s.Length + 25;
            var tmpS = new StringBuilder(len); // hopefully only one alloc

            tmpS.Append("N'");
            tmpS.Append(s.Replace("'", "''"));
            tmpS.Append("'");
            return tmpS.ToString();
        }

        //shortcut of String.Format
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static bool? TryParseBool(this string str)
        {
            bool? ret = null;
            if (str == string.Empty) return ret;

            int? intValue = str.TryParseInt();

            if (!intValue.HasValue)
            {
                ret = bool.Parse(str);
            }

            if (intValue.HasValue && intValue.Value < 2)
            {
                ret = Convert.ToBoolean(intValue.Value);
            }
            return ret;
        }

        public static bool TryParseBool(this int? intValue)
        {
            bool ret = false;
            if (intValue == null) return ret;
            if (intValue.Value < 2)
            {
                ret = Convert.ToBoolean(intValue.Value);
            }
            return ret;
        }

        public static int? TryParseInt(this object obj)
        {
            return (obj == DBNull.Value || obj == null) ? null : obj.ToString().TryParseInt();
        }

        public static DateTime? TryParseDateTime(this string str)
        {
            DateTime dt = DateTime.MinValue;
            DateTime.TryParse(str, out dt);
            if (dt == DateTime.MinValue) return null;
            return dt;
        }

        public static DateTime? TryParseDateTime(this object obj)
        {
            return (obj == DBNull.Value || obj == null) ? null : obj.ToString().TryParseDateTime();
        }

        //Compare 2 string ignore case
        public static bool EqualsIgnoreCase(this string s, string matchWith)
        {
            return s.Equals(matchWith, StringComparison.OrdinalIgnoreCase);
        }

        //parse string if int32 else return 0
        public static bool IsInt32(this string s)
        {
            int x;
            return int.TryParse(s, out x);
        }

        //parse string if int32 else return null
        public static int? TryParseInt(this string str)
        {
            int xValue;
            if (int.TryParse(str, out xValue))
            {
                return xValue;
            }
            return null;
        }

        //check if value is in between the start and end Int value
        public static bool Between(this int n, int start, int end)
        {
            return n >= start && n <= end;
        }

        //check if value is in between the start and end Date value
        public static bool Between(this DateTime n, DateTime start, DateTime end)
        {
            return n >= start && n <= end;
        }

        //check if value is in the collection
        public static bool In(this int n, params int[] sequence)
        {
            return sequence.Any(x => x == n);
        }

        //pick a specified items in the collection.
        public static IEnumerable<T> Pick<T>(this IEnumerable<T> collection, Func<T, bool> when)
        {
            var items = new List<T>();
            foreach (var item in collection)
            {
                if (when(item))
                {
                    items.Add(item);
                }
            }

            return items;
        }

        //parse string if decimal else return 0
        public static decimal? TryParseDecimal(this string str)
        {
            decimal xValue;
            if (decimal.TryParse(str, out xValue))
            {
                return xValue;
            }
            return null;
        }

        public static Guid? TryParseGuid(this string str)
        {
            Guid guid;
            if (Guid.TryParse(str, out guid))
            {
                return guid;
            }
            return null;
        }

        public static LogonInfo ToLogonInfo(this IPrincipal principal)
        {
            return (principal.Identity as LogonInfo);
        }

        public static string ToHtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public static string ToHtmlDecode(this string value)
        {
            return HttpUtility.HtmlDecode(value);
        }

        public static bool IsNullOrEmptyTrimmed(this string value)
        {
            if (value != null)
            {
                return String.IsNullOrEmpty(value.Trim());
            }
            else
            {
                return String.IsNullOrEmpty(value);
            }
        }

        public static void CleanAllBut(this ModelStateDictionary modelState, params string[] includes)
        {
            modelState
              .Where(x => !includes
              .Any(i => String.Compare(i, x.Key, true) == 0))
              .ToList()
              .ForEach(k => modelState.Remove(k));
        }

        public static string ToSlug(this string description)
        {
            var strBuilder = new StringBuilder();

            bool wasHyphen = true;
            foreach (char c in description)
            {
                if (char.IsLetterOrDigit(c))
                {
                    strBuilder.Append(char.ToLower(c));
                    wasHyphen = false;
                }
                else if (char.IsWhiteSpace(c) && !wasHyphen)
                {
                    strBuilder.Append('-');
                    wasHyphen = true;
                }
                else if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherPunctuation && c == '#') 
                {
                    strBuilder.Append("sharp");
                    wasHyphen = false;
                }
            }

            if (wasHyphen && strBuilder.Length > 0) // Avoid trailing hyphens
            {
                strBuilder.Length--;
            }
            return strBuilder.ToString();
        }

    }

}
