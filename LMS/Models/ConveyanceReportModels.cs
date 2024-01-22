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
    public class ConveyanceReportModels
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
        private ConveyanceReport conveyanceReportObj;
        private List<ConveyanceReport> lstConveaynceReport;
        private IPagedList<ConveyanceReport> _lstConveaynceReportPaging;



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

        public ConveyanceReport ConveyanceReportObj
        {
            get { return conveyanceReportObj; }
            set { conveyanceReportObj = value; }
        }

        public IPagedList<ConveyanceReport> LstConveaynceReportPaging
        {
            get { return _lstConveaynceReportPaging; }
            set { _lstConveaynceReportPaging = value; }
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

        public List<ConveyanceReport> LstConveaynceReport
        {
            get { return lstConveaynceReport; }
            set { lstConveaynceReport = value; }
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

        public ConveyanceReportModels GetReportData(ConveyanceReportModels model)
        {
            int totalRows=0;
            model.LstConveaynceReport = ConveyanceReportBLL.GetReportData(model.StrEmpID, model.StrDepartment, model.StrFromDate, model.StrToDate, model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            return model;
        }

    }
}