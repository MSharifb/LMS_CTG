using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.BLL;
using LMS.DAL;
using LMSEntity;
using LMS.Web.ViewModels.Shared;

namespace LMS.Web.Models
{
    public class DataSynchronizationModel
    {
        private DataSynchronizer objSynchronizer;
        public DataSynchronizer Synchronizer
        {
            set { this.objSynchronizer = value; }
            get { return this.objSynchronizer; }
        }

        public string Message { set; get; }

        public DataSynchronizationModel()
        {
            DataSynchronizer obj = new DataSynchronizer();
            obj.bitEmployee = true;
            obj.bitCompany = true;
            obj.bitDepartment = true;
            obj.bitDesignation = true;
            obj.bitLocation = true;
            obj.bitReligion = true;
            obj.bitEmployeeCategory = true;

            this.Synchronizer = obj;
        }

        public int InitializeData()
        {
            DataSynchronizerBAL objBAL = new DataSynchronizerBAL();
            return objBAL.IntializeData(this.Synchronizer);
        }

        public int SynchronizeData()
        {
            DataSynchronizerBAL objBAL = new DataSynchronizerBAL();
            return objBAL.SynchronizeData(this.Synchronizer);
        }

        public bool IsInitialized
        {
            get
            {
                EmployeeBLL bll = new EmployeeBLL();
                if (bll.EmployeeGet("") != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }


}