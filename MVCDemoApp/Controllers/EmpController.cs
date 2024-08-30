using MVCDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MVCDemoApp.ViewModels;
using Newtonsoft.Json;

namespace MVCDemoApp.Controllers
{
    public class EmpController : Controller
    {
        TrainingDBEntities db = new TrainingDBEntities();

        // GET: Emp
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;


            ViewBag.NoSortParam = String.IsNullOrEmpty(sortOrder) ? "No_desc" : "";
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) || sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.JobSortParam = String.IsNullOrEmpty(sortOrder) || sortOrder == "Job" ? "Job_desc" : "Job";
            ViewBag.DeptSortParam = String.IsNullOrEmpty(sortOrder) || sortOrder == "Dept" ? "Dept_desc" : "Dept";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var emps = from emp in db.EMPs select emp;


            if (!String.IsNullOrEmpty(searchString))
            {
                emps = emps.Where(e => e.ENAME.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "No_desc":
                    emps = emps.OrderByDescending(e => e.EMPNO);
                    break;
                case "Name":
                    emps = emps.OrderBy(e => e.ENAME);
                    break;
                case "Name_desc":
                    emps = emps.OrderByDescending(e => e.ENAME);
                    break;
                case "Job":
                    emps = emps.OrderBy(e => e.JOB);
                    break;
                case "Job_desc":
                    emps = emps.OrderByDescending(e => e.JOB);
                    break;
                case "Dept":
                    emps = emps.OrderBy(e => e.DEPTNO);
                    break;
                case "Dept_desc":
                    emps = emps.OrderByDescending(e => e.DEPTNO);
                    break;
                default:
                    emps = emps.OrderBy(e => e.EMPNO);
                    break;
            }
            // return View(emps.ToList()); 

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(emps.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            //search emp by passing empno
            return View(db.EMPs.SingleOrDefault(e => e.EMPNO == id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new EMP());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EMP emp)
        {
            if (ModelState.IsValid)
            {
                db.EMPs.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emp);
        }

        //get - edit
        //display edit form with emp data
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //search emp by passing empno
            return View(db.EMPs.SingleOrDefault(e => e.EMPNO == id));
        }

        //post - eidt
        //save changes to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EMP emp)
        {
            if (ModelState.IsValid)
            {

                //[1]

                //EMP editemp = db.EMPs.SingleOrDefault(e => e.EMPNO == emp.EMPNO);
                //if (editemp != null)
                //{
                //   editemp.ENAME = emp.ENAME;
                //    editemp.JOB=emp.JOB;
                //    //...
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}

                //[2]
                if (ModelState.IsValid)
                {
                    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();

            }

            return View(emp);
        }


        public ActionResult EmpDeptData()
        {
            //inner join
            var data = (from e in db.EMPs join d in db.DEPTs on e.DEPTNO equals d.DEPTNO into joindata from dept in joindata.DefaultIfEmpty() select new EmpDeptViewModel { Emp = e, Dept = dept }).ToList();

            //left join --- king

            return View(data);
        }

        public ActionResult EmpDeptRelation(int? DEPTNO)
        {
            //populating dropdown
            ViewBag.depts = (from dept in db.DEPTs select dept).ToList();

            //if DEPTNO parameter not null
            //when user select deptno from dropdown , dept data will be stored in data variable
            //else data = null
            EmpDeptRelationViewModel data = (from d in db.DEPTs
                                             where d.DEPTNO == DEPTNO
                                             select new EmpDeptRelationViewModel() { DEPTNO = d.DEPTNO, DeptName = d.DNAME, Location = d.LOC }).FirstOrDefault();

            if (data != null)
            {
                if (data.DEPTNO != null)
                {
                    var empdata = (from e in db.EMPs
                                   where e.DEPTNO == DEPTNO
                                   select e).ToList();
                    data.EmpList = empdata;
                }
            }

            return View(data);
        }


        [HttpGet]
        public ActionResult DropDownDemo()
        {
            var deptList = (from d in db.DEPTs
                            select new SelectListItem()
                            {
                                Text = d.DNAME,
                                Value = d.DEPTNO.ToString()
                            }).ToList();
            deptList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            DropDownViewModel dropDownViewModel = new DropDownViewModel();
            dropDownViewModel.DeptList = deptList;
            dropDownViewModel.EmpList = new List<EMP>();        
            //store DeptList in session so 
            Session["dataobj"] = dropDownViewModel;
            return View(dropDownViewModel);
        }

        [HttpPost]
        public ActionResult DropDownDemo(DropDownViewModel viewmodel)
        {
            //read session varaible
            DropDownViewModel dropDownViewModel = Session["dataobj"] as DropDownViewModel;

            //populate CategoryList from session variable else we will get null error for dropdown SelectList
            viewmodel.DeptList = dropDownViewModel.DeptList;

            int dno = viewmodel.DeptNo;
            //for the deptno fetch data from database and populate object

            DEPT deptobj = (from d in db.DEPTs where d.DEPTNO == dno select d).SingleOrDefault();

            viewmodel.Dept = deptobj;

            //populate emp list
            var empdata = (from e in db.EMPs where e.DEPTNO == dno select e).ToList();

            viewmodel.EmpList = empdata;

            return View(viewmodel);
        }
    }
}