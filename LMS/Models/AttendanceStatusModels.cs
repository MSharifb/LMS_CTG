using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.BLL;
using LMS.DAL;
using LMSEntity;


namespace LMS.Web.Models
{
    public class AttendanceStatusModels
    {

        public ATT_tblAtdStatusSetup AtdStatusSetup {get; set;}
     
        ATT_AtdStatusSetupBLL objBLL = new ATT_AtdStatusSetupBLL();
   
        public string Message  {get ;   set ; }
        
        public int SaveData(AttendanceStatusModels model)
        {
            int returnid = 0;

            try
            {
                model.AtdStatusSetup.strEUser = LoginInfo.Current.LoginName;
                if (model.AtdStatusSetup.intRowID > 0)
                {
                    returnid = objBLL.Edit(model.AtdStatusSetup);
                }
                else
                {
                    model.AtdStatusSetup.strIUser = LoginInfo.Current.LoginName;                    
                    returnid = objBLL.Add(model.AtdStatusSetup);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnid;

        }


        public int Delete(int Id)
        {

            int returnid = 0;

            try
            {

                returnid = objBLL.Delete(Id);
            }

            catch (Exception ex)
            {

                throw ex;
            }

            return returnid;
        }


        public ATT_tblAtdStatusSetup GetAttendanceStatus(int Id)
        {
            AttendanceStatusModels model = new AttendanceStatusModels();

            try
            {

                return model.AtdStatusSetup  = objBLL.AteendanceStatusGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}