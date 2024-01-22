using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class BillTypeDAL
    {
        public static List<BillType> GetBillType()
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                object paramval = null;
                DBHelper db = new DBHelper();                
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspBillType");

                List<BillType> results = new List<BillType>();
                foreach (DataRow dr in dt.Rows)
                {
                    BillType obj = new BillType();
                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
                        
        }
    }
}
