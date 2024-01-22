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
    public class ApprovalPathController : Controller
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
            ApprovalPathModels model = new ApprovalPathModels();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            try
            {
                model.IsModeEdit = false;
                string strPathName = model.strSearchName;

                if (!string.IsNullOrEmpty(strPathName))
                {
                    strPathName = model.strSearchName.Trim();
                }
                model.GetApprovalPathMasterAll(strPathName);
                model.LstApprovalPathMaster1 = model.LstApprovalPathMaster.ToPagedList(currentPageIndex, AppConstant.PageSize);
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
        public ActionResult ApprovalPath(int? page, ApprovalPathModels model)
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
                model.GetApprovalPathMasterAll(strPathName);
                model.LstApprovalPathMaster1 = model.LstApprovalPathMaster.ToPagedList(currentPageIndex, AppConstant.PageSize);
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
        public ActionResult AddNode(ApprovalPathModels model)
        {
            model.BlnTextBlank = false;
            try
            {

                if (model.LstApprovalPathDetails != null)
                {
                    if (model.ApprovalPathDetails.intAuthorTypeID == 1)
                    {
                        ApprovalPathDetails apd = new ApprovalPathDetails();
                        apd = model.LstApprovalPathDetails.Where(c => c.intAuthorTypeID == 1).SingleOrDefault();

                        if (apd != null)
                        {
                            string strauthorType = model.AuthorType.Where(c => c.Value == model.ApprovalPathDetails.intAuthorTypeID.ToString()).Select(c => c.Text).SingleOrDefault();
                            model.Message = Util.Messages.GetErroMessage("Step type " + strauthorType.ToString() + " already exists.");
                            if (model.ApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
                        }
                    }

                    if (model.ApprovalPathDetails.intParentNodeID == 0)
                    {
                        ApprovalPathDetails apd = new ApprovalPathDetails();
                        apd = model.LstApprovalPathDetails.Where(c => c.strParentNode == "Initial Step").SingleOrDefault();
                        if (apd != null)
                        {
                            model.Message = Util.Messages.GetErroMessage("Initial step already exists.");

                            if (model.ApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
                        }
                    }
                    else
                    {
                        ApprovalPathDetails apd = new ApprovalPathDetails();
                        apd = model.LstApprovalPathDetails.Where(c => c.intParentNodeID == model.ApprovalPathDetails.intParentNodeID).SingleOrDefault();

                        if (apd != null)
                        {

                            model.Message = Util.Messages.GetErroMessage("Selected step sequence already exists.");
                            if (model.ApprovalPathMaster.intPathID > 0)
                            {
                                model.IsModeEdit = true;
                            }

                            return PartialView(LMS.Util.PartialViewName.ApprovalPathDetails, model);
                        }

                    }
                }


                if (model.LstApprovalPathDetails == null)
                {
                    model.LstApprovalPathDetails = new List<ApprovalPathDetails>();
                }


                model.ApprovalPathDetails.strAuthorType = model.AuthorType.Where(c => c.Value == model.ApprovalPathDetails.intAuthorTypeID.ToString()).Select(c => c.Text).SingleOrDefault();

                if (model.ApprovalPathDetails.intParentNodeID == 0)
                {
                    model.ApprovalPathDetails.strParentNode = "Initial Step";
                }
                else
                {
                    model.ApprovalPathDetails.strParentNode = model.ParentNode.Where(c => c.Value == model.ApprovalPathDetails.intParentNodeID.ToString()).Select(c => c.Text).SingleOrDefault();
                }


                if (model.LstApprovalPathDetails.Count > 0)
                {
                    model.ApprovalPathMaster.intPathID = model.LstApprovalPathDetails[0].intPathID;
                    model.ApprovalPathDetails.intPathID = model.LstApprovalPathDetails[0].intPathID;
                }


                int id = model.AddNode(model);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.LstApprovalPathDetails.Add(model.ApprovalPathDetails);
                    model.BlnTextBlank = true;
                    if (model.ApprovalPathMaster.intPathID > 0)
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
        public ActionResult GetAuthors(ApprovalPathModels model)
        {
            try
            {
                model.LstApprovalAuthor = model.GetApproverAuth(model.ApprovalAuthor.intNodeID);
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
        public ActionResult AddAuthor(ApprovalPathModels model)
        {
            model.BlnTextBlank = false;
            ApprovalAuthor apAut = new ApprovalAuthor();
            try
            {
                if (model.LstApprovalAuthor == null)
                {
                    if (model.ApprovalAuthor.strAuthorType != null && model.ApprovalAuthor.strAuthorType == "Alternate")
                    {
                        model.Message = Util.Messages.GetErroMessage("Author Type Main must have exists.");
                        return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                    }

                    if (model.CheckPathIdWiseAuthorExists(model) == 1)
                    {
                        model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected path.");

                        return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                    }

                    model.LstApprovalAuthor = new List<ApprovalAuthor>();
                }
                else
                {

                    if (model.ApprovalAuthor.strAuthorID != null && model.ApprovalAuthor.strAuthorID != "")
                    {
                        if (model.CheckPathIdWiseAuthorExists(model) == 1)
                        {
                            model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected path.");

                            return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                        }
                        else
                        {

                            apAut = model.LstApprovalAuthor.Where(c => c.strAuthorID == model.ApprovalAuthor.strAuthorID && c.intNodeID == model.ApprovalAuthor.intNodeID).SingleOrDefault();

                            if (apAut != null)
                            {
                                model.Message = Util.Messages.GetErroMessage("Author ID already exists in the selected node.");
                                return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                            }
                        }
                    }

                    if (model.ApprovalAuthor.strAuthorType != null && model.ApprovalAuthor.strAuthorType == "Main")
                    {
                        apAut = model.LstApprovalAuthor.Where(c => c.strAuthorType == "Main").SingleOrDefault();

                        if (apAut != null)
                        {
                            model.Message = Util.Messages.GetErroMessage("Author type Main already exists.");
                            return View(LMS.Util.PartialViewName.SetApproverDetails, model);
                        }
                    }
                }

                model.ApprovalAuthor.intPathID = model.ApprovalPathMaster.intPathID;
                model.LstApprovalAuthor.Add(model.ApprovalAuthor);
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
        public ActionResult deleteAuthor(ApprovalPathModels model, string Id)
        {
            ApprovalAuthor apd = new ApprovalAuthor();
            try
            {
                apd = model.LstApprovalAuthor.Where(c => c.intNodeID == model.ApprovalAuthor.intNodeID && c.intPathID == model.ApprovalPathMaster.intPathID && c.strAuthorID == Id).SingleOrDefault();
                model.LstApprovalAuthor.Remove(apd);
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
        public ActionResult DeleteNode(ApprovalPathModels model, int Id, FormCollection fc)
        {
            ApprovalPathDetails apd = new ApprovalPathDetails();

            try
            {
                apd = model.LstApprovalPathDetails.Where(c => c.intParentNodeID == Id).SingleOrDefault();

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
                        apd = new ApprovalPathDetails();
                        apd = model.LstApprovalPathDetails.Where(c => c.intNodeID == Id).SingleOrDefault();
                        model.LstApprovalPathDetails.Remove(apd);

                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.RemoveSuccessfully.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = Util.Messages.GetErroMessage(Util.Messages.ExceptionOccurred.ToString());
            }

            if (model.ApprovalPathMaster.intPathID > 0)
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
            ApprovalPathModels model = new ApprovalPathModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                model.IsModeEdit = true;
                model.GetApprovalPath(Id);

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
        public ActionResult Details(ApprovalPathModels model, FormCollection fc)
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
                    model = new ApprovalPathModels();
                    model.IsModeEdit = false;
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());

                    model.ApprovalPathMaster = new ApprovalPathMaster();
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
        public ActionResult Delete(ApprovalPathModels model, FormCollection fc)
        {
            try
            {
                int id = model.Delete(model.ApprovalPathMaster.intPathID);

                if (id < 0)
                {
                    model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                }
                else
                {
                    model.Message = Util.Messages.GetSuccessMessage(Util.Messages.DeleteSuccessfully.ToString());
                    model.ApprovalPathMaster = new ApprovalPathMaster();
                    model.ApprovalPathDetails = new ApprovalPathDetails();
                    model.LstApprovalPathDetails = new List<ApprovalPathDetails>();
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
            ApprovalPathModels model = new ApprovalPathModels();
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
            ApprovalPathModels model = new ApprovalPathModels();
            model.Message = Util.Messages.GetErroMessage("");
            try
            {
                model.ApprovalPathMaster = model.GetApprovalPathMaster(id);
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
        public ActionResult SetApprover(ApprovalPathModels model)
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
                    model.ApprovalAuthor = new ApprovalAuthor();
                    model.LstApprovalAuthor = new List<ApprovalAuthor>();
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
        public ActionResult ApprovalPathAdd(ApprovalPathModels model)
        {
            try
            {

                if (model.ApprovalPathMaster.strPathName != null && model.ApprovalPathMaster.strPathName != "")
                {

                    int id = model.SaveData(model);

                    if (id < 0)
                    {
                        model.Message = Util.Messages.GetErroMessage(Util.Messages.DbErrorMessage(id));
                    }
                    else
                    {
                        model = new ApprovalPathModels();
                        model.IsModeEdit = false;
                        model.Message = Util.Messages.GetSuccessMessage(Util.Messages.UpdateSuccessfully.ToString());
                        model.ApprovalPathMaster = new ApprovalPathMaster();
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
        public ActionResult SaveApprovalPath(ApprovalPathModels model)
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
