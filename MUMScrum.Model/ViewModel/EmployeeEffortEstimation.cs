using MUMScrum.Model.ENUMS;
using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Model.ViewModel
{
    public class EmployeeEffortEstimation
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal? Effort { get; set; }
        public int UserStoryId { get; set; }
        public EmployeeEstimateENUM EstimateType { get; set; }

        public virtual UserStory userStory { get; set; }
        public virtual Employee employee { get; set; }

    }
}
