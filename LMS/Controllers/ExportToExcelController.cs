using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections;
using System.Globalization;
using MvcContrib.Pagination;
using LMSEntity;
using LMS.Web;
using LMS.Web.Models;
using LMS.Util;
using MvcPaging;
using System.IO;
using System.Web.UI;

namespace LMS.Web.Controllers
{
    public class ExportToExcelController : Controller
    {
        //
        // GET: /ExportToExcel/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExportToExcel(ReportsModels model, int? page)
        {
            HttpResponse httpResp = System.Web.HttpContext.Current.Response;
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("Content-Disposition", "attachment; filename=LMSExcel.xls");
                Response.Charset = "";
                
                string strRptHTMLData = "";
                strRptHTMLData = GetData(model, page);
                if (strRptHTMLData.ToString().Length > 0)
                {
                    Response.Write(strRptHTMLData);
                    Response.End();
                }
                else
                {
                    model.Message = "Data not found to preview report.";
                    Response.Clear();
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            { }
            return View();
        }


        public JsonResult GetReportData(ReportsModels model, int? page)
        {
            string strRptHTMLData = "";
            strRptHTMLData = GetData(model, page);

            return Json(strRptHTMLData);
        }

        private string GetData(ReportsModels model, int? page)
        {
            string strRptHTMLData = "";
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                if (model.ReportId == LMS.Util.ReportId.LeaveStatus)
                {
                    //strFileName = LMS.Util.ReportId.LeaveStatus + ".xls";

                   // model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                   // model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveStatus = model.GetLeaveStatus(model);

                    if (model.LstRptLeaveStatus.Count > 0)
                    {
                        strRptHTMLData = LMS.Web.Common.getHTMLData_LeaveStatus(model.LstRptLeaveStatus);
                    }

                }
                else if (model.ReportId == LMS.Util.ReportId.LeaveAvailed)
                {
                   // model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                  //  model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType,dtApplyFromDate";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveEnjoyed = model.GetLeaveEnjoyed(model);
                    int type = 0;
                    int wpay = 0;
                    if (model.LstRptLeaveEnjoyed.Count > 0)
                    {
                        string FromDate="", ToDate="";
                        if (model.StrFromDate != null && model.StrFromDate != "" && model.StrToDate != null && model.StrToDate != "")
                          { 
                        
                        if (model.IsApplyDate == true)
                        {
                            type = 0;
                            FromDate=model.StrFromDate.ToString();
                            ToDate = model.StrToDate.ToString();
                        }
                          else
                          { type=1;
                            FromDate=model.StrFromDate.ToString();
                            ToDate = model.StrToDate.ToString();
                          }
                        }
                          else
                          {
                              //Html.Encode(Model.LstRptLeaveEnjoyed[0].strYearTitle.ToString());
                        }
                        if (model.IsWithoutPay == true)
                        {
                            wpay = 0;
                        }
                        else wpay = 1;

                        strRptHTMLData = LMS.Web.Common.getHTMLData_LeaveEnjoyed(model.LstRptLeaveEnjoyed, FromDate, ToDate, type,wpay);
                    }

                }
                else if (model.ReportId == LMS.Util.ReportId.LeaveEncashment)
                {
                 //   model.startRowIndex = (currentPageIndex) * AppConstant.PageSize20 + 1;
                 //   model.maximumRows = AppConstant.PageSize20;
                    model.strSortBy = "strEmpID,strLeaveType";
                    model.strSortType = LMS.Util.DataShortBy.ASC;
                    model.LstRptLeaveEncasment = model.GetLeaveEncasment(model);
                    if (model.LstRptLeaveEncasment.Count > 0)
                    {
                        strRptHTMLData = LMS.Web.Common.getHTMLData_LeaveEncasment(model.LstRptLeaveEncasment);
                    }
                }

            }
            catch (Exception ex)
            { }
            return strRptHTMLData;
        }





    }
}
