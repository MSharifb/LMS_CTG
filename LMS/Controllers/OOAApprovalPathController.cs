using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSEntity;
using LMS.Web;
using System.Globalization;
using MvcContrib.Pagination;
using LMS.Web.Models;
using LMS.Util;
using System.Configuration;
using MvcPaging;

namespace LMS.Web.Controllers
{
    [NoCache]
    public class OOAApprovalPathController : Controller
    {     
        //GET: /ApprovalPath/
        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /ApprovalPath/ApprovalPath
        [HttpGet]
        [NoCache]
        public ActionResult ApprovalPath(int? page)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.IsModeEdit = false;
                string strPathName = model.strSearchName;

                if (!string.IsNullOrEmpty(strPathName))
                {
                    strPathName = model.strSearchName.Trim();
                }
                model.GetOOAApprovalPathMasterAll(strPathName);
                model.LstOOAApprovalPathMaster1 = model.LstOOAApprovalPathMaster.ToPagedList(currentPageIndex, AppConstant.PageSize);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }

  
        //POST: /ApprovalPath/ApprovalPath
        [HttpPost]
        [NoCache]
        public ActionResult ApprovalPath(int? page, OOAApprovalPathModels model)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            try
            {
                model.IsModeEdit = false;
                string strPathName = model.strSearchName;

                if (!string.IsNullOrEmpty(strPathName))
                {
                    strPathName = model.strSearchName.Trim();
                }
                model.GetOOAApprovalPathMasterAll(strPathName);
                model.LstOOAApprovalPathMaster1 = model.LstOOAApprovalPathMaster.ToPagedList(currentPageIndex, AppConstant.PageSize);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);
        }

        //POST: /ApprovalPath/AddNode
        [HttpPost]
        [NoCache]
        public ActionResult AddNode(OOAApprovalPathModels model)
        {
            model.BlnTextBlank = false;
            try
            {

                if (model.LstOOAApprovalPathDetails != null)
                {
                    if (model.OOAApprovalPathDetails.intAuthorTypeID == 1)
                    {
                        OOAApprovalPathDetails apd = new OOAApprovalPathDetails();
                        apd = model.LstOOAApprovalPathDetails.Where(c => c.intAuthorTypeID == 1).SingleOrDefault();

                        if (apd != null)
                        {
                            string strauthorType = model.OOAAuthorType.Where(c => c.Value == model.OOAApprovalPathDetails.intAuthorTypeID.ToString()).Select(c => c.Text).SingleOrDefault();
                            model.Message = Util.Messages.GetErroMessage("Step type " + strauthorType.ToString() + " already exists.");
                            if (model.OOAApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.OOAApprovalPathDetails, model);
                        }
                    }

                    if (model.OOAApprovalPathDetails.intParentNodeID == 0)
                    {
                        OOAApprovalPathDetails apd = new OOAApprovalPathDetails();
                        apd = model.LstOOAApprovalPathDetails.Where(c => c.strParentNode == "Initial Step").SingleOrDefault();
                        if (apd != null)
                        {
                            model.Message = Util.Messages.GetErroMessage("Initial step already exists.");

                            if (model.OOAApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
                        }
                    }
                    else
                    {
                        OOAApprovalPathDetails apd = new OOAApprovalPathDetails();
                        apd = model.LstOOAApprovalPathDetails.Where(c => c.intParentNodeID == model.OOAApprovalPathDetails.intParentNodeID).SingleOrDefault();

                        if (apd != null)
                        {

                            model.Message = Util.Messages.GetErroMessage("Selected step sequence already exists.");
                            if (model.OOAApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
                        }

                    }
                }


                if (model.LstOOAApprovalPathDetails == null)
                {
                    model.LstOOAApprovalPathDetails = new List<OOAApprovalPathDetails>();
                }


                model.OOAApprovalPathDetails.strAuthorType = model.OOAAuthorType.Where(c => c.Value == model.OOAApprovalPathDetails.intAuthorTypeID.ToString()).Select(c => c.Text).SingleOrDefault();

                if (model.OOAApprovalPathDetails.intParentNodeID == 0)
                {
                    model.OOAApprovalPathDetails.strParentNode = "Initial Step";
                }
                else
                {
                    model.OOAApprovalPathDetails.strParentNode = model.ParentNode.Where(c => c.Value == model.OOAApprovalPathDetails.intParentNodeID.ToString()).Select(c => c.Text).SingleOrDefault();
                }


                if (model.LstOOAApprovalPathDetails.Count > 0)
                {
                    model.OOAApprovalPathMaster.intPathID = model.LstOOAApprovalPathDetails[0].intPathID;
                    model.OOAApprovalPathDetails.intPathID = model.LstOOAApprovalPathDetails[0].intPathID;
                }


                int id = model.AddNode(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.LstOOAApprovalPathDetails.Add(model.OOAApprovalPathDetails);
                    model.BlnTextBlank = true;
                    if (model.OOAApprovalPathMaster.intPathID > 0)
                    {
                        model.IsModeEdit = true;
                    }
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.AddSuccessfully.ToString());
                }

            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
        }


        //POST: /ApprovalPath/GetAuthors
        [HttpPost]
        [NoCache]
        public ActionResult GetAuthors(OOAApprovalPathModels model)
        {
            try
            {
                model.LstOOAApprovalAuthor = model.GetOOAApproverAuth(model.OOAApprovalAuthor.intNodeID);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            ModelState.Clear();
            HttpContext.Response.Clear();

            return PartialView(LMS.Util.PartialViewName.SetApproverDetails, model);
        }

        
        //POST: /ApprovalPath/AddAuthor
        [HttpPost]
        [NoCache]
        public ActionResult AddAuthor(OOAApprovalPathModels model)
        {
            model.BlnTextBlank = false;
            OOAApprovalAuthor apAut = new OOAApprovalAuthor();
            try
            {
                if (model.LstOOAApprovalAuthor == null)
                {
                    if (model.OOAApprovalAuthor.strAuthorType != null && model.OOAApprovalAuthor.strAuthorType == "Alternate")
                    {
                        model.Message = Util.Messages.GetErroMessage("Author Type Main must have exists.");
                        return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                    }

                    if (model.CheckPathIdWiseAuthorExists(model) == 1)
                    {
                        model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected path.");

                        return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                    }

                    model.LstOOAApprovalAuthor = new List<OOAApprovalAuthor>();
                }
                else
                {

                    if (model.OOAApprovalAuthor.strAuthorID != null && model.OOAApprovalAuthor.strAuthorID != "")
                    {
                        if (model.CheckPathIdWiseAuthorExists(model) == 1)
                        {
                            model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected path.");

                            return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                        }
                        else
                        {

                            apAut = model.LstOOAApprovalAuthor.Where(c => c.strAuthorID == model.OOAApprovalAuthor.strAuthorID && c.intNodeID == model.OOAApprovalAuthor.intNodeID).SingleOrDefault();

                            if (apAut != null)
                            {
                                model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected node.");
                                return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                            }
                        }
                    }

                    if (model.OOAApprovalAuthor.strAuthorType != null && model.OOAApprovalAuthor.strAuthorType == "Main")
                    {
                        apAut = model.LstOOAApprovalAuthor.Where(c => c.strAuthorType == "Main").SingleOrDefault();

                        if (apAut != null)
                        {
                            model.Message = Util.Messages.GetErroMessage("Author type Main already exists.");
                            return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                        }
                    }
                }

                model.OOAApprovalAuthor.intPathID = model.OOAApprovalPathMaster.intPathID;
                model.LstOOAApprovalAuthor.Add(model.OOAApprovalAuthor);
                model.BlnTextBlank = true;
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            return PartialView(LMS.Util.PartialViewName.SetApproverDetails, model);
        }


        //POST: /ApprovalPath/deleteAuthor
        [HttpPost]
        [NoCache]
        public ActionResult deleteAuthor(OOAApprovalPathModels model, string Id)
        {
            OOAApprovalAuthor apd = new OOAApprovalAuthor();
            try
            {
                apd = model.LstOOAApprovalAuthor.Where(c => c.intNodeID == model.OOAApprovalAuthor.intNodeID && c.intPathID == model.OOAApprovalPathMaster.intPathID && c.strAuthorID == Id).SingleOrDefault();
                model.LstOOAApprovalAuthor.Remove(apd);
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView(LMS.Util.PartialViewName.SetApproverDetails, model);

        }


        //POST: /ApprovalPath/DeleteNode
        [HttpPost]
        [NoCache]
        public ActionResult DeleteNode(OOAApprovalPathModels model, int Id, FormCollection fc)
        {
            OOAApprovalPathDetails apd = new OOAApprovalPathDetails();

            try
            {
                apd = model.LstOOAApprovalPathDetails.Where(c => c.intParentNodeID == Id).SingleOrDefault();

                if (apd != null)
                {
                    model.Message = Util.Messages.GetErroMessage("Node cannot be removed which is assigned as parent node.");
                }
                else
                {

                    int id = model.DeleteNode(Id);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        apd = new OOAApprovalPathDetails();
                        apd = model.LstOOAApprovalPathDetails.Where(c => c.intNodeID == Id).SingleOrDefault();
                        model.LstOOAApprovalPathDetails.Remove(apd);

                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.RemoveSuccessfully.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            if (model.OOAApprovalPathMaster.intPathID > 0)
            {
                model.IsModeEdit = true;
            }

            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);

        }

        
        //GET: /ApprovalPath/Details/5
        [HttpGet]
        [NoCache]
        public ActionResult Details(int Id)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                model.IsModeEdit = true;
                model.GetOOAApprovalPath(Id);

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);

        }

        //POST: /ApprovalPath/Details
        [HttpPost]
        [NoCache]
        public ActionResult Details(OOAApprovalPathModels model, FormCollection fc)
        {

            try
            {
                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model = new OOAApprovalPathModels();
                    model.IsModeEdit = false;
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                    model.OOAApprovalPathMaster = new OOAApprovalPathMaster();
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /ApprovalPath/Delete
        [HttpPost]
        [NoCache]
        public ActionResult Delete(OOAApprovalPathModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.OOAApprovalPathMaster.intPathID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.OOAApprovalPathMaster = new OOAApprovalPathMaster();
                    model.OOAApprovalPathDetails = new OOAApprovalPathDetails();
                    model.LstOOAApprovalPathDetails = new List<OOAApprovalPathDetails>();
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.CouldnotDelete.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);

        }


        //GET: /ApprovalPath/ApprovalPathAdd    
        [HttpGet]
        [NoCache]
        public ActionResult ApprovalPathAdd(string id)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            model.Message = Util.Messages.GetErroMessage("");

            try
            {
                model.IsModeEdit = false;

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.UnableToLoad.ToString());
            }

            return View(model);

        }


        //GET: /ApprovalPath/SetApprover
        [HttpGet]
        [NoCache]
        public ActionResult SetApprover(int id)
        {
            OOAApprovalPathModels model = new OOAApprovalPathModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                model.OOAApprovalPathMaster = model.GetOOAApprovalPathMaster(id);
            }

            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }
            
            return View(model);
        }


        //POST: /ApprovalPath/SetApprover
        [HttpPost]
        [NoCache]
        public ActionResult SetApprover(OOAApprovalPathModels model)
        {
            try
            {
                int id = model.SetApprover(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.SavedSuccessfully.ToString());
                    model.OOAApprovalAuthor = new OOAApprovalAuthor();
                    model.LstOOAApprovalAuthor = new List<OOAApprovalAuthor>();
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.SetApproverDetails, model);
        }


        //POST: /ApprovalPath/ApprovalPathAdd
        [HttpPost]
        [NoCache]
        public ActionResult ApprovalPathAdd(OOAApprovalPathModels model)
        {
            try
            {

                if (model.OOAApprovalPathMaster.strPathName != null && model.OOAApprovalPathMaster.strPathName != "")
                {

                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model = new OOAApprovalPathModels();
                        model.IsModeEdit = false;
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                        model.OOAApprovalPathMaster = new OOAApprovalPathMaster();
                        ModelState.Clear();
                    }
                }
                else
                {
                    model.Message = Util.Messages.GetErroMessage("Path Name couldn't be blank.");
                }
            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
        }


        //POST: /ApprovalPath/SaveApprovalPath
        [HttpPost]
        [NoCache]
        public ActionResult SaveApprovalPath(OOAApprovalPathModels model)
        {
            try
            {
                int id = model.SaveData(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            return View(model);
        }


        //POST: /ApprovalPath/Create
        [HttpPost]
        [NoCache]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        //GET: /ApprovalPath/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            return View();
        }

        
        //POST: /ApprovalPath/Edit/5
        [HttpPost]
        [NoCache]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
