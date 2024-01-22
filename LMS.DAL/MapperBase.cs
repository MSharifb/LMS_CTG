using System;

using System.Reflection;
using System.Data.SqlClient;


using System.Data;
using LMSEntity;
namespace LMS.DAL
{
   public class MapperBase
    {
       public static MapperBase objMaper;

       public static MapperBase GetInstance()
       {
           if (objMaper == null)
           {
               objMaper = new MapperBase();
               return objMaper;
           }
           else
           {
               return objMaper;
           }
       }


       public virtual EntityBase MapItem(EntityBase obj, DataRow drContent)
       {
          
           PropertyInfo[] properties = obj.GetType().GetProperties();

           foreach (PropertyInfo property in properties)
           {
               object[] attributes = property.GetCustomAttributes(typeof(LMSEntity.DataColumn), true);

               if (attributes.GetLength(0) > 0)
               {

                   if (((LMSEntity.DataColumn)attributes[0]).IsDataColumn())
                   {
                       if (drContent.Table.Columns.Contains(property.Name))
                       {

                           if (property.PropertyType == typeof(System.String))
                           {
                               //property.SetValue(obj, drContent[property.Name] as string, null);
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? "" : Convert.ToString(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Int32))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? 0 : Convert.ToInt32(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Int64))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? 0 : Convert.ToInt64(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Double))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? 0 : Convert.ToDouble(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Decimal))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? 0 : Convert.ToDecimal(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Boolean))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? false : Convert.ToBoolean(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.DateTime))
                           {
                               property.SetValue(obj, drContent[property.Name] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(drContent[property.Name]), null);
                           }
                           else if (property.PropertyType == typeof(System.Byte []))
                           {
                               if (drContent[property.Name] == DBNull.Value)
                               {
                                   property.SetValue(obj, 0, null);
                               }
                               else 
                               {
                                   property.SetValue(obj, (System.Byte[])drContent[property.Name], null);
                               }
                           }
                       }

                   }

               }
             
           }  
             
           return obj;

       }
    
    }
}
