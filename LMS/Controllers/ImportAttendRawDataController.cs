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
using LMS.BLL;

namespace LMS.Web.Controllers
{
    public class ImportAttendRawDataController : Controller
    {
        //
        // GET: /ImportAttendRawData/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /ImportAttendRawData/ImportAttendRawData
        [HttpGet]
        [NoCache]
        public ActionResult ImportAttendRawData(int? page)
        {
            ImportAttendRawDataModels model = new ImportAttendRawDataModels();
            try
            {
                
            }
            catch (Exception ex) { }
                      
            return PartialView("ImportAttendRawDataDetails", model);

            //return View(model);
        }

        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImportAttendRawData(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
            }
            return View();
        }
        */


        public FileUploadJsonResult ImportAttendRawData(HttpPostedFileBase file, FormCollection fc)
        {
            string strmsg = "";
            
            ImportAttendRawDataBLL objBll = new ImportAttendRawDataBLL();

            FileUploadJsonResult result = new FileUploadJsonResult();

            if (file != null)
            {

                if (file.FileName.ToString() != "" && (Path.GetExtension(file.FileName.ToString()).ToLower() == ".mdb" ))
                {

                    string strUser = fc["txtUser"].ToString();
                    string strPassword = fc["txtPass"].ToString();

                    //Check validation
                    if (true)
                    {
                        //Create File

                        //string filePath = Server.MapPath("..\\Content\\ImportAttendRawData\\") + Path.GetFileName(file.FileName);
                        //FileUploadJsonResult.CreateFile(filePath, FileUploadJsonResult.GetByteArrayFromStream(file.InputStream));

                        string filePath = file.FileName;

                        //Import data from created file   
                        try
                        {
                            objBll.Add(filePath, strUser, strPassword, LoginInfo.Current.LoginName);  
                            
                            //objBll.ImportExcelData(filePath, LoginInfo.Current.strCompanyID, stropeningdate, LoginInfo.Current.LoginName, out strmsg);
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


        //GET: /LeaveOpening/GetLeaveOpeningAll   
        [NoCache]
        public JsonResult GetImportAttendValidation(ImportAttendRawDataModels model)
        {
            bool IsFound = false;
            try
            {
                IsFound = true;// model.IsExistItemList();
            }
            catch (Exception ex)
            {

            }
            return Json(IsFound);
        }



   /*
        //POST: /ImportAttendRawData/SaveImportAttendRawData
        [HttpPost]
        [NoCache]
        public ActionResult ImportAttendRawData(ImportAttendRawDataModels model)
        {
            string strmsg = "";
            try
            {
                //SaveData


                foreach (string inputTagName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[inputTagName];
                    if (file.ContentLength > 0)
                    {
                     
                    }
                }

                
                model.strFileLocation = @"E:MIST_NBR\MIST_NBR_SHARE\ACCESS.mdb";

                model.SaveData(model);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    //model = GetModelWithData();
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView("ImportAttendRawDataDetails", model);
        }

        */

    }
}
