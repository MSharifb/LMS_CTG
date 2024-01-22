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
using LMS.DAL;

namespace LMS.Web.Models
{
    public class OutOfOfficeModels
    {

        IList<OutOfOfficeLocaton> lstOutOfOfficeLocation;
        IList<OutOfOffice> lstOutOfOffice;
        OutOfOffice _OutOfOffice;
        private IPagedList<OutOfOffice> _LstOutOfOfficePaging;
        private IPagination<OutOfOffice> lstOutOfOffice1;
        private List<OOAApprovalComments> lstOOAApprovalComments;
        List<OOALocationWiseComments> lstOOALocationComments;
        List<string> authorTypeList;

              
        SelectList _lstEmployee;
        private bool _hasFlow;

        string fromDate;
        string toDate;
        public string _Message;
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        private bool isNew;
        string strEmpID;
        string employeeName;
        string _purpose;
        bool editPermission;
        int authorTypeID;
        int pathID;
        string approverComment;
        string advanceDate;



        public string AdvanceDate
        {
            get { return advanceDate; }
            set { advanceDate = value; }
        }



        public string ApproverComment
        {
            get { return approverComment; }
            set { approverComment = value; }
        }

        public int PathID
        {
            get { return pathID; }
            set { pathID = value; }
        }

        public bool EditPermission
        {
            get { return editPermission; }
            set { editPermission = value; }
        }


        public int AuthorTypeID
        {
            get { return authorTypeID; }
            set { authorTypeID = value; }
        }


        public bool HasFlow
        {
            get { return _hasFlow; }
            set { _hasFlow = value; }
        }

        public string FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        public string ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        public List<OOALocationWiseComments> LstOOALocationComments
        {
            get { return lstOOALocationComments; }
            set { lstOOALocationComments = value; }
        }

