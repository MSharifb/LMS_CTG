using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class CommonConfigBLL
    {
        public int Add(CommonConfig objConfig, out string strmsg)
        {
            return CommonConfigDAL.SaveItem(objConfig, "I", out strmsg);
        }

        public void Edit(List<CommonConfig> objConfigList, out string strmsg)
        {
            strmsg = "";
            try
            {
                CommonConfigDAL.SaveItemList(objConfigList, "U", out strmsg);
            }
            catch (Exception ex)
            {
                
            }
        }

        public int Delete(int Id, out string strmsg)
        {
            CommonConfig obj = new CommonConfig();
            obj.ConfigID = Id;
            return CommonConfigDAL.SaveItem(obj, "D", out strmsg);
        }

        public CommonConfig CommonConfigGet(string configKey)
        {
            return CommonConfigDAL.GetItemList(configKey).Single();
        }
        
        public List<CommonConfig> CommonConfigGetAll()
        {
            return CommonConfigDAL.GetItemList("");
        }

    }
}
