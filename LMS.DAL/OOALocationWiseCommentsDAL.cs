using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class OOALocationWiseCommentsDAL
    {
        public static int SaveData(OOALocationWiseComments obj, IDbTransaction transaction, IDbConnection con, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@COMMENTID", obj.COMMENTID, DbType.Int64));
            cpList.Add(new CustomParameter("@OUTOFOFFICEID", obj.OUTOFOFFICEID, DbType.Int64));
            cpList.Add(new CustomParameter("@LOCATIONID", obj.LOCATIONID, DbType.Int32));
            cpList.Add(new CustomParameter("@STRAUTHORID", obj.STRAUTHORID, DbType.String));
            cpList.Add(new CustomParameter("@INTAUTHORTYPEID", obj.INTAUTHORTYPEID, DbType.Int32));
            cpList.Add(new CustomParameter("@STRCOMMENTS", obj.STRCOMMENTS, DbType.String));
            cpList.Add(new CustomParameter("@STRIUSER", obj.STRIUSER, DbType.String));
            cpList.Add(new CustomParameter("@STREUSER", obj.STREUSER, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));


            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "OOA_USPLOCATIONWISECOMMENTSAVE");
                

            }
            catch (Exception ex)
            {
                return -5000;
            }

        }

        public static List<OOALocationWiseComments> Get(OOALocationWiseComments obj)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@COMMENTID", obj.COMMENTID, DbType.Int64));
                cpList.Add(new CustomParameter("@OUTOFOFFICEID", obj.OUTOFOFFICEID, DbType.Int64));
                cpList.Add(new CustomParameter("@LOCATIONID", obj.LOCATIONID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPLOCATIONWISECOMMENTGET");

                List<OOALocationWiseComments> results = new List<OOALocationWiseComments>();
                foreach (DataRow dr in dt.Rows)
                {

                    OOALocationWiseComments objData = new OOALocationWiseComments();

                    MapperBase.GetInstance().MapItem(objData, dr); ;
                    results.Add(objData);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
