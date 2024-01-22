using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Web
{
    public static class StringFormatter
    {
        public static string FormatDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return "";

            return dateTime.Value.ToString("MMM d, yyyy");
        }


        public static string FormatInputDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return "";

            return dateTime.Value.ToString("MM/dd/yyyy");
        }

        public static string FormatPhone(String phone)
        {
            if (phone == null)
                return "";

            string result;
            if (phone.Length == 10)
            {
                result = phone.Substring(0, 10);
                result = "(" + result.Substring(0, 3) + ") " + result.Substring(3, 3) + "-" + result.Substring(6, 4);
            }
            else
            {
                result = phone;
            }

            return result;

        }
    }
}
