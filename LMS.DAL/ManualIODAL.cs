using System;
using System.Collections.Generic;
using System.Data;
using LMSEntity;
using TVL.DB;
using System.Globalization;

namespace LMS.DAL
{
    public class ManualIODAL
    {
        public static List<ManualIO> GetItemList(

            Int32 intRowID,
            string strEmpID,
            string strEmpName,
             string strAttendDateFrom,
            string strAttendDateTo,
            Int32 intShiftID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intRowID", intRowID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));

                string AttendDateFrom = "";
                string AttendDateTo = "";

                try
                {
                    if (strAttendDateFrom != "" && strAttendDateFrom != null )
                        AttendDateFrom = getFormatedDate(strAttendDateFrom).ToString("yyyy-MM-dd");
                }
                catch { }
                try
                {
                    if (strAttendDateTo != "" && strAttendDateTo != null)
                        AttendDateTo = getFormatedDate(strAttendDateTo).ToString("yyyy-MM-dd");
                }
                catch { }

                cpList.Add(new CustomParameter("@strAttendDateFrom", AttendDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strAttendDateTo", AttendDateTo, DbType.String));
                cpList.Add(new CustomParameter("@intShiftID", intShiftID, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendanceRawDataGet");

                List<ManualIO> results = new List<ManualIO>();
                foreach (DataRow dr in dt.Rows)
                {

                    ManualIO obj = new ManualIO();

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




        public static DateTime getFormatedDate(String strDate)
        {
            IFormatProvider culture = null;

            DateTime dt = new DateTime(1754, 1, 1); ;

            if (strDate != "" && strDate != null)
            {
                try
                {
                    culture = new CultureInfo("fr-FR", true);
                    dt = DateTime.Parse(strDate, culture, DateTimeStyles.NoCurrentDateDefault);
                }
                catch (Exception exp)
                {
                }
            }
            return dt;
        }


        public static int SaveItem(ManualIO obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intRowID", obj.intRowID, DbType.Int32));

            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intCardID", obj.intCardID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCardID", obj.strCardID, DbType.String));

            DateTime dt = obj.dtAttendDate.Add(obj.dtAttenTime.TimeOfDay);

            cpList.Add(new CustomParameter("@dtAttendDateTime", dt, DbType.DateTime));
            cpList.Add(new CustomParameter("@intPort", obj.intPort, DbType.Int32));
            cpList.Add(new CustomParameter("@strDeviceID", obj.strDeviceID, DbType.String));
            //cpList.Add(new CustomParameter("@strEntryType", obj.strEntryType, DbType.String));
            cpList.Add(new CustomParameter("@strEntryType", "M", DbType.String));
            cpList.Add(new CustomParameter("@RandomValue", obj.RandomValue, DbType.Int32));
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@strReason", obj.strReason, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendanceRawDataSave");
            }
            catch (Exception ex)
            {
                return -5000;
            }

        }

        public static int SaveItemForMobile(MobileIO obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intRowID", obj.intRowID, DbType.Int32));

            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intCardID", obj.intCardID, DbType.Int32));
            cpList.Add(new CustomParameter("@strCardID", obj.strCardID, DbType.String));

            DateTime dt =obj.dtAttenTime;

            cpList.Add(new CustomParameter("@dtAttendDateTime", dt, DbType.DateTime));
            cpList.Add(new CustomParameter("@intPort", obj.intPort, DbType.Int32));
            cpList.Add(new CustomParameter("@strDeviceID", obj.strDeviceID, DbType.String));
            //cpList.Add(new CustomParameter("@strEntryType", obj.strEntryType, DbType.String));
            cpList.Add(new CustomParameter("@strEntryType", "M", DbType.String));
            cpList.Add(new CustomParameter("@RandomValue", obj.RandomValue, DbType.Int32));
            cpList.Add(new CustomParameter("@intShiftID", obj.intShiftID, DbType.Int32));
            cpList.Add(new CustomParameter("@strReason", obj.strReason, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));

             cpList.Add(new CustomParameter("@bitFromMobile", obj.bitFromMobile, DbType.Boolean));
             cpList.Add(new CustomParameter("@strLocation", obj.strLocation, DbType.String));
             cpList.Add(new CustomParameter("@Longitude", obj.Longitude, DbType.String));
             cpList.Add(new CustomParameter("@Latitude", obj.Latitude, DbType.String));
                 
            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendanceRawDataSaveForMobile");
            }
            catch (Exception ex)
            {
                return -5000;
            }

        }


    }
}
