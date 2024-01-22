using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class CardInfoDAL
    {
        public static List<CardInfo> Get(int intCardID, string strCardID, int intStatus)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intCardID", intCardID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCardID", strCardID, DbType.String));
                cpList.Add(new CustomParameter("@intStatus", intStatus, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspCardInfoGet");

                List<CardInfo> results = new List<CardInfo>();
                foreach (DataRow dr in dt.Rows)
                {

                    CardInfo obj = new CardInfo();

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

        public static int Save(CardInfo obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intCardID", obj.intCardID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCardID", obj.strCardID, DbType.String));
            cpList.Add(new CustomParameter("@intStatus", obj.intStatus, DbType.Int32));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspCardInfoSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

    }
}
