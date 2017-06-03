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
    public class WorkLog:BaseEntity<int>
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Employee User { get; set; }
        [Display(Name = "Work Completed")]
        public decimal? WorkCompleted { get; set; }
        public int UserStoryId { get; set; }
        [ForeignKey("UserStoryId")]
        public virtual UserStory userStory { get; set; }
    }
}
