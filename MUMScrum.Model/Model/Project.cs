using MUMScrum.Model.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Model.Model
{
    public class Project:BaseEntity<int>
    {
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Product Owner")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]

        [Display(Name = "Product Owner")]
        public virtual Employee ProductOwner { get; set; }
        public virtual ICollection<ReleaseBacklog> ReleaseBacklog { get; set; }
        public virtual ICollection<UserStory> userStoryList { get; set; }
    }
}
