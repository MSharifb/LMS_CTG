using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
   public class LeaveApplicationValidationDAL
    {

       public static List<ValidationMessage> ValidateLeaveApplication(LeaveApplication obj,string strMode)
       {
           List<ValidationMessage> messageList = new List<ValidationMessage>();
           try
           {
               CustomParameterList cpList = new CustomParameterList();

               cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
               cpList.Add(new CustomParameter("@intApplicationID", obj.intApplicationID, DbType.Int64));
               cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
               cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
               cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
               cpList.Add(new CustomParameter("@dtApplyFromDate", obj.dtApplyFromDate, DbType.DateTime));
               cpList.Add(new CustomParameter("@dtApplyToDate", obj.dtApplyToDate, DbType.DateTime));

               cpList.Add(new CustomParameter("@strApplyFromTime", obj.strApplyFromTime, DbType.String));
               cpList.Add(new CustomParameter("@strApplyToTime", obj.strApplyToTime, DbType.String));

               cpList.Add(new CustomParameter("@strApplicationType", obj.strApplicationType, DbType.String));
               cpList.Add(new CustomParameter("@intAppStatusID", obj.intAppStatusID, DbType.Int32));
               cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Double));
               cpList.Add(new CustomParameter("@fltWithPayDuration", obj.fltWithPayDuration, DbType.Double));
               cpList.Add(new CustomParameter("@fltWithoutPayDuration", obj.fltWithoutPayDuration, DbType.Double));
               
               
               cpList.Add(new CustomParameter("@bitIsApprovalProcess", obj.bitIsApprovalProcess, DbType.Boolean));
               cpList.Add(new CustomParameter("@strSupervisorID", obj.strSupervisorID, DbType.String));
               cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
               cpList.Add(new CustomParameter("@strApplyHalfDayFor", obj.strHalfDayFor, DbType.String));
               
               
              
               object paramval = null;
               DBHelper db = new DBHelper();
               DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspLeaveAppValidation");

               foreach (DataRow dr in dt.Rows)
               {
                   ValidationMessage vm = new ValidationMessage();
                   MapperBase.GetInstance().MapItem(vm, dr);
                   messageList.Add(vm);
               }
                             
           }
           catch (Exception ex)
           {
               throw (ex);
           }

           return messageList;
       }

    }
}
