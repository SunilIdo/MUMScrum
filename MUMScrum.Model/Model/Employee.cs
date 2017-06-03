using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.ENUMS;

namespace MUMScrum.Model.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Sex { get; set; }
        public double Salary { get; set; }
        public double Bonus { get; set; }
        public double Commission { get; set; }
        public DateTime HireDate { get; set; }
        public RoleEnum RoleEnum { get; set; }
    }
}
