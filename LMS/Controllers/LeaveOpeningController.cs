using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using System.Web.UI.WebControls;
using LMS.Web.Helpers;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class LeaveOpeningController : Controller
    {

        //GET: /LeaveOpening/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /LeaveOpening/LeaveOpeningDetails
        [HttpGet]
        [NoCache]
        public ActionResult LeaveOpeningDetails()
        {
            LeaveOpeningModels model = GetModelWithData();
            return PartialView(LMS.Util.PartialViewName.LeaveOpeningDetails, model);
        }

        
        // GET: /LeaveOpening/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(FormCollection fc)
        {
            var model = new LeaveOpeningModels();

            string Id = fc.Get("strEmpID");
            string name = fc.Get("strEmpName");
            try
            {
                model.GetLeaveOpening(Id, model);
                if (!string.IsNullOrEmpty(model.LeaveOpening.strEmpName))
                {
                    model.IsExists = true;
                }
                else
                {
                    model.IsExists = false;
                    model.LeaveOpening.strEmpID = Id;
                    if (Id == model.LeaveOpening.Employee.strEmpID)
                    {
                        model.LeaveOpening.strEmpName = model.LeaveOpening.Employee.strEmpName;
                        model.LeaveOpening.strEmpInitial = model.LeaveOpening.Employee.strEmpInitial;
                    }
                    else
                    {
                        model.IsValidId = false;
                    }
                   // model.LeaveOpening.strEmpName = name;
                }
                model.LeaveOpening.strEmpInitial = model.LeaveOpening.Employee.strEmpInitial;
                if (model.LeaveOpening.Employee.dtJoiningDate != DateTime.MinValue)
                {
                    model.strJoiningDate = model.LeaveOpening.Employee.dtJoiningDate.ToString(LMS.Util.DateTimeFormat.Date);
                    
                }

                if (model.LeaveOpening.Employee.dtConfirmationDate != DateTime.MinValue)
                {
                    model.strConfirmationDate = model.LeaveOpening.Employee.dtConfirmationDate.ToString(LMS.Util.DateTimeFormat.Date);
                }

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveOpeningDetails, model);

        }


        public FileUploadJsonResult UploadLeaveOpening(HttpPostedFileBase file, FormCollection fc)
        {
            string strmsg = "";
            LeaveOpeningBLL objBll = new LeaveOpeningBLL();
            FileUploadJsonResult result = new FileUploadJsonResult();

            if (file != null)
            {

                if (file.FileName.ToString() != "" && (Path.GetExtension(file.FileName.ToString()).ToLower() == ".xls" || Path.GetExtension(file.FileName.ToString()).ToLower() == ".xlsx"))
                {

                    string stropeningdate = fc["txtOpeningDate"].ToString();

                    //Check validation
                    if (objBll.CheckImportValidation(stropeningdate, LoginInfo.Current.strCompanyID, out strmsg) == true)
                    {
                        //Create File
                        string filePath = Server.MapPath("..\\Content\\LeaveOpening\\") + Path.GetFileName(file.FileName);
                        FileUploadJsonResult.CreateFile(filePath, FileUploadJsonResult.GetByteArrayFromStream(file.InputStream));

                        //Import data from created file   
                        try
                        {
                            objBll.ImportExcelData(filePath, LoginInfo.Current.strCompanyID, stropeningdate, LoginInfo.Current.LoginName, out strmsg);
                        }
                        catch (Exception ex)
                        {

                        }
                        if (strmsg.ToString().Length > 0)
                        {
                            return result = new FileUploadJsonResult { Data = new { message = strmsg, flag = false } };
                        }

                        result = new FileUploadJsonResult { Data = new { message = string.Format("{0} file imported successfully.", System.IO.Path.GetFileName(file.FileName)), flag = true } };

                    }
                    else
                    {
                        return result = new FileUploadJsonResult { Data = new { message = strmsg, flag = false } };
                    }

                }
                else
                {
                    return result = new FileUploadJsonResult { Data = new { message = "Please select an excel file.", flag = false } };
                }

            }
            else
            {
                return result = new FileUploadJsonResult { Data = new { message = "Please select an excel file.", flag = false } };
            }

            return result;
        }

        
        //POST: /LeaveOpening/SaveLeaveOpening
        [HttpPost]
        [NoCache]
        public ActionResult SaveLeaveOpening(LeaveOpeningModels model)
        {
            string strmsg = "";
            try
            {
                //SaveData
                model.SaveData(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    if (model.IsExists == false)
                    {
                        model = GetModelWithData();
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    }
                    else
                    {
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                    }
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.LeaveOpeningDetails, model);

        }

        
        //POST: /LeaveOpening/DeleteData
        [HttpPost]
        [NoCache]
        public ActionResult DeleteData(LeaveOpeningModels model, FormCollection fc)
        {
            string strmsg = "";
            try
            {
                model.Delete(model.LeaveOpening.strEmpID, model.LeaveOpening.intLeaveYearID, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    model = GetModelWithData();
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.LeaveOpeningDetails, model);
        }

        
        //GET: /LeaveOpening/GetLeaveOpeningAll   
        [NoCache]
        public JsonResult GetLeaveOpeningAll(LeaveOpeningModels model)
        {
            bool IsFound = false;
            try
            {
                IsFound = model.IsExistItemList();
            }
            catch (Exception ex)
            {

            }
            return Json(IsFound);
        }


        
        [NoCache]
        private LeaveOpeningModels GetModelWithData()
        {
            LeaveOpeningModels model = new LeaveOpeningModels();

            model.LeaveOpening.dtBalanceDate = DateTime.Today;
            model.LeaveOpening.LstLeaveOpening = model.GetLeaveType();

            if (model.LeaveOpening.LstLeaveOpening.Count() > 0)
            {
                model.LeaveOpening.intLeaveYearID = model.LeaveOpening.LstLeaveOpening[0].intLeaveYearID;
                model.LeaveOpening.strYearTitle = model.LeaveOpening.LstLeaveOpening[0].strYearTitle;
            }

            return model;
        }
    
    
    
    }
}
