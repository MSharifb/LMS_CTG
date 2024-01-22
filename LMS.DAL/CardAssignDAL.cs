using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class CardAssignDAL
    {
        public static List<CardAssign> Get(int intCardAssignID, string strAssignID, string strEmpID, string strCardID, string dtEffectiveDate)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intCardAssignID", intCardAssignID, DbType.Int32));
                cpList.Add(new CustomParameter("@strAssignID", strAssignID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCardID", strCardID, DbType.String));
                cpList.Add(new CustomParameter("@dtEffectiveDate", dtEffectiveDate, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspCardAssignGet");

                List<CardAssign> results = new List<CardAssign>();
                foreach (DataRow dr in dt.Rows)
                {

                    CardAssign obj = new CardAssign();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static int Save(CardAssign obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intCardAssignID", obj.intCardAssignID, DbType.Int32));
            cpList.Add(new CustomParameter("@strAssignID", obj.strAssignID, DbType.String));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intCardID", obj.intCardID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtEffectiveDate", obj.dtEffectiveDate, DbType.Date));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspCardAssignSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
