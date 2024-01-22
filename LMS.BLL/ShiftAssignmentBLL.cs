using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DAL;
using LMSEntity;
namespace LMS.BLL
{
    public class ShiftAssignmentBLL
    {
        public int Add(ShiftAssignment objShiftAssignment)
        {            

            return ShiftAssignmentDAL.Save(objShiftAssignment, "I");
        }


        public int Edit(ShiftAssignment objShiftAssignment)
        {
            return ShiftAssignmentDAL.Save(objShiftAssignment, "U");

        }

        public int Delete(int Id)
        {
            ShiftAssignment obj = new ShiftAssignment();
            obj.intShiftAssignmentID = Id;

            return ShiftAssignmentDAL.Save(obj, "D");
        }

        public ShiftAssignment GetByID(int Id)
        {
            return ShiftAssignmentDAL.Get(Id, "",0,"").Single();
        }

        public List<ShiftAssignment> GetAll()
        {
            return ShiftAssignmentDAL.Get(-1, "", 0, "");
        }

        public List<ShiftAssignment> Get(int intShiftAssignmentID, string strEmpID, int intShiftID, string dtEffectiveDate)
        {
            return ShiftAssignmentDAL.Get(intShiftAssignmentID, strEmpID, intShiftID, dtEffectiveDate);
        }
        
    }
}