        public List<OOAApprovalComments> LstOOAApprovalComments
        {
            get { return lstOOAApprovalComments; }
            set { lstOOAApprovalComments = value; }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        public string StrEmpID
        {
            get { return strEmpID; }
            set { strEmpID = value; }
        }

        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
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

        public SelectList LstEmployee
        {
            get
            {
                int p = 0;
                List<SelectListItem> lst = new List<SelectListItem>();
                List<Employee> empList = EmployeeDAL.GetItemList("","", "", "", "", "", "", "", "", "", "", "strEmpID", "asc", 1, 1, out p);

                foreach (Employee item in empList)
                {
                    lst.Add(new SelectListItem { Text = item.strEmpName, Value = item.strEmpID });
                }
                return new SelectList(lst, "Value", "Text");
            }
            set { _lstEmployee = value; }
        }

        public IPagedList<OutOfOffice> LstOutOfOfficePaging
        {
            get { return _LstOutOfOfficePaging; }
            set { _LstOutOfOfficePaging = value; }
        }

        public IPagination<OutOfOffice> LstOutOfOffice1
        {
            get { return lstOutOfOffice1; }
            set { lstOutOfOffice1 = value; }
        }

        public IList<OutOfOfficeLocaton> LstOutOfOfficeLocation
        {
            get { return lstOutOfOfficeLocation; }
            set { lstOutOfOfficeLocation = value; }
        }

        public OutOfOffice OutOfOffice
        {
            get
            {
                if (this._OutOfOffice == null)
                {
                    this._OutOfOffice = new OutOfOffice();
                }
                return _OutOfOffice;
            }
            set { _OutOfOffice = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public List<string> AuthorTypeList
        {
            get { return authorTypeList; }
            set { authorTypeList = value; }
        }

        public IList<OutOfOffice> LstOutOfOffice
        {
            get
            {
                if (lstOutOfOffice == null)
                    lstOutOfOffice = new List<OutOfOffice>();

                return lstOutOfOffice;
            }
            set { lstOutOfOffice = value; }
        }


        public Employee GetEmployeeInfo(string Id)
        {
            EmployeeBLL objEmpBLL = new EmployeeBLL();

            try
            {
                return objEmpBLL.EmployeeGet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string strDesignation;
        public string StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }

        private string _strDepartment;
        public string StrDepartment
        {
            get { return _strDepartment; }
            set { _strDepartment = value; }
        }

        public static int Save(OutOfOfficeModels obj, string company, string userID)
        {
            int i = -1;
            int TypeID = 0;

            i = OutOfOfficeBLL.Save(obj.OutOfOffice, obj.LstOutOfOfficeLocation, LMS.Web.LoginInfo.Current.strEmpID, obj.AuthorTypeID,company, userID, "I");
            
            OutOfOfficeModels model = new OutOfOfficeModels();


            /*---[if Out of Office added successfully--*/
            if (i > 0 && obj.OutOfOffice.STATUS == "GO" && (obj.OutOfOffice.PURPOSE == "Official" || obj.OutOfOffice.PURPOSE == "Meeting" || obj.OutOfOffice.PURPOSE == "Other"))
            {
                TypeID = model.RecommendAUthorTypeGet(i);
                Common.SendEmailOutofOffice(obj.OutOfOffice, i, TypeID,false);
            }
            return i;
        }

        public static int Update(OutOfOfficeModels obj, string company, string userID)
        {
            int i = -1;

            i = OutOfOfficeBLL.Save(obj.OutOfOffice, obj.LstOutOfOfficeLocation, LMS.Web.LoginInfo.Current.strEmpID,obj.AuthorTypeID, company, userID, "U");
           
            return i;
        }

        public static int Delete(OutOfOffice obj)
        {
            return OutOfOfficeBLL.Save(obj, null, "", 0, "", "", "D");
        }

       
        public IList<OutOfOffice> GetData(OutOfOffice searchObj, DateTime fromDate, DateTime toDate, int startRowIndex, int maximumRows)
        {
            int P;
            LstOutOfOffice = OutOfOfficeBLL.GetData(searchObj, fromDate, toDate, startRowIndex, maximumRows, out P);
            numTotalRows = P;
            return LstOutOfOffice;
        }

        public OutOfOfficeModels GetDetailsData(OutOfOffice searchObj, DateTime fromDate, DateTime toDate, int startIndex, int rowNumber)
        {
            OutOfOfficeModels model = new OutOfOfficeModels();

            try
            {
                int P;
                List<OutOfOffice> lst = new List<OutOfOffice>();
                lst = OutOfOfficeBLL.GetData(searchObj, fromDate, toDate, startIndex, rowNumber, out P);
                OutOfOfficeLocaton Obj = new OutOfOfficeLocaton();
                Obj.OUTOFOFFICEID = lst[0].ID;
                model.LstOutOfOfficeLocation = GetOutOfOfficeLocation(Obj);
                model.LstOOALocationComments = GetOOALocationComments(searchObj);
                model.LstOutOfOffice = lst;
                model.OutOfOffice = lst[0];
                numTotalRows = P;
                if(model.LstOOALocationComments.Count>1)
                   model.AuthorTypeID = 3;
                model.ProcessAuthor();
            }
            catch (Exception EX)
            {

            }
            return model;
        }


        public OutOfOfficeModels GetReportDetailsData(OutOfOffice searchObj, string fromDate, string toDate, int startIndex, int rowNumber)
        {
            OutOfOfficeModels model = new OutOfOfficeModels();

            try
            {
                int P;
                List<OutOfOffice> lst = new List<OutOfOffice>();
                lst = OutOfOfficeBLL.GetReport(searchObj, fromDate, toDate, startIndex, rowNumber, out P);
                OutOfOfficeLocaton Obj = new OutOfOfficeLocaton();
                Obj.OUTOFOFFICEID = lst[0].ID;
                model.LstOutOfOfficeLocation = GetOutOfOfficeLocation(Obj);
                model.LstOutOfOffice = lst;
                model.OutOfOffice = lst[0];
                numTotalRows = P;
            }
            catch (Exception EX)
            {

            }
            return model;
        }

        public IList<OutOfOffice> GetReportData(OutOfOffice searchObj, string fromDate, string toDate, int startRowIndex, int maximumRows)
        {
            int p;
            LstOutOfOffice = OutOfOfficeBLL.GetReport(searchObj, fromDate, toDate, startRowIndex, maximumRows, out p);
            numTotalRows = p;
            return LstOutOfOffice;
        }

        private static IList<OutOfOfficeLocaton> GetOutOfOfficeLocation(OutOfOfficeLocaton searchObj)
        {
            IList<OutOfOfficeLocaton> lst = new List<OutOfOfficeLocaton>();
            lst = OutOfOfficeBLL.GetOutOfOfficeLocation(searchObj, DateTime.MinValue, DateTime.MaxValue, 1, 1);
            return lst;
        }

        private List<OOALocationWiseComments> GetOOALocationComments(OutOfOffice obj)
        {
            OOALocationWiseComments searchObj = new OOALocationWiseComments();
            searchObj.OUTOFOFFICEID = obj.ID;

            return OOALocationWiseCommentsBLL.Get(searchObj);
        }

        public IList<OutOfOffice> getDataForPermissionNVerify(string strAuthorID, string strEmpID, string fromDate, int StartIndex, int RowNum)
        {
            int P;
            LstOutOfOffice = OutOfOfficeBLL.GetListForPermissionNVerification(strAuthorID, strEmpID, fromDate, StartIndex, RowNum, out P);
            numTotalRows = P;
            return LstOutOfOffice;
        }

        public IList<LeaveApplication> GetOutOfOfficePaging(OutOfOffice objSearch)
        {
            return null;
        }
        public List<SelectListItem> GetTimeData
        {
            get { return GetTime(); }
        }

        public List<SelectListItem> GetTransportMode
        {
            get { return GetTransport(); }
        }

        private List<SelectListItem> GetTransport()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem { Text = "Office vehicle", Value = "Office vehicle" });
            lst.Add(new SelectListItem { Text = "Air", Value = "Air" });
            lst.Add(new SelectListItem { Text = "Bus", Value = "Bus" });
            lst.Add(new SelectListItem { Text = "Baby Taxi", Value = "Baby Taxi" });
            lst.Add(new SelectListItem { Text = "Black Cab", Value = "Black Cab" });
            lst.Add(new SelectListItem { Text = "Yellow Cab", Value = "Yellow Cab" });
            lst.Add(new SelectListItem { Text = "Riksha", Value = "Riksha" });


            return lst;
        }

        public List<SelectListItem> GetPurpose
        {
            get
            {
                List<SelectListItem> lst = new List<SelectListItem>();

                lst.Add(new SelectListItem { Text = "Official", Value = "Official" });
                lst.Add(new SelectListItem { Text = "Meeting", Value = "Meeting" });
                lst.Add(new SelectListItem { Text = "Lunch", Value = "Lunch" });
                lst.Add(new SelectListItem { Text = "Personal", Value = "Personal" });
                lst.Add(new SelectListItem { Text = "Sick", Value = "Sick" });
                lst.Add(new SelectListItem { Text = "Other", Value = "Other" });

                return lst;
            }
        }


        private List<SelectListItem> GetTime()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 1; i < 25; i++)
            {
                string AMPM = "AM";
                int t = i;

                if (i > 12)
                {
                    AMPM = "PM";
                    t = i - 12;
                }
                for (int j = 0; j < 60; )
                {
                    string time = "";

                    time = (t).ToString().PadLeft(2, '0') + ":" + j.ToString().PadLeft(2, '0') + "  " + AMPM;

                    items.Add(new SelectListItem
                    {
                        Text = time,
                        Value = time
                    });

                    j = j + 15;
                }

            }


            return items;
        }


