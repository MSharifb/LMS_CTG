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
    public class JobTypeNameController : Controller
    {
        //
        // GET: /HRPolicyName/

        [HttpGet]
        [NoCache]
        public ActionResult Index(int? page)
        {
            int totalRows = 0;
            JobTypeNameModels model = new JobTypeNameModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstJobTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstJobTypeNamePaging = model.LstJobTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("JobTypeNameList", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult JobTYpeNameAdd()
        {
            int i = -1;
            JobTypeNameModels model = new JobTypeNameModels();

            ModelState.Clear();
            return View("Details", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult JobTypeNameAdd(JobTypeNameModels model)
        {
            int i = -1;

            try
            {


                if (model.JobTypeNameObj.JOBTYPENAMEID > 0)
                {
                    i = model.UpdateData(model.JobTypeNameObj);
                    model.Message =Messages.GetSuccessMessage(LMS.Util.Messages.UpdateSuccessfully);
                }
                else
                {
                    i = model.SaveData(model.JobTypeNameObj);
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
            JobTypeNameModels model = new JobTypeNameModels();
            model.JobTypeNameObj = model.GetDataByID(int.Parse(Id));

            ModelState.Clear();
            return View("Details", model);
        }

        public ActionResult Search(int? page,JobTypeNameModels model)
        {
            int totalRows = 0;
            
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstJobTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstJobTypeNamePaging = model.LstJobTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("JobTypeNameList", model);

        }

        public ActionResult JobTypeNamePaging(int? page, JobTypeNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstJobTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstJobTypeNamePaging = model.LstJobTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("JobTypeNameList", model);

        }

    }
}
