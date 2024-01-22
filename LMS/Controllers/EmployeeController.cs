using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Util;
using LMS.Web.Models;
using LMS.Web.ViewModels.Shared;
using System.Linq.Expressions;
using MvcPaging;

namespace LMS.Web.Controllers
{

    public class EmployeeEditViewModel
    {
        public bool IsAdd { get; set; }
        public bool HasErrors { get; set; }
        public Employee Employee { get; set; }
    }

    public class EmployeeSearchForm : SearchForm
    {
        public string strEmpID { get; set; }
        public string strEmpName { get; set; }
        public string strDesignation { get; set; }

        public string Active { get; set; }
        public string Inactive { get; set; }
        public string All { get; set; }
    }

    [NoCache]
    public class EmployeeController : Controller
    {

        private EmployeeModels _EmployeeModels;
        private GridStateService<Employee, EmployeeSearchForm> _gridStateService;

        private Grid<Employee, EmployeeSearchForm> _grid;

        public EmployeeController()
        {
            _EmployeeModels = new EmployeeModels();

            _gridStateService = new GridStateService<Employee, EmployeeSearchForm>();
        }


        [NoCache]
        public ActionResult Index()
        {
            //return RedirectToAction("List");
            return View();
        }


        [HttpGet]
        [NoCache]
        public ActionResult EmployeeList(int? page)
        {

            EmployeeModels model = new EmployeeModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.strSortBy = "strEmpID";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 10;
            model.Employee = new Employee();

            Employee objSearch = model.Employee;
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

            if (!string.IsNullOrEmpty(model.strSearchZoneId))
            {
                objSearch.ZoneId =Convert.ToInt32(model.strSearchZoneId);
            }



            objSearch.strDepartmentID = model.strSearchDepartmentId;
            objSearch.ActiveStatus = model.strSearchStatus;

            objSearch.strSearchType = "AND";

            model.Employees = model.GetEmployeeData(objSearch).OrderBy(x => x.strEmpInitial).ToList();
            // Added For BEPZA
            //if (LoginInfo.Current.LoggedZoneId > 0)
            //{
            //    model.Employees = model.Employees.Where(e => e.ZoneId == LoginInfo.Current.LoggedZoneId).ToList();
            //    model.LstEmployeesPaging = model.Employees.Where(e=> e.ZoneId == LoginInfo.Current.LoggedZoneId).ToPagedList(currentPageIndex, AppConstant.PageSize);
            //}
            //else
                //model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            // added by Nazrul

            model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            //return PartialView(LMS.Util.PartialViewName.SearchEmployee, model);
            return PartialView("EmpList", model);
        }


        [HttpPost]
        [NoCache]
        public ActionResult EmployeeList(int? page,string id, EmployeeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.strSortBy = "strEmpID";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 10;
            model.Employee = new Employee();

            Employee objSearch = model.Employee;

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
            if (!string.IsNullOrEmpty(model.strSearchZoneId))
            {
                objSearch.ZoneId = Convert.ToInt32(model.strSearchZoneId);
            }

            objSearch.strDepartmentID = model.strSearchDepartmentId;
            objSearch.ActiveStatus = model.strSearchStatus;

            objSearch.strSearchType = "AND";

            model.Employees = model.GetEmployeeData(objSearch).OrderBy(x=>x.strEmpInitial).ToList();

            // Added For BEPZA
            //if (LoginInfo.Current.LoggedZoneId > 0)
            //{
            //    model.Employees = model.Employees.Where(e => e.ZoneId == LoginInfo.Current.LoggedZoneId).ToList();
            //    model.LstEmployeesPaging = model.Employees.Where(e => e.ZoneId == LoginInfo.Current.LoggedZoneId).ToPagedList(currentPageIndex, AppConstant.PageSize);
            //}
            //else
            //model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            model.CurrentFocusID = id;
            return PartialView(LMS.Util.PartialViewName.SearchEmployee, model);
        }


