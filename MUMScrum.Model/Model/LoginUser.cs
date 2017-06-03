using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.ENUMS;

namespace MUMScrum.Model.Model
{
    public class LoginUser
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        public int EmployeeId { get; set; }
        public RoleEnum RoleId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
