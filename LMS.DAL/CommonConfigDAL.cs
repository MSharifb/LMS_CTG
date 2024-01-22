using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class CommonConfigDAL
    {
        public static List<CommonConfig> GetItemList(string configKey)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strConfigKey", configKey, DbType.String));                

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCommonConfigGet");

                List<CommonConfig> results = new List<CommonConfig>();
                foreach (DataRow dr in dt.Rows)
                {
                    CommonConfig obj = new CommonConfig();
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

        public static int SaveItem(CommonConfig obj, string strMode, out string strmsg)
        {
            strmsg = "";
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intConfigID", obj.ConfigID, DbType.Int32));
            cpList.Add(new CustomParameter("@strConfigKey", obj.ConfigKey, DbType.String));
            cpList.Add(new CustomParameter("@strConfigValue", obj.ConfigValue, DbType.String));
            cpList.Add(new CustomParameter("@strControlType", obj.strControlType, DbType.String));
            cpList.Add(new CustomParameter("@strDataType", obj.strDataType, DbType.String));
            cpList.Add(new CustomParameter("@bitIsMandatory", obj.bitIsMandatory, DbType.Boolean));
            cpList.Add(new CustomParameter("@intParentID", obj.intParentID, DbType.Int32));
            cpList.Add(new CustomParameter("@strIfParentVal", obj.strIfParentVal, DbType.String));
            cpList.Add(new CustomParameter("@bitIsChildMandatory", obj.bitIsChildMandatory, DbType.Boolean));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCommonConfigSave");
            }
            catch (Exception ex)
            {
                strmsg = ex.Message;
                return -5000;
            }
        }


        public static void SaveItemList(List<CommonConfig> objList, string strMode, out string strmsg)
        {
            strmsg = "";
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transection = DBHelper.GetTransaction(con);

             foreach (CommonConfig obj in objList)
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intConfigID", obj.ConfigID, DbType.Int32));
                cpList.Add(new CustomParameter("@strConfigKey", obj.ConfigKey, DbType.String));
                cpList.Add(new CustomParameter("@strConfigValue", obj.ConfigValue, DbType.String));
                cpList.Add(new CustomParameter("@strControlType", obj.strControlType, DbType.String));
                cpList.Add(new CustomParameter("@strDataType", obj.strDataType, DbType.String));
                cpList.Add(new CustomParameter("@bitIsMandatory", obj.bitIsMandatory, DbType.Boolean));
                cpList.Add(new CustomParameter("@intParentID", obj.intParentID, DbType.Int32));
                cpList.Add(new CustomParameter("@strIfParentVal", obj.strIfParentVal, DbType.String));
                cpList.Add(new CustomParameter("@bitIsChildMandatory", obj.bitIsChildMandatory, DbType.Boolean));
                cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
                cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
                cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

                try
                    {
                        DBHelper db = new DBHelper();
                        db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transection, con, "LMS_uspCommonConfigSave");

                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        strmsg = ex.Message;
                    }
                }
                transection.Commit();
            }
            
    }
}
