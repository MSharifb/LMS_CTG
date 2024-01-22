//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Reflection;
//using System.Web;
//using System.Text;

//namespace LMS.Web
//{
//    public static class Utils
//    {
//        public static void AddObjectPropertiesToQueryString(object obj, NameValueCollection queryString, string objPrefix)
//        {
//            Type objType = obj.GetType();

//            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
//            foreach (PropertyInfo property in properties)
//            {
//                if (property.CanRead && property.CanWrite)
//                {
//                    object value = property.GetValue(obj, null);
//                    if (value != null)
//                        queryString[objPrefix + "." + property.Name] = value.ToString();
//                }

//            }
//        }

//    }
//}
