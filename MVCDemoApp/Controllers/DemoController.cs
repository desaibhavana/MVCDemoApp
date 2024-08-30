using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoApp.Controllers
{
    public class DemoController : Controller
    {
        //Data persistant techniques

        //[1]- pass data from controller action ---> view

        //ViewBag  - dynamic  - assign datatype at runtime based on value
        // ViewData - Dictionary collection - store every value as an object collection

        //[2]  pass data from controller action ---> action ---> view
        //TempData  - Dictionary collection - store every value as an object collection

        //[3] -  controller action ---> action or model or view
        //Session


        // GET: Demo
        public ActionResult Index()
        {
            ViewBag.data = "ViewBag demo";
            ViewData["data1"] = "ViewData demo";
            TempData["tempdata"] = "TempData demo";
            Session["sessiondata"] = "Session demo";

            //return RedirectToAction("actionname", "controllername");
            return RedirectToAction("Display");
        }

        public ActionResult Display()
        {
            return View();
        }
    }
}