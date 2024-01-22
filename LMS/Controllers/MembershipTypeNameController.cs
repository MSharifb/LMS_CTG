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
    public class MembershipTypeNameController : Controller
    {
        //
        // GET: /HRPolicyName/

        [HttpGet]
        [NoCache]
        public ActionResult Index(int? page)
        {
            int totalRows = 0;
            MembershipTypeNameModels model = new MembershipTypeNameModels();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstMembershipTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstMembershipTypeNamePaging = model.LstMembershipTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("MembershipTypeNameList", model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult MembershipTYpeNameAdd()
        {
            int i = -1;
            MembershipTypeNameModels model = new MembershipTypeNameModels();

            ModelState.Clear();
            return View("Details", model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult MembershipTypeNameAdd(MembershipTypeNameModels model)
        {
            int i = -1;

            try
            {


                if (model.MembershipTypeNameObj.MEMBERSHIPTYPENAMEID > 0)
                {
                    i = model.UpdateData(model.MembershipTypeNameObj);
                    model.Message =Messages.GetSuccessMessage(LMS.Util.Messages.UpdateSuccessfully);
                }
                else
                {
                    i = model.SaveData(model.MembershipTypeNameObj);
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
            MembershipTypeNameModels model = new MembershipTypeNameModels();
            model.MembershipTypeNameObj = model.GetDataByID(int.Parse(Id));

            ModelState.Clear();
            return View("Details", model);
        }


        public ActionResult Search(int? page, MembershipTypeNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstMembershipTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstMembershipTypeNamePaging = model.LstMembershipTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("MembershipTypeNameList", model);

        }

        public ActionResult MembershipTypeNamePaging(int? page, MembershipTypeNameModels model)
        {
            int totalRows = 0;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.startRowIndex = (currentPageIndex) * AppConstant.PageSize10 + 1;
            model.maximumRows = AppConstant.PageSize10;


            model.LstMembershipTypeName = model.GetData(-1, "", model.startRowIndex, model.maximumRows, out totalRows);
            model.numTotalRows = totalRows;
            model.LstMembershipTypeNamePaging = model.LstMembershipTypeName.ToPagedList(currentPageIndex, AppConstant.PageSize10);

            return PartialView("MembershipTypeNameList", model);

        }

    }
}
