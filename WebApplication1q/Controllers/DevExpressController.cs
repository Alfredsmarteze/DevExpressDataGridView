using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1q.Controllers
{
    public class DevExpressController : Controller
    {
        // GET: DevExpress
        public ActionResult Index()
        {
            return View();
        }



        WebApplication1q.Models.DevExpressPracticeDBEntities6 db = new WebApplication1q.Models.DevExpressPracticeDBEntities6();

        [ValidateInput(false)]
        public ActionResult GridView2Partial()
        {
            var model = db.DevExps;
            return PartialView("_GridView2Partial", model.ToList());
        }

        [HttpGet]
        public ActionResult GridView2PartialAddNew()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        //
        public ActionResult GridView2PartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] WebApplication1q.Models.DevExp item)
        {
            var model = db.DevExps;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return View("Index", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        //[ModelBinder(typeof(DevExpressEditorsBinder))]
        public ActionResult GridView2PartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] WebApplication1q.Models.DevExp item)
        {
            var model = db.DevExps;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialDelete(int Id)
        {
            var model = db.DevExps;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView2Partial", model.ToList());
        }
    }
}