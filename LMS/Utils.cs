using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LMS.Web
{
    public static class Utils
    {
        public static void AddObjectPropertiesToQueryString(object obj, NameValueCollection queryString, string objPrefix)
        {
            Type objType = obj.GetType();

            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(obj, null);
                    if (value != null)
                        queryString[objPrefix + "." + property.Name] = value.ToString();
                }

            }
        }

        public enum LeaveAppStatus
        {
            Approve = 1,
            Cancel = 2,
            Reject = 3,
            Recommend = 4,
            Submit = 6            
        }


        public enum ApplicationStatus
        {
            Approved = 1,
            Canceled = 2,
            Rejected = 3,
            Recommended = 4,
            Submitted = 6
        }




        public static string GetApplicationStatus(int statusId)
        {
            Array arApplicationStatus = Enum.GetValues(typeof(ApplicationStatus));

            for (int i = 0; i < arApplicationStatus.Length; i++)
            {

                if (Convert.ToInt32(arApplicationStatus.GetValue(i)) == statusId)
                {
                    return arApplicationStatus.GetValue(i).ToString();
                }

            }

            return null;

        }

        public static class ApprovalProcess
        {
            public static string TRUE = "T";
            public static string FALSE = "F";
        }
    
    }
}
