using MVCDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoApp.ViewModels
{
    public class EmpDeptRelationViewModel
    {
        public Nullable<int> DEPTNO { get; set; }
        public string DeptName { get; set; }
        public string Location { get; set; }

        private List<EMP> empList = new List<EMP>();
        public List<EMP> EmpList
        {
            get
            {
                return empList;
            }
            set
            {
                empList = value;
            }
        }
    }
}