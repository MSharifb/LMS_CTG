using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class EmployeeWiseOOAApprovalPath : EntityBase
    {
        private List<EmployeeWiseOOAApprovalPath> _LstEmployeeOOAApprovalPath;
        public List<EmployeeWiseOOAApprovalPath> LstEmployeeOOAApprovalPath
        {
            get
            {
                if (_LstEmployeeOOAApprovalPath == null)
                {
                    _LstEmployeeOOAApprovalPath = new List<EmployeeWiseOOAApprovalPath>();
                }
                return _LstEmployeeOOAApprovalPath;
            }
            set { _LstEmployeeOOAApprovalPath = value; }
        }

        [DataMember, DataColumn(true)]
        public string StrFlowType { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intEmpPathID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intFlowType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intPathID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intNodeID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strCompanyID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strIUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strEUser { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtIDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtEDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplyType { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strLocationID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intParentNodeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strPathName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strNodeName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }


        [DataMember, DataColumn(true)]
        public System.String strAuthorID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strAuthorName { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intIsSelect { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLocation { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsIndividual
        {
            get
            {
                if (this.strApplyType == "Individual")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                    this.strApplyType = "Individual";
                else
                    this.strApplyType = "All";
            }
        }

        public EmployeeWiseOOAApprovalPath() { }
    }
}
