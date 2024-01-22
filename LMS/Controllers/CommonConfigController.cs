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
    public class CommonConfigController : Controller
    {
        //
        // GET: /CommonConfig/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [NoCache]
        public ActionResult CommonConfigAdd(CommonConfigModels model)
        {

            return View(model);
        }


        //GET: /CommonConfig/CommonConfigDetails
        [HttpGet]
        [NoCache]
        public ActionResult CommonConfigDetails()
        {
            CommonConfigModels model = GetModelWithData();           
            return PartialView(LMS.Util.PartialViewName.CommonConfigDetails, model);            
        }

        //POST: /CommonConfig/SaveCommonConfig
        [HttpPost]
        [NoCache]
        public ActionResult SaveCommonConfig(CommonConfigModels model)
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
                    ModelState.Clear();
                   model = GetModelWithData();
                   model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                   
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.CommonConfigDetails, model);
        }

        [HttpPost]
        [NoCache]
        public ActionResult SaveHRMTableMapping(CommonConfigModels model)
        {
            string strmsg = "";
            try
            {
                //SaveData
                model.Save(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    model = GetData();
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView("Table", model);
        }


        [HttpPost]
        [NoCache]
        public ActionResult SaveHRMColumnMapping(CommonConfigModels model)
        {
            string strmsg = "";
            try
            {
                //SaveData
                model.SaveColumn(model, out strmsg);

                if (strmsg.ToString().Length > 0)
                {
                    model.Message = Util.Messages.GetErroMessage(strmsg.ToString());
                }
                else
                {
                    GetColumnsByTableId(model);
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotSave.ToString());
            }

            return PartialView("Column", model);
        }

        [HttpPost]
        [NoCache]
        public PartialViewResult GetColumns(CommonConfigModels model)
        {
            try
            {
              ModelState.Clear();
                GetColumnsByTableId(model);
            }
            catch (Exception ex)
            {
              
            }

            return PartialView("Column", model);
        }

        private void GetColumnsByTableId(CommonConfigModels model)
        {
            model.HRMColumnMapping.LstHRMColumnMapping = new List<HRMColumnMapping>();
            model.HRMColumnMapping.LstHRMColumnMapping = model.GetColumnConfigurationByTableId(model.IntTableID);                
        }



        [NoCache]
        private CommonConfigModels GetModelWithData()
        {
            CommonConfigModels model = new CommonConfigModels();
            model.CommonConfig.LstCommonConfig = model.GetCommonConfigAll();
            return model;
        }

        [NoCache]
        private CommonConfigModels GetData()
        {
            CommonConfigModels model = new CommonConfigModels();
            model.HRMTableMapping.LstHRMTableMapping = model.GetTableConfigurationAll();
            return model;
        }

        [NoCache]
        private CommonConfigModels GetColumnData()
        {
            CommonConfigModels model = new CommonConfigModels();
            model.HRMColumnMapping.LstHRMColumnMapping = model.GetColumnConfigurationAll();
            model.IsFirstTime = false;
            return model;
        }

        [HttpGet]
        [NoCache]
        public ActionResult getAjaxTab(int? page, int id)
        {
            string viewName = string.Empty;
            CommonConfigModels model = new CommonConfigModels();
            model.Message = string.Empty;
            switch (id)
            {
                case 1:
                    viewName = "Table";
                    model.HRMTableMapping.LstHRMTableMapping = model.GetTableConfigurationAll();
                    break;
                case 2:
                    viewName = "Column";
                    //model.IsFirstTime = true;
                    //GetColumnsByTableId(model);                   
                    break;

                default:
                    viewName = "Table";
                    model.HRMTableMapping.LstHRMTableMapping = model.GetTableConfigurationAll();
                    break;
            }
            //model.LstConveyanceMasterPaging = model.LstConveyanceMaster.ToPagedList(currentPageIndex, AppConstant.PageSize10);
            //ModelState.Clear();

            return PartialView(viewName, model);
        }
    

    }
}
