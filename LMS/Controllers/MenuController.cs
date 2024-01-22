using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Web.Models;
using LMS.BLL;
using LMS.Util;
using LMS.UserMgtService;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class MenuController : Controller
    {
        // GET: /Menu/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get parent menu list for the current user
        /// </summary>
        /// <returns></returns>

        [ChildActionOnly]
        [NoCache]
        public ActionResult Header()
        {
            MenuModels model = new MenuModels();

            if (User.Identity.Name.ContainsCaseInsensitive(AppConstant.SysInitializer))
            {
                Menu menu = UserMgtAgent.GetMenuByMenuName(Permission.MenuNamesId.SystemInitialization.ToString());
                HttpContext.Session["pmenuId"] = menu.MenuId;
                model.MenuList = new List<Menu>();
                model.MenuList.Add(menu);
            }
            else
            {
                /*
                    The below line is commented out because to get others module too.
                    If we use the commented out line then menu related to LMS module will show.
                */

                //model.MenuList = UserMgtAgent.GetMenus(User.Identity.Name.Trim(), "LMS", "LMS");
                model.MenuList = UserMgtAgent.GetMenus(User.Identity.Name.Trim(), "IWM", "LMS");
                model.MenuList = model.MenuList.Where(c => c.PageUrl.Contains("/LMS/")).ToList();
                //model.MenuList = model.MenuList.OrderBy(m => m.MenuId).ToList();
                if (model.MenuList.Count > 0 && HttpContext.Session["pmenuId"] == null)
                {
                    foreach (Menu menu in model.MenuList)
                    {
                        if (menu.IsAssignedMenu && menu.ParentMenuId == -1)
                        {
                            HttpContext.Session["pmenuId"] = menu.MenuId;
                            HttpContext.Session["pmenuName"] = menu.MenuName;
                            break;
                        }
                    }

                }
            }
            return PartialView(model);
        }

        /// <summary>
        /// Get Left/Child menu contents but parent Id stored in Session
        /// </summary>
        /// <returns></returns>

        [ChildActionOnly]
        [NoCache]
        public ActionResult LeftMenu(int pid)
        {

            MenuModels model = new MenuModels();
            if (HttpContext.Session["pmenuId"] != null)
            {
                model.MenuList = UserMgtAgent.GetChildMenusByLoginAndParentId(User.Identity.Name, pid);
                //model.MenuList = model.MenuList.OrderBy(m => m.MenuId).ToList();
                model.MenuList = model.MenuList.OrderBy(m => m.SerialNo).ToList();
            }
            return PartialView(model);
        }

    }
}
