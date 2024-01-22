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
    public class ImportAttendRawDataModels
    {

        //ImportAttendRawDataDAL  objDAL = new ImportAttendRawDataDAL();

        public string Message { get; set; }
        public string strFileLocation { get; set; }
        public string strDBUser { get; set; }
        public string strDBPW { get; set; }
        public Int16 numMinID { get; set; }
        public Int16 numMaxID { get; set; }
        public string strIUser { get; set; }
        public Int16 intRowID { get; set; }


        public int SaveData(ImportAttendRawDataModels model)
        {
            int returnid = 0;

            try
            {
                model.strIUser = LoginInfo.Current.LoginName;

                returnid = ImportAttendRawDataDAL.SaveItem(model.strFileLocation, model.strDBUser, model.strDBPW, model.numMinID,
                    model.numMaxID, model.strIUser);
             
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnid;

        }



    }
}