using MVCDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoApp.ViewModels
{
    public class DropDownViewModel
    {
        public int DeptNo { get; set; }
        public DEPT Dept { get; set; }
        public List<SelectListItem> DeptList { get; set; }
        public List<EMP> EmpList { get; set; }
    }
}