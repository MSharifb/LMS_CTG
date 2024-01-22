using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
using System.Data.SqlClient;
namespace LMS.DAL
{
    public class LeaveTypeDAL
    {
        public static List<LeaveTypeDeduct> GetDeductedLeaveItemList(int intLeaveTypeID, int intLeaveTypeDeductId)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveTypeDeductGet");

                List<LeaveTypeDeduct> results = new List<LeaveTypeDeduct>();
                foreach (DataRow dr in dt.Rows)
                {

                    LeaveTypeDeduct obj = new LeaveTypeDeduct();

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

        public static List<LeaveType> GetItemList(int intLeaveTypeID, string strLeaveType, string strLeaveShortName, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strLeaveType", strLeaveType, DbType.String));
                cpList.Add(new CustomParameter("@strLeaveShortName", strLeaveShortName, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveTypeGet");

                List<LeaveType> results = new List<LeaveType>();
                foreach (DataRow dr in dt.Rows)
                {

                    LeaveType obj = new LeaveType();

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

        public static int SaveItem(LeaveType obj, string strMode)
        {
           var conn = new SqlConnection(Utility.strDBConnectionString);
           SqlCommand cmd = new SqlCommand();
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "LMS_uspLeaveTypeSave";
           cmd.Parameters.AddWithValue("@intLeaveTypeID", obj.intLeaveTypeID);
           cmd.Parameters.AddWithValue("@strLeaveType", obj.strLeaveType);
           cmd.Parameters.AddWithValue("@strLeaveShortName", obj.strLeaveShortName);
           cmd.Parameters.AddWithValue("@intPriority", obj.intPriority);
           cmd.Parameters.AddWithValue("@bitIsEarnLeave", obj.bitIsEarnLeave);
           cmd.Parameters.AddWithValue("@bitIsEncashable", obj.bitIsEncashable);
           cmd.Parameters.AddWithValue("@strEntitlementType", obj.strEntitlementType);
           cmd.Parameters.AddWithValue("@intEarnLeaveUnitForDays", obj.intEarnLeaveUnitForDays);
           cmd.Parameters.AddWithValue("@strEarnLeaveCalculationType", obj.strEarnLeaveCalculationType);
           cmd.Parameters.AddWithValue("@intEarnLeaveType", obj.intEarnLeaveType);

           cmd.Parameters.AddWithValue("@strCompanyID", obj.strCompanyID);
           cmd.Parameters.AddWithValue("@strIUser", obj.strIUser);
           cmd.Parameters.AddWithValue("@strEUser", obj.strEUser);
           cmd.Parameters.AddWithValue("@strMode", strMode);
           cmd.Parameters.AddWithValue("@isServiceLifeType", obj.isServiceLifeType);
           if (obj.intLeaveYearTypeId == 0)
             cmd.Parameters.AddWithValue("@intLeaveYearTypeId", DBNull.Value);
            else
               cmd.Parameters.AddWithValue("@intLeaveYearTypeId", obj.intLeaveYearTypeId);

           cmd.Parameters.AddWithValue("@intApprovalGroupId", obj.intApprovalGroupId);
           // Added For BEPZA
           cmd.Parameters.AddWithValue("@bitIsRecreationLeave", obj.bitIsRecreationLeave);
           cmd.Parameters.AddWithValue("@bitIsDeductLeaveCalculate", obj.bitIsDeductLeaveCalculate);
           cmd.Parameters.Add("@numErrorCode", SqlDbType.Int,4).Direction = ParameterDirection.Output;
           cmd.Parameters.Add("@strErrorMsg", SqlDbType.VarChar,200).Direction = ParameterDirection.Output; 
           cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int,4).Direction = ParameterDirection.Output;
           cmd.Connection = conn;

            //CustomParameterList cpList = new CustomParameterList();
            //cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            //cpList.Add(new CustomParameter("@strLeaveType", obj.strLeaveType, DbType.String));
            //cpList.Add(new CustomParameter("@strLeaveShortName", obj.strLeaveShortName, DbType.String));
            //cpList.Add(new CustomParameter("@intPriority", obj.intPriority, DbType.Int32));
            //cpList.Add(new CustomParameter("@bitIsEarnLeave", obj.bitIsEarnLeave, DbType.Boolean));
            //cpList.Add(new CustomParameter("@bitIsEncashable", obj.bitIsEncashable, DbType.Boolean));
            //cpList.Add(new CustomParameter("@strEntitlementType", obj.strEntitlementType, DbType.String));
            //cpList.Add(new CustomParameter("@intEarnLeaveUnitForDays", obj.intEarnLeaveUnitForDays, DbType.Int32));
            //cpList.Add(new CustomParameter("@strEarnLeaveCalculationType", obj.strEarnLeaveCalculationType, DbType.String));
            //cpList.Add(new CustomParameter("@intEarnLeaveType", obj.intEarnLeaveType, DbType.Int32)); // Added For MPA
            //cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            //cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            //cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            //cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            ////cpList.Add(new CustomParameter("@strIsServiceLifeType", obj.strIsServiceLifeType, DbType.String));
            //cpList.Add(new CustomParameter("@isServiceLifeType", obj.isServiceLifeType, DbType.Boolean));
            //cpList.Add(new CustomParameter("@intLeaveYearTypeId", obj.intLeaveYearTypeId, DbType.Int32));
            //cpList.Add(new CustomParameter("@intApprovalGroupId", obj.intApprovalGroupId, DbType.Int32));
            //// Added For BEPZA
            //cpList.Add(new CustomParameter("@bitIsRecreationLeave", obj.bitIsRecreationLeave, DbType.Boolean));
            //cpList.Add(new CustomParameter("@bitIsDeductLeaveCalculate", obj.bitIsDeductLeaveCalculate, DbType.Boolean));
            //cpList.Add(new CustomParameter("@RETURN_VALUE", 0, DbType.Int32,ParameterDirection.Output ));
            // End

           try
           {
               conn.Open();
               cmd.ExecuteNonQuery();
               return Convert.ToInt32(cmd.Parameters["@RETURN_VALUE"].Value.ToString());
               //object paramval = null;
               //DBHelper db = new DBHelper();
               //db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveTypeSave");
               // return db.intLastInsertId;
           }
           catch (Exception ex)
           {
               conn.Close();
               conn.Dispose();
               throw ex;
               return -5000;
           }
           finally
           {
               conn.Close();
               conn.Dispose();
           }

        }

        public static int SaveLeaveTypeDeductItem(LeaveTypeDeduct objLeaveTypeDeduct, string strMode)
        {
            var conn = new SqlConnection(Utility.strDBConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LMS_uspLeaveTypeDeductSave";
            cmd.Parameters.AddWithValue("@intLeaveTypeDeductID", objLeaveTypeDeduct.intLeaveTypeDeductID);
            cmd.Parameters.AddWithValue("@intLeaveTypeID", objLeaveTypeDeduct.intLeaveTypeID);
            cmd.Parameters.AddWithValue("@intDeductLeaveTypeID", objLeaveTypeDeduct.intDeductLeaveTypeID);

            cmd.Parameters.AddWithValue("@strIUser", objLeaveTypeDeduct.strIUser);
            cmd.Parameters.AddWithValue("@strEUser", objLeaveTypeDeduct.strEUser);
            cmd.Parameters.AddWithValue("@strMode", strMode);
            cmd.Parameters.Add("@numErrorCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@strErrorMsg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@RETURN_VALUE"].Value.ToString());
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw ex;
                return -5000;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
   
        }


    }
}