        [HttpPost]
        [NoCache]
        public ActionResult Search(int? page, string id, EmployeeModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.strSortBy = "strEmpID";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 10;
            model.Employee = new Employee();

            Employee objSearch = model.Employee;

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
            if (!string.IsNullOrEmpty(model.strSearchZoneId))
            {
                objSearch.ZoneId = Convert.ToInt32(model.strSearchZoneId);
            }

            objSearch.strDepartmentID = model.strSearchDepartmentId;
            objSearch.ActiveStatus = model.strSearchStatus;

            objSearch.strSearchType = "AND";

            model.Employees = model.GetEmployeeData(objSearch).OrderBy(x => x.strEmpInitial).ToList();

            // Added For BEPZA
            //if (LoginInfo.Current.LoggedZoneId > 0)
            //{
            //    model.Employees = model.Employees.Where(e => e.ZoneId == LoginInfo.Current.LoggedZoneId).ToList();
            //    model.LstEmployeesPaging = model.Employees.Where(e => e.ZoneId == LoginInfo.Current.LoggedZoneId).ToPagedList(currentPageIndex, AppConstant.PageSize);
            //}
            //else
            //    model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            model.LstEmployeesPaging = model.Employees.ToPagedList(currentPageIndex, AppConstant.PageSize);

            model.CurrentFocusID = id;
            return PartialView(LMS.Util.PartialViewName.SearchEmployee, model);
        }


