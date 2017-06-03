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
    public class Sprint:BaseEntity<int>
    {
        [Display(Name = "Sprint Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime DueDate { get; set; }
        public virtual ICollection<UserStory> UserStoryList { get; set; }
        public int ReleaseBacklogId { get; set; }
        [ForeignKey("ReleaseBacklogId")]
        public virtual ReleaseBacklog ReleaseBacklog { get; set; }
    }
}
