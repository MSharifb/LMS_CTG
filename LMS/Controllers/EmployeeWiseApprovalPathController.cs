using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LMSEntity;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;
using System.Collections;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class EmployeeWiseApprovalPathController : Controller
    {

        //GET: /EmployeeWiseApprovalPath/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        //GET: /EmployeeWiseApprovalPath/EmployeeWiseApprovalPath
        [HttpGet]
        [NoCache]
        public ActionResult EmployeeWiseApprovalPath(int? page)
        {

            EmployeeWiseApprovalPathModels model = new EmployeeWiseApprovalPathModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.strSortBy = "strPathName,strNodeName";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.PageNumber = currentPageIndex + 1;


            model.EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
            EmployeeWiseApprovalPath objSearch = model.EmployeeWiseApprovalPath;

            //---[Search by approval author ID and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchAuthorInitial))
            {
                objSearch.strAuthorInitial = model.strSearchAuthorInitial.Trim();
            }
            else
            {
                objSearch.strAuthorInitial = model.strSearchAuthorInitial;
            }

            if (!string.IsNullOrEmpty(model.strSearchAuthorName))
            {
                objSearch.strAuthorName = model.strSearchAuthorName.Trim();
            }
            else
            {
                objSearch.strAuthorName = model.strSearchAuthorName;
            }


            //---[Search by applicant ID and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchInitial))
            {
                objSearch.strEmpInitial = model.strSearchInitial.Trim();
            }
            else
            {
                objSearch.strEmpInitial = model.strSearchInitial;
            }

            if (!string.IsNullOrEmpty(model.strSearchName))
            {
                objSearch.strEmpName = model.strSearchName.Trim();
            }
            else
            {
                objSearch.strEmpName = model.strSearchName;
            }

            objSearch.intPathID = model.intSearchPathID;
            objSearch.intNodeID = model.intSearchNodeID;
            objSearch.strDepartmentID = model.strSearchDepartmentId;
            objSearch.strDesignationID = model.strSearchDesignationId;
            objSearch.strLocationID = model.strSearchLocationId;

            model.GetEmployeeWiseApprovalPathAll(objSearch);
            model.LstEmployeeWiseApprovalPathPaging = model.LstEmployeeWiseApprovalPath.ToPagedList(currentPageIndex, AppConstant.PageSize);

            return View(model);
        }

        //POST: /EmployeeWiseApprovalPath/EmployeeWiseApprovalPath
        [HttpPost]
        [NoCache]
        public ActionResult EmployeeWiseApprovalPath(int? page, EmployeeWiseApprovalPathModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "strPathName,strNodeName";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.PageNumber = currentPageIndex + 1;

            model.EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
            EmployeeWiseApprovalPath objSearch = model.EmployeeWiseApprovalPath;

            //---[Search by approval author Initial and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchAuthorInitial))
            {
                objSearch.strAuthorInitial = model.strSearchAuthorInitial.Trim();
            }
            else
            {
                objSearch.strAuthorInitial = model.strSearchAuthorInitial;
            }

            if (!string.IsNullOrEmpty(model.strSearchAuthorName))
            {
                objSearch.strAuthorName = model.strSearchAuthorName.Trim();
            }
            else
            {
                objSearch.strAuthorName = model.strSearchAuthorName;
            }


            //---[Search by applicant Initial and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchInitial))
            {
                objSearch.strEmpInitial = model.strSearchInitial.Trim();
            }
            else
            {
                objSearch.strEmpInitial = model.strSearchInitial;
            }

            if (!string.IsNullOrEmpty(model.strSearchName))
            {
                objSearch.strEmpName = model.strSearchName.Trim();
            }
            else
            {
                objSearch.strEmpName = model.strSearchName;
            }

            objSearch.intPathID = model.intSearchPathID;
            objSearch.intNodeID = model.intSearchNodeID;
            objSearch.strDepartmentID = model.strSearchDepartmentId;
            objSearch.strDesignationID = model.strSearchDesignationId;
            objSearch.strLocationID = model.strSearchLocationId;
            model.GetEmployeeWiseApprovalPathAll(objSearch);

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPath, model);
        }


        //GET: /EmployeeWiseApprovalPath/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int? pkid, int? pathId)
        {

            EmployeeWiseApprovalPathModels model = new EmployeeWiseApprovalPathModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            try
            {
                int pkid1 = pkid.HasValue ? pkid.Value : 0;
                int appPathId = pathId.HasValue ? pathId.Value : 0;

                model.EmployeeWiseApprovalPath.intEmpPathID = pkid1;
                model.EmployeeWiseApprovalPath.intPathID = appPathId;
                model.EmployeeWiseApprovalPath = model.GetEmployeeWiseApprovalPath(model);
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }

        //POST: /EmployeeWiseApprovalPath/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(EmployeeWiseApprovalPathModels model)
        {
            try
            {
                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }


        //POST: /EmployeeWiseApprovalPath/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(EmployeeWiseApprovalPathModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.EmployeeWiseApprovalPath.intEmpPathID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPathDetails, model);
        }

        
        //GET: /EmployeeWiseApprovalPath/EmployeeWiseApprovalPathAdd
        [HttpGet]
        [NoCache]
        public ActionResult EmployeeWiseApprovalPathAdd(string id)
        {
            EmployeeWiseApprovalPathModels model = new EmployeeWiseApprovalPathModels();
            try
            {
                model.Message = Util.Messages.GetSuccessMessage("");
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /EmployeeWiseApprovalPath/EmployeeWiseApprovalPathAdd
        [HttpPost]
        [NoCache]
        public ActionResult EmployeeWiseApprovalPathAdd(EmployeeWiseApprovalPathModels model)
        {
            try
            {
                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {

                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }


        //POST: /EmployeeWiseApprovalPath/SaveEmployeeWiseApprovalPath
        [HttpPost]
        [NoCache]
        public ActionResult SaveEmployeeWiseApprovalPath(EmployeeWiseApprovalPathModels model)
        {
            try
            {
                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.EmployeeWiseApprovalPath = new EmployeeWiseApprovalPath();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPathDetails, model);
        }


        //POST: /EmployeeWiseApprovalPath/GetApprovalPathDetails
        [HttpPost]
        [NoCache]
        public ActionResult GetApprovalPathDetails(EmployeeWiseApprovalPathModels model)
        {
            try
            {
                model.EmployeeWiseApprovalPath = model.GetEmployeeWiseApprovalPath(model);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            ModelState.Clear();
            HttpContext.Response.Clear();

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPathDetails, model);
        }


        public JsonResult GetInitialNode(EmployeeWiseApprovalPathModels model)
        {

            ArrayList list = new ArrayList();
            try
            {
                if (model.intSearchPathID > 0)
                {

                    var items = (from item in model.GetPathwiseNodes(model.intSearchPathID)
                                 select new { item.intNodeID, item.strNodeName }).ToList();

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }


            return Json(list);
        }

       
    }
}
