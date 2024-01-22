using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMSEntity;
using TVL.DB;
using System.Data;

namespace LMS.DAL
{
    public class OOAApprovalCommentsDAL
    {
        public static int SaveData(OOAApprovalComments obj,string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@RECORDID", obj.RECORDID, DbType.Int64));
            cpList.Add(new CustomParameter("@INTOUTOFOFFICEID", obj.INTOUTOFOFFICEID, DbType.Int64));
            cpList.Add(new CustomParameter("@INTFLOWPATHID", obj.INTFLOWPATHID, DbType.Int32));
            cpList.Add(new CustomParameter("@STRAPPROVERID", obj.STRAPPROVERID, DbType.String));
            cpList.Add(new CustomParameter("@INTAPPROVERTYPEID", obj.INTAPPROVERTYPEID, DbType.Int32));
            cpList.Add(new CustomParameter("@STRCOMMENTS", obj.STRCOMMENTS, DbType.String));
            cpList.Add(new CustomParameter("@STRCOMPANYID", obj.STRCOMPANYID, DbType.String));
            cpList.Add(new CustomParameter("@STRIUSERID", obj.STRIUSERID, DbType.String));
            cpList.Add(new CustomParameter("@STREUSERID", obj.STREUSERID, DbType.String));
            cpList.Add(new CustomParameter("@STRMODE", strMode, DbType.String));
           

            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPAPPROVALCOMMENTSSAVE");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
              
        }

        public static List<OOAApprovalComments> Get(OOAApprovalComments obj)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@RECORDID", obj.RECORDID, DbType.Int64));
                cpList.Add(new CustomParameter("@INTOUTOFOFFICEID", obj.INTOUTOFOFFICEID, DbType.Int64));
                cpList.Add(new CustomParameter("@STRAPPROVERID", obj.STRAPPROVERID, DbType.String));
                cpList.Add(new CustomParameter("@STRCOMPANYID", obj.STRCOMPANYID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_USPAPPROVALCOMMENTSGET");

                List<OOAApprovalComments> results = new List<OOAApprovalComments>();
                foreach (DataRow dr in dt.Rows)
                {

                    OOAApprovalComments objData = new OOAApprovalComments();

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
