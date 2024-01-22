using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;

namespace LMS.BLL
{
    public class EmployeeWiseApprovalPathBLL
    {
        public int Add(EmployeeWiseApprovalPath objEmployeeWiseApprovalPath)
        {
            return EmployeeWiseApprovalPathDAL.SaveItem(objEmployeeWiseApprovalPath, "I");
        }
        public int Edit(EmployeeWiseApprovalPath objEmployeeWiseApprovalPath)
        {
            return EmployeeWiseApprovalPathDAL.SaveItem(objEmployeeWiseApprovalPath, "U");
        }
        public int Delete(int Id)
        {
            EmployeeWiseApprovalPath obj = new EmployeeWiseApprovalPath();
            obj.intEmpPathID = Id;

            return EmployeeWiseApprovalPathDAL.SaveItem(obj, "D");
        }

        public EmployeeWiseApprovalPath EmployeeWiseApprovalPathGet(int Id)
        {
            int total=0;
            return EmployeeWiseApprovalPathDAL.GetItemList(Id, 0, "", "", "", "", -1,"","","","","", "", "", 1, 20, out total).Single();
        }
        
        
        public List<EmployeeWiseApprovalPath> EmployeeWiseApprovalPathGet(int Id, int intPathID, string strCompanyId, string strEmpID,
                                                                          string strEmpName, int intNodeID, string strDepartmentID, 
                                                                          string strDesignationID, string strLocationID, string strSortBy, 
                                                                          string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {            
            return EmployeeWiseApprovalPathDAL.GetItemList(Id, intPathID,"", strCompanyId, strEmpID, strEmpName, intNodeID,strDepartmentID,
                                                           strDesignationID, strLocationID,"","",strSortBy, strSortType, startRowIndex, 
                                                           maximumRows, out numTotalRows);           
        }



        public List<EmployeeWiseApprovalPath> EmployeeWiseApprovalPathGetAll()
        {
            int total=0;
            return EmployeeWiseApprovalPathDAL.GetItemList(0, 0, "ALL", "", "", "", -1,"","","","","", "", "", 1, 20, out total);
        }

        public List<EmployeeWiseApprovalPath> EmployeeWiseApprovalPathGetAll(string strCompanyId, string strEmpInitial, string strEmpName, 
                                                                             int intPathID, int intNodeID, string strDepartmentID,
                                                                             string strDesignationID, string strLocationID, string strAuthorInitial,
                                                                             string strAuthorName, string strSortBy, string strSortType, 
                                                                             int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return EmployeeWiseApprovalPathDAL.GetItemList(0, intPathID, "ALL", strCompanyId, strEmpInitial, strEmpName, intNodeID, strDepartmentID,
                                                           strDesignationID, strLocationID, strAuthorInitial, strAuthorName, 
                                                           strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);
        }


    }
}
