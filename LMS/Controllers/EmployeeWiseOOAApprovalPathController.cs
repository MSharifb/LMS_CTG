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
    public class EmployeeWiseOOAApprovalPathController : Controller
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
            
            EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.strSortBy = "strPathName,strNodeName";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.PageNumber = currentPageIndex + 1;


            model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
            EmployeeWiseOOAApprovalPath objSearch = model.EmployeeWiseOOAApprovalPath;

            //---[Search by approval author ID and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchAuthorID))
            {
                objSearch.strAuthorID = model.strSearchAuthorID.Trim();
            }
            else
            {
                objSearch.strAuthorID = model.strSearchAuthorID;
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
            if (!string.IsNullOrEmpty(model.strSearchID))
            {
                objSearch.strEmpID = model.strSearchID.Trim();
            }
            else
            {
                objSearch.strEmpID = model.strSearchID;
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
            objSearch.intFlowType = model.IntFlowTypeID;

            model.GetEmployeeWiseOOAApprovalPathAll(objSearch);
            model.LstEmployeeWiseOOAApprovalPathPaging = model.LstEmployeeWiseOOAApprovalPath.ToPagedList(currentPageIndex, AppConstant.PageSize);

            return View(model);
        }

        //POST: /EmployeeWiseApprovalPath/EmployeeWiseApprovalPath
        [HttpPost]
        [NoCache]
        public ActionResult EmployeeWiseApprovalPath(int? page, EmployeeWiseOOAApprovalPathModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.strSortBy = "strPathName,strNodeName";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize + 1;
            model.maximumRows = AppConstant.PageSize;
            model.PageNumber = currentPageIndex + 1;

            model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
            EmployeeWiseOOAApprovalPath objSearch = model.EmployeeWiseOOAApprovalPath;

            //---[Search by approval author ID and Name]------------------------
            if (!string.IsNullOrEmpty(model.strSearchAuthorID))
            {
                objSearch.strAuthorID = model.strSearchAuthorID.Trim();
            }
            else
            {
                objSearch.strAuthorID = model.strSearchAuthorID;
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
            if (!string.IsNullOrEmpty(model.strSearchID))
            {
                objSearch.strEmpID = model.strSearchID.Trim();
            }
            else
            {
                objSearch.strEmpID = model.strSearchID;
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
            objSearch.intFlowType = model.IntFlowTypeID;
            model.GetEmployeeWiseOOAApprovalPathAll(objSearch);

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPath, model);
        }


        //GET: /EmployeeWiseApprovalPath/Details
        [HttpGet]
        [NoCache]
        public ActionResult Details(int? pkid, int? pathId,int? flowID)
        {

            EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();
            model.Message = Util.Messages.GetSuccessMessage("");

            try
            {
                int pkid1 = pkid.HasValue ? pkid.Value : 0;
                int appPathId = pathId.HasValue ? pathId.Value : 0;
                int flowTypeID = flowID.HasValue ? flowID.Value : 0;

                model.EmployeeWiseOOAApprovalPath.intEmpPathID = pkid1;
                model.EmployeeWiseOOAApprovalPath.intPathID = appPathId;
                model.EmployeeWiseOOAApprovalPath.intFlowType = flowTypeID;
                model.EmployeeWiseOOAApprovalPath = model.GetEmployeeWiseOOAApprovalPath(model);
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
        public ActionResult Details(EmployeeWiseOOAApprovalPathModels model)
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
        public ActionResult Delete(EmployeeWiseOOAApprovalPathModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.EmployeeWiseOOAApprovalPath.intEmpPathID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
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
            
            EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();

            try
            {
                //SelectList lst = model.OOAFLOWLIST(model.EmployeeWiseOOAApprovalPath.intFlowType);
                //ViewData["data"] = lst;

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
        public ActionResult EmployeeWiseApprovalPathAdd(EmployeeWiseOOAApprovalPathModels model)
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
                    model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
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
        public ActionResult SaveEmployeeWiseApprovalPath(EmployeeWiseOOAApprovalPathModels model)
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
                    model.EmployeeWiseOOAApprovalPath = new EmployeeWiseOOAApprovalPath();
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
        public ActionResult GetApprovalPathDetails(EmployeeWiseOOAApprovalPathModels model)
        {
            try
            {
                int pathID = model.EmployeeWiseOOAApprovalPath.intPathID;
                model.EmployeeWiseOOAApprovalPath = model.GetEmployeeWiseOOAApprovalPath(model);
                model.EmployeeWiseOOAApprovalPath.intPathID = pathID;
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            ModelState.Clear();
            HttpContext.Response.Clear();

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPathDetails, model);
        }


        public JsonResult GetInitialNode(EmployeeWiseOOAApprovalPathModels model)
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

        public ActionResult GetFlowList(EmployeeWiseOOAApprovalPathModels model)
        {
           // EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();
            List<SelectListItem> lst = model.OOAFLOWLIST;
            //ViewData["data"] = lst;
          //  model.intPathID = model.EmployeeWiseOOAApprovalPath.intPathID;

            return PartialView(LMS.Util.PartialViewName.EmployeeWiseApprovalPathDetails, model);
        }

        [HttpGet]
        public ActionResult GetFlowList1(EmployeeWiseOOAApprovalPathModels model)
        {
            // EmployeeWiseOOAApprovalPathModels model = new EmployeeWiseOOAApprovalPathModels();
            model.EmployeeWiseOOAApprovalPath.intFlowType = model.IntFlowTypeID;
            List<SelectListItem> lst = model.OOAFLOWLIST;
            //ViewData["data"] = lst;
            //  model.intPathID = model.EmployeeWiseOOAApprovalPath.intPathID;

            return Json(new SelectList(lst, "Value", "Text"), JsonRequestBehavior.AllowGet);
            
        }
    }
}
