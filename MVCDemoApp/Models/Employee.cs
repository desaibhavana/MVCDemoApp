using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVCDemoApp.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "EmpId is required")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Enter four digit EmpId")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "EmpName is required")]
        public string EmpName { get; set; }

        public decimal Salary { get; set; }
    }
}
