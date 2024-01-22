using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LMS.Util
{
    public class PropertyReflector
    {
        /// <summary>
        /// Copy values from identical objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="toObject"></param>
        /// <param name="primaryKeyFieldName"></param>
        /// <returns>T</returns>
        public static T SetProperties<T>(T fromObject, T toObject, string primaryKeyFieldName  )
        {
            foreach (PropertyInfo field in typeof(T).GetProperties())
            {
                if (field.Name != primaryKeyFieldName)
                {
                    field.SetValue(toObject, field.GetValue(fromObject, null), null);
                }
            }
            return toObject;
        }

        /// <summary>
        /// Copy values form same properties of different type of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="fromRecord"></param>
        /// <param name="toRecord"></param>
        /// <returns>T</returns>
        public static T SetProperties<T, U>(U fromRecord, T toRecord)
        {
            foreach (PropertyInfo fromField in typeof(U).GetProperties())
            {
                if (fromField.Name != "Id")
                {
                    foreach (PropertyInfo toField in typeof(T).GetProperties())
                    {
                        if (fromField.Name == toField.Name)
                        {
                            toField.SetValue(toRecord, fromField.GetValue(fromRecord, null), null);
                            break;
                        }
                    }
                }
            }
            return toRecord;
        }
    }
}
