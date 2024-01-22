using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models;
using LMS.Web.ViewModels.Shared;
using LMS.Web;
using LMS.Util;

namespace LMS.Web.Controllers
{
    [NoCache]
	public class CustomerEditViewModel
	{
		public bool IsAdd { get; set; }
		public bool HasErrors { get; set; }
		public Customer Customer { get; set; }
	}

    [NoCache]
	public class CustomerSearchForm : SearchForm
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public DateTime? FromDateOfLastOrder { get; set; }
		public DateTime? ToDateOfLastOrder { get; set; }
	}

    [NoCache]
	public class CustomerController : Controller
	{
        
		private CustomerService _customerService;
		private  GridStateService<Customer, CustomerSearchForm> _gridStateService;

		private Grid<Customer, CustomerSearchForm> _grid;
        
        public CustomerController()
		{
			_customerService=new CustomerService();

			_gridStateService = new GridStateService<Customer, CustomerSearchForm>();
		}

        [NoCache]
		public ActionResult Index()
		{
			return RedirectToAction("List");
		}

        [NoCache]
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

        [NoCache]
		public ActionResult List()
		{
			if (this.Request.RequestType == "POST" 
					|| this.Request.QueryString.Count > 0)	//In case Javascript is disabled or user wants to pass parameters in query string
			{
				UpdateGrid();

				return View(_grid);
			}

			string key = GetGridKey();
			if (_gridStateService.Exists(key))
			{
				_grid = _gridStateService.Load(key);
				
			}
			else
			{
				InitGrid();
			}

			FillGridData();

			return View(_grid);
		}

        [NoCache]
		private void InitGrid()
		{
			_grid = new Grid<Customer, CustomerSearchForm>
					(
						new Pager { CurrentPage = 1, PageSize = 5 },
						new Sorter("ID", SortDirection.Asc)
					);
		}

        [NoCache]
		public void UpdateGrid()
		{
			InitGrid();

			if (!TryUpdateModel(_grid))
			{
				_grid.Data = new List<Customer>();
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

        [NoCache]
		private void FillGridData()
		{
			IQueryable<Customer> query = _customerService.GetQueryable();

			query = AddQuerySearchCriteria(query, _grid.SearchForm);
			
			int totalRows = query.Count();
			_grid.Pager.Init(totalRows);

			if (totalRows == 0)
			{
				_grid.Data = new List<Customer>();
				return;
			}

			query = AddQuerySorting(query, _grid.Sorter);
			query = AddQueryPaging(query, _grid.Pager);

			List<Customer> customers = query.ToList();
			_grid.Data = customers;
		}

        [NoCache]
		private void SaveGridState()
		{
			string key = GetGridKey();
			_gridStateService.Save(key, _grid);
		}

        [NoCache]
		private IQueryable<Customer> AddQuerySearchCriteria(IQueryable<Customer> query, CustomerSearchForm searchForm)
		{
			if (!searchForm.IsAdvanced)
			{
				if (!String.IsNullOrEmpty(searchForm.Keyword))
				{
					string keyword = searchForm.Keyword;
					query = query.Where(customer => customer.FirstName.ContainsCaseInsensitive(keyword)
													|| customer.LastName.ContainsCaseInsensitive(keyword)
													|| (customer.Email != null && customer.Email.ContainsCaseInsensitive(keyword))
													|| (customer.Phone != null && customer.Phone.ContainsCaseInsensitive(keyword)));
				}
				return query;

			}

			if (!String.IsNullOrEmpty(searchForm.FirstName))
				query = query.Where(customer => customer.FirstName.ContainsCaseInsensitive(searchForm.FirstName));
			
			if (!String.IsNullOrEmpty(searchForm.LastName))
				query = query.Where(customer => customer.LastName.ContainsCaseInsensitive(searchForm.LastName));

			if (!String.IsNullOrEmpty(searchForm.Phone))
				query = query.Where(customer => customer.Phone != null && customer.Phone.ContainsCaseInsensitive(searchForm.Phone));

			if (!String.IsNullOrEmpty(searchForm.Email))
				query = query.Where(customer => customer.Email != null && customer.Email.ContainsCaseInsensitive(searchForm.Email));
			
			if (searchForm.FromDateOfLastOrder.HasValue)
				query = query.Where(customer => customer.DateOfLastOrder >= searchForm.FromDateOfLastOrder);

			if (searchForm.ToDateOfLastOrder.HasValue)
				query = query.Where(customer => customer.DateOfLastOrder <= searchForm.ToDateOfLastOrder);

			return query;
		}

        [NoCache]
		private IQueryable<Customer> AddQuerySorting(IQueryable<Customer> query, Sorter sorter)
		{
			if (String.IsNullOrEmpty(sorter.SortField))
				return query;

			//Used approach from http://www.singingeels.com/Articles/Self_Sorting_GridView_with_LINQ_Expression_Trees.aspx
			//instead of a long switch statement 
			var param = Expression.Parameter(typeof(Customer), "customer");
			var sortExpression = Expression.Lambda<Func<Customer, object>>
									(Expression.Convert(Expression.Property(param, sorter.SortField), typeof(object)), param);

			if (sorter.SortDirection == SortDirection.Asc)
				query = query.OrderBy(sortExpression);
			else
				query = query.OrderByDescending(sortExpression);

			return query;
		}

        [NoCache]
		private IQueryable<Customer> AddQueryPaging(IQueryable<Customer> query, Pager pager)
		{
			if (pager.TotalPages == 0)
				return query;

			query = query.Skip((pager.CurrentPage - 1) * pager.PageSize)
								.Take(pager.PageSize);
			return query;
		}

        [NoCache]
		public ActionResult Add()
		{
			Customer customer = new Customer();
	
			var viewModel = new CustomerEditViewModel { IsAdd = true, Customer = customer };
			return View("Edit", viewModel);
		}


		[AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
		public ActionResult AddSave()
		{
			var customer = new Customer();

			if (!TryUpdateModel(customer, "Customer"))
			{
				var viewModel = new CustomerEditViewModel
				                	{
				                		IsAdd = true,
				                		HasErrors = true,
										Customer = customer 
									};
			
				return View("Edit", viewModel);
			}

			_customerService.Add(customer);

			TempData["StatusLine"] = "Added new customer " + customer.GetFullName() + "."; ;
			return RedirectToAction("List");
		}

        [NoCache]
		public ActionResult Edit(int id)
		{
			Customer customer = _customerService.GetByID(id);

			var viewModel = new CustomerEditViewModel { IsAdd = false, Customer = customer };
			
			return View(viewModel);
		}


		[AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
		public ActionResult EditSave(int customerID)
		{
			Customer customer = _customerService.GetByID(customerID);

			if (!TryUpdateModel(customer, "Customer"))
			{
				var viewModel = new CustomerEditViewModel
				{
					IsAdd = false,
					HasErrors = true,
					Customer = customer
				};

				return View("Edit", viewModel);
			}
				
			_customerService.Update(customer);

			TempData["StatusLine"] = "Updated customer " + customer.GetFullName() + "."; ;
			
			return RedirectToAction("List");
		}

        [NoCache]
		public ActionResult Delete(int id)
		{
			Customer customer = _customerService.GetByID(id);

			_customerService.Delete(id);

			TempData["StatusLine"] = "Deleted customer " + customer.GetFullName() + ".";
			
			return RedirectToAction("List");
		}

        [NoCache]
		public ActionResult GetKeywordAutoCompleteData(string q, int limit)
		{
			string keyword = q;

			IQueryable<Customer> query = _customerService.GetQueryable();
			query = query.Where(customer => customer.FirstName.ToLower().StartsWith(keyword)
			                                || customer.LastName.ToLower().StartsWith(keyword))
							.Take(10)
							.OrderBy(customer => customer.FirstName)
							.ThenBy(customer => customer.LastName);

			var list = query.Select(customer => new { 
														customer.ID, 
														Name = customer.FirstName + " " + customer.LastName});

			return Json(list);
		}
	}
}
