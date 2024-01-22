using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HolidayWeekDayDAL
    {
        public static List<HolidayWeekDay> GetItemList(int Id, int intLeaveYearID, string strType, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intHolidayWeekendMasterID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strType", strType, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayMasterGet");

                List<HolidayWeekDay> results = new List<HolidayWeekDay>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekDay obj = new HolidayWeekDay();

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

        //public static List<HolidayWeekDay> GetItemList(int Id, int intLeaveYearID, string strType, string strCompanyID)
        //{

        //    try
        //    {
        //        CustomParameterList cpList = new CustomParameterList();

        //        cpList.Add(new CustomParameter("@intHolidayWeekendID", Id, DbType.Int32));
        //        cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
        //        cpList.Add(new CustomParameter("@strType", strType, DbType.String));
        //        cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

        //        object paramval = null;
        //        DBHelper db = new DBHelper();
        //        DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayGet");

        //        List<HolidayWeekDay> results = new List<HolidayWeekDay>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            HolidayWeekDay obj = new HolidayWeekDay();

        //            MapperBase.GetInstance().MapItem(obj, dr); ;
        //            results.Add(obj);
        //        }
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}


        public static int SaveMasterItem(HolidayWeekDay obj, string strMode, IDbTransaction transaction, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intHolidayWeekendMasterID", obj.intHolidayWeekendMasterID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtDateFrom", obj.dtDateFrom, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtDateTo", obj.dtDateTo, DbType.DateTime));
            //cpList.Add(new CustomParameter("@intDuration", obj.intDuration, DbType.Int32));
            cpList.Add(new CustomParameter("@strType", obj.strType, DbType.String));
            cpList.Add(new CustomParameter("@strHolidayTitle", obj.strHolidayTitle, DbType.String));
            cpList.Add(new CustomParameter("@isAutomatic", obj.isAutomatic, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();

                if (transaction != null)
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "LMS_uspHolidayWeekDayMasterSave");
                }
                else
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayMasterSave");
                }

               // return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayMasterSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

        public static int SaveItem(HolidayWeekDay obj, string strMode, IDbTransaction transaction, IDbConnection con)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intHolidayWeekendID", obj.intHolidayWeekendID, DbType.Int32));
            cpList.Add(new CustomParameter("@intHolidayWeekendMasterID", obj.intHolidayWeekendMasterID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtDateFrom", obj.dtDateFrom, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtDateTo", obj.dtDateTo, DbType.DateTime));
            cpList.Add(new CustomParameter("@intDuration", obj.intDuration, DbType.Int32));
            cpList.Add(new CustomParameter("@strType", obj.strType, DbType.String));
            cpList.Add(new CustomParameter("@strHolidayTitle", obj.strHolidayTitle, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                if (transaction != null)
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transaction, con, "LMS_uspHolidayWeekDaySave");
                }
                else
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDaySave");
                }

                //return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDaySave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }
    }
}