        [HttpGet]
        public JsonResult LookUps(string q, int limit)
        {

           

            EmployeeModels model = new EmployeeModels();
            Employee objSearch = model.Employee;

            int currentPageIndex = 0;
            model.strSortBy = "strEmpID";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 15;
            
            
            objSearch.strEmpID = "";
            

            if (!string.IsNullOrEmpty(q))
            {
                objSearch.strEmpName = q.Trim();
            }
            

            objSearch.strDepartmentID ="";
            objSearch.ActiveStatus = "Active";

            objSearch.strSearchType = "AND";




            model.Employees = model.GetEmployeeData(objSearch).OrderBy(x => x.strEmpInitial).ToList();

            var data = from s in model.Employees select new { s.strEmpID,s.strEmpName};

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [NoCache]
        public JsonResult LookUpById(string id)
        {



            EmployeeModels model = new EmployeeModels();
            Employee objSearch = model.Employee;

            int currentPageIndex = 0;
            model.strSortBy = "strEmpInitial";
            model.strSortType = LMS.Util.DataShortBy.ASC;
            model.startRowIndex = (currentPageIndex) * 10 + 1;
            model.maximumRows = 15;



            if (!string.IsNullOrEmpty(id))
            {
                objSearch.strEmpInitial = id.Trim();
            }


            objSearch.strDepartmentID = "";
            objSearch.ActiveStatus = "Active";

            objSearch.strSearchType = "AND";




            model.Employees = model.GetEmployeeData(objSearch);

            var first = model.Employees.FirstOrDefault();
            var data = new string[3];

            if (first != null)
            {
                data[0] = first.strEmpID;
                data[1] = first.strEmpName;
                data[2] = first.strEmpInitial;
            }

            return Json(data);
        }



        private string GetGridKey()
        {
            return this.Request.Path;
        }


        [NoCache]
        public ActionResult _Grid()
        {
            UpdateGrid();

            return View(_grid);
        }


        //public ActionResult List()
        //{
        //    if (this.Request.RequestType == "POST" 
        //            || this.Request.QueryString.Count > 0)	//In case Javascript is disabled or user wants to pass parameters in query string
        //    {
        //        UpdateGrid();

        //        return View(_grid);
        //    }

        //    string key = GetGridKey();
        //    if (_gridStateService.Exists(key))
        //    {
        //        _grid = _gridStateService.Load(key);

        //        //code to use if you would like query string to include grid filters, etc...
        //        //string url = _grid.GetUrl();
        //        //Response.Redirect(url);
        //    }
        //    else
        //    {
        //        InitGrid();
        //    }

        //    FillGridData();

        //    return View(_grid);
        //}


        //private void InitGrid()
        //{
        //    _grid = new Grid<Employee, EmployeeSearchForm>
        //            (
        //                new Pager { CurrentPage = 1, PageSize = 10 },
        //                new Sorter("strEmpID", SortDirection.Asc)
        //            );
        //}


        public void UpdateGrid()
        {
            //InitGrid();

            if (!TryUpdateModel(_grid))
            {
                _grid.Data = new List<Employee>();
                return;
            }

            //Javascript disabled or using query string
            if (this.Request.RequestType == "GET")
            {
                if (Request.QueryString["advanced-search-submit"] != null)
                    _grid.GridAction = GridAction.AdvancedSearch;

                if (Request.QueryString["keyword-search-submit"] != null)
                    _grid.GridAction = GridAction.KeywordSearch;
            }

            _grid.ProcessAction();

            FillGridData();

            if (_grid.Data.Count > 0)
                SaveGridState();
        }


        private void FillGridData()
        {

            _EmployeeModels.startRowIndex = ((_grid.Pager.CurrentPage - 1) * (_grid.Pager.PageSize) + 1);
            _EmployeeModels.strSortBy = _grid.Sorter.SortField;
            _EmployeeModels.strSortType = _grid.Sorter.SortDirection.ToString();
            _EmployeeModels.maximumRows = _grid.Pager.PageSize;








            AddQuerySearchCriteria(_EmployeeModels, _grid.SearchForm);

            IQueryable<Employee> query = _EmployeeModels.GetQueryable();

            _grid.Pager.Init(_EmployeeModels.numTotalRows);

            if (_EmployeeModels.numTotalRows == 0)
            {
                _grid.Data = new List<Employee>();
                return;
            }



            List<Employee> Employees = query.ToList();
            _grid.Data = Employees;
        }


        private void SaveGridState()
        {
            string key = GetGridKey();
            _gridStateService.Save(key, _grid);
        }
        // objBLL.EmployeeGet("", Employee.strEmpName, "Active", Employee.strDepartmentID, Employee.strDepartmentID, Employee.strGender, Employee.strReligionID, LoginInfo.Current.strCompanyID, strSortBy, strSortType, startRowIndex, maximumRows, numTotalRows);

        private void AddQuerySearchCriteria(EmployeeModels _EmployeeModels, EmployeeSearchForm searchForm)
        {






            if (searchForm.IsAdvanced)
            {
                if (!string.IsNullOrEmpty(searchForm.strEmpID))
                    _EmployeeModels.Employee.strEmpID = searchForm.strEmpID.Trim();
                else
                    _EmployeeModels.Employee.strEmpID = searchForm.strEmpID;

                if (!string.IsNullOrEmpty(searchForm.strEmpName))
                    _EmployeeModels.Employee.strEmpName = searchForm.strEmpName.Trim();
                else
                    _EmployeeModels.Employee.strEmpName = searchForm.strEmpName;

                if (!string.IsNullOrEmpty(searchForm.strDesignation))
                    _EmployeeModels.Employee.strDesignation = searchForm.strDesignation.Trim();

                else
                    _EmployeeModels.Employee.strDesignation = searchForm.strDesignation;

                _EmployeeModels.Employee.strSearchType = " AND ";


            }
            else
            {
                if (!string.IsNullOrEmpty(searchForm.Keyword))
                {
                    _EmployeeModels.Employee.strEmpID = searchForm.Keyword.Trim();

                    _EmployeeModels.Employee.strEmpName = searchForm.Keyword.Trim();
                    _EmployeeModels.Employee.strDesignation = searchForm.Keyword.Trim();
                }
                else
                {

                    _EmployeeModels.Employee.strEmpID = searchForm.Keyword;

                    _EmployeeModels.Employee.strEmpName = searchForm.Keyword;
                    _EmployeeModels.Employee.strDesignation = searchForm.Keyword;
                }

                _EmployeeModels.Employee.strSearchType = " OR ";

            }



            /*	if (!searchForm.IsAdvanced)
                {
                    if (!String.IsNullOrEmpty(searchForm.Keyword))
                    {
                        string keyword = searchForm.Keyword;
                        query = query.Where(Employee => Employee.FirstName.ContainsCaseInsensitive(keyword)
                                                        || Employee.LastName.ContainsCaseInsensitive(keyword)
                                                        || (Employee.Email != null && Employee.Email.ContainsCaseInsensitive(keyword))
                                                        || (Employee.Phone != null && Employee.Phone.ContainsCaseInsensitive(keyword)));
                }
                    return query;

                }

                if (!String.IsNullOrEmpty(searchForm.FirstName))
                    query = query.Where(Employee => Employee.FirstName.ContainsCaseInsensitive(searchForm.FirstName));
			
                if (!String.IsNullOrEmpty(searchForm.LastName))
                    query = query.Where(Employee => Employee.LastName.ContainsCaseInsensitive(searchForm.LastName));

                if (!String.IsNullOrEmpty(searchForm.Phone))
                    query = query.Where(Employee => Employee.Phone != null && Employee.Phone.ContainsCaseInsensitive(searchForm.Phone));

                if (!String.IsNullOrEmpty(searchForm.Email))
                    query = query.Where(Employee => Employee.Email != null && Employee.Email.ContainsCaseInsensitive(searchForm.Email));
			
                if (searchForm.FromDateOfLastOrder.HasValue)
                    query = query.Where(Employee => Employee.DateOfLastOrder >= searchForm.FromDateOfLastOrder);

                if (searchForm.ToDateOfLastOrder.HasValue)
                    query = query.Where(Employee => Employee.DateOfLastOrder <= searchForm.ToDateOfLastOrder);
                */

        }


        private IQueryable<Employee> AddQuerySorting(IQueryable<Employee> query, Sorter sorter)
        {
            if (String.IsNullOrEmpty(sorter.SortField))
                return query;

            //Used approach from http://www.singingeels.com/Articles/Self_Sorting_GridView_with_LINQ_Expression_Trees.aspx
            //instead of a long switch statement 
            var param = Expression.Parameter(typeof(Employee), "Employee");
            var sortExpression = Expression.Lambda<Func<Employee, object>>
                                    (Expression.Convert(Expression.Property(param, sorter.SortField), typeof(object)), param);

            if (sorter.SortDirection == SortDirection.Asc)
                query = query.OrderBy(sortExpression);
            else
                query = query.OrderByDescending(sortExpression);

            return query;
        }


        //private IQueryable<Employee> AddQueryPaging(IQueryable<Employee> query, LMS.Web.ViewModels.Shared.Pager pager)
        //{
        //    if (pager.TotalPages == 0)
        //        return query;

        //    query = query.Skip((pager.CurrentPage - 1) * pager.PageSize)
        //                        .Take(pager.PageSize);
        //    return query;
        //}


        public ActionResult Add()
        {
            Employee Employee = new Employee();

            var viewModel = new EmployeeEditViewModel { IsAdd = true, Employee = Employee };
            return View("Edit", viewModel);
        }

        /*
                [AcceptVerbs(HttpVerbs.Post)]
                public ActionResult AddSave()
                {
                    var Employee = new Employee();

                    if (!TryUpdateModel(Employee, "Employee"))
                    {
                        var viewModel = new EmployeeEditViewModel
                                            {
                                                IsAdd = true,
                                                HasErrors = true,
                                                Employee = Employee 
                                            };
			
                        return View("Edit", viewModel);
                    }

                    _EmployeeModels.Add(Employee);

                    TempData["StatusLine"] = "Added new Employee " + Employee.GetFullName() + "."; ;
                    return RedirectToAction("List");
                }


                public ActionResult Edit(int id)
                {
                    Employee Employee = _EmployeeModels.GetByID(id);

                    var viewModel = new EmployeeEditViewModel { IsAdd = false, Employee = Employee };
			
                    return View(viewModel);
                }


                [AcceptVerbs(HttpVerbs.Post)]
                public ActionResult EditSave(int EmployeeID)
                {
                    Employee Employee = _EmployeeModels.GetByID(EmployeeID);

                    if (!TryUpdateModel(Employee, "Employee"))
                    {
                        var viewModel = new EmployeeEditViewModel
                        {
                            IsAdd = false,
                            HasErrors = true,
                            Employee = Employee
                        };

                        return View("Edit", viewModel);
                    }
				
                    _EmployeeModels.Update(Employee);

                    TempData["StatusLine"] = "Updated Employee " + Employee.GetFullName() + "."; ;
			
                    return RedirectToAction("List");
                }


                public ActionResult Delete(int id)
                {
                    Employee Employee = _EmployeeModels.GetByID(id);

                    _EmployeeModels.Delete(id);

                    TempData["StatusLine"] = "Deleted Employee " + Employee.GetFullName() + ".";
			
                    return RedirectToAction("List");
                }

                */

        [NoCache]
        public ActionResult GetKeywordAutoCompleteData(string q, int limit)
        {
            string keyword = q;

            //IQueryable<Employee> query = _EmployeeModels.GetQueryable();
            ///*query = query.Where(Employee => Employee.FirstName.ToLower().StartsWith(keyword)
            //                                || Employee.LastName.ToLower().StartsWith(keyword))
            //                .Take(10)
            //                .OrderBy(Employee => Employee.FirstName)
            //                .ThenBy(Employee => Employee.LastName);
            //*/
            //var list = query.Select(Employee => new { 
            //                                            Employee.strEmpID, 

            //                                            Name = Employee.strEmpName + " " + Employee.strEmpName});



            BLL.EmployeeBLL objBLL = new LMS.BLL.EmployeeBLL();

            int total = 0;
            var list = objBLL.EmployeeGet("","", q, "Active", "", "", "", "", "", LoginInfo.Current.strCompanyID, "AND", "strEmpName", "ASC", 1, limit, out total).Select(Employee => new
            {
                Employee.strEmpID,

                Name = Employee.strEmpName + " " + Employee.strEmpName
            });





            return Json(list);
        }
    }
}
