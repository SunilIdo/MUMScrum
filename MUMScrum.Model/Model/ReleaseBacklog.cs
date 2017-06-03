using MUMScrum.Model.BaseEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUMScrum.Model.Model
{
    public class ReleaseBacklog: BaseEntity<int>
    {
        [Display(Name = "Release Backlog Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserStory> UserStoryList { get; set; }
        public virtual ICollection<Sprint> SprintList { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project project { get; set; }
    }
}