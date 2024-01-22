using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using System.Data;
using LMSEntity;

namespace LMS.BLL
{
    public class EmployeeWiseOOAApprovalPathBLL
    {
        public int Add(EmployeeWiseOOAApprovalPath objEmployeeWiseOOAApprovalPath)
        {
            return EmployeeWiseOOAApprovalPathDAL.SaveItem(objEmployeeWiseOOAApprovalPath, "I");
        }
        public int Edit(EmployeeWiseOOAApprovalPath objEmployeeWiseOOAApprovalPath)
        {
            return EmployeeWiseOOAApprovalPathDAL.SaveItem(objEmployeeWiseOOAApprovalPath, "U");
        }
        public int Delete(int Id)
        {
            EmployeeWiseOOAApprovalPath obj = new EmployeeWiseOOAApprovalPath();
            obj.intEmpPathID = Id;

            return EmployeeWiseOOAApprovalPathDAL.SaveItem(obj, "D");
        }

        public EmployeeWiseOOAApprovalPath EmployeeWiseOOAApprovalPathGet(int Id)
        {
            int total=0;
            return EmployeeWiseOOAApprovalPathDAL.GetItemList(0,Id, 0, "", "", "", "", -1,"","","","","", "", "", 1, 20, out total).Single();
        }
        
        
        public List<EmployeeWiseOOAApprovalPath> EmployeeWiseOOAApprovalPathGet(int intFlowType,int Id, int intPathID, string strCompanyId, string strEmpID,
                                                                          string strEmpName, int intNodeID, string strDepartmentID, 
                                                                          string strDesignationID, string strLocationID, string strSortBy, 
                                                                          string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return EmployeeWiseOOAApprovalPathDAL.GetItemList(intFlowType, Id, intPathID, "", strCompanyId, strEmpID, strEmpName, intNodeID, strDepartmentID,
                                                           strDesignationID, strLocationID,"","",strSortBy, strSortType, startRowIndex, 
                                                           maximumRows, out numTotalRows);           
        }



        public List<EmployeeWiseOOAApprovalPath> EmployeeWiseOOAApprovalPathGetAll()
        {
            int total=0;
            return EmployeeWiseOOAApprovalPathDAL.GetItemList(0,0, 0, "ALL", "", "", "", -1,"","","","","", "", "", 1, 20, out total);
        }

        public List<EmployeeWiseOOAApprovalPath> EmployeeWiseOOAApprovalPathGetAll(string strCompanyId, string strEmpID, string strEmpName, 
                                                                             int intPathID,int intFlowTypeID, int intNodeID, string strDepartmentID, 
                                                                             string strDesignationID, string strLocationID,string strAuthorID,
                                                                             string strAuthorName, string strSortBy, string strSortType, 
                                                                             int startRowIndex, int maximumRows, out int numTotalRows)
        {
            return EmployeeWiseOOAApprovalPathDAL.GetItemList(intFlowTypeID, 0, intPathID, "ALL", strCompanyId, strEmpID, strEmpName, intNodeID, strDepartmentID, 
                                                           strDesignationID, strLocationID, strAuthorID, strAuthorName, 
                                                           strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);
        }


        public DataTable GetFlowTypeWise(int FlowType)
        {
            return EmployeeWiseOOAApprovalPathDAL.GetFlowTypewise(FlowType);
        }

    }
}
