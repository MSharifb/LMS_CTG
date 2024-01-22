using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using TVL.DB;
using LMS.BLL;
using LMSEntity;

namespace LMS.DAL
{
    public class EmployeePhotoDAL
    {
        public static List<EmployeePhoto> GetItemList(string EmployeeId, string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@EmployeeId", EmployeeId, DbType.String));

                //cpList.Add(new CustomParameter("@numErrorCode", 0, DbType.Int32));
                //cpList.Add(new CustomParameter("@strErrorMsg", "", DbType.String));

                /*
                    cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                    cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                    cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.String));
                    cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.String));
                    cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.String));
                */
               
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "PRM_uspEmployeePhotoGet");
                numTotalRows = dt.Rows.Count;
                List<EmployeePhoto> results = new List<EmployeePhoto>();
                
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeePhoto obj = new EmployeePhoto();
                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }
                return results.OrderBy(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
