using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.BLL;
using LMSEntity;
using System.Data;
using MvcPaging;
using System.Web.Mvc;
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class MiscellaneousReportModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _message;
        string strToDate;
        string strFromDate;
        string strDepartment;
        string strEmpID;
        string strEmpName;
        private SelectList _Department;
        private MiscellaneousReport miscellaneousReportObj;
        private List<MiscellaneousReport> lstMiscellaneousReport;
        private IPagedList<MiscellaneousReport> _lstMiscellaneousReportPaging;



        public string StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }

        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }

        public string StrDepartment
        {
            get { return strDepartment; }
            set { strDepartment = value; }
        }

        public string StrFromDate
        {
            get { return strFromDate; }
            set { strFromDate = value; }
        }

        public string StrToDate
        {
            get { return strToDate; }
            set { strToDate = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public MiscellaneousReport MiscellaneousReportObj
        {
            get { return miscellaneousReportObj; }
            set { miscellaneousReportObj = value; }
        }

        public IPagedList<MiscellaneousReport> LstMiscellaneousReportPaging
        {
            get { return _lstMiscellaneousReportPaging; }
            set { _lstMiscellaneousReportPaging = value; }
        }

        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }

        public int maximumRows
        {
            get { return _maximumRows; }
            set { _maximumRows = value; }
        }

        public int numTotalRows
        {
            get { return _numTotalRows; }
            set { _numTotalRows = value; }
        }

        public List<MiscellaneousReport> LstMiscellaneousReport
        {
            get { return lstMiscellaneousReport; }
            set { lstMiscellaneousReport = value; }
        }


        public SelectList Department
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();

                List<Department> lstDepartment = new List<Department>();

                lstDepartment = Common.fetchDepartment().Where(c => c.strCompanyID == LoginInfo.Current.strCompanyID).ToList();

                foreach (Department lt in lstDepartment)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.strDepartmentID;
                    item.Text = lt.strDepartment;
                    itemList.Add(item);
                }
                this._Department = new SelectList(itemList, "Value", "Text");
                return _Department;
            }
            set { _Department = value; }
        }

        public MiscellaneousReportModels GetReportData(MiscellaneousReportModels model)
        {
            int totalRows = 0;
            model.LstMiscellaneousReport = MiscellaneousReportBLL.GetReportData(model.StrEmpID, model.StrDepartment, model.StrFromDate, model.StrToDate, model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            return model;
        }

    }
}