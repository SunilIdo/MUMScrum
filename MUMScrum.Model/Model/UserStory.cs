using MUMScrum.Model.BaseEntity;
using MUMScrum.Model.ENUMS;
using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Model.Model
{
    public class UserStory: BaseEntity<int>
    {
        [Display(Name = "User Story")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "User Story Type")]
        public UserStoryTypeENUM UserStoryType { get; set; }
        public WorkStatusEnum Status { get; set; }
        public int? DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]

        [Display(Name = "Developer")]
        public virtual Employee Developer { get; set; }
        public int? TesterId { get; set; }
        [ForeignKey("TesterId")]

        [Display(Name = "Tester")]
        public virtual Employee Tester { get; set; }
        [Display(Name = "Developer Estimate")]
        public decimal? DeveloperEstimate { get; set; }
        [Display(Name = "Tester Estimate")]
        public decimal? TesterEstimate { get; set; }
        public int? ReleaseBacklogId { get; set; }
        [ForeignKey("ReleaseBacklogId")]

        [Display(Name = "Release Backlog")]
        public virtual ReleaseBacklog ReleaseBacklog { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project project { get; set; }
        public int? SprintId { get; set; }
        [ForeignKey("SprintId")]
        public virtual Sprint Sprint { get; set; }

    }
}
