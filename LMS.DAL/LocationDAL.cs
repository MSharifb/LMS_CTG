﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;
namespace LMS.DAL
{
    public class LocationDAL
    {
        public static List<Location> GetItemList(string strLocationID, string strLocation, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strLocationID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strLocation", strLocation, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLocationGet");

                List<Location> results = new List<Location>();
                foreach (DataRow dr in dt.Rows)
                {
                    Location obj = new Location();

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

        public static int SaveItem(Location obj, string strMode)
        {
            CustomParameterList cpList = new CustomParameterList();
            cpList.Add(new CustomParameter("@strLocationID", obj.strLocationID, DbType.Int32));
            cpList.Add(new CustomParameter("@strLocation", obj.strLocation, DbType.String));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.DateTime));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            try
            {

                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLocationSave");
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

    }
}
