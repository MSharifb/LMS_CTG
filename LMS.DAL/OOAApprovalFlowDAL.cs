using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class OOAApprovalFlowDAL
    {
        public static List<OOAApprovalFlow> GetItemList(int intAppFlowID,long intApplicationID,int intNodeID, string strAuthorID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intAppFlowID", intAppFlowID, DbType.Int32));
                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.UInt64));
                cpList.Add(new CustomParameter("@intNodeID", intNodeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAApprovalFlowGet");

                List<OOAApprovalFlow> results = new List<OOAApprovalFlow>();
                foreach (DataRow dr in dt.Rows)
                {
                    OOAApprovalFlow obj = new OOAApprovalFlow();

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
      

        public static int SaveItem(OOAApprovalFlow obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intApplicationID", obj.intApplicationID, DbType.Int32));
            cpList.Add(new CustomParameter("@intSourceNodeID", obj.intSourceNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intDestNodeID", obj.intDestNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strSourceAuthorID", obj.strSourceAuthorID, DbType.String));
            cpList.Add(new CustomParameter("@strDestAuthorID", obj.strDestAuthorID, DbType.String));
            cpList.Add(new CustomParameter("@intAppStatusID", obj.intAppStatusID, DbType.Int32));            
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));            
            cpList.Add(new CustomParameter("@strComments", obj.strComments, DbType.String));
            cpList.Add(new CustomParameter("@strUser", obj.strIUser, DbType.String));       
            cpList.Add(new CustomParameter("@strApplicationType", obj.strApplicationType, DbType.String));
            cpList.Add(new CustomParameter("@dtApplyFromDate", obj.dtApplyFromDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtApplyToDate", obj.dtApplyToDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strApplyFromTime", obj.strApplyFromTime, DbType.String));
            cpList.Add(new CustomParameter("@strApplyToTime", obj.strApplyToTime, DbType.String));
            cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithPayDuration", obj.fltWithPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithoutPayDuration", obj.fltWithoutPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null,"LMS_uspOOAApprovalFlow");
            }
            catch (Exception ex)
            {
                throw ex;
                return -5000;
            }
        }

        public static int AlternateOOAApproval(string strAlternateSupervisorID, OOAApprovalFlow obj, out string strmsg)
        {
            strmsg = "";
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intApplicationID", obj.intApplicationID, DbType.Int32));            
            cpList.Add(new CustomParameter("@intSourceNodeID", obj.intSourceNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@intDestNodeID", obj.intDestNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strSourceAuthorID", obj.strSourceAuthorID, DbType.String));
            cpList.Add(new CustomParameter("@strDestAuthorID", obj.strDestAuthorID, DbType.String));            
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@intAppStatusID", obj.intAppStatusID, DbType.Int32));
            cpList.Add(new CustomParameter("@strComments", obj.strComments, DbType.String));
            cpList.Add(new CustomParameter("@strApplicationType", obj.strApplicationType, DbType.String));
            cpList.Add(new CustomParameter("@dtApplyFromDate", obj.dtApplyFromDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtApplyToDate", obj.dtApplyToDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strApplyFromTime", obj.strApplyFromTime, DbType.String));
            cpList.Add(new CustomParameter("@strApplyToTime", obj.strApplyToTime, DbType.String));
            cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithPayDuration", obj.fltWithPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithoutPayDuration", obj.fltWithoutPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strUser", obj.strEUser, DbType.String));
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspOOAAlternateApproval");
            }
            catch (Exception ex)
            {
                strmsg = ex.Message;
                return -5000;
            }
        }


        public static int RecommendAUthorTypeGet(int outOfOfficeID)
        {
           
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@OUTOFOFFICEID", outOfOfficeID, DbType.Int32));            
           
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "OOA_GETAUTHORTYPEID_RECOMMEND");
                return int.Parse(dt.Rows[0][0].ToString());                
            }
            catch (Exception ex)
            {                
                return -5000;
            }
        }


        
    
    }
}
