using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class LeaveApplication : EntityBase
    {
        private List<LeaveLedger> _lstLeaveLedger;
        public List<LeaveLedger> LstLeaveLedger
        {
            get { return _lstLeaveLedger; }
            set { _lstLeaveLedger = value; }
        }

        [DataMember, DataColumn(true)]
        public System.Int64 intApplicationID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strApprovedByInitial { get; set; }
         
        [DataMember, DataColumn(true)]
        public System.String strResponsibleInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strPLInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strPLName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveYearID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intLeaveTypeID { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean isServiceLifeType { get; set; }
        
        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyFromDate { get; set; }

        [DataMember, DataColumn(true)]
        public System.DateTime dtApplyToDate { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltDays { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltHour { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltWPDays { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltWPHour { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltWOPDays { get; set; }

        [DataMember, DataColumn(false)]
        public System.Double fltWOPHour { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strApplyDate
        {
            get
            {
                return dtApplyDate.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    dtApplyDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtApplyDate = DateTime.Now;
                }
            }
        }


        [DataMember, DataColumn(false)]
        public System.String strApplyFromDate
        {
            get
            {
                return dtApplyFromDate.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    dtApplyFromDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtApplyFromDate = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(false)]
        public System.String strApplyToDate
        {
            get
            {
                return dtApplyToDate.ToString("dd-MM-yyyy");
            }
            set
            {

                if (value != null)
                {

                    dtApplyToDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtApplyToDate = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strApplyFromTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strApplyToTime { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strApplicationType { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsFullDay { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsHourly { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsFullDayHalfDay { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltWithPayDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltWithoutPayDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strPurpose { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strContactAddress { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strContactNo { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strRemarks { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intAppStatusID { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsApprovalProcess { get; set; }
        [DataMember, DataColumn(false)]
        public System.Int32 intDestNodeID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strSupervisorID { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strSupervisorName { get; set; }
        [DataMember, DataColumn(false)]
        public System.String strSupervisorInitial { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsDiscard { get; set; }
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

        [DataMember, DataColumn(false)]
        public System.DateTime dtApplyDateFrom { get; set; }
        [DataMember, DataColumn(false)]
        public System.DateTime dtApplyDateTo { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strEmpName { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartment { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strLeaveType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strStatus { get; set; }

        [DataMember, DataColumn(false)]
        public bool IsForApproval { set; get; }

        [DataMember, DataColumn(false)]
        public System.String strApprovalProcess { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int32 intAppFlowID { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strAuthorID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strOfflineApprovedById { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strOfflineApproverInitial { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltBeforeBalance { get; set; }

        [DataMember, DataColumn(true)]
        public System.Double fltNetBalance { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean IsChecked { get; set; }
        
        [DataMember, DataColumn(true)]
        public System.String strResponsibleId { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strPLID { get; set; } 

        [DataMember, DataColumn(true)]
        public System.String strResponsibleName { get; set; }

        [DataMember, DataColumn(true)]
        public System.Boolean bitIsAdjustment { get; set; }

        [DataMember, DataColumn(true)]
        public System.Int64 intRefApplicationID { get; set; }

        [DataMember, DataColumn(false)]
        public System.Boolean bitIsForAlternateProcess { get; set; }

        /*updated by mamun on 15 feb 2011*/
        [DataMember, DataColumn(true)]
        public System.DateTime dtSubmittedApplyFromDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtSubmittedApplyToDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strSubmittedApplyFromTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strSubmittedApplyToTime { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltSubmittedDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltSubmittedWithPayDuration { get; set; }
        [DataMember, DataColumn(true)]
        public System.Double fltSubmittedWithoutPayDuration { get; set; }

        [DataMember, DataColumn(false)]
        public System.String strSubmittedApplyFromDate
        {
            get
            {
                return dtSubmittedApplyFromDate.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    dtSubmittedApplyFromDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtSubmittedApplyFromDate = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(false)]
        public System.String strSubmittedApplyToDate
        {
            get
            {
                return dtSubmittedApplyToDate.ToString("dd-MM-yyyy");
            }
            set
            {

                if (value != null)
                {

                    dtSubmittedApplyToDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtSubmittedApplyToDate = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(true)]
        public System.String strSubmittedApplicationType { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDesignationID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public System.String strBranch { get; set; }

        /* end of update*/

       
        
        /* update by shaiful 22 June 2011*/

        [DataMember, DataColumn(true)]
        public System.String strOffLineAppvDesignationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strOffLineAppvDepartmentID { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime dtOffLineAppvDate { get; set; }
        [DataMember, DataColumn(true)]
        public System.Boolean bitIsOffLine { get; set; }               
        [DataMember, DataColumn(true)]
        public System.Int32 intDurationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strHalfDayFor { get; set; }
        [DataMember, DataColumn(true)]
        public System.Int32 intSubmittedDurationID { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strSubmittedHalfDayFor { get; set; } 
        [DataMember, DataColumn(true)]
        public System.String strSubmitHalfDayDurationFor { get; set; }
        [DataMember, DataColumn(true)]
        public System.String strHalfDayDurationFor { get; set; }
        [DataMember, DataColumn(true)]
        public System.DateTime ApproveDateTime { get; set; }
            
        
        [DataMember, DataColumn(false)]
        public System.String strOffLineAppvDate
        {
            get
            {
                return dtOffLineAppvDate.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    dtOffLineAppvDate = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    dtOffLineAppvDate = DateTime.Now;
                }
            }
        }                    
        
        /* end of update*/
        /// <summary>
        /// Added for MPA
        /// </summary>
        [DataMember, DataColumn(true)]
        public Int32 intCountryID { get; set; }
        /// <summary>
        /// Added for MPA
        /// </summary>
        [DataMember, DataColumn(true)]
        public Int32 intEarnLeaveType { get; set; }
        /// <summary>
        /// Added for MPA
        /// </summary>
        [DataMember, DataColumn(true)]
        public System.String strOfflineApproverName { get; set; }


        /// <summary>
        /// Added for BEPZA
        /// </summary>
        public Int32 ZoneId { get; set; }

        public LeaveApplication() { }

        public LeaveApplication ShallowCopy()
        {
            return (LeaveApplication)base.MemberwiseClone();
        }
    
    
    }
}