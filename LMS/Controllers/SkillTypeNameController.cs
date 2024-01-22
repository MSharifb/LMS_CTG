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
    public class SkillTypeNameController : Controller
    {
        //
        // GET: /SkillTypeName/

        [HttpGet]
        [NoCache]
        public ActionResult Index(int? page)
        {
            int totalRows = 0;
            SkillTypeNameModels model = new SkillTypeNameModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstSkillTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstSkillTypeNamePaging = model.LstSkillTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("SkillTypeNameList", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult SkillTypeNameAdd()
        {
            SkillTypeNameModels model = new SkillTypeNameModels();
            SkillTypeName obj = new SkillTypeName();
            model.SkillTypeNameObj = obj;

            ModelState.Clear();
            return View("Details", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult SkillTypeNameAdd(SkillTypeNameModels model)
        {
            int i = -1;
            try
            {

                if (model.SkillTypeNameObj.SKILLTYPENAMEID > 0)
                {


                    i = model.UpdateData(model.SkillTypeNameObj);
                    model.Message = Messages.GetSuccessMessage(LMS.Util.Messages.UpdateSuccessfully);
                }
                else
                {
                    i = model.SaveData(model.SkillTypeNameObj);
                    model.Message = Messages.GetSuccessMessage(Messages.AddSuccessfully);
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
            SkillTypeNameModels model = new SkillTypeNameModels();
            model.SkillTypeNameObj = model.GetDataByID(int.Parse(Id));

            ModelState.Clear();
            return View("Details", model);
        }

        public ActionResult Search(int? page, SkillTypeNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstSkillTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstSkillTypeNamePaging = model.LstSkillTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("SkillTypeNameList", model);

        }

        public ActionResult SkillTypeNamePaging(int? page, SkillTypeNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstSkillTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstSkillTypeNamePaging = model.LstSkillTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("SkillTypeNameList", model);

        }

    }
}
