using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ImportAttendRawDataDAL
    {

        public static int SaveItem(string strFileLocation, string strDBUser, string strDBPW, int numMinID, int numMaxID, string strIUser)
        {

            CustomParameterList cpList = new CustomParameterList();

            if (strDBPW == null)
                strDBPW = "";

            cpList.Add(new CustomParameter("@strFileLocation", strFileLocation, DbType.String));
            cpList.Add(new CustomParameter("@strDBUser", strDBUser, DbType.String));
            cpList.Add(new CustomParameter("@strDBPW", strDBPW, DbType.String));
            cpList.Add(new CustomParameter("@numMinID", numMinID, DbType.String));
            cpList.Add(new CustomParameter("@numMaxID", numMaxID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", strIUser, DbType.String));
                   
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspImportAttendRawData");
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }
    }
}
