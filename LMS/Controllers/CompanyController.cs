using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Util;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class CompanyController : Controller
    {
       
        // GET: /Company/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

       
        // GET: /Company/Details/5        
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

       
        // GET: /Company/Create
        [NoCache]
        public ActionResult Create()
        {
            return View();
        } 

        
        // POST: /Company/Create
        [HttpPost]
        [NoCache]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        // GET: /Company/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

       
        // POST: /Company/Edit/5
        [HttpPost]
        [NoCache]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
