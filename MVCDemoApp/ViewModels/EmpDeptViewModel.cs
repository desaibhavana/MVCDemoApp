using MVCDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MVCDemoApp.ViewModels
{
    public class EmpDeptViewModel
    {
        public EMP Emp { get; set; }
        //  public DEPT Dept { get; set; }

        //Handle null dept for King Record
       
        private DEPT dept;

        public DEPT Dept
        {
            get { return dept; }
            set
            {
                dept = value;
                if (dept == null)
                {
                    dept = new DEPT();
                }
            }
        }



        private int expYears = 0;
        public int YearsCompleted   //read only property
        {
            get
            {
                if (Emp.HIREDATE.HasValue)
                {


                    //check if year complete or not
                    //if no from expYears - 1 year
                    // DateTime dt = Convert.ToDateTime("1/3/2025");  //60

                    /*
                    2023 - 03 - 01 00:00:00.000
                    2020 - 03 - 01 00:00:00.000   //leap  - 61
                    2024 - 03 - 01 00:00:00.000   //leap
                    2020 - 02 - 29 00:00:00.000   //leap
                    2021 - 02 - 28 00:00:00.000
                    2024 - 02 - 29 00:00:00.000     //leap
                    2023 - 08 - 23 00:00:00.000
                    */
                    //find years difference
                    expYears = DateTime.Now.Year - Emp.HIREDATE.Value.Year;

                    //to check leap year logic

                    //Change today's date year to the same year of hire date
                    DateTime today = DateTime.Now.AddYears(-expYears);

                    //if hiredate > today then --1

                    // hiredate  17 Dec 1980
                    //today      22 Aug 1980

                    if (Emp.HIREDATE.Value > today)
                    {
                        expYears--;
                    }
                }
                return expYears;
            }
        }
        public string SalaryColor  //read only 
        {
            get
            {
                if (Emp.SAL.HasValue)
                {

                    decimal salvalue = 2500;

                    if (Emp.SAL.Value >= salvalue)
                    {
                        return "red";
                    }
                    else
                    {
                        return "blue";
                    }
                }
                return "white";
            }
        }
    }
}