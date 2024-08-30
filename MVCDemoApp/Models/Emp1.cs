using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemoApp.Models
{
    public class Emp1
    {
        [Required(ErrorMessage ="EmpNo is a primary key")]
        public int EMPNO { get; set; }
    }
}