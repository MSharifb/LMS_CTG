using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models;
using LMSEntity;
using MvcPaging;
using LMS.Util;

namespace LMS.Web.Controllers
{
    public class HRPolicyNameController : Controller
    {
        //
        // GET: /HRPolicyName/

        [HttpGet]
        [NoCache]
        public ActionResult Index(int? page)
        {
            int totalRows=0;
            HRPolicyNameModels model = new HRPolicyNameModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstHRPolicyTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstHRPolicyTypeNamePaging = model.LstHRPolicyTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            
            return PartialView("HRPolicyNameList", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult HRPolicyNameAdd()
        {           
            HRPolicyNameModels model = new HRPolicyNameModels();
            HRPolicyTypeName obj = new HRPolicyTypeName();
            model.HrPolicyTypeNameObj = obj;

            ModelState.Clear();
            return View("Details", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult HRPolicyNameAdd(HRPolicyNameModels model)
        {
            int i = -1;
            try
            {

                if (model.HrPolicyTypeNameObj.HRPOLICYTYPENAMEID > 0)
                {


                    i = model.UpdateData(model.HrPolicyTypeNameObj);
                    model.Message = Messages.GetSuccessMessage(LMS.Util.Messages.UpdateSuccessfully);
                }
                else
                {
                    i = model.SaveData(model.HrPolicyTypeNameObj);
                    model.Message =Messages.GetSuccessMessage(Messages.AddSuccessfully);
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(ex.Message);
               
            }
            ModelState.Clear();
            return View("Details", model);
        }

    
        [HttpGet]
        [NoCache]
        public ActionResult GetDetails(string Id)
        {
            HRPolicyNameModels model = new HRPolicyNameModels();
            model.HrPolicyTypeNameObj = model.GetDataByID(int.Parse(Id));

            ModelState.Clear();
            return View("Details", model);
        }

        public ActionResult Search(int? page, HRPolicyNameModels model)
        {
            int totalRows = 0;
           
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstHRPolicyTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstHRPolicyTypeNamePaging = model.LstHRPolicyTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("HRPolicyNameList", model);

        }

        public ActionResult HRPolicyTypeNamePaging(int? page, HRPolicyNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstHRPolicyTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstHRPolicyTypeNamePaging = model.LstHRPolicyTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("HRPolicyNameList", model);
            
        }

    }
}
