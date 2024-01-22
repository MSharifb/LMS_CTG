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
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class LeaveOpeningModels
    {
        private string _strSortBy;
        private string _strSortType;
        private int _startRowIndex;
        private int _maximumRows;
        private int _numTotalRows;

        private string _Message;
        private string _strJoiningDate;
        private string _strConfirmationDate;
        private bool _IsExists;
        private string _strFilePath;
        bool _bitIsSelect;
        private bool _isValidId=true;

        private LeaveOpening _LeaveOpening;
        private LeaveOpeningBLL objBLL = new LeaveOpeningBLL();
        private EmployeeBLL objEmpBLL = new EmployeeBLL();

        public string strJoiningDate
        {
            get { return _strJoiningDate; }
            set { _strJoiningDate = value; }
        }

        public string strConfirmationDate
        {
            get { return _strConfirmationDate; }
            set { _strConfirmationDate = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public bool IsExists
        {
            get { return _IsExists; }
            set { _IsExists = value; }
        }

        public bool IsValidId
        {
            get { return _isValidId; }
            set { _isValidId = value; }
        }

        public string strFilePath
        {
            get { return _strFilePath; }
            set { _strFilePath = value; }
        }

        public bool bitIsSelect
        {
            get
            {
                if (_bitIsSelect == null)
                {
                    _bitIsSelect = false;
                }
                return _bitIsSelect;
            }
            set { _bitIsSelect = value; }
        }

        public LeaveOpening LeaveOpening
        {
            get
            {
                if (_LeaveOpening == null)
                {
                    _LeaveOpening = new LeaveOpening();
                }
                return _LeaveOpening;
            }
            set { _LeaveOpening = value; }
        }

        public void SaveImportData(LeaveOpeningModels model, out string strmsg)
        {
            LeaveOpeningBLL objBll = new LeaveOpeningBLL();
            try
            {
                objBll.ImportExcelData(model.strFilePath.ToString(), LoginInfo.Current.strCompanyID, model.LeaveOpening.strBalanceDate, LoginInfo.Current.LoginName, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveData(LeaveOpeningModels model, out string strmsg)
        {
            strmsg = "";

            LeaveOpeningBLL objBll = new LeaveOpeningBLL();
            LeaveLedgerBLL objLedBLL = new LeaveLedgerBLL();
            LeaveApplicationBLL objLvAppBLL = new LeaveApplicationBLL();

            try
            {
                if (model.IsExists == true)
                {
                    List<LeaveLedger> objLedg = new List<LeaveLedger>();
                    objLedg = objLedBLL.LeaveLedgerGet(model.LeaveOpening.strEmpID, LoginInfo.Current.strCompanyID).Where(c => c.intLeaveYearID != model.LeaveOpening.intLeaveYearID).ToList();
                    if (objLedg.Count > 0)
                    {
                        strmsg = "Yearly entitlement is availble for the employee.";
                    }
                }

                if (strmsg.ToString().Length == 0)
                {
                    foreach (LeaveOpening Lop in model.LeaveOpening.LstLeaveOpening)
                    {
                        LeaveOpening objLOP = new LeaveOpening();

                        //if (Lop.fltOB > 0 || Lop.fltAvailed > 0)
                        if ((Lop.fltOB + Lop.fltAvailed + Lop.fltAvailedWOP) != 0)
                        {
                            objLOP.strEmpID = model.LeaveOpening.strEmpID;
                            objLOP.dtBalanceDate = model.LeaveOpening.dtBalanceDate;
                            objLOP.strCompanyID = LoginInfo.Current.strCompanyID;

                            objLOP.intLeaveYearID = Lop.intLeaveYearID;
                            objLOP.intLeaveTypeID = Lop.intLeaveTypeID;
                            objLOP.strLeaveType = Lop.strLeaveType;
                            objLOP.fltOB = Lop.fltOB;
                            objLOP.fltAvailed = Lop.fltAvailed;
                            objLOP.fltAvailedWOP = Lop.fltAvailedWOP;
                            objLOP.strIUser = LoginInfo.Current.LoginName;
                            objLOP.strEUser = LoginInfo.Current.LoginName;
                            objBll.LeaveOpeningList.Add(objLOP);
                        }
                    }

                    objBll.Add(model.LeaveOpening.strEmpID.ToString(), LoginInfo.Current.strCompanyID, model.LeaveOpening.strBalanceDate, out strmsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string Id, int intleaveyearId, out string strmsg)
        {
            strmsg = "";
            LeaveOpeningBLL objBll = new LeaveOpeningBLL();
            LeaveLedgerBLL objLedBLL = new LeaveLedgerBLL();

            try
            {
                List<LeaveLedger> objLedg = new List<LeaveLedger>();
                objLedg = objLedBLL.LeaveLedgerGet(Id, LoginInfo.Current.strCompanyID).Where(c => c.intLeaveYearID != intleaveyearId).ToList();
                if (objLedg.Count > 0)
                {
                    strmsg = "Yearly entitlement is availble for the employee.";
                }
                else
                {
                    objBLL.Delete(Id, LoginInfo.Current.strCompanyID, intleaveyearId, out strmsg);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LeaveOpening> GetLeaveType()
        {
            try
            {
                return objBLL.LeaveOpeningGet("", 0, LoginInfo.Current.strCompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetLeaveOpening(string empId, LeaveOpeningModels model)
        {

            try
            {

                model.LeaveOpening.LstLeaveOpening = objBLL.LeaveOpeningGet(empId, 0, LoginInfo.Current.strCompanyID);

                if (!string.IsNullOrEmpty(empId))
                {
                    model.LeaveOpening.Employee = objEmpBLL.EmployeeGet(empId);

                }
                
                if (model.LeaveOpening.LstLeaveOpening != null)
                {
                    model.LeaveOpening.intLeaveYearID = model.LeaveOpening.LstLeaveOpening[0].intLeaveYearID;
                    model.LeaveOpening.strYearTitle = model.LeaveOpening.LstLeaveOpening[0].strYearTitle;
                    model.LeaveOpening.strBalanceDate = model.LeaveOpening.LstLeaveOpening[0].strBalanceDate;

                    foreach (LeaveOpening lop in model.LeaveOpening.LstLeaveOpening)
                    {
                        //if (lop.fltOB + lop.fltAvailed)
                        //if (lop.fltOB + lop.fltAvailed + lop.fltAvailedWOP != 0)
                        //{
                            model.LeaveOpening.intLeaveYearID = lop.intLeaveYearID;
                            model.LeaveOpening.strYearTitle = lop.strYearTitle;
                            model.LeaveOpening.strEmpID = lop.strEmpID;
                            model.LeaveOpening.strEmpName = lop.strEmpName;
                            model.LeaveOpening.strBalanceDate = lop.strBalanceDate;
                        //    break;
                        //}

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetLeaveOpeningAll()
        {
            LeaveOpening.LstLeaveOpening = objBLL.LeaveOpeningGetAll(LoginInfo.Current.strCompanyID.ToString());
        }

        public bool IsExistItemList()
        {
            return objBLL.IsExistItemList(LoginInfo.Current.strCompanyID.ToString());
        }
    }
}
