using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOLibrary;

namespace MVCDemoApp.Controllers
{
    public class DataController : Controller
    {
        EmployeeDataStore dataStore = new EmployeeDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);

        // GET: Data
        public ActionResult Index()
        {
            List<Employee> employees = dataStore.GetEmployees();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Details(Employee employee)
        {
            if (employee.EmpNo != 0)
            {
                employee = dataStore.GetEmpByNo(employee.EmpNo);
            }
            return View(employee);
        }

    }
}