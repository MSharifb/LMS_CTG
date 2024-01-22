using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using LMS.DAL;
using LMS.BLL;
using LMSEntity;
using System.Globalization;
using System.Data.OleDb;
using System.Data;

namespace LMSEntity
{
    public class LeaveOpeningBLL
    {
        private List<LeaveOpening> _LeaveOpeningList;
        public List<LeaveOpening> LeaveOpeningList
        {
            get
            {
                if (_LeaveOpeningList == null)
                {
                    _LeaveOpeningList = new List<LeaveOpening>();
                }
                return _LeaveOpeningList;
            }
            set { _LeaveOpeningList = value; }
        }

        private DataTable loadExcelSheet(string strPath)
        {
            string connectionString = @"provider=Microsoft.JET.OLEDB.4.0; data source=" + strPath + ";" + "Extended Properties=Excel 8.0;";
            //string connectionString = @"provider=Microsoft.ACE.OLEDB.12.0; data source=" + strPath + ";" + "Extended Properties=Excel 12.0;";

            string fName = System.IO.Path.GetFileName(strPath);

            OleDbConnection conn = new OleDbConnection(connectionString);
            DataTable myTable = new DataTable();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("Select T.* From [Sheet1$] as T ", conn);
                da.Fill(myTable);
                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
            }
            return myTable;
        }

        private bool isAllowLeaveTypeForImport(string LTShortName, DataTable dt)
        {
            foreach (var item in dt.Columns )
            {
                string fieldName = "CO-" + LTShortName;

                if (item.ToString() == fieldName)
                    return true;
            }

            return false;
        }

        public void ImportExcelData(string strPath, string strcompanyId, string stropeningdate, string strIUser, out string strmsg)
        {
            strmsg = "";
            DataTable myTable = new DataTable();
            LeaveTypeBLL objLTBll = new LeaveTypeBLL();
            EmployeeBLL objBLL = new EmployeeBLL();
            try
            {
                List<LeaveType> objLType = new List<LeaveType>();
                objLType = objLTBll.LeaveTypeGetAll(strcompanyId);

                myTable = this.loadExcelSheet(strPath);  

                if (myTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in myTable.Rows)
                    {
                        List<LeaveOpening> objListOP = new List<LeaveOpening>();
                        //string empId = dr["EmployeeID"].ToString();
                        int numTotalRows1 = 0;
                        string employeeInitial = dr["EmployeeInitial"].ToString();
                        var objEmp = objBLL.EmployeeGet(employeeInitial, "", "", "Active", "", "", "", "", "", "", "AND", "strEmpID", "ASC", 1, 10, out numTotalRows1).LastOrDefault();

                        //RH#2016-01-04
                        if (objEmp == null)
                        {
                            continue;
                        }
                        //End RH

                        foreach (LeaveType LT in objLType)
                        {
                            double numPY = 0,numCY_WP = 0,numCY_WOP = 0;;

                            string strPY_LT = "CO-", strCY_WP_LT = "AVWP-", strCY_WOP_LT = "AVWOP-"; 

                            strPY_LT = strPY_LT.ToString() + LT.strLeaveShortName.ToString().ToUpper();
                            strCY_WP_LT = strCY_WP_LT.ToString() + LT.strLeaveShortName.ToString().ToUpper();   //--Availed(With Pay)
                            strCY_WOP_LT = strCY_WOP_LT.ToString() + LT.strLeaveShortName.ToString().ToUpper();   //--Availed(Without Pay)

                            if (isAllowLeaveTypeForImport(LT.strLeaveShortName.ToString().ToUpper(), myTable) == true)
                            {
                                numPY = Convert.ToDouble(dr[strPY_LT.ToString()]);
                                numCY_WP = Convert.ToDouble(dr[strCY_WP_LT.ToString()]);      //--Availed(With Pay)
                                numCY_WOP = Convert.ToDouble(dr[strCY_WOP_LT.ToString()]);    //--Availed(Without Pay)

                                LeaveOpening objLOP = new LeaveOpening();

                                if (numPY > 0 || numCY_WP > 0 || numCY_WOP > 0)
                                {
                                    objLOP.strEmpID = objEmp.strEmpID;

                                    objLOP.dtBalanceDate = DateTime.ParseExact(stropeningdate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    objLOP.strCompanyID = strcompanyId.ToString();
                                    objLOP.intLeaveTypeID = LT.intLeaveTypeID;
                                    objLOP.strLeaveType = LT.strLeaveType;
                                    objLOP.fltOB = numPY;
                                    objLOP.fltAvailed = numCY_WP;
                                    objLOP.fltAvailedWOP = numCY_WOP;

                                    objLOP.strIUser = strIUser;
                                    objLOP.strEUser = strIUser;

                                    // added obaidul on 24 August 2014
                                    if (LT.intLeaveYearID != 0)
                                    {
                                        objLOP.intLeaveYearID = LT.intLeaveYearID;
                                    }
                                    else
                                    {
                                        objLOP.intLeaveYearID = 0;
                                    }

                                    objListOP.Add(objLOP);
                                }
                            }
                          
                        }

                        LeaveOpeningDAL.SaveOpeningList(objListOP, objEmp != null ? objEmp.strEmpID.ToString() : "", strcompanyId);
                    }

                }
                else
                {
                    strmsg = "Data not found to import.";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("column"))
                {
                    strmsg = ex.Message;//Column 'CO-LFA' does not belong to table. 
                    strmsg = strmsg.Replace(".", "");
                    strmsg = strmsg.Replace("belong", "exist");
                    strmsg = "Data import failed." + Environment.NewLine + strmsg.Replace("table", "the excel file.");
                }

                throw ex;
            }
        }


