using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class HolidayWeekDayRuleDAL
    {
        public static List<HolidayWeekDayRule> GetItemList(int ruleId, int intLeaveYearID, int intHolidayWeekendID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intHolidayRuleID", ruleId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayRuleGet");

                List<HolidayWeekDayRule> results = new List<HolidayWeekDayRule>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekDayRule obj = new HolidayWeekDayRule();

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

        public static List<HolidayWeekDayRuleChild> GetChildItemList(int ruleId, int intLeaveYearID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intHolidayRuleID", ruleId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayRuleDetailsGet");

                List<HolidayWeekDayRuleChild> results = new List<HolidayWeekDayRuleChild>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekDayRuleChild obj = new HolidayWeekDayRuleChild();

                    MapperBase.GetInstance().MapItem(obj, dr);
                    if (obj.IsSelect == 1)
                    {
                        obj.IsChecked = true;
                    }
                    else
                    {
                        obj.IsChecked = false;
                    }
                    results.Add(obj);
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<HolidayWeekDayRuleChild> GetChildItemListByRuleId(int ruleId)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intHolidayRuleID", ruleId, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspGetHolidayWeekDayDetailsByRuleID");

                List<HolidayWeekDayRuleChild> results = new List<HolidayWeekDayRuleChild>();
                foreach (DataRow dr in dt.Rows)
                {
                    HolidayWeekDayRuleChild obj = new HolidayWeekDayRuleChild();
                    MapperBase.GetInstance().MapItem(obj, dr);
                    results.Add(obj);
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int SaveItem(HolidayWeekDayRule obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intHolidayRuleID", obj.intHolidayRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strHolidayRule", obj.strHolidayRule, DbType.String));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspHolidayWeekDayRuleSave");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

        public static int SaveItemWithList(HolidayWeekDayRule obj, string strMode)
        {
            int intruleId = 0;
            int intErrNo = 0;
            DBHelper db = new DBHelper();
            CustomParameterList cpList = new CustomParameterList();

            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

            cpList.Add(new CustomParameter("@intHolidayRuleID", obj.intHolidayRuleID, DbType.Int32));
            cpList.Add(new CustomParameter("@strHolidayRule", obj.strHolidayRule, DbType.String));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                intruleId = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspHolidayWeekDayRuleSave");

                if (intruleId < 0)
                {
                    transection.Rollback();
                    return intruleId;
                }

                foreach (HolidayWeekDayRuleChild WHRDet in obj.HolidayWeekDayRuleChild)
                {
                    if (WHRDet.IsChecked == true)
                    {
                        cpList = new CustomParameterList();

                        if (strMode == "I")
                        {
                            cpList.Add(new CustomParameter("@intHolidayRuleID", intruleId, DbType.Int32));
                        }
                        else
                        {
                            cpList.Add(new CustomParameter("@intHolidayRuleID", obj.intHolidayRuleID, DbType.Int32));
                        }
                        cpList.Add(new CustomParameter("@intHolidayWeekendID", WHRDet.intHolidayWeekendID, DbType.Int32));
                        cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
                        cpList.Add(new CustomParameter("@strIUser", obj.strEUser, DbType.String));
                        cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));

                        try
                        {
                            intErrNo = db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspHolidayWeekDayRuleDetailsSave");

                            if (intErrNo < 0)
                            {
                                transection.Rollback();
                                return intErrNo;
                            }
                        }
                        catch (Exception ex)
                        {
                            transection.Rollback();
                            throw ex;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                transection.Rollback();
                return -5000;
            }
            transection.Commit();
            return 0;
        }

    }
}