        public int GetAuthorTypeID(string strAuthorID, string strEMPID)
        {
            return OOAAuthorTypeBLL.GetAuthorTypeID(strAuthorID, strEMPID);
        }

        public void GetAuthorEmployeeWise(string strAuthorID, string strEMPID)
        {
            try
            {
                List<OOAApprovalPathDetails> lst = OOAAuthorTypeBLL.GetAuthorEmployeeWise(strAuthorID, strEMPID);
                if (lst != null)
                {
                    if (lst.Count < 1) return;
                    EditPermission = lst[0].isEdit;
                    AuthorTypeID = lst[0].intAuthorTypeID;
                    PathID = lst[0].intPathID;

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void GetApprovalComments(OutOfOfficeModels model)
        {
            OOAApprovalComments searchObj = new OOAApprovalComments();
            searchObj.RECORDID = -1;
            searchObj.INTOUTOFOFFICEID = model.OutOfOffice.ID;
            searchObj.INTFLOWPATHID = PathID;
            searchObj.STRAPPROVERID = "";
            searchObj.INTAPPROVERTYPEID = AuthorTypeID;
            searchObj.STRCOMPANYID = LMS.Web.LoginInfo.Current.strCompanyID;
            model.LstOOAApprovalComments = OOAApprovalCommentsBLL.Get(searchObj);

        }

        public int RecommendAUthorTypeGet(int OutOfOfficeID)
        {
            OOAApprovalFlowBLL objBll = new OOAApprovalFlowBLL();
            return objBll.RecommendAUthorTypeGet(OutOfOfficeID);
        }


        public string GetLabelStatus(int status, string outOfOfficeID)
        {
            string statusLabel = "";
            if (status == 0)
                statusLabel = "Permission Pending";

            else if (status == 1)
                statusLabel = "Verification Pending";

            else if (status == 2)
            {
                if (RecommendAUthorTypeGet(int.Parse(outOfOfficeID)) == 2)
                    statusLabel = "Recommend Pending";
                else
                    statusLabel = "Approval Pending";
            }


            else if (status == 3)
                statusLabel = "Approved";

            if (status == 4)
                statusLabel = "Pending for Approval";

            if (status == 5)
                statusLabel = "Reverify";

            return statusLabel;
        }

        public int SaveApproverComment(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsBLL.Save(obj);
        }

        public int UpdateApproverComment(OOAApprovalComments obj)
        {
            return OOAApprovalCommentsBLL.Update(obj);
        }

        public  void ProcessAuthor()
        {
            List<string> lst = new List<string>();
            AuthorTypeList = new List<string>();
            

            if (LstOOALocationComments.Count(l => l.INTAUTHORTYPEID == 1) > 0)
                if (1 <= AuthorTypeID)
                    authorTypeList.Add("Comment of Verifier");

            if (LstOOALocationComments.Count(l => l.INTAUTHORTYPEID == 2) > 0)
                if (2 <= AuthorTypeID)
                    authorTypeList.Add("Comment of Recommender");

            if (LstOOALocationComments.Count(l => l.INTAUTHORTYPEID == 3) > 0)
                if (3<= AuthorTypeID)
                    authorTypeList.Add("Comment of Approver");

        }

        public List<SearchLocation> GetSearchLocaton(string searchObj)
        {
            return OutOfOfficeBLL.GetSearchLocation(searchObj);
        }

        public  bool isReverify(string authorID, string empID, int intFlowType)
        {
            return OOAApprovalProcessBLL.isReverify(authorID, empID, intFlowType);
        }
    }
}