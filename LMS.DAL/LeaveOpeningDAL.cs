using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;


namespace LMS.DAL
{
    public class LeaveOpeningDAL
    {
        public static List<LeaveOpening> GetItemList(string empId, int intLeaveTypeID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", empId, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveOpeningGet");

                List<LeaveOpening> results = new List<LeaveOpening>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveOpening obj = new LeaveOpening();

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

        public static bool IsExistItemList(string empId, int intLeaveTypeID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", empId, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveOpeningGetCount");

                bool results = false;
                if (dt.Rows.Count > 0)
                {
                    int val = 0;
                    Int32.TryParse(dt.Rows[0][0].ToString(), out val);
                    results = val > 0;
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int SaveItem(LeaveOpening obj, string strMode, IDbTransaction transacrtion, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtBalanceDate", obj.dtBalanceDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@fltOB", obj.fltOB, DbType.Double));
            cpList.Add(new CustomParameter("@fltAvailed", obj.fltAvailed, DbType.Double));
            cpList.Add(new CustomParameter("@fltAvailedWOP", obj.fltAvailedWOP, DbType.Double));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transacrtion, con, "LMS_uspLeaveOpeningSave");

            }
            catch (Exception ex)
            {

                throw ex;

                return -5000;
            }
        }

        public static void DeleteItem(LeaveOpening obj)
        {
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);
            try
            {
                LeaveOpeningDAL.SaveItem(obj, "D", transection, con);
            }
            catch (Exception ex)
            {
                transection.Rollback();
                throw ex;
            }
            transection.Commit();
        }

        public static void SaveOpeningList(List<LeaveOpening> objList, string empId, string strcompanyId)
        {


            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            LeaveOpening obj = new LeaveOpening();
            obj.strEmpID = empId;
            obj.strCompanyID = strcompanyId;
            int i = LeaveOpeningDAL.SaveItem(obj, "D", transection, con);

            foreach (LeaveOpening Lop in objList)
            {
                LeaveOpening objLOP = new LeaveOpening();

                //if (Lop.fltOB > 0 || Lop.fltAvailed > 0)
                if (Lop.fltOB + Lop.fltAvailed + Lop.fltAvailedWOP !=0)
                {
                    objLOP.strEmpID = Lop.strEmpID;
                    objLOP.dtBalanceDate = Lop.dtBalanceDate;
                    objLOP.strCompanyID = Lop.strCompanyID;
                    objLOP.intLeaveYearID = Lop.intLeaveYearID;
 
                    objLOP.intLeaveTypeID = Lop.intLeaveTypeID;
                    objLOP.fltOB = Lop.fltOB;
                    objLOP.fltAvailed = Lop.fltAvailed;
                    objLOP.fltAvailedWOP = Lop.fltAvailedWOP;

                    objLOP.strIUser = Lop.strIUser;
                    objLOP.strEUser = Lop.strEUser;

                    CustomParameterList cpList = new CustomParameterList();

                    cpList.Add(new CustomParameter("@strEmpID", objLOP.strEmpID, DbType.String));
                    cpList.Add(new CustomParameter("@intLeaveTypeID", objLOP.intLeaveTypeID, DbType.Int32));
                    cpList.Add(new CustomParameter("@dtBalanceDate", objLOP.dtBalanceDate, DbType.DateTime));
                    cpList.Add(new CustomParameter("@intLeaveYearID", objLOP.intLeaveYearID, DbType.Int32));
                    cpList.Add(new CustomParameter("@fltOB", objLOP.fltOB, DbType.Double));
                    cpList.Add(new CustomParameter("@fltAvailed", objLOP.fltAvailed, DbType.Double));
                    cpList.Add(new CustomParameter("@fltAvailedWOP", objLOP.fltAvailedWOP, DbType.Double));
                    cpList.Add(new CustomParameter("@strCompanyID", objLOP.strCompanyID, DbType.String));
                    cpList.Add(new CustomParameter("@strIUser", objLOP.strIUser, DbType.String));
                    cpList.Add(new CustomParameter("@strEUser", objLOP.strEUser, DbType.String));
                    cpList.Add(new CustomParameter("@strMode", "I", DbType.String));

                    try
                    {
                        DBHelper db = new DBHelper();
                        db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspLeaveOpeningSave");

                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        throw ex;
                    }
                }
            }

            transection.Commit();
        }

    }
}
