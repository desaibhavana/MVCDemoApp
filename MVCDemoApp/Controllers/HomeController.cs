using MVCDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //attribute base routing
        [Route("home/Greet/{name}")]
        public ActionResult Greet(string name)
        {
            ViewBag.wish = name;
            // ViewData["wish"] = name;
            return View();
        }

        public ActionResult GetData(int? id)
        {
            // ViewBag.data = id;
            ViewBag.data = "id param value = " + id;
            return View();
        }


        public ActionResult AddNumbers(int num1 = 0, int num2 = 0)
        {
            int result = num1 + num2;
            ViewBag.sum = $"Addition of {num1} + {num2} = {result}  ";
            return View();
        }


        public ActionResult ModelDemo()
        {
            Employee employee = new Employee() { EmpId = 1, EmpName = "bhavana", Salary = 10000 };
            ViewBag.employee = employee;
            ViewData["emp"] = employee;
            return View();
        }

        //Display Form to take inout
        public ActionResult EmployeeForm()
        {
            return View();
        }

        //on submit call this method to display data
        public ActionResult DisplayEmployee()
        {
            Employee emp = new Employee();
            emp.EmpId = Convert.ToInt32(Request.Form["txtEmpID"]);
            emp.EmpName = Request.Form["txtName"];
            emp.Salary = Convert.ToDecimal(Request.Form["txtSalary"]);
            ViewBag.emp = emp;
            return View();
        }

        //Display Form to take inout using model binder / strongly typed view
        //STV - one class
        //STV - cannot use anonymous class
        public ActionResult EmployeeFormST()
        {
            return View(new Employee() );
        }

        //read data from view using model binder object / strongly typed view
        public ActionResult DisplayEmployeeST(Employee employee)
        {
            if (ModelState.IsValid == true)
            {
                return View(employee);
            }

            return View("EmployeeFormST", employee);
        }
    }
}