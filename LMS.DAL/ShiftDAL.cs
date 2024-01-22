using System;
using System.Collections.Generic;
using System.Data;
using LMSEntity;
using TVL.DB;
namespace LMS.DAL
{
    public class ShiftDAL
    {
        public static List<Shift> GetItemList(Int32 intShiftID,string strShiftName, bool bitIsRoaster, string strPeriodFrom,string strPeriodTo)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intShiftID", intShiftID, DbType.String));
                cpList.Add(new CustomParameter("@strShiftName", strShiftName, DbType.String));
               // cpList.Add(new CustomParameter("@bitIsRoaster", bitIsRoaster, DbType.Boolean));
                cpList.Add(new CustomParameter("@strPeriodFrom", strPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@strPeriodTo", strPeriodTo, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspShiftGet");

                List<Shift> results = new List< Shift>();
                foreach (DataRow dr in dt.Rows)
                {

                    Shift obj = new Shift();

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

        public static int SaveItem(Shift obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@strShiftName", obj.strShiftName, DbType.String));
            cpList.Add(new CustomParameter("@strDescription", obj.strDescription, DbType.String));
            cpList.Add(new CustomParameter("@bitIsRoaster", obj.bitIsRoaster, DbType.Boolean));
            cpList.Add(new CustomParameter("@intDuration", obj.intDuration, DbType.Int32));
            cpList.Add(new CustomParameter("@dtpPeriodFrom", obj.dtpPeriodFrom, DbType.String));
            cpList.Add(new CustomParameter("@dtpPeriodTo", obj.dtpPeriodTo, DbType.String));
            cpList.Add(new CustomParameter("@dtpIntime", obj.strIntime, DbType.String));
            cpList.Add(new CustomParameter("@dtpOuttime", obj.strOuttime, DbType.String));
            cpList.Add(new CustomParameter("@intGraceInMin", obj.intGraceInMin, DbType.Int32));
            cpList.Add(new CustomParameter("@intGraceOutTimeMin", obj.intGraceOutTimeMin, DbType.Int32));
            cpList.Add(new CustomParameter("@intAbsentMin", obj.intAbsentMin, DbType.Int32));
            cpList.Add(new CustomParameter("@dtpHalfTime", obj.strHalfTime, DbType.String));
            cpList.Add(new CustomParameter("@dtEffectiveDate", obj.dtEffectiveDate, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspShiftSave");
            }
            catch (Exception ex)
            {
                return -5000;
            }

        }

    }
}