        public void Add(string empId, string strcompanyId, string stropeningdate, out string strmsg)
        {
            try
            {
                if (CheckValidation(this.LeaveOpeningList, stropeningdate, strcompanyId, out strmsg) == true)
                {
                    LeaveOpeningDAL.SaveOpeningList(this.LeaveOpeningList, empId, strcompanyId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(LeaveOpening objLeaveOpening)
        {
            return LeaveOpeningDAL.SaveItem(objLeaveOpening, "U", null, null);
        }

        public void Delete(string empId, string strCompanyID, int intLeaveyearId, out string strMSG)
        {
            strMSG = "";
            LeaveOpening obj = new LeaveOpening();
            obj.strEmpID = empId;
            obj.strCompanyID = strCompanyID;

            LeaveYearBLL objLYBLL = new LeaveYearBLL();
            List<LeaveYear> objLYList = new List<LeaveYear>();

            objLYList = objLYBLL.LeaveYearGetAll(0,strCompanyID);
            LeaveYear objLY = objLYList.Where(c => c.bitIsActiveYear == true).SingleOrDefault();
            if (objLY == null)
            {
                strMSG = "Please assign leave active year.";
            }
            else
            {
                if (intLeaveyearId != objLY.intLeaveYearID)
                {
                    strMSG = "Delete allow only for active leave year.";
                }
                else
                {
                    LeaveOpeningDAL.DeleteItem(obj);
                }
            }

        }

        public LeaveOpening LeaveOpeningGet(string empId, string strCompanyId)
        {
            return LeaveOpeningDAL.GetItemList(empId, 0, strCompanyId).FirstOrDefault();
        }


        public List<LeaveOpening> LeaveOpeningGet(string empId, int intLeaveTypeId, string strCompanyId)
        {
            return LeaveOpeningDAL.GetItemList(empId, intLeaveTypeId, strCompanyId);
        }

        public List<LeaveOpening> LeaveOpeningGetAll()
        {
            return LeaveOpeningDAL.GetItemList("", 0, "");
        }

        public List<LeaveOpening> LeaveOpeningGetAll(string strCompanyId)
        {
            return LeaveOpeningDAL.GetItemList("", 0, strCompanyId);
        }

        public bool IsExistItemList(string strCompanyId)
        {
            return LeaveOpeningDAL.IsExistItemList("", 0, strCompanyId);
        }

        private bool CheckValidation(List<LeaveOpening> objLeaveOpening, string stropeningdate, string strcompanyId, out string strMSG)
        {
            bool isvalid = true;
            strMSG = "";

            if (stropeningdate != null)
            {
                LeaveYearBLL objLYBLL = new LeaveYearBLL();
                List<LeaveYear> objLYList = new List<LeaveYear>();
                DateTime dtOpeningDate = DateTime.Parse(stropeningdate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                objLYList = objLYBLL.LeaveYearGetAll(0,strcompanyId);
                DateTime dtMinStartDate = DateTime.Parse(objLYList.Min().dtStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                LeaveYear objMinYear = objLYList.Where(c => (c.dtStartDate == dtMinStartDate)).SingleOrDefault();
                //LeaveYear objLY = objLYList.Where(c => (c.intLeaveYearID == objMinYear.intLeaveYearID) && (c.dtStartDate <= dtOpeningDate) && (c.dtEndDate >= dtOpeningDate)).SingleOrDefault();

                LeaveYear objLY = objLYList.Where(c => c.bitIsActiveYear == true && c.dtStartDate <= dtOpeningDate && c.dtEndDate > dtOpeningDate).FirstOrDefault();
                if (objLY == null)
                {
                    strMSG = "Opening date must be within the leave year " + objMinYear.strYearTitle.ToString();
                    isvalid = false;
                    return isvalid;
                }
                else
                {

                    OfficeTimeBLL objOTBLL = new OfficeTimeBLL();
                    OfficeTime objOT = objOTBLL.OfficeTimeGet(strcompanyId, objLY.intLeaveYearID);

                    if (objOT == null)
                    {
                        strMSG = "Please enter office time for the year " + objLY.strYearTitle.ToString();
                        isvalid = false;
                        return isvalid;
                    }
                }

            }

            if (objLeaveOpening != null)
            {
                double dblTotal = 0;
                //dblTotal = (from p in objLeaveOpening select p.fltOB + p.fltAvailed).Sum();
                dblTotal = (from p in objLeaveOpening select p.fltOB + p.fltAvailed + p.fltAvailedWOP).Sum();
                if (dblTotal == 0)
                {
                    //strMSG = "Carry Over or Availed must be greater than zero.";
                    strMSG = "Opening balance cannot be zero.";
                    isvalid = false;
                    return isvalid;
                }
                else
                {
                    dblTotal = (from p in objLeaveOpening select p.fltAvailed + p.fltAvailedWOP).Sum();
                  if (dblTotal < 0)
                  {
                      strMSG = "Availed(WP) or Availed(WOP) must be greater than zero.";
                      isvalid = false;
                      return isvalid;
                  }
                }
            }

            else
            {
                strMSG = "Please enter leave type at first.";
                isvalid = false;
                return isvalid;
            }

            return isvalid;

        }

        public bool CheckImportValidation(string stropeningdate, string strcompanyId, out string strMSG)
        {
            bool isvalid = true;

            strMSG = "";
            LeaveTypeBLL objLTBll = new LeaveTypeBLL();
            List<LeaveType> objLType = new List<LeaveType>();


            objLType = objLTBll.LeaveTypeGetAll(strcompanyId);

            if (objLType == null || objLType.Count <= 0)
            {
                strMSG = "Please enter leave type at first.";
                isvalid = false;
                return isvalid;
            }

            if (stropeningdate != null)
            {
                LeaveYearBLL objLYBLL = new LeaveYearBLL();
                List<LeaveYear> objLYList = new List<LeaveYear>();
                DateTime dtOpeningDate = DateTime.Parse(stropeningdate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                objLYList = objLYBLL.LeaveYearGetAll(0,strcompanyId);                

                //LeaveYear objLYAct = objLYList.Where(c => (c.bitIsActiveYear == true) && (c.dtStartDate <= dtOpeningDate) && (c.dtEndDate >= dtOpeningDate)).SingleOrDefault();

                LeaveYear objLYAct = (from tr in objLYList
                                      where (tr.bitIsActiveYear == true) && (tr.dtStartDate <= dtOpeningDate) && (tr.dtEndDate >= dtOpeningDate)
                                      select tr).SingleOrDefault();

                DateTime dtMinStartDate = DateTime.Parse(objLYAct.dtStartDate.ToString(), new CultureInfo("fr-Fr", true), DateTimeStyles.None);

                if (objLYAct == null)
                {
                    strMSG = "Opening date must be within active leave year.";
                    isvalid = false;
                    return isvalid;
                }

                //LeaveYear objMinYear = objLYList.Where(c => (c.dtStartDate == dtMinStartDate)).SingleOrDefault();

                LeaveYear objMinYear = (from tr in objLYList
                                        where  (tr.bitIsActiveYear == true) && (tr.dtStartDate >= dtMinStartDate)
                                        select tr).SingleOrDefault();

                //LeaveYear objLY = objLYList.Where(c => (c.intLeaveYearID == objMinYear.intLeaveYearID) && (c.dtStartDate <= dtOpeningDate) && (c.dtEndDate >= dtOpeningDate)).SingleOrDefault();

                LeaveYear objLY = (from tr in objLYList
                                   where (tr.intLeaveYearID == objMinYear.intLeaveYearID) && (tr.dtStartDate <= dtOpeningDate) && (tr.dtEndDate >= dtOpeningDate)
                                   select tr).SingleOrDefault();

                if (objLY == null)
                {
                    strMSG = "Opening date must be within the leave year " + objMinYear.strYearTitle.ToString();
                    isvalid = false;
                    return isvalid;
                }
                else
                {
                    OfficeTimeBLL objOTBLL = new OfficeTimeBLL();
                    OfficeTime objOT = objOTBLL.OfficeTimeGet(strcompanyId, objLY.intLeaveYearID);

                    if (objOT == null)
                    {
                        strMSG = "Please enter office time for the year " + objLY.strYearTitle.ToString();
                        isvalid = false;
                        return isvalid;
                    }
                }

            }
          

            return isvalid;

        }
    }
}
